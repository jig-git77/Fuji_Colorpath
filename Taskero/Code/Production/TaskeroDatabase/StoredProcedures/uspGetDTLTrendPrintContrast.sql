IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendPrintContrast')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLTrendPrintContrast]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 16 2008

Purpose : This retrieves the print contrast using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black.
Instruction : @MeasurementGUID should be sent comma sepearted.
28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
*/
CREATE PROCEDURE [CPV].[uspGetDTLTrendPrintContrast]
( 
	@MeasurementGUID VARCHAR(8000) 
) 
AS  
BEGIN
	BEGIN TRY

		/*---------- ASSIGN TO XML ----------*/
		DECLARE @xmlMeasurementGUID XML	
		SET @xmlMeasurementGUID = '<GUIDs><GUID>' + REPLACE( @MeasurementGUID , ',' , '</GUID><GUID>' ) + '</GUID></GUIDs>'

		/*---------- CYAN ----------*/
		SELECT
			'Cyan' AS Color
			, A.MeasurementGUID  
		   , MAX(A.Created_Time) AS Created_Time
		   , ISNULL(MAX(A.Density),0) AS D100  
		   , ISNULL(MAX(B.Density),0) AS D75  
		   , ISNULL(MAX(Print_Contrast_C) , 0 ) AS Print_Contrast  
		   , ISNULL(MAX(Print_Contrast_TH), 0 ) AS Print_Contrast_TH
		FROM
			(SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)) AS A1
			LEFT OUTER JOIN CPPatchDataLab A ON A.MeasurementGUID = A1.GUID	AND (A.CMYK_C = 100 AND A.CMYK_M = 0 AND A.CMYK_Y = 0 AND A.CMYK_K = 0)
			LEFT OUTER JOIN CPPatchDataLab B ON B.MeasurementGUID = A1.GUID AND (B.CMYK_C IN (75 , 70 , 80)  AND B.CMYK_M = 0 AND B.CMYK_Y = 0 AND B.CMYK_K = 0)
			INNER JOIN CPMeasurement_PT_Prop ON CPMeasurementGUID  = A1.GUID
		GROUP BY A.MeasurementGUID , A.Created_Time
		ORDER BY A.Created_Time  
	

		/*---------- MAGENTA ----------*/
		SELECT
			'Magenta' AS Color
			, A.MeasurementGUID  
		   , MAX(A.Created_Time) AS Created_Time
		   , ISNULL(MAX(A.Density),0) AS D100  
		   , ISNULL(MAX(B.Density),0) AS D75  
		   , ISNULL(MAX(Print_Contrast_C) , 0 ) AS Print_Contrast  
		   , ISNULL(MAX(Print_Contrast_TH), 0 ) AS Print_Contrast_TH
		FROM
			(SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)) AS A1
			LEFT OUTER JOIN CPPatchDataLab A ON A.MeasurementGUID = A1.GUID	AND (A.CMYK_C = 0 AND A.CMYK_M = 100 AND A.CMYK_Y = 0 AND A.CMYK_K = 0)
			LEFT OUTER JOIN CPPatchDataLab B ON B.MeasurementGUID = A1.GUID AND (B.CMYK_C = 0 AND B.CMYK_M  IN (75 , 70 , 80) AND B.CMYK_Y = 0 AND B.CMYK_K = 0)
			INNER JOIN CPMeasurement_PT_Prop ON CPMeasurementGUID  = A1.GUID
		GROUP BY A.MeasurementGUID , A.Created_Time
		ORDER BY A.Created_Time  

		/*---------- YELLOW ----------*/
		SELECT
			'Yellow' AS Color
			, A.MeasurementGUID  
		   , MAX(A.Created_Time) AS Created_Time
		   , ISNULL(MAX(A.Density),0) AS D100  
		   , ISNULL(MAX(B.Density),0) AS D75  
		   , ISNULL(MAX(Print_Contrast_C) , 0 ) AS Print_Contrast  
		   , ISNULL(MAX(Print_Contrast_TH), 0 ) AS Print_Contrast_TH
		FROM
			(SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)) AS A1
			LEFT OUTER JOIN CPPatchDataLab A ON A.MeasurementGUID = A1.GUID	AND (A.CMYK_C = 0 AND A.CMYK_M = 0 AND A.CMYK_Y = 100 AND A.CMYK_K = 0)
			LEFT OUTER JOIN CPPatchDataLab B ON B.MeasurementGUID = A1.GUID AND (B.CMYK_C = 0 AND B.CMYK_M = 0 AND B.CMYK_Y  IN (75 , 70 , 80) AND B.CMYK_K = 0)
			INNER JOIN CPMeasurement_PT_Prop ON CPMeasurementGUID  = A1.GUID
		GROUP BY A.MeasurementGUID , A.Created_Time
		ORDER BY A.Created_Time  

		/*---------- Black ----------*/
		SELECT
			'Black' AS Color
			, A.MeasurementGUID  
		   , MAX(A.Created_Time) AS Created_Time
		   , ISNULL(MAX(A.Density),0) AS D100  
		   , ISNULL(MAX(B.Density),0) AS D75  
		   , ISNULL(MAX(Print_Contrast_C) , 0 ) AS Print_Contrast  
		   , ISNULL(MAX(Print_Contrast_TH), 0 ) AS Print_Contrast_TH
		FROM
			(SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)) AS A1
			LEFT OUTER JOIN CPPatchDataLab A ON A.MeasurementGUID = A1.GUID	AND (A.CMYK_C = 0 AND A.CMYK_M = 0 AND A.CMYK_Y = 0 AND A.CMYK_K = 100)
			LEFT OUTER JOIN CPPatchDataLab B ON B.MeasurementGUID = A1.GUID AND (B.CMYK_C = 0 AND B.CMYK_M = 0 AND B.CMYK_Y = 0 AND B.CMYK_K  IN (75 , 70 , 80))
			INNER JOIN CPMeasurement_PT_Prop ON CPMeasurementGUID  = A1.GUID
		GROUP BY A.MeasurementGUID , A.Created_Time
		ORDER BY A.Created_Time  

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


