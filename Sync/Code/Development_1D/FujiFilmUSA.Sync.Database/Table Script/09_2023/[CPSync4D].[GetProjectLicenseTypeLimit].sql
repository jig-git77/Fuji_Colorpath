
/****** Object:  StoredProcedure [CPSync4D].[GetProjectLicenseTypeLimit]    Script Date: 9/13/2023 11:16:52 AM ******/
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
	WHERE CPDeviceID = @DeviceID AND LicenseTypeID = @LicenseTypeID
	AND PRJ.IsUsed=1

	SELECT @TotalLimit AS Limit 
END

