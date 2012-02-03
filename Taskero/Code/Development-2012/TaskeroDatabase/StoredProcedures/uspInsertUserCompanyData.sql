IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertUserCompanyData')
	BEGIN
		DROP  Procedure [CPV].[uspInsertUserCompanyData]
	END

GO

/*    
AUTHOR : Jitendra Makwana  
Created Date : July 28 2008    
Purpose : Insert branch data into the Userbranch data  
*/  
  
CREATE PROCEDURE [CPV].[uspInsertUserCompanyData] 
(
	@TaskeroUserID INT,
	@SelectedIDs VARCHAR(4000),
	@NonSelectedIDs VARCHAR(4000) , 
	@Result INT = 0 OUTPUT
)
AS
BEGIN  
	BEGIN TRY

		/*---------- ASSIGN SELECTED IDs TO XML ----------*/
		IF (@SelectedIDs != '')
		BEGIN
				DECLARE @xmlCompany XML  
				SET @xmlCompany = '<CompanyIDs><CompanyID>'+ REPLACE( @SelectedIDs ,',','</CompanyID><CompanyID>') +'</CompanyID></CompanyIDs>' 

				--************************  Inserting into UserDongle table with TaskeroUserID as ref Key  ************************--  
				INSERT INTO 
						UserCompany
							SELECT   
									@TaskeroUserID , xmlCompany.value('.', 'INT') AS ID   
							FROM   
									@xmlCompany.nodes('/CompanyIDs/CompanyID') d(xmlCompany)
							WHERE
									xmlCompany.value('.', 'INT') NOT IN
														(
															SELECT 
																	CompanyID 
															FROM 
																	UserCompany
															WHERE 
																	TaskeroUserID = @TaskeroUserID
														)
		END		
		
		IF (@NonSelectedIDs != '')
		BEGIN
				/*---------- ASSIGN NON SELECTED IDs TO XML ----------*/
				DECLARE @xmlNonSelectedIDs XML	
				SET @xmlNonSelectedIDs = '<CompanyIDs><CompanyID>'+ REPLACE( @NonSelectedIDs ,',','</CompanyID><CompanyID>') +'</CompanyID></CompanyIDs>' 

				/*------------- Delete all the records from user Dongle where dongle ids exist in non selected list ----------------*/ 
				DELETE FROM 
						UserCompany
				WHERE
						TaskeroUserID = @TaskeroUserID 
				AND		
						CompanyID 
				IN
				(
					SELECT   
							xmlNonSelectedIDs.value('.', 'INT') AS ID   
					FROM   
							@xmlNonSelectedIDs.nodes('/CompanyIDs/CompanyID') d(xmlNonSelectedIDs)
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