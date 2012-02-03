IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTOP100CPMeasurements')
	BEGIN
		DROP  Procedure  [CPV].[uspGetTOP100CPMeasurements] 
	END

GO
/*  
AUTHOR : Maulik Dhorajia
Created Date : July 31 2008  
Purpose : To get the TempRH Device Data
*/  
CREATE PROCEDURE [CPV].[uspGetTOP100CPMeasurements] 
(
	@TaskeroUserID INT,
	@RoleID INT
)
AS   
BEGIN  
	/*---------- Only If Admin ----------*/
	BEGIN TRY
		IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)  
		BEGIN 
			/*---------- Gets All Company By UserID----------*/
			SELECT 
				TOP 100 
				CPMeasurementGUID , CPSerialNum , PrintTarget , MeasurementID 
				, CPDeviceDesc , CreatedDT , MeasurementType  
				, CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial
			FROM 
				CPV.vwAllCPMeasurement  
			ORDER BY 
				[TimeStamp] 
			DESC  	
		END
		ELSE
		BEGIN
			/*---------- Gets All Company By UserID----------*/
			SELECT 
				TOP 100 
				CPMeasurementGUID , CPSerialNum , PrintTarget , MeasurementID 
				, CPDeviceDesc , CreatedDT , MeasurementType  
				, CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial
			FROM 
				CPV.vwCPMeasrMentByUserID
			WHERE 
				TaskeroUserID = @TaskeroUserID
			ORDER BY 
				[TimeStamp] 
			DESC  	
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