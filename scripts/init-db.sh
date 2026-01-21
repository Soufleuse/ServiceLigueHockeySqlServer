#!/bin/bash
set -e

# Vérifier si déjà initialisé
if [ -f "/var/opt/mssql/.db-initialized" ]; then
    echo "Base de données déjà initialisée. Sortie."
    exit 0
fi

echo "Création de l'utilisateur application et de la base de données..."

# Attendre que SQL Server soit complètement démarré (max 2 minutes)
COUNTER=0
MAX_TRIES=24

until /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -Q "SELECT 1" > /dev/null 2>&1
do
  COUNTER=$((COUNTER+1))
  if [ $COUNTER -gt $MAX_TRIES ]; then
    echo "ERREUR: SQL Server n'a pas démarré après 2 minutes."
    exit 1
  fi
  echo "Attente de SQL Server... (tentative $COUNTER/$MAX_TRIES)"
  sleep 5
done

echo "SQL Server est prêt!"

# Créer la base de données et l'utilisateur
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -Q "
    IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'lemste')
    BEGIN
        CREATE LOGIN [lemste] WITH PASSWORD = 'Misty@00';
    END
    
    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'LigueHockey')
    BEGIN
        CREATE DATABASE [LigueHockey];
    END
    
    USE [LigueHockey];
    
    IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'lemste')
    BEGIN
        CREATE USER [lemste] FOR LOGIN [lemste];
        ALTER ROLE [db_owner] ADD MEMBER [lemste];
    END
"

# Marquer comme initialisé
touch /var/opt/mssql/.db-initialized
echo "Base de données initialisée avec succès."