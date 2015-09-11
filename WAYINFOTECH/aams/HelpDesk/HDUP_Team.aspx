<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_Team.aspx.vb" Inherits="HelpDesk_MSUP_Team"  ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::HelpDesk::Manage Team</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">HelpDesk-&gt;</span><span class="sub_menu">Manage Team</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Team</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">   <table style="width: 100%" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                            <tr>
                                                <td style="width: 16%">
                                                </td>
                                                <td class="textbold" style="width: 18%">
                                                </td>
                                                <td colspan="4" class="gap">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                <td >
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            Team Name<span class="Mandatory" >*</span></td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtTeam" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1" Width="97%"></asp:TextBox></td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            </td>
                                        <td colspan="4" class="textbold">
                                                                    </td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                           </td>
                                        <td style="width: 25%" class="textbold" colspan="4" >
                                         </td>
                                        
                                        <td>
                                                                    </td>
                                                                    <td> <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                            </td>
                                        <td colspan="4" style="height: 23px">
                                                                   </td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                                                    </td>
                                    </tr>
                                    
                                        <tr>
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                            </td>
                                        <td colspan="4" style="height: 21px">
                                         </td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>    
                                            
                                        <tr>                                        
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                           </td>
                                        <td colspan="4" style="height: 23px">
                                         </td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                                                    </td>
                                    </tr>       
                                           
                                             
                                        <tr>                                        
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                          </td>
                                        <td colspan="4" style="height: 23px">
                                         </td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                                                    </td>
                                    </tr>          
                                           <tr>                                        
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                        <td style="width: 18%; height: 21px;" class="textbold">
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                          </td>
                                        <td colspan="4" style="height: 21px">
                                         </td>
                                        <td>
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>      
                                           
                                            
                                       
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td class="ErrorMsg" colspan="5">
                                            Field Marked * are Mandatory</td>
                                        <td>
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                 </table>
                                        </td>
                                    </tr>
                                </table>
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtTeam.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Team name is mandatory.'
            document.getElementById('<%=txtTeam.ClientId%>').focus();
            return false;
        }
//        else
//        {
//            var strValue = document.getElementById('<%=txtTeam.ClientId%>').value
//            reg = new RegExp("^[a-zA-Z ]+$"); 

//            if(reg.test(strValue) == false) 
//            {

//                document.getElementById('<%=lblError.ClientId%>').innerText ='Team name should contain only alphabets.'
//                return false;

//             }
//        }
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtTeam").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>
</html>
