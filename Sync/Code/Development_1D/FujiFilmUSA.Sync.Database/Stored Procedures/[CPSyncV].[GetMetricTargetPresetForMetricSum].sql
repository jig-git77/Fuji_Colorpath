
/****** Object:  StoredProcedure [CPSyncV].[GetMetricTargetPresetForMetricSum]    Script Date: 9/23/2022 1:22:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [CPSyncV].[GetMetricTargetPresetForMetricSum]   
	@MetricTargetPresetID INT,
	@CurrentUserID INT
AS  
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
	BEGIN TRY
		
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
						--,CASE COUNT(PatchTargetPresetID)
						--	WHEN 0 THEN 0
						--	ELSE 1
						--END AS IsPatchTolerance
						,MTP.IsUsed
						--,RCP.ReferenceChartTypeID
						--,RCT.InstrumentTypeID
					  FROM   
							CPSyncV.MetricTargetPreset MTP 
							--LEFT JOIN CPSyncV.PatchTargetPreset AS PTP
							--	ON PTP.MetricTargetPresetID = MTP.MetricTargetPresetID
							LEFT JOIN CP.Region R  
								ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(1,MTP.RegionID) = 1  
							LEFT JOIN CP.Company C  
								ON C.CompanyID = MTP.CompanyID  
							--LEFT JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCP.MetricTargetPresetID = MTP.MetricTargetPresetID
							--LEFT JOIN CP.ReferenceChartType AS RCT ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID
					  WHERE  
						MTP.MetricTargetPresetID = @MetricTargetPresetID
					--GROUP BY MTP.MetricTargetPresetID
					--	,MTP.MetricTargetPresetName
					--	,MTP.MetricTargetPresetDesc
					--	,R.RegionID
					--	,R.Region
					--	,C.CompanyID
					--	,C.CompanyName
					--	,MTP.dEMethod
					--	,MTP.ValidationType
					--	,MTP.IsUsed
					--	,RCP.ReferenceChartTypeID
					--	,RCT.InstrumentTypeID
				------Select all MetricSum Tolerances for this MetricTargetPreset--------
				SELECT 
						MT.MetricTargetID
						,MS.MetricSumID
						,MS.MetricSumName
						,MT.TargetValue
						,ISNULL(MT.RankID,0) AS RankID
				FROM
						CPSyncV.MetricSum AS MS				
					INNER JOIN CPSyncV.MetricTarget AS MT ON MS.MetricSumID = MT.MetricSumID AND MT.MetricTargetPresetID = @MetricTargetPresetID
				WHERE  MetricTargetPresetID = @MetricTargetPresetID
				--		 MS.MetricSumID IN (1,2,3,4,5,6,7,25)

							
		
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








