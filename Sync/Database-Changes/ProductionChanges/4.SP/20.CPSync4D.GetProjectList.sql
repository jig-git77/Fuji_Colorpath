
/****** Object:  StoredProcedure [CPSync4D].[GetProjectList]    Script Date: 8/18/2021 10:34:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CPSync4D].[GetProjectList] 
 @DeviceID INT
,@LicenseTypeID INT        
AS        
    
  BEGIN TRY	 
   
	  SELECT         
	   PRJ.ProjectID,    
	   PRJ.ProjectName,    
	   PRJ.CPDeviceID,    
	   PRJ.LicenseTypeID,
	   PRJ.IsUsed,    
	   PST.SourceProfileID,  
	   PRJ.EnabledValidation,  
	   SP.ProfileName,  
	   PRJ.PrintStandardTypeID,
	   PST.PrintStandard,
	   PCON.Media,    
	   PCON.Colorant,    
	   PCON.RipSettings,    
	   PRJ.MetricTargetPresetID,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
			(SELECT TOP 1 
			CASE 
					WHEN  ISNULL(PRJ.LastActiveDate,'1900-01-01') > ISNULL(SR.EndDateTime,'1900-01-01') THEN PRJ.LastActiveDate 
					ELSE SR.EndDateTime 
			END
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)
	   
	   ELSE	   
			(SELECT TOP 1 
			CASE 
					WHEN  ISNULL(PRJ.LastActiveDate,'1900-01-01') > ISNULL(SR.EndDateTime,'1900-01-01') THEN PRJ.LastActiveDate 
					ELSE SR.EndDateTime 
			END
			FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC) END) 
	   AS CreateDateTime,
	  (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
			(SELECT TOP 1 SR.EndDateTime 
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)
	   
	   ELSE	   
			(SELECT TOP 1 SR.EndDateTime 
			FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC) END) 
		AS EndDateTime,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		ISNULL((SELECT TOP 1 SR.Status FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC),1)
	   ELSE
		ISNULL((SELECT TOP 1 SR.Status FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC),1)
	   END)AS ProjStatus,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		(SELECT TOP 1 
		CASE 
			WHEN DeviceLinkProfile IS NULL THEN 0
			ELSE 1
			END			
		FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC)
	   ELSE
		(SELECT TOP 1 
		CASE 
			WHEN DeviceLinkProfile IS NULL THEN 0
			ELSE 1
			END			
		FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC)
	   END) AS HasDLP,
		(CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		ISNULL((SELECT COUNT(SR.OptimizeRunID) FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID),0) 
	   ELSE
		ISNULL((SELECT COUNT(SR.OptimizeRunID) FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID),0) 
	   END)AS TotalRuns,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		(SELECT (R-1) FROM (SELECT OptimizeRunId,RANK() OVER (ORDER BY OptimizeRunID) R FROM CPSync.OptimizeRun OPT WHERE ProjectId = PRJ.ProjectID) rankTbl WHERE rankTbl.OptimizeRunId = PRJ.CurrDlpID)
	   ELSE
		(SELECT (R-1) FROM (SELECT OptimizeRunId,RANK() OVER (ORDER BY OptimizeRunID) R FROM CPSync4D.OptimizeRun OPT WHERE ProjectId = PRJ.ProjectID) rankTbl WHERE rankTbl.OptimizeRunId = PRJ.CurrDlpID)
	   END) AS CurrentDlpNo,
	   CPSyncV.GetValidationStatus(PRJ.ProjectID, PRJ.EnabledValidation, PRJ.LicenseTypeID) AS ValidationStatus
	   ,(
		CASE 
			WHEN EXISTS (
					SELECT 1
					FROM CP.DeviceLicenseType DLT
					INNER JOIN [CP].[LicenseType] LT ON LT.LicenseTypeID = DLT.LicenseTypeID
					WHERE DeviceID = @DeviceID
						AND LT.LicenseTypeID = 10
						AND NumOfProject != 0
					)
				THEN CAST(1 AS BIT)
			ELSE CASE 
					WHEN EXISTS (
							SELECT 1
							FROM [CPSpot].[SpotProject]
							WHERE ProjectID = PRJ.ProjectID
							)
						THEN CAST(1 AS BIT)
					ELSE CAST(0 AS BIT)
					END
			END
		) AS HasSpotProject
	  FROM    
	   [CPSync4D].[Project] PRJ INNER JOIN CPSync4D.PrintCondition PCON     
		ON PCON.ProjectID = PRJ.ProjectID    
		INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
	   INNER JOIN CPSync4D.SourceProfile SP     
		ON SP.SourceProfileID = PST.SourceProfileID    		
		  WHERE    
	   PRJ.CPDeviceID = @DeviceID AND (ISNULL(@LicenseTypeID,0) = 0 OR PRJ.LicenseTypeID = @LicenseTypeID) AND
	   PRJ.IsUsed = 1    
      ORDER BY CreateDateTime DESC
  END TRY
	BEGIN CATCH
      DECLARE @ERROR_NUMBER INT   
	  DECLARE @ERROR_SEVERITY INT  
	  DECLARE @ERROR_STATE INT   
	  DECLARE @ERROR_PROCEDURE NVARCHAR(252)   
	  DECLARE @ERROR_LINE INT   
	  DECLARE @ERROR_MESSAGE NVARCHAR(4000)   
	  SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()   
	  EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE   
	END CATCH
