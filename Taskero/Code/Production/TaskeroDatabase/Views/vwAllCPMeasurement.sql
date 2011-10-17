IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllCPMeasurement')
	BEGIN
		DROP  View [CPV].[vwAllCPMeasurement]
	END
GO

/*
	AUTHOR : Maulik Dhorajia
	Description : Created for the replacement of vwCPMeasurementData.
*/

CREATE View [CPV].[vwAllCPMeasurement] AS

	SELECT     
  CONVERT(varchar(50),CPMeasurement.CPMeasurementGUID) As CPMeasurementGUID  
 ,CPMeasurement.CPMeasurementID As MeasurementID  
 ,CPMeasurement.Originator As Originator  
 ,CPMeasurement.CreatedDT As CreatedDT  
 ,CPMeasurement.CreatedGMT As CreatedGMT  
 ,CPMeasurement.[User] As CPUser  
 ,CPMeasurement.Customer As Customer   
 ,CPMeasurement.Job AS Job  
 ,CPMeasurement.Info_Text As Info_Text  
 ,CPMeasurement.Status As Proof_Status  
 ,CPMeasurement.NumOfPatches As NumOfPatches  
 ,CPMeasurement.TimeStamp As [TimeStamp]  
   
 ,CPMeasurement_ACT.Paper As Paper  
 ,CPMeasurement_ACT.Average As Average  
 ,CPMeasurement_ACT.PatchMaximumdE As PatchMaximumdE  
 ,CPMeasurement_ACT.PatchdEValue As PatchdEValue  
 ,CPMeasurement_ACT.PatchPrimarydE As PatchPrimarydE  
 ,CPMeasurement_ACT.PatchPrimarydEValue As PatchPrimarydEValue  
