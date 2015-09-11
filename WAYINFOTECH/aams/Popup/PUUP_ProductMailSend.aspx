<%@ Page  ValidateRequest ="false" Language="VB" AutoEventWireup="false" CodeFile="PUUP_ProductMailSend.aspx.vb" Inherits="Popup_PUUP_ProductMailSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <base target="_self"/>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <script language="javascript" type="text/javascript" src="../JavaScript/TextEditor.js"></script>
    <script language="javascript" type="text/javascript" >
    function TextContent()
    {
         try
         {
        
            NewsBody_rich.document.body.innerHTML=document.getElementById("DivBody").innerHTML;
			self.focus();		
			
    	}
    	catch(err)
    	{
    	      
    	}
			
    }
    </script>
    <script  id="Js" language ="javascript" type="text/javascript">
     
//     function ResetBodyValue()
//     {
//          document.getElementById("DivBody").innerHTML= NewsBody_rich.document.body.innerHTML;
//          
//     }
     function MailMandatory()
     {
//           if(document.getElementById("txtEmailFrom").value.trim()=="")
//            { 
//                    document.getElementById("lblError").innerHTML='From email Id is mandatory.';
//                    document.getElementById("txtEmailFrom").focus();
//                    return false;           
//            }
//              if(document.getElementById("txtEmailFrom").value!='')
//            {              
//                if(checkEmail(document.getElementById("txtEmailFrom").value)==false)
//                {
//                    document.getElementById("lblError").innerHTML='Enter valid email Id.';
//                    document.getElementById("txtEmailFrom").focus();
//                    return false;
//                }
//            }  
            if(document.getElementById("txtEmailTo").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='To email Id is mandatory.';
                    document.getElementById("txtEmailTo").focus();
                    return false;           
            } 
            
              if(document.getElementById("txtEmailTo").value!='')
            {      
                var strEmail =document.getElementById("txtEmailTo").value 
                          
                 var EmailArr = strEmail.split(','); 
                 // alert("abhishek" + strEmail.split(',').length);
                 var blnWrongEmail=false;
                 
                for (i=0; i<EmailArr.length;i++)
                {
                  //  alert(EmailArr[i]);
                    if(checkEmail(EmailArr[i])==false)
                    {                      
                        blnWrongEmail= true;
                        break;                       
                    }
                }
                if (blnWrongEmail==true)
                {
                        document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                        document.getElementById("txtEmailTo").focus();
                         return false;     
                }
            }  
         
           // alert(document.getElementById("hdRequestId").value);
            if (document.getElementById("hdRequestId").value!='' )
            {
                            if(document.getElementById("txtCC").value!='')
                {      
                    var strEmail =document.getElementById("txtCC").value 
                              
                     var EmailArr = strEmail.split(','); 
                     // alert("abhishek" + strEmail.split(',').length);
                     var blnWrongEmail=false;
                     
                    for (i=0; i<EmailArr.length;i++)
                    {
                      //  alert(EmailArr[i]);
                        if(checkEmail(EmailArr[i])==false)
                        {                      
                            blnWrongEmail= true;
                            break;                       
                        }
                    }
                    if (blnWrongEmail==true)
                    {
                            document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                            document.getElementById("txtCC").focus();
                             return false;     
                    }
                }  
            
                       if(document.getElementById("txtBCC").value!='')
                {      
                    var strEmail =document.getElementById("txtBCC").value 
                              
                     var EmailArr = strEmail.split(','); 
                     // alert("abhishek" + strEmail.split(',').length);
                     var blnWrongEmail=false;
                     
                    for (i=0; i<EmailArr.length;i++)
                    {
                      //  alert(EmailArr[i]);
                        if(checkEmail(EmailArr[i])==false)
                        {                      
                            blnWrongEmail= true;
                            break;                       
                        }
                    }
                    if (blnWrongEmail==true)
                    {
                            document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                            document.getElementById("txtBCC").focus();
                             return false;     
                    }
                }  
            }


             if(document.getElementById("txtSub").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='Subject is mandatory.';
                    document.getElementById("txtSub").focus();
                    return false;           
            } 
//               if(document.getElementById("txtbody").value.trim()=="")
//            { 
//                    document.getElementById("lblError").innerHTML='Body is mandatory.';
//                    document.getElementById("txtbody").focus();
//                    return false;           
//            } 
                document.getElementById("hdnmsg").value= NewsBody_rich.document.body.innerHTML;
     }
     
     function  ResetValueofBody()
     {
         document.getElementById("hdnmsg").value= NewsBody_rich.document.body.innerHTML;
     }
     
       
    </script>
