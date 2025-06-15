@echo off
setlocal enabledelayedexpansion

:: Colores (estos solo funcionan uno a la vez en toda la consola)
:: Verde claro: 0A
:: Rojo claro: 0C
:: Amarillo claro: 0E
:: Blanco (por defecto): 07

:: Check for Chocolatey
where choco >nul 2>nul
if %errorlevel% neq 0 (
    call :install_chocolatey
    goto :MainScript
) else (
    color 0A
    echo Chocolatey ya esta instalado.
    color 07
)

:: Check for MySQL
where mysql >nul 2>nul
if %errorlevel% neq 0 (
    call :install_mysql
    goto :MainScript
)

:: Check for dotnet
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    call :install_dotnet
    goto :MainScript
)

goto :MainScript

:: Función: Instalar Chocolatey
:install_chocolatey
color 0E
echo Chocolatey no esta instalado. Instalando...
powershell -Command "Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))"
if %errorlevel% neq 0 (
    color 0C
    echo Error: No se pudo instalar Chocolatey.
    pause
    exit /b
)
color 07
goto :eof

:: Función: Instalar MySQL
:install_mysql
color 0E
echo El comando mysql no se encuentra. Instalando MySQL...
choco install mysql -y
choco install mysql-cli -y
if %errorlevel% neq 0 (
    color 0C
    echo Error: No se pudo instalar MySQL.
    pause
    exit /b
)
color 07
goto :eof

:: Función: Instalar .NET SDK
:install_dotnet
color 0E
echo El comando dotnet no se encuentra. Instalando .NET 8.0 SDK...
choco install dotnet-8.0-sdk -y
if %errorlevel% neq 0 (
    color 0C
    echo Error: No se pudo instalar .NET.
    pause
    exit /b
)
color 07
goto :eof

:MainScript
:: Variables de base de datos
set DB_HOST=localhost
set DB_ROOT_USER=root
set DB_USER=fituser
set DB_PASSWORD=fitpassword
set DB_NAME=FitTrack_Database
set SQL_FILE=%~dp0db\database.sql

:: Crear usuario de MySQL
color 0E
echo Creando usuario de MySQL y asignando privilegios...
echo CREATE USER IF NOT EXISTS '%DB_USER%'@'localhost' IDENTIFIED BY '%DB_PASSWORD%'; GRANT ALL PRIVILEGES ON %DB_NAME%.* TO '%DB_USER%'@'localhost'; FLUSH PRIVILEGES; > create_user.sql

mysql -u %DB_ROOT_USER% < create_user.sql > mysql_output.log 2>&1

for %%A in (mysql_output.log) do set FILESIZE=%%~zA
if %FILESIZE%==0 (
    color 0A
    echo Usuario creado correctamente.
) else (
    color 0E
    echo Parece que MySQL requiere contraseña de root. Reintentando...
    mysql -u %DB_ROOT_USER% -p < create_user.sql > mysql_output.log 2>&1
)

if %errorlevel% neq 0 (
    color 0C
    echo Error: No se pudo crear el usuario o asignar privilegios.
    pause
    exit /b
)

del create_user.sql

color 0A
echo Usuario creado y privilegios asignados.
mysql -u root -e "SELECT user, host FROM mysql.user WHERE user = 'fituser';"

:: Ejecutar script de base de datos
mysql -h %DB_HOST% -u %DB_USER% -p%DB_PASSWORD% < %SQL_FILE%
if %errorlevel% neq 0 (
    color 0C
    echo Error: No se pudo ejecutar el script de la base de datos.
    pause
    exit /b
)

color 0A
echo Script de base de datos ejecutado con exito.
color 07
pause
