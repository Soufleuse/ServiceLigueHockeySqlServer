# Construire l'image
docker build --no-cache -t service-boutique-qc .
-- ou --
docker compose -f docker-compose.yml build --no-cache

# Rouler l'image.
# En dev
docker-compose -f docker-compose.yml up -d

# Stopper l'image
docker-compose -f docker-compose.yml down

# Fouilles-moi pourquoi, il y a deux problèmes avec ce projet.
# 1-Le script d'initialisation de BD n'a pas réussi à créer le user lemste ni
#   à l'assigner à LigueHockey; mais si je fais un docker exec sur le
#   conteneur pour ajouter ce user et à l'assigner, ça marche (!). 
# 2-Les migrations ne fonctionnent pas quand on laisse Program.cs s'en
#   charger; mais si on fait un dotnet ef database update avec une chaîne
#   de connexion qui pointe sur la bonne BD (et surtout le bon port),
#   ça marche.