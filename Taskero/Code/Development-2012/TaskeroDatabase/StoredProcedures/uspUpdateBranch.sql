IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateBranch')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateBranch]
	END

GO

/*========== UPDATE ==========*/  
/*  
AUTHOR : Jitendra Makwana
LAST UPDATED DATE : July 15 , 2008
PURPOSE :- Updates a new Branch
*/  
CREATE PROCEDURE [CPV].[uspUpdateBranch]
( 
	@BranchID INT , 
	@CompanyID INT , 
	@BranchName NVARCHAR(60) , 
	@BranchLocation NVARCHAR(60) , 
	@PrimaryContactName NVARCHAR(100) , 
	@Tel_CountryCode NVARCHAR(10) , 
	@Tel_PhoneNo NVARCHAR(40) , 
	@Tel_Extension NVARCHAR(10) , 
	@PrimaryContactEmail NVARCHAR(100) , 
	@PrimaryContactCell NVARCHAR(40) , 
	@LastModifiedBy INT , 
	@LastModifiedDate DATETIME , 
	@IsActive BIT ,
	@Result int=0 output 
) 
AS   
BEGIN  
	BEGIN TRY
		IF NOT EXISTS
		( 
			SELECT 
				BranchName 
			FROM
				Branch
			WHERE
				BranchName = @BranchName AND CompanyID = @CompanyID	AND BranchID != @BranchID
		)
		BEGIN  
  				UPDATE 
					Branch 
				SET 
					[CompanyID] = @CompanyID 
					, [BranchName] = @BranchName 
					, [BranchLocation] = @BranchLocation 
					, [PrimaryContactName] = @PrimaryContactName 
					, [Tel_CountryCode] = @Tel_CountryCode 
					, [Tel_PhoneNo] = @Tel_PhoneNo 
					, [Tel_Extension] = @Tel_Extension 
					, [PrimaryContactEmail] = @PrimaryContactEmail 
					, [PrimaryContactCell] = @PrimaryContactCell 
					, [LastModifiedBy] = @LastModifiedBy 
					, [LastModifiedDate] = @LastModifiedDate 
					, [IsActive] = @IsActive 
				WHERE 
					[BranchID] = @BranchID 
				SET @Result = 1
		END
		ELSE
		BEGIN
				SET @Result = 0
		END 
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
