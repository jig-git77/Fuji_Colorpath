IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateCompanyBrandingData')
	BEGIN
		DROP  Procedure  [CPV].[uspUpdateCompanyBrandingData]
	END

GO

/*========== INSERT ==========*/  
/*  
AUTHOR : Amit Patel
LAST UPDATED DATE : Aug 14 , 2008  
PURPOSE :- Creates a new Branding Data  
*/  

CREATE Procedure [CPV].[uspUpdateCompanyBrandingData]
	(
		@BrandID int
		, @BrandName VARCHAR(50)
		, @Css VARCHAR(25) 
		, @HeaderLogo VARCHAR(25)
		, @LargeImage VARCHAR(25)
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
 
	 UPDATE
		CompanyBranding  
	SET
	   [BrandName] = @BrandName
	   , [Css] = @Css
	   , [HeaderLogo] = @HeaderLogo
	   , [LargeImage] = @LargeImage
	-- , [CreatedDate] = GETDATE()
	   , [ModifiedDate] = GETDATE()
		, TopLine	= @TopLine
		, MainTabMenuText	= @MainTabMenuText
		, MainTabMenuTextOver	= @MainTabMenuTextOver
		, MainTabMenuBack	= @MainTabMenuBack
		, MainTabMenuBackOver	= @MainTabMenuBackOver
		, MainTabMenuBorder	= @MainTabMenuBorder
		, MainTabMenuBorderOver	= @MainTabMenuBorderOver
		, MenuBottomLine	= @MenuBottomLine
		, SubTabMenuText	= @SubTabMenuText
		, SubTabMenuTextOver	= @SubTabMenuTextOver		
		, MainForm	= @MainForm
		, Footer	= @Footer
		, FooterText	= @FooterText
		, CompanyID	= @CompanyID
		, PopUpTabMainColor = @PopUpTabMainColor 
		, PopUpTabText = @PopUpTabText
		, PopUpTabSelectedText = @PopUpTabSelectedText
	WHERE
		[BrandID] = @BrandID
   
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
