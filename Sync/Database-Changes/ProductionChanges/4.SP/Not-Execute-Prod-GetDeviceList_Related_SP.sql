/****** Object:  StoredProcedure [CPSync].[GetDeviceListForCertified1DProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSync].[GetDeviceListForCertified1DProjects]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CPSync].[GetDeviceListForCertified1DProjects]
GO
/****** Object:  StoredProcedure [CPSpot].[GetDeviceListForReTarget1DProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSpot].[GetDeviceListForReTarget1DProjects]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CPSpot].[GetDeviceListForReTarget1DProjects]
GO
/****** Object:  StoredProcedure [CPSpot].[GetDeviceListForCertifiedSpotProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSpot].[GetDeviceListForCertifiedSpotProjects]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CPSpot].[GetDeviceListForCertifiedSpotProjects]
GO
/****** Object:  StoredProcedure [CPSpot].[GetDeviceListForCertifiedSpotProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSpot].[GetDeviceListForCertifiedSpotProjects]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [CPSpot].[GetDeviceListForCertifiedSpotProjects] AS' 
END
GO
-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-08-13
-- Description:	Certied 
-- =============================================
/*
	EXEC [CPSpot].[GetDeviceListForCertifiedSpotProjects] 
	@CompanyID = 1,
	@BranchID=47,
	@CurrentUserID=1
*/
ALTER PROCEDURE [CPSpot].[GetDeviceListForCertifiedSpotProjects] 
@CompanyID INT,
@BranchID INT,
@CurrentUserID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT
		CP.Device.DeviceID,
		CP.Device.DeviceName		
	FROM
		CP.Company INNER JOIN CP.Branch 
			ON CP.Company.CompanyID = CP.Branch.CompanyID
		INNER JOIN CP.License 
			ON CP.Branch.BranchID = CP.License.BranchID
		INNER JOIN CP.Device 
			ON cp.License.LicenseID = cp.Device.LicenseID
		INNER JOIN cp.DeviceFamily 
			ON CP.Device.DeviceFamilyID = CP.DeviceFamily.DeviceFamilyID
		INNER JOIN CPSpot.SpotProject
			ON CP.Device.DeviceID = CPSpot.SpotProject.CPDeviceID
	WHERE
		CP.Device.IsActive = 1 AND
		CP.DeviceFamily.IsActive = 1 AND
		(ISNULL(@CompanyID,0) = 0 OR Branch.CompanyID = @CompanyID) AND
		(ISNULL(@BranchID,0) = 0 OR Branch.BranchID = @BranchID) AND
		CP.CheckUserCompany(@CurrentUserID,CP.Company.CompanyID) = 1 AND
		CP.CheckUserBranch(@CurrentUserID,CP.Branch.BranchID) = 1 AND
		ISNULL((
				SELECT TOP 1 SR.STATUS
				FROM CPSpot.OptimizeRun AS SR
				WHERE SR.SpotProjectID = CPSpot.SpotProject.SpotProjectID
				ORDER BY SR.OptimizeRunID DESC
				), 1) = 3
END



GO
/****** Object:  StoredProcedure [CPSpot].[GetDeviceListForReTarget1DProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSpot].[GetDeviceListForReTarget1DProjects]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [CPSpot].[GetDeviceListForReTarget1DProjects] AS' 
END
GO
-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-08-13
-- Description:	Certied 
-- =============================================
/*
	EXEC [CPSpot].[GetDeviceListForReTarget1DProjects] 
	@CompanyID = 1,
	@BranchID=47,
	@CurrentUserID=1
*/
ALTER PROCEDURE [CPSpot].[GetDeviceListForReTarget1DProjects] 
@CompanyID INT,
@BranchID INT,
@CurrentUserID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT
		CP.Device.DeviceID,
		CP.Device.DeviceName		
	FROM
		CP.Company INNER JOIN CP.Branch 
			ON CP.Company.CompanyID = CP.Branch.CompanyID
		INNER JOIN CP.License 
			ON CP.Branch.BranchID = CP.License.BranchID
		INNER JOIN CP.Device 
			ON cp.License.LicenseID = cp.Device.LicenseID
		INNER JOIN cp.DeviceFamily 
			ON CP.Device.DeviceFamilyID = CP.DeviceFamily.DeviceFamilyID
		INNER JOIN CPSync4D.Project
			ON CP.Device.DeviceID = CPSync4D.Project.CPDeviceID
	WHERE
		CP.Device.IsActive = 1 AND
		CP.DeviceFamily.IsActive = 1 AND
		(ISNULL(@CompanyID,0) = 0 OR Branch.CompanyID = @CompanyID) AND
		(ISNULL(@BranchID,0) = 0 OR Branch.BranchID = @BranchID) AND
		CP.CheckUserCompany(@CurrentUserID,CP.Company.CompanyID) = 1 AND
		CP.CheckUserBranch(@CurrentUserID,CP.Branch.BranchID) = 1 AND
		ISNULL((
				SELECT TOP 1 CPSync.OptimizeRun.STATUS
				FROM CPSync.OptimizeRun
				WHERE CPSync.OptimizeRun.ProjectID = CPSync4D.Project.ProjectID
				ORDER BY CPSync.OptimizeRun.OptimizeRunID DESC
				), 1) = 3
END



GO
/****** Object:  StoredProcedure [CPSync].[GetDeviceListForCertified1DProjects]    Script Date: 2/20/2020 3:37:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CPSync].[GetDeviceListForCertified1DProjects]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [CPSync].[GetDeviceListForCertified1DProjects] AS' 
END
GO
-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-08-13
-- Description:	Certied 
-- =============================================
/*
	EXEC [CPSync].[GetDeviceListForCertified1DProjects]  
	@CompanyID = 1,
	@BranchID=1,
	@CurrentUserID=1
*/
ALTER PROCEDURE [CPSync].[GetDeviceListForCertified1DProjects] 
@CompanyID INT,
@BranchID INT,
@CurrentUserID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT
		CP.Device.DeviceID,
		CP.Device.DeviceName		
	FROM
		CP.Company INNER JOIN CP.Branch 
			ON CP.Company.CompanyID = CP.Branch.CompanyID
		INNER JOIN CP.License 
			ON CP.Branch.BranchID = CP.License.BranchID
		INNER JOIN CP.Device 
			ON cp.License.LicenseID = cp.Device.LicenseID
		INNER JOIN cp.DeviceFamily 
			ON CP.Device.DeviceFamilyID = CP.DeviceFamily.DeviceFamilyID
		INNER JOIN CPSync4D.Project
			ON CP.Device.DeviceID = CPSync4D.Project.CPDeviceID
	WHERE
		CP.Device.IsActive = 1 AND
		CP.DeviceFamily.IsActive = 1 AND
		(ISNULL(@CompanyID,0) = 0 OR Branch.CompanyID = @CompanyID) AND
		(ISNULL(@BranchID,0) = 0 OR Branch.BranchID = @BranchID) AND
		CP.CheckUserCompany(@CurrentUserID,CP.Company.CompanyID) = 1 AND
		CP.CheckUserBranch(@CurrentUserID,CP.Branch.BranchID) = 1 AND
		ISNULL((
				SELECT TOP 1 CPSync.OptimizeRun.STATUS
				FROM CPSync.OptimizeRun
				WHERE CPSync.OptimizeRun.ProjectID = CPSync4D.Project.ProjectID
				ORDER BY CPSync.OptimizeRun.OptimizeRunID DESC
				), 1) = 3
END



GO