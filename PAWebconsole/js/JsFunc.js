
var pcnt=0;
var page=0;

//set the value for Allow Updates from Creditors that charge fee
function setValue(element)
{
  if (element.checked==true)
    element.value="y";
  else if(element.checked==false)
    element.value="n";
}   

//setStle to the rows on mouseover
function setStyle(row,style)
{
	for(var i=0;i<row.cells.length;i++)
	{
		row.cells(i).className=style;
	}
}

//Highlight the SelectedRow
function highlightRow(row)
{
row=event.srcElement;
while(row.tagName!='TR')
	row=row.parentElement;
if(row.cells(0).className!="selectedLineItem")
 {		
  	setStyle(row,"highlightLineItem");    
 }
 	row.style.cursor="hand"	
}

//set the Selected Row
function setSelectedRow(row)
{
	var tabObject=row;
	if(row!=null)
	{
		while(tabObject.tagName!='TABLE')
			tabObject=tabObject.parentElement;
		tabObject.selectedRow=row;
	}
}

//Normalise all the rows
function normaliseRow(row)
{
row=event.srcElement; 
while(row.tagName!='TR')
	row=row.parentElement;
if(row.cells(0).className!='selectedLineItem')
	   normaliseSelectedRow(row);
}

//Normalise the selected row
function normaliseSelectedRow(row)
{
        setStyle(row,'searchLineItem');

}

function selectRow(row)
{
	
	var element=row;
	while(element.tagName!='TABLE')
		element=element.parentElement;
	if(element.selectedRow!=null)
	{
		normaliseSelectedRow(element.selectedRow);
	}
	while(row.tagName!='TR')
			row=row.parentElement;
	setStyle(row,'selectedLineItem');
	element.selectedRow=row;
}

var secs=0;
var popup=0;
var timerID = null;
var delay = 1000;

function InitializeTimer(ltime,popupDelay)
{    
    secs = ltime * 60;
    self.status = ltime + ' minutes to automatic logout. Thank you for using XPITAX!';
    popup = popupDelay * 60;
    StartTheTimer();
}

function resetTime(ltime){
    secs = ltime * 60;
}

function StartTheTimer()
{   
    if (secs==popup)
    {        
        document.getElementById("transparentDiv").style.display="block";
        secs = secs - 1;
        timerRunning = true;
        self.setTimeout("StartTheTimer()", delay);
    } 
    else if (secs<1)
    {        
        window.location.href="SessionTimeout.aspx";
    }
    else
    {
        if(secs % 60 == 0)
        {
            self.status = (secs/60) + ' minutes to automatic logout. Thank you for using XCM!';            
        }
        secs = secs - 1;
        timerRunning = true;
        timerID = self.setTimeout("StartTheTimer()", delay);
    }
}


var isIE = document.all?true:false;
var isNS = document.layers?true:false;
function onlyDigits(e) 
{
    var _ret = true;
    if (isIE) {
    if (window.event.keyCode < 48 || window.event.keyCode > 57) {
    window.event.keyCode = 0;
    _ret = false;
    }
    }
    if (isNS) {
    if (e.which < 48 || e.which > 57) {
    e.which = 0;
    _ret = false;
    }
    }
    return (_ret); 
}

function onlyFloat(e)
{
	var floatPoint = false;
    var _ret = true;
	for(var i=0; i<=e.value.length; i++)
	{
		var c = e.value.charAt(i);
		if(c == ".")
		{
			floatPoint = true;
			break;
		}
	}

    if (isIE) 
	{
		if(window.event.keyCode == 46)
		{
			if(floatPoint == false)
			{
				_ret = true;
			}
			else
			{
				window.event.keyCode = 0;
				_ret = false
			}
		}
		else
		{
			if (window.event.keyCode < 48 || window.event.keyCode > 57) 
			{
				window.event.keyCode = 0;
				_ret = false;
			}
		}
    }

    if (isNS) 
	{
		if(e.which == 46)
		{
			if(floatPoint == false)
			{
				_ret = true;
			}
			else
			{
				window.event.keyCode = 0;
				_ret = false
			}
		}
		else
		{
			if (e.which < 48 || e.which > 57) 
			{
				e.which = 0;
				_ret = false;
			}
		}
    }
    return (_ret); 
}


function NoSpecialChar()
{ 
  /*  if((event.keyCode>31 && event.keyCode<48) || (event.keyCode>57 && event.keyCode<65) || (event.keyCode>90 && event.keyCode<97) || (event.keyCode>122))
    { 
        event.keyCode=0;
        return false;
    } */
    
    if((event.keyCode== 32))
    { 
        event.keyCode=0;
        return false;
    }
} 

function confirmDelete()
{
    var c = confirm("Are you sure to Delete?");
    return c;
}

//Function to clear the date value 
function clearDate(ele)
{
    document.getElementById(ele).value = '';
}