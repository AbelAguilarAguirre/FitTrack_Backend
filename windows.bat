@echo off
setlocal enabledelayedexpansion

:: Check for Chocolatey
where choco >nul 2>nul
if %errorlevel% neq 0 (
    call :install_chocolatey
) else (
    echo Chocolatey is already installed.
)

:: Check for MySQL
where mysql >nul 2>nul
if %errorlevel% neq 0 (
    call :install_mysql
)

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

if %errorlevel% neq 0 (
    echo Error: No se pudo instalar MySQL.
    pause
    exit /b
)
goto :eof

:: Variables
set DB_HOST=localhost
set DB_USER=fituser
set DB_PASSWORD=fitpassword
set DB_NAME=FitTrack_Database
set SQL_FILE=db\database.sql

:: Run the SQL script
mysql -h %DB_HOST% -u %DB_USER% -p%DB_PASSWORD% %DB_NAME% < %SQL_FILE%

echo Database script executed successfully.
pause