-- Migration script to add user_id foreign key to activities table
-- This makes activities unique per user
USE `FitTrack_Database`;
-- Step 1: Add user_id column as nullable first
ALTER TABLE `activities`
ADD COLUMN `user_id` INT NULL
AFTER `id`;
-- Step 2: Update existing activities to assign them to the first user (if exists)
-- You can change this to assign to a specific user or handle differently based on your needs
UPDATE `activities`
SET `user_id` = (
        SELECT MIN(id)
        FROM `users`
        LIMIT 1
    )
WHERE `user_id` IS NULL;
-- Step 3: Make the column NOT NULL now that all rows have values
ALTER TABLE `activities`
MODIFY COLUMN `user_id` INT NOT NULL;
-- Step 4: Add foreign key constraint
ALTER TABLE `activities`
ADD CONSTRAINT `fk_activities_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
-- Step 5: Add index for better performance
ALTER TABLE `activities`
ADD INDEX `idx_activities_user_id` (`user_id` ASC);