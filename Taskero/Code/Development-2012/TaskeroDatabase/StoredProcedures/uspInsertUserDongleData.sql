IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertUserDongleData')
	BEGIN
		DROP  Procedure [CPV].[uspInsertUserDongleData] 
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 28 2008  
Purpose : Insert dongle data into the UserDongle data
*/

CREATE PROCEDURE [CPV].[uspInsertUserDongleData] 
(
	@TaskeroUserID INT,
	@SelectedIDs VARCHAR(4000),
	@NonSelectedIDs VARCHAR(4000) , 
	@Result INT = 0 OUTPUT
)
AS    
BEGIN  
	BEGIN TRY

		/*---------- ASSIGN SELECTED IDs TO XML ----------*/
		IF (@SelectedIDs != '')
		BEGIN
				DECLARE @xmlDongle XML  
				SET @xmlDongle = '<DongleIDs><DongleID>'+ REPLACE( @SelectedIDs ,',','</DongleID><DongleID>') +'</DongleID></DongleIDs>' 

				--************************  Inserting into UserDongle table with TaskeroUserID as ref Key  ************************--  
				INSERT INTO 
						UserDongle
							SELECT   
									@TaskeroUserID , xmlDongle.value('.', 'INT') AS ID   
							FROM   
									@xmlDongle.nodes('/DongleIDs/DongleID') d(xmlDongle)
							WHERE
									xmlDongle.value('.', 'INT') NOT IN
														(
															SELECT 
																	CPDongleID 
															FROM 
																	UserDongle 
															WHERE 
																	TaskeroUserID = @TaskeroUserID
														)
		END		
		
		IF (@NonSelectedIDs != '')
		BEGIN
				/*---------- ASSIGN NON SELECTED IDs TO XML ----------*/
				DECLARE @xmlNonSelectedIDs XML	
				SET @xmlNonSelectedIDs = '<DongleIDs><DongleID>'+ REPLACE( @NonSelectedIDs ,',','</DongleID><DongleID>') +'</DongleID></DongleIDs>' 

				/*------------- Delete all the records from user Dongle where dongle ids exist in non selected list ----------------*/ 
				DELETE FROM 
						UserDongle
				WHERE
						TaskeroUserID = @TaskeroUserID 
				AND		
						CPDongleID 
				IN
				(
					SELECT   
							xmlNonSelectedIDs.value('.', 'INT') AS ID   
					FROM   
							@xmlNonSelectedIDs.nodes('/DongleIDs/DongleID') d(xmlNonSelectedIDs)
				)
		END
		SET @Result = 1
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

