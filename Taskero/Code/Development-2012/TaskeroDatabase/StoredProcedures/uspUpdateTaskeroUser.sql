IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateTaskeroUser')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateTaskeroUser]
	END

GO

/*========== INSERT ==========*/    
/*    
AUTHOR : Shivpal Jhala    
LAST UPDATED DATE : July 16 , 2008    
PURPOSE :- Creates a new user    
*/    
CREATE PROCEDURE [CPV].[uspUpdateTaskeroUser]     
(     
	@TaskeroUserID INT = NULL,    
	@LoginID NVARCHAR(100) ,     
	@LoginPass NVARCHAR(40) ,     
	@LastName NVARCHAR(60) ,     
	@FirstName NVARCHAR(60) ,     
	@Email NVARCHAR(100) ,     
	@ShowCPVData BIT = NULL,     
	@LastModified_UserID INT = NULL,     
	@LastModifiedDate DATETIME = NULL,     
	@RoleID TINYINT ,     
	@Customer NVARCHAR(100) = NULL,     
	@IsActive BIT,       
	@CompanyID NVARCHAR(1000),    
	@BranchID NVARCHAR(1000),    
	@PermissionID NVARCHAR(2000),    
	@DongleID NVARCHAR(2000),  
	@ReportID NVARCHAR(2000),  
	@AllowedIP VARCHAR(15),
	@BrandID INT,
	@EmailWhenYellow BIT,
	@EmailWhenRed BIT,
	@Result INT = 0 OUTPUT     
) 
AS     
BEGIN    
	BEGIN TRY 
		SET @Result = 1
		/*IF EXISTS (SELECT LoginID FROM TaskeroUser WHERE Email=@Email AND TaskeroUserID <> @TaskeroUserID)
		BEGIN
			SET @Result = 1 
			RETURN
		END*/
		--************************  Updating The TaskeroUser Table is passowrd is not set ************************--    
		if isnull(@LoginPass,'')=''
		BEGIN    
			--*** setting the @Result =1 if the duplicate Username is found***--    
			Update 
				TaskeroUser    
			SET  
				[LastName] = @LastName     
				, [FirstName] = @FirstName 
				, [Email] = @Email
				, [LastModified_UserID] = @LastModified_UserID
				, [LastModifiedDate] = GETDATE()
				, [RoleID] = @RoleID
				, [Customer] = @Customer
				, [IsActive] = @IsActive
				, [AllowedIP] = @AllowedIP
				, [BrandID] = @BrandID
				, [EmailWhenYellow] = @EmailWhenYellow
				, [EmailWhenRed] = @EmailWhenRed
			WHERE
				TaskeroUserID = @TaskeroUserID
		END    
		ELSE    
		BEGIN       
			--************************  Updating The TaskeroUser Table is passowrd is set ************************--    
			Update 
				TaskeroUser    
			SET     
				[LoginPass] = @LoginPass
				, [LastName] = @LastName     
				, [FirstName] = @FirstName 
				, [Email] = @Email
				, [LastModified_UserID] = @LastModified_UserID
				, [LastModifiedDate] = GETDATE()
				, [RoleID] = @RoleID
				, [Customer] = @Customer
				, [IsActive] = @IsActive
				, [AllowedIP] = @AllowedIP
				, [BrandID] = @BrandID
				, [EmailWhenYellow] = @EmailWhenYellow
				, [EmailWhenRed] = @EmailWhenRed
			WHERE
				TaskeroUserID = @TaskeroUserID
		END

		--***** Delete TaskeroUserID Data from UserCompany****--    
		DELETE FROM
			UserCompany
		WHERE
			TaskeroUserID = @TaskeroUserID				
		--************************  Inserting into UserCompany table with TaskeroUserID as ref Key  ************************--    
		DECLARE @xmlCompany XML    
		SET @xmlCompany = @CompanyID    
		INSERT INTO 
			UserCompany     
		SELECT     
			@TaskeroUserID, xmlCompany.value('.', 'VARCHAR(20)') AS ID     
		FROM 
			@xmlCompany.nodes('/Company/CompanyID') d(xmlCompany)    


		--***** Delete TaskeroUserID Data from UserBranch****--    
		DELETE FROM
			UserBranch
		WHERE
			TaskeroUserID = @TaskeroUserID				         
		--************************  Inserting into UserBranch table with TaskeroUserID as ref Key  ************************--    
		DECLARE @xmlBranch XML    
		SET @xmlBranch = @BranchID    
		INSERT INTO 
			UserBranch     
		SELECT     
			@TaskeroUserID, xmlBranch.value('.', 'VARCHAR(20)') AS ID     
		FROM     
			@xmlBranch.nodes('/Branch/BranchID') d(xmlBranch)    


		--***** Delete TaskeroUserID Data from UserDongle****--    
		DELETE FROM
			UserDongle
		WHERE
			TaskeroUserID = @TaskeroUserID    
		--************************  Inserting into UserDongle table with TaskeroUserID as ref Key  ************************--    
		DECLARE @xmlDongle XML    
		SET @xmlDongle = @DongleID    
		INSERT INTO 
			UserDongle     
		SELECT     
			@TaskeroUserID, xmlDongle.value('.', 'VARCHAR(20)') AS ID     
		FROM
			@xmlDongle.nodes('/Dongle/DongleID') d(xmlDongle)  
			
		--***** Delete TaskeroUserID Data from UserReport****--    
		DELETE FROM
			UserReport
		WHERE
			TaskeroUserID = @TaskeroUserID    
		--************************  Inserting into UserReport table with TaskeroUserID as ref Key  ************************--    
		DECLARE @xmlReport XML    
		SET @xmlReport = @ReportID    
		INSERT INTO 
			UserReport     
		SELECT     
			@TaskeroUserID, xmlReport.value('.', 'VARCHAR(20)') AS ID     
		FROM
			@xmlReport.nodes('/Report/ReportID') d(xmlReport)    


		--************************  Checking for role if no role selected then permission will be inserted  ************************--    
		IF @RoleID = 0     
		BEGIN    
			--***** Delete TaskeroUserID Data from UserPermission****--    
			DELETE FROM
				UserPermission
			WHERE
				TaskeroUserID = @TaskeroUserID  
			--************************  Inserting into UserPermission table with TaskeroUserID as ref Key  ************************--    
			DECLARE @xmlPermission XML    
			SET @xmlPermission = @PermissionID    
			INSERT INTO 
				UserPermission     
			SELECT     
				@TaskeroUserID, xmlPermission.value('.', 'VARCHAR(20)') AS ID     
			FROM 
				@xmlPermission.nodes('/Permission/PermissionID') d(xmlPermission)    
		END    
		--*** setting the @Result =2 for the successfully insert of data***--    
		SET @Result = 2    
		    	    
	END TRY    
	BEGIN CATCH    
		--*** Error Handling***--    
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


