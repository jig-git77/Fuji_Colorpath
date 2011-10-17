IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTop1TempRHData')
	BEGIN
		DROP  Procedure  [CPV].[uspGetTop1TempRHData]
	END
Go

/*
AUTHOR : Maulik Dhorajia
Created Date : July 30 2008
Purpose : Gets the Top 1 Temperature RH Data.
*/

CREATE PROCEDURE [CPV].[uspGetTop1TempRHData] ( @CPDeviceID INT )
AS  
BEGIN
	BEGIN TRY

		SELECT TOP 1
			TRHDeviceSerialNum
			, CONVERT(Varchar(100),CreatedDT) As   Date_Time
			, TempF
			, TargetTemp
			, RH
			, TargetRH 
		FROM TempRHData
		INNER JOIN TempRHDevice ON TempRHData.TempRHDeviceID = TempRHDevice.TempRHDeviceID
		INNER JOIN TempRHDevice_CPDevice ON TempRHDevice_CPDevice.TempRHDeviceID = TempRHData.TempRHDeviceID 
		WHERE TempRHDevice_CPDevice.CPDeviceID = @CPDeviceID
		ORDER BY CreatedGMT DESC

		
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
