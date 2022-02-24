

ALTER PROCEDURE [CP].[GetLoginInformation]    --'APlummer','Em3r9y5D8NcbWSTzDHMeTdeMRps=',0,'dsfd'
(   
	@LoginID NVARCHAR(50) ,     
	@LoginPass NVARCHAR(40),
	@UserType TINYINT,
	@AuthToken NVARCHAR(200)
)   
AS   
	DECLARE @UserID INT
	SELECT
		@UserID = UserID
	FROM
		[CP].[User] 
	WHERE
		LoginID = @LoginID AND
		LoginPass = @LoginPass AND
		ISNULL(IsActive,0) = 1 AND
		ISNULL(IsActivated,1) = 1 AND --TODO: make IsActivated by default 0 in ISNULL function
		ISNULL(UserType,0) = @UserType --TODO: make usertype by default 0 in ISNULL function
		
	UPDATE [CP].[User] SET APIAuthKey = @AuthToken WHERE UserID = @UserID
	
	SELECT     
		UserID
		,LoginID  
		--,LoginPass  
		,LastName  
		,FirstName  
		,Email  
		,RoleID  
		,RegionID  
		,LastModifiedUserID  
		,LastModifiedDate  
		,IsActive
		,AuthenticationKey
		,SecurityQuestionID
		,SecurityAnswer
		,IsActivated
		,UserType
		,LanguageCultureCode
		,MeasurementDeviceID
		,DensityType
		,UVMode
		,APIAuthKey
		,IsUpdateAlert
		,IsAlertPopup
	FROM
		CP.[User]
	WHERE
		UserID = @UserID

	SELECT 
		P.PermissionID, 
		ISNULL((SELECT 
			TOP 1 Case When RP.RolePermissionID IS NOT NULL Then 1 End
			FROM CP.[User] U INNER JOIN CP.RolePermission RP ON RP.RoleID = U.RoleID Where U.UserID = @UserID AND RP.PermissionID = P.PermissionID),0) As Allow
	FROM CP.Permission P
	
	SELECT UC.CompanyID,C.CompanyName FROM CP.UserCompany UC INNER JOIN CP.Company C ON UC.CompanyID = C.CompanyID WHERE UserID = @UserID
	
	SELECT UB.BranchID,B.BranchName FROM CP.UserBranch UB INNER JOIN CP.Branch B ON UB.BranchID = B.BranchID WHERE UserID = @UserID

	----Update User last Active date as Today date when user login everytimes------
	UPDATE [CP].[User] SET LastActiveDate = GETDATE()			
		WHERE UserID = @UserID





