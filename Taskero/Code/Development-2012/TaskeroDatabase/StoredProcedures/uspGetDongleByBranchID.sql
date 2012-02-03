IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDongleByBranchID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDongleByBranchID]
	END

GO

/*              
AUTHOR : Shivpal Jhala              
Created Date : July 15, 2008              
Purpose : To Retrieve the Dongle Data by BranchID.              
*/              
CREATE PROCEDURE [CPV].[uspGetDongleByBranchID]        
(      
	@BranchID NVARCHAR(1000),      
	@TaskeroUserID INT,  
	@RoleID TINYINT  
)      
AS       
BEGIN          
 BEGIN TRY  
   DECLARE @xmlDongle XML      
   SET @xmlDongle = @BranchID  
   SELECT @RoleID = RoleLevel from TaskeroUser,Roles where TaskeroUserID = @TaskeroUserID AND TaskeroUser.RoleID = Roles.RoleID 
 if @RoleID = 1  
 BEGIN  
   SELECT       
    CPDongle.CPDongleID ,CPDongle.DongleDescription ,CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial      
   FROM       
    ( SELECT xmlDongle.value('.', 'VARCHAR(20)') AS ID FROM @xmlDongle.nodes('/Branch/BranchID') d(xmlDongle)) as J , CPDongle      
   WHERE       
    CPDongle.BranchID = J.ID   
    --AND	isnull(CPDongle.IsUsed,1) = 1   
   ORDER BY      
    CPDongle.DongleDescription 
 END  
 ELSE  
 BEGIN  
   SELECT       
    CPDongle.CPDongleID ,CPDongle.DongleDescription ,(CPSerialNUM+' - '+DongleDescription) AS DongleSerial       
   FROM       
    ( SELECT xmlDongle.value('.', 'VARCHAR(20)') AS ID FROM @xmlDongle.nodes('/Branch/BranchID') d(xmlDongle)) as J , CPDongle , UserDongle      
   WHERE       
    CPDongle.BranchID = J.ID and 
	( UserDongle.TaskeroUserID = @TaskeroUserID and CPDongle.CPDongleID = UserDongle.CPDongleID ) AND
	isnull(CPDongle.IsUsed,1) = 1
	
    
   ORDER BY      
    CPDongle.DongleDescription        
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

