@echo off
setlocal enabledelayedexpansion

:: Check for Chocolatey
where choco >nul 2>nul
if %errorlevel% neq 0 (
    call :install_chocolatey
    goto :MainScript
) else (
    echo Chocolatey is already installed.
)

:: Check for MySQL
where mysql >nul 2>nul
if %errorlevel% neq 0 (
    call :install_mysql
    goto :MainScript
)

goto :MainScript

:: Function to install Chocolatey
:install_chocolatey
echo Chocolatey no esta instalado. Instalandolo ahora...
powershell -Command "Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))"
if %errorlevel% neq 0 (
    echo Error: No se pudo instalar Chocolatey.
    pause
    exit /b
)
goto :eof

:: Function to install MySQL
:install_mysql
echo El comando mysql no se encuentra en el sistema.
echo Instalando MySQL...
choco install mysql -y
choco install mysql-cli -y
if %errorlevel% neq 0 (
    echo Error: No se pudo instalar MySQL.
    pause
    exit /b
)
goto :eof

:MainScript
:: Variables
set DB_HOST=localhost
set DB_ROOT_USER=root
set DB_USER=fituser
set DB_PASSWORD=fitpassword
set DB_NAME=FitTrack_Database
set SQL_FILE=%~dp0db\database.sql


:: Create the user and grant privileges
echo Creating MySQL user and granting privileges...
echo CREATE USER IF NOT EXISTS '%DB_USER%'@'localhost' IDENTIFIED BY '%DB_PASSWORD%'; GRANT ALL PRIVILEGES ON %DB_NAME%.* TO '%DB_USER%'@'localhost'; FLUSH PRIVILEGES; > create_user.sql

mysql -u %DB_ROOT_USER%  < create_user.sql
if %errorlevel% neq 0 (
    echo Error: No se pudo crear el usuario de MySQL o asignar privilegios.
    pause
    exit /b
)

del create_user.sql


echo: User created and privileges granted.
mysql -u root -e "SELECT user, host FROM mysql.user WHERE user = 'fituser';"


:: Run the SQL script as the new user

mysql -h %DB_HOST% -u %DB_USER% -p%DB_PASSWORD% < %SQL_FILE%


if %errorlevel% neq 0 (
    echo Error: No se pudo ejecutar el script de la base de datos.
    pause
    exit /b
)

echo Database script executed successfully.
pause