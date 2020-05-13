------------------------------------------ALTER TABLE COLUMN---------------------------------
ALTER TABLE CP.License 
ALTER COLUMN IsCustomPlugin TINYINT;
----------------------------------------------------------------------------------------
--------------------In The table if Custom plugin iS SYNC Measure APP measns it is set value 2

UPDATE CP.License 
SET IsCustomPlugin=2
WHERE IsCustomPlugin=1


-------------------------------------------------------------------------------------------------------
/* We set NPAPI Plugin is  in column we set "1" those column except "SYNC MEASURE APP"
*/
UPDATE CP.License 
SET IsCustomPlugin=1
WHERE IsCustomPlugin != 2
