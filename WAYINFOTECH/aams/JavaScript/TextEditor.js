
var type;
function button_over(eButton)
	{
		eButton.style.backgroundColor =	"LightSlateGray";
		eButton.style.borderColor =	"darkblue darkblue darkblue	darkblue";
	}
function button_out(eButton)
	{
		eButton.style.backgroundColor =	"threedface";
		eButton.style.borderColor =	"threedface";
	}
function button_down(eButton)
	{
		eButton.style.backgroundColor =	"#8494B5";
		eButton.style.borderColor =	"darkblue darkblue darkblue	darkblue";
	}
function button_up(eButton)
	{
		eButton.style.backgroundColor =	"#B5BDD6";
		eButton.style.borderColor =	"darkblue darkblue darkblue	darkblue";
		eButton	= null;	
	}
var	isHTMLMode=false
function document.onreadystatechange()
	{
	try
	{
		NewsBody_rich.document.designMode="On";		
		}
		catch(err){}
	}
function cmdExec(cmd,opt) 
	{
		if (isHTMLMode)
		{
			alert("Formatting happens only in Normal mode");
			return;
		}
		NewsBody_rich.focus();
		NewsBody_rich.document.execCommand(cmd,"",opt);
		NewsBody_rich.focus();
	} // end cmdExec

function ChangeImage (ImageName,FileName) {
document[ImageName].src = FileName;
}

var intHCount = 0;
var intNCount = 0;

function setMode(Nbut, Hbut) 
	{
		var	sTmp;
		isHTMLMode = Hbut;
		isNormalMode = Nbut;

		if(Hbut == 1)
		{
			intHCount++;
			intNCount = 0;
		}

		if(Nbut == 1)
		{
			intNCount++;
			intHCount = 0;
		}	

		if (isHTMLMode && intHCount == 1)
		{
			sTmp=NewsBody_rich.document.body.innerHTML;NewsBody_rich.document.body.innerText=sTmp;
			ChangeImage ('HTMLbut','Img/HTML_on.jpg');
			ChangeImage ('Normalbut','Img/Normal_off.jpg'); return true;
		}
		if(isNormalMode && intNCount == 1)
		{
			sTmp=NewsBody_rich.document.body.innerText;NewsBody_rich.document.body.innerHTML=sTmp;
			ChangeImage ('HTMLbut','Img/HTML_off.jpg');
			ChangeImage ('Normalbut','Img/Normal_on.jpg'); return true;
		}
		NewsBody_rich.focus();
	} // end setMode
	
function createLink()
	{
		if (isHTMLMode){alert("Please uncheck 'Edit	HTML'");return;}
		cmdExec("CreateLink");
	} // end createLink
	
function foreColor()
	{
		var	arr	= showModalDialog("../Helpdesk/ColorPalette.htm","","font-family:Verdana;	font-size:12; dialogWidth:18;	dialogHeight:18" );
		if (arr	!= null) cmdExec("ForeColor",arr);	
	} // end foreColor

function setImage()	
{ 
	var	imgSrc = " ";
	var	flag = 0;	
	while(imgSrc ==	" ")
	{
		imgSrc = prompt('Enter image location with image name ', ''); 
		if (imgSrc == "	") alert("Please enter the required	URL");
		if(imgSrc != "")
		{
			cmdExec('insertimage', imgSrc);	
		}
	} // end while
} // end setImage
function ShowCM(t,headerText)
	{
	NewsBody_rich.document.designMode='On';
	if (divContent.style.display=="block")
		{
			divContent.style.display="none";
		}
	else
		{
			divContent.style.display="block";
		}
	type=t;
	spnHeader.innerText=headerText;
	if(document.getElementById(type).value != '')
		{
		NewsBody_rich.document.body.innerHTML=document.getElementById(type).value;
		}
	}
	
function ClearIFrame()
	{
	NewsBody_rich.document.body.innerHTML ='';
	divContent.style.display="none";
	}
function DisableSave(bool)
	{
		document.Form1.btnSaveHead.style.display="none";
		if(bool=='show')
		{	
			if(document.Form1.btnSaveHead.style.display=="none")
			{
				document.Form1.btnSaveHead.style.display="block";
			}
			else
			{
				document.Form1.btnSaveHead.style.display="none";
			}
		}
	}
