IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetLastStatusPerDevice')
	BEGIN
		DROP  View [CPV].[vwGetLastStatusPerDevice]
	END
GO

-- =============================================
-- Author:		Maulik Dhorajia
-- Create date: 08/20/2008
-- Description:	Gets the list of final reading status of device.
-- =============================================

CREATE View [CPV].[vwGetLastStatusPerDevice] AS

SELECT
	CPDeviceStatus.CPDeviceStatusID
	, CPDeviceStatus.CPDeviceID
	, CPDeviceStatus.CPDeviceStatusTypeID
	, CPDeviceStatus.OperatorReset
	, CPDeviceStatus.StatusDT
	, StatusDesc
FROM
	CPDeviceStatus
	INNER JOIN (
					SELECT 
						CPDeviceStatus.CPDeviceID 
						, Max(CPDeviceStatus.CPDeviceStatusID) as CPDeviceStatusID 
					FROM 
						CPDeviceStatus 
					GROUP BY 
						CPDeviceStatus.CPDeviceID 
				)  AS CPDeviceStatus1  ON CPDeviceStatus1.CPDeviceStatusID = CPDeviceStatus.CPDeviceStatusID
	INNER JOIN CPDeviceStatusType ON CPDeviceStatusType.CPDeviceStatusTypeID = CPDeviceStatus.CPDeviceStatusTypeID
GO
