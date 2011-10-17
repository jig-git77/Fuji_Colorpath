IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDTLChartsPatchDetailsOnCPPatchDataLabID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDTLChartsPatchDetailsOnCPPatchDataLabID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : July 23 2008
Purpose : Retrieves data on CPPatchDataLabID for patch data.
*/
CREATE PROCEDURE [CPV].[uspGetDTLChartsPatchDetailsOnCPPatchDataLabID]
( 
	@CPPatchDataLabID INT 
) 
AS  
BEGIN
	BEGIN TRY

	SELECT
		Sample_Name 
		, ISNULL( Density , 0 ) as Density
		, ISNULL( CMYK_C , 0 ) as CMYK_C
		, ISNULL( CMYK_M , 0 ) as CMYK_M
		, ISNULL( CMYK_Y , 0 ) as CMYK_Y
		, ISNULL( CMYK_K , 0 ) as CMYK_K
		, ISNULL( LAB_L1 , 0 ) as LAB_L1
		, ISNULL( LAB_A1 , 0 ) as LAB_A1
		, ISNULL( LAB_B1 , 0 ) as LAB_B1
		, ISNULL( LAB_L2 , 0 ) as LAB_L2
		, ISNULL( LAB_A2 , 0 ) as LAB_A2
		, ISNULL( LAB_B2 , 0 ) as LAB_B2
		, ISNULL( ID_N , 0 ) as ID_N
		, ISNULL( TOL_STRICT , 0 ) as TOL_STRICT
		, ISNULL( TOL_STAND , 0 ) as TOL_STAND
		, ISNULL( dl , 0 ) as dL
		, ISNULL( dA , 0 ) as dA
		, ISNULL( dB , 0 ) as dB
		, ISNULL( dE , 0 ) as dE
		, ISNULL( dH , 0 ) as dH
		, ISNULL( dC , 0 ) as dC
	FROM 
		CPPatchDataLab
	WHERE
		CPPatchDataLabID = @CPPatchDataLabID

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
