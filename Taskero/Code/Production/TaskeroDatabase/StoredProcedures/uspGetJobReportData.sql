IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetJobReportData')
	BEGIN
		DROP  Procedure [CPV].[uspGetJobReportData]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/17/2008
-- Modified By:	Maulik Dhorajia
-- Modified date: 07/22/2008
-- Description:	Job Report
-- =============================================
CREATE PROCEDURE [CPV].[uspGetJobReportData]
(
	@TaskeroUserID AS INT,
	@CompanyID AS INT,
	@Press as BIT
)
AS
BEGIN
	BEGIN TRY  
	
		/*---------- Declare Variables ----------*/
	    DECLARE @DIFFMIN INT 
		SELECT @DIFFMIN = DATEDIFF( MI , GETUTCDATE() , GETDATE())
		
		/*---------- Query For Colors ----------*/
		IF @Press = 0
		Begin
		SELECT 
			StatusID
			, Status
			, Job 
			, PrintTarget 
			, CPDeviceDesc 
			, DongleDescription
			, CPDeviceTypeDesc
			, CreatedDT
			, [DateTime]
			, [Date]
			, [TIME] 
			, [GUID] 
			, CPMeasurementID 
			, vwJRCP.Customer
			, CPDevicePrintTargetID
			, CPDeviceID
			, vwJRCP.CPDongleID
			, vwJRCP.BranchID
			, BranchName
			, vwJRCP.CompanyID
			, CompanyName
			, CPDeviceTypeID 
			, DongleSerial 
			, DATEADD( MI , @DIFFMIN , ISNULL(CreatedDT, '') )as [GMTDateTime] 
			, CPSerialNUM
		FROM CPV.vwGetJobReportsColorPlate vwJRCP 
		INNER JOIN TaskeroUser ON TaskeroUser.TaskeroUserID = @TaskeroUserID
		INNER JOIN UserDongle ON UserDongle.TaskeroUserID = TaskeroUser.TaskeroUserID
		WHERE vwJRCP.CPDongleID = UserDongle.CPDongleID
			AND 1 = ( CASE @CompanyID 
						WHEN 0 THEN 1 
						ELSE 
							CASE 
								WHEN CompanyID = @CompanyID THEN 1
								ELSE 0
							END
					  END
					)	
			AND 1 =  ( CASE ISNULL(TaskeroUser.RoleID , 0)
					WHEN 5 THEN
						CASE
							WHEN vwJRCP.Customer = TaskeroUser.Customer
							THEN 1
							ELSE 0
						END
					ELSE 
						1							 
					END			
				)
			AND CPDeviceTypeID  <> 5 Order By CreatedDT Desc
			End
		Else
		Begin			
		/*UNION*/
		
		/*---------- Query For Press ----------*/
		SELECT 
			StatusID
			, Status
			, Job 			
			, CPDeviceDesc 
			, DongleDescription
			, CPDeviceTypeDesc
			, CreatedDT
			, [DateTime]
			, [Date]
			, [TIME] 
			, [GUID] 
			, CPMeasurementID 
			, vwJRCP.Customer
			, CPDevicePrintTargetID
			, CPDeviceID
			, vwJRCP.CPDongleID
			, vwJRCP.BranchID
			, BranchName
			, vwJRCP.CompanyID
			, CompanyName
			, CPDeviceTypeID 
			, DongleSerial 
			, DATEADD( MI , @DIFFMIN , ISNULL(CreatedDT, '') )as [GMTDateTime] 
			, FirstSheet
			, LastSheet
			, CPSerialNUM
		FROM Press.vwGetJobPressReports vwJRCP 
		INNER JOIN TaskeroUser ON TaskeroUser.TaskeroUserID = @TaskeroUserID
		INNER JOIN UserDongle ON UserDongle.TaskeroUserID = TaskeroUser.TaskeroUserID
		WHERE vwJRCP.CPDongleID = UserDongle.CPDongleID
		AND 1 = ( CASE @CompanyID 
					WHEN 0 THEN 1 
					ELSE 
						CASE 
							WHEN vwJRCP.CompanyID = @CompanyID THEN 1
							ELSE 0
						END
				  END
				)
		AND 1 =  ( CASE ISNULL(TaskeroUser.RoleID , 0)
					WHEN 5 THEN
						CASE
							WHEN vwJRCP.Customer = TaskeroUser.Customer
							THEN 1
							ELSE 0
						END
					ELSE 
						1							 
					END			
				)
				AND CPDeviceTypeID  = 5
							
Order By CreatedDT Desc
End

		

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

