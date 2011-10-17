IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressChartZoneSelectionColorsSheet')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressChartZoneSelectionColorsSheet]
	END
GO
/*
	Author :- Maulik Dhorajia
	Date :- Aug 29 2008
	Description :- The query will give 2 results 
		1. The record with Zone chart Infromation to be plottede with DeltaE and ZoneNumber.
		2. The information of the colors per zone.
		
		By SheetNumber
*/

CREATE Procedure [Press].[uspGetPressChartZoneSelectionColorsSheet]( @JobID INT , @DongleID VARCHAR(10) , @SheetNumber INT ) 
AS
BEGIN
	BEGIN TRY 

	/*---------- Gets the information for the chart ----------*/
	EXEC Press.uspGetPressJobZoneSummarySheet @JobID , @DongleID , @SheetNumber;

	/*---------- Gets the colors ----------*/
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
