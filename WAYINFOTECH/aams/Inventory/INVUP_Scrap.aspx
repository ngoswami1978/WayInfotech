<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_Scrap.aspx.vb" Inherits="Inventory_INVUP_Scrap" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Manage Scrap</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language ="javascript" type="text/javascript">
    
    function  ScrapExecute()
    {
       if( confirm('Scrap once executed cannot be modified. Continue ?') ==true)
       {
        return true;
       }
       else
       {
        return false;
       }
    }
    </script>
</head>
<body >
    <form id="form1" runat="server" >
   
     <div>
    <table width="840px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Manage Scrap</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Manage Scrap</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td class="center" colspan="7"  height="25px" >
                                              <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">
                                            </td>
                                            <td class="textbold" style="width: 10%">
                                                Godown<span class="Mandatory">*</span></td>
                                            <td class="textbold" colspan="2">
                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlGodown" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                    Width="200px">
                                                    <asp:ListItem Selected="True">Select One</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="width: 10%">
                                                Trash ID</td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtTrashID" runat="server" CssClass="textboxgrey" TabIndex="20" MaxLength="10" ReadOnly="True" Width="195px"></asp:TextBox></td>
                                            <td class="center" style="width: 15%">
                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4" AccessKey="S" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Logged Date</td>
                                            <td class="textbold" colspan="2">
                                                <asp:TextBox ID="txtLoggedDate" runat="server" CssClass="textboxgrey" TabIndex="20" MaxLength="25" ReadOnly="True" Width="195px"></asp:TextBox>
                                            </td>
                                            <td class="textbold" style="width: 8%">
                                                Logged By</td>
                                            <td style="width: 22%">
                                                <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textboxgrey" TabIndex="20" MaxLength="50" ReadOnly="True" Width="195px"></asp:TextBox>
                                                </td>
                                            <td class="center">
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Product<span class="Mandatory">*</span></td>
                                            <td class="textbold" colspan="2">
                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlProduct" runat="server" CssClass="dropdown" TabIndex="2"
                                                    Width="200px">
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="width: 8%">
                                                Quantity<span class="Mandatory">*</span></td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox" TabIndex="20" Width="85px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                &nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="button" TabIndex="3" Text="Add" Width="87px" /></td>
                                            <td class="center">
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="ErrorMsg" colspan="3">
                                            Field Marked * are Mandatory
                                            </td>
                                            <td class="textbold" style="width: 8%">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="center">
                                                <asp:Button ID="btnExecute" CssClass="button" runat="server" Text="Execute" TabIndex="6" OnClientClick ="return ScrapExecute();" AccessKey="e" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                            </td>
                                            <td style="width: 20%">
                                            </td>
                                            <td class="textbold" style="width: 4%">
                                            </td>
                                            <td class="textbold" style="width: 8%">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="5">
                                            
                                               <asp:GridView ID="gvProduct" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                                <Columns>
                                                                                 <asp:BoundField HeaderText="SNo" DataField="LineNumber" />
                                                                                    <asp:BoundField HeaderText="Product" DataField="ProductName">
                                                                                     <ItemStyle Width="20%" />
                                                                                     </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Quantity" DataField="QTY" />
                                                                                                           
                                                                                   
                                                                                     <asp:TemplateField HeaderText="Serial Number">
                                                                                     <ItemTemplate >
                                                                                     <asp:TextBox ID="txtVenderSerial" Text='<% #DataBinder.Eval(Container.DataItem, "SerialNumber") %>' runat="server" CssClass="textbox"></asp:TextBox>
                                                                                     </ItemTemplate>
                                                                                     
                                                                                     </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CommandArgument='<% #DataBinder.Eval(Container.DataItem, "LineNumber") %>' CssClass="LinkButtons"></asp:LinkButton>
                                                               <asp:HiddenField ID="hdProductID" runat="server" Value='<%#Eval("ProductID")%>' />   
                                                            </ItemTemplate>
                                                           </asp:TemplateField>    
                                                                                 </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue center" />
                                                                                    <RowStyle CssClass="textbold center" />
                                                                                    <HeaderStyle CssClass="Gridheading center" />                                                    
                                                                                 </asp:GridView>
                                            </td>
                                            <td class="center">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td colspan="7" style ="width:100%" valign ="top" >
                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                        </td>
                                    </tr>
                                </table>
                                <input id="hdDeleteId" runat="server" enableviewstate="true" style="width: 1px" type="hidden" />
                                <input id="hdEmployeeID" runat="server" enableviewstate="true" style="width: 1px" type="hidden" />
                                <input id="hdProductList" runat="server" enableviewstate="true" style="width: 1px" type="hidden" />
                                <input id="hdScrapID" runat="server" enableviewstate="true" style="width: 1px" type="hidden" />
                               
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
   
    </form>
    <script language ="javascript" type="text/javascript">
    function ValidateProduct()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
      //   *********************** Validating product  **********************************
        if(document.getElementById('<%=ddlProduct.ClientId%>').selectedIndex==0)
        {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Product is mandatory.'
                document.getElementById('<%=ddlProduct.ClientId%>').focus();
                return false;
        
        }
      //   *********************** Validating product quantity  **********************************
     if(document.getElementById('<%=txtQuantity.ClientId%>').value =='')
         {
          document.getElementById('<%=lblError.ClientId%>').innerText ='Quantity is mandatory.'
                document.getElementById('<%=txtQuantity.ClientId%>').focus();
                return false;
         
         }
         else
         {
            var strValue = document.getElementById('<%=txtQuantity.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
             {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Quantity should contain only digits.'
                document.getElementById('<%=txtQuantity.ClientId%>').focus();
                return false;
             }
        }
        
        return true;
    }
     function ValidateForm()
    {
    
        document.getElementById('<%=lblError.ClientId%>').innerText=''
        //   *********************** Validating Godown  **********************************
         if(document.getElementById('<%=ddlGodown.ClientId%>').selectedIndex==0)
            {
            
                document.getElementById('<%=lblError.ClientId%>').innerText ='Godown is mandatory.'
                document.getElementById('<%=ddlGodown.ClientId%>').focus();
                return false;
            }
            
            // ****************** Validating serial number *************************************
             for(intcnt=1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
               {  
                if (document.getElementById('gvProduct').rows[intcnt].cells[3].children.length == "1")
                {
                    if (document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].type=="text")
                    {                 
                       if(document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim()=='')
                       {
                        document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].focus();
                        document.getElementById("lblError").innerHTML="Serial number is mandatory.";
                        return false;
                       }
                    } 
                }
             }
             
            // ****************** Validating Unique serial number *************************************
            
            var intcntChild=0;
            for(intcnt=1;intcnt<=document.getElementById('gvProduct').rows.length-1;intcnt++)
               {  
                        if (document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].type=="text")
                        { 
                        //Take Previous data
                            var serialNo=document.getElementById('gvProduct').rows[intcnt].cells[3].children[0].value.trim();
                        }
                        
                       // intcnt=intcnt+1;
                        //Check from next row
                        for(intcntChild=intcnt+1;intcntChild<=document.getElementById('gvProduct').rows.length-1;intcntChild++)
                       {  
                           if(serialNo==document.getElementById('gvProduct').rows[intcntChild].cells[3].children[0].value.trim())
                           {
                                document.getElementById('gvProduct').rows[intcntChild].cells[3].children[0].focus();
                                document.getElementById("lblError").innerHTML="Serial number is not unique";
                                return false;
                           }
                       
                       }
                       
               }
               
                
             
 
         return true;
    
    }
    </script>
</body>
</html>
