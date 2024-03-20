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
,@Channel_5_CMYK NVARCHAR(20)
,@Channel_6 TINYINT
,@Channel_6_CMYK NVARCHAR(20)
,@Channel_7 TINYINT
,@Channel_7_CMYK NVARCHAR(20)
,@Channel_8 TINYINT
,@Channel_8_CMYK NVARCHAR(20)
,@Channel_9 TINYINT
,@Channel_9_CMYK NVARCHAR(20)
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
				,Channel_5_CMYK
				,Channel_6
				,Channel_6_CMYK
				,Channel_7
				,Channel_7_CMYK
				,Channel_8
				,Channel_8_CMYK
				,Channel_9
				,Channel_9_CMYK
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
				,@Channel_5_CMYK
				,@Channel_6
				,@Channel_6_CMYK
				,@Channel_7
				,@Channel_7_CMYK
				,@Channel_8
				,@Channel_8_CMYK
				,@Channel_9
				,@Channel_9_CMYK
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