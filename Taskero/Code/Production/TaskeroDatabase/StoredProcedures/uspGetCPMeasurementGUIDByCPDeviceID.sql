IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCPMeasurementGUIDByCPDeviceID')
	BEGIN
		DROP  Procedure [CPV].[uspGetCPMeasurementGUIDByCPDeviceID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 16 2008

UPDATED BY : Maulik Dhorajia
UPDATED DATE : July 22 2008

Purpose : This retrieves the CPMeasurementGuid and Status with DeviceID
*/
CREATE PROCEDURE [CPV].[uspGetCPMeasurementGUIDByCPDeviceID]
( @CPDeviceID INT , @TaskeroUserID INT , @RoleID INT ) 
AS  
BEGIN
	BEGIN TRY
		
		IF @RoleID = 1 OR  @RoleID = 0 /* Taskero Admin User */
			BEGIN 
				
				/*========== Gets all Meausrement If an admin user =========*/
				SELECT 
					CPMeasurementGUID 
					, Status
				FROM 
					CPMeasurement 
				WHERE 
					CPDeviceID = @CPDeviceID
				AND	ISNULL(IsUsed,1) = 1
				ORDER BY
					CreatedDT
			END
		ELSE IF @RoleID = 5 /* Print Buyer */
		
			BEGIN
			
				/*========== Gets all Meausrement if a user is print buyer =========*/	
				SELECT 
					CPMeasurement.CPMeasurementGUID
					, CPMeasurement.Status
				FROM
					CPMeasurement 
					INNER JOIN CPDevice ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID
					INNER JOIN CPDongle ON CPDongle.CPDongleID = CPMeasurement.CPDongleID AND ISNULL( CPDongle.IsUsed , 1 ) = 1
					INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDongle.CPDongleID AND UserDongle.TaskeroUserID = @TaskeroUserID
					INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID AND ISNULL( Branch.IsActive , 1 ) = 1
					INNER JOIN UserBranch ON UserBranch.BranchID = Branch.BranchID AND UserBranch.TaskeroUserID = @TaskeroUserID
					INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
					INNER JOIN UserCompany ON UserCompany.CompanyID = Branch.CompanyID AND UserCompany.TaskeroUserID = @TaskeroUserID
					INNER JOIN TaskeroUser ON TaskeroUser.Customer = CPMeasurement.Customer AND TaskeroUser.TaskeroUserID = @TaskeroUserID
				WHERE
					CPMeasurement.CPDeviceID = @CPDeviceID
					AND ISNULL( CPMeasurement.IsUsed , 1 ) = 1
				ORDER BY 
					CPMeasurement.CreatedDT
					
			END
			
		ELSE
			BEGIN
			
				/*========== Gets all Meausrement on basis of access =========*/	
				SELECT 
					CPMeasurement.CPMeasurementGUID
					, CPMeasurement.Status
				FROM
					CPMeasurement 
					INNER JOIN CPDevice ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID
					INNER JOIN CPDongle ON CPDongle.CPDongleID = CPMeasurement.CPDongleID AND ISNULL( CPDongle.IsUsed , 1 ) = 1
					INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDongle.CPDongleID AND UserDongle.TaskeroUserID = @TaskeroUserID
					INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID AND ISNULL( Branch.IsActive , 1 ) = 1
					INNER JOIN UserBranch ON UserBranch.BranchID = Branch.BranchID AND UserBranch.TaskeroUserID = @TaskeroUserID
					INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
					INNER JOIN UserCompany ON UserCompany.CompanyID = Branch.CompanyID AND UserCompany.TaskeroUserID = @TaskeroUserID
				WHERE
					CPMeasurement.CPDeviceID = @CPDeviceID
					AND ISNULL( CPMeasurement.IsUsed , 1 ) = 1					
				ORDER BY 
					CPMeasurement.CreatedDT
								
			END
		
	END TRY
	BEGIN CATCH
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

