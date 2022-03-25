
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [CPSync4DPlus].[GetPreviousRunID](@MeasurementID INT)
RETURNS INT
AS
	
	BEGIN
		DECLARE @PreviousRunID INT;
		DECLARE @CurrentRunID INT;
		DECLARE @TotalRuns INT;

		SELECT  @TotalRuns = count(*)  FROM CPSync4D.OptimizeRun WHERE ProjectID = 
				(SELECT P.ProjectID FROM CPSync4D.Measurement AS M inner join CPSync4D.OptimizeRun AS OPT 
					ON OPT.OptimizeRunID = M.OptimizeRunID inner join CPSync4D.Project AS P
					ON P.ProjectID = OPT.ProjectID WHERE MeasurementID = @MeasurementID)

		SELECT @CurrentRunID = OptimizeRunID FROM CPSync4D.Measurement WHERE MeasurementID = @MeasurementID

		SELECT @PreviousRunID = CASE 

		WHEN @TotalRuns > 1 THEN 
				(SELECT TOP 1 OptimizeRunID FROM
					(SELECT  TOP 2 OptimizeRunID FROM CPSync4D.OptimizeRun WHERE ProjectID = 
						(SELECT P.ProjectID FROM CPSync4D.Measurement AS M inner join CPSync4D.OptimizeRun AS OPT 
							ON OPT.OptimizeRunID = M.OptimizeRunID inner join CPSync4D.Project AS P
							ON P.ProjectID = OPT.ProjectID WHERE MeasurementID = @MeasurementID)
				AND OptimizeRunID < @CurrentRunID ORDER BY OptimizeRunID DESC) X                   
				ORDER BY OptimizeRunID DESC
		)
		ELSE 0
		END

		RETURN ISNULL(@PreviousRunID,0)
END






GO


