IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertDevice')
	BEGIN
		DROP  Procedure [CPV].[uspInsertDevice]
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 17 2008
Purpose : Create a New Device.
*/ 
CREATE PROCEDURE [CPV].[uspInsertDevice] 
( 
	@CPDongleID INT , 
	@CPDeviceDesc NVARCHAR(50) ,
	@MeasurementTypeID TINYINT ,
	@CPDeviceTypeID TINYINT ,
	@LastMeasurementID INT
	
) 
AS   
BEGIN  
	BEGIN TRY  

		/*---------- Insert a new Device ----------*/
  		INSERT INTO CPDevice
		(
			 [CPDongleID]
		   , [CPDeviceDesc]
		   , [MeasurementTypeID]
		   , [CPDeviceTypeID] 
		   , [LastMeasurementID] 		   
		) 
		VALUES 
		(
			@CPDongleID ,
			@CPDeviceDesc ,
			@MeasurementTypeID ,
			@CPDeviceTypeID ,
			@LastMeasurementID 
		)  

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






