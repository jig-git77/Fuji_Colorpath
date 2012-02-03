IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressColorChars')
	BEGIN
		DROP  Procedure [Press].[uspGetPressColorChars]
	END

GO

/*    
	AUTHOR : Maulik Dhorajia
	Created Date : Sept 10 2008
	Purpose : Retrieves data for Charts on Color Chars.    
*/    
CREATE PROCEDURE [Press].[uspGetPressColorChars](@JobID INT , @DongleID VARCHAR(10))
AS      
BEGIN    
	BEGIN TRY    
	
	-- ========== Gets the Gaumat View ========== 
	SELECT 
		(CASE 
			WHEN Colors.ColorName LIKE 'Cyan%(%)+Yellow%(%)' THEN 2
			WHEN Colors.ColorName LIKE 'Magenta%(%)+Yellow%(%)'THEN 4
			WHEN Colors.ColorName LIKE 'Cyan%(%)+Magenta%(%)'  THEN 6
			WHEN Colors.ColorName LIKE 'Cyan%(%)' THEN 1 				
			WHEN Colors.ColorName LIKE 'Yellow%(%)' THEN 3				
			WHEN Colors.ColorName LIKE 'Magenta%(%)' THEN 5						
			ELSE 8
		END)  as Rank
		, Colors.ColorID
		, Colors.ColorName	
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1		
		, ISNULL( AVG(PCalc.CIELAB_L) , 0 ) AS ACTUAL_L , ISNULL( AVG(PCalc.CIELAB_A) , 0 ) AS ACTUAL_A , ISNULL( AVG(PCalc.CIELAB_B) , 0 ) AS ACTUAL_B
		, ISNULL( AVG(CTT.CIELAB_L) , 0 ) AS TARGET_L , ISNULL( AVG(CTT.CIELAB_A) , 0 ) AS TARGET_A , ISNULL( AVG(CTT.CIELAB_B) , 0 ) AS TARGET_B
	FROM 
		Press.Colors AS Colors

	LEFT OUTER JOIN Press.InkWells AS IWells ON Colors.ColorName LIKE '%'+ IWells.InkName +'%' 
		AND IWells.IsUsed = 1 
		AND IWells.JobID = Colors.JobID AND IWells.DongleID = Colors.DongleID
		AND IWells.PrimaryColorType IN (1,2,3)

	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1

	LEFT OUTER JOIN Press.ColorTarTol AS CTT ON CTT.ColorID = Colors.ColorID 
		AND CTT.JobID = Colors.JobID AND CTT.DongleID = Colors.DongleID

	WHERE 
		Colors.JobID = @JobID AND Colors.DongleID = @DongleID
		AND Colors.ColorTypeId IN (0,2,5)
		AND Colors.ColorName NOT LIKE 'Black%'
		AND Colors.ColorName NOT LIKE 'Pan%'
	GROUP BY 
		Colors.ColorID , Colors.ColorName
	ORDER BY
		Rank

