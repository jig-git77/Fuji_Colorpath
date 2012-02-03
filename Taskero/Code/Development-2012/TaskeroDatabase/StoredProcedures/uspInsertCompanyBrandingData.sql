IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertCompanyBrandingData')
	BEGIN
		DROP  Procedure  [CPV].[uspInsertCompanyBrandingData]
	END

GO

/*========== INSERT ==========*/  
/*  
AUTHOR : Amit Patel
LAST UPDATED DATE : Aug 14 , 2008  
PURPOSE :- Creates a new Branding Data  
*/  

CREATE Procedure [CPV].[uspInsertCompanyBrandingData]
	(
		@BrandName VARCHAR(50)
		,@Css VARCHAR(25)
		,@HeaderLogo VARCHAR(25)
		,@LargeImage VARCHAR(25)
		, @TopLine VARCHAR(7)
		, @MainTabMenuText VARCHAR(7)
		, @MainTabMenuTextOver VARCHAR(7)
		, @MainTabMenuBack VARCHAR(7)
		, @MainTabMenuBackOver VARCHAR(7)
		, @MainTabMenuBorder VARCHAR(7)
		, @MainTabMenuBorderOver VARCHAR(7)
		, @MenuBottomLine VARCHAR(7)
		, @SubTabMenuText VARCHAR(7)
		, @SubTabMenuTextOver VARCHAR(7)
		, @MainForm VARCHAR(7)
		, @Footer VARCHAR(7)
		, @FooterText VARCHAR(7)
		, @CompanyID INT
		, @PopUpTabMainColor VARCHAR(7)
		, @PopUpTabText VARCHAR(7)
		, @PopUpTabSelectedText VARCHAR(7)
		, @Result INT = 0 OUTPUT   
	)
AS
BEGIN  
 BEGIN TRY 
 
 INSERT INTO CompanyBranding  
    (  
     [BrandName]   
     , [Css]
     , [HeaderLogo]
     , [LargeImage]
     , [CreatedDate]
     , [ModifiedDate]
     , [TopLine]
	, [MainTabMenuText]
	, [MainTabMenuTextOver]
	, [MainTabMenuBack]
	, [MainTabMenuBackOver]
	, [MainTabMenuBorder]
	, [MainTabMenuBorderOver]
	, [MenuBottomLine]
	, [SubTabMenuText]
	, [SubTabMenuTextOver]	
	, [MainForm]
	, [Footer]
	, [FooterText]
	, [CompanyID]
	, [PopUpTabMainColor] 
	, [PopUpTabText] 
	, [PopUpTabSelectedText]	
    )   
    VALUES   
    (  
     @BrandName   
     , @Css   
     , @HeaderLogo   
     , @LargeImage
     , GETDATE()   
     , GETDATE()   
     , @TopLine
	, @MainTabMenuText
	, @MainTabMenuTextOver
	, @MainTabMenuBack
	, @MainTabMenuBackOver
	, @MainTabMenuBorder
	, @MainTabMenuBorderOver
	, @MenuBottomLine
	, @SubTabMenuText
	, @SubTabMenuTextOver
	, @MainForm
	, @Footer
	, @FooterText
	, @CompanyID
	, @PopUpTabMainColor 
	, @PopUpTabText 
	, @PopUpTabSelectedText
     )  
     
 IF @@ERROR <> 0
	SET @Result = 0
 ELSE
	SET @Result = 1
     
 END TRY  
 BEGIN CATCH  
  --*** Error Handling***--  
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
