IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLColorChartsByGUID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetDTLColorChartsByGUID]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/30/2008
-- Description:	COLOR CHARTS
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDTLColorChartsByGUID]
(
	@MeasurementGUID AS uniqueidentifier
)
AS 
BEGIN
	BEGIN TRY
-----------------------------------------------------------------------------------------			
		--  Table 0 -- L - AB - 25%
-----------------------------------------------------------------------------------------
		SELECT 'L - AB - 25%' AS 'Gray Balance'
			, MeasurementGUID
			, ISNULL(LAB_L1, 0) AS LAB_L1, ISNULL(LAB_A1, 0) AS LAB_A1, ISNULL(LAB_B1, 0) AS LAB_B1
			, ISNULL(LAB_L2, 0) AS LAB_L2, ISNULL(LAB_A2, 0) AS LAB_A2, ISNULL(LAB_B2, 0) AS LAB_B2

		FROM 
			CPPatchDataLab
		WHERE	
			( 
						(CMYK_C = 25 AND CMYK_M = 19 AND CMYK_Y = 19 ) 
				OR		(CMYK_C = 25 AND CMYK_M = 16 AND CMYK_Y = 16 ) 
				OR		(CMYK_C = 25 AND CMYK_M = 25 AND CMYK_Y = 25 ) 
				OR		(CMYK_C = 20 AND CMYK_M = 20 AND CMYK_Y = 20 ) 
			)
			AND CMYK_K = 0
			AND MeasurementGUID = @MeasurementGUID 
-----------------------------------------------------------------------------------------
		--  Table 1 -- L - AB - 50%
-----------------------------------------------------------------------------------------
		SELECT 'L - AB - 50%' AS 'Gray Balance'
			, MeasurementGUID
			, ISNULL(LAB_L1, 0) AS LAB_L1, ISNULL(LAB_A1, 0) AS LAB_A1, ISNULL(LAB_B1, 0) AS LAB_B1
			, ISNULL(LAB_L2, 0) AS LAB_L2, ISNULL(LAB_A2, 0) AS LAB_A2, ISNULL(LAB_B2, 0) AS LAB_B2

		FROM 
			CPPatchDataLab
		WHERE	
			( 
					(CMYK_C = 50 AND CMYK_M = 40 AND CMYK_Y = 40 ) 
				OR	(CMYK_C = 50 AND CMYK_M = 39 AND CMYK_Y = 39 ) 
				OR	(CMYK_C = 50 AND CMYK_M = 50 AND CMYK_Y = 50 ) 
			)

			AND CMYK_K = 0
			AND MeasurementGUID = @MeasurementGUID 

-----------------------------------------------------------------------------------------
		--  Table 2 -- L - AB - 75%
-----------------------------------------------------------------------------------------
		SELECT 'L - AB - 75%' AS 'Gray Balance'
			, MeasurementGUID
			, ISNULL(LAB_L1, 0) AS LAB_L1, ISNULL(LAB_A1, 0) AS LAB_A1, ISNULL(LAB_B1, 0) AS LAB_B1
			, ISNULL(LAB_L2, 0) AS LAB_L2, ISNULL(LAB_A2, 0) AS LAB_A2, ISNULL(LAB_B2, 0) AS LAB_B2

		FROM 
			CPPatchDataLab
		WHERE	
			( 
					(CMYK_C = 75 AND CMYK_M = 64 AND CMYK_Y = 64 ) 
				OR	(CMYK_C = 75 AND CMYK_M = 63 AND CMYK_Y = 63 ) 
				OR	(CMYK_C = 75 AND CMYK_M = 66 AND CMYK_Y = 66 ) 
				OR	(CMYK_C = 75 AND CMYK_M = 75 AND CMYK_Y = 75 ) 
				OR	(CMYK_C = 70 AND CMYK_M = 70 AND CMYK_Y = 70 ) 
			)

			AND CMYK_K = 0
			AND MeasurementGUID = @MeasurementGUID 
-----------------------------------------------------------------------------------------
		--  Table 3 -- Gamut
-----------------------------------------------------------------------------------------
		SELECT 
		MeasurementGUID
		, ColorType 
		, CMYK_C , CMYK_M , CMYK_Y , CMYK_K
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_L1)), 0) AS LAB_L1
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_A1)), 0) AS LAB_A1
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_B1)), 0) AS LAB_B1
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_L2)), 0) AS LAB_L2
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_A2)), 0) AS LAB_A2
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(LAB_B2)), 0) AS LAB_B2
		, 	
				CASE WHEN ColorType = 'X' THEN 0
					 WHEN ColorType = 'Cyan' THEN 1
					 WHEN ColorType = 'Green' THEN 2
					 WHEN ColorType = 'Yellow' THEN 3
					 WHEN ColorType = 'Red' THEN 4
					 WHEN ColorType = 'Magenta' THEN 5
					 WHEN ColorType = 'Blue' THEN 6
					 WHEN ColorType = 'White' THEN 7
					 --WHEN ColorType = 'Black' THEN 8
				END AS RANK
		FROM 
			(
				SELECT MeasurementGUID, 
					CASE WHEN CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'Cyan'
						 WHEN CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'Magenta'
						 WHEN CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0 THEN 'Yellow'
						 --WHEN CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100 THEN 'Black'
						 WHEN CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0 THEN 'Green'
						 WHEN CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 100 AND CMYK_K = 0 THEN 'Red'
						 WHEN CMYK_C = 100 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'Blue'
						 WHEN CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'White'
						 ELSE 'X'		
					END AS ColorType
				
				,ISNULL(CMYK_C, 0.0) AS CMYK_C, ISNULL(CMYK_M, 0.0) AS CMYK_M, ISNULL(CMYK_Y, 0.0) AS CMYK_Y, ISNULL(CMYK_K, 0.0) AS CMYK_K
				,ISNULL(LAB_L1, 0.0) AS LAB_L1, ISNULL(LAB_A1, 0.0) AS LAB_A1, ISNULL(LAB_B1, 0.0) AS LAB_B1
				,ISNULL(LAB_L2, 0.0) AS LAB_L2, ISNULL(LAB_A2, 0.0) AS LAB_A2, ISNULL(LAB_B2, 0.0) AS LAB_B2
				FROM CPPatchDataLab
				WHERE MeasurementGUID = @MeasurementGUID
			) AS GAMUT 

		WHERE ColorType <> 'X'
		GROUP BY MeasurementGUID , ColorType , CMYK_C , CMYK_M , CMYK_Y , CMYK_K
		ORDER BY RANK
		
-----------------------------------------------------------------------------------------
		--  Table 4 -- Density
-----------------------------------------------------------------------------------------
		SELECT 
			ColorType
			, MAX(DENSITY) AS DENSITY
		FROM 
			(
				SELECT 
					CASE WHEN CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'Cyan'
						 WHEN CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0 THEN 'Magenta'
						 WHEN CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0 THEN 'Yellow'
						 WHEN CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100 THEN 'Black'
						 ELSE 'X'		
					END AS ColorType
				, ISNULL(DENSITY, 0.0) AS DENSITY
				FROM CPPatchDataLab
				WHERE MeasurementGUID = @MeasurementGUID
			) AS Density 
		WHERE ColorType <> 'X'
		GROUP BY ColorType
		
-----------------------------------------------------------------------------------------
		--  Table 7 -- Print Contrast
-----------------------------------------------------------------------------------------

EXEC cpv.uspGetDTLTrendPrintContrast @MeasurementGUID
-- In This SP has 4 Table


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

