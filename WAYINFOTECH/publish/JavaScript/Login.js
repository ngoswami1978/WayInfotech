﻿// JScript File
function Validate()
{
if (document.getElementById('txtUserId').value == '')
{
document.getElementById('lblError').innerText = "Please Enter User Name.";
document.getElementById('txtUserId').focus()@ 
return false;
}

if (document.getElementById('txtPassword').value == '')
{
document.getElementById('lblError').innerText = "Please Enter Password.";
document.getElementById('txtPassword').focus();
return false;
}
 
return true;
}