
ALTER PROCEDURE [CPSync4D].[CreateOptimizeRun]    
 @ProjectId INT,    
 @Status INT, 
 @StartDateTime DATETIME
AS    
 BEGIN TRY    
 
   --IF alreday Run "Open" so no need to create new "Open" Run.
	IF NOT EXISTS (SELECT * FROM CPSync4D.OptimizeRun WHERE [Status] = 1 AND EndDateTime IS NULL AND ProjectID = @ProjectId)
	BEGIN
			DECLARE @MasterRunID INT             
			SELECT TOP 1 @MasterRunID=OptimizeRunID FROM CPSync4D.OptimizeRun WHERE ProjectID = @ProjectID ORDER BY OptimizeRunID DESC    
  
			DECLARE @RunCount INT            
			SELECT @RunCount = COUNT(*) FROM CPSync4D.OptimizeRun WHERE ProjectID = @ProjectID            

			DECLARE @vOptimizeRunID INT,@vProfileType NVARCHAR(50)
   
			INSERT INTO CPSync4D.OptimizeRun    
			 (RunName,    
			 ProjectID,    
			 Status,    
			 StartDateTime,    
			 EndDateTime,    
			 DeviceLinkProfile,    
			 MasterRunID,    
			 IsUsed)    
			VALUES(    
			 'Run-' + CONVERT(VARCHAR(3),@RunCount),          
			 @ProjectID,            
			 @Status,        
			 @StartDateTime,            
			 NULL,    
			 NULL,    
			 @MasterRunID,    
			 1)  
			
			SET @vOptimizeRunID=SCOPE_IDENTITY();

			DECLARE @LicenseTypeID TINYINT
			SELECT @LicenseTypeID=LicenseTypeID FROM [CPSync4D].[Project] WHERE ProjectID = @ProjectId
			IF(@LicenseTypeID = 11)---IF DLPPLUS then we stroe previous run details
			BEGIN
				-----Call stored procedure to stored Previous Run data for comparison----
		     	IF @MasterRunID > 0 
				BEGIN
					EXEC [CPSync4DPlus].[StorePreviousRunDataByOptimizeRunID] @MasterRunID
				END
			END
			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE() WHERE ProjectID = @ProjectId

			--------If user is using third party app then update its run status---
			IF(@Status = 1)
			BEGIN
					IF(@RunCount = 0)
					BEGIN
						SET @vProfileType='Profiler'
					END
					ELSE IF(@RunCount >= 1)
					BEGIN
						SET @vProfileType='Optimization'
					END
					
					EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @ProjectID,@IsActive=1,@IsAllowDelete=0,@vProfileType=@vProfileType,@vOptimizationRunID=@vOptimizeRunID
			END
	END

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



GO


