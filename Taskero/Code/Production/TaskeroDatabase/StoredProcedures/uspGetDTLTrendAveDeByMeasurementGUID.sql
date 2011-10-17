IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendAveDeByMeasurementGUID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLTrendAveDeByMeasurementGUID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 16 2008

MODIFIED BY : Maulik Dhorajia
MODIFIED DATE : July 23 2008

Purpose : This retrieves the Average Delta E & Density for Trend Ave De Chart using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black.
Instruction : @MeasurementGUID should be sent comma sepearted.
28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
*/
CREATE PROCEDURE [CPV].[uspGetDTLTrendAveDeByMeasurementGUID]
( 
	@MeasurementGUID VARCHAR(8000) 
) 
AS  
BEGIN
	BEGIN TRY

		/*---------- ASSIGN TO XML ----------*/
		DECLARE @xmlMeasurementGUID XML	
		SET @xmlMeasurementGUID = '<GUIDs><GUID>' + REPLACE( @MeasurementGUID , ',' , '</GUID><GUID>' ) + '</GUID></GUIDs>'

		/*---------- This retrieves the Average Delta E & Density for Trend Ave De Chart using @MeasurementGUID for Cyan ----------*/
		SELECT
			  'Cyan' as Color
			, MeasurementGUID
			, ISNULL( AVG(DE) , 0 ) as AVE_DE
			, ISNULL( MAX(Density) , 0 ) as Density
			, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
			--, AVG(ABS(LAB_L1)) as LAB_L1 , AVG(ABS(LAB_A1)) as LAB_A1 , AVG(ABS(LAB_B1)) as LAB_B1
			--, AVG(ABS(LAB_L2)) as LAB_L2 , AVG(ABS(LAB_A2)) as LAB_A2 , AVG(ABS(LAB_B2)) as LAB_B2
		FROM 
			CPPatchDataLab
		WHERE			
			(CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0)  
			AND MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		GROUP BY
			MeasurementGUID
		ORDER BY 
			Created_Time

		/*---------- This retrieves the Average Delta E for Trend Ave De & Density Chart using @MeasurementGUID for Magenta ----------*/
		SELECT 	
			  'Magenta' as Color
			, MeasurementGUID
			, ISNULL( AVG(DE) , 0 ) as AVE_DE
			, ISNULL( MAX(Density) , 0 ) as Density
			, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
			--, AVG(ABS(LAB_L1)) as LAB_L1 , AVG(ABS(LAB_A1)) as LAB_A1 , AVG(ABS(LAB_B1)) as LAB_B1
			--, AVG(ABS(LAB_L2)) as LAB_L2 , AVG(ABS(LAB_A2)) as LAB_A2 , AVG(ABS(LAB_B2)) as LAB_B2
		FROM 
			CPPatchDataLab
		WHERE			
			(CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0)
			AND MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		GROUP BY
			MeasurementGUID
		ORDER BY 
			Created_Time

		/*---------- This retrieves the Average Delta E for Trend Ave De & Density Chart using @MeasurementGUID for Yellow ----------*/
		SELECT 	
			'Yellow' as Color
			, MeasurementGUID
			, ISNULL( AVG(DE) , 0 ) as AVE_DE
			, ISNULL( MAX(Density) , 0 ) as Density
			, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
			--, AVG(ABS(LAB_L1)) as LAB_L1 , AVG(ABS(LAB_A1)) as LAB_A1 , AVG(ABS(LAB_B1)) as LAB_B1
			--, AVG(ABS(LAB_L2)) as LAB_L2 , AVG(ABS(LAB_A2)) as LAB_A2 , AVG(ABS(LAB_B2)) as LAB_B2
		FROM 
			CPPatchDataLab
		WHERE			
			(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0)
			AND MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		GROUP BY
			MeasurementGUID
		ORDER BY 
			Created_Time

		/*---------- This retrieves the Average Delta E for Trend Ave De & Density Chart using @MeasurementGUID for Black ----------*/
		SELECT 	
			'Black' as Color
			, MeasurementGUID
			, ISNULL( AVG(DE) , 0 ) as AVE_DE
			, ISNULL( MAX(Density) , 0 ) as Density
			, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
			--, AVG(ABS(LAB_L1)) as LAB_L1 , AVG(ABS(LAB_A1)) as LAB_A1 , AVG(ABS(LAB_B1)) as LAB_B1
			--, AVG(ABS(LAB_L2)) as LAB_L2 , AVG(ABS(LAB_A2)) as LAB_A2 , AVG(ABS(LAB_B2)) as LAB_B2
		FROM 
			CPPatchDataLab
		WHERE			
			(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100 )
			AND MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		GROUP BY
			MeasurementGUID
		ORDER BY 
			Created_Time

		/*---------- This retrieves the Average Delta E for Trend Ave De & Density Chart using @MeasurementGUID for Black ----------*/
		SELECT 	
			'Gray' as Color
			, MeasurementGUID
			, ISNULL( AVG(DE) , 0 ) as AVE_DE
			, ISNULL( MAX(Density) , 0 ) as Density
			, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
			--, AVG(ABS(LAB_L1)) as LAB_L1 , AVG(ABS(LAB_A1)) as LAB_A1 , AVG(ABS(LAB_B1)) as LAB_B1
			--, AVG(ABS(LAB_L2)) as LAB_L2 , AVG(ABS(LAB_A2)) as LAB_A2 , AVG(ABS(LAB_B2)) as LAB_B2
		FROM 
			CPPatchDataLab
		WHERE			
			(
				   (CMYK_C = 50 AND CMYK_M = 39 AND CMYK_Y = 39 AND CMYK_K = 0)
				OR (CMYK_C = 50 AND CMYK_M = 40 AND CMYK_Y = 40 AND CMYK_K = 0)
				OR (CMYK_C = 50 AND CMYK_M = 50 AND CMYK_Y = 50 AND CMYK_K = 0)
			)
			AND MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		GROUP BY
			MeasurementGUID
		ORDER BY 
			Created_Time

		/*---------- This retrieves the de Paper, de Average, de Max & de Primary ----------*/  
		SELECT
			CPMeasurement.CPMeasurementGUID
			, ISNULL( Paper , 0 ) AS dePaperWhite
			, ISNULL( Average , 0 ) AS deAverage
			, ISNULL( PatchdEValue , 0 ) AS deMaximum
			, ISNULL( PatchPrimarydEValue , 0 ) AS dePrimary
			, CreatedDT
		FROM
			CPMeasurement_ACT
		INNER JOIN CPMeasurement ON CPMeasurement.CPMeasurementGUID = CPMeasurement_ACT.CPMeasurementGUID
		WHERE
			CPMeasurement.CPMeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
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
