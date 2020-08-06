
GO
/****** Object:  StoredProcedure [CP].[ValidateApplicationMeasurement]    Script Date: 8/6/2020 6:27:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
EXEC [CP].[ValidateApplicationMeasurement]
	@optimizatioNRunID=37952,
	@ProjectId=19605,
	@MeaDeviceSerialNo=304060,
	@EntryDate = 11111

*/


ALTER PROCEDURE [CP].[ValidateApplicationMeasurement]
(   
	@optimizationRunID INT,
	@ProjectId INT,
	@MeaDeviceSerialNo nvarchar(100),
	@EntryDate nvarchar(100)
)   
AS   	
BEGIN
	
	DECLARE  @vLicenseTypeID TINYINT = 0, @vMesaurementCount INT = 0, @vMeasurementsLimit INT = 0;
	DECLARE @vOptiRunStatus TINYINT = 0
	DECLARE @IsValidationProject BIT = 0
	
	IF(@optimizationRunID = -1 OR @optimizationRunID IS NULL)
	BEGIN
		SET @optimizationRunID = 0;
	END

	SELECT @vMeasurementsLimit = ConfigurationValue FROM ApplicationConfiguration WHERE ConfigurationKey = 'MeasurementsLimit'
 	
	IF EXISTS (SELECT	* FROM CP.MeasurementPluginStatus WHERE ISNULL(OptimizationRunID,0) = ISNULL(@optimizationRunID,0)  AND ProjectID = @ProjectId AND MeaDeviceSerialNo = @MeaDeviceSerialNo) ---If Entry exists
	BEGIN

		IF EXISTS (SELECT	* FROM CP.MeasurementPluginStatus WHERE ISNULL(OptimizationRunID,0) = ISNULL(@optimizationRunID,0)  AND ProjectID = @ProjectId AND MeaDeviceSerialNo = @MeaDeviceSerialNo AND IsActive = 1 AND DATEDIFF(SECOND,'1970-01-01',EntryDate) > @EntryDate)  ---If Entry exists and OriginalEntryDate EPoch value is lesser than current EntryDateValue, that means MTool app needs to be updated
		BEGIN
				SET @vOptiRunStatus = 5
		END
		ELSE
		BEGIN
			SET @vOptiRunStatus = 1

				SELECT
					@vLicenseTypeID = LicenseTypeID
				FROM CP.MeasurementPluginStatus
				WHERE OptimizationRunID = @optimizationRunID
				AND ProjectID = @ProjectId
				AND MeaDeviceSerialNo = @MeaDeviceSerialNo
				--AND IsActive = 1

				---Check Condition for Validation project 
				IF EXISTS (SELECT ProjectID FROM CP.MeasurementPluginStatus WHERE ISNULL(OptimizationRunID,0) = ISNULL(@optimizationRunID,0) AND ProjectID = @ProjectId 
				AND MeaDeviceSerialNo = @MeaDeviceSerialNo AND LOWER(REPLACE(ProfileType,' ','')) = LOWER(REPLACE('Validation',' ','')))
				BEGIN
					SET @IsValidationProject = 1
				END
		END

	END			
	ELSE
	BEGIN
			IF EXISTS (SELECT	* FROM CP.MeasurementPluginStatus WHERE ISNULL(OptimizationRunID,0) = ISNULL(@optimizationRunID,0)  AND ProjectID = @ProjectId AND MeaDeviceSerialNo != @MeaDeviceSerialNo) ---If Entry exists, but device is changed
			BEGIN	
				SET @vOptiRunStatus = 6 -- Means project exists, but device is changed.
			END
			ELSE
			BEGIN
				SET @vOptiRunStatus = 4 -- Means project doesn't exists.
			END
		
	END

	PRINT @vLicenseTypeID
	--IF Project is not Validation Project
	IF(@IsValidationProject = 0 AND @vLicenseTypeID != 0)
	BEGIN
		
					IF (@vLicenseTypeID = 1)-- 1d project
					BEGIN
							PRINT '1D'
							SELECT
								@vMesaurementCount = COUNT(MeasurementID)
							FROM CPSync.Measurement
							WHERE OptimizeRunID = @optimizationRunID

							SELECT
								@vOptiRunStatus = [Status]
							FROM CPSync.OptimizeRun
							WHERE OptimizeRunID = @optimizationRunID
							
					END
	
					ELSE IF (@vLicenseTypeID = 2 OR @vLicenseTypeID =3 OR @vLicenseTypeID = 5 OR @vLicenseTypeID = 6  OR @vLicenseTypeID = 7 ) -- 4D project 
					BEGIN
							PRINT '4D'
							SELECT
								@vMesaurementCount = COUNT(MeasurementID)
							FROM CPSync4D.Measurement
							WHERE OptimizeRunID = @optimizationRunID

							SELECT
								@vOptiRunStatus =[Status]
							FROM CPSync4D.OptimizeRun
							WHERE OptimizeRunID = @optimizationRunID
					END
					ELSE IF (@vLicenseTypeID = 10) -- Spot project 
					BEGIN
							PRINT 'Sport'
							SELECT
								@vMesaurementCount = COUNT(MeasurementID)
							FROM CPSpot.Measurement
							WHERE OptimizeRunID = @optimizationRunID

							SELECT
								@vOptiRunStatus =[Status]
							FROM CPSpot.OptimizeRun
							WHERE OptimizeRunID = @optimizationRunID
					END
	END
	ELSE IF(@IsValidationProject = 1 AND @vLicenseTypeID != 0) --If Project is Validation Project
	BEGIN
		
		IF (@vLicenseTypeID != 0)
			BEGIN
				SELECT
					@vMesaurementCount = COUNT(MeasurementID)
				FROM CPSyncV.Measurement
				WHERE ProjectID = @ProjectId
					
		 END
	END

	SELECT
		@vOptiRunStatus AS isValidProjectRun
	   ,@vMeasurementsLimit - @vMesaurementCount AS RemainingMeasurementsCount
	  PRINT @IsValidationProject  
END

