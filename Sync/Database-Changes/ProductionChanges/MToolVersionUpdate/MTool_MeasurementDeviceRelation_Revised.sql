
GO
/****** Object:  StoredProcedure [CP].[DeleteMeasurementDevice]    Script Date: 8/20/2020 3:52:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[DeleteMeasurementDevice]
 
 @MeasurementDeviceID		INT

AS
BEGIN
	
	BEGIN TRY
		BEGIN TRAN

		DECLARE @ProjCount INT;
		DECLARE @ProjCountV INT;
		DECLARE @ProjCountTotal INT;
		DECLARE @SerialNumber NVARCHAR(20);
		DECLARE @InstrumentTypeID	TINYINT;

		----------------Delete MeasurementDevice record from [CP].[MeasurementPluginStatus] table if there is any for this serial number-----------
		--SELECT @SerialNumber = [SerialNumber], @InstrumentTypeID = [InstrumentTypeID] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] = @MeasurementDeviceID
		--DELETE FROM [CP].[MeasurementPluginStatus] WHERE [MeaDeviceSerialNo] = @SerialNumber AND [InstrumentTypeID] = @InstrumentTypeID

		-------Get existing project details associated with this MeasurementDevice------------------
		SELECT @ProjCount = COUNT(*) FROM CPSync4D.MeasurementSetting
			WHERE MeasurementDeviceID = @MeasurementDeviceID OR VMeasurementDeviceID = @MeasurementDeviceID

		SELECT @ProjCountV = COUNT(*) FROM CPSyncV.MeasurementSetting
			WHERE VMeasurementDeviceID = @MeasurementDeviceID
		
		SET @ProjCountTotal = @ProjCount + @ProjCountV;

		IF @ProjCountTotal > 0  ----If there are projects associated with this, then inActivate otherwise delete it...
			BEGIN
				UPDATE CP.MeasurementDevice 
					SET IsActive = 0				
				WHERE MeasurementDeviceID = @MeasurementDeviceID
			END
		ELSE
			BEGIN
				DELETE FROM CP.MeasurementDevice 
				WHERE
					 MeasurementDeviceID = @MeasurementDeviceID
			END

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END

GO








GO
/****** Object:  StoredProcedure [CP].[InsertMeasurementDevice]    Script Date: 8/20/2020 3:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[InsertMeasurementDevice]  
   
 @LicenseID        INT  
,@SerialNumber     NVARCHAR(20)  
,@InstrumentTypeID TINYINT 
,@Description      NVARCHAR(100) 
,@IsActive         BIT  
  
AS  
BEGIN  
   
 IF EXISTS (SELECT * FROM CP.MeasurementDevice Where Upper(SerialNumber) = Upper(@SerialNumber) AND LicenseID = @LicenseID AND InstrumentTypeID = @InstrumentTypeID AND IsActive = 1)   
 --IF EXISTS (SELECT * FROM CP.MeasurementDevice Where Upper(SerialNumber) = Upper(@SerialNumber) AND InstrumentTypeID = @InstrumentTypeID AND IsActive = 1)   
		BEGIN    
			Return -1  
		END      
       
   
 BEGIN TRY  
  BEGIN TRAN  
  INSERT INTO CP.MeasurementDevice   
   (  
     LicenseID  
    ,SerialNumber  
    ,InstrumentTypeID  
	,[Description]
    ,IsActive  
   )  
  VALUES   
   (  
     @LicenseID  
    ,@SerialNumber  
    ,@InstrumentTypeID  
	,@Description
    ,@IsActive  
   )     
   COMMIT TRAN  
 END TRY  
 BEGIN CATCH  
  ROLLBACK TRAN  
  DECLARE @ERROR_NUMBER INT   
  DECLARE @ERROR_SEVERITY INT  
  DECLARE @ERROR_STATE INT   
  DECLARE @ERROR_PROCEDURE NVARCHAR(252)   
  DECLARE @ERROR_LINE INT   
  DECLARE @ERROR_MESSAGE NVARCHAR(4000)   
  SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()   
  EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE   
  RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/   
 END CATCH  
  
END
GO








GO
/****** Object:  StoredProcedure [CP].[UpdateMeasurementDevice]    Script Date: 8/20/2020 3:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[UpdateMeasurementDevice]

 @MeasurementDeviceID INT
,@LicenseID			INT
,@SerialNumber		NVARCHAR(20)
,@InstrumentTypeID	TINYINT
,@Description       NVARCHAR(100)
,@IsActive			BIT

AS
BEGIN
	
	IF EXISTS (SELECT * FROM CP.MeasurementDevice  Where MeasurementDeviceID <> @MeasurementDeviceID AND Upper(SerialNumber) = Upper(@SerialNumber) AND LicenseID = @LicenseID AND InstrumentTypeID = @InstrumentTypeID AND IsActive = 1)  
	--IF EXISTS (SELECT * FROM CP.MeasurementDevice  Where MeasurementDeviceID <> @MeasurementDeviceID AND Upper(SerialNumber) = Upper(@SerialNumber) AND InstrumentTypeID = @InstrumentTypeID AND IsActive = 1)  
		BEGIN  
			Return -1
		END

	BEGIN TRY
		BEGIN TRAN 
		
		 	UPDATE CP.MeasurementDevice 
			SET 
					 LicenseID=@LicenseID
					,SerialNumber=@SerialNumber
					,InstrumentTypeID=@InstrumentTypeID
					,[Description] = @Description
					,IsActive=@IsActive
			WHERE
					MeasurementDeviceID = @MeasurementDeviceID
			
			--------------Delete MeasurementDevice record from [CP].[MeasurementPluginStatus] table if there is any for this serial number once device is updated-----------
			DELETE FROM [CP].[MeasurementPluginStatus] WHERE [MeaDeviceSerialNo] = @SerialNumber AND [InstrumentTypeID] = @InstrumentTypeID

			COMMIT TRAN
	END TRY
	BEGIN CATCH
			ROLLBACK TRAN
	
	  DECLARE @ERROR_NUMBER INT   
	  DECLARE @ERROR_SEVERITY INT  
	  DECLARE @ERROR_STATE INT   
	  DECLARE @ERROR_PROCEDURE NVARCHAR(252)   
	  DECLARE @ERROR_LINE INT   
	  DECLARE @ERROR_MESSAGE NVARCHAR(4000)   
	  SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()   
	  EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE   
	END CATCH

	
END
GO





