/****** Object:  StoredProcedure [CPSpot].[GetSpotTargets]    Script Date: 11/15/2022 2:10:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-05-22
-- Description:	List the spot targets 
-- =============================================
/*
EXEC CpSpot.GetSpotTargets 0,1

*/
ALTER PROCEDURE [CPSpot].[GetSpotTargets] (
	@UserID INT
	,@CurrentUserID INT
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RoleLevel TINYINT

	SELECT @RoleLevel = RoleLevel
	FROM [CP].[User] AS U
	INNER JOIN [CP].[Role] AS R ON U.RoleID = R.RoleID
	WHERE UserID = @CurrentUserID

	SELECT DISTINCT st.SpotTargetID
		,st.SpotTargetName
		,ISNULL(c.CompanyID, 0) AS CompanyID
		,ISNULL(c.CompanyName, '') AS CompanyName
		,IsUsed
	FROM CPSpot.SpotTarget st
	LEFT JOIN CP.Company c ON st.CompanyID = c.CompanyID
	LEFT OUTER JOIN CP.UserCompany UC ON UC.CompanyID = C.CompanyID
	LEFT OUTER JOIN [CP].[User] U ON U.UserID = UC.CompanyID
	LEFT OUTER JOIN [CP].[Role] RL ON RL.RoleID = U.RoleID
	WHERE CP.CheckUserCompany(@CurrentUserID, C.CompanyID) = 1
		AND CP.CheckUserRegion(@CurrentUserID, C.RegionID) = 1
		AND CP.CheckUserCompany(@CurrentUserID, UC.CompanyID) = 1
		AND (
			c.CompanyID <> 0
			OR @RoleLevel = 0
			)
		AND	IsCustom = 0
END
