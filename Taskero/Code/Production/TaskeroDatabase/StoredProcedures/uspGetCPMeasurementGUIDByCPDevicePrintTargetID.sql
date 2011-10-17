IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCPMeasurementGUIDByCPDevicePrintTargetID')
	BEGIN
		DROP  Procedure [CPV].[uspGetCPMeasurementGUIDByCPDevicePrintTargetID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : Aug 12 2008

Purpose : This retrieves the CPMeasurementGuid and Status with CPDevicePrintTargetID
*/
CREATE PROCEDURE [CPV].[uspGetCPMeasurementGUIDByCPDevicePrintTargetID]( @CPDevicePrintTargetID INT ) 
AS  
BEGIN
	BEGIN TRY
		
		/*---------- This retrieves the CPMeasurementGuid with DeviceID ----------*/
		SELECT 
			CPMeasurement.CPMeasurementGUID 
			, CPMeasurement.Status
		FROM 
			CPMeasurement 	
			INNER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID 	
		WHERE 
			CPMeasurement_PT.CPDevicePrintTargetID = @CPDevicePrintTargetID
			AND ISNULL(IsUsed,1) = 1
		ORDER BY
			CPMeasurement.CreatedDT
		
	END TRY
	BEGIN CATCH
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC CPV.uspInsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 
	END CATCH
END

