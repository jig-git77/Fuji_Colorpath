GO
/****** Object:  StoredProcedure [CPSpot].[GetSpotProjectSummaryByDeltaE]    Script Date: 8/26/2022 3:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CPSpot].[GetSpotProjectSummaryByDeltaE] 
	@OptimizeRunID INT,
	@Dec_E_Value DECIMAL(6,2)
AS
BEGIN

	BEGIN TRY

		DECLARE @MeasurementID INT

		SET @MeasurementID = 0

		IF EXISTS (
			SELECT MeasurementID
			FROM CPSpot.Measurement
			WHERE OptimizeRunID = @OptimizeRunID
			)
		BEGIN
			SELECT @MeasurementID = ISNULL(MeasurementID, 0)
			FROM CPSpot.Measurement
			WHERE OptimizeRunID = @OptimizeRunID
		END	

		IF ISNULL(@MeasurementID, 0) = 0			-----For Run-0
	
			BEGIN

				SELECT	@MeasurementID AS  MeasurementID
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID) AS TotalColors
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND ( @Dec_E_Value = NULL OR [ΔE] <= @Dec_E_Value  )) AS ELTEQ_VALUE
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND ( @Dec_E_Value = NULL OR [ΔE] > @Dec_E_Value  )) AS EGRT_VALUE
				FROM	[CPSpot].[OptimizeRun] AS O
				INNER JOIN [CPSpot].[SpotProject] SP ON SP.SpotProjectID=O.SpotProjectID
				WHERE	O.OptimizeRunID = @OptimizeRunID 
				
			END
			ELSE								-----------For Run-1 or above
			BEGIN
				SELECT	@MeasurementID AS  MeasurementID
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID) AS TotalColors
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND ( @Dec_E_Value = NULL OR [ΔE_M_T] <= @Dec_E_Value  )) AS ELTEQ_VALUE
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND ( @Dec_E_Value = NULL OR [ΔE_M_T] > @Dec_E_Value  )) AS EGRT_VALUE
				FROM	[CPSpot].[Measurement] M
				INNER JOIN [CPSpot].[OptimizeRun] AS O  ON O.OptimizeRunID=M.OptimizeRunID
				WHERE	O.OptimizeRunID = @OptimizeRunID 
			END
		
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
