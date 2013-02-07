/*--------------- Function used to check the valid alpho numeric key character for the name value ----------------------*/
function CheckAlphaNumeric(e)
{
    key = (window.event) ? e.keyCode : e.which;
    if ( (key>=65 && key<=90) || (key>=48 && key<=57) || (key>=97 && key<=122) || key == 39 || key == 32 )
    {
        
    }
    else
    {
        RemoveCharKeyValue(e);
    }    
}
/*--------------- Function used to check the valid key character for the phone number ----------------------*/
function CheckNumber(e)
{
    key = (window.event) ? e.keyCode : e.which;
    if ( (key>=48 && key<=57) || key == 40 || key == 41 || key == 32 || key == 45  || key == 47 )
    {
        
    }
    else
    {
        RemoveCharKeyValue(e);
    }    
}
/*--------------- Function used to remove the character and special characters event ----------------------*/
function RemoveCharKeyValue(e)
{
    /*find the key value of the key that is pressed*/
    key = (window.event) ? e.keyCode : e.which; 
    /*check whether the key is pressed with the shift key */
    if ( e.shiftKey && ((key>=58 && key<=126)  || (key>=33 && key<=47)))
    {
        RestoreEvent(e);
    }
    /*if the normal keys are pressed*/
    if ((key>=58 && key<=126) || (key>=42 && key<=47) || key==39  || key==32) 
    {
        RestoreEvent(e);
    }
}
/*--------------- Function to null the current event generated ----------------------*/
function RestoreEvent(e)
{
    if (navigator.appName == "Microsoft Internet Explorer") 
        e.returnValue = null;
    else
        e.preventDefault();
}
/*Function to ensure only numeric value with a single . 'DOT' value ----------------------*/
function CheckDouble(obj,e)
{
    key = (window.event) ? e.keyCode : e.which;
    if ((key>=48 && key<=57) || key == 46 || key == 8 || key == 9 )
    {
        if (key==46)
        {
            var text;
            text=obj.value;
            if(text.toString().indexOf(".")!=-1)
                RestoreEvent(e)
        }
    }
    else
    {
        RemoveCharKeyValue(e);
    }
}
/*--------------- Function to ensure the only numeric value ----------------------*/
function CheckNumeric(e)
{
    key = (window.event) ? e.keyCode : e.which;
    if (key>=48 && key<=57)
    {
    }
    else
    {
        RemoveCharKeyValue(e);
    }
}


function CallPrint(strid)
{
	var prtContent = document.getElementById(strid);
	var WinPrint = window.open('','','letf=1500,top=1500,width=1,height=1,toolbar=0,scrollbars=0,status=0');
	WinPrint.document.write(prtContent.innerHTML);
	WinPrint.document.close();
	WinPrint.focus();
	WinPrint.print();
	WinPrint.close();
	prtContent.innerHTML=strOldOne;
}


