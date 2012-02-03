IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllCompanyByUserID')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllCompanyByUserID]
	END

GO

/*    
AUTHOR : Jitendra Makwana
Created Date : July 28 2008    
Purpose : To get all the Company List.    
*/    
CREATE PROCEDURE [CPV].[uspGetAllCompanyByUserID]
(
	@TaskeroUserID INT,
	@RoleID INT
)
AS      
BEGIN    
	BEGIN TRY
		IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)  
		BEGIN 
			/*---------- Gets All Company By UserID----------*/
			SELECT     
				Company.CompanyID 
				, Company.CompanyName
				,ISNULL( CompanyBranding.BrandName, 'Default' ) AS BrandName
				,ISNULL( Company.BrandID, 0 )AS BrandID
			FROM     
				Company 
				LEFT OUTER JOIN CompanyBranding ON ISNULL( Company.BrandID, 0 ) = ISNULL( CompanyBranding.BrandID, 0 )
			ORDER BY
				CompanyName
		END
		ELSE
		BEGIN
			/*---------- Gets All Company By UserID----------*/
			SELECT     
				Company.CompanyID 
				, Company.CompanyName
				,ISNULL( CompanyBranding.BrandName, 'Default' ) AS BrandName
				,ISNULL( Company.BrandID, 0 )AS BrandID
			FROM     
				Company    
				LEFT OUTER JOIN CompanyBranding ON ISNULL( Company.BrandID, 0 ) = ISNULL( CompanyBranding.BrandID, 0 )
				LEFT OUTER JOIN UserCompany ON UserCompany.CompanyID = Company.CompanyID 
			WHERE     
				UserCompany.TaskeroUserID = @TaskeroUserID		
			ORDER BY
				CompanyName
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
