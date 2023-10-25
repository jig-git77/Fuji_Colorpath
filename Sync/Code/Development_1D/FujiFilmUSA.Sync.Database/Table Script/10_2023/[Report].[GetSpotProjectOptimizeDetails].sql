
/****** Object:  StoredProcedure [Report].[GetSpotProjectOptimizeDetails]    Script Date: 10/25/2023 12:59:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXEC [Report].[GetSpotProjectOptimizeDetails] 
	@OptimizeRunID  = 92
*/

ALTER PROCEDURE [Report].[GetSpotProjectOptimizeDetails] 
@OptimizeRunID  int  
AS
BEGIN
	BEGIN TRY

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

				SELECT	@MeasurementID AS  MeasurementID
						,P.ProjectName
						,'Measurement Name' AS MeasurementName
						,GETDATE() AS MeasurementDate
						,D.DeviceName
						,PC.Media
						,PC.Colorant
						,PC.RipSettings
						,PC.TechnicianName
						,PC.Notes
						,ST.SpotTargetName AS ProfileName
						,B.Location
						,C.CompanyName
						,B.BranchName
						,P.LicenseTypeID
						,IT.InstrumentType
						,MD.SerialNumber
						,'E2000' AS DeltaEFormula
						,MS.UVMode	
						,(SELECT COUNT(1) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID) AS TotalColors
						,(SELECT COUNT(1) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND IsSelected=1) AS SelectedColors
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 5  ) AS EGRT5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 3  ) AS EGRT3
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 2.5  ) AS EGRT2_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 2  ) AS EGRT2
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 1.5  ) AS EGRT1_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 1  ) AS EGRT1
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 0.5  ) AS EGRT0_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 0.25  ) AS EGRT0_25
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 0.1  ) AS EGRT0_1
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] > 0.05  ) AS EGRT0_05		
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 5  ) AS ELTEQ5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 3  ) AS ELTEQ3
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 2.5  ) AS ELTEQ2_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 2  ) AS ELTEQ2
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 1.5  ) AS ELTEQ1_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 1  ) AS ELTEQ1
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 0.5  ) AS ELTEQ0_5
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 0.25  ) AS ELTEQ0_25
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 0.1  ) AS ELTEQ0_1
						,(SELECT COUNT(*) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND [ΔE] <= 0.05  ) AS ELTEQ0_05
						,(SELECT AVG([ΔE]) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID ) AS EAVG
						,(SELECT MAX([ΔE]) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID ) AS EMAX
				FROM	[CPSpot].[OptimizeRun] AS O
				INNER JOIN [CPSpot].[SpotProject] SP ON SP.SpotProjectID=O.SpotProjectID
				INNER JOIN CPSpot.SpotTarget ST ON SP.SpotTargetID = ST.SpotTargetID  
				INNER JOIN CPSync4D.Project AS P ON P.ProjectID = SP.ProjectID
				INNER JOIN CPSpot.PrintCondition AS PC ON SP.SpotProjectID = PC.SpotProjectID
				LEFT JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeId = P.PrintStandardTypeId
				LEFT JOIN CPSync4D.SourceProfile AS S ON PST.SourceProfileID = S.SourceProfileID
				INNER JOIN CP.Device AS D ON P.CPDeviceID = D.DeviceID
				INNER JOIN CP.License AS L ON D.LicenseID = L.LicenseID
				INNER JOIN CP.Branch AS B ON L.BranchID = B.BranchID
				INNER JOIN CP.Company AS C ON C.CompanyID = B.CompanyID
				INNER JOIN [CPSpot].[MeasurementSetting] AS MS ON MS.SpotProjectID = SP.SpotProjectID
				INNER JOIN [CP].[MeasurementDevice] AS MD ON MD.MeasurementDeviceID = MS.MeasurementDeviceID
				INNER JOIN [CP].[InstrumentType] AS IT ON IT.InstrumentTypeID = MD.InstrumentTypeID
				WHERE	O.OptimizeRunID = @OptimizeRunID 
				
			END
			ELSE								-----------For Run-1 or above
			BEGIN
				SELECT	@MeasurementID AS  MeasurementID
						,P.ProjectName
						,'Measurement Name' AS MeasurementName
						,GETDATE() AS MeasurementDate
						,D.DeviceName
						,PC.Media
						,PC.Colorant
						,PC.RipSettings
						,PC.TechnicianName
						,PC.Notes
						,ST.SpotTargetName AS ProfileName
						,B.Location
						,C.CompanyName
						,B.BranchName
						,P.LicenseTypeID
						,IT.InstrumentType
						,MD.SerialNumber
						,'E2000' AS DeltaEFormula
						,MS.UVMode	
						,(SELECT COUNT(1) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID) AS TotalColors
						,(SELECT COUNT(1) FROM [CPSpot].[SpotProjectLAB] AS SPL WHERE SPL.SpotProjectID = SP.SpotProjectID AND IsSelected=1) AS SelectedColors
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 5  ) AS EGRT5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 3  ) AS EGRT3
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 2.5  ) AS EGRT2_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 2  ) AS EGRT2
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 1.5  ) AS EGRT1_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 1  ) AS EGRT1
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 0.5  ) AS EGRT0_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 0.25  ) AS EGRT0_25
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 0.1  ) AS EGRT0_1
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] > 0.05  ) AS EGRT0_05		
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 5  ) AS ELTEQ5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 3  ) AS ELTEQ3
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 2.5  ) AS ELTEQ2_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 2  ) AS ELTEQ2
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 1.5  ) AS ELTEQ1_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 1  ) AS ELTEQ1
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 0.5  ) AS ELTEQ0_5
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 0.25  ) AS ELTEQ0_25
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 0.1  ) AS ELTEQ0_1
						,(SELECT COUNT(*) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID AND [ΔE_M_T] <= 0.05  ) AS ELTEQ0_05
						,(SELECT AVG([ΔE_M_T]) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID ) AS EAVG
						,(SELECT MAX([ΔE_M_T]) FROM [CPSpot].[MeasurementPatch] AS MP  WHERE MP.MeasurementID = M.MeasurementID ) AS EMAX
				FROM	[CPSpot].[MeasurementPatch] AS MP
				INNER JOIN [CPSpot].[Measurement] M ON M.MeasurementID=MP.MeasurementID
				INNER JOIN [CPSpot].[OptimizeRun] AS O  ON O.OptimizeRunID=M.OptimizeRunID
				INNER JOIN [CPSpot].[SpotProject] SP ON SP.SpotProjectID=O.SpotProjectID
				INNER JOIN CPSpot.SpotTarget ST ON SP.SpotTargetID = ST.SpotTargetID  
				INNER JOIN CPSync4D.Project AS P ON P.ProjectID = SP.ProjectID
				INNER JOIN CPSpot.PrintCondition AS PC ON SP.SpotProjectID = PC.SpotProjectID
				LEFT JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeId = P.PrintStandardTypeId
				LEFT JOIN CPSync4D.SourceProfile AS S ON PST.SourceProfileID = S.SourceProfileID
				INNER JOIN CP.Device AS D ON P.CPDeviceID = D.DeviceID
				INNER JOIN CP.License AS L ON D.LicenseID = L.LicenseID
				INNER JOIN CP.Branch AS B ON L.BranchID = B.BranchID
				INNER JOIN CP.Company AS C ON C.CompanyID = B.CompanyID
				INNER JOIN [CPSpot].[MeasurementSetting] AS MS ON MS.SpotProjectID = SP.SpotProjectID
				INNER JOIN [CP].[MeasurementDevice] AS MD ON MD.MeasurementDeviceID = MS.MeasurementDeviceID
				INNER JOIN [CP].[InstrumentType] AS IT ON IT.InstrumentTypeID = MD.InstrumentTypeID
				WHERE	O.OptimizeRunID = @OptimizeRunID 
			END
		
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
