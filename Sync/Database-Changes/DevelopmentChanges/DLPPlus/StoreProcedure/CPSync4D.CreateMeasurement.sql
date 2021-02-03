
GO

/****** Object:  StoredProcedure [CPSync4D].[CreateMeasurement]    Script Date: 1/12/2021 4:29:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CPSync4D].[CreateMeasurement]
	@MeasurementName NVARCHAR(50),
	@OptimizeRunID INT,
	@MeasurementDate DATETIME,
	@ReferenceChartTypeID TINYINT,
	@MeasurementPatches XML,
	@MeasurementID INT OUTPUT,
	@PCSDataType INT = 1,
	@UserID INT,
	@MeasurementInfo  NVARCHAR(500)
AS
	BEGIN TRY
		
		BEGIN TRAN
			DECLARE @MTPID smallInt;
			DECLARE @ProjectID INT;
			DECLARE @LicenseTypeID TINYINT
			SELECT @ProjectID = ProjectID from CPSync4D.OptimizeRun where OptimizeRunID = @OptimizeRunID
			
			SELECT @MTPID = MetricTargetPresetID from CPSync4D.Project where ProjectID = @ProjectID
							
			INSERT INTO CPSync4D.Measurement
				(
					MeasurementName,
					OptimizeRunID,
					MeasurementDate,
					ReferenceChartTypeID,
					IsUsed,
					MetricTargetPresetID,
					MeasurementInfo
				)
			VALUES
				(
					@MeasurementName,
					@OptimizeRunID,
					@MeasurementDate,
					@ReferenceChartTypeID,
					1,
					@MTPID,
					@MeasurementInfo
				)
				
			SET @MeasurementID = @@IDENTITY
			EXEC [CPSync4D].[CreateMeasurementPatches] @PCSDataType,@MeasurementPatches,@MeasurementID 

			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE() WHERE ProjectID = @ProjectID

			-----Delete From dbo.MeasurementError_File table if measurement upload sucessfully ---
			 DELETE FROM dbo.MeasurementError_File WHERE OptimizationRunID=@OptimizeRunID AND ProjectID=@ProjectID

			 SELECT @LicenseTypeID=LicenseTypeID FROM CPSync4D.Project where ProjectID=@ProjectID
			 IF(@LicenseTypeID =11)---DLP PLUS
			 BEGIN
				  ---INSERT ΔCh VALUES INTO CPSync.MeasuredColorChar TABLE
					EXEC [CPSync4DPlus].[uspInsertColorCharΔCh] @MeasurementID,@UserID
					---INSERT ΔL VALUES INTO CPSync.MeasuredColorChar TABLE
					EXEC [CPSync4DPlus].[uspInsertColorCharΔL] @MeasurementID,@UserID
					--------Set Metrics----------------			
					EXEC [CPSync4DPlus].[CalculateMetrics] @MeasurementID,@UserID											
					-----Update IsOk entry onto CPSync.Measurement table-------
					UPDATE CPSync4D.Measurement SET IsOK = 1 WHERE MeasurementID = @MeasurementID
					-----Delete From dbo.MeasurementError_File table if ISOK =1 ---
					DELETE FROM dbo.MeasurementError_File WHERE OptimizationRunID=@OptimizeRunID AND ProjectID=@ProjectID

			 END

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @MeasurementID=-1
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 

		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in 4D project measurement',@ERROR_MESSAGE,'Sync4D'

		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH



GO


