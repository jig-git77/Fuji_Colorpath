
/****** Object:  StoredProcedure [CPSpot].[GetPatchDetailsByOptimizeRunID]    Script Date: 9/6/2022 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [CPSpot].[GetPatchDetailsByOptimizeRunID]
	@OptimizeRunID INT
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
	IF ISNULL(@MeasurementID, 0) = 0			-----For Run-0
	BEGIN
		-------Dataset for getting measurement patch details-------------
		SELECT SPLAB.SpotID
			,SPLAB.SpotName
			,SPLAB.SpotProjectLABID
			,OptCMYK.CMYK_C
			,OptCMYK.CMYK_M
			,OptCMYK.CMYK_Y
			,OptCMYK.CMYK_K
			--,STLAB.HTMLColor
			,SPLAB.HTMLColor
			,SPLAB.[Target_L*] AS TargetL
			,SPLAB.[Target_a*] AS Targeta
			,SPLAB.[Target_b*] AS Targetb
			,SPLAB.[Predicted_L*] AS CIELAB_L
			,SPLAB.[Predicted_a*] AS CIELAB_a
			,SPLAB.[Predicted_b*] AS CIELAB_b
			,(SPLAB.[Target_L*] - SPLAB.[Predicted_L*]) AS ΔL
			,(SPLAB.[Target_a*] - SPLAB.[Predicted_a*]) AS Δa
			,(SPLAB.[Target_b*] - SPLAB.[Predicted_b*]) AS Δb
			,SPLAB.ΔE
			,@MeasurementID AS MeasurementID
			,SPLAB.Predicted_HTMLColor
			,SPLAB.Predicted_HTMLColor AS Measured_HTMLColor
			,ISNULL(SPLAB.IsSelected,0) AS IsSelected
		FROM CPSpot.OptimizeRunCMYK AS OptCMYK
		INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = OptCMYK.OptimizeRunID
		INNER JOIN CPSpot.SpotProjectLAB AS SPLAB ON SPLAB.SpotProjectID = Opt.SpotProjectID AND SPLAB.SpotID = OptCMYK.SpotID AND SPLAB.SpotName = OptCMYK.SpotName
		INNER JOIN CPSpot.SpotProject AS SP ON SP.SpotProjectID = SPLAB.SpotProjectID
		--INNER JOIN CPSpot.SpotTargetLAB AS STLAB ON STLAB.SpotTargetID = SP.SpotTargetID AND STLAB.SpotID = OptCMYK.SpotID AND STLAB.SpotName = OptCMYK.SpotName
		WHERE Opt.OptimizeRunID = @OptimizeRunID
		ORDER BY SPLAB.SpotProjectLABID


	END
	ELSE								-----------For Run-1 or above
	BEGIN
		SELECT SPLAB.SpotID
			,SPLAB.SpotName
			,SPLAB.SpotProjectLABID
			,OptCMYK.CMYK_C
			,OptCMYK.CMYK_M
			,OptCMYK.CMYK_Y
			,OptCMYK.CMYK_K
			--,STLAB.HTMLColor
			,SPLAB.HTMLColor
			,SPLAB.[Target_L*] AS TargetL
			,SPLAB.[Target_a*] AS Targeta
			,SPLAB.[Target_b*] AS Targetb
			,SPLAB.[Predicted_L*] AS CIELAB_L
			,SPLAB.[Predicted_a*] AS CIELAB_a
			,SPLAB.[Predicted_b*] AS CIELAB_b
			,(SPLAB.[Target_L*] - SPLAB.[Predicted_L*]) AS ΔL
			,(SPLAB.[Target_a*] - SPLAB.[Predicted_a*]) AS Δa
			,(SPLAB.[Target_b*] - SPLAB.[Predicted_b*]) AS Δb
			,SPLAB.ΔE
			,MP.[Measured_L*] AS Measured_L
			,MP.[Measured_a*] AS Measured_a
			,MP.[Measured_b*] AS Measured_b
			,MP.ΔE_T_P
			,MP.ΔE_M_T
			,MP.ΔE_M_P
			,@MeasurementID AS MeasurementID
			,SPLAB.Predicted_HTMLColor
			,MP.Measured_HTMLColor
			,ISNULL(SPLAB.IsSelected,0) AS IsSelected
		FROM CPSpot.MeasurementPatch AS MP
			INNER JOIN CPSpot.Measurement AS M ON M.MeasurementID = MP.MeasurementID
			INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = M.OptimizeRunID
			INNER JOIN CPSpot.SpotProject AS SP ON SP.SpotProjectID = Opt.SpotProjectID
			INNER JOIN CPSpot.SpotProjectLAB AS SPLAB ON SPLAB.SpotProjectID = SP.SpotProjectID AND SPLAB.SpotID = MP.SpotID AND SPLAB.SpotName = MP.SpotName
			--INNER JOIN CPSpot.SpotTargetLAB AS STLAB ON STLAB.SpotTargetID = SP.SpotTargetID AND STLAB.SpotID = SPLAB.SpotID AND STLAB.SpotName = SPLAB.SpotName
			LEFT JOIN CPSpot.OptimizeRunCMYK AS OptCMYK ON OptCMYK.OptimizeRunID = Opt.OptimizeRunID AND OptCMYK.SpotID = MP.SpotID AND OptCMYK.SpotName = MP.SpotName

		WHERE Opt.OptimizeRunID = @OptimizeRunID
		ORDER BY MP.MeasurementPatchID

		
	END
END
