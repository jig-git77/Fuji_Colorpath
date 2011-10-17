IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetProofAndPlateByTaskeroUserID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetProofAndPlateByTaskeroUserID]
	END

GO

/*  
AUTHOR : Maulik Dhorajia
LAST UPDATED DATE : Aug 14 , 2008
PURPOSE :- Proof And Plate For TaskeroUserID
*/  
CREATE Procedure [CPV].[uspGetProofAndPlateByTaskeroUserID]( @TaskeroUserID INT )
AS
BEGIN 

	/*---------- Gets the date ----------*/
	DECLARE @Now DATETIME
	SELECT @Now = GETDATE()

	SELECT
		CPDevice.CPDeviceID
		, CPDeviceStatus.CPDeviceStatusTypeID
		, StatusDesc 
		--, DATEDIFF( HH , StatusDT , GETDATE() ) AS Age
		, CONVERT( VARCHAR , DATEDIFF( MI , StatusDT , @Now ) / 60 ) +':'+ CONVERT( VARCHAR , DATEDIFF( MI , StatusDT , @Now ) % 60 ) AS Age
		, Company.CompanyName
		, Branch.BranchName
		, (CPDongle.CPSerialNUM +' - '+CPDongle.DongleDescription) AS WorkStation
		, CPDevice.CPDeviceDesc
	FROM 
		CPDevice	 
		INNER JOIN CPDeviceStatus ON CPDeviceStatus.CPDeviceID = CPDevice.CPDeviceID 
		INNER JOIN CPDeviceStatusType ON CPDeviceStatusType.CPDeviceStatusTypeID = CPDeviceStatus.CPDeviceStatusTypeID
		INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDevice.CPDongleID 
		INNER JOIN CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID AND ISNULL( IsUsed , 1 ) = 1
		INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID AND ISNULL( IsActive , 1 ) = 1
		INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
		INNER JOIN UserBranch ON UserBranch.BranchID = Branch.BranchID AND UserBranch.TaskeroUserID = UserDongle.TaskeroUserID
		INNER JOIN UserCompany ON UserCompany.CompanyID = Company.CompanyID AND UserCompany.TaskeroUserID = UserDongle.TaskeroUserID
	WHERE 	
		CPDeviceStatus.CPDeviceStatusID IN ( SELECT	MAX(CPDeviceStatusID) AS CPDeviceStatusID  FROM CPDeviceStatus GROUP BY CPDeviceID )
		AND UserDongle.TaskeroUserID = @TaskeroUserID
	ORDER BY
		CPDeviceStatus.CPDeviceStatusTypeID
	DESC

END
GO
