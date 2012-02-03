ALTER Procedure [CPV].[uspColorSpecificationAdherenceReport_Admin]
( 
	@CompanyID INT
	, @BranchID INT 
	, @StartDate DATETIME
	, @EndDate DATETIME
	, @xmlCPMeasurementGUIDs XML
)
AS      
BEGIN    
	BEGIN TRY    

	/* ---------- Retrieves the XML Temperature and Humidity ---------- */	
	DECLARE @xmlTemp XML;
	SELECT @xmlTemp = (
		SELECT 
			TCPD.CPDeviceID 							
			, CONVERT( DECIMAL(9,2) , AVG(TempRHData.RH) ) AS AVEHumidity , CONVERT( DECIMAL(9,2) , STDEV(TempRHData.RH) ) AS STDHumidity 
			, CONVERT( DECIMAL(9,2) , AVG(TempRHData.TempF) ) AS AVETemp , CONVERT( DECIMAL(9,2) , STDEV(TempRHData.TempF) ) AS STDTemp 
		FROM TempRHData 
		INNER JOIN TempRHDevice_CPDevice TCPD ON TCPD.TempRHDeviceID = TempRHData.TempRHDeviceID  
		WHERE (TempRHData.CreatedDT BETWEEN  @StartDate and @EndDate) 
		GROUP BY TCPD.CPDeviceID 
		FOR XML RAW
	);

