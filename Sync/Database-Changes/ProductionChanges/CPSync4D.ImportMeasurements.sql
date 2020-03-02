USE [ColorPathSync_Demo]
GO
/****** Object:  StoredProcedure [CPSync4D].[ImportMeasurements]    Script Date: 3/2/2020 4:11:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [CPSync4D].[ImportMeasurements]      
 @OriginalMeasurementID  int 
,@NewOptimizeRunID  int  
,@UserID int
AS      
BEGIN      
		DECLARE @NewMeasurementID INT;

		DECLARE @tmpMeasurementPatch Table(
					SampleName nvarchar(50),
					MeasurementPatchID INT,
					MeasurementID INT
				);

		INSERT INTO CPSync4D.Measurement (MeasurementName, OptimizeRunID,  MeasurementDate, ReferenceChartTypeID, IsUsed)
				 SELECT 
					  M.MeasurementName, @NewOptimizeRunID, M.MeasurementDate, M.ReferenceChartTypeID, M.IsUsed   
				  FROM    
					  CPSync4D.Measurement M   
				  WHERE    
				   M.MeasurementID =  @OriginalMeasurementID        

				  SET @NewMeasurementID = @@identity

	   INSERT INTO CPSync4D.MeasurementPatch
		 (MeasurementID, SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*], ΔE, ΔE94, ΔE2000,Density_C,Density_M,Density_Y,Density_V)
			output inserted.SampleName,INSERTED.MeasurementPatchID,INSERTED.MeasurementID into @tmpMeasurementPatch

			SELECT @NewMeasurementID,SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*], ΔE, ΔE94, ΔE2000,Density_C,Density_M,Density_Y,Density_V
					
				FROM CPSync4D.MeasurementPatch WHERE MeasurementID = @OriginalMeasurementID

		INSERT INTO CPSync4D.MeasurementPatch_Spectral
		 (MeasurementPatchID, SampleName 
			       ,nm380
				   ,nm390
				   ,nm400
				   ,nm410
				   ,nm420
				   ,nm430
				   ,nm440
				   ,nm450
				   ,nm460
				   ,nm470
				   ,nm480
				   ,nm490
				   ,nm500
				   ,nm510
				   ,nm520
				   ,nm530
				   ,nm540
				   ,nm550
				   ,nm560
				   ,nm570
				   ,nm580
				   ,nm590
				   ,nm600
				   ,nm610
				   ,nm620
				   ,nm630
				   ,nm640
				   ,nm650
				   ,nm660
				   ,nm670
				   ,nm680
				   ,nm690
				   ,nm700
				   ,nm710
				   ,nm720
				   ,nm730)
			SELECT TMP.MeasurementPatchID						
				   ,TMP.SampleName	
				   ,nm380
				   ,nm390
				   ,nm400
				   ,nm410
				   ,nm420
				   ,nm430
				   ,nm440
				   ,nm450
				   ,nm460
				   ,nm470
				   ,nm480
				   ,nm490
				   ,nm500
				   ,nm510
				   ,nm520
				   ,nm530
				   ,nm540
				   ,nm550
				   ,nm560
				   ,nm570
				   ,nm580
				   ,nm590
				   ,nm600
				   ,nm610
				   ,nm620
				   ,nm630
				   ,nm640
				   ,nm650
				   ,nm660
				   ,nm670
				   ,nm680
				   ,nm690
				   ,nm700
				   ,nm710
				   ,nm720
				   ,nm730
				FROM CPSync4D.MeasurementPatch_Spectral MPS
						INNER JOIN @tmpMeasurementPatch TMP ON TMP.SampleName = MPS.SampleName 
						INNER JOIN CPSync4D.MeasurementPatch AS MP ON MP.MeasurementPatchID = MPS.MeasurementPatchID
				WHERE MP.MeasurementID = @OriginalMeasurementID


			--UPDATING ΔE,ΔE94,ΔE2000 VALUES into new Measurement since older ΔE,ΔE94,ΔE2000 VALUES calculated with different SourceProfile might not match newer other Source Profile
				UPDATE
						MP
							  SET
									MP.ΔE = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END,
									MP.ΔE94 = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE94(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE94(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END,
									MP.ΔE2000 = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE2000(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE2000(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END
							  FROM
									CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch AS MP 
												ON M.MeasurementID = MP.MeasurementID AND M.MeasurementID = @NewMeasurementID 
									INNER JOIN CP.ReferenceChart AS RC
												ON RC.SampleName = MP.SampleName 
												AND RC.ChartTypeID = M.ReferenceChartTypeID
									--INNER JOIN CPSync4D.ReferenceLAB AS RL 
									--	  ON RL.ReferenceChartID = RC.ReferenceChartID
									INNER JOIN CPSync4D.OptimizeRun AS OP 
										  ON OP.OptimizeRunID = M.OptimizeRunID
									INNER JOIN CPSync4D.Project AS P 
										  ON P.ProjectID = OP.ProjectID
									INNER JOIN CP.PrintStandardType AS PST 
											ON PST.PrintStandardTypeID = P.PrintStandardTypeId
									INNER JOIN CPSync4D.SourceProfile AS SP
											ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
									LEFT JOIN CPSync4D.ReferenceLab AS RL 
											ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
									LEFT JOIN CPSync4D.CustomReferenceLab AS CRL 
											ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
					
			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE()
				 WHERE ProjectID = 
						(SELECT OPT.ProjectID FROM CPSync4D.OptimizeRun AS OPT 
							INNER JOIN CPSync4D.Measurement AS M ON OPT.OptimizeRunID = M.OptimizeRunID 
														AND M.MeasurementID = @NewMeasurementID)	
END












