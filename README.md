# ServiceLigueHockeySqlServer
# C'est une version SQL Server de ServiceLigueHockey, simplement.
# Construire l'image
docker build --no-cache -t service-boutique-qc .

# Rouler l'image.
# En dev
docker-compose -f docker-compose.yml up --build -d
