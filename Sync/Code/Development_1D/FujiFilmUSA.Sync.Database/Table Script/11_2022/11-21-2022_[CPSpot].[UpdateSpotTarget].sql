SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-05-24
-- Description:	update spot target chart setting
-- =============================================
ALTER PROCEDURE [CPSpot].[UpdateSpotTarget] (
	@SpotTargetID INT
	,@CompanyID INT
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
	,@SpotTargetLABXml XML
	)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		-- Insert statements for procedure here
		UPDATE [CPSpot].[SpotTarget]
		SET CompanyID =CASE @CompanyID
				WHEN - 1
					THEN NULL
				ELSE @CompanyID
				END
			,SpotTargetName = @SpotTargetName
			,Description = @Description
			,IsUsed = @IsUsed
		WHERE SpotTargetID = @SpotTargetID ;

		UPDATE CPSpot.SpotTargetChartSetting
		SET ChartName = @ChartName
			,NumOfRows = @NumOfRows
			,NumOfCols = @NumOfCols
			,ChartSizeX = @ChartSizeX
			,ChartSizeY = @ChartSizeY
			,PatchSizeX = @PatchSizeX
			,PatchSizeY = @PatchSizeY
			,FirstSample = @FirstSample
			,ReadDirection = @ReadDirection
			,ReadMode = @ReadMode
			,Pages = @Pages
			,RowsPerPage = @RowsPerPage
			,ChartBorder = @ChartBorder
			,Vgap = @Vgap
			,Hgap = @Hgap
			,GapLeval = @GapLeval
			,PreviousL = @PreviousL
			,ChartRes = @ChartRes
		WHERE SpotTargetID = @SpotTargetID ;
		
		IF  (@SpotTargetLABXml IS NOT NULL AND (SELECT COUNT(*) FROM @SpotTargetLABXml.nodes('/SpotTargetLABData/SpotTargetLAB')  p([SpotTargetXml]) ) > 0)
		BEGIN
			DELETE FROM		CPSpot.SpotTargetLAB
			WHERE			[SpotTargetID] = @SpotTargetID ;
			
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
		END;
		
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