-- ========== Gets LAB value for 25% WITHOUT ANY COLOR DISTINCTION ========== 
	--Quarter Tone Gray Balance
	SELECT 
		ISNULL( CTT.CIELAB_L , 0 ) As Target_L , ISNULL(CTT.CIELAB_A,0) As Target_A , ISNULL(CTT.CIELAB_B,0) As Target_B 
		, ISNULL( AVG(PCalc.CIELAB_L) , 0 ) As Actual_L , ISNULL( AVG(PCalc.CIELAB_A) , 0 ) As Actual_A ,ISNULL( AVG(PCalc.CIELAB_B) , 0 ) As Actual_B 
	FROM 
		Press.PatchCalculations AS PCalc
		
	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = PCalc.JobID AND PCalc.DongleID = PCalc.DongleID
		AND Sheets.IsReady = 1

	INNER JOIN Press.Colors AS Colors ON Colors.ColorID = PCalc.ColorID 
		AND Colors.JobID = PCalc.JobID AND Colors.DongleID = PCalc.DongleID

	LEFT OUTER JOIN Press.ColorTarTol AS CTT ON CTT.ColorID = Colors.ColorID 
		AND CTT.JobID = PCalc.JobID AND CTT.DongleID = PCalc.DongleID

	WHERE 
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		AND Colors.ColorTypeID = 6
		AND SUBSTRING(Colors.ColorName, CHARINDEX('_',Colors.ColorName)+1,2) IN ('25','20')
	GROUP BY  
		CTT.CIELAB_L , CTT.CIELAB_A , CTT.CIELAB_B
	
 -- ========== Gets LAB value for 50% WITHOUT ANY COLOR DISTINCTION ========== 
	--Half Tone Gray Balance
	SELECT 
		ISNULL( CTT.CIELAB_L , 0 ) As Target_L , ISNULL(CTT.CIELAB_A,0) As Target_A , ISNULL(CTT.CIELAB_B,0) As Target_B 
		, ISNULL( AVG(PCalc.CIELAB_L) , 0 ) As Actual_L , ISNULL( AVG(PCalc.CIELAB_A) , 0 ) As Actual_A ,ISNULL( AVG(PCalc.CIELAB_B) , 0 ) As Actual_B 
	FROM 
		Press.PatchCalculations AS PCalc
		
	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = PCalc.JobID AND PCalc.DongleID = PCalc.DongleID
		AND Sheets.IsReady = 1

	INNER JOIN Press.Colors AS Colors ON Colors.ColorID = PCalc.ColorID 
		AND Colors.JobID = PCalc.JobID AND Colors.DongleID = PCalc.DongleID

	LEFT OUTER JOIN Press.ColorTarTol AS CTT ON CTT.ColorID = Colors.ColorID 
		AND CTT.JobID = PCalc.JobID AND CTT.DongleID = PCalc.DongleID

	WHERE 
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		AND Colors.ColorTypeID = 6
		AND SUBSTRING(Colors.ColorName, CHARINDEX('_',Colors.ColorName)+1,2) IN ('50')
	GROUP BY  
		CTT.CIELAB_L , CTT.CIELAB_A , CTT.CIELAB_B

 -- ========== Gets LAB value for 75% WITHOUT ANY COLOR DISTINCTION ========== 
	--Three Quarter Tone Gray Balance
	SELECT 
		ISNULL( CTT.CIELAB_L , 0 ) As Target_L , ISNULL(CTT.CIELAB_A,0) As Target_A , ISNULL(CTT.CIELAB_B,0) As Target_B 
		, ISNULL( AVG(PCalc.CIELAB_L) , 0 ) As Actual_L , ISNULL( AVG(PCalc.CIELAB_A) , 0 ) As Actual_A ,ISNULL( AVG(PCalc.CIELAB_B) , 0 ) As Actual_B 
	FROM 
		Press.PatchCalculations AS PCalc
		
	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = PCalc.JobID AND PCalc.DongleID = PCalc.DongleID
		AND Sheets.IsReady = 1

	INNER JOIN Press.Colors AS Colors ON Colors.ColorID = PCalc.ColorID 
		AND Colors.JobID = PCalc.JobID AND Colors.DongleID = PCalc.DongleID

	LEFT OUTER JOIN Press.ColorTarTol AS CTT ON CTT.ColorID = Colors.ColorID 
		AND CTT.JobID = PCalc.JobID AND CTT.DongleID = PCalc.DongleID

	WHERE 
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		AND Colors.ColorTypeID = 6
		AND SUBSTRING(Colors.ColorName, CHARINDEX('_',Colors.ColorName)+1,2) IN ('75','70')
	GROUP BY  
		CTT.CIELAB_L , CTT.CIELAB_A , CTT.CIELAB_B

--	========== Gets the Print Details [Colors.ColorTypeID = 0] ========== 
	SELECT
		(CASE WHEN AVG(IWells.PrimaryColorType) IS NULL THEN 100 WHEN AVG(IWells.PrimaryColorType) = 0 THEN 4 ELSE AVG(IWells.PrimaryColorType) END) AS Rank
		, Colors.ColorName
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1
		, ISNULL( AVG(PCalc.Density) , 0 ) AS AvgDensity		
	
	FROM
		Press.InkWells AS IWells		

	INNER JOIN Press.Colors AS Colors ON Colors.ColorName = IWells.InkName 
		AND Colors.ColorTypeID = 0
		AND Colors.JobID = @JobID AND Colors.DongleID = @DongleID 		
	
	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1

	WHERE
		IWells.IsUsed = 1 
		AND IWells.JobID = Colors.JobID AND IWells.DongleID = Colors.DongleID	
	GROUP BY 
		Colors.ColorName
--	HAVING 
--		AVG(IWells.PrimaryColorType) IS NOT NULL
	ORDER BY
		Rank

-- ========== Gets the DotGain [Colors.ColorTypeID = 1] ========== 
	SELECT 
		(CASE WHEN AVG(IWells.PrimaryColorType) IS NULL THEN 100 WHEN AVG(IWells.PrimaryColorType) = 0 THEN 4 ELSE AVG(IWells.PrimaryColorType) END) AS Rank
		, Colors.ColorName	
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1		
		, REPLACE( REPLACE( REPLACE( REPLACE(Press.fnGetShortStringForColor(Colors.ColorName) , 'C_' , '') , 'M_' , '' ) , 'Y_' , '' ) , 'B_' , '' ) AS XValue
		, ISNULL( AVG(PCalc.Dotgain) , 0 ) AS AvgDotGain		
	FROM 
		Press.Colors AS Colors

	LEFT OUTER JOIN Press.InkWells AS IWells ON Colors.ColorName LIKE '%'+ IWells.InkName +'%' 
		AND IWells.IsUsed = 1 
		AND IWells.JobID = Colors.JobID AND IWells.DongleID = Colors.DongleID
		
	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1

	WHERE 
		Colors.JobID = @JobID AND Colors.DongleID = @DongleID
		AND Colors.ColorTypeId = 1		
	GROUP BY 
		Colors.ColorName
	ORDER BY
		Rank

		
