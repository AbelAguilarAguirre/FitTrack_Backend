#!/bin/bash

# filepath: /home/sharedordaz/Documentos/BYUI/term3/dotnet/MyBackend/Backend/db/run_database.sh
echo "Enter your username:"
read DB_USER


# Variables
DB_HOST="localhost"
DB_USER="fituser"
DB_PASSWORD="fitpassword"
DB_NAME="FitTrack_Database"
SQL_FILE="db/database.sql"



# Run the SQL script
mysql -h $DB_HOST -u $DB_USER -p$DB_PASSWORD $DB_NAME < $SQL_FILE

echo "Database script executed successfully."