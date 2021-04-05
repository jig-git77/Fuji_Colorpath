

/****** Object:  StoredProcedure [CP].[GetDevices]    Script Date: 4/5/2021 4:59:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [CP].[GetDevices]  
	@DeviceID INT,
	@CurrentUserID INT
AS  
BEGIN
	IF ISNULL(@DeviceID,0) = 0
	BEGIN
		SELECT   
			D.DeviceName  
			,DF.DeviceFamilyDesc  
			,B.BranchName  
			,C.CompanyName      
			,D.DeviceID  
			,D.LicenseID  
			,D.DeviceFamilyID  
			,C.CompanyId  
			,B.BranchId  			
			,D.IsActive  
			,D.IsValidation
			,ISNULL(D.IsFlexo,0) AS IsFlexo
			,ISNULL(L.IsCustomProfOptions,0) AS IsCustomProfOptions
			,ISNULL(L.IsCustomPlugin,0) AS IsCustomPlugin
			,ISNULL(D.IsSupportMulticolor,0) AS IsSupportMulticolor
			,ISNULL(D.TotalNoColor,0) AS TotalNoColor
			,ISNULL(D.Channel_5,0) AS Channel_5
			,ISNULL(D.Channel_6,0) AS Channel_6
			,ISNULL(D.Channel_7,0) AS Channel_7
			,ISNULL(D.Channel_8,0) AS Channel_8
			,ISNULL(D.Channel_9,0) AS Channel_9
		FROM CP.Device D INNER JOIN CP.DeviceFamily DF ON
			DF.DeviceFamilyID = D.DeviceFamilyID  
		INNER JOIN CP.License L ON
			L.LicenseID = D.LicenseID 
		INNER JOIN CP.Branch B
			ON B.BranchID = L.BranchID
		INNER JOIN CP.Company C
			ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
		WHERE
			CP.CheckUserBranch(@CurrentUserID,B.BranchID) = 1
		ORDER BY
			D.DeviceName  
		
		------ Query to get list of total limit of projects by License Types ----------
		SELECT
			DL.DeviceId,DL.LicenseTypeID, DL.NumOfProject
		FROM
			CP.DeviceLicenseType DL INNER JOIN CP.Device D ON
				DL.DeviceID = D.DeviceID
			INNER JOIN CP.License L ON
				L.LicenseID = D.LicenseID 
			INNER JOIN CP.Branch B
				ON B.BranchID = L.BranchID
			INNER JOIN CP.Company C
				ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
			WHERE
				CP.CheckUserBranch(@CurrentUserID,B.BranchID) = 1 AND DL.NumOfProject != 0
    END
    ELSE
    BEGIN
		SELECT   
			D.DeviceName  
			,DF.DeviceFamilyDesc  
			,B.BranchName  
			,C.CompanyName      
			,D.DeviceID  
			,D.LicenseID  
			,D.DeviceFamilyID  
			,C.CompanyId  
			,B.BranchId  			
			,D.IsActive  
			,D.IsValidation
			,ISNULL(D.IsFlexo,0) AS IsFlexo
			,ISNULL(L.IsCustomProfOptions,0) AS IsCustomProfOptions
			,ISNULL(L.IsCustomPlugin,0) AS IsCustomPlugin
			,ISNULL(D.IsSupportMulticolor,0) AS IsSupportMulticolor
			,ISNULL(D.TotalNoColor,0) AS TotalNoColor
			,ISNULL(D.Channel_5,0) AS Channel_5
			,ISNULL(D.Channel_6,0) AS Channel_6
			,ISNULL(D.Channel_7,0) AS Channel_7
			,ISNULL(D.Channel_8,0) AS Channel_8
			,ISNULL(D.Channel_9,0) AS Channel_9
		FROM CP.Device D INNER JOIN CP.DeviceFamily DF ON
			DF.DeviceFamilyID = D.DeviceFamilyID  
		INNER JOIN CP.License L ON
			L.LicenseID = D.LicenseID  
		INNER JOIN CP.Branch B
			ON B.BranchID = L.BranchID
		INNER JOIN CP.Company C
			ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
		WHERE
			D.DeviceID = @DeviceID


		------ Query to get list of total limit of projects by License Types ----------
		SELECT DeviceId,LicenseTypeId,NumOfProject FROM CP.DeviceLicenseType WHERE DeviceID = @DeviceID AND NumOfProject != 0

	
    END  
END






GO


