<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_EquipmentCategory.aspx.vb" ValidateRequest ="false"
    Inherits="Incentive_INVUP_EquipmentCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Manage Equipment Category</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function  NewMSUPEquipmentCateg()
       {    
           window.location="INVUP_EquipmentCategory.aspx?Action=I";
           return false;
       }    
     function CheckMandatoty()
    {
    
       if (document.getElementById("drpCountry").selectedIndex==0)
        {          
             document.getElementById("lblError").innerHTML="Country is mandatory."
             document.getElementById("drpCountry").focus();
             return false;
        }
        if (document.getElementById("txtEquipmentCateg").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Equipment Category is mandatory."
             document.getElementById("txtEquipmentCateg").focus();
             return false;
        }
        if (  document.getElementById("txtEquipmentCateg").value!="")
         {
           if(IsDataValid(document.getElementById("txtEquipmentCateg").value,2)==false)
            {
            document.getElementById("lblError").innerHTML="Equipment Category is not valid.";
            document.getElementById("txtEquipmentCateg").focus();
            return false;
            } 
         }  
         
         if (document.getElementById("ChkUnitCostMan").checked==true)
         {         
                 if (document.getElementById("txtUnitCost").value.trim()=="")
            {          
                 document.getElementById("lblError").innerHTML="Unit Cost is mandatory."
                 document.getElementById("txtUnitCost").focus();
                 return false;
                 
            }
         } 
         
           if (  document.getElementById("txtUnitCost").value!="")
         {
           if(IsDataValid(document.getElementById("txtUnitCost").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Unit cost is not valid.";
            document.getElementById("txtUnitCost").focus();
            return false;
            } 
         } 
          
        return true;      
    }
    
       function AddSelectEquipmentPage()
       {
          var type = "../Inventory/InvSR_EquipmentList.aspx";
    	  window.open(type,"EquipList","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	               	    
          return false;
      }
    
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtEquipmentCateg">
        <div>
            <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Inventory-&gt; Equipment Category</span>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="heading" align="center" valign="top">
                                    Manage Equipment Category&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="100%" class="redborder">
                                                <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <%--<tr>
                                                                <td width="20px"  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>--%>
                                                                <tr>
                                                                   
                                                                    <td align="center" class="textbold" colspan="6" height="20px" valign="top">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 319px; height: 22px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px;">
                                                                    </td>
                                                                    <td style="height: 22px; width: 320px;" align="left">
                                                                        Country <span class="Mandatory">* </span>
                                                                    </td>
                                                                    <td style="height: 22px; width: 347px;">
                                                                        <span class="textbold">
                                                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                TabIndex="1" Width="184px">
                                                                            </asp:DropDownList></span></td>
                                                                    <td width="18%" style="height: 22px">
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="3"
                                                                            AccessKey="A" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="textbold" colspan="1" valign="top" style="width: 319px">
                                                                    </td>
                                                                    <td class="textbold" colspan="5" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 319px; height: 22px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px;">
                                                                    </td>
                                                                    <td style="height: 22px; width: 320px;" align="left">
                                                                        Equipment Category<span class="Mandatory">* </span>
                                                                    </td>
                                                                    <td style="height: 22px; width: 347px;">
                                                                        <span class="textbold">
                                                                            <asp:TextBox ID="txtEquipmentCateg" runat="server" CssClass="textfield" TabIndex="1"
                                                                                Width="177px" MaxLength="100"></asp:TextBox></span></td>
                                                                    <td width="18%" style="height: 22px">
                                                                        <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="3"
                                                                            AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="textbold" colspan="1" valign="top" style="width: 319px">
                                                                    </td>
                                                                    <td class="textbold" colspan="5" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 319px; height: 22px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px;">
                                                                    </td>
                                                                    <td style="height: 22px; width: 320px;" align="left">
                                                                        Unit Cost</td>
                                                                    <td class="textbold" style="height: 22px; width: 347px;">
                                                                        <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textfield" MaxLength="7" TabIndex="1"
                                                                            Width="177px"></asp:TextBox></td>
                                                                    <td style="height: 22px">
                                                                           <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="3"
                                                                            AccessKey="R" /></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td align="center" class="textbold" colspan="1" valign="top" style="width: 319px">
                                                                    </td>
                                                                    <td class="textbold" colspan="5" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                   <tr>
                                                                    <td class="textbold" style="width: 319px; height: 22px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px;">
                                                                    </td>
                                                                    <td style="height: 22px; width: 320px;" align="left">
                                                                        </td>
                                                                    <td class="textbold" style="height: 22px; width: 347px;">
                                                                        <asp:CheckBox ID="ChkUnitCostMan" runat="server" Text="Unit Cost required" TabIndex="1" /></td>
                                                                    <td style="height: 22px">
                                                                           </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 319px; height: 22px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px;">
                                                                    </td>
                                                                    <td style="height: 22px" align="left" colspan="2" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 22px">
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="textbold" colspan="1" valign="top" style="width: 319px">
                                                                    </td>
                                                                    <td class="textbold" colspan="5" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 319px; height: 15px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 15px; width: 173px;">
                                                                        &nbsp;</td>
                                                                    <td style="height: 15px">
                                                                    </td>
                                                                    <td colspan="2" style="height: 15px" align ="center" > <asp:Button ID="Button1" runat="server" CssClass="button" Text="Add Product" TabIndex="2" OnClientClick="return AddSelectEquipmentPage()" />
                                                                    </td>
                                                                    <td style="height: 30px">
                                                                       </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="textbold" colspan="1" valign="top" style="width: 319px">
                                                                    </td>
                                                                    <td class="textbold" colspan="5" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 319px">
                                                                    </td>
                                                                  
                                                                    <td colspan="4">
                                                                        <asp:GridView ID="dbgrdCategoryProductDesc" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" TabIndex="6">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="EQUIPMENT_CODE" HeaderText="Equipment Code" SortExpression="EQUIPMENT_CODE">
                                                                                    <HeaderStyle Width="20%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="EQUIPMENT_GROUP" HeaderText="Equipment Group" SortExpression="EQUIPMENT_GROUP">
                                                                                    <HeaderStyle Width="25%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" SortExpression="DESCRIPTION">
                                                                                    <HeaderStyle Width="44%" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CommandArgument='<% #DataBinder.Eval(Container.DataItem, "PRODUCTID") %>' CssClass="LinkButtons"></asp:LinkButton>
                                                                                  
                                                                                  <asp:HiddenField ID="hdProductId" Value='<%#Eval("PRODUCTID") %>' runat="server" />
                                                                                   <asp:HiddenField ID="hdEquipCode" Value='<%#Eval("EQUIPMENT_CODE") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                               </asp:TemplateField>    
                                                                              
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="textbold" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="1" style="height: 4px; width: 319px;">
                                                                    </td>
                                                                    <td colspan="5" style="height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr height="20px">
                                                                    <td colspan="1" height="4" style="width: 319px">
                                                                    </td>
                                                                    <td colspan="5" height="4">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="1" style="height: 12px; width: 319px;">
                                                                    </td>
                                                                    <td colspan="5" style="height: 12px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                        <td width="18%" rowspan="1" valign="top">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="textbold" colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
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
                <tr>
                    <td valign="top" style="height: 23px">  <input id="hdProductListPopUpPage" runat="server" style="width: 1px" type="hidden"  /> <input id="hdProductList" runat="server" style="width: 1px" type="hidden"  />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
