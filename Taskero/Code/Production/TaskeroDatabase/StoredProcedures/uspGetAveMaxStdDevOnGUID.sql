IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAveMaxStdDevOnGUID')
	BEGIN
		DROP  Procedure [CPV].[uspGetAveMaxStdDevOnGUID]
	END

GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 07/21/2008
-- Description:	Gets the Aver Density, Maximum Density, Standerd Deviation Density.
-- =============================================
CREATE PROCEDURE [CPV].[uspGetAveMaxStdDevOnGUID]
( 
	@MeasurementGUID VARCHAR(40) 
)
AS
BEGIN
	BEGIN TRY  

			SELECT 
				CONVERT( DECIMAL(18,2) , ISNULL( AVG(DE) , 0 ) ) AS AVE_DE 
				, CONVERT( DECIMAL(18,2) , ISNULL( MAX(DE) , 0 ) ) AS MAX_DE 
				, CONVERT( DECIMAL(18,2) , ISNULL( STDEV(DE) , 0 ) ) AS STDEV_DE 
			FROM 
				CPPatchDataLab 
			WHERE MeasurementGUID = @MeasurementGUID
			
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

