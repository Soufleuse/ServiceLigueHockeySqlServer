# ServiceLigueHockeySqlServer
# C'est une version SQL Server de ServiceLigueHockey, simplement.
# Construire l'image
docker build --no-cache -t service-boutique-qc .
-- ou --
docker compose -f docker-compose.yml build --no-cache

# Rouler l'image.
# En dev
docker-compose -f docker-compose.yml up -d

# Stopper l'image
docker-compose -f docker-compose.yml down
