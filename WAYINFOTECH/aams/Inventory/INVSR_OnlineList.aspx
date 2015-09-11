<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_OnlineList.aspx.vb"
    Inherits="Inventory_INVSR_OnlineList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Inventory::OnlineStatus</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
     function OnlineStatusMandatory()
    {
        if (document.getElementById("txtStatusCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtStatusCode").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Status Code is not valid.";
            document.getElementById("txtStatusCode").focus();
            return false;
            } 
         }
          if (  document.getElementById("txtOnlineStatus").value!="")
         {
           if(IsDataValid(document.getElementById("txtOnlineStatus").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Onlinle Status is not valid.";
            document.getElementById("txtOnlineStatus").focus();
            return false;
            } 
         }
         
         return true;
     }
  
    
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtOnlineStatus">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-></span><span class="sub_menu">Online Status </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Online Status
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 149px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 208px; height: 22px;">
                                                                    Status Code</td>
                                                                <td style="width: 187px; height: 22px" >
                                                                   <asp:TextBox ID="txtStatusCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="6" EnableViewState="False" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td style="height: 22px; width: 140px;">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 149px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 208px; height: 22px;">
                                                                    Online Status</td>
                                                                <td class="textbold" style="width: 187px; height: 22px;">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textbox" MaxLength="30" Width="208px" EnableViewState="False" TabIndex="2"></asp:TextBox></td>
                                                                <td style="height: 22px; width: 140px;">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 149px">
                                                                </td>
                                                                <td style="width: 208px" class="textbold">
                                                                    Segment Expected</td>
                                                                <td style="width: 187px">
                                                                    <asp:TextBox ID="txtSegExpected" runat="server" CssClass="textbox" EnableViewState="False"
                                                                        MaxLength="5" TabIndex="2" Width="208px"></asp:TextBox>&nbsp;</td>
                                                                <td style="width: 140px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    </td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 149px;">
                                                                </td>
                                                                <td style="width: 208px;">
                                                                    </td>
                                                                <td style="width: 187px;">
                                                                </td>
                                                                <td style="width: 140px;">
                                                                </td>
                                                                <td style="">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 149px">
                                                                </td>
                                                                <td style="width: 208px">
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                <td style="width: 187px">
                                                                </td>
                                                                <td style="width: 140px">
                                                                </td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 187px">
                                                                    &nbsp;</td>
                                                                <td style="width: 140px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                             <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="button" OnClientClick="return ReturnData()"
                                                            Text="Select" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:GridView ID="gvOnlineStatus" runat="server" AutoGenerateColumns="False" TabIndex="7"
                                                            Width="100%" EnableViewState="False" AllowSorting="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectItem();" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" id="chkSelect" name="chkSelect" runat="server" />
                                                                        <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("StatusCode") + "|" + Container.DataItem("OnlineStatus") + "|" + Container.DataItem("SegExpected") + "|" + Container.DataItem("UnitCost") %> ' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status Code" SortExpression="StatusCode">
                                                                    <ItemTemplate>
                                                                        <%#Eval("StatusCode")%>
                                                                        <asp:HiddenField ID="hdStatusCode" runat="server" Value='<%#Eval("StatusCode")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="OnlineStatus" HeaderText="Online Status" SortExpression="OnlineStatus" />
                                                                <asp:BoundField DataField="SegExpected" HeaderText="Segment Expected " SortExpression="SegExpected" />
                                                                <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost (IN)"  SortExpression="UnitCost">
                                                                    <HeaderStyle    />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NPUnitCost" HeaderText="Unit Cost (NP)" SortExpression="NPUnitCost" ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="LKUnitCost" HeaderText="Unit Cost (LK)" SortExpression="LKUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BDUnitCost" HeaderText="Unit Cost (BD)" SortExpression="BDUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BTUnitCost" HeaderText="Unit Cost (BT)" SortExpression="BTUnitCost"   ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="MLUnitCost" HeaderText="Unit Cost (ML)" SortExpression="MLUnitCost"  ItemStyle-HorizontalAlign="right"  />
                                                                      
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
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

    <script language="javascript" type="text/javascript">
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
                 if (window.opener.document.forms['form1']['hdOnlineListPopUpPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdOnlineListPopUpPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdOnlineListPopUpPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdOnlineListPopUpPage'].value=window.opener.document.forms['form1']['hdOnlineListPopUpPage'].value + "," + data;
                    }
                  //  alert(window.opener.document.forms['form1']['hdOnlineListPopUpPage'].value );
                    
                    window.opener.document.forms['form1'].submit();
                    window.close();
                    return false;
                 }
           }
       }   
				
    
    </script>

</body>
</html>
