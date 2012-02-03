IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendDetails')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLTrendDetails]
	END

GO

-- =============================================
-- Author:	Maulik Dhorajia
-- Create date: 07/25/2008
-- Description:	Gets all data for Trend Chart Line Details Page
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDTLTrendDetails]
( 
	@CPMeasurementGUID VARCHAR(50) , 
	@CPDeviceID INT , 
	@CPDevicePrintTargetID INT 
)
AS
BEGIN
BEGIN TRY

		/*=========== CPMeasurement ===========*/
		SELECT 
			[USER] , Customer , Info_Text 
		FROM 
			CPMeasurement
		WHERE 
			CPMeasurementGUID = @CPMeasurementGUID

		/*=========== CPMeasurement_Printer ===========*/
		SELECT 
			Printer_User , Printer_Host , Printer_Time , Printer_Paper 
			, Printer_CC_Table , Printer_ICC_Target
			, Printer_InkValue_1 , Printer_InkValue_2 , Printer_InkValue_3 , Printer_InkValue_4 , Printer_InkValue_5
		FROM 
			CPMeasurement_Printer
		WHERE 
			CPMeasurementGUID = @CPMeasurementGUID

		/*=========== CPDevicePrintTarget ===========*/
		SELECT 
			TOP 1 TargetTemp , TempF , TargetRH , RH
		FROM 
			TempRHDevice 
			INNER JOIN TempRHData ON TempRHData.TempRHDeviceID = TempRHDevice.TempRHDeviceID
			INNER JOIN TempRHDevice_CPDevice ON TempRHDevice.TempRHDeviceID = TempRHDevice_CPDevice.TempRHDeviceID
		WHERE 
			TempRHDevice_CPDevice.CPDeviceID = @CPDeviceID

		/*=========== CPMeasurement_DP ===========*/
		SELECT 
			Ink , Substrate , Chemistry
		FROM
			CPMeasurement_DP
		WHERE
			CPMeasurementGUID = @CPMeasurementGUID

		/*=========== CPDevice ===========*/
		SELECT 
			CPDeviceDesc
		FROM
			CPDevice
		WHERE CPDeviceID = @CPDeviceID		

		/*=========== CPDevicePrintTarget ===========*/
		SELECT 
			PrintTarget
		FROM
			CPDevicePrintTarget 
		WHERE 
			CPDevicePrintTargetID = @CPDevicePrintTargetID
			
		/*=========== CPDevicePrintTarget ===========*/
		SELECT 
			(Instrument +', '+ DeviceSerialNum) AS Spectro
		FROM
			CPMeasurement_MD 
		WHERE 
			CPMeasurementGUID = @CPMeasurementGUID


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


