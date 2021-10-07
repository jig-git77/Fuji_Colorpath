USE [ColorPathSync_Stage]
GO
/****** Object:  UserDefinedFunction [CP].[GetPatchTypeID]    Script Date: 9/22/2021 3:48:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [CP].[GetPatchTypeID](
		@CMYK_C decimal(6,2),
		@CMYK_M decimal(6,2),
		@CMYK_Y decimal(6,2),
		@CMYK_K decimal(6,2),
		@Channel_5 decimal(6,2),
		@Channel_6 decimal(6,2),
		@Channel_7 decimal(6,2),
		@Channel_8 decimal(6,2),
		@Channel_9 decimal(6,2))
RETURNS TINYINT
AS
	BEGIN
		DECLARE @PatchTypeID TINYINT
		SET @CMYK_C = ROUND(@CMYK_C,0)
		SET @CMYK_M = ROUND(@CMYK_M,0)
		SET @CMYK_Y = ROUND(@CMYK_Y,0)
		SET @CMYK_K = ROUND(@CMYK_K,0)
		SET @Channel_5 = ROUND(@Channel_5,0)
		SET @Channel_6 = ROUND(@Channel_6,0)
		SET @Channel_7 = ROUND(@Channel_7,0)
		SET @Channel_8 = ROUND(@Channel_8,0)
		SET @Channel_9 = ROUND(@Channel_9,0)

		IF (ISNULL(@Channel_5,0) > 0 OR ISNULL(@Channel_6,0) > 0 OR ISNULL(@Channel_7,0) > 0 OR ISNULL(@Channel_8,0) > 0 OR ISNULL(@Channel_9,0) > 0)
		BEGIN  -----If extra channels has any value then directly assign this patch as 51 ignore
			SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C IS NULL AND CMYK_M IS NULL AND CMYK_Y IS NULL AND CMYK_K IS NULL
		END
		ELSE
		BEGIN
		
			IF EXISTS(SELECT * FROM CP.PatchType WHERE CMYK_C = @CMYK_C AND CMYK_M = @CMYK_M AND CMYK_Y = @CMYK_Y AND CMYK_K = @CMYK_K AND (ISNULL(@Channel_5,0) = 0 AND ISNULL(@Channel_6,0) = 0 AND ISNULL(@Channel_7,0) = 0 AND ISNULL(@Channel_8,0) = 0 AND ISNULL(@Channel_9,0) = 0))
			BEGIN
				SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
					WHERE CMYK_C = @CMYK_C AND CMYK_M = @CMYK_M AND CMYK_Y = @CMYK_Y AND CMYK_K = @CMYK_K
			END
			ELSE
			BEGIN
				--REMAINING CYAN PATCHES
				IF @CMYK_C > 0 AND @CMYK_M = 0 AND @CMYK_Y = 0 AND @CMYK_K = 0 AND (ISNULL(@Channel_5,0) = 0 AND ISNULL(@Channel_6,0) = 0 AND ISNULL(@Channel_7,0) = 0 AND ISNULL(@Channel_8,0) = 0 AND ISNULL(@Channel_9,0) = 0)
				BEGIN
					SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C IS NULL AND CMYK_M = @CMYK_M AND CMYK_Y = @CMYK_Y AND CMYK_K = @CMYK_K
				END
				--REMAINING MAGENTA PATCHES
				ELSE IF @CMYK_C = 0 AND @CMYK_M > 0 AND @CMYK_Y = 0 AND @CMYK_K = 0 AND (ISNULL(@Channel_5,0) = 0 AND ISNULL(@Channel_6,0) = 0 AND ISNULL(@Channel_7,0) = 0 AND ISNULL(@Channel_8,0) = 0 AND ISNULL(@Channel_9,0) = 0)
				BEGIN
					SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C = @CMYK_C AND CMYK_M IS NULL AND CMYK_Y = @CMYK_Y AND CMYK_K = @CMYK_K
				END
				--REMAINING YELLOW PATCHES
				ELSE IF @CMYK_C = 0 AND @CMYK_M = 0 AND @CMYK_Y > 0 AND @CMYK_K = 0 AND (ISNULL(@Channel_5,0) = 0 AND ISNULL(@Channel_6,0) = 0 AND ISNULL(@Channel_7,0) = 0 AND ISNULL(@Channel_8,0) = 0 AND ISNULL(@Channel_9,0) = 0)
				BEGIN
					SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C = @CMYK_C AND CMYK_M = @CMYK_M AND CMYK_Y IS NULL AND CMYK_K = @CMYK_K
				END
				--REMAINING YELLOW PATCHES
				ELSE IF @CMYK_C = 0 AND @CMYK_M = 0 AND @CMYK_Y = 0 AND @CMYK_K > 0 AND (ISNULL(@Channel_5,0) = 0 AND ISNULL(@Channel_6,0) = 0 AND ISNULL(@Channel_7,0) = 0 AND ISNULL(@Channel_8,0) = 0 AND ISNULL(@Channel_9,0) = 0)
				BEGIN
					SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C = @CMYK_C AND CMYK_M = @CMYK_M AND CMYK_Y = @CMYK_Y AND CMYK_K IS NULL
				END
				ELSE
				BEGIN
					SELECT @PatchTypeID = PatchTypeID FROM CP.PatchType
						WHERE CMYK_C IS NULL AND CMYK_M IS NULL AND CMYK_Y IS NULL AND CMYK_K IS NULL
				END
			END
		END
		RETURN @PatchTypeID
	END


