USE [ColorPathStage]
GO
/****** Object:  StoredProcedure [CP].[DeleteProject]    Script Date: 1/2/2020 10:49:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[DeleteProject]	 
	@ProjectID int 	
AS  
BEGIN	
	
	BEGIN TRY
		BEGIN TRAN
		--DELETE FROM CPSync4D.SourceProfile Where ProjectID = @ProjectID
		DELETE FROM CPSync4D.PrintCondition Where ProjectID = @ProjectID
		DELETE FROM CPSync4D.OptimizeRun Where ProjectID = @ProjectID
		DELETE FROM [CPSync4D].Project Where ProjectID = @ProjectID		
		
		--If user is using third party app then we delete from Cp.MeasurementPluginStatus table record
		EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @ProjectID, @IsActive = NULL, @IsAllowDelete = 1, @vProfileType = NULL, @vOptimizationRunID = NULL

		
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



