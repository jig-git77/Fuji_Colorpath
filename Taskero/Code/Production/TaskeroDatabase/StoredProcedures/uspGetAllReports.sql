IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllReports')
	BEGIN
		DROP  Procedure  [CPV].[uspGetAllReports]
	END

GO
/*
	Authors : Maulik Dhorajia
	Date : Oct 15 2008
	Description : Gets all the reports.
*/
CREATE Procedure [CPV].[uspGetAllReports]
@TaskeroUserID	INT = 0
AS
BEGIN
	
	IF((SELECT RoleID FROM TaskeroUser where TaskeroUserID = @TaskeroUserID) < 2)
	BEGIN
		SELECT * FROM Report
	END
	ELSE
	BEGIN
		SELECT * FROM Report where ReportId IN (SELECT DISTINCT ReportID FROM UserReport WHERE TaskeroUserID = @TaskeroUserID)
	END

END
