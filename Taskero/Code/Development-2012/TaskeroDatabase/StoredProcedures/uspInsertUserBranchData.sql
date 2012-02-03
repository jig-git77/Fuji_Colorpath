IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertUserBranchData')
	BEGIN
		DROP  Procedure [CPV].[uspInsertUserBranchData]
	END

GO

/*    
AUTHOR : Jitendra Makwana  
Created Date : July 28 2008    
Purpose : Insert branch data into the Userbranch data  
*/  
  
CREATE PROCEDURE [CPV].[uspInsertUserBranchData]   
(  
 @TaskeroUserID INT,  
 @SelectedIDs VARCHAR(4000),  
 @NonSelectedIDs VARCHAR(4000) , 
 @Result INT = 0 OUTPUT
)  
AS      
BEGIN    
 BEGIN TRY  
		IF (@SelectedIDs != '')
		BEGIN
			  /*---------- ASSIGN SELECTED IDs TO XML ----------*/  
			  DECLARE @xmlBranch XML    
			  SET @xmlBranch = '<BranchIDs><BranchID>'+ REPLACE( @SelectedIDs ,',','</BranchID><BranchID>') +'</BranchID></BranchIDs>'   
			  
			  --************************  Inserting into UserBranch table with TaskeroUserID as ref Key  ************************--    
			  INSERT INTO   
				UserBranch     
				 SELECT     
				   @TaskeroUserID , xmlBranch.value('.', 'INT') AS ID     
				 FROM     
				   @xmlBranch.nodes('/BranchIDs/BranchID') d(xmlBranch)  
				 WHERE  
				   xmlBranch.value('.', 'INT') NOT IN  
						(  
						 SELECT   
						   BranchID   
						 FROM   
						   UserBranch   
						 WHERE   
						   TaskeroUserID = @TaskeroUserID  
						) 
		END
		IF (@NonSelectedIDs != '')
		BEGIN
			  /*---------- ASSIGN NON SELECTED IDs TO XML ----------*/  
			  DECLARE @xmlNonSelectedIDs XML   
			  SET @xmlNonSelectedIDs = '<BranchIDs><BranchID>'+ REPLACE( @NonSelectedIDs ,',','</BranchID><BranchID>') +'</BranchID></BranchIDs>'   
			  
			  /*------------- Delete all the records from user branch which ids exist in non selected list ----------------*/   
			  DELETE FROM   
				UserBranch  
			  WHERE  
				TaskeroUserID = @TaskeroUserID   
			  AND    
				BranchID   
			  IN  
			  (  
			   SELECT     
				 xmlNonSelectedIDs.value('.', 'INT') AS ID     
			   FROM     
				 @xmlNonSelectedIDs.nodes('/BranchIDs/BranchID') d(xmlNonSelectedIDs)  
			  )  
		END
		SET @Result = 1

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