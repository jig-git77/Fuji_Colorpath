IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetColorCharDotGain')
	BEGIN
		DROP  Procedure  [CPV].[uspGetColorCharDotGain]
	END

GO

CREATE PROCEDURE [CPV].[uspGetColorCharDotGain]
(@CPMeasurementGUID		uniqueidentifier)
/*
Author: Chirag Shah
Retrieve DotGain values for a CPMeasurement 
Last Modified Date: 10/10/2008
*/

As 
BEGIN
	BEGIN TRY
	
		
		DECLARE @D0			Decimal(18,2)	--Density of White Paper Patch
		DECLARE @D100_C		Decimal(18,2)	--Density of Solid Cyan Patch
		DECLARE @D100_M		Decimal(18,2)	--Density of Solid Magenta Patch
		DECLARE @D100_Y		Decimal(18,2)   --Density of Solid Yellow  Patch
		DECLARE @D100_K		Decimal(18,2)   --Density of Solid Black   Patch	

		IF (SELECT COUNT(*) as total FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID ) < 175 
		BEGIN

		--Density of White Paper Patch
		SELECT  @D0 = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 0

		--Find density of 100% Cyan Dot
		SELECT  @D100_C = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 100 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density > 0

		--Find density of 100% Magenta Dot
		SELECT  @D100_M = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 100 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density > 0
	
		--Find density of 100% Yellow Dot
		SELECT  @D100_Y = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 100 AND CMYK_K = 0 AND Density > 0		

		--Find density of 100% Key(Black) Dot
		SELECT  @D100_K = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 100 AND Density > 0	

	
		--using Common table expression
		;WITH  Cyan  --find all Cyan Screens
		AS
		(
			SELECT CMYK_C, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C IN (25,30,40,50,70,75,80) AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_C
		)
		SELECT 'Cyan' AS ColorName , CYAN.CMYK_C As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_C,CYAN.Density,CMYK_C),0) As DotGain
		FROM CYAN

		--using Common table expression
		;WITH  Magenta --find all Magenta Screens
		AS
		(
			SELECT CMYK_M, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C = 0 AND CMYK_M IN (25,30,40,50,70,75,80) AND CMYK_Y = 0 AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_M
		)
		SELECT 'Magenta' AS ColorName , Magenta.CMYK_M As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_M,Magenta.Density,CMYK_M),0) As DotGain
		FROM Magenta

		--using Common table expression
		;WITH  Yellow --find all Magenta Screens
		AS
		(
			SELECT CMYK_Y, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y IN (25,30,40,50,70,75,80) AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_Y
		)
		SELECT  'Yellow' AS ColorName , Yellow.CMYK_Y As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_Y,Yellow.Density,CMYK_Y),0) As DotGain
		FROM Yellow
	

		--using Common table expression
		;WITH  Black--find all Yellow Screens
		AS
		(
			SELECT CMYK_K, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID 
			AND CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K IN (25,30,40,50,70,75,80) AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_K
		)
		SELECT 'Black' AS ColorName ,  Black.CMYK_K As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_K,Black.Density,CMYK_K),0) As DotGain
		FROM Black


		END
		ELSE
		BEGIN
		
				--Density of White Paper Patch
		SELECT  @D0 = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 0

		--Find density of 100% Cyan Dot
		SELECT  @D100_C = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 100 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density > 0

		--Find density of 100% Magenta Dot
		SELECT  @D100_M = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 100 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density > 0
	
		--Find density of 100% Yellow Dot
		SELECT  @D100_Y = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 100 AND CMYK_K = 0 AND Density > 0		

		--Find density of 100% Key(Black) Dot
		SELECT  @D100_K = DENSITY FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID AND
		CMYK_C = 0 AND CMYK_M= 0 AND CMYK_Y = 0 AND CMYK_K = 100 AND Density > 0	

	
		--using Common table expression
		;WITH  Cyan  --find all Cyan Screens
		AS
		(
			SELECT CMYK_C, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C IN (25,50,75) AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_C
		)
		SELECT 'Cyan' AS ColorName , CYAN.CMYK_C As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_C,CYAN.Density,CMYK_C),0) As DotGain
		FROM CYAN

		--using Common table expression
		;WITH  Magenta --find all Magenta Screens
		AS
		(
			SELECT CMYK_M, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C = 0 AND CMYK_M IN (25,50,75) AND CMYK_Y = 0 AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_M
		)
		SELECT 'Magenta' AS ColorName , Magenta.CMYK_M As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_M,Magenta.Density,CMYK_M),0) As DotGain
		FROM Magenta

		--using Common table expression
		;WITH  Yellow --find all Magenta Screens
		AS
		(
			SELECT CMYK_Y, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID
			AND CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y IN (25,50,75) AND CMYK_K = 0 AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_Y
		)
		SELECT  'Yellow' AS ColorName , Yellow.CMYK_Y As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_Y,Yellow.Density,CMYK_Y),0) As DotGain
		FROM Yellow
	

		--using Common table expression
		;WITH  Black--find all Yellow Screens
		AS
		(
			SELECT CMYK_K, AVG(Density) As Density FROM CPPatchDataLab WHERE MeasurementGUID = @CPMeasurementGUID 
			AND CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K IN (25,50,75) AND Density IS NOT NULL
			AND Density <> 0
			GROUP BY CMYK_K
		)
		SELECT 'Black' AS ColorName ,  Black.CMYK_K As CMYK , COALESCE(CPV.fnCalcDotGain(@D0,@D100_K,Black.Density,CMYK_K),0) As DotGain
		FROM Black

		END

	END TRY
	BEGIN CATCH          
		DECLARE @ERROR_NUMBER INT           
		DECLARE @ERROR_SEVERITY INT          
		DECLARE @ERROR_STATE INT           
		DECLARE @ERROR_PROCEDURE NVARCHAR(252)           
		DECLARE @ERROR_LINE INT           
		DECLARE @ERROR_MESSAGE NVARCHAR(4000)           
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()           
		EXEC CPV.uspInsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE           
	 END CATCH  
END