-- ,CPMeasurement_ACT.Dens_AV_Neg As Dens_AV_Neg  
-- ,CPMeasurement_ACT.Dens_AV_Pos As Dens_AV_Pos  
--  ,CPMeasurement_ACT.Dens_MAX_Neg As Dens_MAX_Neg  
-- ,CPMeasurement_ACT.Dens_Max_Neg_Val As Dens_Max_Neg_Val  
-- ,CPMeasurement_ACT.Dens_Max_Pos As Dens_Max_Pos  
-- ,CPMeasurement_ACT.Dens_Max_Pos_Val As Dens_Max_Pos_Val  
  
 ,CPMeasurement_MD.Filter As Filter  
 ,CPMeasurement_MD.DeviceSerialNum As MDSerialNum   
 ,CPMeasurement_MD.Illuminant As Illuminant  
 ,CPMeasurement_MD.ObserverType As ObserverType  
 ,CPMeasurement_MD.Instrument AS Instrument  
  
 ,CPMeasurement_DP.Substrate As Substrate  
 ,CPMeasurement_DP.Substrate_SKU As Substrate_SKU  
 ,CPMeasurement_DP.Substrate_Batch As Substrate_Batch  
 ,CPMeasurement_DP.Ink As Ink  
 ,CPMeasurement_DP.Ink_SKU As Ink_SKU  
 ,CPMeasurement_DP.Ink_Batch As Ink_Batch  
 ,CPMeasurement_DP.Chemistry As Chemistry  
 ,CPMeasurement_DP.Chemistry_SKU As Chemistry_SKU  
 ,CPMeasurement_DP.Chemistry_Batch As Chemistry_Batch  
 ,CPMeasurement_DP.Resolution As Resolution  
 ,CPMeasurement_DP.Line_Screen As Line_Screen  
 ,CPMeasurement_DP.Dot_Shape As Dot_Shape  
  
 ,CPMeasurement_TOL.Paper_Limit As Paper_Limit  
 ,CPMeasurement_TOL.Average_Limit AS Average_Limit  
 ,CPMeasurement_TOL.Maximum_Limit As Maximum_Limit  
 ,CPMeasurement_TOL.Primary_Limit As Primary_Limit   
 ,CPMeasurement_TOL.Dens_AV_Neg_Limit As Dens_AV_Neg_Limit  
 ,CPMeasurement_TOL.Dens_AV_Pos_Limit As Dens_AV_Pos_Limit  
 ,CPMeasurement_TOL.Dens_Max_Neg_Limit As Dens_Max_Neg_Limit  
 ,CPMeasurement_TOL.Dens_Max_Pos_Limit As Dens_Max_Pos_Limit  
 ,CPMeasurement_TOL.Paper_Strict As Paper_Strict  
 ,CPMeasurement_TOL.Average_Strict As Average_Strict  
 ,CPMeasurement_TOL.Maximum_Strict As Maximum_Strict  
 ,CPMeasurement_TOL.Primary_Strict As Primary_Strict  
 ,CPMeasurement_PT.Target_File As Target_File  
 ,CPMeasurement_PT.Proof_Supplier As Proof_Supplier  
  
 ,CPMeasurement_Printer.Printer_Model As Printer_Model  
 ,CPMeasurement_Printer.Printer_SerialNum As Printer_SerialNum  
 ,CPMeasurement_Printer.Printer_State As Printer_State  
 ,CPMeasurement_Printer.Printer_User As Printer_User  
 ,CPMeasurement_Printer.Printer_Host As Printer_Host  
 ,CPMeasurement_Printer.Printer_Time As Printer_Time  
 ,CPMeasurement_Printer.Printer_InkName_0 As Printer_InkName_0  
 ,CPMeasurement_Printer.Printer_InkValue_0 As Printer_InkValue_0  
 ,CPMeasurement_Printer.Printer_InkName_1 As Printer_InkName_1  
 ,CPMeasurement_Printer.Printer_InkValue_1 As Printer_InkValue_1   
 ,CPMeasurement_Printer.Printer_InkName_2 As Printer_InkName_2  
 ,CPMeasurement_Printer.Printer_InkValue_2 As Printer_InkValue_2  
 ,CPMeasurement_Printer.Printer_InkName_3 As Printer_InkName_3   
 ,CPMeasurement_Printer.Printer_InkValue_3 As Printer_InkValue_3  
 ,CPMeasurement_Printer.Printer_InkName_4 As Printer_InkName_4  
 ,CPMeasurement_Printer.Printer_InkValue_4 As Printer_InkValue_4  
 ,CPMeasurement_Printer.Printer_InkName_5 As Printer_InkName_5  
 ,CPMeasurement_Printer.Printer_InkValue_5 As Printer_InkValue_5  
 ,CPMeasurement_Printer.Printer_InkName_6 As Printer_InkName_6  
 ,CPMeasurement_Printer.Printer_InkValue_6 As Printer_InkValue_6  
 ,CPMeasurement_Printer.Printer_InkName_7 As Printer_InkName_7  
 ,CPMeasurement_Printer.Printer_InkValue_7 As Printer_InkValue_7  
 ,CPMeasurement_Printer.Printer_InkName_8 As Printer_InkName_8  
 ,CPMeasurement_Printer.Printer_InkValue_8 As Printer_InkValue_8  
 ,CPMeasurement_Printer.Printer_Finisher As Printer_Finisher  
 ,CPMeasurement_Printer.Printer_FinisherValue As Printer_FinisherValue  
 ,CPMeasurement_Printer.Printer_Paper As Printer_Paper  
 ,CPMeasurement_Printer.Printer_CC_Table As Printer_CC_Table   
 ,CPMeasurement_Printer.Printer_ICC_Target As Printer_ICC_Target  
   
 ,deltaEFormula.deltaEFormula As deltaEFormula  
  
   
  
 ,CPMeasurement_OD.Category As Category  
 ,CPMeasurement_OD.Separation As Separation  
 ,CPMeasurement_OD.Manufacturer As Manufacturer  
 ,CPMeasurement_OD.Model As Model  
 ,CPMeasurement_OD.Serial_Num As Serial_Num  
 ,CPMeasurement_OD.RIP_Type As RIP_Type  
  
 ,CPDongle.CPSerialNUM As CPSerialNUM  
 ,CPDongle.DongleDescription As DongleDescription
 
 ,CPMeasurement_PT_Prop.Density_C As Density_C  
 ,CPMeasurement_PT_Prop.Density_M As Density_M  
 ,CPMeasurement_PT_Prop.Density_Y As Density_Y  
 ,CPMeasurement_PT_Prop.Density_K As Density_K  
 ,CPMeasurement_PT_Prop.Density_TH As Density_TH  
 ,CPMeasurement_PT_Prop.Dotgain_C As Dotgain_C  
 ,CPMeasurement_PT_Prop.Dotgain_M As Dotgain_M  
 ,CPMeasurement_PT_Prop.Dotgain_Y As Dotgain_Y  
 ,CPMeasurement_PT_Prop.Dotgain_K As Dotgain_K  
 ,CPMeasurement_PT_Prop.Dotgain_TH As Dotgain_TH  
 ,CPMeasurement_PT_Prop.Print_Contrast_C As Print_Contrast_C  
 ,CPMeasurement_PT_Prop.Print_Contrast_M As Print_Contrast_M  
 ,CPMeasurement_PT_Prop.Print_Contrast_Y As Print_Contrast_Y  
 ,CPMeasurement_PT_Prop.Print_Contrast_K As Print_Contrast_K  
 ,CPMeasurement_PT_Prop.Print_Contrast_TH As Print_Contrast_TH  
 ,CPMeasurement_PT_Prop.Trap_R As Trap_R  
 ,CPMeasurement_PT_Prop.Trap_G As Trap_G  
 ,CPMeasurement_PT_Prop.Trap_B As Trap_B  
 ,CPMeasurement_PT_Prop.Trap_TH As Trap_TH  
 ,CPMeasurement_PT_Prop.[1ST_INK] As [1ST_INK]  
 ,CPMeasurement_PT_Prop.[2ND_INK] As [2ND_INK]  
 ,CPMeasurement_PT_Prop.[3RD_INK] As [3RD_INK]  
 ,CPMeasurement_PT_Prop.[4TH_INK] As [4TH_INK]  
  
 ,CPDevice.CPDeviceID As CPDeviceID  
 ,CPDevice.CPDeviceDesc As CPDeviceDesc  
 ,CPDevicePrintTarget.PrintTarget As PrintTarget   
 ,CPMeasurementType.CPMeasurementType As MeasurementType  
  
