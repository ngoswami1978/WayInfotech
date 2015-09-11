<%@ Page Language="Vb"  AutoEventWireup="false" CodeFile="InvSR_EquipmentList.aspx.vb" Inherits="Inventory_InvSR_EquipmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Inventory::Equipment</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
   
    
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
                                                        Equipment Type</td>
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
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td class="textbold" nowrap="nowrap" style="height: 28px"></td>
                                                    <td class="textbold" style="width: 235px; height: 28px;">
                                                       </td>
                                                    <td nowrap="nowrap" class="textbold" style="width: 147px; height: 28px;">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        </td>
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
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="button" OnClientClick="return ReturnData()"
                                                            Text="Select" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:GridView ID="grdEquipment" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" HeaderStyle-ForeColor="white"  EnableViewState="False" TabIndex="3" AllowSorting="false">
                                                            <Columns>
                                                                  <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectItem();" /> 
                                                                        </HeaderTemplate>
                                                                            <ItemTemplate>                                                                            
                                                                             <input type="checkbox" id="chkSelect" name="chkSelect" runat="server"  /> 
                                                                             <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("PRODUCTID") + "|" + Container.DataItem("EQUIPMENT_CODE") + "|" + Container.DataItem("EGROUP_CODE") + "|" + Container.DataItem("DESCRIPTION") %> ' />   
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                <asp:BoundField DataField="EQUIPMENT_CODE" HeaderText="Equipment Code" SortExpression="EQUIPMENT_CODE" >
                                                                    <HeaderStyle Width="6%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EGROUP_CODE" HeaderText="Equipment Group" SortExpression="EGROUP_CODE">
                                                                    <HeaderStyle Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CONFIG" HeaderText="Configuration" SortExpression="CONFIG" >
                                                                    <HeaderStyle Width="18%" />
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
                                                                      
                                                                
                                                               
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                
                                                  <tr>
                                                                <td colspan="6" style="width: 100%">
                                                                   
                                                                     <input id="hdData" style="width: 1px" type="hidden" runat="server" />
                                                                   
                                                                     
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
     <script language ="javascript" type="text/javascript">
   // Function for checking all check boxes.
  
    function SelectItem() 
    {
       CheckAllGridViewCheckBoxes(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllGridViewCheckBoxes(value) 
    {
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
              elm.checked = value
            }
        }
    }
    
	function ReturnData()
       {
     //  {debugger;}
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         if (elm.checked == true && elm.id != "chkAllSelect")
                         {
                            var chkname=elm.id;
                            var gvname=chkname.split("_")[0];
                            var ctrlidname=chkname.split("_")[1];
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                         
                         }
                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
           else
           {
                 if (window.opener.document.forms['form1']['hdProductListPopUpPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdProductListPopUpPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdProductListPopUpPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdProductListPopUpPage'].value=window.opener.document.forms['form1']['hdProductListPopUpPage'].value + "," + data;
                    }
                  //  alert(window.opener.document.forms['form1']['hdProductListPopUpPage'].value );
                    
                    window.opener.document.forms['form1'].submit();
                    window.close();
                    return false;
                 }
           }
       }   
				
    
    </script> 
</body>
</html>
