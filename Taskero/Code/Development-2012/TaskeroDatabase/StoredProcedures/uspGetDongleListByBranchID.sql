IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDongleListByBranchID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDongleListByBranchID]
	END

GO

-- =============================================
-- Author:		Jignesh Patel
-- Create date: 07/18/2008
-- Description:	Dongle List By Branch
-- =============================================
CREATE PROCEDURE [CPV].[uspGetDongleListByBranchID]
(
	@BranchID AS INT
)
AS  
BEGIN
BEGIN TRY
	SELECT CPDongleID, DongleDescription FROM CPDongle
	WHERE BranchID = @BranchID
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
