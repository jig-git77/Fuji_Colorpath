/****** Object:  StoredProcedure [CPSpot].[CreateSpotTarget]    Script Date: 10/20/2022 12:23:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-05-21
-- Description:	Save spot target chart setting
-- =============================================
ALTER PROCEDURE [CPSpot].[CreateSpotTarget] (
	@CompanyID INT
	,@SpotTargetName NVARCHAR(100)
	,@Description NVARCHAR(300)
	,@IsUsed BIT
	,@ChartName NVARCHAR(50)
	,@NumOfRows TINYINT
	,@NumOfCols TINYINT
	,@ChartSizeX DECIMAL(4, 1)
	,@ChartSizeY DECIMAL(4, 1)
	,@PatchSizeX DECIMAL(3, 1)
	,@PatchSizeY DECIMAL(3, 1)
	,@FirstSample NVARCHAR(15)
	,@ReadDirection NVARCHAR(15)
	,@ReadMode NVARCHAR(10)
	,@Pages TINYINT
	,@RowsPerPage TINYINT
	,@ChartBorder TINYINT
	,@Vgap TINYINT
	,@Hgap TINYINT
	,@GapLeval TINYINT
	,@PreviousL TINYINT
	,@ChartRes TINYINT
	,@IsCustom BIT
	,@SpotTargetLABXml XML
	)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		DECLARE @SpotTargetID INT

		-- Insert statements for procedure here
		INSERT INTO [CPSpot].[SpotTarget] (
			CompanyID
			,SpotTargetName
			,MeasurementMode
			,Description
			,OrderID
			,IsUsed
			,IsCustom
			)
		SELECT CASE @CompanyID
				WHEN - 1
					THEN NULL
				ELSE @CompanyID
				END
			,@SpotTargetName
			,0
			,@Description
			,(
				SELECT MAX(OrderID) + 1
				FROM CpSpot.SpotTarget
				)
			,@IsUsed
			,@IsCustom

		SELECT @SpotTargetID = SCOPE_IDENTITY()

		INSERT INTO CPSpot.SpotTargetChartSetting (
			SpotTargetID
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
			)
		SELECT @SpotTargetID
			,@ChartName
			,@NumOfRows
			,@NumOfCols
			,@ChartSizeX
			,@ChartSizeY
			,@PatchSizeX
			,@PatchSizeY
			,@FirstSample
			,@ReadDirection
			,@ReadMode
			,@Pages
			,@RowsPerPage
			,@ChartBorder
			,@Vgap
			,@Hgap
			,@GapLeval
			,@PreviousL
			,@ChartRes
			--INSERTING SPOT TARGET LAB DATA
			;

		WITH SpotTargetLABData
		AS (
			SELECT [SpotTargetXml].value('@SpotID', 'SMALLINT') AS SpotID
				,[SpotTargetXml].value('@SpotName', 'NVARCHAR(50)') AS SpotName
				,[SpotTargetXml].value('@Target_L', 'DECIMAL(6,2)') AS Target_L
				,[SpotTargetXml].value('@Target_a', 'DECIMAL(6,2)') AS Target_a
				,[SpotTargetXml].value('@Target_b', 'DECIMAL(6,2)') AS Target_b
			--,[SpotTargetXml].value('@HTMLColor', 'VARCHAR(7)') AS HTMLColor
			FROM @SpotTargetLABXml.nodes('/SpotTargetLABData/SpotTargetLAB') p([SpotTargetXml])
			)
		INSERT INTO CPSpot.SpotTargetLAB (
			SpotTargetID
			,SpotID
			,SpotName
			,[Target_L*]
			,[Target_a*]
			,[Target_b*]
			,[HTMLColor]
			)
		SELECT @SpotTargetID
			,SpotID
			,SpotName
			,Target_L
			,Target_a
			,Target_b
			--,HTMLColor
			,CP.fn_GetHTMLColor(Target_L, Target_a, Target_b)
		FROM SpotTargetLABData
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

		RAISERROR (
				@ERROR_MESSAGE
				,@ERROR_SEVERITY
				,@ERROR_STATE
				) /******Raise Error backto client *****/
	END CATCH
END



/****** Object:  StoredProcedure [CPSpot].[DeleteSpotTarget]    Script Date: 6/15/2018 6:07:39 PM ******/
SET ANSI_NULLS ON
