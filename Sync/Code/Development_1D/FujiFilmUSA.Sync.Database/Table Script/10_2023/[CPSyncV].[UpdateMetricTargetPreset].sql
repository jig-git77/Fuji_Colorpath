/****** Object:  StoredProcedure [CPSyncV].[UpdateMetricTargetPreset]    Script Date: 10/30/2023 12:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSyncV].[UpdateMetricTargetPreset]   
	@MetricTargetPresetID INT,
	@RegionID TINYINT,
	@CompanyID INT,
	@MetricTargetPresetName NVARCHAR(50),
	@MetricTargetPresetDesc NVARCHAR(300),
	@dEMethod NVARCHAR(50),
	@ValidationType INT,
	@IsSync1D BIT,
	@Validation_1D BIT,
	@Validation_4D BIT,
	@Weighted BIT,
	@IsUsed INT
AS  
BEGIN 
	
	IF EXISTS (SELECT * FROM CPSyncV.MetricTargetPreset Where MetricTargetPresetID <> @MetricTargetPresetID 
					AND Upper(MetricTargetPresetName) = Upper(@MetricTargetPresetName) AND ISNULL(RegionID,0) = ISNULL(@RegionID,0) AND ISNULL(CompanyID,0) = ISNULL(@CompanyID,0) AND IsUsed = 1)  
		BEGIN  
			Return -1
		END

	BEGIN TRY
		BEGIN TRAN
			UPDATE	CPSyncV.MetricTargetPreset
			SET 
				RegionID = @RegionID
				,CompanyID = @CompanyID
				,MetricTargetPresetName = @MetricTargetPresetName
				,MetricTargetPresetDesc = @MetricTargetPresetDesc
				,dEMethod = @dEMethod
				,ValidationType = @ValidationType
				,IsSync1D = @IsSync1D
				,Validation_1D = @Validation_1D
				,Validation_4D = @Validation_4D
				,Weighted =@Weighted
				,IsUsed = @IsUsed
			WHERE
				MetricTargetPresetID = @MetricTargetPresetID	
			
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