
GO
/****** Object:  StoredProcedure [CPSyncV].[uspInsertColorCharΔCh]    Script Date: 3/10/2021 10:49:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSyncV].[uspInsertColorCharΔCh]
@MeasurementID		int
AS
BEGIN
	
	BEGIN TRY	

	DECLARE @Paper_a  Decimal(6,2),@Paper_b As Decimal(6,2) 

	--SELECT @Paper_a = ISNULL(AVG([CIELAB_a*]),0) , @Paper_b = ISNULL(AVG([CIELAB_b*]),0) from
	--	CPSyncV.Measurement AS M INNER JOIN 
	--CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	--CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	--WHERE  RC.PatchTypeID =8  AND M.MeasurementID = @MeasurementID

	DECLARE @EnabledPaperScaling BIT;

	SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project AS P 
		INNER JOIN CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID
	WHERE M.MeasurementID = @MeasurementID

	------Getting LicenseType of Project-----------
	DECLARE @LicenseType int;
	SELECT @LicenseType = LicenseTypeID FROM CPSync4D.Project AS P INNER JOIN
			CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID
				WHERE M.MeasurementID = @MeasurementID

	IF @LicenseType = 1 -----1D Project Validation, then Pass 1D Target L, a, b values from CPSync.ProjectTargetLAB table
		BEGIN
				-------Get Paper values-------
				SELECT @Paper_a = ISNULL(AVG([Targeta]),0) , @Paper_b = ISNULL(AVG([Targetb]),0)
				FROM   CPSyncV.vwGetVUniquePatches_1D
				WHERE MeasurementID = @MeasurementID and patchtypeID = 8

				--Insert Gray --ΔCh / ΔF values
				INSERT INTO CPSyncV.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
				SELECT DISTINCT MeasurementPatchID , 4 --ΔF or --ΔCh
				   ,CP.fn_CalcΔCh([CIELAB_a],[CIELAB_b],CMYK_C,@Paper_a,@Paper_b)  
					FROM  CPSyncV.vwGetVUniquePatches_1D
				WHERE  PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND MeasurementID = @MeasurementID
				UNION ALL
				SELECT DISTINCT MeasurementPatchID , 18 --Weighted ΔCh
					,(CP.fn_CalcΔCh([CIELAB_a],[CIELAB_b],CMYK_C,@Paper_a,@Paper_b) * (1 - (SELECT CASE WHEN 0 > ISNULL(((CMYK_C - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((CMYK_C - 50)/50 * 0.75),0) END )))
					FROM  CPSyncV.vwGetVUniquePatches_1D
				WHERE  PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND MeasurementID = @MeasurementID
	END
	ELSE		-----For all other type of projects pass Target L, a, b values from CPSync4D.ReferenceLAB table
		BEGIN
			IF (@EnabledPaperScaling = 0) ---If no Paper scaling------
				BEGIN
				
						-------Get Paper values-------
						SELECT @Paper_a = ISNULL(AVG([Targeta]),0) , @Paper_b = ISNULL(AVG([Targetb]),0)
						FROM   CPSyncV.vwGetVUniquePatches
						WHERE MeasurementID = @MeasurementID and patchtypeID = 8

						--Insert Gray --ΔCh / ΔF values
						INSERT INTO CPSyncV.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
						SELECT DISTINCT MeasurementPatchID , 4 --ΔF or --ΔCh
							,CP.fn_CalcΔCh_Sync4D([CIELAB_a],[CIELAB_b],Targeta,Targetb)    
							FROM  CPSyncV.vwGetVUniquePatches 
						WHERE  PatchTypeID IN (28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND MeasurementID = @MeasurementID
						UNION ALL
						SELECT DISTINCT MeasurementPatchID , 18 --Weighted ΔCh
							,(CP.fn_CalcΔCh_Sync4D([CIELAB_a],[CIELAB_b],Targeta,Targetb) * (1 - (SELECT CASE WHEN 0 > ISNULL(((CMYK_C - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((CMYK_C - 50)/50 * 0.75),0) END )))
							FROM  CPSyncV.vwGetVUniquePatches 
						WHERE  PatchTypeID IN (28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND MeasurementID = @MeasurementID
		
				END
			ELSE		---If Paper scaling------
				BEGIN
					--print 0
					DECLARE @NewPaper_White_L DECIMAL(6,2);
					DECLARE @NewPaper_White_a DECIMAL(6,2); 
					DECLARE @NewPaper_White_b DECIMAL(6,2);
					DECLARE @NewPaper_Black_L DECIMAL(6,2);
					DECLARE @NewPaper_Black_a DECIMAL(6,2); 
					DECLARE @NewPaper_Black_b DECIMAL(6,2);
					DECLARE @CurrPaper_L DECIMAL(6,2);
					DECLARE @CurrPaper_a DECIMAL(6,2); 
					DECLARE @CurrPaper_b DECIMAL(6,2);
							
					SELECT @NewPaper_White_L = NewPaper_White_L, @NewPaper_White_a = NewPaper_White_a, @NewPaper_White_b = NewPaper_White_b, @NewPaper_Black_L = NewPaper_Black_L, @NewPaper_Black_a = NewPaper_Black_a, @NewPaper_Black_b = NewPaper_Black_b FROM CPSync4D.Project AS P 
						INNER JOIN CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID
					WHERE M.MeasurementID = @MeasurementID

					SELECT     
								@CurrPaper_L = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(ISNULL(RL.[TargetL*],0)) ELSE AVG(ISNULL(CRL.[TargetL*],0)) END,
								@CurrPaper_a = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(ISNULL(RL.[Targeta*],0)) ELSE AVG(ISNULL(CRL.[Targeta*],0)) END,
								@CurrPaper_b = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(ISNULL(RL.[Targetb*],0)) ELSE AVG(ISNULL(CRL.[Targetb*],0)) END
					FROM CPSyncV.Measurement AS M
								INNER JOIN CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSync4D.Project AS P ON M.ProjectID = P.ProjectID
								INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
								INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
								LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
								LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1

					WHERE     M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8
					GROUP BY SP.SourceProfileType,RC.PatchTypeID

					--SELECT @NewPaper_White_L,@NewPaper_White_a,@NewPaper_White_b,@CurrPaper_L,@CurrPaper_a,@CurrPaper_b;
			
					-------Storing Paper scaled L,a,b values into temp table-------------
					DECLARE @tmpReferenceLAB Table(
								ReferenceChartID int,
								SourceProfileID int,
								TargetL DECIMAL(6,2),
								Targeta DECIMAL(6,2),
								Targetb DECIMAL(6,2)
							);
					
					INSERT INTO @tmpReferenceLAB
									(
										ReferenceChartID
										,SourceProfileID
										,TargetL
										,Targeta
										,Targetb
									)
						SELECT a.ReferenceChartID
								,a.SourceProfileID
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 1, ',') AS DECIMAL(6,2)) AS TargetL
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 2, ',') AS DECIMAL(6,2)) AS Targeta
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 3, ',') AS DECIMAL(6,2)) AS Targetb
						FROM
							(
								SELECT
										CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.ReferenceChartID ELSE CRL.ReferenceChartID END AS ReferenceChartID
										,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.SourceProfileID ELSE CRL.SourceProfileID END AS SourceProfileID
										,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN 
											CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, RL.[TargetL*],RL.[Targeta*],RL.[Targetb*]) 
										ELSE 
											CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, CRL.[TargetL*],CRL.[Targeta*],CRL.[Targetb*]) 
										END AS NewPaperLAB
								FROM CPSyncV.Measurement AS M
								INNER JOIN CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSyncV.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
								INNER JOIN CPSync4D.Project AS P ON M.ProjectID = P.ProjectID
								INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
								INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
								LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
								LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
								WHERE     M.MeasurementID = @MeasurementID
							)  as a
					
					----Get new Paper value from Paper scalling------
					SELECT @Paper_a = ISNULL(AVG([Targeta]),0) , @Paper_b = ISNULL(AVG([Targetb]),0)
						FROM CP.ReferenceChart AS RC INNER JOIN
								@tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
								CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
								CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
								CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID AND M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSyncV.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
						WHERE  RC.PatchTypeID = 8  AND M.MeasurementID = @MeasurementID
				
					--Insert Gray --ΔCh / ΔF values
					INSERT INTO CPSyncV.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
						SELECT DISTINCT MP.MeasurementPatchID , 4 --ΔF or --ΔCh
							,CP.fn_CalcΔCh_Sync4D([CIELAB_a*],[CIELAB_b*],Targeta,Targetb)   
							FROM CP.ReferenceChart AS RC INNER JOIN
								@tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
								CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
								CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
								CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID AND M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSyncV.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
						WHERE  RC.PatchTypeID between 28 and 50  AND M.MeasurementID = @MeasurementID
					UNION ALL
					SELECT DISTINCT MP.MeasurementPatchID , 18 --Weighted ΔCh
							,(CP.fn_CalcΔCh_Sync4D([CIELAB_a*],[CIELAB_b*],Targeta,Targetb) * (1 - (SELECT CASE WHEN 0 > ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) THEN 0 ELSE ISNULL(((RC.CMYK_C - 50)/50 * 0.75),0) END )))
							FROM CP.ReferenceChart AS RC INNER JOIN
								@tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
								CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
								CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
								CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID AND M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSyncV.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
						WHERE  RC.PatchTypeID between 28 and 50  AND M.MeasurementID = @MeasurementID
		
				END
		END

	--Insert Gray a* values
	INSERT INTO CPSyncV.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
    SELECT DISTINCT MP.MeasurementPatchID , 11 --Gray a* (Target) G7 
	   ,ROUND((@Paper_a * (100 - RC.CMYK_C)/100),2) As [Gray a*] from
	CPSyncV.Measurement AS M INNER JOIN 
	CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
	CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
	WHERE  RC.PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)  AND M.MeasurementID = @MeasurementID
	
	--Insert Gray b* values
	INSERT INTO CPSyncV.MeasuredColorChar (MeasurementPatchID, ColorCharID, ColorCharValue)
    SELECT DISTINCT MP.MeasurementPatchID , 12 --Gray b* (Target) G7
	   ,ROUND((@Paper_b * (100 - RC.CMYK_C)/100),2) As [Gray b*] from
	CPSyncV.Measurement AS M INNER JOIN 
	CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
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
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END
