IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDeviationByCPDevicePrintTargetID_Admin')
	BEGIN
		DROP  Procedure [CPV].[uspGetDeviationByCPDevicePrintTargetID_Admin]
	END

GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 08/12/2008
-- Description:	Deviation on Print Target ID
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDeviationByCPDevicePrintTargetID_Admin](@CPDevicePrintTargetID INT)
AS
BEGIN
BEGIN TRY
	
	SELECT 
		CPMeasurement.CPMeasurementGUID AS MeasurementGUID
		, ISNULL(A.Deviation , 0) AS Deviation_25  
		, ISNULL(B.Deviation , 0) AS Deviation_50
		, ISNULL(C.Deviation , 0) AS Deviation_75  
		, ISNULL(D.Deviation , 0) AS Deviation_95  
		, (CPMeasurement.CreatedDt) as Created_Time
		, Status
	FROM 
		CPMeasurement
		INNER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID
		LEFT OUTER JOIN CPPatchDataPlate A ON A.MeasurementGUID = CPMeasurement.CPMeasurementGUID AND A.DOT_AREA_TARGET = 25
		LEFT OUTER JOIN CPPatchDataPlate B ON B.MeasurementGUID = CPMeasurement.CPMeasurementGUID AND B.DOT_AREA_TARGET = 50
		LEFT OUTER JOIN CPPatchDataPlate C ON C.MeasurementGUID = CPMeasurement.CPMeasurementGUID AND C.DOT_AREA_TARGET = 75
		LEFT OUTER JOIN CPPatchDataPlate D ON D.MeasurementGUID = CPMeasurement.CPMeasurementGUID AND D.DOT_AREA_TARGET = 95
	WHERE
		CPDevicePrintTargetID = @CPDevicePrintTargetID
		AND ISNULL( CPMeasurement.IsUsed , 1 ) = 1
	ORDER BY
		CPMeasurement.CreatedDt

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