--SELECT xmlTemp.value('@CPDeviceID', 'INT') AS CPDeviceID , xmlTemp.value('@AVEHumidity', 'DECIMAL(9,2)') AS AVEHumidity , xmlTemp.value('@STDHumidity', 'DECIMAL(9,2)') AS STDHumidity , xmlTemp.value('@AVETemp', 'DECIMAL(9,2)') AS AVETemp , xmlTemp.value('@STDTemp', 'DECIMAL(9,2)') AS STDTemp FROM @xmlTemp.nodes('row') d(xmlTemp) ;
--SELECT xmlCPMeasurementGUIDs.value('.', 'VARCHAR(40)') AS CPMeasurementGUID FROM @xmlCPMeasurementGUIDs.nodes('GUID') d(xmlCPMeasurementGUIDs)

	/* ---------- Gets the records for the Branches ---------- */	
	SELECT 
		Branch.BranchID , Branch.BranchName 
		, COUNT(DISTINCT PMD.CPMeasurementGUID) AS Reads 
		, SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors  
		, CONVERT( DECIMAL(9,2) , (CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(9,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(9,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) ) AS ErrorRate 	
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(ACT.AVERAGE) , 0) ) AS AVG_DE , CONVERT( DECIMAL(9,2) , ISNULL(MAX(ACT.PatchdEValue) , 0) ) AS MAX_DE 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_SUBSTRATE.AVE_DE) , 0) ) AS SUBSTRATE_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_SUBSTRATE.AVE_DH)) , 0) ) AS SUBSTRATE_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_CYAN.AVE_DE) , 0) ) AS CYAN_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_CYAN.AVE_DH)) , 0) ) AS CYAN_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_MAGENTA.AVE_DE) , 0) ) AS MAGENTA_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_MAGENTA.AVE_DH)) , 0) ) AS MAGENTA_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_YELLOW.AVE_DE) , 0) ) AS YELLOW_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_YELLOW.AVE_DH)) , 0) ) AS YELLOW_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_BLACK.AVE_DE) , 0) ) AS BLACK_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_BLACK.AVE_DH)) , 0) ) AS BLACK_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_GRAY.AVE_DE) , 0) ) AS GRAY_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_GRAY.AVE_DH)) , 0) ) AS GRAY_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.AVEHumidity) , 0) ) AS HUMIDITY_AVE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.STDHumidity) , 0) ) AS HUMIDITY_STD 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.AVETemp) , 0) ) AS TEMP_AVE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.STDTemp) , 0) ) AS TEMP_STD 
	FROM 
		Branch 
		INNER JOIN CPDongle ON CPDongle.BranchID = Branch.BranchID
		INNER JOIN CPDevice CPD ON CPD.CPDongleID = CPDongle.CPDongleID

		LEFT OUTER JOIN CPMeasurement PMD ON PMD.CPDeviceID = CPD.CPDeviceID 
			AND PMD.CPMeasurementGUID IN (SELECT xmlCPMeasurementGUIDs.value('.', 'VARCHAR(40)') AS CPMeasurementGUID FROM @xmlCPMeasurementGUIDs.nodes('GUID') d(xmlCPMeasurementGUIDs))
		
		LEFT OUTER  JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID  

		LEFT OUTER  JOIN CPV.vwAllSubstratePatches AS CP_SUBSTRATE ON  CP_SUBSTRATE.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllCyanPatches AS CP_CYAN ON  CP_CYAN.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllMagentaPatches AS CP_MAGENTA ON  CP_MAGENTA.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllYellowPatches AS CP_YELLOW ON  CP_YELLOW.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllBlackPatches AS CP_Black ON  CP_Black.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllGrayPatches AS CP_GRAY ON  CP_GRAY.MeasurementGUID = PMD.CPMeasurementGUID  			

		LEFT OUTER JOIN 
						(
							SELECT 
								xmlTemp.value('@CPDeviceID', 'INT') AS CPDeviceID 
								, xmlTemp.value('@AVEHumidity', 'DECIMAL(9,2)') AS AVEHumidity , xmlTemp.value('@STDHumidity', 'DECIMAL(9,2)') AS STDHumidity
								, xmlTemp.value('@AVETemp', 'DECIMAL(9,2)') AS AVETemp , xmlTemp.value('@STDTemp', 'DECIMAL(9,2)') AS STDTemp
							FROM @xmlTemp.nodes('row') d(xmlTemp) 
						) AS vwTemperature ON vwTemperature.CPDeviceID = CPD.CPDeviceID  

	WHERE 
		Branch.CompanyID = @CompanyID	
		AND 1 = ( CASE @BranchID WHEN 0 THEN 1 ELSE CASE WHEN Branch.BranchID = @BranchID THEN 1  ELSE 0 END END) 
	GROUP BY Branch.BranchID , Branch.BranchName 
	ORDER BY Branch.BranchName;

	/* ---------- Gets the records for the Device ---------- */
	SELECT 
		CPD.CPDeviceID , CPD.CPDeviceDesc , Branch.BranchID , PMD.CPMeasurementTypeID 
		, COUNT(DISTINCT PMD.CPMeasurementGUID) AS Reads 
		, SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors 
		, CONVERT( DECIMAL(9,2) , (CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(9,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(9,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) ) AS ErrorRate 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(ACT.AVERAGE) , 0) ) AS AVG_DE , CONVERT( DECIMAL(9,2) , ISNULL(MAX(ACT.PatchdEValue) , 0 ) ) AS MAX_DE 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(ACT.Dens_AV_Neg) ,  0 ) ) AS AVG_Delta_NEG , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ACT.Dens_AV_POS ) , 0 ) ) AS AVG_Delta_POS 
		, CONVERT( DECIMAL(9,2) , ISNULL(MIN(ACT.Dens_Max_Neg_Val) , 0) ) AS MAX_Delta_NEG , CONVERT( DECIMAL(9,2) , ISNULL(MAX(ACT.Dens_Max_Pos_Val) , 0) ) AS MAX_Delta_POS 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_SUBSTRATE.AVE_DE) , 0) ) AS SUBSTRATE_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_SUBSTRATE.AVE_DH)) , 0) ) AS SUBSTRATE_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_CYAN.AVE_DE) , 0) ) AS CYAN_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_CYAN.AVE_DH)) , 0) ) AS CYAN_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_MAGENTA.AVE_DE) , 0) ) AS MAGENTA_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_MAGENTA.AVE_DH)) , 0) ) AS MAGENTA_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_YELLOW.AVE_DE) , 0) ) AS YELLOW_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_YELLOW.AVE_DH)) , 0) ) AS YELLOW_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_BLACK.AVE_DE) , 0) ) AS BLACK_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_BLACK.AVE_DH)) , 0) ) AS BLACK_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(CP_GRAY.AVE_DE) , 0) ) AS GRAY_DE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(ABS(CP_GRAY.AVE_DH)) , 0) ) AS GRAY_DH 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.AVEHumidity) , 0) ) AS HUMIDITY_AVE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.STDHumidity) , 0) ) AS HUMIDITY_STD 
		, CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.AVETemp) , 0) ) AS TEMP_AVE , CONVERT( DECIMAL(9,2) , ISNULL(AVG(vwTemperature.STDTemp) , 0) ) AS TEMP_STD 
	FROM CPDevice CPD 
		
		INNER JOIN CPDongle ON CPDongle.CPDongleID = CPD.CPDongleID 
		INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID 
		
		LEFT OUTER JOIN CPMeasurement PMD ON PMD.CPDeviceID = CPD.CPDeviceID 
			AND PMD.CPMeasurementGUID IN (SELECT xmlCPMeasurementGUIDs.value('.', 'VARCHAR(40)') AS CPMeasurementGUID FROM @xmlCPMeasurementGUIDs.nodes('GUID') d(xmlCPMeasurementGUIDs))

		LEFT OUTER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID 
		
		LEFT OUTER  JOIN CPV.vwAllSubstratePatches AS CP_SUBSTRATE ON  CP_SUBSTRATE.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllCyanPatches AS CP_CYAN ON  CP_CYAN.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllMagentaPatches AS CP_MAGENTA ON  CP_MAGENTA.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllYellowPatches AS CP_YELLOW ON  CP_YELLOW.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllBlackPatches AS CP_Black ON  CP_Black.MeasurementGUID = PMD.CPMeasurementGUID  			
		LEFT OUTER  JOIN CPV.vwAllGrayPatches AS CP_GRAY ON  CP_GRAY.MeasurementGUID = PMD.CPMeasurementGUID  			

		LEFT OUTER JOIN 
						(
							SELECT 
								xmlTemp.value('@CPDeviceID', 'INT') AS CPDeviceID 
								, xmlTemp.value('@AVEHumidity', 'DECIMAL(9,2)') AS AVEHumidity , xmlTemp.value('@STDHumidity', 'DECIMAL(9,2)') AS STDHumidity
								, xmlTemp.value('@AVETemp', 'DECIMAL(9,2)') AS AVETemp , xmlTemp.value('@STDTemp', 'DECIMAL(9,2)') AS STDTemp
							FROM @xmlTemp.nodes('row') d(xmlTemp) 
						) AS vwTemperature ON vwTemperature.CPDeviceID = CPD.CPDeviceID  

	WHERE 
		Branch.CompanyID = @CompanyID
	AND 1 = ( CASE @BranchID WHEN 0 THEN 1 ELSE CASE WHEN Branch.BranchID = @BranchID THEN 1  ELSE 0 END END) 		
	GROUP BY CPD.CPDeviceID , CPD.CPDeviceDesc , Branch.BranchID , PMD.CPMeasurementTypeID 
	ORDER BY CPD.CPDeviceDesc; 
	
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
