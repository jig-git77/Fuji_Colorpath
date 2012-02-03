IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPrintTargetByDeviceID')
	BEGIN
		DROP  Procedure [CPV].[uspGetPrintTargetByDeviceID]
	END

GO

/*
AUTHOR : Amit Patel
Created Date : July 21 2008
Purpose : To get PrintTarget of the particular DeviceID
*/
CREATE PROCEDURE [CPV].[uspGetPrintTargetByDeviceID] 
( 
	@DeviceID INT 
) 
AS  
BEGIN
	BEGIN TRY

		/*----------- Gets the PrintTarget on DeviceID ----------*/
		SELECT
			CPDevicePrintTargetId, DeviceId, PrintTarget 
		FROM
			CPDevicePrintTarget 
		WHERE
			DeviceId = @DeviceID
		ORDER BY
			PrintTarget 

		
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


