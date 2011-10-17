IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetSupportUserWithDongleByBranchID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetSupportUserWithDongleByBranchID]
	END

GO

/*              
AUTHOR : Jitendra Makwana
Created Date : August 21, 2008              
Purpose : To Retrieve the Dongle Data by BranchID with support user list.
*/
CREATE Procedure [CPV].[uspGetSupportUserWithDongleByBranchID]
(
	@BranchID NVARCHAR(1000) ,      
	@TaskeroUserID INT ,  
	@RoleID TINYINT , 
	@UserRoleID TINYINT  
)
AS
BEGIN
	BEGIN TRY
		/*----- Get Support User List ---------*/
		DECLARE @xmlDongle XML      
		SET @xmlDongle = @BranchID
		SELECT 
			TaskeroUserID , 
			LoginID 
		FROM 
			TaskeroUser 
		WHERE 
			TaskeroUserID 
		IN 
		( 
			SELECT 
				TaskeroUserID 
			FROM 
				UserDongle 
			WHERE CPDongleID 
			IN 
			( 
				SELECT 
					CPDongleID 
				FROM 
					CPDongle 
				WHERE 
					BranchID 
					IN 
					(
						SELECT 
							BranchID 
						FROM 
							Branch 
						WHERE 
							BranchID IN(SELECT xmlDongle.value('.', 'VARCHAR(20)') AS ID FROM @xmlDongle.nodes('/Branch/BranchID') d(xmlDongle))
					)
			)
		) 
		AND 
			RoleID = @UserRoleID
		UNION
		SELECT 0 as TaskeroUserID 
			, ''
		ORDER BY TaskeroUserID DESC
		
		/*----- Get Dongle List ---------uspGetDongleByBranchID*/
		EXEC CPV.uspGetDongleByBranchID @BranchID = @BranchID, @TaskeroUserID = @TaskeroUserID , @RoleID = @RoleID
		/*----- Get All Support User */
		SELECT 
			TaskeroUserID , 
			LoginID 
		FROM 
			TaskeroUser 
		WHERE
			RoleID = @UserRoleID
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

GO
