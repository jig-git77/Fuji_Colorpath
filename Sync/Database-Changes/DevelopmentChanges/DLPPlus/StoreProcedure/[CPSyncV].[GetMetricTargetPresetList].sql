ALTER PROCEDURE [CPSyncV].[GetMetricTargetPresetList]
    -- Add the parameters for the stored procedure here
    @CurrentUserID INT
   ,@ProjectTypeID INT 
   ,@PrintingTypeID TINYINT
   ,@LicenseTypeID INT ---Addd parameter for DLPPLus Validation
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    IF(@ProjectTypeID = 1) -----Sync1D G7 Projects
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
                Validation_1D = 1 AND ValidationType = 1 AND IsUsed = 1 AND
                (ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
                 AND ISNULL(PrintingTypeID,0) =    ----If Flexo print type, then pass only flexo enabled presets
                    (CASE  
                        WHEN @PrintingTypeID = 1 THEN 1
                        ELSE 0
                     END
                    )
                
    END
    ELSE IF(@ProjectTypeID = 2) -----Sync1D ISO Projects
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
                Validation_1D = 1 AND ValidationType = 2 AND IsUsed = 1 AND
                (ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
                 AND ISNULL(PrintingTypeID,0) =    ----If Flexo print type, then pass only flexo enabled presets
                    (CASE  
                        WHEN @PrintingTypeID = 1 THEN 1
                        ELSE 0
                     END
                    )

    END
    ELSE ----Sync4D Projects
    BEGIN
        IF (@LicenseTypeID = 11) 
        BEGIN
             --SELECT  
                --MTP.MetricTargetPresetID
                --,MTP.MetricTargetPresetName    
                --,MTP.MetricTargetPresetDesc            
             -- FROM   
                --    CPSyncV.MetricTargetPreset MTP 
                --    LEFT JOIN CP.Region R  
                --        ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
                --    LEFT JOIN CP.Company C  
                --        ON C.CompanyID = MTP.CompanyID  
             -- WHERE  
                --MTP.Validation_4D = 1 AND ValidationType = 1 AND IsUsed = 1 AND
                --(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
                -- AND ISNULL(PrintingTypeID,0) =    ----If Flexo print type, then pass only flexo enabled presets
                --    (CASE  
                --        WHEN @PrintingTypeID = 1 THEN 1
                --        ELSE 0
                --     END
                --    )
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
                Validation_4D = 1 AND IsUsed = 1 AND ValidationType IN (0,1,2) AND  ---Add Default mode as 0 also
                (ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
                 AND ISNULL(PrintingTypeID,0) =    ----If Flexo print type, then pass only flexo enabled presets
                    (CASE  
                        WHEN @PrintingTypeID = 1 THEN 1
                        ELSE 0
                     END