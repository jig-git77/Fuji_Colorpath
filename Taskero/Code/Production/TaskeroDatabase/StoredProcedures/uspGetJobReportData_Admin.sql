
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetJobReportData_Admin')
	BEGIN
		DROP  Procedure [CPV].[uspGetJobReportData_Admin]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/17/2008
-- Modified By:	Maulik Dhorajia
-- Modified date: 07/22/2008
-- Description:	Job Report For Admin
-- =============================================
CREATE PROCEDURE [CPV].[uspGetJobReportData_Admin]
(
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
			* 
			, DATEADD( MI , @DIFFMIN , ISNULL(CreatedDT, '') )as [GMTDateTime] 
		FROM 
			CPV.vwGetJobReportsColorPlate
		WHERE 
			1 = ( CASE @CompanyID 
						WHEN 0 THEN 1 
						ELSE 
							CASE 
								WHEN CompanyID = @CompanyID THEN 1
								ELSE 0
							END
					  END
					)	
					AND CPDeviceTypeID  <> 5 Order By CreatedDT Desc
			End
		Else
		Begin	
		/*UNION*/
		
		/*---------- Query For Press ----------*/
		SELECT 
			* 
			, DATEADD( MI , @DIFFMIN , ISNULL(CreatedDT, '') )as [GMTDateTime] 
		FROM 
			Press.vwGetJobPressReports
		WHERE 
			1 = ( CASE @CompanyID 
						WHEN 0 THEN 1 
						ELSE 
							CASE 
								WHEN CompanyID = @CompanyID THEN 1
								ELSE 0
							END
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

