# Dockerfile multi-stage pour SQL Server + API .NET

# Stage 1: Build de l'application .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier projet et restaurer les dépendances
COPY ServiceLigueHockeySqlServer/ServiceLigueHockeySqlServer.csproj ServiceLigueHockeySqlServer/
RUN dotnet restore ServiceLigueHockeySqlServer/ServiceLigueHockeySqlServer.csproj

# Copier tout le code source et compiler
COPY ServiceLigueHockeySqlServer/ ServiceLigueHockeySqlServer/
RUN dotnet build ServiceLigueHockeySqlServer/ServiceLigueHockeySqlServer.csproj -c Release -o /app/build

# Publier l'application
RUN dotnet publish ServiceLigueHockeySqlServer/ServiceLigueHockeySqlServer.csproj -c Release -o /app/publish

# Stage 2: Image finale avec SQL Server
FROM mcr.microsoft.com/mssql/server:2022-latest

# Installer .NET Runtime et ASP.NET Core Runtime pour l'API
USER root
RUN apt-get update && \
    apt-get install -y wget && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y aspnetcore-runtime-8.0 dotnet-runtime-8.0 && \
    rm -rf /var/lib/apt/lists/* && \
    rm packages-microsoft-prod.deb

# Créer le répertoire pour l'application .NET
RUN mkdir -p /app

# Copier l'application .NET depuis le stage de build
COPY --from=build /app/publish /app/

RUN echo $MSSQL_PID

# Créer le script d'initialisation de la base de données
RUN echo '#!/bin/bash' > /init-db.sh && \
    echo 'echo "Création de l''utilisateur application et de la base de données..."' >> /init-db.sh && \
    echo '/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -C -Q "CREATE LOGIN [lemste] WITH PASSWORD = '"'"'Misty@00'"'"'; CREATE DATABASE [LigueHockey]; USE [LigueHockey]; CREATE USER [lemste] FOR LOGIN [lemste]; ALTER ROLE [db_owner] ADD MEMBER [lemste];GO"' >> /init-db.sh && \
    echo 'echo "Base de données initialisée."' >> /init-db.sh && \
    chmod +x /init-db.sh

# Créer le script de démarrage principal
RUN echo '#!/bin/bash' > /start.sh && \
    echo 'echo "Démarrage de SQL Server..."' >> /start.sh && \
    echo '/opt/mssql/bin/sqlservr &' >> /start.sh && \
    echo 'echo "Attente du démarrage de SQL Server..."' >> /start.sh && \
    echo 'sleep 30' >> /start.sh && \
    echo 'if [ ! -f "/var/opt/mssql/.db-initialized" ]; then' >> /start.sh && \
    echo '    echo "Première exécution - initialisation de la base de données..."' >> /start.sh && \
    echo '    /init-db.sh' >> /start.sh && \
    echo '    touch /var/opt/mssql/.db-initialized' >> /start.sh && \
    echo 'else' >> /start.sh && \
    echo '    echo "Base de données déjà initialisée."' >> /start.sh && \
    echo 'fi' >> /start.sh && \
    echo 'echo "Démarrage de l''API .NET..."' >> /start.sh && \
    echo 'cd /app' >> /start.sh && \
    echo 'echo "Vérification des runtimes disponibles :"' >> /start.sh && \
    echo 'dotnet --list-runtimes' >> /start.sh && \
    echo 'echo "Vérification des fichiers de l''application :"' >> /start.sh && \
    echo 'ls -la' >> /start.sh && \
    echo 'echo "Contenu du fichier .runtimeconfig.json :"' >> /start.sh && \
    echo 'cat *.runtimeconfig.json 2>/dev/null || echo "Fichier .runtimeconfig.json non trouve"' >> /start.sh && \
    echo 'echo "\nTentative de démarrage de l''application..."' >> /start.sh && \
    echo 'dotnet ServiceLigueHockeySqlServer.dll &' >> /start.sh && \
    echo 'wait' >> /start.sh && \
    chmod +x /start.sh

# Exposer les ports
EXPOSE 1433 5246

# Créer le point de montage pour la persistance des données
VOLUME ["/var/opt/mssql"]

# Script de démarrage
CMD ["/start.sh"]