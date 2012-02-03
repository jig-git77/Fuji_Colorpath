IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDeviceReportData')
	BEGIN
		DROP  Procedure [CPV].[uspGetDeviceReportData]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/18/2008
-- Modified By:	Maulik Dhorajia
-- Modified date: 08/25/2008
-- Description:	Device List
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDeviceReportData]
(
	@TaskeroUserID AS INT,
	@CompanyID AS INT,
	@Press as BIT
)
AS
BEGIN
BEGIN TRY

/*---------- Retrieves record for Color, Plate, Wide Format & Monitor ----------*/
IF @Press = 0
		Begin
SELECT 
	vwGRCP.CPDeviceID
	, CPDeviceDesc 
	, vwGRCP.CPDongleID	
	, DongleDescription  
	, DongleSerial 
	, BranchID
	, BranchName
	, CompanyID
	, CompanyName
	, CPDeviceTypeID
	, CPDeviceTypeDesc
	, [GUID]
	, CPDevicePrintTargetID
	, PrintTarget 
	, Job 
	, CPMeasurementID
	, vwGRCP.Customer
	, [DateTime]
	, [Date] 
	, [TIME] 
	, FlgOpenMeasurementWindow
	, StatusID 
	, Status
	, CPSerialNUM
FROM CPV.vwGetDeviceReportColorPlate vwGRCP
INNER JOIN TaskeroUser ON TaskeroUser.TaskeroUserID = @TaskeroUserID
INNER JOIN UserDongle ON UserDongle.TaskeroUserID = TaskeroUser.TaskeroUserID
WHERE vwGRCP.CPDongleID = UserDongle.CPDongleID
AND 1 = ( CASE @CompanyID 
				WHEN 0 THEN 1 
				ELSE 
					CASE 
						WHEN CompanyID = @CompanyID THEN 1
						ELSE 0
					END
			  END
			)
		AND 
			1 =  ( CASE ISNULL(TaskeroUser.RoleID , 0)
						WHEN 5 THEN
							CASE
								WHEN vwGRCP.Customer = TaskeroUser.Customer
								THEN 1
								ELSE 0
							END
						ELSE 
							1							 
						END			
					)
			AND CPDeviceTypeID  <> 5 Order By StatusID Desc
				End
		Else
		Begin		
/*UNION*/

/*---------- Retrieves record for Press ----------*/
SELECT 
	vwGRCP.CPDeviceID
	, CPDeviceDesc 
	, vwGRCP.CPDongleID	
	, DongleDescription  
	, DongleSerial 
	, BranchID
	, BranchName
	, CompanyID
	, CompanyName
	, CPDeviceTypeID
	, CPDeviceTypeDesc
	, [GUID]
	, CPDevicePrintTargetID
	, PrintTarget 
	, Job 
	, CPMeasurementID
	, vwGRCP.Customer
	, [DateTime]
	, [Date] 
	, [TIME] 
	, FlgOpenMeasurementWindow
	, StatusID 
	, Status
	, CPSerialNUM
FROM Press.vwGetDeviceReportPress vwGRCP
INNER JOIN TaskeroUser ON TaskeroUser.TaskeroUserID = @TaskeroUserID
INNER JOIN UserDongle ON UserDongle.TaskeroUserID = TaskeroUser.TaskeroUserID
WHERE vwGRCP.CPDongleID = UserDongle.CPDongleID
AND 1 = ( CASE @CompanyID 
				WHEN 0 THEN 1 
				ELSE 
					CASE 
						WHEN CompanyID = @CompanyID THEN 1
						ELSE 0
					END
			  END
			)
		AND 
			1 =  ( CASE ISNULL(TaskeroUser.RoleID , 0)
						WHEN 5 THEN
							CASE
								WHEN vwGRCP.Customer = TaskeroUser.Customer
								THEN 1
								ELSE 0
							END
						ELSE 
							1							 
						END			
					)
AND CPDeviceTypeID  = 5
							
Order By StatusID Desc
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