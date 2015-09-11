<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Supplier.aspx.vb" Inherits="Inventory_INVSR_Supplier" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Supplier</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script language="javascript" type="text/javascript">
      
    function EditFunction(SuppId)
        {           
           window.location.href="INVUP_Supplier.aspx?Action=U&SuppId=" + SuppId;               
           return false;
        }
        
         function NewFunction(SuppId)
        {           
          window.location.href="INVUP_Supplier.aspx?Action=I";               
          return false;
        }
        
      
           function SelectFunction(str3,strAdd)
        {   
        
            //alert(str3);
            var pos=str3.split('|'); 
        
            
            if (window.opener.document.forms['form1']['hdChallanSupplierName']!=null)
            {
            window.opener.document.forms['form1']['hdChallanSupplierName'].value=pos[0];
            window.opener.document.forms['form1']['txtSupplierName'].value=pos[1];
            window.opener.document.forms['form1']['txtChallanSupplierAddress'].value=pos[5];
            window.close();
            }
            
            if (window.opener.document.forms['form1']['hdSupplierID']!=null)
            {
            window.opener.document.forms['form1']['hdSupplierID'].value=pos[0];
            }
           if (window.opener.document.forms['form1']['txtSupplierName']!=null)
            {
            window.opener.document.forms['form1']['txtSupplierName'].value=pos[1];
            }
         
              if (window.opener.document.forms['form1']['txtPhone']!=null)
            {
            window.opener.document.forms['form1']['txtPhone'].value=pos[2];
            }
             if (window.opener.document.forms['form1']['txtCity']!=null)
            {
            window.opener.document.forms['form1']['txtCity'].value=pos[3];
            }
             if (window.opener.document.forms['form1']['txtPhone']!=null)
            {
            window.opener.document.forms['form1']['txtPhone'].value=pos[4];
            }
            
            
              if (window.opener.document.forms['form1']['txtAddress']!=null)
           {
            window.opener.document.forms['form1']['txtAddress'].value=strAdd;
            }
            
            window.close ();
      
        }
       
        
        
      
        function DeleteFunction(SUPPLIERID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
            
              document.getElementById('<%=hdID.ClientId%>').value  =SUPPLIERID
                return true;
            }
            return false;
	      }
	      
        
	      
            function DeleteFunction2(SUPPLIERID)
          {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
            document.getElementById('<%=hdID.ClientId%>').value  =SUPPLIERID
                return true;
            }
            return false;
	      }
	      
        

	      
        function SupplierReset()
        {
         document.getElementById("txtName").value=""; 
         document.getElementById("lblError").innerHTML=""; 
         document.getElementById("drpCity").selectedIndex=0;  
         document.getElementById("drpCountry").selectedIndex=0;  
          if (document.getElementById("pnlPaging")!=null) 
        document.getElementById("pnlPaging").style.display ="none"; 
         
        if (document.getElementById("gvSupplier")!=null) 
        document.getElementById("gvSupplier").style.display ="none"; 
        document.getElementById("txtName").focus(); 
        return false;
        }
    
  
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultfocus="txtName" defaultbutton="btnSearch">
   
     <div>
    <table width="840px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Supplier </span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Supplier</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td class="center" colspan="7"  height="25px" >
                                              <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                        </tr>
                                         <tr>
                                            <td style="width:15%"></td>
                                            <td class="textbold" style="width:8%"> Name</td>                                                                               
                                            <td colspan="4" style="width:52%">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1" Width="426px"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%;" class="center">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" AccessKey="A" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width:15%">
                                            </td>
                                            <td class="textbold" style="width:8%">
                                                City</td>
                                            <td style="width: 20%"><asp:DropDownList onkeyup="gotop(this.id)" ID="drpCity" runat="server" CssClass="dropdownlist" Width="160px" TabIndex="2">
                                            </asp:DropDownList></td>
                                            <td class="textbold" style="width:4%">
                                               </td>
                                            <td class="textbold" style="width: 8%">
                                                Country</td>
                                            <td style="width: 20%">
                                            <asp:DropDownList ID="drpCountry" onkeyup="gotop(this.id)" runat="server" CssClass="dropdownlist" Width="160px" TabIndex="3"></asp:DropDownList></td>
                                            <td class="center" style="width: 25%">
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:15%"></td>
                                            <td class="textbold" style="width:8%" >
                                                </td>
                                            <td style="width:20%">             &nbsp;<input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                             <td class="textbold" style="width:4%">
                                               </td>
                                            <td class="textbold" style="width:8%">     </td>
                                            <td style="width:20%">
                                                &nbsp;</td>
                                            <td style="width:25%" class="center"> 
                                                 <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="6" Text="Export" AccessKey="E" /></td>
                                        </tr>
                                         <tr>
                                        <td ></td>
                                        <td >       </td>    <td  colspan="4">
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                             <td colspan="1" class="center">
                                                 <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                       </tr>                                                         
                                          <tr>
                                            <td colspan="7" style ="width:100%" valign ="top" >
                                                <asp:GridView  ID="gvSupplier" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="100%" EnableViewState="False" AllowSorting="True">
                                                <Columns>
                                             
                                                   <asp:BoundField HeaderText="Name" DataField="SUPPLIERNAME" SortExpression="SUPPLIERNAME" >
                                                       <ItemStyle Width="20%" Wrap="True" />
                                                   </asp:BoundField>
                                                    <asp:BoundField HeaderText="Address" DataField="ADDRESS" SortExpression="ADDRESS" >
                                                        <ItemStyle Width="25%" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="City " DataField="CITY" SortExpression="CITY">
                                                        <ItemStyle Width="15%" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Country " DataField="COUNTRY" SortExpression="COUNTRY">
                                                        <ItemStyle Width="12%" Wrap="False" />
                                                    </asp:BoundField>    
                                                     <asp:BoundField HeaderText="Phone" DataField="PHONENUMBER" SortExpression="PHONENUMBER" >
                                                         <ItemStyle Width="18%" Wrap="True" />
                                                     </asp:BoundField>  
                                                     <asp:TemplateField  HeaderText="Action">
                                                                    <ItemTemplate>
                                                                       <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SUPPLIERID") + "|" + DataBinder.Eval(Container.DataItem, "SUPPLIERNAME")+  "|" + DataBinder.Eval(Container.DataItem, "CITY")+ "|" + DataBinder.Eval(Container.DataItem, "COUNTRY") + "|" + DataBinder.Eval(Container.DataItem, "PHONENUMBER") + "|" + DataBinder.Eval(Container.DataItem, "ADDRESS") %>'>Select</asp:LinkButton>&nbsp;&nbsp;
                                                                       <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;
                                                                        <asp:LinkButton ID="linkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton>
                                                                      &nbsp;
                                                                       <asp:HiddenField ID="hdSuppId" runat="server" Value='<%#Eval("SUPPLIERID")%>' />
                                                                       <asp:HiddenField ID="hdSuppAdd" runat="server" Value='<%#Eval("ADDRESS")%>' />   
                                                                    </ItemTemplate>                                                                  
                                                         <HeaderStyle Wrap="False" />
                                                         <ItemStyle Width="10%" Wrap="False" />
                                                                </asp:TemplateField> 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                        <tr>
                                            <td colspan="7" style="width: 100%" valign="top">
                                               <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                <asp:HiddenField ID="hdID" runat="server" />
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
    </div>
   
    </form>
   
</body>
</html>
