
/****** Object:  StoredProcedure [CPSpot].[CreateSpotProject]    Script Date: 5/26/2023 3:49:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSpot].[CreateSpotProject] @CPDeviceID INT
	,@SpotTargetID INT
	,@ProjectName NVARCHAR(30)
	,@CreateDate DATETIME
	,@TechnicalName NVARCHAR(30)
	,@Notes NVARCHAR(500)
	,@Media NVARCHAR(50)
	,@Colorant NVARCHAR(50)
	,@RipSettings NVARCHAR(50)
	,@ObserverType TINYINT
	,@Illuminant TINYINT
	,@UVMode TINYINT
	,@MeasurementCount TINYINT
	,@ReadMode NVARCHAR(30)
	,@MeasurementDeviceID SMALLINT
	,@Status INT
	,@StartDateTime DATETIME
	,@LicenseTypeID INT
	,@MasterProjectID INT
	,@ReTargetSpotProjectID INT
	,@SpotProjectID INT OUTPUT
	,@IsCustomPlugin BIT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN

		INSERT INTO CPSpot.SpotProject (
			ProjectID
			,CPDeviceID
			,SpotTargetID
			,ProjectName
			,CreateDate
			,CurrDLPID
			,IsUsed
			,ReTargetSpotProjectID
			)
		VALUES (
			@MasterProjectID
			,@CPDeviceID
			,@SpotTargetID
			,@ProjectName
			,@CreateDate
			,NULL
			,1
			,@ReTargetSpotProjectID
			)

		SET @SpotProjectID = @@IDENTITY

		INSERT INTO CPSpot.PrintCondition (
			SpotProjectID
			,TechnicianName
			,Notes
			,Media
			,Colorant
			,RipSettings
			,IsCustomPlugin
			)
		VALUES (
			@SpotProjectID
			,@TechnicalName
			,@Notes
			,@Media
			,@Colorant
			,@RipSettings
			,@IsCustomPlugin
			)

		INSERT INTO CPSpot.MeasurementSetting (
			SpotProjectID
			,ObserverType
			,Illuminant
			,UVMode
			,MeasurementCount
			,ReadMode
			,MeasurementDeviceID
			--,PluginType
			)
		VALUES (
			@SpotProjectID
			,@ObserverType
			,@Illuminant
			,@UVMode
			,@MeasurementCount
			,@ReadMode
			,@MeasurementDeviceID
			--,@PluginType
			)

		---Add ChartSetting Entry row for spot project

		---Get NewSPotTargetID based on spot project is Re Targted custom made or normal Spot Target project
		DECLARE @NewSpotTargetID INT;
		IF ISNULL(@ReTargetSpotProjectID,0) = 0	---If this Spot project is not custom Re target spot project
			BEGIN
				SET @NewSpotTargetID = @SpotTargetID
			END
			ELSE							---If this Spot project is custom Re target spot project
			BEGIN
				SET @NewSpotTargetID = (SELECT SpotTargetID FROM [CPSpot].[SpotProject] WHERE SpotProjectID = @ReTargetSpotProjectID)
			END

		
			INSERT INTO [CPSpot].[SpotProjectChartSetting] (
				SpotProjectID
				,ChartName
				,NumOfRows
				,NumOfCols
				,ChartSizeX
				,ChartSizeY
				,PatchSizeX
				,PatchSizeY
				,FirstSample
				,ReadDirection
				,ReadMode
				,Pages
				,RowsPerPage
				,ChartBorder
				,Vgap
				,Hgap
				,GapLeval
				,PreviousL
				,ChartRes
				,TotalRowsInChart
				)
			SELECT @SpotProjectID
				,ChartName
				,NumOfRows
				,NumOfCols
				,ChartSizeX
				,ChartSizeY
				,PatchSizeX
				,PatchSizeY
				,FirstSample
				,ReadDirection
				,ReadMode
				,Pages
				,RowsPerPage
				,ChartBorder
				,Vgap
				,Hgap
				,GapLeval
				,PreviousL
				,ChartRes
				,NumOfRows
			FROM [CPSpot].[SpotTargetChartSetting]
			WHERE SpotTargetID = @NewSpotTargetID
		

		--okay to send close status same as open status as there will be no optimize run to close at this stage...   
		IF @LicenseTypeID = 10
			EXEC CPSpot.CreateOptimizeRun @SpotProjectID
				,@Status
				,@StartDateTime

		-------Update LastActiveDate for Project as Today's date-------
		UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE() WHERE ProjectID = @MasterProjectID

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

		SELECT @ERROR_NUMBER = ERROR_NUMBER()
			,@ERROR_NUMBER = ERROR_NUMBER()
			,@ERROR_SEVERITY = ERROR_SEVERITY()
			,@ERROR_STATE = ERROR_STATE()
			,@ERROR_PROCEDURE = ERROR_PROCEDURE()
			,@ERROR_LINE = ERROR_LINE()
			,@ERROR_MESSAGE = ERROR_MESSAGE()

		EXEC InsertSQLErrorLog @ERROR_NUMBER
			,@ERROR_SEVERITY
			,@ERROR_STATE
			,@ERROR_PROCEDURE
			,@ERROR_LINE
			,@ERROR_MESSAGE
	END CATCH
END



