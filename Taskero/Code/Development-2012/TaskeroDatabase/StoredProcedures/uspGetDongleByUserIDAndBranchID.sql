IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDongleByUserIDAndBranchID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDongleByUserIDAndBranchID]
	END

GO

/*
AUTHOR : Tushar Patel
Created Date : July 17 2008
Purpose : To Get CPDongle of Branch by UserID
*/
CREATE PROCEDURE [CPV].[uspGetDongleByUserIDAndBranchID] 
( 
	@BranchID INT,
	@TaskeroUserID INT,  
	@RoleID TINYINT
) 
AS   
BEGIN
	BEGIN TRY

		/*----------- Get the CPDongle of Company by UserID ----------*/
		if @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the CPDonglees)
		BEGIN
		
			SELECT 
				CPDongleID ,CPSerialNUM ,DongleDescription , CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial    
			FROM 
				CPDongle
			WHERE
				BranchID = @BranchID
			ORDER BY 
				DongleDescription  
				
		END
		ELSE
		BEGIN
			
			SELECT 
				DISTINCT CPDongle.CPDongleID ,CPSerialNUM , DongleDescription  , (CPSerialNUM +' - '+ DongleDescription) AS DongleSerial
			FROM 
				CPDongle
			INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDongle.CPDongleID
			WHERE
				UserDongle.TaskeroUserID = @TaskeroUserID
				AND CPDongle.BranchID = @BranchID
			ORDER BY 
				DongleDescription
			
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


