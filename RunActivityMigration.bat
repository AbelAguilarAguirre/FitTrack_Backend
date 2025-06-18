@echo off
echo Running migration to add user_id to activities table...
echo.
echo Make sure your MySQL server is running and the FitTrack_Database exists.
echo.
pause
echo Executing SQL script...
mysql -u root -p < db\add_user_to_activities.sql
echo.
echo Migration completed.
pause
