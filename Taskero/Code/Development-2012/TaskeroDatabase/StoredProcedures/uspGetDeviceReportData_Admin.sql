IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDeviceReportData_Admin')
	BEGIN
		DROP  Procedure [CPV].[uspGetDeviceReportData_Admin]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 08/01/2008
-- Modified By:	
-- Modified date: 
-- Description:	Device List For Admin
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDeviceReportData_Admin]
(
	@CompanyID AS INT,
	@Press as BIT
)
AS
BEGIN
BEGIN TRY
	
/*---------- Retrieves record for Color, Plate, Wide Format & Monitor ----------*/
IF @Press = 0
		Begin
SELECT * FROM CPV.vwGetDeviceReportColorPlate 
WHERE 1 = ( CASE @CompanyID 
				WHEN 0 THEN 1 
				ELSE 
					CASE 
						WHEN CompanyID = @CompanyID THEN 1
						ELSE 0
					END
			  END
			)
			AND CPDeviceTypeID  <> 5 Order By StatusID Desc
			End
		Else
		Begin	

/*UNION*/
/*---------- Retrieves record for Press ----------*/
SELECT * FROM Press.vwGetDeviceReportPress
WHERE 1 = ( CASE @CompanyID 
				WHEN 0 THEN 1 
				ELSE 
					CASE 
						WHEN CompanyID = @CompanyID THEN 1
						ELSE 0
					END
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