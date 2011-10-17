IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLDetailsOnGUID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLDetailsOnGUID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 29 2008
Purpose : Gets the patch details for Detail page.
*/
CREATE PROCEDURE [CPV].[uspGetDTLDetailsOnGUID]
( 
	@MeasurementGUID VARCHAR(36) 
) 
AS  
BEGIN
	BEGIN TRY
	
		/*========== Color Limit =========*/
		DECLARE @PRIMARY_LIMIT Decimal(18,2)
		DECLARE @PRIMARY_STRICT Decimal(18,2)
		DECLARE @Paper_Limit DECIMAL(9,2)
		DECLARE @Paper_Strict DECIMAL(9,2)
		
		SELECT 
			@Paper_Limit = NULLIF(Paper_Limit , 0) 
			, @Paper_Strict = NULLIF(Paper_Strict , 0)
			, @PRIMARY_LIMIT = NULLIF(PRIMARY_LIMIT , 0)
			, @PRIMARY_STRICT = NULLIF(PRIMARY_STRICT , 0)
		FROM 
			CPMeasurement_TOL 
		WHERE 
			CPMeasurementGUID = @MeasurementGUID


		/*========== Cyan Gets the DE AND DH StdDev =========*/
		SELECT 
			'Cyan' as Color
			,CONVERT(DECIMAL(18,2) , AVG(DE)) as AveDe , CONVERT(DECIMAL(18,2) , STDEV(DE)) as StdDe
			,CONVERT(DECIMAL(18,2) , AVG(DH)) as AveDH , CONVERT(DECIMAL(18,2) , STDEV(DH)) as StdDH
			, CONVERT( DECIMAL(18,2) , COALESCE ( AVG(TOL_STRICT) , @Primary_Strict  , @Primary_Limit ) ) as ThrshDe
		FROM 
			CPPatchDataLab
		WHERE	
			(CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0)
			AND MeasurementGUID = @MeasurementGUID

		/*========== Magenta the DE AND DH StdDev =========*/
		SELECT 
			'Magenta' as Color
			,CONVERT(DECIMAL(18,2) , AVG(DE)) as AveDe , CONVERT(DECIMAL(18,2) , STDEV(DE)) as StdDe
			,CONVERT(DECIMAL(18,2) , AVG(DH)) as AveDH , CONVERT(DECIMAL(18,2) , STDEV(DH)) as StdDH
			, CONVERT( DECIMAL(18,2) , COALESCE ( AVG(TOL_STRICT) , @Primary_Strict  , @Primary_Limit ) ) as ThrshDe
		FROM 
			CPPatchDataLab
		WHERE	
			(CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0)
			AND MeasurementGUID = @MeasurementGUID

		/*========== Yellow the DE AND DH StdDev =========*/
		SELECT 
			'Yellow' as Color
			,ISNULL( CONVERT(DECIMAL(18,2) , AVG(DE)) , 0 ) as AveDe , ISNULL( CONVERT(DECIMAL(18,2) , STDEV(DE)) , 0 ) as StdDe
			,ISNULL( CONVERT(DECIMAL(18,2) , AVG(DH)) , 0 ) as AveDH , ISNULL( CONVERT(DECIMAL(18,2) , STDEV(DH)) , 0 ) as StdDH
			, CONVERT( DECIMAL(18,2) , COALESCE ( AVG(TOL_STRICT) , @Primary_Strict  , @Primary_Limit ) ) as ThrshDe
		FROM 
			CPPatchDataLab
		WHERE	
			(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0)
			AND MeasurementGUID = @MeasurementGUID

		/*========== Black the DE AND DH StdDev =========*/
		SELECT 
			'Black' as Color
			,ISNULL( CONVERT(DECIMAL(18,2) , AVG(DE)) , 0 ) as AveDe , ISNULL( CONVERT(DECIMAL(18,2) , STDEV(DE)) , 0 ) as StdDe
			,ISNULL( CONVERT(DECIMAL(18,2) , AVG(DH)) , 0 ) as AveDH , ISNULL( CONVERT(DECIMAL(18,2) , STDEV(DH)) , 0 ) as StdDH
			, CONVERT( DECIMAL(18,2) , COALESCE ( AVG(TOL_STRICT) , @Primary_Strict  , @Primary_Limit ) ) as ThrshDe
		FROM 
			CPPatchDataLab
		WHERE	
			(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100)
			AND MeasurementGUID = @MeasurementGUID

		SELECT 
			'Paper White' as Color
			, CONVERT(DECIMAL(18,2) , AVG(DE)) as AveDe 
			, CONVERT(DECIMAL(18,2) , STDEV(DE)) as StdDe
			, CONVERT( DECIMAL(18,2) , COALESCE ( AVG(TOL_STRICT) , @Paper_Strict  , @Paper_Limit ) ) as ThrshDe
		FROM 
			CPPatchDataLab
		WHERE	
			(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0)
			AND MeasurementGUID = @MeasurementGUID

		/*========== Cyan Gets the DE AND DH StdDev =========*/
		SELECT 
			Top 1 'Gray Ave Dh' as Color	
			,CONVERT(DECIMAL(18,2) , (DH)) as AveDH , CONVERT(DECIMAL(18,2) , (DH)) as StdDH
		FROM 
			CPPatchDataLab
		WHERE	
			( 
				(CMYK_C = 25 AND CMYK_M = 19 AND CMYK_Y = 19 ) 
		OR		(CMYK_C = 25 AND CMYK_M = 16 AND CMYK_Y = 16 ) 
		OR		(CMYK_C = 25 AND CMYK_M = 25 AND CMYK_Y = 25 ) 
		OR		(CMYK_C = 20 AND CMYK_M = 20 AND CMYK_Y = 20 ) 

		OR		(CMYK_C = 50 AND CMYK_M = 40 AND CMYK_Y = 40 ) 
		OR		(CMYK_C = 50 AND CMYK_M = 39 AND CMYK_Y = 39 ) 
		OR		(CMYK_C = 50 AND CMYK_M = 50 AND CMYK_Y = 50 ) 

		OR		(CMYK_C = 75 AND CMYK_M = 64 AND CMYK_Y = 64 ) 
		OR		(CMYK_C = 75 AND CMYK_M = 63 AND CMYK_Y = 63 ) 
		OR		(CMYK_C = 75 AND CMYK_M = 66 AND CMYK_Y = 66 ) 
		OR		(CMYK_C = 75 AND CMYK_M = 75 AND CMYK_Y = 75 ) 
		OR		(CMYK_C = 70 AND CMYK_M = 70 AND CMYK_Y = 70 ) 
			)
			AND CMYK_K = 0
			AND MeasurementGUID = @MeasurementGUID
		
		/*========== Gets the Average Delata E  and Max Delata E =========*/
		SELECT 
			CONVERT(DECIMAL(18,2) , Average) AS Average
			, CONVERT(DECIMAL(18,2) , PatchDeValue )AS PatchDeValue	
		FROM 
			CPMeasurement_ACT
		WHERE
			CPMeasurementGUID = @MeasurementGUID
			
		/*========== Cyan Gets the Thrsh Limit =========*/
		SELECT 
			Average_Limit AS Average_Limit
			, Maximum_Limit AS Maximum_Limit
			, PRIMARY_LIMIT_DH AS PRIMARY_LIMIT_DH
			, GRAY_AVERAGE_LIMIT_DH AS GRAY_AVERAGE_LIMIT_DH
		FROM 
			CPMeasurement_TOL 
		WHERE 
			CPMeasurementGUID = @MeasurementGUID

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

