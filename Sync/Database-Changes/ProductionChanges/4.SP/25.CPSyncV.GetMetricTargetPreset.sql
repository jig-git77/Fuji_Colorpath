
/****** Object:  StoredProcedure [CPSyncV].[GetMetricTargetPreset]    Script Date: 4/15/2022 11:06:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [CPSyncV].[GetMetricTargetPreset]   
	@MetricTargetPresetID INT,
	@CurrentUserID INT
AS  
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
	BEGIN TRY
		BEGIN TRAN
			IF ISNULL(@MetricTargetPresetID,0) = 0
			BEGIN
				SELECT  
						MTP.MetricTargetPresetID
						,MTP.MetricTargetPresetName
						,MTP.MetricTargetPresetDesc
						,mtp.RegionID
						,R.Region
						,C.CompanyID
						,C.CompanyName
						,MTP.dEMethod
						,MTP.ValidationType
						,MTP.IsSync1D
						,MTP.Validation_1D
						,MTP.Validation_4D
						,CASE COUNT(PatchTargetPresetID)
							WHEN 0 THEN 0
							ELSE 1
						END AS IsPatchTolerance
						,MTP.IsUsed
					  FROM   
							CPSyncV.MetricTargetPreset MTP 
							LEFT JOIN CP.Region R  
								ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
							LEFT JOIN CP.Company C  
								ON C.CompanyID = MTP.CompanyID  
							LEFT JOIN CPSyncV.PatchTargetPreset AS PTP ON PTP.MetricTargetPresetID = MTP.MetricTargetPresetID

					  WHERE  
						---- MTP.IsUsed = 1 
						----AND
						((CP.CheckUserCompany(@CurrentUserID,ISNULL(MTP.CompanyID,1000)) = 1)
						AND (CP.CheckUserRegion(@CurrentUserID,ISNULL(MTP.RegionID,255)) = 1))

                      GROUP BY MTP.MetricTargetPresetID
						,MTP.MetricTargetPresetName
						,MTP.MetricTargetPresetDesc
						,mtp.RegionID
						,R.Region
						,C.CompanyID
						,C.CompanyName
						,MTP.dEMethod
						,MTP.ValidationType
						,MTP.IsSync1D
						,MTP.Validation_1D
						,MTP.Validation_4D
						,MTP.IsUsed
			END
			ELSE
			BEGIN
				SELECT  
						MTP.MetricTargetPresetID
						,MTP.MetricTargetPresetName
						,MTP.MetricTargetPresetDesc
						,R.RegionID
						,R.Region
						,C.CompanyID
						,C.CompanyName
						,MTP.dEMethod
						,MTP.ValidationType
						,MTP.IsSync1D
						,MTP.Validation_1D
						,MTP.Validation_4D
						,CASE COUNT(PatchTargetPresetID)
							WHEN 0 THEN 0
							ELSE 1
						END AS IsPatchTolerance
						,MTP.IsUsed
					  FROM   
							CPSyncV.MetricTargetPreset MTP 
							LEFT JOIN CP.Region R  
								ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
							LEFT JOIN CP.Company C  
								ON C.CompanyID = MTP.CompanyID  
							LEFT JOIN CPSyncV.PatchTargetPreset AS PTP ON PTP.MetricTargetPresetID = MTP.MetricTargetPresetID

					  WHERE  
						MTP.MetricTargetPresetID = @MetricTargetPresetID
					  GROUP BY MTP.MetricTargetPresetID
						,MTP.MetricTargetPresetName
						,MTP.MetricTargetPresetDesc
						,R.RegionID
						,R.Region
						,C.CompanyID
						,C.CompanyName
						,MTP.dEMethod
						,MTP.ValidationType
						,MTP.IsSync1D
						,MTP.Validation_1D
						,MTP.Validation_4D
						,MTP.IsUsed
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









