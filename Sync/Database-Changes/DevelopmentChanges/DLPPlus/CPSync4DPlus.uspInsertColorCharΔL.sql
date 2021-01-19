
GO

/****** Object:  StoredProcedure [CPSync4DPlus].[uspInsertColorCharΔL]    Script Date: 1/12/2021 4:30:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CPSync4DPlus].[uspInsertColorCharΔL]
@MeasurementID	int,
@UserID int
AS
BEGIN
	
	BEGIN TRY

DECLARE @XYZ_Y_Paper  Decimal(6,2),@XYZ_Y_100CMY As Decimal(6,2),@XYZ_Y_100K As Decimal(6,2)

 
SELECT @XYZ_Y_Paper=ISNULL(AVG([CIEXYZ_Y]),0)
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID
			INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
			WHERE  M.MeasurementID = @MeasurementID  and patchtypeID =8

SELECT @XYZ_Y_100CMY=ISNULL(AVG([CIEXYZ_Y]),0)
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID
			INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
			WHERE  M.MeasurementID =@MeasurementID  and patchtypeID =9

SELECT @XYZ_Y_100K=ISNULL(AVG([CIEXYZ_Y]),0)
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID
			INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
			WHERE  M.MeasurementID =@MeasurementID  and patchtypeID =4
						

				--Insert Gray NPD Target values ColorCharID =13
			INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
			SELECT DISTINCT MP.MeasurementPatchID , 13 --G7 NPD
				   ,CP.fn_CalcNPDTarg(RC.CMYK_C,@XYZ_Y_100CMY,@XYZ_Y_Paper,1) ---set to calc gray NPD
				FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID
			INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
			WHERE M.MeasurementID = @MeasurementID and RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) 

				--Insert Black NPD Target values ColorCharID =13
			INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
			SELECT DISTINCT MP.MeasurementPatchID , 13 --G7 NPD
				   ,CP.fn_CalcNPDTarg(RC.CMYK_K,@XYZ_Y_100K,@XYZ_Y_Paper,0) ---set to calc Black NPD
				FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID
			INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
			WHERE M.MeasurementID = @MeasurementID and RC.PatchTypeID in (4,15,25,26,27)
				
			--Insert CMY and K  Delta L* ColorCharID =16
			INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
			SELECT DISTINCT MP.MeasurementPatchID , 16 --DeltaL*
			,CP.fn_CalcDeltaL([CIEXYZ_Y],ColorCharValue,@XYZ_Y_Paper)
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName inner join
			CPSync4D.MeasuredColorChar AS MC 
			ON MP.MeasurementPatchID = MC.MeasurementPatchID
			WHERE M.MeasurementID = @MeasurementID and ColorCharID = 13
			UNION ALL
			SELECT DISTINCT MP.MeasurementPatchID , 17 --Weighted DeltaL*	
			,(CP.fn_CalcDeltaL([CIEXYZ_Y],ColorCharValue,@XYZ_Y_Paper) * (1 - (SELECT CASE WHEN 0 > ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) END )))
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName inner join
			CPSync4D.MeasuredColorChar AS MC 
			ON MP.MeasurementPatchID = MC.MeasurementPatchID
			WHERE M.MeasurementID = @MeasurementID and ColorCharID = 13  AND RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) ---For CMY / Gray ΔL
			UNION ALL
			SELECT DISTINCT MP.MeasurementPatchID , 17 --Weighted DeltaL*	
			,(CP.fn_CalcDeltaL([CIEXYZ_Y],ColorCharValue,@XYZ_Y_Paper) * (1 - (SELECT CASE WHEN 0 > ISNULL(((RC.CMYK_K - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((RC.CMYK_K - 50)/50 * 0.75),0) END )))
			FROM  CP.ReferenceChart AS RC INNER JOIN CPSync4D.Measurement AS M 
			ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN CPSync4D.MeasurementPatch AS MP 
			ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName inner join
			CPSync4D.MeasuredColorChar AS MC 
			ON MP.MeasurementPatchID = MC.MeasurementPatchID
			WHERE M.MeasurementID = @MeasurementID and ColorCharID = 13  AND RC.PatchTypeID IN (4,15,25,26,27) ---For Black ΔL


	
			--do we need thisΔΔΔΔΔΔ
			--Insert Gray L* values  ColorCharID = 10
			INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
			SELECT DISTINCT MP.MeasurementPatchID , 10 --Gray L* (Target) G7 
			   ,CP.fn_CalcTargetL(CP.fn_CalcNPDTarg(RC.CMYK_C,@XYZ_Y_100CMY,@XYZ_Y_Paper,1)) As [Gray L*] from
			CPSync4D.Measurement AS M INNER JOIN 
			CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
			CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
			WHERE  RC.PatchTypeID IN (8,9,10,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID

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
		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in CPSync4DPlus.uspInsertColorCharΔL measurement',@ERROR_MESSAGE,'DLPPlus'
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END




GO


