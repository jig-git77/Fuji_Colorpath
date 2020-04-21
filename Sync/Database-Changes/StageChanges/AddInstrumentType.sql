USE [ColorPathStage]
GO


---Insert 2 new Instrument types entry into CP.IntrumentType table------

INSERT INTO [CP].[InstrumentType]
           ([InstrumentTypeID]
           ,[InstrumentType]
           ,[IsUsed])
     VALUES
           (9
           ,'i1-Pro3'
           ,1)

		   
INSERT INTO [CP].[InstrumentType]
           ([InstrumentTypeID]
           ,[InstrumentType]
           ,[IsUsed])
     VALUES
           (10
           ,'i1-IO-Pro3'
           ,1)
GO
GO


