
/****** Object:  StoredProcedure [CP].[GetReferenceChartTypeList]    Script Date: 1/23/2024 11:59:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[GetReferenceChartTypeList] 
	@MeasurementDeviceID INT
   ,@ChartDisplayType INT
   ,@ProjectTypeID INT
   ,@MetricTargetPresetID INT
   ,@InstrumentTypeID INT
   ,@IsDLPPlus BIT=0 
   ,@LicenseTypeId TINYINT
   ,@DeviceId INT
AS

	DECLARE @FinalInstrumentTypeID INT;
	DECLARE @TotalNoColor INT;
	DECLARE @DeviceFamilyID INT;
	
	SELECT @DeviceFamilyID = DeviceFamilyID  FROM [CP].[Device] WHERE DeviceID = @DeviceId;
	
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
						IF(@LicenseTypeId = 13)---When project type is Sync1DMulticolor
						BEGIN
							   SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId 
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)))
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
									
								END
								IF (@TotalNoColor = 6)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId											
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0))
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		

								END
								IF (@TotalNoColor = 7)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))

													)
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 8)
								BEGIN
												SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0))

												)
											)	
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 9)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_8,0))
												)
										)	
										AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
						END
						ELSE
						BEGIN
		
								SELECT 
									RCT.ReferenceChartTypeID, ChartName, ChartPreview
								FROM
									CP.ReferenceChartType  AS RCT INNER JOIN
									CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
									AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
								WHERE				
									InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
									AND IsSync1D_G7 = 1 AND IsUsed = 1
									AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
						END
				END
				ELSE IF (@ProjectTypeID = 2) ----Sync1D ISO Project Type
				BEGIN
				
						IF(@LicenseTypeId = 13)---When project type is Sync1DMulticolor
						BEGIN
								SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										 
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)))
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
											
									
								END
								IF (@TotalNoColor = 6)
								BEGIN
										 
										SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId											
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0))
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 7)
								BEGIN
											
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))

													)
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 8)
								BEGIN
											 
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0))

												)
											)	
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 9)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_8,0))
												)
										)	
										AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
						   
						END
						ELSE
						BEGIN
								SELECT 
									RCT.ReferenceChartTypeID, ChartName, ChartPreview
								FROM
									CP.ReferenceChartType  AS RCT INNER JOIN
									CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
								WHERE				
									InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
									AND IsSync1D_ISO = 1 AND IsUsed = 1
									AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
						END
				
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType AS RCT 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
				END
				ELSE  ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN
						IF(@LicenseTypeId = 12)---When project type is multicolor
						BEGIN
								
								
								SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										
										SELECT 
											RCT.ReferenceChartTypeID, RCT.ChartName, RCT.ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0) 					   
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RCT.IsDLPMultiColor = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)))
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
								END
								IF (@TotalNoColor = 6)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, RCT.ChartName, RCT.ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0) 					   
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RCT.IsDLPMultiColor = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0))
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
									END
								IF (@TotalNoColor = 7)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, RCT.ChartName, RCT.ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0) 					   
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RCT.IsDLPMultiColor = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))

													)
												)
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
									END
								IF (@TotalNoColor = 8)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, RCT.ChartName, RCT.ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0) 					   
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RCT.IsDLPMultiColor = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
										AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0))

												)
											)	
											AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
									END
								IF (@TotalNoColor = 9)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, RCT.ChartName, RCT.ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0) 					   
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RCT.IsDLPMultiColor = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
										AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_8,0))
												)
										)			
										AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
									END
								
								/*
									END:Sequancing of channel color 
									if Totalcolor =6 then channel_5 and channel_6
									need to compare both way with Device and referencechart table.
								*/  	

						END							
						ELSE---WHen we have only 4D project
						BEGIN
							SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM CP.ReferenceChartType AS RCT WHERE InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
						AND ((IsSync4D = 1 AND IsUsed = 1))
						AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
						END
						
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
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
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
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
				END   
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					
					SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
				
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
							
				END
				IF (@ProjectTypeID = 3 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType AS RCT 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
							SELECT @MainInstrumentTypeID,@FinalInstrumentTypeID
				END
				ELSE ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN

					IF(@LicenseTypeId = 12)---When project type is multicolor
					BEGIN
							
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND ((Validation_4D_Mutli_Color = 1 AND IsUsed=1))
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
					END
					ELSE
					BEGIN ---When we have only 4D project
							
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND ((Validation_4D >= 0 AND IsUsed = 1))
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
					END
				
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
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
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
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
							AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
				END
				ELSE   ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN
						IF(@LicenseTypeId = 12)---When project type is multicolor
						BEGIN
							SELECT 
								ReferenceChartTypeID, ChartName, ChartPreview
							FROM
								CP.ReferenceChartType AS RCT  
							WHERE				
								InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
								AND ((IsDLPMultiColor >= 1 AND IsUsed = 1)) 
								AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart	
						END
						ELSE
						BEGIN   ---When we have only 4D project
							SELECT 
								ReferenceChartTypeID, ChartName, ChartPreview
							FROM
								CP.ReferenceChartType AS RCT  
							WHERE				
								InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
								 AND ((IsSync4D >= 1 AND IsUsed = 1)) 
								AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart	
						END
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
			CP.ReferenceChartType AS RCT
		WHERE				
			InstrumentTypeID IN (@InstrumentTypeID,@FinalInstrumentTypeID)
			AND ((Validation_4D >= 0 AND IsUsed = 1) OR (Validation_4D_Mutli_Color >= 1 AND IsUsed = 1))
			AND ISNULL(RCT.DeviceFamilyID,0) = CASE WHEN EXISTS (SELECT DeviceFamilyID FROM CP.ReferenceChartType WHERE DeviceFamilyID = @DeviceFamilyID) THEN @DeviceFamilyID ELSE 0 END -- Line Added to pass device family oriented chart		
	END
