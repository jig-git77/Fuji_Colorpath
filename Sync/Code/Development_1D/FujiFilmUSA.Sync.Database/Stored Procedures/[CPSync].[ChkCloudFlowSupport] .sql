
/****** Object:  StoredProcedure [CPSync].[ChkCloudFlowSupport]    Script Date: 3/17/2023 2:14:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CPSync].[ChkCloudFlowSupport]	
	@ProjectID INT,	
	@ReferenceChartTypeID INT,
	@CompatibilityNo INT OUTPUT	
AS
	BEGIN TRY	
		BEGIN TRAN
			
			DECLARE @DF_DeviceFamilyID INT;
			DECLARE @DF_IsSupportCloudFlow BIT;
			DECLARE @R_DeviceFamilyID INT;
			DECLARE @R_IsSupportCloudFlow BIT;

			SELECT @DF_DeviceFamilyID = DF.[DeviceFamilyID], @DF_IsSupportCloudFlow = DF.[IsSupportCloudFlow]
				FROM [CPSync4D].[Project] AS P
				INNER JOIN [CP].[Device] AS D ON D.DeviceID = P.CPDeviceID
				INNER JOIN [CP].[DeviceFamily] AS DF ON DF.DeviceFamilyID = D.DeviceFamilyID
				WHERE [ProjectID] = @ProjectID
		

			SELECT @R_DeviceFamilyID = [DeviceFamilyID], @R_IsSupportCloudFlow = [IsSupportCloudFlow]
				FROM [CP].[ReferenceChartType]
				WHERE [ReferenceChartTypeID] = @ReferenceChartTypeID
			
			IF(@DF_DeviceFamilyID = @R_DeviceFamilyID AND @DF_IsSupportCloudFlow = @R_IsSupportCloudFlow)
			BEGIN
				SET @CompatibilityNo = 1 ----Success
			END
			ELSE
			BEGIN
				SET @CompatibilityNo = 0 ---Not Success
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






