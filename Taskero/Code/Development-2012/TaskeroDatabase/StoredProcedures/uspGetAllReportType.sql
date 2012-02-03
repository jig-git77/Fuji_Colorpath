IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllReportType')
	BEGIN
		DROP  Procedure  [CPV].[uspGetAllReportType]
	END

GO

/*  
AUTHOR : Amit Patel
Created Date : July 22 2008  
Purpose : To Get All The Report Types.  
*/  

CREATE PROCEDURE [CPV].[uspGetAllReportType] 
AS    
BEGIN  
 BEGIN TRY  
  
	/*------------- Get All The Report Types----------------*/  	
	SELECT 
		ReportTypeID, ReportTypeDesc 
	FROM 
		ReportType
    
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