</head>
<body  >
    <form id="form1" runat="server"  style ="height:100%">
     <table  border="0" >
            <tr>
                <td valign="top" style="width:100%; height:100%"  >
                    <table style="width:100%; height:100%" cellpadding="0" cellspacing ="0" >
                        <tr>
                            <td valign="top" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Send Mail&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right"><asp:HiddenField id="hdRequestId" Value =""  runat="server"></asp:HiddenField><asp:HiddenField ID="hdFrom" runat ="server" /><a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;</td>
                        </tr>   
                        <tr>
                            <td valign="top" style="width:100%;" >
                                <table  style="width:100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                       <td align="LEFT" class="redborder"style="width:100%">                                 
                                                <table style="width:100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center"  valign="TOP"><asp:Label ID="lblError" runat="server"  CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                              <td  colspan="7" ></td>
                                                            </tr>
                                                            <tr>
                                                                <td  class="textbold" style="height: 58px"></td>                                                               
                                                                <td class="textbold" style="width:65px; height: 58px;" ><span class ="textbold"><b style="width: 90px">&nbsp;To</b></span></td>
                                                                <td style="width: 451px; height: 58px;" >
                                                                     <asp:TextBox ID="txtEmailTo" runat="server" MaxLength="1000" TabIndex="2" TextMode="MultiLine"
                                                                         Width="566px" Height="50px"></asp:TextBox></td>
                                                                <td style="height: 58px; width: 17px;"></td>
                                                                 <td class="textbold" style="width: 4px; height: 58px;" ><asp:Button  CssClass ="button" ID="btnSendMail" runat="server" Text="Send Mail" TabIndex="7" />&nbsp;</td>
                                                                  <td class="textbold" style="width: 4px; height: 58px;" ></td>
                                                                <td class="textbold" style="height: 58px" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP"></td>
                                                            </tr> 
                                                           <tr>
                                                             <td class="textbold"></td>                                                            
                                                                <td colspan ="6" style ="width:100%">
                                                                      <table>
                                                                          <asp:Panel ID="pnlAttach" runat ="server" >
                                                                               <tr>
                                                                                         <td class="textbold" style="width:65px; " ><span class ="textbold"><b style="width: 90px">CC</b></span></td>
                                                                                      <td  >
                                                                                     <asp:TextBox id="txtCC" tabIndex="3" runat="server" Width="565px" MaxLength="200"></asp:TextBox></td>
                                                                              </tr>  
                                                                                <tr>
                                                                                         <td class="textbold" style="width:65px; " ><span class ="textbold"><b style="width: 90px"></b></span></td>
                                                                                      <td  >
                                                                                    </td>
                                                                              </tr>   
                                                                                <tr>
                                                                                         <td class="textbold" style="width:65px; " ><span class ="textbold"><b style="width: 90px">BCC</b></span></td>
                                                                                      <td  >
                                                                                    <asp:TextBox id="txtBCC" tabIndex="4" runat="server" Width="565px" MaxLength="200"></asp:TextBox></td>
                                                                              </tr>   
                                                                                <tr>
                                                                                         <td class="textbold" style="width:65px;" ><span class ="textbold"><b style="width: 90px"></b></span></td>
                                                                                      <td  >
                                                                                    </td>
                                                                              </tr>                                                                             
                                                                               <tr>
                                                                                         <td class="textbold" style="width:65px; " ><span class ="textbold"><b style="width: 90px">Attachment</b></span></td>
                                                                                      <td  ><asp:FileUpload ID="FileAttach" runat ="server"  Width="565px" TabIndex ="5" />
                                                                                   </td>
                                                                              </tr> 
                                                                                <tr>
                                                                                         <td class="textbold" style="width:65px; " ><span class ="textbold"><b style="width: 90px"></b></span></td>
                                                                                      <td  >
                                                                                         
                                                                                   </td>
                                                                              </tr>   
                                                                              <tr>
                                                                                         <td class="textbold" style="width:65px;" ><span class ="textbold"><b style="width: 90px"></b></span></td>
                                                                                      <td  ><asp:Button id="btnUpLoad" tabIndex=7 runat="server" CssClass="button" Text="Upload"  ></asp:Button>
                                                                                    </td>
                                                                              </tr> 
                                                                              
                                                                              <tr>
                                                                                         <td class="textbold" style="width:65px;" ><span class ="textbold"><b style="width: 90px"></b></span></td>
                                                                                      <td  >
                                                                                          <asp:ListBox ID="lstAttachment" runat="server" Width ="200px"></asp:ListBox>  &nbsp;&nbsp;&nbsp;<asp:Button id="btnRemove" tabIndex=7 runat="server" CssClass="button" Text="Remove File"  ></asp:Button>
                                                                                    </td>
                                                                              </tr>                                                                                       
                                                                          </asp:Panel>
                                                                      </table>  
                                                                </td>                                                             
                                                            </tr> 
                                                              <tr>
                                                                <td  class="textbold" ></td>                                                              
                                                                <td class="textbold" style="width: 75px"  >
                                                                    &nbsp;<b>Subject</b></td>
                                                                 <td class="textbold" style="width: 451px"   >
                                                                     <asp:TextBox ID="txtSub" runat="server" MaxLength="200" TabIndex="6"
                                                                         Width="565px"></asp:TextBox></td>
                                                                 <td style="width: 17px" ></td>
                                                                  <td class="textbold" style="width: 4px">
                                                                      &nbsp;</td>
                                                                  <td class="textbold"  ></td>
                                                                  <td class="textbold" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP">   </td>
                                                            </tr> 
                                                              <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP">   </td>
                                                            </tr> 
                                                              <tr>
                                                                <td  class="textbold" colspan="7" align="center" valign="TOP">   <div id="divContent">
                                                                <asp:Panel ID="pnlContentMgmt" runat="server" Width="100%">
                                                                    <table style="width: 100%; border-collapse: collapse; height: 77px" bordercolor="black"
                                                                        cellspacing="0" cellpadding="0" width="500" align="left" bgcolor="buttonface"
                                                                        border="1">
                                                                        <tr>
                                                                            <td height="20">
                                                                                <table height="18" cellspacing="1" cellpadding="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                              <img height="21" src="../Images/VertLine2.jpg" width="10">
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('createLink')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Link" hspace="2" src="../Images/Link.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine2.jpg" width="10">
                                                                                            </td> 
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('Unlink')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Unlink" hspace="2" src="../Images/unlink.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10">
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('bold')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Bold" hspace="1" src="../Images/Bold.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('italic')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Italic" hspace="1" src="../Images/Italic.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('underline')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Underline" hspace="1" src="../Images/Under.gif" width="20" align="absMiddle"
                                                                                                    vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10"></td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="cmdExec('RemoveFormat')" onmouseout="button_out(this);">
                                                                                                <img height="19" alt="Remove Format" hspace="2" src="../Images/UnSelect.gif" width="20"
                                                                                                    align="absMiddle" vspace="1">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                                onclick="foreColor()" onmouseout="button_out(this);">
                                                                                                <img id="BtnColor" height="19" alt="Forecolor" hspace="2" src="../Images/fgcolor.gif" width="20"
                                                                                                    align="absMiddle" vspace="1"></div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine.jpg" width="10"></td>
                                                                                        <td>
                                                                                            <select style="width: 80px" onchange="cmdExec('fontname',this[this.selectedIndex].value);"
                                                                                                name="selfontname">
                                                                                                <option selected>Font</option>
                                                                                                <option value="Arial">Arial</option>
                                                                                                <option value="Arial Black">Arial Black</option>
                                                                                                <option value="Arial Narrow">Arial Narrow</option>
                                                                                                <option value="Comic Sans MS">Comic Sans MS</option>
                                                                                                <option value="Courier New">Courier New</option>
                                                                                                <option value="System">System</option>
                                                                                                <option value="Tahoma">Tahoma</option>
                                                                                                <option value="Times">Times</option>
                                                                                                <option value="Verdana">Verdana</option>
                                                                                                <option value="Wingdings">Wingdings</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="cmdExec('fontsize',this[this.selectedIndex].value);" name="selfontsize">
                                                                                                <option selected>Size</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                                <option value="5">5</option>
                                                                                                <option value="6">6</option>
                                                                                                <option value="7">7</option>
                                                                                                <option value="8">8</option>
                                                                                                <option value="9">9</option>
                                                                                                <option value="10">10</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <img height="21" src="../Images/VertLine2.jpg" width="10"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="mam" height="380">
                                                                                <iframe id="NewsBody_rich" src="../Popup/MsgBody.htm" width="100%" height="100%" font-name="Verdana" onload ="TextContent()">
                                                                                </iframe>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                </div>   </td>
                                                            </tr>                                                            
                                                             <tr>                                                              
                                                               <td class="textbold"   valign ="Top"></td>
                                                                <td  valign ="Top" colspan ="6"  align ="left" >
                                                                <div id="DivBody" runat ="server" style="height:390px;width:700px; padding-top:5px; " class ="displayNone" ></div>
                                                               <%--<asp:TextBox ID="txtbody" runat="server" Height="390px" MaxLength="8000" TextMode="MultiLine" Width="700px" EnableViewState="False" visible="false" ></asp:TextBox>--%></td>
                                                            </tr>
                                                            <tr>
                                                              <td colspan ="7" style="height:5px;"><asp:TextBox ID="txtbody" runat="server" Height="1px" MaxLength="8000" TextMode="MultiLine" Width="700px" EnableViewState="False" visible="false" ></asp:TextBox></td>
                                                            </tr>
                                               </table> 
                                      </td>
                                    </tr>
                                </table>
                                <input id="hdnmsg" runat="server" name="hdnmsg" type="hidden" />
                                <input id="hdnmsg2" runat="server" name="hdnmsg2" type="hidden" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
