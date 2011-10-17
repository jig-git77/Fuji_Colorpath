IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllDevice')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllDevice] 
	END

GO

/*  
AUTHOR : Tushar Patel
Created Date : July 17 2008  
Purpose : To Get all the Device.  
*/  

CREATE PROCEDURE [CPV].[uspGetAllDevice] 
AS    
BEGIN  
 BEGIN TRY  
  
	/*------------- Get All Device List----------------*/  	
	SELECT 
		CPDevice.CPDeviceID ,	
		Company.CompanyID , 
		Company.CompanyName , 
		Branch.BranchID ,
		Branch.BranchName ,
		CPDongle.CPDongleID,
		CPDongle.CPSerialNUM,
		CPDevice.CPDeviceDesc ,
		CPMeasurementType.CPMeasurementTypeID , 
		CPMeasurementType.CPMeasurementType , 
		CPDeviceType.CPDeviceTypeID ,
		CPDeviceType.CPDeviceTypeDesc	
	FROM 
		Company 
			INNER JOIN Branch ON Company.CompanyID = Branch.CompanyID
			INNER JOIN CPDongle ON CPDongle.BranchID = Branch.BranchID
			INNER JOIN CPDevice ON CPDongle.CPDongleID = CPDevice.CPDongleID
			INNER JOIN CPMeasurementType ON CPDevice.MeasurementTypeID = CPMeasurementType.CPMeasurementTypeID
			INNER JOIN CPDeviceType ON CPDevice.CPDeviceTypeID = CPDeviceType.CPDeviceTypeID
    
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



