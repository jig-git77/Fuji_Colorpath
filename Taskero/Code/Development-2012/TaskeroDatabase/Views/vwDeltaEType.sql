IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwDeltaEType')
	BEGIN
		DROP  View [Press].[vwDeltaEType]
	END
GO
/*
	Creater : Maulik Dhorajia
	Created Date : Sept 9 2008
	Description : This view acts as a temperory table for the master of Delta E Type DeltaEType.
*/
CREATE View [Press].[vwDeltaEType] AS

SELECT 0 AS DeltaETypeID , 'DE_LAB' AS DeltaETypeValue
	UNION
SELECT 1 AS DeltaETypeID , 'DE_CMC' AS DeltaETypeValue
	UNION
SELECT 2 AS DeltaETypeID , 'DE_94' AS DeltaETypeValue
	UNION
SELECT 3 AS DeltaETypeID , 'DE_2000' AS DeltaETypeValue

GO