FROM           
  
CPDevice   
INNER JOIN CPDongle ON CPDevice.CPDongleID = CPDongle.CPDongleID   
INNER JOIN CPMeasurement ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID   
INNER JOIN CPMeasurement_ACT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_ACT.CPMeasurementGUID  
INNER JOIN CPMeasurement_DP ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_DP.CPMeasurementGUID   
INNER JOIN CPMeasurement_MD ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_MD.CPMeasurementGUID   
INNER JOIN CPMeasurement_OD ON  CPMeasurement.CPMeasurementGUID = CPMeasurement_OD.CPMeasurementGUID  
INNER JOIN CPMeasurement_Printer ON dbo.CPMeasurement.CPMeasurementGUID = CPMeasurement_Printer.CPMeasurementGUID   
INNER JOIN CPMeasurement_PT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT.CPMeasurementGUID   
INNER JOIN CPMeasurement_PT_Prop ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT_Prop.CPMeasurementGUID   
INNER JOIN CPMeasurement_TOL ON CPMeasurement.CPMeasurementGUID = CPMeasurement_TOL.CPMeasurementGUID  
INNER JOIN CPMeasurementType ON CPMeasurementType.CPMeasurementTypeID = CPMeasurement.CPMeasurementTypeID  
INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID  
LEFT OUTER JOIN deltaEFormula ON CPMeasurement_TOL.deltaEForumlaID = deltaEFormula.CPdeltaEFormulaID  
  