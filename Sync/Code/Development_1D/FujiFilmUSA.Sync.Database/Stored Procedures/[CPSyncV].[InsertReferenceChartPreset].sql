CREATE PROCEDURE [CPSyncV].[InsertReferenceChartPreset]
	@ReferenceChartTypeID INT,
	@MetricTargetPresetIDXML XML,
	@IsSaved INT OUT
AS  
BEGIN 

	BEGIN TRY
		BEGIN TRAN
		
			DECLARE @tab AS TABLE(ReferenceChartTypeID INT,MetricTargetPresetID INT)
		
			INSERT INTO @tab(ReferenceChartTypeID,MetricTargetPresetID)
			SELECT  
					[v].value('@ReferenceChartTypeID', 'INT'),
					[v].value('@MetricTargetPresetID', 'INT')									
			FROM @MetricTargetPresetIDXML.nodes('/ReferenceChartPresetList/ReferenceChartPreset') p([v])			

			--SELECT * FROM @tab

			--DELETE from the destination table
			DELETE rc
			FROM [CPSyncV].[ReferenceChartPreset] rc
			WHERE rc.ReferenceChartTypeID = @ReferenceChartTypeID


			--INSERT INTO the destination table
			INSERT INTO [CPSyncV].[ReferenceChartPreset]
			(ReferenceChartTypeID,MetricTargetPresetID) 
			SELECT ReferenceChartTypeID,MetricTargetPresetID FROM @tab;
			
			SELECT @IsSaved=1
			
		COMMIT TRAN
		
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT @IsSaved=-1
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


