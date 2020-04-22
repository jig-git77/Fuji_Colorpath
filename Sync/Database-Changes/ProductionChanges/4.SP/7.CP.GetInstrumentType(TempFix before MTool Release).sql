GO
/****** Object:  StoredProcedure [CP].[GetInstrumentType]    Script Date: 4/7/2020 12:19:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[GetInstrumentType]

AS
BEGIN

		SELECT 
				I.InstrumentTypeID,
				I.InstrumentType
		FROM CP.InstrumentType AS I
		WHERE I.IsUsed = 1 AND I.InstrumentTypeID NOT IN (9,10)
		ORDER BY I.InstrumentType


END

