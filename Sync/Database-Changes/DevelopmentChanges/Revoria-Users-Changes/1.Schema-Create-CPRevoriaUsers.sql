IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'CPRevoriaUsers')
BEGIN
	EXEC('CREATE SCHEMA CPRevoriaUsers')
END
