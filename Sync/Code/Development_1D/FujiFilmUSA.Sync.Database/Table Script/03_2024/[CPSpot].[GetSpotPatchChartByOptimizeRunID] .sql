/****** Object:  StoredProcedure [CPSpot].[GetSpotPatchChartByOptimizeRunID]    Script Date: 3/19/2024 9:06:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CPSpot].[GetSpotPatchChartByOptimizeRunID] 
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
		SELECT SPLAB.SpotID
			,SPLAB.SpotName
			,OptCMYK.CMYK_C
			,OptCMYK.CMYK_M
			,OptCMYK.CMYK_Y
			,OptCMYK.CMYK_K
			,SPLAB.[Predicted_L*] AS CIELAB_L
			,SPLAB.[Predicted_a*] AS CIELAB_a
			,SPLAB.[Predicted_b*] AS CIELAB_b
		FROM CPSpot.OptimizeRunCMYK AS OptCMYK
		INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = OptCMYK.OptimizeRunID
		INNER JOIN CPSpot.SpotProjectLAB AS SPLAB ON SPLAB.SpotProjectID = Opt.SpotProjectID AND SPLAB.SpotName = OptCMYK.SpotName
		INNER JOIN CPSpot.SpotProject AS SP ON SP.SpotProjectID = SPLAB.SpotProjectID
		WHERE Opt.OptimizeRunID = @OptimizeRunID
		ORDER BY SPLAB.SpotProjectLABID
	END

	ELSE								-----------For Run-1 or above
	BEGIN
		SELECT SPLAB.SpotID
			,SPLAB.SpotName
			,OptCMYK.CMYK_C
			,OptCMYK.CMYK_M
			,OptCMYK.CMYK_Y
			,OptCMYK.CMYK_K
			,SPLAB.HTMLColor
			,SPLAB.[Predicted_L*] AS CIELAB_L
			,SPLAB.[Predicted_a*] AS CIELAB_a
			,SPLAB.[Predicted_b*] AS CIELAB_b
		FROM CPSpot.MeasurementPatch AS MP
		INNER JOIN CPSpot.Measurement AS M ON M.MeasurementID = MP.MeasurementID
		INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.OptimizeRunID = M.OptimizeRunID
		INNER JOIN CPSpot.SpotProject AS SP ON SP.SpotProjectID = Opt.SpotProjectID
		INNER JOIN CPSpot.SpotProjectLAB AS SPLAB ON SPLAB.SpotProjectID = SP.SpotProjectID
			AND SPLAB.SpotID = MP.SpotID
			AND SPLAB.SpotName = MP.SpotName
		LEFT JOIN CPSpot.OptimizeRunCMYK AS OptCMYK ON OptCMYK.OptimizeRunID = Opt.OptimizeRunID
			AND OptCMYK.SpotID = SPLAB.SpotID
			AND OptCMYK.SpotName = SPLAB.SpotName
		WHERE Opt.OptimizeRunID = @OptimizeRunID
		ORDER BY MP.MeasurementPatchID
	END

	-- 03/19/2024 to support mnulticolor into BCO chart creation 
	SELECT (SELECT MultiColorChannelName  FROM CP.MultiColorChannel WHERE MultiColorChannelID = Channel_5 ) AS Channel_5
		  ,(SELECT MultiColorChannelName  FROM CP.MultiColorChannel WHERE MultiColorChannelID = Channel_6 ) AS Channel_6
		  ,(SELECT MultiColorChannelName  FROM CP.MultiColorChannel WHERE MultiColorChannelID = Channel_7 ) AS Channel_7
		  ,(SELECT MultiColorChannelName  FROM CP.MultiColorChannel WHERE MultiColorChannelID = Channel_8 ) AS Channel_8
		  ,(SELECT MultiColorChannelName  FROM CP.MultiColorChannel WHERE MultiColorChannelID = Channel_9 ) AS Channel_9
		  ,[Channel_5_CMYK]
		  ,[Channel_6_CMYK]
		  ,[Channel_7_CMYK]
		  ,[Channel_8_CMYK]
		  ,[Channel_9_CMYK]
	FROM CP.Device AS D
	INNER JOIN CPSpot.SpotProject AS SP ON SP.CPDeviceID = D.DeviceID
	INNER JOIN CPSpot.OptimizeRun AS Opt ON Opt.SpotProjectID = SP.SpotProjectID
	WHERE Opt.OptimizeRunID = @OptimizeRunID
END