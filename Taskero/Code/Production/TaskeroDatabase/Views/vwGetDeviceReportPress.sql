IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetDeviceReportPress')
	BEGIN
		DROP  View [Press].[vwGetDeviceReportPress]
	END
GO
/*
	Name :- Maulik Dhorajia
	Created On :- Aug 25 2008
	Description :- Created to get the records for All devices.
*/
CREATE View [Press].[vwGetDeviceReportPress] AS

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

	, NewID() AS [GUID]
	, 0 AS CPDevicePrintTargetID
	, '' AS PrintTarget 
	, ISNULL(vwTopJobID.JobName , '') AS Job 
	, ISNULL( vwTopJobID.JobID , 0 ) AS CPMeasurementID
	, Customer
	, vwTopJobID.CreatedDT
	, ISNULL(vwTopJobID.CreatedDT, '') as [DateTime]
	, CONVERT( VARCHAR , vwTopJobID.CreatedDT , 101 ) as [Date] 
	, CONVERT( VARCHAR , vwTopJobID.CreatedDT , 108 ) as [TIME] 
	, (CASE ISNULL( vwTopJobID.JobID , 0 ) WHEN 0 THEN 0 ELSE 1 END) AS FlgOpenMeasurementWindow
	, ISNULL(vwLastStatus.CPDeviceStatusTypeID, 0) AS StatusID 
	, ISNULL(vwLastStatus.StatusDesc, 'None') AS Status

FROM
	CPDevice
	INNER JOIN CPDongle ON CPDongle.CPDongleID = CPDevice.CPDongleID AND ISNULL( CPDongle.IsUsed , 1 ) = 1
	INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID AND ISNULL( Branch.IsActive , 1 ) = 1
	INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
	INNER JOIN CPDeviceType ON CPDeviceType.CPDeviceTypeID = CPDevice.CPDeviceTypeID
	LEFT OUTER JOIN CPV.vwGetLastStatusPerDevice AS vwLastStatus ON vwLastStatus.CPDeviceID = CPDevice.CPDeviceID 
	LEFT OUTER JOIN (SELECT PJ1.JobID , PJ1.JobName , PJ1.JobCreatedDt as CreatedDt , PJ1.Customer , PPI1.PressID , PPI1.CPDeviceID FROM Press.Jobs PJ1 INNER JOIN (SELECT PJ2.PressID , MAX(PJ2.JobID) AS JobID FROM Press.Jobs PJ2 GROUP BY PJ2.PressID) AS vwGetTopJobID ON vwGetTopJobID.JobID = PJ1.JobID INNER JOIN Press.PressInfo AS PPI1 ON PPI1.PressID = vwGetTopJobID.PressID ) AS vwTopJobID ON vwTopJobID.CPDeviceID = CPDevice.CPDeviceID		
WHERE
	CPDeviceType.CPDeviceTypeID = 5 


GO
