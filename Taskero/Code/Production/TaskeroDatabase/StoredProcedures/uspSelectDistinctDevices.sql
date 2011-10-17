IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspSelectDistinctDevices')
	BEGIN
		DROP  Procedure  [CPV].[uspSelectDistinctDevices]
	END

GO

/*          
AUTHOR : Jitendra Makwana
Created Date : September 19, 2008          
Purpose : To Retrieve the distict Device  Data
*/

CREATE Procedure [CPV].[uspSelectDistinctDevices]
(  
	@TaskeroUserID INT , 
	@RoleID INT,
	@CompanyID INT,
	@BranchID INT
)
AS
BEGIN      
	BEGIN TRY   
		IF  @RoleID = 1    /*--------- @RoleID = 1 = Tasker Admin ----------*/
		BEGIN     
			Select 
				Distinct CPDevice.CPDeviceDesc , CPDevice.CPDeviceID  
			From 
				CPDevice
			LEFT OUTER JOIN 
				CPDongle ON CPDongle.CPDongleID = CPDevice.CPDongleID
			LEFT OUTER JOIN 
				Branch ON Branch.BranchID = CPDongle.BranchID
			LEFT OUTER JOIN 
				Company ON Company.CompanyID = Branch.CompanyID
			Where 
				Company.CompanyID = @CompanyID 
			AND 
				Branch.BranchID = @BranchID
		END
		ELSE
		BEGIN
			Select 
				Distinct CPDevice.CPDeviceDesc , CPDevice.CPDeviceID  
			From 
				CPDevice
			LEFT OUTER JOIN 
				CPDongle ON CPDongle.CPDongleID = CPDevice.CPDongleID
			LEFT OUTER JOIN 
				Branch ON Branch.BranchID = CPDongle.BranchID
			LEFT OUTER JOIN 
				Company ON Company.CompanyID = Branch.CompanyID
			Where 
				Company.CompanyID  IN(Select CompanyId From UserCompany Where TaskeroUserID = @TaskeroUserID And CompanyID IN(@CompanyID))
			AND 
				Branch.BranchID IN(Select BranchID From UserBranch Where TaskeroUserID = @TaskeroUserID And BranchID IN(@BranchID))
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
GO

