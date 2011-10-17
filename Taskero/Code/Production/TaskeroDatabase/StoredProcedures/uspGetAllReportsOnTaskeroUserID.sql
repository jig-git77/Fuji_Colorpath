IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllReportsOnTaskeroUserID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetAllReportsOnTaskeroUserID]
	END

GO
/*
	Authors : Maulik Dhorajia
	Date : Oct 15 2008
	Description : Gets all the reports by TaskeroUserID.
*/
CREATE Procedure [CPV].[uspGetAllReportsOnTaskeroUserID](@TaskeroUserID INT)
AS
BEGIN

DECLARE @RoleID INT
SELECT @RoleID = RoleID FROM TaskeroUser WHERE TaskeroUserID = @TaskeroUserID

	IF @RoleID = 1 
		BEGIN
			SELECT 
				* 
			FROM Report
		END	
	ELSE
		BEGIN	
			SELECT 
				* 
			FROM Report
			INNER JOIN UserReport ON UserReport.ReportID = Report.ReportID
			WHERE UserReport.TaskeroUserID = @TaskeroUserID			
		END

END
