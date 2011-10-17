IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertTaskeroUser')
	BEGIN
		DROP  Procedure [CPV].[uspInsertTaskeroUser]
	END

GO

/*========== INSERT ==========*/  
/*  
AUTHOR : Shivpal Jhala  
LAST UPDATED DATE : July 14 , 2008  
PURPOSE :- Creates a new user  
*/  
CREATE PROCEDURE [CPV].[uspInsertTaskeroUser]   
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
	@Css VARCHAR(25)= NULL ,   
	@LogoPath1 VARCHAR(50)= NULL ,   
	@LogoPath2 VARCHAR(50) = NULL,   
	@RoleID TINYINT = 0 ,   
	@Customer NVARCHAR(100) = NULL,   
	@IsActive BIT,     
	@CompanyID NVARCHAR(1000),  
	@BranchID NVARCHAR(1000),  
	@PermissionID NVARCHAR(2000),  
	@DongleID NVARCHAR(2000),
	@ReportID NVARCHAR(2000),
	@AllowedIP VARCHAR(15),
	@BrandID INT = 0 ,  
	@EmailWhenYellow BIT,
	@EmailWhenRed BIT,
	@Result INT = 0 OUTPUT   
) 
AS   
BEGIN  
 BEGIN TRY  
   /*IF EXISTS (SELECT LoginID FROM TaskeroUser WHERE Email=@Email )  
   BEGIN  
	   --*** setting the @Result = 3 if the duplicate Email is found***--  
	   SET @Result = 3
	   RETURN
   END*/
  --************************  Duplicate Check for the Username  ************************--  
  IF EXISTS (SELECT LoginID FROM TaskeroUser WHERE LoginID = @LoginID)  
   BEGIN  
   --*** setting the @Result =1 if the duplicate Username is found***--  
    set @Result = 1  
   END  
  ELSE  
   BEGIN     
    --************************  Inserting into TaskeroUser table  ************************--  
    INSERT INTO TaskeroUser  
    (  
     [LoginID]   
     , [LoginPass]   
     , [LastName]   
     , [FirstName]   
     , [Email]   
     , [ShowCPVData]        
     , [LastModified_UserID]   
     , [LastModifiedDate]   
     , [RoleID]   
     , [Customer]   
     , [IsActive]  
     , [AllowedIP]
     , [BrandID]
     , [EmailWhenYellow]
     , [EmailWhenRed]
    )   
    VALUES   
    (  
     @LoginID   
     , @LoginPass   
     , @LastName   
     , @FirstName   
     , @Email   
     , @ShowCPVData   
     , @LastModified_UserID   
     , GETDATE()   
     , @RoleID   
     , @Customer   
     , @IsActive 
     , @AllowedIP 
     , @BrandID
     , @EmailWhenYellow
     , @EmailWhenRed
    )  
      
    --************************  Fetching the TaskeroUserID into @Result Variable from TaskeroUser for the further use  ************************--  
    SELECT   
     @Result = TaskeroUserID   
    FROM   
     TaskeroUser   
    WHERE   
     [LoginID]= @LoginID  
  
    IF @Result > 0   
    BEGIN  

	--************************  Inserting into UserCompany table with TaskeroUserID as ref Key  ************************--  
     DECLARE @xmlCompany XML  
     SET @xmlCompany = @CompanyID  
       
     INSERT INTO UserCompany   
     SELECT   
      @Result, xmlCompany.value('.', 'VARCHAR(20)') AS ID   
     FROM @xmlCompany.nodes('/Company/CompanyID') d(xmlCompany)  
       
     --************************  Inserting into UserBranch table with TaskeroUserID as ref Key  ************************--  
     DECLARE @xmlBranch XML  
     SET @xmlBranch = @BranchID  
       
     INSERT INTO UserBranch   
     SELECT   
      @Result, xmlBranch.value('.', 'VARCHAR(20)') AS ID   
     FROM   
      @xmlBranch.nodes('/Branch/BranchID') d(xmlBranch)  
  
     --************************  Inserting into UserDongle table with TaskeroUserID as ref Key  ************************--  
     DECLARE @xmlDongle XML  
     SET @xmlDongle = @DongleID  
       
     INSERT INTO UserDongle   
     SELECT   
      @Result, xmlDongle.value('.', 'VARCHAR(20)') AS ID   
     FROM @xmlDongle.nodes('/Dongle/DongleID') d(xmlDongle)  
     
     --************************  Inserting into UserReport table with TaskeroUserID as ref Key  ************************--  
     DECLARE @xmlReport XML  
     SET @xmlReport = @ReportID  
       
     INSERT INTO UserReport   
     SELECT   
      @Result, xmlReport.value('.', 'VARCHAR(20)') AS ID   
     FROM @xmlReport.nodes('/Report/ReportID') d(xmlReport)  
  
     --************************  Checking for role if no role selected then permission will be inserted  ************************--  
     IF @RoleID = 0   
     BEGIN  
        
      --************************  Inserting into UserPermission table with TaskeroUserID as ref Key  ************************--  
      DECLARE @xmlPermission XML  
      SET @xmlPermission = @PermissionID  
        
      INSERT INTO UserPermission   
      SELECT   
       @Result, xmlPermission.value('.', 'VARCHAR(20)') AS ID   
      FROM @xmlPermission.nodes('/Permission/PermissionID') d(xmlPermission)  
  
     END  
     --*** setting the @Result =2 for the successfully insert of data***--  
     SET @Result = 2  
    END  
   END  
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
  

