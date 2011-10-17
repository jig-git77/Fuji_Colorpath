IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateTaskeroUserBrandings')
	BEGIN
		DROP  Procedure  [CPV].[uspUpdateTaskeroUserBrandings]
	END

GO

CREATE Procedure [CPV].[uspUpdateTaskeroUserBrandings]
( 
	@TaskeroUserID INT 
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
)
AS
BEGIN  
 BEGIN TRY 

	IF @TaskeroUserID = 0 
	BEGIN
		SELECT @TaskeroUserID = MAX(TaskeroUserID) FROM TaskeroUser
	END

	UPDATE 
		TaskeroUser 
	SET
		 TopLine	= @TopLine
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
	WHERE
		TaskeroUserID = @TaskeroUserID
	
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
