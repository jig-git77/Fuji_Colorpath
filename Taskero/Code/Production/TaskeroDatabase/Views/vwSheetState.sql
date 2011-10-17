IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSheetState')
	BEGIN
		DROP  View [Press].[vwSheetState]
	END
GO
/*
	Creater : Maulik Dhorajia
	Created Date : Sept 9 2008
	Description : This view acts as a temperory table for the master of SheetState.
*/
CREATE View [Press].[vwSheetState] AS

SELECT  0  as SheetStateID , 'MakeReady' AS SheetStateValue
	UNION
SELECT  1  as SheetStateID , 'OKSheet' AS SheetStateValue
	UNION
SELECT  2  as SheetStateID , 'Production' AS SheetStateValue


GO
