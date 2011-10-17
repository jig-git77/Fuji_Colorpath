IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCompanyBrandingData')
	BEGIN
		DROP  Procedure [CPV].[uspGetCompanyBrandingData]
	END

GO

/*              
AUTHOR : Amit Patel              
Created Date : Aug 14, 2008              
Purpose : To Retrieve the Dongle Data by BranchID.              
*/              
CREATE PROCEDURE [CPV].[uspGetCompanyBrandingData](@TaskeroUserID INT)
AS       
BEGIN          
 BEGIN TRY  
   
   DECLARE @RoleID INT
   
   SELECT @RoleID = RoleID FROM TaskeroUser WHERE TaskeroUserID = @TaskeroUserID
   
   IF @RoleID = 1 
	   BEGIN
	   
			SELECT       
				CompanyBranding.BrandID,BrandName,Css,HeaderLogo,LargeImage,CreatedDate,ModifiedDate 
				, TopLine
				, MainTabMenuText, MainTabMenuTextOver, MainTabMenuBack, MainTabMenuBackOver, MainTabMenuBorder, MainTabMenuBorderOver
				, MenuBottomLine
				, SubTabMenuText, SubTabMenuTextOver
				, MainForm	
				, Footer, FooterText	
				, Company.CompanyID , Company.CompanyName
				, PopUpTabMainColor , PopUpTabText , PopUpTabSelectedText
			FROM       
				CompanyBranding				
				LEFT OUTER JOIN Company ON Company.CompanyID = CompanyBranding.CompanyID
			ORDER BY      
				CompanyBranding.BrandName  
	
		END
	ELSE
		BEGIN
	   
			SELECT       
				CompanyBranding.BrandID,BrandName,Css,HeaderLogo,LargeImage,CreatedDate,ModifiedDate 
				, TopLine
				, MainTabMenuText, MainTabMenuTextOver, MainTabMenuBack, MainTabMenuBackOver, MainTabMenuBorder, MainTabMenuBorderOver
				, MenuBottomLine
				, SubTabMenuText, SubTabMenuTextOver
				, MainForm	
				, Footer, FooterText	
				, Company.CompanyID , Company.CompanyName
				, PopUpTabMainColor , PopUpTabText , PopUpTabSelectedText
			FROM       
				CompanyBranding
				INNER JOIN UserCompany ON UserCompany.CompanyID = CompanyBranding.CompanyID
				LEFT OUTER JOIN Company ON Company.CompanyID = CompanyBranding.CompanyID
			WHERE
				UserCompany.TaskeroUserID = @TaskeroUserID
			ORDER BY      
				CompanyBranding.BrandName  
	
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

