#!/bin/bash
set -e

echo "=== Démarrage du conteneur ==="

# Créer les répertoires nécessaires
mkdir -p /var/log/supervisor

# Démarrer Supervisor qui gérera tous les services
exec /usr/bin/supervisord -c /etc/supervisord.conf