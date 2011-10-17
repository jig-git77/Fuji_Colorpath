IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetTopMeasurementGUIDPerDevice')
	BEGIN
		DROP  View [CPV].[vwGetTopMeasurementGUIDPerDevice]
	END
GO
-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 08/20/2008
-- Description:	Gets the list of CPMeasurement Per Device(Last Reading).
-- =============================================
CREATE View [CPV].[vwGetTopMeasurementGUIDPerDevice] AS

	SELECT 
			CPM1.CPDeviceID 
			, CPM2.CPMeasurementGUID 
			, CPM2.CPMeasurementID 
			, CPM2.Customer
			, CPM2.Job
			, CPM2.CreatedDT
			, CPDevicePrintTarget.CPDevicePrintTargetID
			, CPDevicePrintTarget.PrintTarget
		FROM CPMeasurement CPM1 
		LEFT OUTER JOIN CPMeasurement CPM2 ON CPM2.CPDeviceID = CPM1.CPDeviceID AND CPM2.CPMeasurementID = CPM1.CPMeasurementID 
		INNER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPM2.CPMeasurementGUID
		INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID AND CPDevicePrintTarget.DeviceID = CPM1.CPDeviceID
		WHERE CPM2.CPMeasurementID = ( SELECT MAX( CPM3.CPMeasurementID )  FROM CPMeasurement AS CPM3 WHERE CPM3.CPDeviceID = CPM1.CPDeviceID and ISNULL( CPM3.IsUsed , 1 ) = 1 )     
GO

