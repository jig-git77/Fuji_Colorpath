

/****** Object:  StoredProcedure [CP].[InsertDevice]    Script Date: 4/5/2021 4:59:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CP].[InsertDevice]
 @LicenseID			INT
,@DeviceName		NVARCHAR(50)
,@DeviceFamilyID	TINYINT
,@IsActive			BIT
,@IsValidation			BIT
,@LicenseDetail		XML
,@IsFlexo			BIT
,@IsSupportMulticolor BIT
,@TotalNoColor TINYINT
,@Channel_5 TINYINT
,@Channel_6 TINYINT
,@Channel_7 TINYINT
,@Channel_8 TINYINT
,@Channel_9 TINYINT
AS
BEGIN
	
	IF EXISTS (SELECT * FROM [CP].Device Where Upper(DeviceName) = Upper(@DeviceName) AND LicenseID = @LicenseID)  
				BEGIN  
					Return -1
				END   

	BEGIN TRY
		BEGIN TRAN
		
		INSERT INTO	CP.Device 
			(
				 LicenseID
				,DeviceName
				,DeviceFamilyID
				,IsActive
				,IsValidation
				,IsFlexo
				,IsSupportMulticolor
				,TotalNoColor
				,Channel_5
				,Channel_6
				,Channel_7
				,Channel_8
				,Channel_9
			)
		VALUES 
			(
				 @LicenseID
				,@DeviceName
				,@DeviceFamilyID
				,@IsActive
				,@IsValidation
				,@IsFlexo
				,@IsSupportMulticolor
				,@TotalNoColor
				,@Channel_5
				,@Channel_6
				,@Channel_7
				,@Channel_8
				,@Channel_9
			) 		
		DECLARE @DeviceID INT
		SET @DeviceID = @@IDENTITY

		--INSERTING LICENSE DETAILS
		;WITH LicenseDetails AS
		(
			SELECT           
				[LicenseDetailXml].value('@LicenseTypeID', 'TINYINT') AS LicenseTypeID,
				[LicenseDetailXml].value('@NumOfProject', 'INT') AS NumOfProject
			FROM
				@LicenseDetail.
					nodes('/LicenseDetails/LicenseDetail') p([LicenseDetailXml])
		)
		
		INSERT INTO CP.DeviceLicenseType
			(
				DeviceID,
				LicenseTypeID,
				NumOfProject
			)
		SELECT
				@DeviceID,
				LicenseTypeID,
				NumOfProject
		FROM
				LicenseDetails
		  
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END



GO


