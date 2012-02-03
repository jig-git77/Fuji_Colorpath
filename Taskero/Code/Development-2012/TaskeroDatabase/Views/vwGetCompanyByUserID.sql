IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetCompanyByUserID')
	BEGIN
		DROP  View [CPV].[vwGetCompanyByUserID]
	END
GO

CREATE View [CPV].[vwGetCompanyByUserID] AS
	SELECT 
		DISTINCT 
		Company.CompanyID
		,UserCompany.TaskeroUserID
		,Company.CompanyName
	FROM 
		Company
	LEFT OUTER JOIN UserCompany ON Company.CompanyID = UserCompany.CompanyID 