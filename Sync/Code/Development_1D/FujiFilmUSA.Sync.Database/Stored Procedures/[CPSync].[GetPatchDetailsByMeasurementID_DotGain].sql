USE [ColorPathSync_Stage]
GO
/****** Object:  StoredProcedure [CPSync].[GetPatchDetailsByMeasurementID_DotGain]    Script Date: 3/17/2023 12:14:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSync].[GetPatchDetailsByMeasurementID_DotGain]  
@MeasurementID   int   
AS  
BEGIN  

	DECLARE @RefChartName NVARCHAR(100);

	SELECT @RefChartName = ChartName FROM [CP].[ReferenceChartType] AS RCT
		INNER JOIN [CPSync].[Measurement] AS M ON M.ReferenceChartTypeID = RCT.ReferenceChartTypeID
		WHERE M.MeasurementID = @MeasurementID

	PRINT @RefChartName

	IF @RefChartName LIKE '%' + 'Linearization' + '%' -----DotGain report generation for Linearization type chart
	BEGIN
		DECLARE @Counter INT
		DECLARE @PageCounter INT 
		DECLARE @MeasurementPatchID int
		DECLARE @FinalCounter int

		SET @Counter = 1
		SET @FinalCounter = 1
		SET @PageCounter = 1

		DECLARE @tmpDensityVCMY Table(
							SampleName NVARCHAR(50),
							Density_V DECIMAL(9,3),
							Density_C DECIMAL(9,3),
							Density_M DECIMAL(9,3),
							Density_Y DECIMAL(9,3)
						);
				
		WHILE (@PageCounter <= 4)
		BEGIN
	
			IF(@PageCounter = 1)
			BEGIN
			 SET @Counter = 1
			 SET @FinalCounter = @Counter + 18
			END
			ELSE IF(@PageCounter = 2)
			BEGIN 
			 SET @Counter = (18*4) + 1
			 SET @FinalCounter = @Counter + 18
			END
			ELSE IF(@PageCounter = 3)
			BEGIN 
			 SET @Counter = (18*8) + 1
			 SET @FinalCounter = @Counter + 18
			END
			ELSE IF(@PageCounter = 4)
			BEGIN 
			 SET @Counter = (18*12) + 1
			 SET @FinalCounter = @Counter + 18
			END

			WHILE ( @Counter < @FinalCounter)
			BEGIN
		
					  SELECT @MeasurementPatchID = [MeasurementPatchID] FROM (SELECT [MeasurementPatchID], ROW_NUMBER() OVER (ORDER BY [MeasurementPatchID]) AS RowNumber 
							FROM [CPSync].[MeasurementPatch] WHERE MeasurementID = @MeasurementID) AS d 
							WHERE RowNumber = @Counter
			
					INSERT INTO @tmpDensityVCMY
									(
										SampleName
										,Density_V
										,Density_C
										,Density_M,
										Density_Y
									)

					 SELECT SampleName , MAX([Density_V]) AS Density_V, MAX([Density_C]) AS Density_C, MAX([Density_M]) AS Density_M, MAX([Density_Y]) AS Density_Y from 

					 (SELECT PARSENAME(REPLACE(SampleName , '_' , '.'),1) AS SampleName , [Density_V] AS Density_V , null AS Density_C , null AS Density_M , null AS Density_Y
					 FROM [CPSync].[MeasurementPatch] where [MeasurementPatchID] = @MeasurementPatchID
					 UNION ALL
					 SELECT PARSENAME(REPLACE(SampleName , '_' , '.'),1) AS SampleName ,  null AS Density_V , [Density_C] AS Density_C , null AS Density_M , null AS Density_Y
					 FROM [CPSync].[MeasurementPatch] where [MeasurementPatchID] = @MeasurementPatchID + 18
					 UNION ALL
					 SELECT PARSENAME(REPLACE(SampleName , '_' , '.'),1) AS SampleName ,  null AS Density_V , null AS Density_C , [Density_M] AS Density_M , null AS Density_Y
					 FROM [CPSync].[MeasurementPatch] where [MeasurementPatchID] = @MeasurementPatchID + 18 + 18
					 UNION ALL
					 SELECT PARSENAME(REPLACE(SampleName , '_' , '.'),1) AS SampleName ,  null AS Density_V , null AS Density_C , null AS Density_M , [Density_Y] AS Density_Y
					 FROM [CPSync].[MeasurementPatch] where [MeasurementPatchID] = @MeasurementPatchID + 18 + 18 + 18
					 )
					 AS a
					GROUP BY SampleName
			
				 SET @Counter  = @Counter  + 1
			END

			SET @PageCounter  = @PageCounter  + 1
		END

		SELECT 'FP790Linearization' AS ChartName, ROW_NUMBER() OVER (ORDER BY [SampleName]) AS SampleID ,*from @tmpDensityVCMY ORDER BY SampleName
	END
	ELSE IF @RefChartName LIKE '%' + 'InkLimit' + '%'  --------DotGain report generation for InkLimit type chart
	BEGIN
		
		SELECT  
			'InkLimit' AS ChartName,
			RC.SampleID, 
			RC.SampleName, 
			MP.[CIELAB_L*] AS LAB_L,
			MP.[CIELAB_a*] AS LAB_A,
			MP.[CIELAB_b*] AS LAB_B
		FROM        CPSync.Measurement AS M INNER JOIN 
					CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID  INNER JOIN
					CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID		
				
		WHERE  M.MeasurementID = @MeasurementID 

		ORDER BY RC.SampleID
	END
	ELSE		------------------For Everything else, don't generate any report
		PRINT 'It doesn''t find'

	

END
