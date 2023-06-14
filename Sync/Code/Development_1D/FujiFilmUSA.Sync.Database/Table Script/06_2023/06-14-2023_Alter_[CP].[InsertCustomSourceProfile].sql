
GO
/****** Object:  StoredProcedure [CP].[InsertCustomSourceProfile]    Script Date: 6/14/2023 2:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[InsertCustomSourceProfile]

 @ProjectID				INT
,@ProfileName			NVARCHAR(50)
,@ProfileDesc			NVARCHAR(400)
,@Profile				VARBINARY(MAX)
,@IsUsed				BIT

AS
BEGIN
	DECLARE @RegionID				INT;
	DECLARE @CompanyID				INT;
	DECLARE @SourceProfileID		INT;
	DECLARE @PrintStandardTypeID	INT;

	IF EXISTS (SELECT * FROM CPSync4D.SourceProfile Where Upper(ProfileName) = Upper(@ProfileName))  
		BEGIN  
			Return -1
		END   

	BEGIN TRY
		BEGIN TRAN		
		
		SELECT @RegionID = R.RegionID, @CompanyID = C.CompanyID 
			FROM CPSync4D.Project AS P 
				INNER JOIN CP.Device AS D ON P.CPDeviceID = D.DeviceID
				INNER JOIN CP.License AS L ON D.LicenseID = L.LicenseID
				INNER JOIN CP.Branch AS B ON L.BranchID = B.BranchID
				INNER JOIN CP.Company AS C ON B.CompanyID = C.CompanyID
				INNER JOIN CP.Region AS R ON C.RegionID = R.RegionID
			WHERE P.ProjectID = @ProjectID

		 SELECT @ProfileDesc = 'Custom source profile generated in ' + B.BranchName + ' on the ' + D.DeviceName 
			 + '. Media: '+ PC.Media 
			 + '; Colorant: ' + PC.Colorant 
			 + '; Rip Settings: '+ PC.RipSettings  
			 +'; Date: ' + DATENAME(MONTH,GETDATE()) + ' ' + DATENAME(DD,GETDATE()) + ', ' + DATENAME(YEAR,GETDATE())
			 FROM CPSync4D.Project AS P
			 INNER JOIN CP.Device AS D ON D.DeviceID=P.CPDeviceID
			 INNER JOIN CP.License AS L ON L.LicenseID=D.LicenseID
			 INNER JOIN CP.Branch AS B ON B.BranchID=L.BranchID
			 INNER JOIN CPSync4D.PrintCondition AS PC ON PC.ProjectID=P.ProjectID			 
			 where P.ProjectID=@ProjectID


		INSERT INTO	CPSync4D.SourceProfile
			(
				 RegionID		
				,CompanyID		
				,ProfileName	
				,ProfileDesc	
				,[Profile]
				,SourceProfileType
				,IsUsed	
				,Category	
			)
		VALUES 
			(
				 @RegionID		
				,@CompanyID		
				,@ProfileName	
				,@ProfileDesc	
			 	,@Profile	
				,1
				,@IsUsed
				,4 --For custom source profile 	
			)	
		
			SET @SourceProfileID = @@IDENTITY
			SELECT @SourceProfileID = SourceProfileID FROM CPSync4D.SourceProfile WHERE ProfileName = @ProfileName;

			EXEC [CP].[InsertPrintStandardType] @ProfileName,0,@SourceProfileID,0 
			SELECT @PrintStandardTypeID = PrintStandardTypeID FROM CP.PrintStandardType WHERE PrintStandard = @ProfileName;

			UPDATE CPSync4D.Project SET PrintStandardTypeId = @PrintStandardTypeID where ProjectID = @ProjectID
	
			DECLARE @OptimizeRunID INT
			SELECT @OptimizeRunID = OptimizeRunID FROM CPsync4D.OptimizeRun WHERE ProjectID = @ProjectID

			UPDATE     
				CPSync4D.OptimizeRun    
			SET  
				[DeviceLinkProfile] = @Profile,
				[Status] = 2,
				EndDateTime = GETDATE()     
			WHERE    
				OptimizeRunID = @OptimizeRunID 
			
			UPDATE
				CPSync4D.Project
			SET
				CurrDLPID = @OptimizeRunID
			WHERE
				ProjectID = @ProjectID

		COMMIT TRAN	
				
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		
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
