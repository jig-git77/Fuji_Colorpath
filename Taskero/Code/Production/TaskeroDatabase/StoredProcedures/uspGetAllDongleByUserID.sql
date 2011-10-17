IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllDongleByUserID')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllDongleByUserID]
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 28 2008  
Purpose : To Get all the Dongle by user id.  
*/

CREATE PROCEDURE [CPV].[uspGetAllDongleByUserID] 
(
	@TaskeroUserID INT,
	@CompanyID INT,
	@RoleID INT
)
AS    
BEGIN  
	BEGIN TRY
		IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)  
		BEGIN 
				/*------------- Get All Dongle List ----------------*/  	
				SELECT 
						CPDongle.CPDongleID , 
						CPDongle.BranchID ,
						CPSerialNUM ,
						DongleDescription ,			
						CPDongle.LastModifiedBy ,
						CPDongle.LastModifiedDate ,
						ISNULL( IsUsed , 1 ) AS IsUsed ,
						Branch.BranchName ,
						Company.CompanyName ,
						Branch.CompanyID ,
						CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial	
				FROM  
					    CPDongle 
						LEFT OUTER JOIN Branch ON CPDongle.BranchID = Branch.BranchID
						LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
				--WHERE
				--		Company.CompanyID = @CompanyID
				ORDER BY 
						CPSerialNUM
		END
		ELSE
		BEGIN
				/*------------- Get All Dongle List----------------*/  	
				SELECT 
						CPDongle.CPDongleID , 
						CPDongle.BranchID ,
						CPSerialNUM ,
						DongleDescription ,			
						CPDongle.LastModifiedBy ,
						CPDongle.LastModifiedDate ,
												ISNULL( IsUsed , 1 ) AS IsUsed ,
						Branch.BranchName ,
						Company.CompanyName ,
						Branch.CompanyID	
				FROM 
					CPDongle
					LEFT OUTER JOIN Branch ON CPDongle.BranchID = Branch.BranchID
					LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
					INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDongle.CPDongleID
				WHERE 
					UserDongle.TaskeroUserID = @TaskeroUserID
					AND ISNULL(CPDongle.IsUsed,1) = 1
				ORDER BY 
					CPSerialNUM
		END
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