-- ========== Gets the Average and Standard Deviation Delta H for all ========== 
	SELECT
		 1 AS Rank
		, 'All' AS ColorName
		, 'All' ColorName1		
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.DeltaE) ) , 0 ) AS AvgDe
		, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.DeltaE) )  , 0 ) AS StdevDe
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.DeltaH) )  , 0 ) AS AvgDH
		, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.DeltaH) )  , 0 ) AS StdevDH	
	FROM
		Press.InkWells AS IWells		

	INNER JOIN Press.Colors AS Colors ON Colors.ColorName = IWells.InkName 
		AND Colors.ColorTypeID = 0
		AND Colors.JobID = IWells.JobID AND Colors.DongleID = IWells.DongleID
	
	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1

	WHERE
		IWells.IsUsed = 1 
		AND IWells.JobID = @JobID AND IWells.DongleID = @DongleID	

UNION

	SELECT
		(CASE WHEN AVG(IWells.PrimaryColorType) IS NULL THEN 100 WHEN AVG(IWells.PrimaryColorType) = 0 THEN 5 ELSE AVG(IWells.PrimaryColorType) + 1 END) AS Rank 
		, Colors.ColorName
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1		
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.DeltaE) ) , 0 ) AS AvgDe
		, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.DeltaE) )  , 0 ) AS StdevDe
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.DeltaH) )  , 0 ) AS AvgDH
		, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.DeltaH) )  , 0 ) AS StdevDH	
	FROM 
		Press.Colors AS Colors

	LEFT OUTER JOIN Press.InkWells AS IWells ON IWells.InkName = Colors.ColorName 
		AND IWells.IsUsed = 1 
		AND IWells.JobID = Colors.JobID AND IWells.DongleID = Colors.DongleID
		
	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1

	WHERE 
		Colors.JobID = @JobID AND Colors.DongleID = @DongleID
		AND Colors.ColorTypeId IN (0,5)
	GROUP BY 
		Colors.ColorID , Colors.ColorName , IWells.InkName
	ORDER BY
		Rank

-- ========== Gets the Print Contrast [Added with Chirag's Query] ==========
	SELECT
		(CASE WHEN AVG(IWells.PrimaryColorType) IS NULL THEN 100 WHEN AVG(IWells.PrimaryColorType) = 0 THEN 4 ELSE AVG(IWells.PrimaryColorType) END) AS Rank
		, Colors.ColorName
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1		
		, ISNULL( AVG(PCalc.PrintContrast) , 0 ) AS AvgPrintContrast
	FROM
		Press.Colors AS Colors

	LEFT OUTER JOIN Press.InkWells AS IWells ON Colors.ColorName LIKE '%'+ IWells.InkName +'%' 
		AND IWells.IsUsed = 1 
		AND IWells.JobID = Colors.JobID AND IWells.DongleID = Colors.DongleID
		
	INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorId = Colors.ColorID 
		AND PCalc.JobID = Colors.JobID AND PCalc.DongleID = Colors.DongleID

	INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
		AND Sheets.JobID = Colors.JobID AND Sheets.DongleID = Colors.DongleID
		AND Sheets.IsReady = 1 AND Sheets.SheetState = 2 

	WHERE
		Colors.JobID = @JobID AND IWells.DongleID = @DongleID
		AND Colors.ColorTypeId = 1
		AND (Colors.Colorname LIKE '%_75%'  OR Colors.Colorname LIKE '%_70%' OR Colors.Colorname LIKE '%_80%')	 
	GROUP BY 
		Colors.ColorName
	HAVING
		AVG(PCalc.PrintContrast) > 0
	ORDER BY 
		RANK

-- ========== Gets K NPD & CMY NPD ========== 
	SELECT
		Colors.ColorName
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1
		, REPLACE( REPLACE( REPLACE( REPLACE(Press.fnGetShortStringForColor(Colors.ColorName) , 'C_' , '') , 'M_' , '' ) , 'Y_' , '' ) , 'B_' , '' ) AS XValue
		, AVG(PCalc.Density) AS Density		
		, AVG(IWells.PrimaryColorType) AS PrimaryColorType
	FROM 
		Press.InkWells AS IWells
		
		INNER JOIN Press.Colors ON Colors.ColorName LIKE '%'+ IWells.InkName +'%' 
			AND Colors.ColorTypeID = 1 
			AND Colors.JobID = @JobID AND Colors.DongleID = @DongleID
			
		INNER JOIN Press.PatchCalculations AS PCalc ON PCalc.ColorID = Colors.ColorID  
			AND PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
			
		INNER JOIN Press.Sheets ON Sheets.SheetID = PCalc.SheetID 
			AND Sheets.SheetState = 2 AND Sheets.IsReady = 1 
			AND Sheets.JobID = @JobID AND Sheets.DongleID = @DongleID
			
	WHERE
		IWells.JobID = @JobID AND IWells.DongleID = @DongleID 
		AND IWells.IsUsed = 1
	GROUP BY 
		Colors.ColorName
	ORDER BY
		Colors.ColorName

     
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