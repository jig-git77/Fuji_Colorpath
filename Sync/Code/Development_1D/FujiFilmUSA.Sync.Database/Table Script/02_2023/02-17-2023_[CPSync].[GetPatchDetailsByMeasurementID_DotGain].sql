
/****** Object:  StoredProcedure [CPSync].[GetPatchDetailsByMeasurementID_DotGain]    Script Date: 2/15/2023 11:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CPSync].[GetPatchDetailsByMeasurementID_DotGain]  
@MeasurementID   int   
AS  
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

	SELECT ROW_NUMBER() OVER (ORDER BY [SampleName]) AS SampleID ,*from @tmpDensityVCMY ORDER BY SampleName

END
