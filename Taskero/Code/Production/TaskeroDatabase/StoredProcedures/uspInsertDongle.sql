IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertDongle')
	BEGIN
		DROP  Procedure [CPV].[uspInsertDongle] 
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 15 2008
Purpose : Create a New Dongle.
*/ 
CREATE PROCEDURE [CPV].[uspInsertDongle] 
( 
	@BranchID INT , 
	@CPSerialNUM NVARCHAR(50) ,
	@DongleDescription NVARCHAR(50) , 
	@AllowedIPFrom NVARCHAR(50) ,
	@AllowedIPTo NVARCHAR(50) , 
	@LastModifiedBy INT ,
	@LastModifiedDate DATETIME , 
	@IsUsed BIT		
) 
AS   
BEGIN  
	BEGIN TRY  

		/*---------- Insert a new Dongle ----------*/
  		INSERT INTO CPDongle
		(
			 [BranchID]
		   , [CPSerialNUM]
		   , [DongleDescription]
		   , [AllowedIPFrom] 
		   , [AllowedIPTo] 
		   , [LastModifiedBy] 
		   , [LastModifiedDate]
		   , [IsUsed]
		) 
		VALUES 
		(
			@BranchID
		  , @CPSerialNUM 
		  , @DongleDescription 
		  , @AllowedIPFrom 
		  , @AllowedIPTo 
		  , @LastModifiedBy 
		  , GETDATE()
		  , @IsUsed
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


