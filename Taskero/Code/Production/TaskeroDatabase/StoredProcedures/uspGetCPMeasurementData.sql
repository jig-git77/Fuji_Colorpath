IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCPMeasurementData')
	BEGIN
		DROP  Procedure [CPV].[uspGetCPMeasurementData]
	END

GO

/*  
AUTHOR : Amit Patel
Created Date : July 29 2008  
Purpose : To Get all the CPMeasurement Information BY CPMeasurementGUID.  
*/  

CREATE PROCEDURE [CPV].[uspGetCPMeasurementData]
( 
	@CPMeasurementGUID VARCHAR(36) 
) 
AS         
BEGIN
	BEGIN TRY
	
/* To Get all the CPMeasurement Information BY CPMeasurementGUID.  */

	SELECT     
		 CPMeasurement.CPMeasurementGUID As GUID
		,CPMeasurement.Originator As ORIGINATOR
		,CPMeasurement.CreatedDT As CREATED
		,CPMeasurement.CreatedGMT As CREATEDGMT
		,CPDevicePrintTarget.PrintTarget As TARGET_NAME
		,CPMeasurement_PT.Proof_Supplier As PROOF_SUPPLIER
		,CPMeasurement_PT.Target_File As TARGET_FILE
		,CPDongle.CPSerialNUM As CP_SERIAL_NUM
		,CPMeasurementType.CPMeasurementType As MEASUREMENT_TARGET
		,deltaEFormula.deltaEFormula As DE_FORMULA
		,CPMeasurement_TOL.Paper_Limit As PAPER_LIMIT
		,CPMeasurement_TOL.Average_Limit AS AVERAGE_LIMIT
		,CPMeasurement_TOL.Maximum_Limit As MAXIMUM_LIMIT
		,CPMeasurement_TOL.Primary_Limit As PRIMARY_LIMIT 
		,CPMeasurement_TOL.PRIMARY_LIMIT_DH AS  PRIMARY_LIMIT_DH
		,CPMeasurement_TOL.GRAY_AVERAGE_LIMIT_DH AS  GRAY_AVERAGE_LIMIT_DH
		,CPMeasurement_TOL.Dens_AV_Neg_Limit As DENS_AV_NEG_LIMIT
		,CPMeasurement_TOL.Dens_AV_Pos_Limit As DENS_AV_POS_LIMIT
		,CPMeasurement_TOL.Dens_Max_Neg_Limit As DENS_MAX_NEG_LIMIT
		,CPMeasurement_TOL.Dens_Max_Pos_Limit As DENS_MAX_POS_LIMIT
		,CPMeasurement_TOL.Paper_Strict As PAPER_STRICT
		,CPMeasurement_TOL.Average_Strict As AVERAGE_STRICT
		,CPMeasurement_TOL.Maximum_Strict As MAXIMUM_STRICT
		,CPMeasurement_TOL.Primary_Strict As PRIMARY_STRICT
		,CPMeasurement.CPMeasurementID As ID

		,CPMeasurement_ACT.Paper As PAPER
		,CPMeasurement_ACT.Average As AVERAGE
		,CPMeasurement_ACT.PatchMaximumdE As MAXIMUM_DE
		,CPMeasurement_ACT.PatchdEValue As MAXIMUM_DE_VALUE
		,CPMeasurement_ACT.PatchPrimarydE As PRIMARY_DE
		,CPMeasurement_ACT.PatchPrimarydEValue As PRIMARY_DE_VALUE
		, CONVERT(VARCHAR,CPMeasurement_ACT.Primary_Max_DH) +' '+ CPMeasurement_ACT.Primary_Max_DH_Patch AS Primary_DH 
		, CPMeasurement_ACT.Gray_Average_DH
		
		,CPMeasurement_ACT.Dens_AV_Neg As DENS_AV_NEG
		,CPMeasurement_ACT.Dens_AV_Pos As DENS_AV_POS
	    ,CPMeasurement_ACT.Dens_MAX_Neg As DENS_MAX_NEG
		,CPMeasurement_ACT.Dens_Max_Neg_Val As DENS_MAX_NEG_VAL
		,CPMeasurement_ACT.Dens_Max_Pos As DENS_MAX_POS
		,CPMeasurement_ACT.Dens_Max_Pos_Val As DENS_MAX_POS_VAL

		--,'0.0' As DENS_AV_NEG
		--,'0.0' As DENS_AV_POS
	    --,'0.0' As DENS_MAX_NEG
		--,'0.0' As DENS_MAX_NEG_VAL
		--,'0.0' As DENS_MAX_POS
		--,'0.0' As DENS_MAX_POS_VAL

		,CPMeasurement.Status As PROOF_OK --Proof_Status
		,CPMeasurement_MD.Instrument AS DEVICE
		,CPMeasurement_MD.DeviceSerialNum As DEVICE_SerialNo
		,CPMeasurement_MD.ObserverType As MEASUREMENT
		,CPMeasurement_MD.Illuminant As ILLUMINANT
		,CPMeasurement_MD.Filter As FILTER

		,CPMeasurement.[User] As 'USER'
		,CPMeasurement.Customer As CUSTOMER	
	

		,CPMeasurement_Printer.Printer_Model As PRINTER_MODEL
		,CPMeasurement_Printer.Printer_SerialNum As PRINTER_SERIAL
		,CPMeasurement_Printer.Printer_State As PRINTER_STATE
		,CPMeasurement_Printer.Printer_User As PRINTER_USER
		,CPMeasurement_Printer.Printer_Host As PRINTER_HOST
		,CPMeasurement_Printer.Printer_Time As 	PRINTER_TIME
		,CPMeasurement_Printer.Printer_InkName_0 As PRINTER_INK_NAME_0
		,CPMeasurement_Printer.Printer_InkValue_0 As PRINTER_INK_VALUE_0
		,CPMeasurement_Printer.Printer_InkName_1 As PRINTER_INK_NAME_1
		,CPMeasurement_Printer.Printer_InkValue_1 As PRINTER_INK_VALUE_1
		,CPMeasurement_Printer.Printer_InkName_2 As PRINTER_INK_NAME_2
		,CPMeasurement_Printer.Printer_InkValue_2 As PRINTER_INK_VALUE_2
		,CPMeasurement_Printer.Printer_InkName_3 As PRINTER_INK_NAME_3 
		,CPMeasurement_Printer.Printer_InkValue_3 As PRINTER_INK_VALUE_3
		,CPMeasurement_Printer.Printer_InkName_4 As PRINTER_INK_NAME_4
		,CPMeasurement_Printer.Printer_InkValue_4 As PRINTER_INK_VALUE_4
		,CPMeasurement_Printer.Printer_InkName_5 As PRINTER_INK_NAME_5
		,CPMeasurement_Printer.Printer_InkValue_5 As PRINTER_INK_VALUE_5
		,CPMeasurement_Printer.Printer_InkName_6 As PRINTER_INK_NAME_6
		,CPMeasurement_Printer.Printer_InkValue_6 As PRINTER_INK_VALUE_6
		,CPMeasurement_Printer.Printer_InkName_7 As PRINTER_INK_NAME_7
		,CPMeasurement_Printer.Printer_InkValue_7 As PRINTER_INK_VALUE_7
		,CPMeasurement_Printer.Printer_InkName_8 As PRINTER_INK_NAME_8
		,CPMeasurement_Printer.Printer_InkValue_8 As PRINTER_INK_VALUE_8
		,CPMeasurement_Printer.Printer_Finisher As PRINTER_FINISHER_NAME
		,CPMeasurement_Printer.Printer_FinisherValue As PRINTER_FINISHER_VALUE
		,CPMeasurement_Printer.Printer_Paper As PRINTER_PAPER
		,CPMeasurement_Printer.Printer_CC_Table As PRINTER_CC_TABLE 
		,CPMeasurement_Printer.Printer_ICC_Target As PRINTER_ICC_TARGET
		,CPMeasurement.Job AS JOB
		,CPDevice.CPDeviceDesc As PROOFER
		,CPMeasurement.Info_Text As INFO_TEXT
		,CPMeasurement.NumOfPatches As NUMBER_OF_SETS
		,CPMeasurement_OD.Category As OUTPUT_CATEGORY
		,CPMeasurement_OD.Manufacturer As OUTPUT_MANUFACTURER
		,CPMeasurement_OD.Model As OUTPUT_MODEL
		,CPMeasurement_OD.Serial_Num As OUTPUT_SERIAL_NUMBER
		,CPMeasurement_OD.RIP_Type As OUTPUT_RIP_TYPE

		,CPMeasurement_DP.Substrate As OUTPUT_SUBSTRATE
		,CPMeasurement_DP.Substrate_SKU As OUTPUT_SUBSTRATE_SKU
		,CPMeasurement_DP.Substrate_Batch As OUTPUT_SUBSTRATE_BATCH
		,CPMeasurement_DP.Ink As OUTPUT_INK
		,CPMeasurement_DP.Ink_SKU As OUTPUT_INK_SKU
		,CPMeasurement_DP.Ink_Batch As OUTPUT_INK_BATCH
		,CPMeasurement_DP.Chemistry As OUTPUT_CHEMISTRY
		,CPMeasurement_DP.Chemistry_SKU As OUTPUT_CHEMISTRY_SKU
		,CPMeasurement_DP.Chemistry_Batch As OUTPUT_CHEMISTRY_BATCH
		,CPMeasurement_DP.Resolution As OUTPUT_RESOLUTION
		,CPMeasurement_DP.Line_Screen As OUTPUT_LINE_SCREEN
		,CPMeasurement_DP.Dot_Shape As OUTPUT_DOT_SHAPE
		,CPMeasurement_OD.Separation As OUTPUT_SEPARATION
		
		,CPMeasurement_PT_Prop.Density_C As OUTPUT_TARGET_DENSITY_C
		,CPMeasurement_PT_Prop.Density_M As OUTPUT_TARGET_DENSITY_M
		,CPMeasurement_PT_Prop.Density_Y As OUTPUT_TARGET_DENSITY_Y
		,CPMeasurement_PT_Prop.Density_K As OUTPUT_TARGET_DENSITY_K
		,CPMeasurement_PT_Prop.Density_TH As OUTPUT_TARGET_DENSITY_THRESHOLD
		,CPMeasurement_PT_Prop.Dotgain_C As OUTPUT_TARGET_DOT_GAIN_C
		,CPMeasurement_PT_Prop.Dotgain_M As OUTPUT_TARGET_DOT_GAIN_M
		,CPMeasurement_PT_Prop.Dotgain_Y As OUTPUT_TARGET_DOT_GAIN_Y
		,CPMeasurement_PT_Prop.Dotgain_K As OUTPUT_TARGET_DOT_GAIN_K
		,CPMeasurement_PT_Prop.Dotgain_TH As OUTPUT_TARGET_DOT_GAIN_THRESHOLD
		,CPMeasurement_PT_Prop.Print_Contrast_C As OUTPUT_TARGET_PRINT_CONTRAST_C
		,CPMeasurement_PT_Prop.Print_Contrast_M As OUTPUT_TARGET_PRINT_CONTRAST_M
		,CPMeasurement_PT_Prop.Print_Contrast_Y As OUTPUT_TARGET_PRINT_CONTRAST_Y
		,CPMeasurement_PT_Prop.Print_Contrast_K As OUTPUT_TARGET_PRINT_CONTRAST_K
		,CPMeasurement_PT_Prop.Print_Contrast_TH As OUTPUT_TARGET_PRINT_CONTRAST_THRESHOLD
		,CPMeasurement_PT_Prop.Trap_R As OUTPUT_TARGET_TRAP_R
		,CPMeasurement_PT_Prop.Trap_G As OUTPUT_TARGET_TRAP_G
		,CPMeasurement_PT_Prop.Trap_B As OUTPUT_TARGET_TRAP_B
		,CPMeasurement_PT_Prop.Trap_TH As OUTPUT_TARGET_TRAP_THRESHOLD
		,CPMeasurement_PT_Prop.[1ST_INK] As OUTPUT_1ST_INK
		,CPMeasurement_PT_Prop.[2ND_INK] As OUTPUT_2ND_INK
		,CPMeasurement_PT_Prop.[3RD_INK] As OUTPUT_3RD_INK
		,CPMeasurement_PT_Prop.[4TH_INK] As OUTPUT_4TH_INK

		,CPMeasurement.TimeStamp As [TIMESTAMP]
			
		
	FROM         
		CPDevice 
		INNER JOIN CPDongle ON CPDevice.CPDongleID = CPDongle.CPDongleID 
		INNER JOIN CPMeasurement ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID 
		INNER JOIN CPMeasurement_ACT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_ACT.CPMeasurementGUID
		INNER JOIN CPMeasurement_DP ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_DP.CPMeasurementGUID 
		INNER JOIN CPMeasurement_MD ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_MD.CPMeasurementGUID 
		INNER JOIN CPMeasurement_OD ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_OD.CPMeasurementGUID
		INNER JOIN CPMeasurement_Printer ON dbo.CPMeasurement.CPMeasurementGUID = CPMeasurement_Printer.CPMeasurementGUID 
		INNER JOIN CPMeasurement_PT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT.CPMeasurementGUID 
		INNER JOIN CPMeasurement_PT_Prop ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT_Prop.CPMeasurementGUID 
		INNER JOIN CPMeasurement_TOL ON CPMeasurement.CPMeasurementGUID = CPMeasurement_TOL.CPMeasurementGUID
		INNER JOIN CPMeasurementType ON CPMeasurementType.CPMeasurementTypeID = CPMeasurement.CPMeasurementTypeID
		INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID
		LEFT OUTER JOIN deltaEFormula ON CPMeasurement_TOL.deltaEForumlaID = deltaEFormula.CPdeltaEFormulaID

	WHERE 
		CPMeasurement.CPMeasurementGUID = @CPMeasurementGUID 
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




