IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobInformation')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobInformation]
	END

GO

CREATE Procedure [Press].[uspGetPressJobInformation]( @JobID INT , @DongleID VARCHAR(10) ) AS
BEGIN
	BEGIN TRY

		SELECT 
			JobName
			, JobCreatedDt 
			, CPDevice.CPDeviceID
			, CPDevice.CPDeviceDesc	
			, CPDongle.CPDongleID
			, (CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END) AS Workstation
			, Branch.BranchID
			, Branch.BranchName
			, Company.CompanyID
			, Company.CompanyName			
			, 'NA' as Customer
			,  PJobs.DeltaETypeId 
			,  Press.vwDeltaEType.DeltaETypeValue
			, Substrate
			, PIOT.IllumObsTypeId 
			, PIOT.IllumObsType
			, 'NA' as SalesPerson
			, 'NA' as Standard
			, 'NA' as Inks
		FROM Press.Jobs PJobs
		INNER JOIN Press.PressInfo PPressInfo ON PPressInfo.PressID = PJobs.PressID 
		INNER JOIN CPDevice ON CPDevice.CPDeviceID = PPressInfo.CPDeviceID
		INNER JOIN CPDongle ON CPDongle.CPDongleID = CPDevice.CPDongleID
		INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID
		INNER JOIN Company ON Company.CompanyID = Branch.CompanyID
		INNER JOIN Press.IllumObsType PIOT ON PIOT.IllumObsTypeId = PJobs.IllumObsTypeId
		LEFT OUTER JOIN Press.vwDeltaEType ON  Press.vwDeltaEType.DeltaETypeId = PJobs.DeltaETypeId
		WHERE JOBID = @JobID AND DongleID = @DongleID ;

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

