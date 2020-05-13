
GO
/****** Object:  StoredProcedure [CP].[GetMeasurementDeviceList]    Script Date: 5/13/2020 11:27:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [CP].[GetMeasurementDeviceList]  
 @LoginID INT
,@DeviceID INT  
,@IsSpotTargetType BIT
AS
IF @DeviceID != 0
BEGIN
	IF @IsSpotTargetType = 1 ---- If Spot Target projects, then only display i1-IO, i1-IO-Pro2 and i1-IO-Pro3 devices
	BEGIN
		SELECT   
			MD.MeasurementDeviceID,  
			MD.Description,
			IT.InstrumentTypeID,
			IT.InstrumentType As InstrumentName,
			(IT.InstrumentType) +' - ' + (MD.SerialNumber) AS MeasurementDevi  
		 FROM  
		  CP.Device DV INNER JOIN CP.MeasurementDevice MD  
		   ON DV.LicenseID = MD.LicenseID  
		  INNER JOIN CP.InstrumentType IT  
		   ON MD.InstrumentTypeID = IT.InstrumentTypeID  
		 WHERE  
		  DV.DeviceID = @DeviceID AND MD.IsActive = 1 AND IT.InstrumentTypeID IN (3,5,10)
	END
	ELSE
	BEGIN
		SELECT   
			MD.MeasurementDeviceID,  
			MD.Description,
			IT.InstrumentTypeID,
			IT.InstrumentType As InstrumentName,
			(IT.InstrumentType) +' - ' + (MD.SerialNumber) AS MeasurementDevi  
		 FROM  
		  CP.Device DV INNER JOIN CP.MeasurementDevice MD  
		   ON DV.LicenseID = MD.LicenseID  
		  INNER JOIN CP.InstrumentType IT  
		   ON MD.InstrumentTypeID = IT.InstrumentTypeID  
		 WHERE  
		  DV.DeviceID = @DeviceID AND MD.IsActive = 1
	END
	
END
ELSE
BEGIN

	DECLARE @CompanyID INT;
	DECLARE @BranchID INT;

	SELECT @CompanyID = UC.CompanyID FROM CP.UserCompany UC INNER JOIN CP.Company C ON UC.CompanyID = C.CompanyID WHERE UserID = @LoginID
		
	SELECT @BranchID = UB.BranchID FROM CP.UserBranch UB INNER JOIN CP.Branch B ON UB.BranchID = B.BranchID WHERE UserID = @LoginID

	SELECT 
		MD.MeasurementDeviceID,  
		MD.Description,
		IT.InstrumentTypeID,
		IT.InstrumentType As InstrumentName,
		(IT.InstrumentType) +' - ' + (MD.SerialNumber) AS MeasurementDevi

		FROM
			CP.Company INNER JOIN CP.Branch 
				ON CP.Company.CompanyID = CP.Branch.CompanyID
			INNER JOIN CP.License L
				ON CP.Branch.BranchID = L.BranchID
			INNER JOIN CP.MeasurementDevice MD
				ON L.LicenseID = MD.LicenseID
			INNER JOIN CP.InstrumentType IT
				ON IT.InstrumentTypeID = MD.InstrumentTypeID
		WHERE
			MD.IsActive = 1 AND
			MD.InstrumentTypeID = 4 AND
			(ISNULL(@CompanyID,0) = 0 OR Branch.CompanyID = @CompanyID) AND
			(ISNULL(@BranchID,0) = 0 OR Branch.BranchID = @BranchID) AND
			CP.CheckUserCompany(@LoginID,CP.Company.CompanyID) = 1 AND
			CP.CheckUserBranch(@LoginID,CP.Branch.BranchID) = 1

END



