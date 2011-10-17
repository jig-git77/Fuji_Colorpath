IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDeviceByDongleID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDeviceByDongleID]
	END

GO

/*
AUTHOR : Amit Patel
Created Date : July 21 2008
Purpose : To get Device of the particular Dongle Number
*/
CREATE PROCEDURE [CPV].[uspGetDeviceByDongleID] 
( 
	@DongleID INT 
) 
AS
BEGIN
	BEGIN TRY

		/*----------- Gets the Branch on BranchID ----------*/
		SELECT 
			CPDeviceID, CPDeviceDesc, CPDongleID
			, MeasurementTypeID,CPDeviceTypeID,LastMeasurementID
		FROM 
			CPDevice 
		WHERE 
			CPDongleID = @DongleID			
		ORDER BY
			CPDeviceDesc

		
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

