IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTempRHDeviceData1')
	BEGIN
		DROP  Procedure [CPV].[uspGetTempRHDeviceData1]
	END

GO

/*  
AUTHOR : Maulik Dhorajia
Created Date : Aug 28 2008  
Purpose : To get the TempRH Device Data
*/  
CREATE PROCEDURE [CPV].[uspGetTempRHDeviceData1](@BranchIDs VARCHAR(4000)) 
AS   
BEGIN  
	BEGIN TRY  
		
		/*---------- Convert BranchIDs to XML ----------*/
		DECLARE @xmlBranchIDs XML
		SET @xmlBranchIDs = '<BranchIDs><BranchID>' + REPLACE( @BranchIDs , ',' , '</BranchID><BranchID>' ) + '</BranchID></BranchIDs>'		

		--SELECT xmlBranchIDs.value('.', 'INT') AS BranchID FROM @xmlBranchIDs.nodes('/BranchIDs/BranchID') d(xmlBranchIDs)
		IF(@BranchIDs != '')
		BEGIN
			/*---------- Gets the DataTable ----------*/
			SELECT 
				TempRHDevice.TempRhDeviceID
				, ISNULL(TempRHDevice.BranchID,0) AS BranchID
				, TempRHDevice.TRHDeviceSerialNum
				, TempRHDevice.TRHDeviceTypeID
				, TempRHDevice.TargetTemp
				, TempRHDevice.TargetRH
				, TempRHDevice.TRHDeviceTypeID
				,  ISNULL(Branch.CompanyID,0) as CompanyID
				,TempRHDevice.TempRHDeviceID , Branch.BranchName , TempRHDeviceType.TRHDeviceTypeDesc 
			FROM TempRHDevice 
			LEFT OUTER JOIN Branch ON Branch.BranchID = TempRHDevice.BranchID
			LEFT OUTER JOIN TempRHDeviceType ON TempRHDeviceType.TRHDeviceTypeID = TempRHDevice.TRHDeviceTypeID
			WHERE ISNULL(Branch.BranchID,0) IN (SELECT xmlBranchIDs.value('.', 'INT') AS BranchID FROM @xmlBranchIDs.nodes('/BranchIDs/BranchID') d(xmlBranchIDs))
		END
		BEGIN
			SELECT 
			TempRHDevice.TempRhDeviceID
			, ISNULL(TempRHDevice.BranchID,0) AS BranchID
			, TempRHDevice.TRHDeviceSerialNum
			, TempRHDevice.TRHDeviceTypeID
			, TempRHDevice.TargetTemp
			, TempRHDevice.TargetRH
			, TempRHDevice.TRHDeviceTypeID
			,  ISNULL(Branch.CompanyID,0) as CompanyID
				,TempRHDevice.TempRHDeviceID , Branch.BranchName , TempRHDeviceType.TRHDeviceTypeDesc 
			FROM TempRHDevice 
			LEFT OUTER JOIN Branch ON Branch.BranchID = TempRHDevice.BranchID
			LEFT OUTER JOIN TempRHDeviceType ON TempRHDeviceType.TRHDeviceTypeID = TempRHDevice.TRHDeviceTypeID
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


