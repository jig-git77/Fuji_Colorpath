IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetColorCPVMeasurement')
	BEGIN
		DROP  Procedure [CPV].[uspGetColorCPVMeasurement]
	END

GO

/*  
AUTHOR : Amit Patel
Created Date : July 29 2008  
Purpose : To Get all the CPPatchDataLab Information BY CPMeasurementGUID.  
*/ 

CREATE PROCEDURE [CPV].[uspGetColorCPVMeasurement]
( 
	@MeasurementGUID VARCHAR(36) 
)
AS  
BEGIN
	BEGIN TRY
		
		/* To Get all the CPMeasurement Information BY CPMeasurementGUID.  */
		EXEC [CPV].[uspGetCPMeasurementData]   @MeasurementGUID      

		/*    To Get all the CPPatchDataLab Information BY CPMeasurementGUID       */
		SELECT     
			  ID_N
			, Sample_ID
			, Sample_Name
			, CMYK_C	
			, CMYK_M
			, CMYK_Y
			, CMYK_K
			, LAB_L1
			, LAB_A1
			, LAB_B1
			, LAB_L2
			, LAB_A2
			, LAB_B2
			, DE
			, DL
			, DA
			, DB
			, DH
			, DC
			, DENSITY
			, DENS_TARGET
			, DENS_ACTUAL
			, TOL_STRICT
			, TOL_STAND
			, CREATED_TIME
		FROM 
			CPPatchDataLab
		WHERE 
			MeasurementGUID = @MeasurementGUID
		ORDER BY 
			CREATED_TIME, Sample_ID
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
