<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TCUP_QueryCategory.aspx.vb" Inherits="ETHelpDesk_Technical_MSUP_QueryCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Technical::Manage Query Category</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    
 <script src="../JavaScript/ETracker.js" type="text/javascript"></script>
</head>
<body style="font-size: 12pt; font-family: Times New Roman">
     <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ETrackers Technical-&gt;</span><span class="sub_menu">Manage Query Category</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Query Category</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td class="gap" colspan="2" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Category Name<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                            <asp:TextBox ID="txtCategory" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1" Width="200px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Sub Group<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                            <asp:DropDownList ID="drpSubGroup" runat="server" CssClass="dropdownlist" TabIndex="4"
                                                Width="206px" onkeyup="gotop(this.id)">
                                            </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                            </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="2" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                            <td style="height: 26px">
                                                &nbsp;</td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center; height: 21px;">
                                        <asp:HiddenField ID="hdID" runat ="server" />
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
      var cboGroup=document.getElementById('<%=drpSubGroup.ClientId%>');
       //*********** Validating Currency Code *****************************
   
   if(cboGroup.selectedIndex ==0)
        {
         document.getElementById('<%=lblError.ClientId%>').innerText ='Sub group is mandatory.'
         cboGroup.focus();
         return false;
            
        }
     if(document.getElementById('<%=txtCategory.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Category is mandatory.'
            document.getElementById('<%=txtCategory.ClientId%>').focus();
            return false;
        }
//        else
//        {
//            var strValue = document.getElementById('<%=txtCategory.ClientId%>').value
//            reg = new RegExp("^[a-zA-Z ]+$"); 

//            if(reg.test(strValue) == false) 
//            {

//                document.getElementById('<%=lblError.ClientId%>').innerText ='Category should contain only alphabets.'
//                return false;

//             }
//        }
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtCategory").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>
</html>

