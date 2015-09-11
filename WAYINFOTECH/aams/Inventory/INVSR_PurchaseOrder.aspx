<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_PurchaseOrder.aspx.vb" Inherits="Inventory_INVSR_PurchaseOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Invoice:Purchase Order </title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
 <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
 <script type="text/javascript" language="javascript" >

 
       function ValidateOrderSearch()
       {
       //txtpurchaseOrdrNo
       if(document.getElementById('<%=txtpurchaseOrdrNo.ClientId%>').value != '')
        {
        
         if (IsDataValid(document.getElementById('<%=txtpurchaseOrdrNo.ClientId%>').value.trim(),3) == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Purchase Order No. is Numeric.";			
	       document.getElementById('<%=txtpurchaseOrdrNo.ClientId%>').focus();
	       return(false);  
        }
        
        }
        
        
        if(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date Format is Invalid.";			
	       document.getElementById('<%=txtOpenDtFrm.ClientId%>').focus();
	       return(false);  
        }
       }
         
        if(document.getElementById('<%=txtOpenDtTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDtTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date-To Format is Invalid.";			
	       document.getElementById('<%=txtOpenDtTo.ClientId%>').focus();
	       return(false);  
        }
       }
         if(document.getElementById('<%=txtOpenDtTo.ClientId%>').value != '' && document.getElementById('<%=txtOpenDtFrm.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtOpenDtTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Purchase Order From Date Cann't be Greater than Date-To.";			
	       document.getElementById('<%=txtOpenDtTo.ClientId%>').focus();
	       return(false);  
        }
       }  
       }
       
       function ResetSearchOrder()
       {
        document.getElementById("drpSupplierName").selectedIndex='0';
        document.getElementById("drpProduct").selectedIndex='0';
        document.getElementById("txtOpenDtFrm").value='';
        document.getElementById("txtOpenDtTo").value='';
        document.getElementById("lblError").innerHTML='';
        return false;
        }
        
         function DeleteFunction(PurchaseOrderID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteFlag").value=PurchaseOrderID;
           return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                 return false;
           }
        }
        
        function EditFunction(PruchaseOrderID)
        {               
        window.location.href="INVUP_PurchaseOrder.aspx?MSG=N&Action=U&PurchaseID="+PruchaseOrderID;      
        return false;
        }
          
        function SelectFunction(str3)
    {   
         var pos=str3.split('|');         
         if (window.opener.document.forms['form1']['txtPurchaseOrder']!=null)
        { 
        window.opener.document.forms['form1']['txtPurchaseOrder'].value=pos[0];
        window.opener.document.forms['form1']['txtOrderDate'].value=pos[1];
        window.opener.document.forms['form1']['txtSupplier'].value=pos[2];
        window.opener.document.forms['form1']['txtDescription'].value=pos[3];
        
        if (window.opener.document.forms['form1']['ddlChallanType'].value =="2")
        {
        window.opener.document.forms['form1']['hdPurchaseOrder'].value=pos[0];
        window.opener.document.forms['form1'].submit();
        }
        }        
	  	window.close();
        }
 </script>
 
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch">
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-&gt;</span><span class="sub_menu">Purchase Order</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Purchase Order</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                        <td style="width: 860px;" class="redborder" valign="top" >
                                                          <table width="100%" border="0"   align="left" cellpadding="0" cellspacing="0">
                                                          
                        
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" style="height: 15px">
                                                       <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 8%;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px; width: 14%;">
                                                        Supplier Name
                                                        </td>
                                                    <td colspan="3" style="height: 25px">
                                                         <asp:DropDownList onkeyup="gotop(this.id)" ID="drpSupplierName" runat="server" CssClass="dropdownlist" Width="472px" TabIndex="1">
                                                        </asp:DropDownList>
                                                        </td>
                                                    <td style="width: 176px; height: 25px">
                                                    </td>
                                                    <td style="width: 176px; height: 25px;">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" OnClientClick="return ValidateOrderSearch()" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 8%;">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 14%;">
                                                        Product</td>
                                                    <td width="20%" style="height: 25px" colspan="3">
                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="drpProduct" runat="server" CssClass="dropdownlist" Width="472px" TabIndex="1">
                                                    </asp:DropDownList></td>
                                                    <td style="width: 176px; height: 25px">
                                                    </td>
                                                    
                                                    <td style="height: 25px; width: 176px;">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="n" /></td>
                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" style="width: 8%; height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 14%; height: 25px">
                                                                      Purchase Order No.</td>
                                                                  <td style="width: 23%; height: 25px">
                                                                  <asp:TextBox ID="txtpurchaseOrdrNo" runat="server" CssClass="textbox" Width="160px" TabIndex="1" MaxLength="9"></asp:TextBox>
                                                                  </td>
                                                                  <td class="textbold" style="width: 13%; height: 25px">
                                                                  </td>
                                                                  <td style="height: 25px" width="20%">
                                                                  </td>
                                                                  <td style="width: 176px; height: 25px">
                                                                  </td>
                                                                  <td style="width: 176px; height: 25px">
                                                                      <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="r" /></td>
                                                              </tr>
                                                
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 8%;">
                                                 <input type="hidden" id="hdDeleteFlag" runat="server" style="width: 9px" />
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    <td class="textbold" style="height: 25px; width: 14%;">
                                                        Order Date From
                                                        
                                                        </td>
                                                    <td style="height: 25px; width: 23%;">
                                                                       <asp:TextBox ID="txtOpenDtFrm" runat="server" CssClass="textbox" MaxLength="40" Width="136px" TabIndex="1"></asp:TextBox>
                                                                       <img id="imgApprovalFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" tabindex="1" />
                                                                          <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDtFrm.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> 
                                                                       </td>
                                                    <td class="textbold" style="height: 25px; width: 13%;">
                                                        Order Date To</td>
                                                    <td width="20%" style="height: 25px">
                                                                        <asp:TextBox ID="txtOpenDtTo" runat="server" CssClass="textbox" MaxLength="40" Width="136px" TabIndex="1"></asp:TextBox>
                                                                        <img id="imgApprovalTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" tabindex="1" />
                                                                        
                                                                          <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDtTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                    <td style="width: 176px; height: 25px">
                                                    </td>
                                                    <td style="height: 25px; width: 176px;">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export" AccessKey="e" /></td>
                                                </tr>
                                                
                                                   <tr>
                                                 
                                                    <td colspan="9" align="left" >
                                                    
                                                    <asp:GridView ID="grdvPurchaseOrder" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" Width="840px" TabIndex="3" AllowSorting="True">
                                                              <Columns>
                                                    
                                                 <asp:TemplateField HeaderText="Order No." SortExpression="PurchaseOrderID">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("PurchaseOrderID") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle Width="90px" />
                                                 </asp:TemplateField>
                                                 
                                                  <asp:TemplateField HeaderText="Order Date" SortExpression="OrderDate">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblOrderDt" runat="server" Text='<%# Eval("OrderDate") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                      <ItemStyle Width="100px" />
                                                      <HeaderStyle Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                  <asp:TemplateField HeaderText="Creation Date" SortExpression="CreationDate">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblCreationDt" runat="server" Text='<%# Eval("CreationDate") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                      <ItemStyle Width="100px" />
                                                      <HeaderStyle Wrap="False" />
                                                 </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="Supplier Name" SortExpression="SupplierName" >
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("SupplierName") %>' Width="200px"></asp:Label>
                                                 </ItemTemplate>
                                                     <ItemStyle Width="170px" Wrap="True" />
                                                 </asp:TemplateField>
                                                 
                                                 
                                                  <asp:TemplateField HeaderText="Product Name" SortExpression="PRODUCTNAME">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCTNAME") %>' Width="200px"></asp:Label>
                                                 </ItemTemplate>
                                                      <ItemStyle Width="170px" Wrap="True" />
                                                 </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="Action" >
                                                                                         <ItemTemplate>
                                                                                            <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderID") + "|" + DataBinder.Eval(Container.DataItem, "OrderDate")+ "|" + DataBinder.Eval(Container.DataItem, "SupplierName") + "|" + DataBinder.Eval(Container.DataItem, "SupplierNotes") %>'>Select</asp:LinkButton>&nbsp;
                                                                                            <asp:LinkButton ID="linkEdit" runat="server" CssClass="LinkButtons" Text="Edit"></asp:LinkButton>&nbsp;
                                                                                            <asp:LinkButton ID="linkDelete" runat="server" CssClass="LinkButtons" Text="Delete"></asp:LinkButton>&nbsp;
                                                                                            <asp:HiddenField ID="hdOrderID" runat="server" Value='<%#Eval("PurchaseOrderID")%>' />  
                                                                                           </ItemTemplate>
                                                     <ItemStyle Wrap="False" />
                                                                                </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  ForeColor="White" />    
                                                            <RowStyle CssClass="textbold" />
                                                </asp:GridView>
                                                        &nbsp;
                                                       </td>
                                                </tr>
                                                              <tr>
                                                                  <td align="left" colspan="9">
                                                                   <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" TabIndex="4" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="4"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="4" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="4">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
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
          <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server" ></asp:TextBox></table>
    </form>
</body>
</html>
