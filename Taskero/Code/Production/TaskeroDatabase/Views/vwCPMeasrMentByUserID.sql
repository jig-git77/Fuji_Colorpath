IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwCPMeasrMentByUserID')
	BEGIN
		DROP  View [CPV].[vwCPMeasrMentByUserID]
	END
GO
/*
	Author :- Amit Patel
	Created Date :- Aug 9 2008
	Description :- The view for all Data Moniter Informations
*/
CREATE View [CPV].[vwCPMeasrMentByUserID] AS

SELECT CONVERT(varchar(50), CPMeasurement.CPMeasurementGUID) AS CPMeasurementGUID, 
CPMeasurement.CPMeasurementID AS MeasurementID, CPMeasurement.Originator, CPMeasurement.CreatedDT, 
CPMeasurement.CreatedGMT, CPMeasurement.[User] AS CPUser, CPMeasurement.Customer, CPMeasurement.Job, 
CPMeasurement.Info_Text, CPMeasurement.Status AS Proof_Status, CPMeasurement.NumOfPatches, CPMeasurement.TimeStamp, 
CPMeasurement_ACT.Paper, CPMeasurement_ACT.Average, CPMeasurement_ACT.PatchMaximumdE, 
CPMeasurement_ACT.PatchdEValue, CPMeasurement_ACT.PatchPrimarydE, CPMeasurement_ACT.PatchPrimarydEValue, 
CPMeasurement_MD.Filter, CPMeasurement_MD.DeviceSerialNum AS MDSerialNum, CPMeasurement_MD.Illuminant, 
CPMeasurement_MD.ObserverType, CPMeasurement_MD.Instrument, CPMeasurement_DP.Substrate, 
CPMeasurement_DP.Substrate_SKU, CPMeasurement_DP.Substrate_Batch, CPMeasurement_DP.Ink, CPMeasurement_DP.Ink_SKU, 
CPMeasurement_DP.Ink_Batch, CPMeasurement_DP.Chemistry, CPMeasurement_DP.Chemistry_SKU, 
CPMeasurement_DP.Chemistry_Batch, CPMeasurement_DP.Resolution, CPMeasurement_DP.Line_Screen, 
CPMeasurement_DP.Dot_Shape, CPMeasurement_TOL.Paper_Limit, CPMeasurement_TOL.Average_Limit, 
CPMeasurement_TOL.Maximum_Limit, CPMeasurement_TOL.Primary_Limit, CPMeasurement_TOL.Dens_AV_Neg_Limit, 
CPMeasurement_TOL.Dens_AV_Pos_Limit, CPMeasurement_TOL.Dens_Max_Neg_Limit, CPMeasurement_TOL.Dens_Max_Pos_Limit, 
CPMeasurement_TOL.Paper_Strict, CPMeasurement_TOL.Average_Strict, CPMeasurement_TOL.Maximum_Strict, 
CPMeasurement_TOL.Primary_Strict, CPMeasurement_PT.Target_File, CPMeasurement_PT.Proof_Supplier, 
CPMeasurement_Printer.Printer_Model, CPMeasurement_Printer.Printer_SerialNum, CPMeasurement_Printer.Printer_State, 
CPMeasurement_Printer.Printer_User, CPMeasurement_Printer.Printer_Host, CPMeasurement_Printer.Printer_Time, 
CPMeasurement_Printer.Printer_InkName_0, CPMeasurement_Printer.Printer_InkValue_0, CPMeasurement_Printer.Printer_InkName_1, 
CPMeasurement_Printer.Printer_InkValue_1, CPMeasurement_Printer.Printer_InkName_2, CPMeasurement_Printer.Printer_InkValue_2, 
CPMeasurement_Printer.Printer_InkName_3, CPMeasurement_Printer.Printer_InkValue_3, CPMeasurement_Printer.Printer_InkName_4, 
CPMeasurement_Printer.Printer_InkValue_4, CPMeasurement_Printer.Printer_InkName_5, CPMeasurement_Printer.Printer_InkValue_5, 
CPMeasurement_Printer.Printer_InkName_6, CPMeasurement_Printer.Printer_InkValue_6, CPMeasurement_Printer.Printer_InkName_7, 
CPMeasurement_Printer.Printer_InkValue_7, CPMeasurement_Printer.Printer_InkName_8, CPMeasurement_Printer.Printer_InkValue_8, 
CPMeasurement_Printer.Printer_Finisher, CPMeasurement_Printer.Printer_FinisherValue, CPMeasurement_Printer.Printer_Paper, 
CPMeasurement_Printer.Printer_CC_Table, CPMeasurement_Printer.Printer_ICC_Target, deltaEFormula.deltaEFormula, 
CPMeasurement_OD.Category, CPMeasurement_OD.Separation, CPMeasurement_OD.Manufacturer, CPMeasurement_OD.Model, 
CPMeasurement_OD.Serial_Num, CPMeasurement_OD.RIP_Type, CPDongle.CPSerialNUM,CPDongle.DongleDescription, CPMeasurement_PT_Prop.Density_C, 
CPMeasurement_PT_Prop.Density_M, CPMeasurement_PT_Prop.Density_Y, CPMeasurement_PT_Prop.Density_K, 
CPMeasurement_PT_Prop.Density_TH, CPMeasurement_PT_Prop.Dotgain_C, CPMeasurement_PT_Prop.Dotgain_M, 
CPMeasurement_PT_Prop.Dotgain_Y, CPMeasurement_PT_Prop.Dotgain_K, CPMeasurement_PT_Prop.Dotgain_TH, 
CPMeasurement_PT_Prop.Print_Contrast_C, CPMeasurement_PT_Prop.Print_Contrast_M, CPMeasurement_PT_Prop.Print_Contrast_Y, 
CPMeasurement_PT_Prop.Print_Contrast_K, CPMeasurement_PT_Prop.Print_Contrast_TH, CPMeasurement_PT_Prop.Trap_R, 
CPMeasurement_PT_Prop.Trap_G, CPMeasurement_PT_Prop.Trap_B, CPMeasurement_PT_Prop.Trap_TH, 
CPMeasurement_PT_Prop.[1ST_INK], CPMeasurement_PT_Prop.[2ND_INK], CPMeasurement_PT_Prop.[3RD_INK], 
CPMeasurement_PT_Prop.[4TH_INK], CPDevice.CPDeviceID, CPDevice.CPDeviceDesc, CPDevicePrintTarget.PrintTarget, 
CPMeasurementType.CPMeasurementType AS MeasurementType,UserDongle.TaskeroUserID
FROM CPDevice 
INNER JOIN CPDongle ON CPDevice.CPDongleID = CPDongle.CPDongleID AND ISNULL(CPDongle.IsUsed,1) = 1
INNER JOIN CPMeasurement ON CPDevice.CPDeviceID = CPMeasurement.CPDeviceID 
INNER JOIN CPMeasurement_ACT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_ACT.CPMeasurementGUID 
INNER JOIN CPMeasurement_DP ON CPMeasurement.CPMeasurementGUID = CPMeasurement_DP.CPMeasurementGUID 
INNER JOIN CPMeasurement_MD ON CPMeasurement.CPMeasurementGUID = CPMeasurement_MD.CPMeasurementGUID 
INNER JOIN CPMeasurement_OD ON CPMeasurement.CPMeasurementGUID = CPMeasurement_OD.CPMeasurementGUID 
INNER JOIN CPMeasurement_Printer ON CPMeasurement.CPMeasurementGUID = CPMeasurement_Printer.CPMeasurementGUID 
INNER JOIN CPMeasurement_PT ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT.CPMeasurementGUID 
INNER JOIN CPMeasurement_PT_Prop ON CPMeasurement.CPMeasurementGUID = CPMeasurement_PT_Prop.CPMeasurementGUID 
INNER JOIN CPMeasurement_TOL ON CPMeasurement.CPMeasurementGUID = CPMeasurement_TOL.CPMeasurementGUID 
INNER JOIN CPMeasurementType ON CPMeasurementType.CPMeasurementTypeID = CPMeasurement.CPMeasurementTypeID 
INNER JOIN CPDevicePrintTarget ON CPDevicePrintTarget.CPDevicePrintTargetID = CPMeasurement_PT.CPDevicePrintTargetID 
LEFT OUTER JOIN deltaEFormula ON CPMeasurement_TOL.deltaEForumlaID = deltaEFormula.CPdeltaEFormulaID 
LEFT OUTER JOIN Branch ON CPDongle.BranchID = Branch.BranchID 
LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID 
INNER JOIN UserDongle ON UserDongle.CPDongleID = CPDongle.CPDongleID

