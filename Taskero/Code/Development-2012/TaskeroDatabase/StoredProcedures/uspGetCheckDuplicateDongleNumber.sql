IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCheckDuplicateDongleNumber')
	BEGIN
		DROP  Procedure [CPV].[uspGetCheckDuplicateDongleNumber]
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 16 2008
Purpose : To check the Duplication Dongle Serial No.
*/

CREATE PROCEDURE [CPV].[uspGetCheckDuplicateDongleNumber]
( 
	@CPDongleID INT , 
	@CPSerialNUM NVARCHAR(100) 
) 
AS
BEGIN
	BEGIN TRY
		
		/*---------- Checks Duplicate Dongle Serial No. ----------*/
		SELECT 
			CPSerialNUM 
		FROM 
			[CPDongle] 
		WHERE [CPSerialNUM] = @CPSerialNUM 
				AND [CPDongleID] <> @CPDongleID

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

