IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateDongle')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateDongle]
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 15 2008
Purpose : Update a Existing Dongle.
*/ 
CREATE PROCEDURE [CPV].[uspUpdateDongle] 
( 
	@CPDongleID INT ,
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

		/*---------- Update an Existing Dongle ----------*/
  		UPDATE 
			CPDongle
		SET 
				 [BranchID] = @BranchID
			   , [CPSerialNUM] = @CPSerialNUM 
			   , [DongleDescription] = @DongleDescription 
			   , [AllowedIPFrom] = @AllowedIPFrom 
			   , [AllowedIPTo] = @AllowedIPTo
			   , [LastModifiedBy] = @LastModifiedBy
			   , [LastModifiedDate] = GETDATE()
			   , [IsUsed] = @IsUsed			
		WHERE 
			[CPDongleID] = @CPDongleID

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

