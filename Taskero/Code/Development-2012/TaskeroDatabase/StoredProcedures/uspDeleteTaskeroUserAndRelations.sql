IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspDeleteTaskeroUserAndRelations')
	BEGIN
		DROP  Procedure [CPV].[uspDeleteTaskeroUserAndRelations]      
	END

GO

/*            
AUTHOR : Shivpal Jhala            
Created Date : July 17, 2008            
Purpose : To Delete Taskero User and its Relations
*/            
CREATE PROCEDURE [CPV].[uspDeleteTaskeroUserAndRelations]      
(    
	@LoginID nvarchar (100),
	@Result int output  
)    
AS     
BEGIN        
	BEGIN TRY
		SET @Result = 1
		--***Declaration of TaskeroUserID for Further Use**--       
		DECLARE @TaskeroUserID int

		--***Retrieving the TaskeroUserID for Further Use**--
		SELECT 
			@TaskeroUserID = TaskeroUserID
		FROM
			TaskeroUser
		WHERE
			LoginID=@LoginID

		--***Delete the Reference of Taskero User from UserCompany Table**--       
		DELETE FROM 
			UserCompany
		WHERE
			TaskeroUserID = @TaskeroUserID

		--***Delete the Reference of Taskero User from UserBranch Table**--       
		DELETE FROM 
			UserBranch
		WHERE
			TaskeroUserID = @TaskeroUserID

		--***Delete the Reference of Taskero User from UserDongle Table**--       
		DELETE FROM 
			UserDongle
		WHERE
			TaskeroUserID = @TaskeroUserID

		--***Delete the Reference of Taskero User from UserPermission Table**--       
		DELETE FROM 
			UserPermission
		WHERE
			TaskeroUserID = @TaskeroUserID

		--***Delete the User from TaskeroUser Table**--       
		DELETE FROM 
			TaskeroUser
		WHERE
			TaskeroUserID = @TaskeroUserID
		SET @Result = 2
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
  
  








