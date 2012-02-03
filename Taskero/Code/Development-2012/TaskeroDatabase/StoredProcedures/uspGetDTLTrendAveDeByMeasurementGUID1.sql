IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendAveDeByMeasurementGUID1')
	BEGIN
		DROP  PROCEDURE [CPV].[uspGetDTLTrendAveDeByMeasurementGUID1]
	END
	
GO
/*
AUTHOR : Maulik Dhorajia
Created Date : Aug 6 2008

Purpose : This retrieves the Average Delta E & Density for Trend Ave De Chart using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black.
Aslo this is an exted version for uspGetDTLTrendAveDeByMeasurementGUID.
Instruction : @MeasurementGUID should be sent comma sepearted.
28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
*/
CREATE PROCEDURE [CPV].[uspGetDTLTrendAveDeByMeasurementGUID1]
( 
	@MeasurementGUID VARCHAR(8000) 
) 
AS  
BEGIN
	BEGIN TRY

		/*---------- ASSIGN TO XML ----------*/
		DECLARE @xmlMeasurementGUID XML	
		SET @xmlMeasurementGUID = '<GUIDs><GUID>' + REPLACE( @MeasurementGUID , ',' , '</GUID><GUID>' ) + '</GUID></GUIDs>'

		/*---------- Gets the Values from View for DTLTrend.aspx ----------*/
		SELECT 

			C.MeasurementGUID 
			, C.Created_Time

			, ISNULL( C.AVE_DE , 0 ) AS Cyan_Ave_De
			, ISNULL( C.Density , 0 )  AS Cyan_Density

			, ISNULL( M.AVE_DE , 0 )  AS Magenta_Ave_De
			, ISNULL( M.Density , 0 )  AS Magenta_Density

			, ISNULL( Y.AVE_DE , 0 )  AS Yellow_Ave_De
			, ISNULL( Y.Density , 0 )  AS Yellow_Density

			, ISNULL( K.AVE_DE , 0 )  AS Black_Ave_De
			, ISNULL( K.Density , 0 )  AS Black_Density

			, ISNULL( G.AVE_DE , 0 )  AS Gray_Ave_De
			, ISNULL( G.Density , 0 )  AS Gray_Density

			, ISNULL( O.dePaperWhite , 0 ) AS dePaperWhite
			, ISNULL( O.deAverage , 0 ) AS deAverage
			, ISNULL( O.deMaximum , 0 ) AS deMaximum
			, ISNULL( O.dePrimary , 0 ) AS dePrimary

		FROM CPV.vwAllCyanPatches C
			LEFT OUTER JOIN CPV.vwAllMagentaPatches M ON M.MeasurementGUID = C.MeasurementGUID 
			LEFT OUTER JOIN CPV.vwAllYellowPatches Y ON Y.MeasurementGUID = C.MeasurementGUID
			LEFT OUTER JOIN CPV.vwAllBlackPatches K ON K.MeasurementGUID = C.MeasurementGUID	
			LEFT OUTER JOIN CPV.vwAllGrayPatches G ON G.MeasurementGUID = C.MeasurementGUID
			LEFT OUTER JOIN CPV.vwAlldePaperAvgMaxPrimary O ON O.CPMeasurementGUID = C.MeasurementGUID
		WHERE
			C.MeasurementGUID IN ( SELECT xmlMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlMeasurementGUID.nodes('/GUIDs/GUID') d(xmlMeasurementGUID)  ) 
		ORDER BY C.Created_Time
	
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
