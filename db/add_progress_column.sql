-- Migration script to add progress column to routine table
-- Run this script on your existing database to add the progress field
USE `FitTrack_Database`;
-- Add the progress column to the routine table
ALTER TABLE `routine`
ADD COLUMN `progress` DOUBLE NULL DEFAULT 0
AFTER `repetitions`;
-- Verify the column was added
DESCRIBE `routine`;