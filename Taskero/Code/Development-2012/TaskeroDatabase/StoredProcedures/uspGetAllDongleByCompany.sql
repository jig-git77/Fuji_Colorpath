IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllDongleByCompany')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllDongleByCompany]
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 30 2008  
Purpose : To Get all the Dongle of company.  
*/  

CREATE PROCEDURE [CPV].[uspGetAllDongleByCompany] 
(
	@CompanyID INT
)
AS    
BEGIN  
 BEGIN TRY  
  
	/*------------- Get All Dongle List----------------*/  	
	SELECT CPDongleID , 
			CPSerialNUM ,
			ISNULL( DongleDescription ,'') AS DongleDescription , CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial
	FROM 
		CPDongle 
			LEFT OUTER JOIN Branch ON CPDongle.BranchID = Branch.BranchID
			LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
	WHERE
		Company.CompanyID = @CompanyID

	ORDER BY 
		CPSerialNUM
    
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
