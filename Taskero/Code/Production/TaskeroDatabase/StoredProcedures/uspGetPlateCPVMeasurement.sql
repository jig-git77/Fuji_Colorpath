IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPlateCPVMeasurement')
	BEGIN
		DROP  Procedure [CPV].[uspGetPlateCPVMeasurement]
	END

GO

/*  
AUTHOR : Tushar Patel
Created Date : July 29 2008  
Purpose : To Get all the CPPatchDataPlate Information BY CPMeasurementGUID.  
*/ 

CREATE PROCEDURE [CPV].[uspGetPlateCPVMeasurement]
( @MeasurementGUID VARCHAR(36) 
) 
AS
BEGIN
	BEGIN TRY
		
		/* To Get all the CPMeasurement Information BY CPMeasurementGUID.  */
		EXEC [CPV].[uspGetCPMeasurementData]   @MeasurementGUID      

		/*    To Get all the CPPatchDataLab Information BY CPMeasurementGUID       */
		SELECT     
			ID_N,
			Sample_ID,
			Sample_Name,
			DOT_AREA_TARGET,
			DOT_AREA_ACTUAL,
			DEVIATION,
			TOL_STRICT,
			DOT_AREA_REFERENCE,
			Plate_Color,
			CREATED_TIME	
		FROM 
			CPPatchDataPlate
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


