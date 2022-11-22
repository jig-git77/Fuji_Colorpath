SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trushant Mehta
-- Create date: 2022/11/21
-- Description:	Delete spot target
-- =============================================
CREATE PROCEDURE [CPSpot].[DeleteSpotTargetList]
(	
	@SpotTargetList XML
	,@isDeleted INT OUT
)
AS
BEGIN
		
    BEGIN TRY ;

		WITH SpotTargetListData AS 
		(
			SELECT [SpotTargetXml].value('@SpotTargetId', 'INT') AS SpotTargetID
			FROM @SpotTargetList.nodes('/SpotTargetIdData/SpotTarget') p([SpotTargetXml])
		)
		SELECT  @isDeleted = -2 FROM [CPSpot].[SpotProject] 	WHERE SpotTargetID in (SELECT SpotTargetID FROM SpotTargetListData ) ;

		if (@isDeleted = -2)
		BEGIN 			
			RETURN 
		END ;

		BEGIN TRAN	;

			WITH SpotTargetListData AS 
			(
				SELECT [SpotTargetXml].value('@SpotTargetId', 'INT') AS SpotTargetID
				FROM @SpotTargetList.nodes('/SpotTargetIdData/SpotTarget') p([SpotTargetXml])
			)
			DELETE FROM CPSpot.SpotTarget
			WHERE SpotTargetID in (SELECT SpotTargetID FROM SpotTargetListData ) ;
					
			SET @isDeleted = 1 ;

		COMMIT TRAN 

	END TRY
	
	BEGIN CATCH		
	
		ROLLBACK TRAN
		SET @isDeleted = -1
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	
	END CATCH
	
END
