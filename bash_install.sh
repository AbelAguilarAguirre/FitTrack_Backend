#!/bin/bash

set -e

# Variables
DB_HOST="localhost"
DB_ROOT_USER="root"
read -sp "Enter your root Password" DB_ROOT_PASSWORD
if [ -z "$DB_ROOT_PASSWORD" ]; then
    echo "Root password cannot be empty."
    exit 1
fi
DB_USER="fituser"
DB_PASSWORD="fitpassword"
DB_NAME="FitTrack_Database"
SQL_FILE="db/database.sql"



# Run the SQL script
mysql -h $DB_HOST -u $DB_USER -p$DB_PASSWORD $DB_NAME < $SQL_FILE

echo "Database script executed successfully."