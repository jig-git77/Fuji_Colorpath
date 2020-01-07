USE [ColorPathStage]
GO
/****** Object:  StoredProcedure [CP].[GetValidateUser]    Script Date: 12/31/2019 10:42:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
DECLARE @val BIT 
EXEC [CP].[GetValidateUser] 
	@Token = 'dFPy6MHWtmUFUaaHs9641fIcp8I='
	,@validUser =@val OUT
	SELECT @val
*/

ALTER PROCEDURE [CP].[GetValidateUser] (
	@Token NVARCHAR(200)
	,@validUser BIT OUT
	)
AS
BEGIN
	IF EXISTS (
			SELECT *
			FROM [CP].[User]
			WHERE APIAuthKey = @Token
			AND DATEADD(minute,90,LastActiveDate) > GETDATE()
			)
	BEGIN		
		SET @validUser = 1
	END
	ELSE
	BEGIN
		SET @validUser = 0
	END
END

