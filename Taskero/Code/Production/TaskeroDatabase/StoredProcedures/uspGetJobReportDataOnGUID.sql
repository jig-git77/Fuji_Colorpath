IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetJobReportDataOnGUID')
	BEGIN
		DROP  Procedure [CPV].[uspGetJobReportDataOnGUID]
	END

GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 07/21/2008
-- Description:	Job Report on the basis of CPMeasurement.CPMeasurementGUID.
-- =============================================
CREATE PROCEDURE [CPV].[uspGetJobReportDataOnGUID]
( 
	@CPMeasurementGUID UNIQUEIDENTIFIER
)
AS
BEGIN
	BEGIN TRY  

			SELECT 
				CompanyName , BranchName , DongleDescription , CPDeviceDesc , PrintTarget
				, Job , Customer
				, CreatedDT , CONVERT( VARCHAR , CreatedDT , 101 ) as [Date] 
				, CONVERT( VARCHAR , CreatedDT , 108 ) as [TIME] 
				, Status
			FROM CPDongle  	
			INNER JOIN CPDevice ON CPDongle.CPDongleID = CPDevice.CPDongleID  	
			INNER JOIN CPDeviceType ON CPDeviceType.CPDeviceTypeID = CPDevice.CPDeviceTypeID 	
			INNER JOIN CPMeasurement ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID 	
			INNER JOIN CPMeasurement_PT ON CPMeasurement_PT.CPMeasurementGUID = CPMeasurement.CPMeasurementGUID
			INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID
			INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID	
			INNER JOIN Company ON Company.CompanyID = Branch.CompanyID 
			WHERE 
				CPMeasurement.CPMeasurementGUID = @CPMeasurementGUID
	
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

