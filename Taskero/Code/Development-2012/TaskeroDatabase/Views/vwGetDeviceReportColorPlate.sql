IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetDeviceReportColorPlate')
	BEGIN
		DROP  View [CPV].[vwGetDeviceReportColorPlate]
	END
GO
/*
	Name :- Maulik Dhorajia
	Created On :- Aug 25 2008
	Description :- Created to get the records for All devices.
*/
CREATE View [CPV].[vwGetDeviceReportColorPlate] AS

	
SELECT 
	ISNULL(CPDevice.CPDeviceID, 0) AS CPDeviceID
	, ISNULL(CPDevice.CPDeviceDesc, '') AS CPDeviceDesc 
	, ISNULL(CPDongle.CPDongleID, 0) AS CPDongleID	
	, CPDongle.CPSerialNUM
	, ISNULL(CPDongle.DongleDescription, '') AS DongleDescription  
	, CASE ISNULL(CPDongle.DongleDescription,'') WHEN '' THEN CPDongle.CPSerialNUM ELSE ( CPDongle.CPSerialNUM +' - '+ CPDongle.DongleDescription) END AS DongleSerial 
	, ISNULL(Branch.BranchID, 0 ) AS BranchID
	, ISNULL(Branch.BranchName, '') AS BranchName
	, ISNULL(Company.CompanyID, 0) AS CompanyID
	, ISNULL(Company.CompanyName, '') AS CompanyName
	, ISNULL( CPDeviceType.CPDeviceTypeID , 0) as CPDeviceTypeID
	, ISNULL(CPDeviceType.CPDeviceTypeDesc, '') AS CPDeviceTypeDesc
	, vwTopGUID.CPMeasurementGUID AS [GUID]
	, ISNULL(vwTopGUID.CPDevicePrintTargetID, 0 ) AS CPDevicePrintTargetID
	, ISNULL(vwTopGUID.PrintTarget, '') AS PrintTarget 
	, ISNULL(vwTopGUID.Job, '') AS Job 
	, ISNULL(vwTopGUID.CPMeasurementID, 0) AS CPMeasurementID
	, ISNULL(vwTopGUID.Customer, '') AS Customer
	, ISNULL(vwTopGUID.CreatedDT, '') as [DateTime]
	, vwTopGUID.CreatedDT
	, CONVERT( VARCHAR , vwTopGUID.CreatedDT , 101 ) as [Date] 
	, CONVERT( VARCHAR , vwTopGUID.CreatedDT , 108 ) as [TIME] 
	, (CASE ISNULL( vwTopGUID.CPDevicePrintTargetID , '' ) WHEN '' THEN 0 ELSE 1 END) AS FlgOpenMeasurementWindow
	, ISNULL(vwLastStatus.CPDeviceStatusTypeID, 0) AS StatusID 
	, ISNULL(vwLastStatus.StatusDesc, 'None') AS Status

FROM
	CPDevice
	INNER JOIN CPDongle ON CPDongle.CPDongleID = CPDevice.CPDongleID AND ISNULL( CPDongle.IsUsed , 1 ) = 1
	INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID AND ISNULL( Branch.IsActive , 1 ) = 1
	INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
	INNER JOIN CPDeviceType ON CPDeviceType.CPDeviceTypeID = CPDevice.CPDeviceTypeID
	LEFT OUTER JOIN CPV.vwGetTopMeasurementGUIDPerDevice AS vwTopGUID ON vwTopGUID.CPDeviceID = CPDevice.CPDeviceID	
	LEFT OUTER JOIN CPV.vwGetLastStatusPerDevice AS vwLastStatus ON vwLastStatus.CPDeviceID = CPDevice.CPDeviceID 
WHERE
	CPDeviceType.CPDeviceTypeID < 5 

GO
