IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLTrendDotgain')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLTrendDotgain]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 16 2008

MODIFIED BY : Maulik Dhorajia
MODIFIED DATE : July 21 2008

Purpose : This retrieves the dotgain.
Instruction : @MeasurementGUID should be in below given format.
28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
*/
CREATE PROCEDURE [CPV].[uspGetDTLTrendDotgain]
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
			, ISNULL( [DotGain_C50%] , 0 ) AS Cyan50
			, ISNULL( [DotGain_M50%] , 0 ) AS Magenta50
			, ISNULL( [DotGain_Y50%] , 0 ) AS Yellow50
			, ISNULL( [DotGain_K50%] , 0 ) AS Black50
			, ISNULL( [DotGain_C25%] , 0 ) AS Cyan25
			, ISNULL( [DotGain_M25%] , 0 ) AS Magenta25
			, ISNULL( [DotGain_Y25%] , 0 ) AS Yellow25
			, ISNULL( [DotGain_K25%] , 0 ) AS Black25
			, ISNULL( [DotGain_C75%] , 0 ) AS Cyan75
			, ISNULL( [DotGain_M75%] , 0 ) AS Magenta75
			, ISNULL( [DotGain_Y75%] , 0 ) AS Yellow75
			, ISNULL( [DotGain_K75%] , 0 ) AS Black75
			, CreatedDt
		FROM
			CPMeasurement			
			LEFT OUTER JOIN CPMeasurement_DotGain ON CPMeasurement_DotGain.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID
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





