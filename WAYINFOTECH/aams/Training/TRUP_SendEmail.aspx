<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_SendEmail.aspx.vb"
    Inherits="Training_TRUP_SendEmail" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Travel Agency::Send Mail</title>
    <base target="_self" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../JavaScript/TextEditor.js"></script>

    <script language="javascript" type="text/javascript">
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

    <script  language="javascript" type="text/javascript">
    
    function SetValue(id)
    {
      
      document.getElementById("hdCursorPosition").value=id;
                  
    }
    
     function PopupPage(id)
         {
           
            
            var type;
            type = "../TravelAgency/MSSR_MailGroup.aspx?PopUp=P" ;
            window.open(type,"aaTrainingSendEmail","height=600px,width=920px,top=30,left=20,scrollbars=1,status=1");
   	        return false;	
        }
        
        
        
     function MailMandatory()
     {
 
 try
 {
            if(document.getElementById("txtEmailTo").value.trim()=="")
            { 
                    document.getElementById("lblError").innerHTML='To email Id is mandatory.';
                    document.getElementById("txtEmailTo").focus();
                    return false;           
            }
            
           var strEmailText= document.getElementById("txtEmailTo").value ;
           var arstrEmailText=strEmailText.split(",")
           for(i=0;i<arstrEmailText.length;i++)
           {
               if(checkEmail(arstrEmailText[i])==false)
               {
                  document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                  document.getElementById("txtEmailTo").focus();
                    return false;      
               }
           }
             
           var strEmailCCText= document.getElementById("txtCC").value ;
           var arstrEmailCCText=strEmailCCText.split(",")
           if (arstrEmailCCText.length>1)
           {
               for(i=0;i<arstrEmailCCText.length;i++)
               {
                   if(checkEmail(arstrEmailCCText[i])==false)
                   {
                      document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                      document.getElementById("txtCC").focus();
                        return false;      
                   }
               }
           }
           
           
           var strEmailBccText= document.getElementById("txtBcc").value ;
           var arstrEmailBccText=strEmailBccText.split(",")
           if (arstrEmailBccText.length>1)
           {
               for(i=0;i<arstrEmailBccText.length;i++)
               {
                   if(checkEmail(arstrEmailBccText[i])==false)
                   {
                      document.getElementById("lblError").innerHTML='Enter valid email Id by seperating , ';
                      document.getElementById("txtBcc").focus();
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
         document.getElementById("hdnmsg").value= NewsBody_rich.document.body.innerHTML;
         
        }
        catch(err){}
     }
     
     function  ResetValueofBody()
     {
         document.getElementById("hdnmsg").value= NewsBody_rich.document.body.innerHTML;
     }
     
       
    </script>

</head>
<body>
    <form id="frmTrainingSendEmail" runat="server" defaultfocus="txtEmailTo">
       <table width="860px"  class="border_rightred left">
            <tr>
                <td valign="top" style="width: 100%;">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" align="left">
                            
                            <span class="menu"> Travel Agency -&gt;</span><span class="sub_menu">Send Mail</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Send Mail&nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 100%;">
                                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" style="width: 100%">
                                            <table style="width: 100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 5%" >
                                                    </td>
                                                    <td class="textbold" colspan="5" align="center" >
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="width: 65px;">
                                                        <span class="textbold"><b >&nbsp;To</b></span></td>
                                                    <td colspan="4" >
                                                        <asp:TextBox ID="txtEmailTo" runat="server" TabIndex="2" TextMode="MultiLine"
                                                            Width="566px" Height="50px" onfocus="SetValue(1)"></asp:TextBox>&nbsp;</td>
                                                    <td >
                                                        <asp:Button CssClass="button" ID="btnSendMail" runat="server" Text="Send Mail" TabIndex="7" OnClientClick="return MailMandatory();" /></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td style="width: 65px">
                                                     <span class="textbold"><b >Cc:</b></span>
                                                    </td>
                                                    <td colspan="4" >
                                                     <asp:TextBox ID="txtCC"  runat="server" TabIndex="2" TextMode="MultiLine"
                                                            Width="566px" Height="50px" onfocus="SetValue(2)"></asp:TextBox>
                                                    </td>
                                                    <td >
                                                      <asp:Button CssClass="button" ID="btnEmailGroup" runat="server" Text="Email Group" TabIndex="3" OnClientClick="return PopupPage(1);" /></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td  style="width: 65px;">
                                                     <span class="textbold"><b >Bcc:</b></span>
                                                    </td>
                                                    <td colspan="4" >
                                                     <asp:TextBox ID="txtBcc"  runat="server" TabIndex="2" TextMode="MultiLine"
                                                            Width="566px" Height="50px" onfocus="SetValue(3)"></asp:TextBox>
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                            
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td class="textbold" style="width: 75px">
                                                        &nbsp;<b>Subject</b></td>
                                                    <td class="textbold" style="width: 451px">
                                                        <asp:TextBox ID="txtSub" runat="server" MaxLength="200" TabIndex="6" Width="565px"></asp:TextBox></td>
                                                    <td style="width: 17px">
                                                    </td>
                                                    <td style="width: 4px">
                                                        &nbsp;</td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td colspan="6" >
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td >
                                                    </td>
                                                    <td ></td>
                                                    <td class="textbold" colspan="5" align="left" valign="TOP">
                                                        <div id="divContent">
                                                            <asp:Panel ID="pnlContentMgmt" runat="server" Width="90%">
                                                                <table style="width: 100%; border-collapse: collapse; height: 77px" bordercolor="black"
                                                                    cellspacing="0" cellpadding="0" width="500" align="left" bgcolor="buttonface"
                                                                    border="1">
                                                                    <tr>
                                                                        <td height="20">
                                                                            <table height="18" cellspacing="1" cellpadding="0" border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <img height="21" src="../Images/VertLine2.jpg" width="10" alt="" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('createLink')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Link" hspace="2" src="../Images/Link.gif" width="20" align="absMiddle"
                                                                                                vspace="1"  />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <img height="21" src="../Images/VertLine2.jpg" width="10" alt="" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('Unlink')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Unlink" hspace="2" src="../Images/unlink.gif" width="20" align="absMiddle"
                                                                                                vspace="1" />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <img height="21" src="../Images/VertLine.jpg" width="10" alt="" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('bold')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Bold" hspace="1" src="../Images/Bold.gif" width="20" align="absMiddle"
                                                                                                vspace="1"  />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('italic')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Italic" hspace="1" src="../Images/Italic.gif" width="20" align="absMiddle"
                                                                                                vspace="1"  />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('underline')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Underline" hspace="1" src="../Images/Under.gif" width="20"
                                                                                                align="absMiddle" vspace="1" />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <img height="21" src="../Images/VertLine.jpg" width="10" alt="" /></td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="cmdExec('RemoveFormat')" onmouseout="button_out(this);">
                                                                                            <img height="19" alt="Remove Format" hspace="2" src="../Images/UnSelect.gif" width="20"
                                                                                                align="absMiddle" vspace="1"  />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);"
                                                                                            onclick="foreColor()" onmouseout="button_out(this);">
                                                                                            <img id="BtnColor" height="19" alt="Forecolor" hspace="2" src="../Images/fgcolor.gif"
                                                                                                width="20" align="absMiddle" vspace="1"  /></div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <img height="21" src="../Images/VertLine.jpg" width="10" alt="" /></td>
                                                                                    <td>
                                                                                        <select style="width: 80px" onchange="cmdExec('fontname',this[this.selectedIndex].value);"
                                                                                            name="selfontname">
                                                                                            <option selected= "selected" >Font</option>
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
                                                                                            <option selected= "selected">Size</option>
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
                                                                                        <img height="21" src="../Images/VertLine2.jpg" width="10" alt=""/></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td id="mam" height="380">
                                                                            <iframe id="NewsBody_rich" src="../Popup/MsgBody.htm" width="100%" height="100%"
                                                                                font-name="Verdana" onload="TextContent()"></iframe>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td valign="Top" colspan="6" align="left">
                                                        <div id="DivBody" runat="server" style="height: 390px; width: 700px; padding-top: 5px;"
                                                            class="displayNone">
                                                        </div>
                                                      
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <input id="hdnmsg" runat="server" name="hdnmsg" type="hidden" style="width: 2px" />
                                <input id="hdnmsg2" runat="server" name="hdnmsg2" type="hidden" style="width: 2px"/>
                                <input id="hdCursorPosition" runat="server"  type="hidden" style="width: 2px" value="1" />
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
