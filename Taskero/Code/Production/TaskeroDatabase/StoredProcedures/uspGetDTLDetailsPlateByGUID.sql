IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLDetailsPlateByGUID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetDTLDetailsPlateByGUID]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/29/2008
-- Description:	Details of Plate
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDTLDetailsPlateByGUID]
(
	@CPMeasurementGUID AS uniqueidentifier
)
AS
BEGIN
	BEGIN TRY
		 SELECT 
			  ISNULL(CPDevice.CPDeviceDesc, '') AS DeviceDesc, 
			  ISNULL(CPMeasurement.Customer, '') AS Customer, ISNULL(CPMeasurement.[User], '') as UserName,  
			  ISNULL(CPMeasurement.Info_Text, '') AS InfoText, 
			  ISNULL(CPMeasurement_OD.Manufacturer, '') AS Manufacturer, 
			  ISNULL(CPMeasurement_OD.Model, '') AS Model, 
			  ISNULL(CPMeasurement_OD.Serial_Num, '') AS OD_SerialNum, 
			  ISNULL(CPMeasurement_MD.Instrument, '') AS MF_Device, 
			  ISNULL(CPMeasurement_MD.DeviceSerialNum, '') AS MF_SerialNum, 
			  ISNULL(CPMeasurement_PT.Target_File, '') AS TargetFile, 
			  ISNULL(SUBSTRATE, '') AS SUBSTRATE, ISNULL(SUBSTRATE_SKU, '') AS SUBSTRATE_SKU,  
			  ISNULL(SUBSTRATE_BATCH, '') AS SUBSTRATE_BATCH, ISNULL(CHEMISTRY, '') AS CHEMISTRY, 
			  ISNULL(CHEMISTRY_SKU, '') AS CHEMISTRY_SKU, ISNULL(CHEMISTRY_BATCH,'') AS CHEMISTRY_BATCH  
		  FROM CPMeasurement 
			  LEFT OUTER JOIN CPMeasurement_DP ON CPMeasurement_DP.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID 
			  LEFT OUTER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID 
			  LEFT OUTER JOIN CPMeasurement_OD ON CPMeasurement_OD.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID 
			  LEFT OUTER JOIN CPMeasurement_MD ON CPMeasurement_MD.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID 
			  LEFT OUTER JOIN CPDevice ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID 
          WHERE CPMeasurement.CPMeasurementGUID = @CPMeasurementGUID 

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


