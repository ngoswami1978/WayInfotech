<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Equipment.aspx.vb"
    Inherits="Inventory_INVSR_Equipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Inventory::Equipment</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<script language="javascript" type="text/javascript">
     function Reset()
    {
      document.getElementById("txtEquipmentType").value="";
      document.getElementById("txtEquipmentType").focus();
      document.getElementById("lblError").innerHTML="";
      document.getElementById("txtEquipmentDescription").value="";
      document.getElementById("drpEquipmentGroup").value="";
      document.getElementById("txtEquipmentConfiguration").value="";
      if (document.getElementById("grdEquipment")!=null) 
      document.getElementById("grdEquipment").style.display ="none"; 
      return false;
    }
    function NewFunction()
    {  
        window.location.href="INVUP_Equipment.aspx?Action=I";      
        return false;
    }  
      function EditFunction(Productid)
    {    
        window.location.href="INVUP_Equipment.aspx?Action=U&Productid="+Productid;      
        return false;
     }
    function DeleteFunction(Productid)
    {  
        if (confirm("Are you sure you want to delete?")==true)
        {    
//            window.location.href="INVSR_Equipment.aspx?Action=D&Productid="+Productid+"|"+ document.getElementById("<%=txtEquipmentType.ClientID%>").value ;  
//            return false;
//        }
//    }
             document.getElementById('<%=hdID.ClientId%>').value  =Productid
                return true;
            }
            return false;
	}
    function PopupEquipment()
        {
         var type;
         type = "../Popup/PUSR_InvEquipment.aspx?Popup=T" ;
         var strReturn; 
         if (window.showModalDialog)
         {     
         strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
         }
         else
         {     
         strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
         }	   
         if (strReturn != null)
         {
         var sPos = strReturn.split('|'); 
         document.getElementById('<%=txtEquipmentType.ClientID%>').value=sPos[0];
          }  
   	      return false;
        }
    
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtEquipmentType">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-></span><span class="sub_menu">Equipment </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Equipment
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 25px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold" style="height: 28px">
                                                        Equipment Code</td>
                                                    <td style="width: 235px; height: 28px;">
                                                        <asp:TextBox ID="txtEquipmentType" runat="server" CssClass="textbox" MaxLength="40"
                                                            ReadOnly="false" Style="left: 0px; position: relative; top: 0px" TabIndex="1"
                                                            Width="152px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 147px; height: 28px;">
                                                        <span class="textbold">Equipment Description</span></td>
                                                    <td width="21%" style="height: 28px">
                                                        <asp:TextBox ID="txtEquipmentDescription" runat="server" CssClass="textbox" MaxLength="10"
                                                            Style="position: relative" TabIndex="1"></asp:TextBox></td>
                                                    <td width="18%" style="height: 28px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 28px">
                                                        Equipment Group</td>
                                                    <td style="width: 235px; height: 28px;">
                                                        <asp:DropDownList ID="drpEquipmentGroup" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="1" Width="160px" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" nowrap="nowrap" style="width: 147px; height: 28px;">
                                                        Equipment Configuration</td>
                                                    <td style="height: 28px">
                                                        <asp:TextBox ID="txtEquipmentConfiguration" runat="server" CssClass="textbox" MaxLength="10"
                                                            TabIndex="1"></asp:TextBox></td>
                                                    <td style="height: 28px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="n" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td class="textbold" nowrap="nowrap" style="height: 28px"></td>
                                                    <td class="textbold" style="width: 235px; height: 28px;">
                                                       <asp:TextBox ID="txtSegExpected" runat="server" CssClass="textbox" MaxLength="8" Visible ="false" 
                                                            ReadOnly="false" Style="left: 0px; position: relative; top: 0px" TabIndex="1"
                                                            Width="152px"></asp:TextBox></td>
                                                    <td nowrap="nowrap" class="textbold" style="width: 147px; height: 28px;">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="height: 22px">
                                                    </td>
                                                    <td style="width: 147px; height: 22px;">
                                                        &nbsp;</td>
                                                    <td style="height: 22px">
                                                        &nbsp;</td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                            TabIndex="2" Text="Export" AccessKey="e" />&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td style="width: 147px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:GridView ID="grdEquipment" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" HeaderStyle-ForeColor="white"  EnableViewState="False" TabIndex="3" AllowSorting="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="EQUIPMENT_CODE" HeaderText="Equipment Code" SortExpression="EQUIPMENT_CODE" >
                                                                    <HeaderStyle Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EGROUP_CODE" HeaderText="Equipment Group" SortExpression="EGROUP_CODE">
                                                                    <HeaderStyle Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CONFIG" HeaderText="Configuration" SortExpression="CONFIG" >
                                                                    <HeaderStyle Width="12%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Description"  SortExpression="DESCRIPTION">
                                                                    <HeaderStyle Width="18%" />
                                                                </asp:BoundField>
                                                                   <asp:BoundField DataField="SegExpected" HeaderText="Segment Expected"  SortExpression="SegExpected">
                                                                    <HeaderStyle Width="10%"   />
                                                                </asp:BoundField>
                                                                
                                                                
                                                                 <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost (IN)"  SortExpression="UnitCost">
                                                                    <HeaderStyle Width="10%"   />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NPUnitCost" HeaderText="Unit Cost (NP)" SortExpression="NPUnitCost" ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="LKUnitCost" HeaderText="Unit Cost (LK)" SortExpression="LKUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BDUnitCost" HeaderText="Unit Cost (BD)" SortExpression="BDUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BTUnitCost" HeaderText="Unit Cost (BT)" SortExpression="BTUnitCost"   ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="MLUnitCost" HeaderText="Unit Cost (ML)" SortExpression="MLUnitCost"  ItemStyle-HorizontalAlign="right"  />
                                                                      
                                                                
                                                                <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false" >
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                         <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                          <asp:LinkButton ID="linkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                                        <asp:HiddenField ID="hdEquipmentCode" runat="server" Value='<%#Eval("Productid")%>' />
                                                                        <asp:HiddenField ID="hidCode" runat="server" Value='<%#Eval("EQUIPMENT_CODE")%>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%; height: 30px;" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                    <td style="width: 25%; height: 30px;" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 20%; height: 30px;" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                            ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%; height: 30px;" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
