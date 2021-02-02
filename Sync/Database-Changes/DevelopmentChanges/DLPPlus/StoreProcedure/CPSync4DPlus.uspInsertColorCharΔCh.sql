
GO

/****** Object:  StoredProcedure [CPSync4DPlus].[uspInsertColorCharΔCh]    Script Date: 1/12/2021 4:30:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CPSync4DPlus].[uspInsertColorCharΔCh]
@MeasurementID		int,
@UserID INT
AS
BEGIN
	
	BEGIN TRY	

	DECLARE @Paper_a  Decimal(6,2),@Paper_b As Decimal(6,2) 

	SELECT @Paper_a = ISNULL(AVG([CIELAB_a*]),0) , @Paper_b = ISNULL(AVG([CIELAB_b*]),0) from
		CPSync4D.Measurement AS M INNER JOIN 
	CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID =8  AND M.MeasurementID = @MeasurementID

	--Insert Gray --ΔCh values
	INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
    SELECT DISTINCT MP.MeasurementPatchID , 4 --ΔCh
	   ,CP.fn_CalcΔCh([CIELAB_a*],[CIELAB_b*],RC.CMYK_C,@Paper_a,@Paper_b) from
	CPSync4D.Measurement AS M INNER JOIN 
	CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID
	UNION ALL
	SELECT DISTINCT MP.MeasurementPatchID , 18 --Weighted ΔCh
		,(CP.fn_CalcΔCh([CIELAB_a*],[CIELAB_b*],RC.CMYK_C,@Paper_a,@Paper_b) * (1 - (SELECT CASE WHEN 0 > ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) END ))) FROM
	CPSync4D.Measurement AS M INNER JOIN 
	CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID
							
	--Insert Gray a* values
	INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
    SELECT DISTINCT MP.MeasurementPatchID , 11 --Gray a* (Target) G7 
	   ,ROUND((@Paper_a * (100 - RC.CMYK_C)/100),2) As [Gray a*] from
	CPSync4D.Measurement AS M INNER JOIN 
	CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID
	
	--Insert Gray b* values
	INSERT INTO CPSync4D.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
    SELECT DISTINCT MP.MeasurementPatchID , 12 --Gray b* (Target) G7
	   ,ROUND((@Paper_b * (100 - RC.CMYK_C)/100),2) As [Gray b*] from
	CPSync4D.Measurement AS M INNER JOIN 
	CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID


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
		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in CPSync4DPlus.[uspInsertColorCharΔCh] measurement',@ERROR_MESSAGE,'DLPPlus'
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END




GO


