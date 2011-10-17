IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'fnGetShortStringForColor')
	BEGIN
		DROP  FUNCTION [Press].[fnGetShortStringForColor]
	END
GO

CREATE FUNCTION [Press].[fnGetShortStringForColor] (@fullstring varchar(1000))
RETURNS VARCHAR(1000)
AS

BEGIN

--declare @fullstring varchar(1000)
DECLARE @longstring VARCHAR(1000)
DECLARE @shortstring VARCHAR(100)
DECLARE @funcstring VARCHAR(1000)
DECLARE @finalstring VARCHAR(1000)
DECLARE @finalstring1 VARCHAR(1000)

set @finalstring = ''

--set @fullstring = 'CYAN C(1.2)_50%+MAGENTA C(1.3)_40%+YELLOW C(1.4)_40%'
set @longstring = @fullstring + '+'

if charindex('_' , @fullstring) = 0 
	BEGIN
		
		SET @finalstring1 = @fullstring;
		
	END
ELSE
	BEGIN
		while (len (@longstring) > 1)
		begin


		set @shortstring = (select substring(@longstring,1,charindex('+',@longstring)-1))

		set @funcstring = (select Press.fnGetSubStrForColor (@shortstring))

		set @finalstring = @finalstring + '+'+ @funcstring  

		set @longstring = (select substring(@longstring,charindex('+',@longstring)+1,len(@longstring)))

		end

		set @finalstring1 = substring(@finalstring,2,len (@finalstring))
	END
	
return @finalstring1
end


--select Press.fnGetShortStringForColor ('CYAN C(1.2)_50%+MAGENTA C(1.3)_40%+YELLOW C(1.4)_40%')
