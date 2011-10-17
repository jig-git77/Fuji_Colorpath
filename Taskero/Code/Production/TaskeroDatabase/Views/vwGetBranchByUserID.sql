IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetBranchByUserID')
	BEGIN
		DROP  View [CPV].[vwGetBranchByUserID]
	END
GO

CREATE View [CPV].[vwGetBranchByUserID] AS
	SELECT 
		DISTINCT 
		Branch.BranchID
		,UserBranch.TaskeroUserID
		,Branch.BranchName
	FROM 
		Branch
	LEFT OUTER JOIN UserBranch ON Branch.BranchID = UserBranch.BranchID