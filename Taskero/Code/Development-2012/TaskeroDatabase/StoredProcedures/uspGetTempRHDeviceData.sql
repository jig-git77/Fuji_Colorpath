IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTempRHDeviceData')
	BEGIN
		DROP  Procedure [CPV].[uspGetTempRHDeviceData]
	END

GO

/*  
AUTHOR : Shivpal Jhala 
Created Date : July 22 2008  
Purpose : To get the TempRH Device Data
*/  
CREATE PROCEDURE [CPV].[uspGetTempRHDeviceData]
(
	@TempRHDeviceID smallint
) 
AS   
BEGIN  
	BEGIN TRY  
		/*----- Fetching the TempRHDevice Data-----*/  
		IF @TempRHDeviceID > 0 
		BEGIN
			SELECT 
				TempRHDevice.* ,  Branch.CompanyID
			FROM 
				TempRHDevice
				LEFT OUTER JOIN Branch ON Branch.BranchID = TempRHDevice.BranchID
			WHERE
				TempRHDevice.TempRHDeviceID = @TempRHDeviceID
				
			/*----- Fetching the TempRHDevice_CPDevice Data based on the TempRHDeviceID-----*/  
			Select 
				TempRHDevice_CPDeviceID , CPDeviceID 
			From 
				TempRHDevice_CPDevice 
			Where 
				TempRHDeviceID = @TempRHDeviceID
		END
		ELSE
		BEGIN
			SELECT 
				TempRHDevice.TempRHDeviceID , Branch.BranchName , TempRHDevice.TRHDeviceSerialNum , TempRHDeviceType.TRHDeviceTypeDesc , TempRHDevice.TargetTemp , TempRHDevice.TargetRH
			FROM 
				TempRHDevice
				LEFT OUTER JOIN Branch ON Branch.BranchID = TempRHDevice.BranchID
				LEFT OUTER JOIN TempRHDeviceType ON TempRHDeviceType.TRHDeviceTypeID = TempRHDevice.TRHDeviceTypeID
			ORDER BY 
				TempRHDevice.TRHDeviceSerialNum
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


