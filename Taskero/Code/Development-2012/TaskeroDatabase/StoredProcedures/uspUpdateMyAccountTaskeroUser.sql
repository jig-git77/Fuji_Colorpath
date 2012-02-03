IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateMyAccountTaskeroUser')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateMyAccountTaskeroUser]
	END

GO

/*      
AUTHOR : Shivpal Jhala      
LAST UPDATED DATE : July 23 , 2008      
PURPOSE :- Creates a new user      
*/      
CREATE PROCEDURE [CPV].[uspUpdateMyAccountTaskeroUser]       
(       
 @TaskeroUserID INT = NULL,       
 @LoginPass NVARCHAR(40) ,       
 @LastName NVARCHAR(60) ,       
 @FirstName NVARCHAR(60) ,       
 @Email NVARCHAR(100) ,        
 @LastModified_UserID INT = NULL,
 @Customer VARCHAR(50) = NULL,
 @AllowedIP VARCHAR(15) = NULL,
 @EmailWhenYellow BIT,
 @EmailWhenRed BIT,
 @Result INT = 0 OUTPUT       
) 
AS
BEGIN      
	BEGIN TRY   
		SET @Result = 1     
		--************************  Updating The TaskeroUser Table is passowrd is not set ************************--      
		if isnull(@LoginPass,'')=''  
		BEGIN      
			--*** setting the @Result =1 if the duplicate Username is found***--      
			Update   
				TaskeroUser      
			SET    
				[LastName] = @LastName       
				, [FirstName] = @FirstName   
				, [Email] = @Email  
				, [LastModified_UserID] = @LastModified_UserID  
				, [LastModifiedDate] = GETDATE()    
				, [Customer] = @Customer
				 , AllowedIP = @AllowedIP
				 , [EmailWhenYellow] = @EmailWhenYellow
				, [EmailWhenRed] = @EmailWhenRed
			WHERE  
				TaskeroUserID = @TaskeroUserID  
		END      
	ELSE      
	BEGIN         
		--************************  Updating The TaskeroUser Table is passowrd is set ************************--      
		Update   
			TaskeroUser      
		SET       
			[LoginPass] = @LoginPass  
			, [LastName] = @LastName       
			, [FirstName] = @FirstName   
			, [Email] = @Email  
			, [LastModified_UserID] = @LastModified_UserID  
			, [LastModifiedDate] = GETDATE()  
			, [Customer] = @Customer
			, AllowedIP = @AllowedIP
			, [EmailWhenYellow] = @EmailWhenYellow
				, [EmailWhenRed] = @EmailWhenRed
		WHERE  
			TaskeroUserID = @TaskeroUserID  
	END  
		--*** setting the @Result =2 for the successfully insert of data***--      
		SET @Result = 2      
	END TRY      
	BEGIN CATCH      
		--*** Error Handling***--      
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


