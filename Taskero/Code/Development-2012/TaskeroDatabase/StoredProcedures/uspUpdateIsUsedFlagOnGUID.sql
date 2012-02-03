IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateIsUsedFlagOnGUID')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateIsUsedFlagOnGUID]
	END

GO

-- =============================================
-- Author:	Maulik Dhorajia
-- Create date: 07/25/2008
-- Description:	This will update the isused flag to false.
-- =============================================
CREATE PROCEDURE [CPV].[uspUpdateIsUsedFlagOnGUID]
( 
	@CPMeasurementGUID VARCHAR(50)
)
AS
BEGIN
BEGIN TRY

	UPDATE 
		CPMeasurement 
	SET 
		IsUsed = 0 
	WHERE 
		CPMeasurementGUID = @CPMeasurementGUID

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

