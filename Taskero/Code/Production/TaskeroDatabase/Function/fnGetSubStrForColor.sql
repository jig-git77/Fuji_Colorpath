IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'fnGetSubStrForColor')
	BEGIN
		DROP  FUNCTION [Press].[fnGetSubStrForColor]
	END
GO

CREATE  FUNCTION [Press].[fnGetSubStrForColor] (@shortstring varchar(50))
RETURNS VARCHAR(50)
AS
BEGIN
DECLARE @string VARCHAR(50)
DECLARE @a VARCHAR(50)
DECLARE @b VARCHAR(50)

set @string = 
	(
		SELECT left(@shortstring,1)+ substring(@shortstring,charindex('_',@shortstring),len(@shortstring))
	)

RETURN @string

END


--select Press.fnGetSubStrForColor ('CYAN C(1.2)_50%')
