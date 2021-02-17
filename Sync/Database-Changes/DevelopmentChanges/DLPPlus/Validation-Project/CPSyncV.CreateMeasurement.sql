

ALTER PROCEDURE [CPSyncV].[CreateMeasurement]
	@MeasurementName NVARCHAR(50),
	@ProjectID INT,
	@MeasurementDate DATETIME,
	@ReferenceChartTypeID TINYINT,
	@MeasurementPatches XML,
	@MeasurementID INT OUTPUT,
	@Notes NVARCHAR(1000),
	@Customer NVARCHAR(50),
	@PageOrSurface NVARCHAR(50),
	@JDFJobId NVARCHAR(50),
	@UserID INT,
	@MeasurementInfo  NVARCHAR(500)
AS
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO CPSyncV.Measurement
				(
					MeasurementName,
					ProjectID,
					MeasurementDate,
					IsUsed,
					Notes,
					Customer,
					ReferenceChartTypeID,
					PageOrSurface,
					JDFJobId,
					MeasurementInfo
				)
			VALUES
				(
					@MeasurementName,
					@ProjectID,
					@MeasurementDate,
					1,
					@Notes,
					@Customer,
					@ReferenceChartTypeID,
					@PageOrSurface,
					@JDFJobId,
					@MeasurementInfo
				)
				
			SET @MeasurementID = @@IDENTITY

			--INSERTING MEASUREMENT PATCHES			
			;WITH MeasurementPatches AS
			(
				SELECT           
					[MeasurementPatchXml].value('@SampleName', 'NVARCHAR(50)') AS SampleName,
					[MeasurementPatchXml].value('@CIEXYZ_X', 'DECIMAL(6,2)') AS CIEXYZ_X,
					[MeasurementPatchXml].value('@CIEXYZ_Y', 'DECIMAL(6,2)') AS CIEXYZ_Y,
					[MeasurementPatchXml].value('@CIEXYZ_Z', 'DECIMAL(6,2)') AS CIEXYZ_Z,
					[MeasurementPatchXml].value('@CIELAB_L', 'DECIMAL(6,2)') AS CIELAB_L,
					[MeasurementPatchXml].value('@CIELAB_a', 'DECIMAL(6,2)') AS CIELAB_a,
					[MeasurementPatchXml].value('@CIELAB_b', 'DECIMAL(6,2)') AS CIELAB_b,
					[MeasurementPatchXml].value('@Density_C', 'DECIMAL(9,5)') AS Density_C,
					[MeasurementPatchXml].value('@Density_M', 'DECIMAL(9,5)') AS Density_M,
					[MeasurementPatchXml].value('@Density_Y', 'DECIMAL(9,5)') AS Density_Y,
					[MeasurementPatchXml].value('@Density_V', 'DECIMAL(9,5)') AS Density_V
				FROM
					@MeasurementPatches.
						nodes('/MeasurementPatches/MeasurementPatch') p([MeasurementPatchXml])
			)
			
			INSERT INTO CPSyncV.MeasurementPatch
				(
					MeasurementID,
					SampleName,
					CIEXYZ_X,
					CIEXYZ_Y,
					CIEXYZ_Z,
					[CIELAB_L*],
					[CIELAB_a*],
					[CIELAB_b*],
					Density_C,
					Density_M,
					Density_Y,
					Density_V
				)
			SELECT
					@MeasurementID,
					SampleName,
					CIEXYZ_X,
					CIEXYZ_Y,
					CIEXYZ_Z,
					CIELAB_L,
					CIELAB_a,
					CIELAB_b,
					Density_C,
					Density_M,
					Density_Y,
					Density_V
			FROM
					MeasurementPatches
					
			--UPDATING ΔE,ΔE94,ΔE2000,ΔH VALUES
			
			------Getting LicenseType of Project-----------
			DECLARE @LicenseType int;
			SELECT @LicenseType = LicenseTypeID FROM CPSync4D.Project AS P 
						WHERE P.ProjectID = @ProjectID

			IF @LicenseType = 1 -----1D Project Validation, then Pass 1D Target L, a, b values from CPSync.ProjectTargetLAB table
			BEGIN
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
								CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch AS MP 
									  ON M.MeasurementID = MP.MeasurementID	
								INNER JOIN CPSync4D.Project AS P 
									  ON P.ProjectID = M.ProjectID
								INNER JOIN CPSync.ProjectTargetLAB AS PLAB
									  ON PLAB.ProjectID = P.ProjectID
								INNER JOIN CP.ReferenceChart AS RC
									  ON RC.SampleName = MP.SampleName 
										AND RC.ChartTypeID = M.ReferenceChartTypeID		
										AND RC.PatchTypeID = PLAB.PatchTypeID		
							  WHERE
									M.MeasurementID = @MeasurementID 
			END			
			ELSE   -----For all other type of projects pass Target L, a, b values from CPSync4D.ReferenceLAB table
			BEGIN
				DECLARE @EnabledPaperScaling BIT;

				SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project AS P 
							INNER JOIN CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID
						WHERE M.MeasurementID = @MeasurementID

				IF (@EnabledPaperScaling = 0)	---If no Paper scaling------
					BEGIN
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
													END,
											MP.ΔH = CASE ISNULL(SP.SourceProfileType,0) 
													WHEN 0 THEN		
														CP.fn_CalcΔH(
														  RL.[TargetL*],
														  RL.[Targeta*],
														  RL.[Targetb*],
														  MP.[CIELAB_L*],
														  MP.[CIELAB_a*],
														  MP.[CIELAB_b*])
													ELSE
														CP.fn_CalcΔH(
														  CRL.[TargetL*],
														  CRL.[Targeta*],
														  CRL.[Targetb*],
														  MP.[CIELAB_L*],
														  MP.[CIELAB_a*],
														  MP.[CIELAB_b*])
													END

											--MP.ΔE = CP.fn_CalcΔE(
											--	  RL.[TargetL*],
											--	  RL.[Targeta*],
											--	  RL.[Targetb*],
											--	  MP.[CIELAB_L*],
											--	  MP.[CIELAB_a*],
											--	  MP.[CIELAB_b*]),
											--MP.ΔE94 = CP.fn_CalcΔE94(
											--	  RL.[TargetL*],
											--	  RL.[Targeta*],
											--	  RL.[Targetb*],
											--	  MP.[CIELAB_L*],
											--	  MP.[CIELAB_a*],
											--	  MP.[CIELAB_b*]),
											--MP.ΔE2000 = CP.fn_CalcΔE2000(
											--	  RL.[TargetL*],
											--	  RL.[Targeta*],
											--	  RL.[Targetb*],
											--	  MP.[CIELAB_L*],
											--	  MP.[CIELAB_a*],
											--	  MP.[CIELAB_b*]),
											--MP.ΔH = CP.fn_CalcΔH(
											--	  RL.[TargetL*],
											--	  RL.[Targeta*],
											--	  RL.[Targetb*],
											--	  MP.[CIELAB_L*],
											--	  MP.[CIELAB_a*],
											--	  MP.[CIELAB_b*])
									  FROM
											CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch AS MP 
														ON M.MeasurementID = MP.MeasurementID								
											INNER JOIN CP.ReferenceChart AS RC
														ON RC.SampleName = MP.SampleName 
														AND RC.ChartTypeID = M.ReferenceChartTypeID
											--INNER JOIN CPSync4D.ReferenceLAB AS RL 
											--	  ON RL.ReferenceChartID = RC.ReferenceChartID
											--INNER JOIN CPSync4D.Project AS P 
											--	  ON P.ProjectID = M.ProjectID
											--INNER JOIN CP.PrintStandardType AS PST
											--	  ON P.PrintStandardTypeId = PST.PrintStandardTypeID											
											INNER JOIN CPSync4D.Project AS P 
												  ON P.ProjectID = M.ProjectID
											INNER JOIN CP.PrintStandardType AS PST 
													ON PST.PrintStandardTypeID = P.PrintStandardTypeId
											INNER JOIN CPSync4D.SourceProfile AS SP
													ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
											LEFT JOIN CPSync4D.ReferenceLab AS RL 
													ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
											LEFT JOIN CPSync4D.CustomReferenceLab AS CRL 
													ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
									  WHERE
											M.MeasurementID = @MeasurementID 
											AND CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN  
													RL.SourceProfileID ELSE CRL.SourceProfileID END = PST.SourceProfileID
					END

				ELSE		---If Paper scaling------
					BEGIN
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

						SELECT  @CurrPaper_L = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[TargetL*]) ELSE AVG(CRL.[TargetL*]) END,
								@CurrPaper_a = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targeta*]) ELSE AVG(CRL.[Targeta*]) END,
								@CurrPaper_b = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targetb*]) ELSE AVG(CRL.[Targetb*]) END
						FROM CPSyncV.Measurement AS M
						INNER JOIN CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID 
						INNER JOIN CPSync4D.Project AS P ON M.ProjectID = P.ProjectID
						INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
						INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
						LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
						LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
									
						--FROM       CP.ReferenceChart AS RC INNER JOIN
						--					 -- CPSync4D.ReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
						--					  CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
						--					  CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
						--					  CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID AND M.ReferenceChartTypeID = RC.ChartTypeID 

						WHERE     M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8
						GROUP BY SP.SourceProfileType,RC.PatchTypeID

				--		SELECT @NewPaper_White_L,@NewPaper_White_a,@NewPaper_White_b,@CurrPaper_L,@CurrPaper_a,@CurrPaper_b;
						
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
										--FROM

										--	  CPSync4D.ReferenceLAB AS RL INNER JOIN CP.ReferenceChart AS RC ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
										--	  CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
										--	  CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
										--	  CPSyncV.Measurement AS M ON M.ProjectID = P.ProjectID AND M.ReferenceChartTypeID = RC.ChartTypeID 
										--	  INNER JOIN CPSyncV.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName

										WHERE     M.MeasurementID = @MeasurementID
									)  as a
						
							UPDATE
								MP
									  SET
											MP.ΔE = CP.fn_CalcΔE(
												  RL.[TargetL],
												  RL.[Targeta],
												  RL.[Targetb],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*]),
											MP.ΔE94 = CP.fn_CalcΔE94(
												  RL.[TargetL],
												  RL.[Targeta],
												  RL.[Targetb],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*]),
											MP.ΔE2000 = CP.fn_CalcΔE2000(
												  RL.[TargetL],
												  RL.[Targeta],
												  RL.[Targetb],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*]),
											MP.ΔH = CP.fn_CalcΔH(
												  RL.[TargetL],
												  RL.[Targeta],
												  RL.[Targetb],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
									  FROM
											CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch AS MP 
														ON M.MeasurementID = MP.MeasurementID								
											INNER JOIN CP.ReferenceChart AS RC
														ON RC.SampleName = MP.SampleName 
														AND RC.ChartTypeID = M.ReferenceChartTypeID
											INNER JOIN @tmpReferenceLAB AS RL 
												  ON RL.ReferenceChartID = RC.ReferenceChartID
											INNER JOIN CPSync4D.Project AS P 
												  ON P.ProjectID = M.ProjectID
											INNER JOIN CP.PrintStandardType AS PST
												  ON P.PrintStandardTypeId = PST.PrintStandardTypeID
									  WHERE
											M.MeasurementID = @MeasurementID 
											AND RL.SourceProfileID = PST.SourceProfileID
					END
			
			
				IF @LicenseType = 11 -----DLPPLUS Project Validation then Pass 1D Target L, a, b values from CPSync4DPlus.ProjectTargetLAB table
				BEGIN
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
										CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch AS MP 
											  ON M.MeasurementID = MP.MeasurementID	
										INNER JOIN CPSync4D.Project AS P 
											  ON P.ProjectID = M.ProjectID
										INNER JOIN CPSync4DPlus.ProjectTargetLAB AS PLAB
											  ON PLAB.ProjectID = P.ProjectID
										INNER JOIN CP.ReferenceChart AS RC
											  ON RC.SampleName = MP.SampleName 
												AND RC.ChartTypeID = M.ReferenceChartTypeID		
												AND RC.PatchTypeID = PLAB.PatchTypeID		
									  WHERE
											M.MeasurementID = @MeasurementID 
					

					END
					
			END
			
			-- fetch record from CPSync4D.MeasurementSetting and insert it into CPSyncV.MeasurementSetting for every measurements...
			INSERT INTO CPSyncV.MeasurementSetting
			SELECT
				ProjectID,
				@MeasurementID,
				ObserverType,
				Illuminant,				
				VMeasurementDeviceID,
				VReferenceChartTypeID,
				MetricTargetPresetID,
				VUVMode,
				VReadMode,
				VMeasurementCount,
				PluginType
			FROM
				CPSync4D.MeasurementSetting
			WHERE
				ProjectID = @ProjectID

		COMMIT TRAN

		-----Separate two transactions so even if metric calculations fails still it will make measurement entry as bad measurement into tables-----	

		BEGIN TRAN
			---INSERT DOTGAIN VALUES INTO CPSyncV.MeasuredColorChar TABLE
			EXEC [CPSyncV].[uspInsertColorCharDotGain] @MeasurementID,@UserID

			--------Set metrics----------------			
			EXEC [CPSyncV].[CalculateMetrics] @MeasurementID,@UserID

			-----Update IsOk entry onto CPSyncV.Measurement table-------
			UPDATE CPSyncV.Measurement SET IsOK = 1 WHERE MeasurementID = @MeasurementID

			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE() WHERE ProjectID = @ProjectID

			-----Delete From dbo.MeasurementError_File table if IsOK=1 AND upload sucessfully ---
			DELETE FROM dbo.MeasurementError_File WHERE ISNULL(OptimizationRunID,0)= 0 AND ProjectID=@ProjectID

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @MeasurementID =-1
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 

		--EXEC [dbo].[CreateMeasurementLog] @UserID,@MeasurementID,'Error in validation project measurement',@ERROR_MESSAGE,'SyncValidation'

		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH



GO


