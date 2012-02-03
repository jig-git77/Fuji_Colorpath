IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDeviationByGUID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDeviationByGUID]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/29/2008
-- Description:	Deviation
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDeviationByGUID]
(
	@MeasurementGUID AS  uniqueidentifier
)
AS
BEGIN
	BEGIN TRY
		
SELECT 
	CASE TBL_AREA.AREA_TARGET WHEN 25 THEN '25%'
							WHEN 50 THEN '50%'
							WHEN 75 THEN '75%'
							WHEN 95 THEN '95%'
	END AS AREA_TARGET, 
	ISNULL(DEVIATION, 0) AS DEVIATION
FROM
	(
		SELECT 25 AS AREA_TARGET
		UNION ALL 
		SELECT 50 AS AREA_TARGET
		UNION ALL 
		SELECT 75 AS AREA_TARGET
		UNION ALL 
		SELECT 95 AS AREA_TARGET
	) AS TBL_AREA
	LEFT OUTER JOIN CPPatchDataPlate ON CPPatchDataPlate.MeasurementGUID = @MeasurementGUID
		and CPPatchDataPlate.DOT_AREA_TARGET = TBL_AREA.AREA_TARGET

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
