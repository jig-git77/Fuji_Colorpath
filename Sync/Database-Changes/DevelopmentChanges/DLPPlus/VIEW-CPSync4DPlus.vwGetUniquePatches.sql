
GO

/****** Object:  View [CPSync4DPlus].[vwGetUniquePatches]    Script Date: 1/12/2021 4:25:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [CPSync4DPlus].[vwGetUniquePatches]
WITH SCHEMABINDING 
AS
SELECT     RC.ReferenceChartID, RC.ChartTypeID, RC.SampleID, RC.SampleName, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K, RC.PatchTypeID, RC.IsOuterGamut, 
                      RC.HTMLColor, PTL.ProjectID, PTL.[TargetL*] AS TargetL, PTL.[Targeta*] AS Targeta, PTL.[Targetb*] AS Targetb, M.MeasurementID, M.MeasurementName, 
                      M.OptimizeRunID, M.MeasurementDate, M.ReferenceChartTypeID, M.IsUsed, MP.MeasurementPatchID, MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a, 
                      MP.[CIELAB_b*] AS CIELAB_b, MP.Density_V, MP.Density_C, MP.Density_M, MP.Density_Y, MP.ΔE, MP.ΔE94, MP.ΔE2000
FROM         CP.ReferenceChart AS RC INNER JOIN
                      CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN
                      CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName LEFT OUTER JOIN
                      CPSync4D.MeasuredColorChar AS MCC ON MCC.MeasurementPatchID = MP.MeasurementPatchID INNER JOIN
                      CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
                      CPSync4DPlus.ProjectTargetLAB AS PTL ON PTL.ProjectID = OPT.ProjectID AND PTL.PatchTypeID = RC.PatchTypeID




GO