<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_StockStatus.aspx.vb" Inherits="Inventory_INVSR_StockStatus" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Stock Status</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script language="javascript" type="text/javascript">
      
                
       // function DetailsFunction(Args)
       function DetailsFunction(GodownId,ProductID,GroupId)
        {    
           // alert(Args);   
           // var pos=Args.split('|');  
            var type;        
            //type = "../Inventory/INVSR_SerialNoDetails.aspx?Popup=T&GoDownId=" + pos[0] +  "&PrdId=" +  pos[1] + "&GroupId=" +  pos[2]   ;
            type = "../Inventory/INVSR_SerialNoDetails.aspx?Popup=T&GoDownId=" + GodownId +  "&PrdId=" +  ProductID + "&GroupId=" +  GroupId   ;
   	        window.open(type,"aa","height=600,width=920,top=10,left=20,scrollbars=1,status=1");	
            return false;          
          
        }
        
     
        
           function SelectFunction(str3,strAdd)
        {   
            //alert(str3);
            var pos=str3.split('|'); 
          
            
            window.close ();
      
        }
       
        function StockStatusMandatory()
        
        {
        
        }
    
        function StockStatusReset()
        {
         document.getElementById("txtEquipCode").value=""; 
          document.getElementById("txtEquipGroup").value=""; 
          document.getElementById("txtRecordCount").value="0"; 
         document.getElementById("lblError").innerHTML=""; 
         document.getElementById("drpGodown").selectedIndex=0;  
         document.getElementById("drpProduct").selectedIndex=0;  
        if (document.getElementById("gvStockStatus")!=null) 
        document.getElementById("gvStockStatus").style.display ="none"; 
        document.getElementById("drpGodown").focus(); 
        return false;
        }
    
  
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtEquipGroup" defaultbutton="btnSearch">
   
     <div>
    <table width="840px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Stock Status Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Stock Status</td>
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
                                            <td style="width:3%">
                                            </td>
                                            <td class="textbold" style="width:15%">
                                                Godown</td>
                                            <td style="width: 24%"><asp:DropDownList onkeyup="gotop(this.id)" ID="drpGodown" runat="server" CssClass="dropdownlist" Width="191px" TabIndex="1">
                                            </asp:DropDownList></td>
                                            <td class="textbold" style="width:3%">
                                               </td>
                                            <td class="textbold" style="width: 15%">
                                               Product</td>
                                            <td style="width: 24%"><asp:DropDownList onkeyup="gotop(this.id)" ID="drpProduct" runat="server" CssClass="dropdownlist" Width="192px" TabIndex="2">
                                            </asp:DropDownList>
                                            </td>
                                            <td class="center" style="width: 16%">
                                               <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="5" Width="85px"  AccessKey="A"/></td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width:3%">
                                            </td>
                                            <td class="textbold" style="width:15%">
                                                Equipment Group</td>
                                            <td style="width: 24%">
                                            <asp:TextBox ID="txtEquipGroup" runat="server" CssClass="textbox" MaxLength="3" TabIndex="3" Width="185px"></asp:TextBox></td>
                                            <td class="textbold" style="width:3%">
                                               </td>
                                            <td class="textbold" style="width: 15%">
                                                Equipment Code</td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtEquipCode" runat="server" CssClass="textbox" MaxLength="3" TabIndex="4"
                                                    Width="185px"></asp:TextBox></td>
                                            <td class="center" style="width: 16%">
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="6" Text="Export" AccessKey="E"
                                                    Width="85px" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:3%"></td>
                                            <td class="textbold" style="width:15%" >
                                                </td>
                                            <td style="width:24%">
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                             <td class="textbold" style="width:3%">
                                               </td>
                                            <td class="textbold" style="width:15%">     </td>
                                            <td style="width:24%">
                                                &nbsp;</td>
                                            <td style="width:16%" class="center"> 
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" Width="85px" AccessKey="R" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:3%"></td>
                                            <td class="textbold" style="width:15%" >
                                               <%-- Total Record--%></td>
                                            <td style="width:24%">  <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="False"></asp:TextBox></td>
                                             <td class="textbold" style="width:3%">
                                               </td>
                                            <td class="textbold" style="width:15%">     </td>
                                            <td style="width:24%">
                                                &nbsp;</td>
                                            <td style="width:16%" class="center"> </td>
                                        </tr>                                               
                                          <tr>
                                            <td colspan="7" style ="width:100%" valign ="top" >
                                                    <asp:GridView  ID="gvStockStatus" runat="server"  AutoGenerateColumns="False" TabIndex="8" Width="100%" EnableViewState="False" AllowSorting="True">
                                                            <Columns>  
                                                               <asp:BoundField HeaderText="Godown" DataField="GODOWNNAME" SortExpression="GODOWNNAME" >
                                                                   <ItemStyle Width="34%" Wrap="True" />
                                                                   <HeaderStyle Wrap="False" />
                                                               </asp:BoundField>
                                                                <asp:BoundField HeaderText="GroupCode" DataField="GROUP_CODE" SortExpression="GROUP_CODE" >
                                                                    <ItemStyle Width="6%" Wrap="True" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>                                                   
                                                                <asp:BoundField HeaderText="EquipCode" DataField="EQUIPMENT_CODE" SortExpression="EQUIPMENT_CODE">
                                                                    <ItemStyle Width="6%" Wrap="False" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Product" DataField="PRODUCTNAME" SortExpression="PRODUCTNAME">
                                                                    <ItemStyle Width="18%" Wrap="False" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>   
                                                                <asp:BoundField HeaderText="QtyReceived" DataField="QTYRECEIVED" SortExpression="QTYRECEIVED" >
                                                                    <ItemStyle Width="6%" Wrap="True" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="QtyInHand" DataField="QTYINHAND" SortExpression="QTYINHAND" >
                                                                    <ItemStyle Width="6%" Wrap="True" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="ScrapQty" DataField="QTYSCRAP" SortExpression="QTYSCRAP" >
                                                                    <ItemStyle Width="6%" Wrap="True" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>                                                   
                                                                <asp:BoundField HeaderText="StockOut" DataField="STOCKOUT" SortExpression="STOCKOUT">
                                                                    <ItemStyle Width="6%" Wrap="False" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>  
                                                                                                                    
                                                                <asp:BoundField HeaderText="PC Ins" DataField="PC_INSTALLED" SortExpression="PC_INSTALLED">
                                                                    <ItemStyle Width="6%" Wrap="False" />
                                                                    <HeaderStyle Wrap="False" />
                                                                </asp:BoundField>  
                                                                
                                                                <asp:TemplateField  HeaderText="Action"> 
                                                                              <ItemTemplate>
                                                                                   <%--<asp:LinkButton  ID="lnkDetails" Runat="server" CssClass="LinkButtons" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "GODOWNID") + "|" + DataBinder.Eval(Container.DataItem, "PRODUCTID")+  "|" + DataBinder.Eval(Container.DataItem, "GROUP_CODE")%>'>Details</asp:LinkButton>--%>
                                                                                   <asp:LinkButton  ID="lnkDetails" Runat="server" CssClass="LinkButtons" >Details</asp:LinkButton>
                                                                                   <asp:HiddenField ID="hdStockId" runat="server" Value='<%#Eval("GODOWNID")%>' />
                                                                                   <asp:HiddenField ID="hdGODOWNID" runat="server" Value= '<%#Eval("GODOWNID")%>' />
                                                                                    <asp:HiddenField ID="hdPRODUCTID" runat="server" Value='<%#Eval("PRODUCTID")%>' />
                                                                                    <asp:HiddenField ID="hdGROUP_CODE" runat="server" Value='<%#Eval("GROUP_CODE")%>' />
                                                                                </ItemTemplate>                                                                  
                                                                    <ItemStyle Width="6%" Wrap="False" />
                                                                    <HeaderStyle Wrap="False" />
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
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly="true"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
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
    </div>
   
    </form>
   
</body>
</html>
