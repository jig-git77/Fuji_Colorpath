IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'fnCalcDotGain')
	BEGIN
		DROP  FUNCTION [CPV].[fnCalcDotGain]
	END
GO

 CREATE FUNCTION [CPV].[fnCalcDotGain]
(
   --Author:Chirag Shah
   --Calculate DotGain 
   --Date: 10/10/2008		
	
   @D_0				decimal(6,2)  --Density of Paper
  ,@D_100			decimal(6,2)  --Density of Solid
  ,@D_Screen     	decimal(6,2)  --Density of Screen Patch
  ,@PercentDot		decimal(6,2)  --% Of Sample Patch			
)
RETURNS decimal(9,4)
AS
BEGIN
	DECLARE @DotGain decimal(18,2)
	SET @DotGain = ((CAST(1 AS DECIMAL(18,9)) - POWER(CAST(10 AS DECIMAL(18,9)),(@D_0 - @D_Screen ))) / (CAST(1 AS DECIMAL(18,9)) - POWER(CAST(10 AS DECIMAL(18,9)),(@D_0 - @D_100)))) * 100 - @PercentDot
	RETURN @DotGain
END
