<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_Exercise.aspx.vb" Inherits="Training_TRUP_Exercise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS::HelpDesk::Search Work Order</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    </head>
<body>
    <form id="form1"  defaultfocus="txtTitle" runat="server" defaultbutton="btnSave">
    <table>
    <tr>
    <td>
        <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Training-&gt;</span><span class="sub_menu">Manage Exercise</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Exercise 
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" style="height: 15px" align="center" >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Title<span class="Mandatory">*</span></td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtTitle" CssClass="textbox" MaxLength="100" runat="server" Width="460px" TabIndex="1"></asp:TextBox>
                                                                        </td>
                                                                <td class="top" rowspan="3">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="17" /><br />
                                                                    <asp:Button ID="btnNew" CssClass="button topMargin" runat="server" Text="New" TabIndex="18" />
                                                                    <asp:Button ID="btnReset" CssClass="button topMargin" runat="server" Text="Reset" TabIndex="18" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Description</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" MaxLength="300" TabIndex="2"
                                                                        Width="460px" Height="48px" TextMode="MultiLine"></asp:TextBox></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 40px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 25px; width: 119px;">
                                                                    Url</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtURL" runat="server" CssClass="textbox" MaxLength="300" TabIndex="2"
                                                                        Width="460px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    Order<span class="Mandatory">*</span></td>
                                                                <td style="width: 214px; height: 13px">
                                                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="3" TabIndex="4"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 213px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                </td>
                                                                <td style="width: 214px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 213px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td colspan="2" style="height: 13px; text-align: left" class ="ErrorMsg">Field Marked * are Mandatory
                                                                    </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                    &nbsp;</td>
                                                                <td style="width: 213px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
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
        </td>
    </tr>
    

    </form>
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
        
    
   }
    

    </script>
</body>

</html>
