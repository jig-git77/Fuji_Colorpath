IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetJobReportsColorPlate')
	BEGIN
		DROP  View [CPV].[vwGetJobReportsColorPlate]
	END
GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 08/26/2008
-- Description:	Will retrieve the information for Device Type : Color, Plate, Wide Format & Monitor.
-- =============================================

CREATE View [CPV].[vwGetJobReportsColorPlate] AS

SELECT 
	ISNULL(Status, 0) AS StatusID, 
	(CASE ISNULL(Status, 0) WHEN 0 THEN 'Failed' WHEN 1 THEN 'Caution' WHEN 2 THEN 'Passed'END ) AS Status, 
	ISNULL(Job, '') AS Job , 
	ISNULL(PrintTarget, '') AS PrintTarget , 
	ISNULL(CPDeviceDesc, '') AS CPDeviceDesc , 
	ISNULL(DongleDescription, '') AS DongleDescription , 
	ISNULL(CPDeviceTypeDesc, '') AS CPDeviceTypeDesc, 
	CreatedDT,
	ISNULL(CreatedDT, '') as [DateTime] ,
	CONVERT( VARCHAR , CreatedDT , 101 ) as [Date] ,
	CONVERT( VARCHAR , CreatedDT , 108 ) as [TIME], 
	ISNULL(CPMeasurement.CPMeasurementGUID, '') as [GUID] , 
	ISNULL(CPMeasurementID,0) AS CPMeasurementID, 
	ISNULL(CPMeasurement.Customer, '') AS Customer,	
	ISNULL(CPDevicePrintTarget.CPDevicePrintTargetID, 0) AS CPDevicePrintTargetID,
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
	INNER JOIN CPMeasurement ON CPMeasurement.CPDeviceID = CPDevice.CPDeviceID AND ISNULL(CPMeasurement.IsUsed, 1) = 1
	INNER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID	
	INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID
WHERE CPDevice.CPDeviceTypeID < 5