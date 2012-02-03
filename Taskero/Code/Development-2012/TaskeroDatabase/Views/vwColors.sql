IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwColors')
	BEGIN
		DROP  View [Press].[vwColors]
	END
GO
/*
	Creater : Maulik Dhorajia
	Created Date : Sept 9 2008
	Description : This view acts as a temperory table for the master of identifying colors.
*/
CREATE View [Press].[vwColors] AS

SELECT 0 AS ColorID , 'Black' as ColorName
	UNION
SELECT 1 AS ColorID , 'Cyan' as ColorName
	UNION
SELECT 2 AS ColorID , 'Magenta' as ColorName
	UNION
SELECT 3 AS ColorID , 'Yellow' as ColorName

GO
