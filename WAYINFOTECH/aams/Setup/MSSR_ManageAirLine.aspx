<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_ManageAirLine.aspx.vb"
    Inherits="Setup_MSSR_ManageAirLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Airline</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     
    function AirlineReset()
    {
        document.getElementById("lblError").innerHTML="";
        document.getElementById("txtAirline").value="";       
        document.getElementById("txtAirlinecode").value=""         
        document.getElementById("chekOnlineCarrier").checked = false;
        document.getElementById("dbgrdManageAirline").style.display = "none"; 
        return false;
    }
    
    function EditFunction(CheckBoxObj)
    {   
        window.location.href = "MSUP_ManageAirLine.aspx?Msg=U&Action=U|"+CheckBoxObj;
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            //window.location.href="MSSR_ManageAirLine.aspx?Action=D|"+CheckBoxObj +"|"+ document.getElementById("<%=txtAirline.ClientID%>").value+"|"+document.getElementById("<%=txtAirlinecode.ClientID%>").value+"|"+document.getElementById("chekOnlineCarrier").checked;
          //  return false;
                document.getElementById('hdAirlineID').value = CheckBoxObj;
               document.forms['frmAirline'].submit();     
        }
        else
        {
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_ManageAirLine.aspx?Action=I";       
        return false;
    }
    </script>

</head>
<body >
    <form id="frmAirline" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Airline </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Airline
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" style="width: 100%">
                                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center"  style="width: 100%">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 3%;">
                                                        </td>
                                                    <td class="textbold" style="width: 15%;">
                                                        Airline Name</td>
                                                    <td style="width: 20%;">
                                                        <asp:TextBox ID="txtAirline" CssClass="textbox" runat="server" TabIndex="1" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 20%" >
                                                        Airline Code</td>
                                                    <td style="width: 20%" >
                                                        <asp:TextBox ID="txtAirlinecode" CssClass="textbox" runat="server" TabIndex="2" MaxLength="2"></asp:TextBox></td>
                                                    <td >
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4"  AccessKey="A"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" >
                                                        &nbsp;</td>
                                                    <td class="textbold" >
                                                        Online carrier only</td>
                                                    <td >
                                                        <asp:CheckBox ID="chekOnlineCarrier" runat="server" TabIndex="3"  class="textbold" />
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                        <asp:Button ID="btnNew" CssClass="button" AccessKey="N" runat="server" Text="New" TabIndex="5" OnClientClick="javascript:NewFunction();" /></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td >
                                                        </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                       </td>
                                                    <td >
                                                        </td>
                                                    <td >
                                                        </td>
                                                    <td >
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    <asp:HiddenField ID="hdAirlineID" runat="server" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="6" AccessKey="E" /></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:GridView ID="dbgrdManageAirline" HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" EnableViewState="False" AllowSorting="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Airline Code" SortExpression="AIRLINE_CODE">
                                                                    <ItemTemplate>
                                                                        <%#Eval("AIRLINE_CODE")%>
                                                                        <asp:HiddenField ID="hdAirlineCode" runat="server" Value='<%#Eval("AIRLINE_CODE")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Airline Name" SortExpression="NAME">
                                                                    <ItemTemplate>
                                                                        <%#Eval("NAME")%>                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Online Carrier" SortExpression="ONLINE_CARRIER">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblOnlineCarrier" runat="server" Text='<%#Eval("ONLINE_CARRIER")%>'  ></asp:Label>
                                                                      </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>                                                                    
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                        <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                            <HeaderStyle CssClass="Gridheading" />
                                                            <RowStyle CssClass="ItemColor" />
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                
                                                 <tr>
                                                   <td  colspan="6" >
                                                   <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 200px; height: 29px;" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px; height: 29px;" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px; height: 29px;" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                   </td>
                                                   </tr>
                                                   
                                                   
                                                
                                                   <!-- code for paging----->
                                                   
                                                 
                                            <tr>   
                                                    <td valign ="top"  >
                                                    
                                                    </td>
                                                   
                                                    
                                                    
                                                </tr>
                                                
            <!-- code for paging----->
                                               
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
</body>
</html>
