------------------------------------------ALTER TABLE COLUMN---------------------------------
ALTER TABLE CP.License 
ALTER COLUMN IsCustomPlugin TINYINT;
----------------------------------------------------------------------------------------
--------------------In The table if Custom plugin iS NULL measns it is NAPPI Plugin value set

UPDATE CP.License 
SET IsCustomPlugin=1
WHERE IsCustomPlugin IS NULL

-------------------------------------------------------------------------------------------------------
/* If customer plugin is true in column we set "2" for "Sync Measure App"
*/
UPDATE CP.License 
SET IsCustomPlugin=2
WHERE IsCustomPlugin=1