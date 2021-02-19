
ALTER PROCEDURE [CPSync].[GetMetricTargetPresetList]
	-- Add the parameters for the stored procedure here
	@CurrentUserID INT
   ,@ProjectTypeID INT
   ,@PrintingTypeID TINYINT
    ,@LicenseTypeID INT --Extra parameter no need to use we just use for DLPPLUs validation SP "[CPSyncV].[GetMetricTargetPresetList]"
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (@ProjectTypeID = 1) ----SYNC1D G7
	BEGIN
		 SELECT  
			MTP.MetricTargetPresetID
			,MTP.MetricTargetPresetName	
			,MTP.MetricTargetPresetDesc
		  FROM   
				CPSyncV.MetricTargetPreset MTP 
				LEFT JOIN CP.Region R  
					ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
				LEFT JOIN CP.Company C  
					ON C.CompanyID = MTP.CompanyID  
		  WHERE  
			 IsSync1D = 1 AND ValidationType = 1 AND IsUsed = 1 AND
			(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
			 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
				(CASE  
					WHEN @PrintingTypeID = 1 THEN 1
					ELSE 0
				 END
				)
		 ORDER BY MTP.MetricTargetPresetID DESC
				
	END
	ELSE  ----@ProjectTypeID = 2 FOR SYNC1D ISO
	BEGIN
		SELECT  
			MTP.MetricTargetPresetID
			,MTP.MetricTargetPresetName	
			,MTP.MetricTargetPresetDesc					
		  FROM   
				CPSyncV.MetricTargetPreset MTP 
				LEFT JOIN CP.Region R  
					ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
				LEFT JOIN CP.Company C  
					ON C.CompanyID = MTP.CompanyID  
		  WHERE  
			 IsSync1D = 1 AND ValidationType = 2 AND IsUsed = 1 AND
			(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
			 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
				(CASE  
					WHEN @PrintingTypeID = 1 THEN 1
					ELSE 0
				 END
				)
	END			
END






GO


