IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllBranch')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllBranch]
	END

GO

/*    
AUTHOR : Jitendra Makwana
Created Date : July 15 2008    
Purpose : To get all the Branch List.    
*/    
CREATE PROCEDURE [CPV].[uspGetAllBranch] 
AS      
BEGIN    
	BEGIN TRY    
		 /*------------- Get All Branch List----------------*/  
	   	SELECT   
	  		BranchID , CompanyID , BranchName , BranchLocation , PrimaryContactName 
			, Tel_countryCode , Tel_PhoneNo , PrimaryContactEmail , PrimaryContactCell 
			, (CASE ISNULL( IsActive , 1 ) WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) AS IsActive 
			,  (Tel_countryCode + ' ' + Tel_PhoneNo + ' ' + Tel_Extension) AS Telephone
	 	FROM   
	  		Branch
	 	ORDER BY   
	  		BranchName    
      
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


