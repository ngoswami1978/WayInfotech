<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Godown.aspx.vb" Inherits="Inventory_Inv_Godown" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Godown Name</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   
</head>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<script language="javascript" type="text/javascript">
    
    function SelectFunction(str3)
        {   
            //alert(str3);
            var pos=str3.split('|'); 
            if (window.opener.document.forms['form1']['hdChallanGodownId']!=null)
            {
            window.opener.document.forms['form1']['hdChallanGodownId'].value=pos[0];
             window.opener.document.forms['form1']['txtChallanGodownName'].value=pos[1];
            window.opener.document.forms['form1']['txtGodownAddress'].value=pos[2];
            
            }
               
            window.close();
       }
//    function Reset()
//    {
//        document.getElementById("txtGodownName").value="";
//         document.getElementById("lblError").innerHTML="";
//         document.getElementById("drpCity").value="";
//        if (document.getElementById("grdGodown")!=null) 
//        document.getElementById("grdGodown").style.display ="none"; 
//        return false;
//    }
    function NewFunction()
    {  
        window.location.href="INVUP_Godown.aspx?Action=I";      
        return false;
    }  
      function EditFunction(GODOWNID)
    {    
    {}
        window.location.href="INVUP_Godown.aspx?Action=U&GODOWNID="+GODOWNID;      
        return false;
     }
    function DeleteFunction(GODOWNID)
    {  
    {}
        if (confirm("Are you sure you want to delete?")==true)
         {    
//            var GodownName = document.getElementById('<%=txtGodownName.ClientId%>').value     
//            window.location.href="INVSR_Godown.aspx?Action=D&GODOWNID="+GODOWNID+"&GodownName="+GodownName;  
//            return false;
//        }
//    }
                document.getElementById('<%=hdID.ClientId%>').value  =GODOWNID
                return true;
            }
            return false;
	}
    
</script>
<body>
    <form id="form1" runat="server" defaultfocus="txtGodownName"  defaultbutton="btnSearch">
   
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-&gt;</span><span class="sub_menu">Godown</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Search Godown
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 309px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                <tr>
                                                    <td colspan="4" class="center gap">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg "></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold">
                                                        Godown Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtGodownName" CssClass="textbox" runat="server" MaxLength="100"
                                                            Width="376px" Wrap="False" TabIndex="1"></asp:TextBox></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        City</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                            position: relative; top: 0px" Width="168px"  onkeyup="gotop(this.id)">
                                                        </asp:DropDownList>
                                                        </td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="grdGodown" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" TabIndex="6" AllowSorting="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="GodownName"  HeaderText="Godown Name" SortExpression="GODOWNNAME" >
                                                                    <HeaderStyle Width="22%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" >
                                                                    <HeaderStyle Width="32%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" >
                                                                    <HeaderStyle Width="11%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                                                    <HeaderStyle Width="11%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" SortExpression="PostalCode" >
                                                                    <HeaderStyle Width="10%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" ></asp:LinkButton>&nbsp;     
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                      <%--  <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                       <asp:LinkButton ID="linkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                                        <asp:HiddenField ID="hdGodownId" runat="server" Value='<%#Eval("GODOWNID")%>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left"  ForeColor="White" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                      <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table>
                                                          <asp:HiddenField ID="hdID" runat="server" />
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
</body>
</html>
