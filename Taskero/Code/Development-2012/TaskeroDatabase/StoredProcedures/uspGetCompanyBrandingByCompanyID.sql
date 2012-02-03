IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCompanyBrandingByCompanyID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetCompanyBrandingByCompanyID]
	END
GO

CREATE Procedure [CPV].[uspGetCompanyBrandingByCompanyID](@CompanyID INT)
AS
BEGIN

	SELECT 
		BrandID , BrandName
	FROM CompanyBranding
	WHERE CompanyID = @CompanyID

END