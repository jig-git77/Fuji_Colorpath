


ALTER PROCEDURE [CP].[GetReferenceChartTypeList] 
	@MeasurementDeviceID INT
   ,@ChartDisplayType INT
   ,@ProjectTypeID INT
   ,@MetricTargetPresetID INT
   ,@InstrumentTypeID INT
   ,@IsDLPPlus BIT=0 
AS
	
	DECLARE @FinalInstrumentTypeID INT;
	IF (@InstrumentTypeID = 0) ------For normal Measurement Setup page Reference chart Type list binding
	BEGIN
			--	NEED TO FIX THIS ISSUE FOR I1-PRO2, I1-PRO3 AND IO-PRO2, IO-PRO3 RIGHT NOW PASSING HARDCODED --
			DECLARE @MainInstrumentTypeID INT;
			
			SELECT @MainInstrumentTypeID = InstrumentTypeID FROM CP.MeasurementDevice WHERE MeasurementDeviceID = @MeasurementDeviceID
			IF (@MainInstrumentTypeID = 4 OR @MainInstrumentTypeID = 9)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@MainInstrumentTypeID = 5 OR @MainInstrumentTypeID = 10)
			BEGIN
				SET @FinalInstrumentTypeID = 3;
			END
			ELSE IF (@MainInstrumentTypeID = 7)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@MainInstrumentTypeID = 8)
			BEGIN
				SET @FinalInstrumentTypeID = 2;
			END
			
			-- END --

			IF (@ChartDisplayType = 1)
			BEGIN
				--ChartDisplayType = 1 is Profile Chart
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_G7 = 1 AND IsUsed = 1
				END
				ELSE IF (@ProjectTypeID = 2) ----Sync1D ISO Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_ISO = 1 AND IsUsed = 1
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
				END
				ELSE  ----Sync4D or Ink Optimization Project Type
				BEGIN
						SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D = 1 AND IsUsed = 1
				END
			END
			ELSE IF(@ChartDisplayType = 3)
			BEGIN
			
				--ChartDisplayType = 3 is Validation Chart
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_1D >= 0 AND IsUsed = 1
				END
				ELSE IF (@ProjectTypeID = 2) ----Sync1D ISO Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_1D >= 0 AND IsUsed = 1
				END   
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							SELECT @MainInstrumentTypeID,@FinalInstrumentTypeID
				END
				IF (@ProjectTypeID = 3 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							SELECT @MainInstrumentTypeID,@FinalInstrumentTypeID
				END
				ELSE  ----Sync4D or Ink Optimization Project Type
				BEGIN
				
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4D >= 0 AND IsUsed = 1
				END
			END
			ELSE
			BEGIN
				--Here we are supposed to show Profile and Optimization Chart types
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_G7 >= 1 AND IsUsed = 1
				END
				ELSE IF (@ProjectTypeID = 2)  ----Sync1D ISO Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_ISO >= 1 AND IsUsed = 1
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
				END
				ELSE   ----Sync4D or Ink Optimization Project Type
				BEGIN
						SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D >= 1 AND IsUsed = 1
				END
			END
	END
	ELSE ----For Getting ReferenceChartType list for Admin Patch Level Toletance page (Display Sync4D validation charts by default)
	BEGIN	
	

		IF (@InstrumentTypeID = 4 OR @InstrumentTypeID = 9)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 5 OR @InstrumentTypeID = 10)
			BEGIN
				SET @FinalInstrumentTypeID = 3;
			END
			ELSE IF (@InstrumentTypeID = 7)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 8)
			BEGIN
				SET @FinalInstrumentTypeID = 2;
			END

		SELECT 
			ReferenceChartTypeID, ChartName
		FROM
			CP.ReferenceChartType
		WHERE				
			InstrumentTypeID IN (@InstrumentTypeID,@FinalInstrumentTypeID)
			AND Validation_4D >= 0 AND IsUsed = 1
	END








GO


