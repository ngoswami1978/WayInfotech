<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_Description.aspx.vb" Inherits="ETHelpDesk_HDUP_Description" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::HelpDesk::Manage Call Log</title>
    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function ColorMethod(id,total)
{   
        document.getElementById("lblError").innerHTML='';
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            document.getElementById(Hcontrol).className="headingtabactive";
        }
        
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       
       var pgStatus=document.getElementById('hdPageStatus').value;
       if(pgStatus=='U')
       {      
       
       var strHD_RE_ID =document.getElementById('hdPageHD_RE_ID').value;
       var strLCode=document.getElementById('hdPageLCode').value;
       if (id == (ctextFront +  "00" + ctextBack))
       {   
           if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_CallLog.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           
           if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_Description.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }
           
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
            
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_Solution.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }
         
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
            
            if(strHD_RE_ID!="")
           {
           window.location.href="HDUP_FollowUp.aspx?Action="+pgStatus + "&LCode=" + strLCode + "&HD_RE_ID=" +strHD_RE_ID ;
           }
           else
           {
           window.location.href="HDUP_FollowUp.aspx?Action="+pgStatus + "&LCode=" + strLCode ;
           }
            
            return false;
           
       }
       }
       
           
       
                                     
       
}

</script>
</head>
<body style="font-size: 12pt; font-family: Times New Roman">
     <form id="form1" runat="server"  >
        <table width="860px" style="height:486px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">ETrackers HelpDesk-></span><span class="sub_menu">Description</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Manage Description
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top" style="height: 456px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold center top"  height="25px"  rowspan="0">
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" class="top">                                                       
                                                        <asp:Panel ID="pnlDescription" runat="server" Width="100%">
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="6" class="gap">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Description <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="top">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" colspan="2">
                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Height="200px" Rows="5"
                                                                            TextMode="MultiLine" Width="522px" TabIndex="2"></asp:TextBox></td>
                                                                    <td class="top">
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="3" /><br />
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button topMargin" Text="Reset" TabIndex="3" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 6% ">
                                                                    </td>
                                                                    <td class="textbold" style="width: 16%; ">
                                                                        </td>
                                                                    <td style="width: 53%; ">
                                                                    <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        </td>
                                                                    <td style="width: 25%; ">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="ErrorMsg" colspan="3">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                <td></td>
                                                                    <td colspan="3" >
                                                                        <asp:GridView EnableViewState="False" ID="gvDescription" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="99%">
                                                                                <Columns>
                                                                                     <asp:BoundField HeaderText="User" DataField="EmployeeName" >
                                                                                         <ItemStyle Width="20%" />
                                                                                     </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="DateTime" DataField="DATETIME" >
                                                                                        <ItemStyle Width="20%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Description" DataField="ACTION_TAKEN">
                                                                                        <ItemStyle Width="60%" />
                                                                                    </asp:BoundField>
                                                                                 </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue center" />
                                                                                    <RowStyle CssClass="textbold center" />
                                                                                    <HeaderStyle CssClass="Gridheading center" />                                                    
                                                                                 </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" class="gap">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>                           
                                                       
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
     if(document.getElementById('<%=txtDescription.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Description is mandatory.'
            return false;
        }

       return true; 
        
    }
    
    
    </script>
</body>
</html>
