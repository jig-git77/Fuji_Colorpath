
/****** Object:  StoredProcedure [CPSpot].[GetPatchDetailsByOptimizeRunID_Summary]    Script Date: 9/6/2022 11:31:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 EXEC [CPSpot].[GetPatchDetailsByOptimizeRunID_Summary]
 @OptimizeRunID =104
*/
ALTER PROCEDURE [CPSpot].[GetPatchDetailsByOptimizeRunID_Summary] @OptimizeRunID INT
AS
BEGIN
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

	PRINT @MeasurementID

	IF ISNULL(@MeasurementID, 0) = 0 -----For Run-0
	BEGIN
		SELECT @MeasurementID AS MeasurementID
			,ISNULL(MAX(SPLAB.ΔE),0) AS MaxE
			,ISNULL(MIN(SPLAB.ΔE),0) AS MinE
			,ISNULL(AVG(SPLAB.ΔE),0) AS AvgE
		FROM CPSpot.OptimizeRunCMYK AS OptCMYK
		INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = OptCMYK.OptimizeRunID
		INNER JOIN CPSpot.SpotProjectLAB AS SPLAB ON SPLAB.SpotProjectID = Opt.SpotProjectID
			AND SPLAB.SpotID = OptCMYK.SpotID AND SPLAB.SpotName = OptCMYK.SpotName
		WHERE Opt.OptimizeRunID = @OptimizeRunID
	END
	ELSE -----------For Run-1 or above
	BEGIN
		SELECT M.MeasurementID + 1000 AS MeasurementID			---Set MeasurementID as 1000 more than original number
			,M.MeasurementName
			,M.MeasurementDate
			,ISNULL(MAX(MP.ΔE_T_P),0) AS MaxETP
			,ISNULL(MIN(MP.ΔE_T_P),0) AS MinETP
			,ISNULL(AVG(MP.ΔE_T_P),0) AS AvgETP
			,ISNULL(MAX(MP.ΔE_M_T),0) AS MaxEMT
			,ISNULL(MIN(MP.ΔE_M_T),0) AS MinEMT
			,ISNULL(AVG(MP.ΔE_M_T),0) AS AvgEMT
			,ISNULL(MAX(MP.ΔE_M_P),0) AS MaxEMP
			,ISNULL(MIN(MP.ΔE_M_P),0) AS MinEMP
			,ISNULL(AVG(MP.ΔE_M_P),0) AS AvgEMP
		FROM CPSpot.MeasurementPatch AS MP
		INNER JOIN CPSpot.Measurement AS M ON M.MeasurementID = MP.MeasurementID
		INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = M.OptimizeRunID
		WHERE Opt.OptimizeRunID = @OptimizeRunID
		GROUP BY M.MeasurementID
			,M.MeasurementName
			,M.MeasurementDate
	END
END


