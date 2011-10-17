IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetAllDevicesPerUsers')
	BEGIN
		DROP  View [CPV].[vwGetAllDevicesPerUsers]
	END
GO
/*
	Created By : Maulik Dhorajia
	Created Date : Sept 18 2008
	Description : This will get us all the information on devices associated to a user.
*/
CREATE View [CPV].[vwGetAllDevicesPerUsers] AS

	SELECT     TaskeroUser.TaskeroUserID, ISNULL(TaskeroUser.RoleID, 0) AS RoleID, TaskeroUser.LoginID, TaskeroUser.Customer, 
                      Company.CompanyID, Company.CompanyName, Branch.BranchID, Branch.BranchName, CPDongle.CPDongleID, 
                      CPDongle.CPSerialNUM, CPDongle.DongleDescription, CPDevice.CPDeviceID, CPDevice.CPDeviceDesc, 
                      ISNULL(CPDevice.MeasurementTypeID, 1) AS MeasurementTypeID, 
                      (CASE CPDevice.MeasurementTypeID WHEN 2 THEN 'Plate' WHEN 3 THEN 'Press' ELSE 'Color' END) AS MeasurementTypeIDDesc, 
                      CPDevicePrintTarget.CPDevicePrintTargetID, CPDevicePrintTarget.PrintTarget, CPDeviceType.CPDeviceTypeID, 
                      CPDeviceType.CPDeviceTypeDesc
FROM         TaskeroUser INNER JOIN
                      UserCompany ON UserCompany.TaskeroUserID = TaskeroUser.TaskeroUserID INNER JOIN
                      UserBranch ON UserBranch.TaskeroUserID = TaskeroUser.TaskeroUserID INNER JOIN
                      UserDongle ON UserDongle.TaskeroUserID = TaskeroUser.TaskeroUserID INNER JOIN
                      Company ON Company.CompanyID = UserCompany.CompanyID INNER JOIN
                      Branch ON Branch.BranchID = UserBranch.BranchID AND Branch.CompanyID = Company.CompanyID INNER JOIN
                      CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID AND CPDongle.BranchID = Branch.BranchID INNER JOIN
                      CPDevice ON CPDevice.CPDongleID = CPDongle.CPDongleID INNER JOIN
                      CPDevicePrintTarget ON CPDevicePrintTarget.DeviceID = CPDevice.CPDeviceID INNER JOIN
                      CPDeviceType ON CPDeviceType.CPDeviceTypeID = CPDevice.CPDeviceTypeID
WHERE     (ISNULL(TaskeroUser.IsActive, 1) = 1) AND (ISNULL(Branch.IsActive, 1) = 1) AND (ISNULL(CPDongle.IsUsed, 1) = 1)

GO
