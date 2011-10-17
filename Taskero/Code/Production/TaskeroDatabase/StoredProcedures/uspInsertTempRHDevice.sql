IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertTempRHDevice')
	BEGIN
		DROP  Procedure [CPV].[uspInsertTempRHDevice]
	END

GO

/*    
AUTHOR : Shivpal Jhala    
LAST UPDATED DATE : July 22 , 2008    
PURPOSE :- Inserting Data for TempRHDevice 
*/    
CREATE PROCEDURE [CPV].[uspInsertTempRHDevice]     
(     
	@BranchID INT,
	@TRHDeviceSerialNum	NVARCHAR(100),
	@TRHDeviceTypeID TINYINT,
	@TargetTemp DECIMAL(6, 2),
	@TargetRH DECIMAL(6, 2),
	@Device NVARCHAR(2000),
	@Result INT = 0 OUTPUT     
) 
AS     
BEGIN    
	BEGIN TRY    
		--************************  Duplicate Check for the TRHDeviceSerialNum  ************************--    
		if EXISTS (SELECT TempRHDeviceID FROM TempRHDevice WHERE TRHDeviceSerialNum = @TRHDeviceSerialNum )    
		BEGIN    
			--*** setting the @Result =1 if the duplicate TRHDeviceSerialNum is found***--    
			SET @Result = 1    
		END    
		ELSE    
		BEGIN       
			--************************  Inserting into TempRHDevice table  ************************--    
			INSERT INTO TempRHDevice    
			(    
			BranchID
			,TRHDeviceSerialNum
			,TRHDeviceTypeID
			,TargetTemp
			,TargetRH     
			)     
			VALUES     
			(    
			@BranchID
			,@TRHDeviceSerialNum
			,@TRHDeviceTypeID
			,@TargetTemp
			,@TargetRH
			)
			
			--************************  Fetch the last inserted identity id ************************--  
			DECLARE @TempRHDeviceID INT
			SET @TempRHDeviceID = @@IDENTITY
			
			--************************  Inserting into UserDongle table with TaskeroUserID as ref Key  ************************--  
			 DECLARE @xmlDevice XML  
			 SET @xmlDevice = @Device
		       
			 INSERT INTO TempRHDevice_CPDevice   
			 SELECT   
				xmlDevice.value('.', 'VARCHAR(20)') AS ID , @TempRHDeviceID
			 FROM @xmlDevice.nodes('/Device/DeviceID') d(xmlDevice)

			--*** setting the @Result =2 for the successfully insert of data***--    
			SET @Result = 2    
		END    	
	END TRY    
	BEGIN CATCH    
		--*** Error Handling***--    
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

