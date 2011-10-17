IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetUsersBrandingInfo')
	BEGIN
		DROP  Procedure  [CPV].[uspGetUsersBrandingInfo]
	END

GO
/*
	Name : Maulik Dhorajia
	Date : Sept 24 2008
	Description : Get the information of the css.
*/
CREATE Procedure [CPV].[uspGetUsersBrandingInfo](@TaskeroUserID INT)
AS

	SELECT	
		TaskerouserID
		, Css , LogoPath1 , LogoPath2
		, TopLine
		, MainTabMenuText , MainTabMenuTextOver , MainTabMenuBack , MainTabMenuBackOver , MainTabMenuBorder , MainTabMenuBorderOver
		, MenuBottomLine
		, SubTabMenuText , SubTabMenuTextOver
		, MainForm
		, Footer , FooterText
	FROM Taskerouser
	WHERE TaskerouserID = @TaskerouserID

GO
