USE [ColorPathSync_Stage]
GO
/****** Object:  StoredProcedure [CP].[GetProjectListBySerial_LAB_Xmf]    Script Date: 4/19/2021 2:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [CP].[GetProjectListBySerial_LAB_Xmf]
(   
	@DeviceSerialNo nvarchar(100),
	@InstrumentTypeID tinyint
)   
AS   	

SELECT 
	 P.ProjectID
	,P.ProjectName
	,D.DeviceName
	, 'Validation' AS ProfileType
	, @DeviceSerialNo AS MeaDeviceSerialNo, @InstrumentTypeID AS InstrumentTypeID
	, P.LicenseTypeID
  FROM [CPSync4D].[Project] AS P
  INNER JOIN [CPSync4D].[MeasurementSetting] AS MST ON MST.ProjectID = P.ProjectID
  INNER JOIN CP.MeasurementDevice AS MD ON MD.MeasurementDeviceID = MST.VMeasurementDeviceID
  INNER JOIN CP.Device AS D ON D.DeviceID = P.CPDeviceID
  WHERE MD.InstrumentTypeID = @InstrumentTypeID AND MD.SerialNumber = @DeviceSerialNo