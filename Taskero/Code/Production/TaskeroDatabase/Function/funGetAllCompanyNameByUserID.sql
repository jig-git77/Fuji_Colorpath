IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'funGetAllCompanyNameByUserID')
	BEGIN
		DROP  FUNCTION [CPV].[funGetAllCompanyNameByUserID]
	END
GO

CREATE FUNCTION [CPV].[funGetAllCompanyNameByUserID](@TaskeroUserID INT)          
RETURNS VARCHAR(1000)          
AS          
BEGIN          
 DECLARE @CompanyName VARCHAR(50)          
	DECLARE @AllCompanyName VARCHAR(1000)          
	DECLARE GetService CURSOR FOR Select CompanyName FROM [CPV].[vwGetCompanyByUserID] WHERE TaskeroUserID = @TaskeroUserID  
		OPEN GetService          
			FETCH NEXT FROM GetService INTO @CompanyName          
			WHILE @@FETCH_STATUS = 0          
			BEGIN          
				SET @AllCompanyName = ISNULL(@AllCompanyName + ', ', '') + @CompanyName          
					FETCH NEXT FROM GetService INTO @CompanyName          
			END           
		CLOSE GetService          
		DEALLOCATE GetService          
	RETURN @AllCompanyName        
END     