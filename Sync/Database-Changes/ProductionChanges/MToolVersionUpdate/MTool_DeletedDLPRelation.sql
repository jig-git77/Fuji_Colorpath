
GO
/****** Object:  StoredProcedure [CPSpot].[DeleteOptimizeRunDLP]    Script Date: 8/6/2020 6:22:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Softweb Deepak>
-- ALTER date: <10/26/2017>
-- Description:	<Delete Spot project OptimizeRun's DLP>
-- =============================================
ALTER PROCEDURE [CPSpot].[DeleteOptimizeRunDLP]
	@OptimizeRunID INT  
   ,@SpotProjectID INT
AS
	BEGIN TRY	
		
		--- Delete only DLP for this OptimizeRunID ------
		UPDATE CPSpot.OptimizeRun SET [Status] = 1, EndDateTime = NULL, NamedColorProfile = NULL where OptimizeRunID = @OptimizeRunID

		DECLARE @CurrDLPID INT;
		--- Update CurrDLPID of Project with latest OptimizeRunID which has DLP in that ------
		SELECT @CurrDLPID = max(OptimizeRunID) FROM CPSpot.OptimizeRun WHERE NamedColorProfile IS NOT NULL AND SpotProjectID = @SpotProjectID
		UPDATE CPSpot.SpotProject SET CurrDLPID = @CurrDLPID where SpotProjectID = @SpotProjectID

		--------Delete C,M,Y,K values for this optimize run--------------
		DELETE FROM CPSpot.OptimizeRunCMYK WHERE OptimizeRunID = @OptimizeRunID

		-------Update LastActiveDate for Project as Today's date-------
		UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE()
				 WHERE ProjectID = 
						(SELECT SP.ProjectID FROM CPSpot.SpotProject AS SP 
							WHERE SP.SpotProjectID = @SpotProjectID)

		--If user is using third party app then we update record from Cp.MeasurementPluginStatus table to IsActive = 1 -----
		EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @SpotProjectID, @IsActive = 1, @IsAllowDelete = 0, @vProfileType = NULL, @vOptimizationRunID = NULL

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







GO
/****** Object:  StoredProcedure [CPSync].[DeleteOptimizeRunDLP]    Script Date: 8/6/2020 6:22:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Dhruti>
-- ALTER date: <11/5/2012>
-- Description:	<Delete OptimizeRun's DLP>
-- =============================================
ALTER PROCEDURE [CPSync].[DeleteOptimizeRunDLP]
	@OptimizeRunID INT  
   ,@ProjectID INT
AS
	BEGIN TRY	
		
		--- Delete only DLP for this OptimizeRunID ------
		UPDATE CPSync.OptimizeRun SET [Status] = 1, EndDateTime = NULL, DeviceLinkProfile = NULL where OptimizeRunID = @OptimizeRunID

		DECLARE @CurrDLPID INT;
		--- Update CurrDLPID of Project with latest OptimizeRunID which has DLP in that ------
		SELECT @CurrDLPID = max(OptimizeRunID) FROM CPSync.OptimizeRun WHERE DeviceLinkProfile IS NOT NULL AND ProjectID = @ProjectID
		UPDATE CPSync4D.Project SET CurrDLPID = @CurrDLPID, LastActiveDate = GETDATE() where ProjectID = @ProjectID

		--If user is using third party app then we update record from Cp.MeasurementPluginStatus table to IsActive=1 -----
		EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @ProjectID, @IsActive = 1, @IsAllowDelete = 0, @vProfileType = NULL, @vOptimizationRunID = NULL


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







GO
/****** Object:  StoredProcedure [CPSync4D].[DeleteOptimizeRunDLP]    Script Date: 8/6/2020 6:22:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Dhruti>
-- ALTER date: <11/5/2012>
-- Description:	<Delete OptimizeRun's DLP>
-- =============================================
ALTER PROCEDURE [CPSync4D].[DeleteOptimizeRunDLP]
	@OptimizeRunID INT  
   ,@ProjectID INT
AS
	BEGIN TRY	
		
		--- Delete only DLP for this OptimizeRunID ------
		UPDATE CPSync4D.OptimizeRun SET [Status] = 1, EndDateTime = NULL, DeviceLinkProfile = NULL where OptimizeRunID = @OptimizeRunID

		DECLARE @CurrDLPID INT;
		--- Update CurrDLPID of Project with latest OptimizeRunID which has DLP in that ------
		SELECT @CurrDLPID = max(OptimizeRunID) FROM CPSync4D.OptimizeRun WHERE DeviceLinkProfile IS NOT NULL AND ProjectID = @ProjectID
		UPDATE CPSync4D.Project SET CurrDLPID = @CurrDLPID, LastActiveDate = GETDATE() where ProjectID = @ProjectID
		
		--If user is using third party app then we update record from Cp.MeasurementPluginStatus table to IsActive = 1 -----
		EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @ProjectID, @IsActive = 1, @IsAllowDelete = 0, @vProfileType = NULL, @vOptimizationRunID = NULL

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












