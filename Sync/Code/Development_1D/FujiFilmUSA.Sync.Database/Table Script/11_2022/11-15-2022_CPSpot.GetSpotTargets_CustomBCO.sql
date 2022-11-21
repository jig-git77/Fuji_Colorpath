/****** Object:  StoredProcedure [CPSpot].[GetSpotTargets_CustomBCO]    Script Date: 11/16/2022 1:18:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trushant Mehta
-- Create date: 2022-11-15
-- Description:	List the Custom BCO List
-- =============================================
/*
EXEC CpSpot.GetSpotTargets_CustomBCO 0,1

*/
CREATE PROCEDURE [CPSpot].[GetSpotTargets_CustomBCO] (
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
		,st.Description
		,ISNULL(c.CompanyID, 0) AS CompanyID
		,ISNULL(c.CompanyName, '') AS CompanyName
		,IsUsed
		,IsUsed
		,ISNULL(stl.NumberOfColors,0) NumberOfColors
	FROM CPSpot.SpotTarget st
	LEFT JOIN CP.Company c ON st.CompanyID = c.CompanyID
	LEFT OUTER JOIN CP.UserCompany UC ON UC.CompanyID = C.CompanyID
	LEFT OUTER JOIN [CP].[User] U ON U.UserID = UC.CompanyID
	LEFT OUTER JOIN [CP].[Role] RL ON RL.RoleID = U.RoleID
	LEFT OUTER JOIN ( SELECT COUNT(*) NumberOfColors , SpotTargetID FROM [CPSpot].[SpotTargetLAB] GROUP BY SpotTargetID ) stl ON st.SpotTargetID = stl.SpotTargetID
	WHERE CP.CheckUserCompany(@CurrentUserID, C.CompanyID) = 1
		AND CP.CheckUserRegion(@CurrentUserID, C.RegionID) = 1
		AND CP.CheckUserCompany(@CurrentUserID, UC.CompanyID) = 1
		AND (
			c.CompanyID <> 0
			OR @RoleLevel = 0
			)
		AND	IsCustom = 1
END
