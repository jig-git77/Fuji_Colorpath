IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetJobReportsPress')
	BEGIN
		DROP  View [Press].[vwGetJobReportsPress]
	END
GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 08/26/2008
-- Description:	Will retrieve the information for Device Type : Press.
-- =============================================

CREATE View [Press].[vwGetJobReportsPress] AS

SELECT 
		(0) AS StatusID, 
		('') AS Status, 
		ISNULL(JobName, '') AS Job , 
		('') AS PrintTarget , 
		ISNULL(CPDeviceDesc, '') AS CPDeviceDesc , 
		ISNULL(DongleDescription, '') AS DongleDescription , 
		ISNULL(CPDeviceTypeDesc, '') AS CPDeviceTypeDesc, 
		ISNULL(JobCreatedDT, '') as [DateTime] ,		
		JobCreatedDT as CreatedDT,
		CONVERT( VARCHAR , JobCreatedDT , 101 ) as [Date] ,
		CONVERT( VARCHAR , JobCreatedDT , 108 ) as [TIME], 
		NewID() as [GUID] , 
		(PPressJobs.JobID) AS CPMeasurementID, 
		ISNULL(PPressJobs.Customer, '') AS Customer,	
		(0) AS CPDevicePrintTargetID,
		ISNULL(CPDevice.CPDeviceID, 0) AS CPDeviceID, 
		ISNULL(CPDongle.CPDongleID, 0) AS CPDongleID,
		ISNULL(Branch.BranchID, 0 ) AS BranchID, 
		ISNULL(Branch.BranchName, '') AS BranchName, 
		ISNULL(Company.CompanyID, 0) AS CompanyID, 
		ISNULL(Company.CompanyName, '') AS CompanyName, 
		ISNULL( CPDeviceType.CPDeviceTypeID , 0) as CPDeviceTypeID ,
		CPSerialNUM ,
		CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial	 
FROM 
	Company			
INNER JOIN Branch ON Branch.CompanyID = Company.CompanyID AND ISNULL(Branch.IsActive, 1 ) = 1 
INNER JOIN CPDongle ON CPDongle.BranchID = Branch.BranchID AND ISNULL(CPDongle.IsUsed, 1) = 1
INNER JOIN CPDevice ON CPDevice.CPDongleID = CPDongle.CPDongleID 
INNER JOIN CPDeviceType ON CPDeviceType.CPDeviceTypeID = CPDevice.CPDeviceTypeID 	
INNER JOIN Press.PressInfo PPressInfo ON PPressInfo.CPDeviceID = CPDevice.CPDeviceID
INNER JOIN Press.Jobs PPressJobs ON PPressJobs.PressID = PPressInfo.PressID AND ISNULL( PPressJobs.IsUsed , 1 ) = 1
WHERE CPDevice.CPDeviceTypeID = 5