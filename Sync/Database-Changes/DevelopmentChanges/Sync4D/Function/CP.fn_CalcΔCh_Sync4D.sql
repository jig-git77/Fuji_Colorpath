
GO
/****** Object:  UserDefinedFunction [CP].[fn_CalcΔCh_Sync4D]    Script Date: 3/10/2021 10:50:08 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER FUNCTION [CP].[fn_CalcΔCh_Sync4D](@Measured_a [float], @Measured_b [float], @Target_a [float], @Target_b [float])
RETURNS [float] WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [ColorpathLib_E].[ColorMath].[Sync4D_DeltaF]