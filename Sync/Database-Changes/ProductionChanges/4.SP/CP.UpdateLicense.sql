
GO
/****** Object:  StoredProcedure [CP].[UpdateLicense]    Script Date: 5/14/2021 11:04:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [CP].[UpdateLicense]
	@LicenseID			INT
	,@BranchID			INT
	,@ActivationDate	DATETIME
	,@ExpirationDate	DATETIME
	,@NumOfDevice		TINYINT
	,@LicenseDesc		NVARCHAR(30)
	,@IsOneOff			BIT
	,@IsCustomProfOptions BIT
	,@IsActive			BIT
	,@LicenseDetail		XML
	,@IsCustomPlugin	TINYINT
AS
BEGIN
		   
	IF EXISTS (SELECT * FROM CP.License Where LicenseID <> @LicenseID AND BranchID = @BranchID AND IsActive = 1)
		BEGIN  
			Return -1
		END  

	BEGIN TRY
		BEGIN TRAN
		UPDATE	CP.License 
		SET 
				 BranchID		= @BranchID
				,ActivationDate = @ActivationDate
				,ExpirationDate = @ExpirationDate
				,LicenseDesc	= @LicenseDesc
				,IsOneOff		= @IsOneOff
				,IsCustomProfOptions = @IsCustomProfOptions
				,IsActive		= @IsActive
				,IsCustomPlugin = @IsCustomPlugin
		WHERE
				LicenseID = @LicenseID

		
		
		--UPDATING LICENSE DETAILS
		DELETE FROM CP.LicenseDetail WHERE LicenseID = @LicenseID

		;WITH LicenseDetails AS
		(
			SELECT           
				[LicenseDetailXml].value('@LicenseTypeID', 'TINYINT') AS LicenseTypeID
			FROM
				@LicenseDetail.
					nodes('/LicenseDetails/LicenseDetail') p([LicenseDetailXml])
		)
		
		INSERT INTO CP.LicenseDetail
			(
				LicenseID,
				LicenseTypeID
			)
		SELECT
				@LicenseID,
				LicenseTypeID
		FROM
				LicenseDetails

		-----Set No. Of Projects for all other added licensetypes to 0------
		UPDATE DLT SET DLT.NumOfProject = 0 FROM CP.DeviceLicenseType AS DLT 
				INNER JOIN  CP.Device AS D ON D.DeviceID = DLT.DeviceID
		WHERE D.LicenseID = @LicenseID AND DLT.LicenseTypeID NOT IN (SELECT LicenseTypeID FROM CP.LicenseDetail WHERE LicenseID = @LicenseID)


		-----Update all existing projects' CustomPlugintype (0 = Plugin and 1 = MTool)------
		IF(@IsCustomPlugin = 1 OR @IsCustomPlugin = 2) ----Do only if option selected is 1 = Plugin and 2 = MTool, don't do anything for "Both" option selected----
		BEGIN
			UPDATE PC SET IsCustomPlugin = CASE 
												WHEN @IsCustomPlugin = 1 THEN 0 ----Assign Plugin
												WHEN @IsCustomPlugin = 2 THEN 1 ----Assign MTool
											END
			FROM [CPSync4D].[PrintCondition] AS PC
				INNER JOIN [CPSync4D].[Project] AS P ON P.ProjectID = PC.ProjectID
			WHERE P.CPDeviceID IN (SELECT [DeviceID] FROM [CP].[Device] WHERE [LicenseID] = @LicenseID)
		END

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













