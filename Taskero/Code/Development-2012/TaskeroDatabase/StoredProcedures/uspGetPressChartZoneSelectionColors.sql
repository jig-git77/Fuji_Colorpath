IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressChartZoneSelectionColors')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressChartZoneSelectionColors]
	END
GO
/*
	Name :- Maulik Dhorajia
	Date :- Oct 25 2008
	Description :- Get the information of Zone Selection on Charts.
	1 Returns all the Zones
	2 Will get the colors per zone
	
	Name :- Maulik Dhorajia
	Date :- Nov 4 2008
	Description :- Changed the query for optimization and correct logics.
*/
CREATE Procedure [Press].[uspGetPressChartZoneSelectionColors]( @JobID INT , @DongleID VARCHAR(10) )  AS
BEGIN
	BEGIN TRY
	
		EXEC Press.uspGetPressJobZoneSummary @JobID , @DongleID;

		SELECT
			PM.ZoneNumber
			, PM.ColorName
			, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1
			, Colors.ColorID
			, Colors.HTMlColor		
			, COALESCE
			( 
				NULLIF( (CASE PM.IsEnabled WHEN 1 THEN '' ELSE 'X' END)  , '' )
				, NULLIF( (CASE PM.IsDud WHEN 1 THEN '?' ELSE '' END) , '' )		
				, ''
			)	AS IsDudOrIsEnabled
		FROM
			Press.MeasuredPatches AS PM
			INNER JOIN Press.Colors AS Colors ON Colors.ColorName = PM.ColorName AND Colors.JobID = PM.JobID AND Colors.DongleID = PM.DongleID 
		WHERE 
			PM.JobID = @JobID AND PM.DongleID = @DongleID 		
		ORDER BY
			PM.LocationFromBeginSheet

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

/*
SELECT
	Press.MeasuredPatches.ZoneNumber
	, Press.MeasuredPatches.ColorName
	, Press.fnGetShortStringForColor(Press.Colors.ColorName) AS ColorName1
	, Press.Colors.ColorID
	, Press.Colors.HTMlColor		
	, COALESCE
	( 
		NULLIF( (CASE Press.MeasuredPatches.IsEnabled WHEN 1 THEN '' ELSE 'X' END)  , '' )
		, NULLIF( (CASE Press.MeasuredPatches.IsDud WHEN 1 THEN '?' ELSE '' END) , '' )		
		, ''
	)	AS IsDudOrIsEnabled
FROM
	Press.MeasuredPatches
	INNER JOIN Press.Colors ON Press.Colors.ColorName = Press.MeasuredPatches.ColorName
WHERE 
	Press.MeasuredPatches.JobID = @JobID AND Press.MeasuredPatches.DongleID = @DongleID 
	AND Press.Colors.JobID = @JobID AND Press.Colors.DongleID = @DongleID 
ORDER BY
	Press.MeasuredPatches.LocationFromBeginSheet
*/