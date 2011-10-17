IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertSQLErrorLog')
	BEGIN
		DROP  Procedure [CPV].[uspInsertSQLErrorLog]
	END

GO

/*========== INSERT ==========*/
/*
AUTHOR : System generated script.
*/
CREATE PROCEDURE [CPV].[uspInsertSQLErrorLog] 
( 
	@ErrorNumber INT , 
	@ErrorSeverity INT , 
	@ErrorState INT , 
	@ErrorProcedure NVARCHAR(252) , 
	@ErrorLine INT , 
	@ErrorMessage NVARCHAR(4000) 
) 
AS 
BEGIN
	
	/*---------- INSERT A NEW RECORD IN SQLErrorLog ----------*/
	INSERT INTO SQLErrorLog
	(
		[ErrorTime] 
		, [UserName] 
		, [ErrorNumber] 
		, [ErrorSeverity] 
		, [ErrorState] 
		, [ErrorProcedure] 
		, [ErrorLine] 
		, [ErrorMessage]
	) 
	VALUES 
	(
		GETDATE() 
		, USER 
		, @ErrorNumber 
		, @ErrorSeverity 
		, @ErrorState 
		, @ErrorProcedure 
		, @ErrorLine 
		, @ErrorMessage
	)
END

