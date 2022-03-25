CREATE PROCEDURE [CPSync4DPlus].[StorePreviousRunDataByOptimizeRunID]      
 @OptimizeRunID  int    
AS      
BEGIN   
		---Get ΔE Type based on selected MetricTargetPresetID-------
		DECLARE @From_ΔE NVARCHAR(20);

		SELECT @From_ΔE = dEMethod FROM  CPSyncV.MetricTargetPreset AS MTP
				INNER JOIN CPSync4D.Project AS P ON P.MetricTargetPresetID = MTP.MetricTargetPresetID
				INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
			WHERE OPT.OptimizeRunID = @OptimizeRunID
		
		SET @From_ΔE = substring(@From_ΔE,2,len(@From_ΔE)); ----Remove Δ sign from string and pass just E / E2000---

		---First delete older records under same OptimizeRunID------
		DELETE FROM CPSync4DPlus.OptimizeRun_PreviousRunData WHERE OptimizeRunID = @OptimizeRunID;
   
		---Add new set of records for previous run--------
		INSERT INTO CPSync4DPlus.OptimizeRun_PreviousRunData
           ([OptimizeRunID]
           ,[PatchTypeID]
           ,[AvgDensity]
           ,[MinDensity]
           ,[MaxDensity]
           ,[STDdevDensity]
           ,[AvgΔE]
           ,[MinΔE]
           ,[MaxΔE]
           ,[STDdevΔE]
           ,[IsUsed])

			SELECT @OptimizeRunID, PatchTypeID,
				CONVERT(decimal(5,2),ROUND(AVG(Density),2)) AS AvgDensity,
				CONVERT(decimal(5,2),ROUND(MIN(Density),2)) AS MinDensity,
				CONVERT(decimal(5,2),ROUND(MAX(Density),2)) AS MaxDensity,
				CONVERT(decimal(5,2),ROUND(STDEV(Density),2)) AS STDdevDensity,
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))
				END AS [AvgΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(MIN(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(MIN(ΔE),2))
				END AS [MinΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(MAX(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(MAX(ΔE),2))
				END AS [MaxΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(STDEV(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(STDEV(ΔE),2))
				END AS [STDdevΔE],
				1 AS IsUsed

			FROM
			(SELECT  PatchTypeID, 
				CASE   
					WHEN PatchTypeID = 1 THEN Density_C
					WHEN PatchTypeID = 2 THEN Density_M
					WHEN PatchTypeID = 3 THEN Density_Y
					WHEN PatchTypeID = 4 THEN Density_V
					ELSE  
						0 	 
				END As Density,
				ΔE,
				ΔE2000
					
				FROM    CPSync4DPlus.vwGetUniquePatches AS VW
				WHERE   OptimizeRunID = @OptimizeRunID AND PatchTypeID >= 1 AND PatchTypeID <= 9 

				) AS V
			GROUP BY V.PatchTypeID
		
END












GO


