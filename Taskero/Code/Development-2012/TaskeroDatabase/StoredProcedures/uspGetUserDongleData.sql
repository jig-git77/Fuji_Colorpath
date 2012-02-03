IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetUserDongleData')
	BEGIN
		DROP  Procedure [CPV].[uspGetUserDongleData]
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 28 2008  
Purpose : Get User Dongle Data On User ID
*/

CREATE PROCEDURE [CPV].[uspGetUserDongleData] 
(
	@TaskeroUserID INT
)
AS
BEGIN  
	BEGIN TRY
		/*------------- Select all user Dongle Data----------------*/ 
		SELECT  
			CPDongleID
		FROM
				UserDongle
		WHERE
				TaskeroUserID = @TaskeroUserID 
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

