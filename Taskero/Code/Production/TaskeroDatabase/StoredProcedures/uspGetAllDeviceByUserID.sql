IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllDeviceByUserID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetAllDeviceByUserID] 
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 25 2008  
Purpose : To Get all the Device by user id.
*/ 

CREATE PROCEDURE [CPV].[uspGetAllDeviceByUserID] 
(
	@TaskeroUserID INT,
	@RoleID INT
)
AS    
BEGIN  
	BEGIN TRY  
			IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)  
			BEGIN
				/*------------- Get All Device List----------------*/  	
				SELECT 
					CPDevice.CPDeviceID ,	
					Company.CompanyID , 
					Company.CompanyName , 
					Branch.BranchID ,
					Branch.BranchName ,
					CPDongle.CPDongleID,
					CPDongle.CPSerialNUM,
					CPDevice.CPDeviceDesc ,
					CPMeasurementType.CPMeasurementTypeID , 
					ISNULL( CPMeasurementType.CPMeasurementType , '' ) AS CPMeasurementType ,
					CPDeviceType.CPDeviceTypeID ,
					ISNULL( CPDeviceType.CPDeviceTypeDesc , ' ') AS CPDeviceTypeDesc	
				FROM 
					Company
						INNER JOIN Branch ON Company.CompanyID = Branch.CompanyID
						INNER JOIN CPDongle ON CPDongle.BranchID = Branch.BranchID
						INNER JOIN CPDevice ON CPDongle.CPDongleID = CPDevice.CPDongleID
						LEFT OUTER JOIN CPMeasurementType ON CPDevice.MeasurementTypeID = CPMeasurementType.CPMeasurementTypeID
						LEFT OUTER JOIN CPDeviceType ON CPDevice.CPDeviceTypeID = CPDeviceType.CPDeviceTypeID
			END
			ELSE
			BEGIN
					/*------------- Get All Device List----------------*/  	
				SELECT 
					CPDevice.CPDeviceID ,	
					Company.CompanyID , 
					Company.CompanyName , 
					Branch.BranchID ,
					Branch.BranchName ,
					CPDongle.CPDongleID,
					CPDongle.CPSerialNUM,
					CPDevice.CPDeviceDesc ,
					CPMeasurementType.CPMeasurementTypeID , 
					ISNULL( CPMeasurementType.CPMeasurementType , '' ) AS CPMeasurementType , 
					CPDeviceType.CPDeviceTypeID ,
					ISNULL( CPDeviceType.CPDeviceTypeDesc , ' ') AS CPDeviceTypeDesc
				FROM 
					Company
						INNER JOIN Branch ON Company.CompanyID = Branch.CompanyID
						INNER JOIN CPDongle ON CPDongle.BranchID = Branch.BranchID
						INNER JOIN CPDevice ON CPDongle.CPDongleID = CPDevice.CPDongleID
						LEFT OUTER JOIN CPMeasurementType ON CPDevice.MeasurementTypeID = CPMeasurementType.CPMeasurementTypeID
						LEFT OUTER JOIN CPDeviceType ON CPDevice.CPDeviceTypeID = CPDeviceType.CPDeviceTypeID
				WHERE
					Company.CompanyID IN
					(
						SELECT     
							Company.CompanyID 
						FROM     
							Company , UserCompany    
						WHERE     
							UserCompany.CompanyID = Company.CompanyID    
							and UserCompany.TaskeroUserID = @TaskeroUserID
					)
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
