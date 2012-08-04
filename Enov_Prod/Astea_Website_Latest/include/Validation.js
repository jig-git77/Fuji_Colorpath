// JScript File

/*
Functions to limit number of characters entered.
*/
function limitText(limitField, limitNum) 
{
    if(limitField.value.length >limitNum - 1)
    {
        return false;
    }
    else
    { 
        return true;
    } 

}

function preventPaste(limitField,limitNum)
{

    var CanInsertLength; 
    var sData = window.clipboardData.getData("Text");
    var newData ; 

    CanInsertLength = limitNum - limitField.value.length ; 

    if(CanInsertLength <= 0)    
    {
        return false;
    }
    else
    {
        newData = sData.substr(0,CanInsertLength);
        window.clipboardData.setData("Text",newData);
        return true; 
    } 

}

function TabOnEnterKey()
{
/*
    if(event.keyCode==13){
        event.keyCode = 9;
        return;
    }
  */  
    if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)){event.keyCode=9;}
    return;

}


function SwallowEnter()
{
    if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)){event.keyCode=null;}
    return;

}
