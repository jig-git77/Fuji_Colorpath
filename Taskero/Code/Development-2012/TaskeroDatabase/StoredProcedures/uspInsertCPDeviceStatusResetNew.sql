IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertCPDeviceStatusResetNew')
	BEGIN
		DROP  Procedure  [CPV].[uspInsertCPDeviceStatusResetNew]
	END

GO
/*
AUTHOR : Maulik Dhorajia
Created Date : Aug 14 2008
Purpose : Onbehalf of Chirag Shah's procedure uspInsertCPDeviceStatusReset.
*/ 
CREATE Procedure [CPV].[uspInsertCPDeviceStatusResetNew]( @CPDeviceID INT )AS
BEGIN
	
	-- Turn NOCOUNT back ON
	 SET NOCOUNT ON  
  
	 DECLARE @DateTime datetime  
	 SET @DateTime = GETDATE()  
  
  
	INSERT INTO CPDeviceStatus ( CPDeviceID, CPDeviceStatusTypeID,OperatorReset, StatusDT) 
	VALUES (@CPDeviceID,1,1,@DateTime)  
   
 UPDATE CPDeviceStatusCount SET BadCount = 0 WHERE CPDeviceID = @CPDeviceID

 -- Turn NOCOUNT back OFF  
 SET NOCOUNT OFF  
	
END
GO