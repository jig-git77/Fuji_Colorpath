USE [ColorPathSync_Demo]
GO
/****** Object:  StoredProcedure [CPSync].[ImportMeasurements]    Script Date: 3/2/2020 3:59:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
EXEC [CPSync].[ImportMeasurements]      
 @OriginalMeasurementID=10857
,@NewOptimizeRunID=6569  

*/

ALTER PROCEDURE [CPSync].[ImportMeasurements]      
 @OriginalMeasurementID  int 
,@NewOptimizeRunID  int  
,@UserID int
  
AS      
BEGIN      
		DECLARE @NewMeasurementID INT;
		BEGIN TRY	
		BEGIN TRAN

		DECLARE @tmpMeasurementPatch Table(
					SampleName nvarchar(50),
					MeasurementPatchID INT,
					MeasurementID INT
				);

		INSERT INTO CPSync.Measurement
				(
					MeasurementName,
					OptimizeRunID,
					MeasurementDate,
					ReferenceChartTypeID,
					IsUsed,
					MetricTargetPresetID,
					IsOK
				)
		SELECT M.MeasurementName, @NewOptimizeRunID, 
		M.MeasurementDate, M.ReferenceChartTypeID, M.IsUsed,MetricTargetPresetID,0
		FROM CPSync.Measurement M
		WHERE M.MeasurementID = @OriginalMeasurementID


		SET @NewMeasurementID = @@identity		

		INSERT INTO CPSync.MeasurementPatch
					(
						MeasurementID, SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*]	
					   ,ΔE, ΔE94, ΔE2000, ΔH, Density_C, Density_M, Density_Y, Density_V						   
					)
			output inserted.SampleName,INSERTED.MeasurementPatchID,INSERTED.MeasurementID into @tmpMeasurementPatch
			SELECT @NewMeasurementID,SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*], 
					ΔE, ΔE94, ΔE2000, ΔH, Density_C, Density_M, Density_Y, Density_V				
				FROM CPSync.MeasurementPatch WHERE MeasurementID = @OriginalMeasurementID
				ORDER BY SampleName
	

		INSERT INTO CPSync.MeasurementPatch_Spectral
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
				FROM CPSync.MeasurementPatch_Spectral MPS
						INNER JOIN @tmpMeasurementPatch TMP ON TMP.SampleName = MPS.SampleName 
						INNER JOIN CPSync.MeasurementPatch AS MP ON MP.MeasurementPatchID = MPS.MeasurementPatchID
				WHERE MP.MeasurementID = @OriginalMeasurementID


			--UPDATING ΔE,ΔE94,ΔE2000 VALUES into new Measurement since older ΔE,ΔE94,ΔE2000 VALUES calculated with different SourceProfile might not match newer other Source Profile
			--UPDATING ΔE,ΔE94,ΔE2000,ΔH VALUES
			UPDATE
					MP
						  SET
								MP.ΔE = CP.fn_CalcΔE(
									  PLAB.[TargetL*],
									  PLAB.[Targeta*],
									  PLAB.[Targetb*],
									  MP.[CIELAB_L*],
									  MP.[CIELAB_a*],
									  MP.[CIELAB_b*]),
								MP.ΔE94 = CP.fn_CalcΔE94(
									  PLAB.[TargetL*],
									  PLAB.[Targeta*],
									  PLAB.[Targetb*],
									  MP.[CIELAB_L*],
									  MP.[CIELAB_a*],
									  MP.[CIELAB_b*]),
								MP.ΔE2000 = CP.fn_CalcΔE2000(
									  PLAB.[TargetL*],
									  PLAB.[Targeta*],
									  PLAB.[Targetb*],
									  MP.[CIELAB_L*],
									  MP.[CIELAB_a*],
									  MP.[CIELAB_b*]),
								MP.ΔH = CP.fn_CalcΔH(
									  PLAB.[TargetL*],
									  PLAB.[Targeta*],
									  PLAB.[Targetb*],
									  MP.[CIELAB_L*],
									  MP.[CIELAB_a*],
									  MP.[CIELAB_b*])
						  FROM
							CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch AS MP 
								  ON M.MeasurementID = MP.MeasurementID								
							INNER JOIN CPSync.OptimizeRun AS OPT
								  ON OPT.OptimizeRunID = M.OptimizeRunID
							INNER JOIN CPSync4D.Project AS P 
								  ON P.ProjectID = OPT.ProjectID
							INNER JOIN CPSync.ProjectTargetLAB AS PLAB
								  ON PLAB.ProjectID = P.ProjectID
							INNER JOIN CP.ReferenceChart AS RC
								  ON RC.SampleName = MP.SampleName 
									AND RC.ChartTypeID = M.ReferenceChartTypeID		
									AND RC.PatchTypeID = PLAB.PatchTypeID		
						  WHERE
								M.MeasurementID = @NewMeasurementID 
			
			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE()
				 WHERE ProjectID = 
						(SELECT OPT.ProjectID FROM CPSync4D.OptimizeRun AS OPT 
							INNER JOIN CPSync.Measurement AS M ON OPT.OptimizeRunID = M.OptimizeRunID 				
							AND M.MeasurementID = @NewMeasurementID)	
			COMMIT TRAN									

			BEGIN TRAN
				---INSERT DOTGAIN VALUES INTO CPSync.MeasuredColorChar TABLE
				EXEC [CPSync].[uspInsertColorCharDotGain] @NewMeasurementID, @UserID
				---INSERT ΔCh VALUES INTO CPSync.MeasuredColorChar TABLE
				EXEC [CPSync].[uspInsertColorCharΔCh] @NewMeasurementID, @UserID 
				---INSERT ΔL VALUES INTO CPSync.MeasuredColorChar TABLE
				EXEC [CPSync].[uspInsertColorCharΔL] @NewMeasurementID, @UserID 

				--------Set Metrics----------------			
				EXEC [CPSync].[CalculateMetrics] @NewMeasurementID, @UserID 			
			
				-----Update IsOk entry onto CPSync.Measurement table-------
				UPDATE CPSync.Measurement SET IsOK = 1 WHERE MeasurementID = @NewMeasurementID
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
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






