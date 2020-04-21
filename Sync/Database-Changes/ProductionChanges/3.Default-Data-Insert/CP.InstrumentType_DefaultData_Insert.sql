IF NOT EXISTS(SELECT * FROM CP.InstrumentType WHERE InstrumentTypeID=9 AND InstrumentType='i1-Pro3' AND IsUsed=1)
BEGIN
	INSERT INTO CP.InstrumentType(InstrumentTypeID,InstrumentType,IsUsed)
	VALUES(9,'i1-Pro3',1)
END
GO
IF NOT EXISTS(SELECT * FROM CP.InstrumentType WHERE InstrumentTypeID=10 AND InstrumentType='i1-IO-Pro3' AND IsUsed=1)
BEGIN
	INSERT INTO CP.InstrumentType(InstrumentTypeID,InstrumentType,IsUsed)
	VALUES(10,'i1-IO-Pro3',1)
END


