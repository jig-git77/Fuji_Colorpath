
GO

/****** Object:  View [CPSync4DPlus].[vwGetColorChars]    Script Date: 1/12/2021 4:25:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [CPSync4DPlus].[vwGetColorChars] WITH SCHEMABINDING
/** 

 Author: Dhruti Patel
 Note: View is Schema Bound 

**/
AS
SELECT MC.MeasurementPatchID, CC.ColorCharID
	   , SUM(CASE WHEN CC.ColorCharID = 11 THEN ISNULL(ColorCharValue,0) ELSE 0 END ) AS [Gray_a*] 
	   , SUM(CASE WHEN CC.ColorCharID = 12 THEN ISNULL(ColorCharValue,0) ELSE 0 END) AS [Gray_b*]
	   , SUM(CASE WHEN CC.ColorCharID = 4 THEN ISNULL(ColorCharValue,0)ELSE 0 END) AS [ΔF]
	   , SUM(CASE WHEN CC.ColorCharID = 18 THEN ISNULL(ColorCharValue,0)ELSE 0 END) AS [wΔF]
	   , SUM(CASE WHEN CC.ColorCharID = 9 THEN ISNULL(ColorCharValue,0) ELSE 0 END) As DotGain
	   , SUM(CASE WHEN CC.ColorCharID = 10 THEN ISNULL(ColorCharValue,0) ELSE 0 END) As [Gray_L*]
	   , SUM(CASE WHEN CC.ColorCharID = 13 THEN ISNULL(ColorCharValue,0) ELSE 0 END) As [G7NPDC]	
	   , SUM(CASE WHEN CC.ColorCharID = 16 THEN ISNULL(ColorCharValue,0) ELSE 0 END) As [ΔL]	
	   , SUM(CASE WHEN CC.ColorCharID = 17 THEN ISNULL(ColorCharValue,0) ELSE 0 END) As [wΔL]
	   , COUNT_BIG(*) AS COUNTBIG 
FROM CPSync4D.Measurement AS M
INNER JOIN CPSync4D.MeasurementPatch AS MP ON  M.MeasurementID = MP.MeasurementID
INNER JOIN CPSync4D.MeasuredColorChar As MC ON MC.MeasurementPatchID = MP.MeasurementPatchID
INNER JOIN CP.ColorChar AS CC ON MC.ColorCharID = CC.ColorCharID
Group By MC.MeasurementPatchID, CC.ColorCharID








