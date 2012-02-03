IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendInk')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLTrendInk]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 18 2008

MODIFIED BY : Maulik Dhorajia
MODIFIED DATE : July 21 2008

Purpose : This retrieves the Ink Level.
Instruction : @MeasurementGUID should be in below given format.
28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
*/
CREATE PROCEDURE [CPV].[uspGetDTLTrendInk]
( 
	@CPMeasurementGUID VARCHAR(8000) 
) 
AS  
BEGIN
	BEGIN TRY

		/*---------- ASSIGN TO XML --- -------*/
		DECLARE @xmlCPMeasurementGUID XML	
		SET @xmlCPMeasurementGUID = '<GUIDs><GUID>' + REPLACE( @CPMeasurementGUID , ',' , '</GUID><GUID>' ) + '</GUID></GUIDs>'

		/*---------- Retroeves the Dotgain ----------*/
		SELECT
			  CPMeasurement.CPMeasurementGUID 
			, Printer_InkName_0 , ISNULL( Printer_InkValue_0 , 0 ) AS Printer_InkValue_0 , ISNULL( Printer_InkValue_0 , -1 ) Printer_InkValue_0_Show
			, Printer_InkName_1 , ISNULL( Printer_InkValue_1 , 0 ) AS Printer_InkValue_1 , ISNULL( Printer_InkValue_1 , -1 ) Printer_InkValue_1_Show
			, Printer_InkName_2 , ISNULL( Printer_InkValue_2 , 0 ) AS Printer_InkValue_2 , ISNULL( Printer_InkValue_2 , -1 ) Printer_InkValue_2_Show
			, Printer_InkName_3 , ISNULL( Printer_InkValue_3 , 0 ) AS Printer_InkValue_3 , ISNULL( Printer_InkValue_3 , -1 ) Printer_InkValue_3_Show
			, Printer_InkName_4 , ISNULL( Printer_InkValue_4 , 0 ) AS Printer_InkValue_4 , ISNULL( Printer_InkValue_4 , -1 ) Printer_InkValue_4_Show
			, Printer_InkName_5 , ISNULL( Printer_InkValue_5 , 0 ) AS Printer_InkValue_5 , ISNULL( Printer_InkValue_5 , -1 ) Printer_InkValue_5_Show
			, Printer_InkName_6 , ISNULL( Printer_InkValue_6 , 0 ) AS Printer_InkValue_6 , ISNULL( Printer_InkValue_6 , -1 ) Printer_InkValue_6_Show
			, Printer_InkName_7 , ISNULL( Printer_InkValue_7 , 0 ) AS Printer_InkValue_7 , ISNULL( Printer_InkValue_7 , -1 ) Printer_InkValue_7_Show
			, Printer_InkName_8 , ISNULL( Printer_InkValue_8 , 0 ) AS Printer_InkValue_8 , ISNULL( Printer_InkValue_8 , -1 ) Printer_InkValue_8_Show
			, CreatedDt
		FROM
			CPMeasurement_Printer
		INNER JOIN CPMeasurement ON CPMeasurement.CPMeasurementGUID = CPMeasurement_Printer.CPMeasurementGUID
		WHERE 
			CPMeasurement.CPMeasurementGUID IN ( SELECT xmlCPMeasurementGUID.value('.', 'VARCHAR(36)') AS GUID FROM @xmlCPMeasurementGUID.nodes('/GUIDs/GUID') d(xmlCPMeasurementGUID) )
		ORDER BY
			CreatedDt
		
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
