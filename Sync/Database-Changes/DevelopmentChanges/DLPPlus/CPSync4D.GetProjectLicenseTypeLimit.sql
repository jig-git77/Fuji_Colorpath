GO

/****** Object:  StoredProcedure [CPSync4D].[GetProjectLicenseTypeLimit]    Script Date: 12/30/2020 5:41:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-10-26
-- Description:	Get Project number limit in device
-- =============================================
/*
EXEC CPSync4D.GetProjectLicenseTypeLimit 16,1
*/

ALTER PROCEDURE [CPSync4D].[GetProjectLicenseTypeLimit]
 (@DeviceID INT
 ,@LicenseTypeID INT )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalLimit INT
	SET @TotalLimit=0

	SELECT @TotalLimit = COUNT(1) FROM [CPSync4D].[Project] PRJ
	INNER JOIN CPSync4D.PrintCondition PCON ON PCON.ProjectID = PRJ.ProjectID    
	INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
	INNER JOIN CPSync4D.SourceProfile SP ON SP.SourceProfileID = PST.SourceProfileID    		
	WHERE CPDeviceID = @DeviceID AND LicenseTypeID = @LicenseTypeID
	AND PRJ.IsUsed=1
	--AS Limit
	IF(@LicenseTypeID = 2 OR @LicenseTypeID = 11)
	BEGIN
		IF(@LicenseTypeID  = 2) -- IF DLP then we take DLP + DLPPlus Count
		BEGIN
			SET @TotalLimit = @TotalLimit +(SELECT COUNT(1) FROM [CPSync4D].[Project] PRJ
			INNER JOIN CPSync4D.PrintCondition PCON ON PCON.ProjectID = PRJ.ProjectID    
			INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
			INNER JOIN CPSync4D.SourceProfile SP ON SP.SourceProfileID = PST.SourceProfileID    		
			WHERE CPDeviceID = @DeviceID AND LicenseTypeID =11
			AND PRJ.IsUsed=1)
		END
		IF(@LicenseTypeID  = 11)-- IF DLPPlus then we take  DLPPlus+DLP Count
		BEGIN
			SET @TotalLimit = @TotalLimit +(SELECT  COUNT(1) FROM [CPSync4D].[Project] PRJ
			INNER JOIN CPSync4D.PrintCondition PCON ON PCON.ProjectID = PRJ.ProjectID    
			INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
			INNER JOIN CPSync4D.SourceProfile SP ON SP.SourceProfileID = PST.SourceProfileID    		
			WHERE CPDeviceID = @DeviceID AND LicenseTypeID = 2
			AND PRJ.IsUsed=1)
		END
	END
	SELECT @TotalLimit AS Limit 
END

GO


