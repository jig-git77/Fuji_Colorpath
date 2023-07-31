/****** Object:  StoredProcedure [CPSync4D].[ImportMeasurementDetails]    Script Date: 7/13/2023 5:20:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSync4D].[ImportMeasurementDetails]
	@MeasurementName NVARCHAR(50),
	@OptimizeRunID INT,
	@MeasurementDate DATETIME,
	@ReferenceChartTypeID TINYINT,
	@MeasurementPatches XML,
	@MeasurementID INT OUTPUT,
	@PCSDataType INT = 1,	
	@LicenseType INT,
	@ProjectID INT,
	@Notes NVARCHAR(1000),
	@Customer NVARCHAR(50),
	@PageOrSurface NVARCHAR(50),
	@JDFJobId NVARCHAR(50),
	@UserID INT
AS
	BEGIN TRY

		DECLARE @TotalReferenceChartPatches INT
		DECLARE @TotalCurrentPatches INT

		SELECT @TotalCurrentPatches= COUNT(1) FROM
						@MeasurementPatches.nodes('/MeasurementPatches/MeasurementPatch') p([MeasurementPatchXml])

		IF(@LicenseType = 1 OR @LicenseType = 2 OR @LicenseType = 51 OR @LicenseType = 11 OR @LicenseType = 12 OR @LicenseType = 13 Or  @LicenseType =15 )
		BEGIN

			SELECT @TotalReferenceChartPatches = (CAST(NumOfRows AS INT) * CAST(NumOfCols AS INT)) FROM [CP].[ReferenceChartType]
			WHERE ReferenceChartTypeID=@ReferenceChartTypeID
			
			PRINT @TotalReferenceChartPatches

			PRINT @TotalCurrentPatches

			IF(@TotalReferenceChartPatches = @TotalCurrentPatches)
			BEGIN

				IF(@LicenseType IN (1,13) ) --1D and Sync1DMultiColor
				BEGIN				
						EXEC [CPSync].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END

				IF(@LicenseType IN (2,12,11,15)) -- DLP & DLPMultiColor Channel & DLPLUS & Custom destination profile
				BEGIN
					EXEC [CPSync4D].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END

				IF(@LicenseType = 51) -- validation
				BEGIN
					EXEC [CPSyncV].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@ProjectID  = @ProjectID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@Notes = @Notes,
						@Customer = @Customer,
						@PageOrSurface = @PageOrSurface,
						@JDFJobId = @JDFJobId,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END
			END
			ELSE
			BEGIN
				SET @MeasurementID = -2
				RETURN
			END

		END
		ELSE
		BEGIN

			DECLARE @SpotProjectID INT

			SELECT @SpotProjectID = SpotProjectID FROM CPSpot.OptimizeRun WHERE OptimizeRunID=@OptimizeRunID
			
			SELECT @TotalReferenceChartPatches = (CAST(TotalRowsInChart AS INT) * CAST(NumOfCols AS INT)) FROM CPSpot.SpotProjectChartSetting
			WHERE SpotProjectID=@SpotProjectID

			IF(@TotalReferenceChartPatches = @TotalCurrentPatches)
			BEGIN
				
				EXEC [CPSpot].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL
								
			END
			ELSE
			BEGIN
				SET @MeasurementID = -2
				RETURN
			END

		END

	END TRY
	BEGIN CATCH
		SET @MeasurementID = -1
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
