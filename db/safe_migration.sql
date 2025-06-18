-- Safe migration script to add user_id foreign key to activities table
-- This checks if changes are already applied
USE `FitTrack_Database`;
-- Check if user_id column exists, if not add it
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'FitTrack_Database'
    AND TABLE_NAME = 'activities'
    AND COLUMN_NAME = 'user_id';
-- Add user_id column if it doesn't exist
SET @sql = IF(
        @col_exists = 0,
        'ALTER TABLE `activities` ADD COLUMN `user_id` INT NULL AFTER `id`',
        'SELECT "Column user_id already exists" AS Status'
    );
PREPARE stmt
FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
-- Update existing activities to assign them to the first user (if exists and not already set)
UPDATE `activities`
SET `user_id` = (
        SELECT MIN(id)
        FROM `users`
        LIMIT 1
    )
WHERE `user_id` IS NULL;
-- Make the column NOT NULL if it exists and has data
SET @sql = IF(
        @col_exists = 0,
        'ALTER TABLE `activities` MODIFY COLUMN `user_id` INT NOT NULL',
        'SELECT "Column user_id already processed" AS Status'
    );
PREPARE stmt
FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
-- Check if foreign key constraint exists
SET @fk_exists = 0;
SELECT COUNT(*) INTO @fk_exists
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE CONSTRAINT_SCHEMA = 'FitTrack_Database'
    AND TABLE_NAME = 'activities'
    AND CONSTRAINT_NAME = 'fk_activities_user'
    AND CONSTRAINT_TYPE = 'FOREIGN KEY';
-- Add foreign key constraint if it doesn't exist
SET @sql = IF(
        @fk_exists = 0,
        'ALTER TABLE `activities` ADD CONSTRAINT `fk_activities_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE',
        'SELECT "Foreign key constraint already exists" AS Status'
    );
PREPARE stmt
FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
-- Check if index exists
SET @idx_exists = 0;
SELECT COUNT(*) INTO @idx_exists
FROM INFORMATION_SCHEMA.STATISTICS
WHERE TABLE_SCHEMA = 'FitTrack_Database'
    AND TABLE_NAME = 'activities'
    AND INDEX_NAME = 'idx_activities_user_id';
-- Add index if it doesn't exist
SET @sql = IF(
        @idx_exists = 0,
        'ALTER TABLE `activities` ADD INDEX `idx_activities_user_id` (`user_id` ASC)',
        'SELECT "Index already exists" AS Status'
    );
PREPARE stmt
FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
SELECT "Migration completed successfully!" AS Status;