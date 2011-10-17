IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'funGetAllBranchNameByUserID')
	BEGIN
		DROP  FUNCTION [CPV].[funGetAllBranchNameByUserID]
	END
GO

CREATE FUNCTION [CPV].[funGetAllBranchNameByUserID](@TaskeroUserID INT)          
RETURNS VARCHAR(1000)          
AS          
BEGIN          
 DECLARE @BranchName VARCHAR(50)          
 DECLARE @AllBranchName VARCHAR(1000)          
 DECLARE GetService CURSOR FOR Select BranchName FROM [CPV].[vwGetBranchByUserID] WHERE TaskeroUserID = @TaskeroUserID  
 open GetService          
  FETCH NEXT FROM GetService INTO @BranchName          
   WHILE @@FETCH_STATUS = 0          
   BEGIN          
    SET @AllBranchName = ISNULL(@AllBranchName + ', ', '') + @BranchName          
    FETCH NEXT FROM GetService INTO @BranchName          
   END           
 CLOSE GetService          
 DEALLOCATE GetService          
 RETURN @AllBranchName          
END    