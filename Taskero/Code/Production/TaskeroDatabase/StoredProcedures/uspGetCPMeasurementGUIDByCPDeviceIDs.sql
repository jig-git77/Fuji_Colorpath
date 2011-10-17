IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCPMeasurementGUIDByCPDeviceIDs')
	BEGIN
		DROP  Procedure [CPV].[uspGetCPMeasurementGUIDByCPDeviceIDs]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 16 2008
Purpose : This retrieves the CPMeasurementGuid with DeviceIDs in below given pattern.
@CPDeviceIDs = 1,2,4,5,6
*/
CREATE PROCEDURE [CPV].[uspGetCPMeasurementGUIDByCPDeviceIDs]
( 
	@CPDeviceIDs VARCHAR(8000) 
) 
AS  
BEGIN
	BEGIN TRY

		DECLARE @xmlCPDeviceIDs XML
		SET @xmlCPDeviceIDs = '<CPDeviceIDs><CPDeviceID>' + REPLACE( @CPDeviceIDs , ',' , '</CPDeviceID><CPDeviceID>' ) + '</CPDeviceID></CPDeviceIDs>'		

		--SELECT xmlCPDeviceIDs.value('.', 'INT') AS CPDeviceID FROM @xmlCPDeviceIDs.nodes('/CPDeviceIDs/CPDeviceID') d(xmlCPDeviceIDs)

		/*---------- This retrieves the CPMeasurementGuid with DeviceID ----------*/
		SELECT 
			CPMeasurementGUID 
		FROM 
			CPMeasurement 
		WHERE 
			CPDeviceID IN ( SELECT xmlCPDeviceIDs.value('.', 'INT') AS CPDeviceID FROM @xmlCPDeviceIDs.nodes('/CPDeviceIDs/CPDeviceID') d(xmlCPDeviceIDs) )
		AND 
			ISNULL( IsUsed , 1 ) = 1
		ORDER BY
			CreatedDT
		
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


