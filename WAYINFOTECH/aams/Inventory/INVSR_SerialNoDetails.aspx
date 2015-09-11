<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_SerialNoDetails.aspx.vb"
    Inherits="Inventory_INVSR_SerialNoDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Serial No.</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->
    <!-- <script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script> -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
     
    function PrintBarCodes(ptarget)
        {
            
             ptarget.focus(); 
             ptarget.print();
        }     
        //function ModifySerailNoFunction(Args)
        function ModifySerailNoFunction(GodownId,ProductId,Serialno,VendorSerialNo,ProductName)
        {           
        
               
           // var pos=Args.split('|');  
            var type;        
            type = "../Inventory/INVUP_VendorSerialNoDetails.aspx?Popup=T&PrdId=" +  ProductId + "&SNO=" +  Serialno + "&VSNO=" +  VendorSerialNo  + "&PNAME=" +  ProductName;         
   	        window.open(type,"ab",'height=260px,width=620px,top=10,left=30,scrollbars=0,status=1');	
            return false;          
          
        } 
        function StockDetailsMandatory()
        
        {
        
        }
//        function StockDetailsReset()
//        {
//         document.getElementById("txtSerailNo").value=""; 
//          document.getElementById("txtEquipGroup").value="";
//            document.getElementById("txtEquipCode").value="";
//           
//         
//         document.getElementById("lblError").innerHTML=""; 
//         document.getElementById("drpGodown").selectedIndex=0;  
//         document.getElementById("drpProduct").selectedIndex=0;  
//        if (document.getElementById("gvStockDetails")!=null) 
//        document.getElementById("gvStockDetails").style.display ="none"; 
//          if (document.getElementById("gvStockDetailsOut")!=null) 
//        document.getElementById("gvStockDetailsOut").style.display ="none"; 
//        document.getElementById("txtSerailNo").focus(); 
//        return false;
//        }
    
  
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtSerailNo" defaultbutton="btnSearch">
        <div>
            <table width="840px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%" class="left">
                            <tr>
                                <td>
                                    <span class="menu">Inventory -&gt;</span><span class="sub_menu">Serial No Search</span></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <a href="#" class="LinkButtons" onclick="window.close();window.opener.document.forms['form1']['btnSearch'].click();">
                                        Close</a>&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="heading center" style="width: 840px; height: 20px;">
                                    Search Serial No</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder center" style="width: 800px">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 800px" class="left">
                                                    <tr>
                                                        <td class="center" colspan="7" height="20px">
                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                            Serial No</td>
                                                        <td style="width: 18%">
                                                            <asp:TextBox ID="txtSerailNo" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1"
                                                                Width="135px"></asp:TextBox></td>
                                                        <td class="textbold" style="width: 3%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                            Godown</td>
                                                        <td style="width: 28%">
                                                            <asp:DropDownList ID="drpGodown" runat="server" CssClass="dropdownlist" Width="224px"
                                                                TabIndex="2">
                                                            </asp:DropDownList></td>
                                                        <td class="center" style="width: 117px">
                                                            <asp:Button ID="btnSearch" AccessKey="A" CssClass="button" runat="server" Text="Search"
                                                                TabIndex="7" Width="100px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                            Vendor Sr. No</td>
                                                        <td style="width: 18%">
                                                            <asp:TextBox ID="txtVendorSerialNo" runat="server" CssClass="textbox" MaxLength="50"
                                                                TabIndex="1" Width="135px"></asp:TextBox></td>
                                                        <td class="textbold" style="width: 3%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                            Product</td>
                                                        <td style="width: 28%">
                                                            <asp:DropDownList ID="drpProduct" runat="server" CssClass="dropdownlist" Width="222px"
                                                                TabIndex="4">
                                                            </asp:DropDownList></td>
                                                        <td class="center" style="width: 117px">
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R"
                                                                TabIndex="8" Width="100px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%" valign="top">
                                                            Equipment Group</td>
                                                        <td style="width: 18%" valign="top">
                                                            <asp:TextBox ID="txtEquipGroup" runat="server" CssClass="textbox" MaxLength="50"
                                                                TabIndex="5" Width="135px"></asp:TextBox></td>
                                                        <td class="textbold" style="width: 3%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%" valign="top">
                                                        </td>
                                                        <td style="width: 28%" valign="top">
                                                        </td>
                                                        <td style="width: 117px" class="center" valign="top">
                                                            <asp:Button ID="BtnExport" AccessKey="E" CssClass="button" runat="server" Text="Export"
                                                                TabIndex="9" Width="100px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold" style="width: 15%" valign="top">
                                                            Equipment Code</td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtEquipCode" runat="server" CssClass="textbox" MaxLength="50" TabIndex="3"
                                                                Width="135px"></asp:TextBox></td>
                                                        <td style="width: 117px" align="center">
                                                        </td>
                                                        <td class="textbold" style="width: 3%" valign="top">
                                                            Stock Status</td>
                                                        <td class="textbold" style="width: 15%" valign="top">
                                                            <asp:RadioButtonList ID="rdStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                TabIndex="6" CssClass="textbox">
                                                                <asp:ListItem Value="1">In</asp:ListItem>
                                                                <asp:ListItem Value="2">Out</asp:ListItem>
                                                                <asp:ListItem Value="3">Scrap</asp:ListItem>
                                                                <asp:ListItem Value="4">Wrong</asp:ListItem>
                                                                <asp:ListItem Value="5">Install</asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                        <td style="width: 117px" class="center" valign="top">
                                                            <asp:Button ID="btnPrintBarCodes" CssClass="button" runat="server" Text="Print Bar Codes"
                                                                TabIndex="10" Width="100px" AccessKey="P" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 6%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                        </td>
                                                        <td style="width: 18%">
                                                            <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                TabIndex="3" Width="100px" ReadOnly="True" Text="0" Visible="False"></asp:TextBox></td>
                                                        <td class="textbold" style="width: 3%">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                        </td>
                                                        <td style="width: 28%">
                                                        </td>
                                                        <td class="center" style="width: 117px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" id="tdgvStockDetailsOut" runat="server" style="width: 1100px;" valign="top"
                                    class="redborder center">
                                    <table cellpadding="0" cellspacing="0" style="width: 1100px">
                                        <tr>
                                            <td style="width: 1100px">
                                                <asp:GridView ID="gvStockDetailsOut" runat="server" AutoGenerateColumns="false" TabIndex="12"
                                                    AllowSorting="true" HeaderStyle-ForeColor="white" Width="1100px" EnableViewState="true"
                                                    HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left">
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="1" class="Gridheading" style="width: 1100px">
                                                            <tr>
                                                                <td>
                                                                    Aoffice</td>
                                                                <td>
                                                                    Equipment Group</td>
                                                                <td>
                                                                    Equipment Code</td>
                                                                <td>
                                                                    Product</td>
                                                                <td>
                                                                    Serial No.</td>
                                                                <td>
                                                                    Vendor S. No.</td>
                                                                <td>
                                                                    Challan No.</td>
                                                                <td>
                                                                    Challan Date</td>
                                                                <td>
                                                                    Out To</td>
                                                                <td>
                                                                    Receiving Date</td>
                                                                <td>
                                                                    Action</td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE">
                                                            <ItemStyle Wrap="True" />
                                                            <HeaderStyle Wrap="False" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Equipment Group" DataField="EGROUP_CODE" SortExpression="EGROUP_CODE">
                                                            <ItemStyle Wrap="True" />
                                                            <HeaderStyle Wrap="true" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Equipment Code " DataField="EQUIPMENT_CODE" SortExpression="EQUIPMENT_CODE">
                                                            <ItemStyle Wrap="False" />
                                                            <HeaderStyle Wrap="true" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Product " DataField="PRODUCTNAME" SortExpression="PRODUCTNAME">
                                                            <ItemStyle Wrap="False" />
                                                            <HeaderStyle Wrap="False" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Serial No." DataField="SERIALNUMBER" SortExpression="SERIALNUMBER">
                                                            <ItemStyle Width="140px" Wrap="True" />
                                                            <HeaderStyle Wrap="true" Width="140px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Vendor S.No." DataField="VENDORSR_NUMBER" SortExpression="VENDORSR_NUMBER">
                                                            <ItemStyle Width="120px" Wrap="True" />
                                                            <HeaderStyle Wrap="true" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Challan No." DataField="CHALLANNUMBER" SortExpression="CHALLANNUMBER">
                                                            <ItemStyle Wrap="True" />
                                                            <HeaderStyle Wrap="true" Width="140px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Challan Date" DataField="CREATIONDATE" SortExpression="CREATIONDATE">
                                                            <ItemStyle Wrap="False" />
                                                            <HeaderStyle Wrap="true" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Out To" DataField="OUTTO" SortExpression="OUTTO">
                                                            <ItemStyle Wrap="False" />
                                                            <HeaderStyle Wrap="False" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Receiving Date" DataField="PRODUCTRECEIVEDDATE" SortExpression="PRODUCTRECEIVEDDATE">
                                                            <ItemStyle Wrap="False" />
                                                            <HeaderStyle Wrap="true" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkModifySNo" runat="server" CssClass="LinkButtons" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "GODOWNID") + "|" + DataBinder.Eval(Container.DataItem, "PRODUCTID")+  "|" + DataBinder.Eval(Container.DataItem, "SERIALNUMBER") + "|" + DataBinder.Eval(Container.DataItem, "VENDORSR_NUMBER")  + "|" + DataBinder.Eval(Container.DataItem, "PRODUCTNAME") %>'>Modify vendor S. No.</asp:LinkButton>
                                                                <asp:HiddenField ID="hdGoDownId" runat="server" Value='<%#Eval("GODOWNID")%>' />
                                                                <asp:HiddenField ID="hdPrdId" runat="server" Value='<%#Eval("PRODUCTID")%>' />
                                                                <asp:HiddenField ID="hdSuppId" runat="server" Value='<%#Eval("SUPPLIERID")%>' />
                                                                <asp:HiddenField ID="hdSerialno" runat="server" Value='<%#Eval("SERIALNUMBER")%>' />
                                                                <asp:HiddenField ID="hdVendorSerialNo" runat="server" Value='<%#Eval("VENDORSR_NUMBER")%>' />
                                                                <asp:HiddenField ID="hdProductName" runat="server" Value='<%#Eval("PRODUCTNAME")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" Wrap="False" />
                                                            <HeaderStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" id="tdgvStockDetails" runat="server" style="width: 750px;" valign="top"
                                    class="redborder center">
                                    <table cellpadding="0" cellspacing="0" style="width: 750px">
                                        <tr>
                                            <td style="width: 800px">
                                                <asp:GridView ID="gvStockDetails" runat="server" AutoGenerateColumns="false" TabIndex="11"
                                                    AllowSorting="true" HeaderStyle-ForeColor="white" Width="800px" EnableViewState="true"
                                                    HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left">
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="1" class="Gridheading" style="width: 800px">
                                                            <tr>
                                                                <td>
                                                                    Aoffice</td>
                                                                <td>
                                                                    Equipment Group</td>
                                                                <td>
                                                                    Equipment Code</td>
                                                                <td>
                                                                    Product</td>
                                                                <td>
                                                                    Serial No.</td>
                                                                <td>
                                                                    Vendor S. No.</td>
                                                                <td>
                                                                    Receiving Date</td>
                                                                <td>
                                                                    Action</td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE">
                                                            <ItemStyle Width="100px" Wrap="True" />
                                                            <HeaderStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Equipment Group" DataField="EGROUP_CODE" SortExpression="EGROUP_CODE">
                                                            <ItemStyle Width="100px" Wrap="True" />
                                                            <HeaderStyle Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Equipment Code " DataField="EQUIPMENT_CODE" SortExpression="EQUIPMENT_CODE">
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                            <HeaderStyle Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Product " DataField="PRODUCTNAME" SortExpression="PRODUCTNAME">
                                                            <ItemStyle Width="160px" Wrap="False" />
                                                            <HeaderStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Serial No." DataField="SERIALNUMBER" SortExpression="SERIALNUMBER">
                                                            <ItemStyle Width="130px" Wrap="False" />
                                                            <HeaderStyle Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Vendor S. No." DataField="VENDORSR_NUMBER" SortExpression="VENDORSR_NUMBER">
                                                            <ItemStyle Width="110px" Wrap="False" />
                                                            <HeaderStyle Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Receiving Date" DataField="PRODUCTRECEIVEDDATE" SortExpression="PRODUCTRECEIVEDDATE">
                                                            <ItemStyle Width="170px" Wrap="False" />
                                                            <HeaderStyle Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton ID="lnkModifySNo" runat="server" CssClass="LinkButtons" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "GODOWNID") + "|" + DataBinder.Eval(Container.DataItem, "PRODUCTID")+  "|" + DataBinder.Eval(Container.DataItem, "SERIALNUMBER") + "|" + DataBinder.Eval(Container.DataItem, "VENDORSR_NUMBER")  + "|" + DataBinder.Eval(Container.DataItem, "PRODUCTNAME") %>'>Modify vendor S . No.</asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkModifySNo" runat="server" CssClass="LinkButtons">Modify vendor S . No.</asp:LinkButton>
                                                                <asp:HiddenField ID="hdGoDownId" runat="server" Value='<%#Eval("GODOWNID")%>' />
                                                                <asp:HiddenField ID="hdPrdId" runat="server" Value='<%#Eval("PRODUCTID")%>' />
                                                                <asp:HiddenField ID="hdSuppId" runat="server" Value='<%#Eval("SUPPLIERID")%>' />
                                                                <asp:HiddenField ID="hdSerialno" runat="server" Value='<%#Eval("SERIALNUMBER")%>' />
                                                                <asp:HiddenField ID="hdVendorSerialNo" runat="server" Value='<%#Eval("VENDORSR_NUMBER")%>' />
                                                                <asp:HiddenField ID="hdProductName" runat="server" Value='<%#Eval("PRODUCTNAME")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" Wrap="False" />
                                                            <HeaderStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                             <tr>
                                <td colspan="6" id="tdStockDetailsInstalled" runat="server" style="width: 800px;" valign="top"
                                    class="redborder center">
                                    <table cellpadding="0" cellspacing="0" style="width: 800px">
                                        <tr>
                                            <td style="width: 800px">
                                                <asp:GridView ID="gvStockDetailsInstalled" runat="server" AutoGenerateColumns="false" TabIndex="11"
                                                    AllowSorting="true" HeaderStyle-ForeColor="white" Width="800px" EnableViewState="true"
                                                    HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left">
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="1" class="Gridheading" style="width: 800px">
                                                            <tr>
                                                                
                                                                 <td>
                                                                Lcode</td>
                                                            <td>
                                                                ChainCode</td>
                                                            <td>
                                                                Agency Name</td>
                                                            <td>
                                                                OfficeID</td>
                                                            <td>
                                                                City</td>
                                                            <td>
                                                                Country</td>
                                                            <td>
                                                                OnLine Status</td>
                                                            <td>
                                                                Installed Date</td>
                                                            <td>
                                                                Group Type</td>
                                                            <td>
                                                                Serial No</td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                  
                                                     <Columns>
                                                    <asp:BoundField HeaderText="Lcode" DataField="Lcode" SortExpression ="Lcode">
                                                        <ItemStyle  Wrap="True" />
                                                        <HeaderStyle Wrap="False" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="ChainCode" DataField="ChainCode" SortExpression ="ChainCode">
                                                        <ItemStyle  Wrap="True" />
                                                        <HeaderStyle Wrap="True" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Agency Name" DataField="AgencyName" SortExpression ="AgencyName">
                                                        <ItemStyle Wrap="False" />
                                                        <HeaderStyle Wrap="True" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="OfficeID " DataField="OfficeID"  SortExpression ="OfficeID">
                                                        <ItemStyle Wrap="False" />
                                                        <HeaderStyle Wrap="False" Width="80px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="City" DataField="City"  SortExpression ="City" >
                                                        <ItemStyle width="100px"  Wrap="True" />
                                                        <HeaderStyle Wrap="True" width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Country" DataField="Country"   SortExpression ="Country">
                                                        <ItemStyle Width="80px" Wrap="True" />
                                                        <HeaderStyle Wrap="True"  Width="80px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="OnLine Status" DataField="ONLINE_STATUS"  SortExpression ="ONLINE_STATUS">
                                                        <ItemStyle Wrap="True" />
                                                        <HeaderStyle Wrap="True" Width="60px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Installed Date"  DataField="INSTALLATION_DATE" SortExpression ="INSTALLATION_DATE">
                                                        <ItemStyle Wrap="False" />
                                                        <HeaderStyle Wrap="True"  Width="80px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Group Type" DataField="HARDWARE_TYPE" SortExpression ="HARDWARE_TYPE">
                                                        <ItemStyle Wrap="False" />
                                                        <HeaderStyle Wrap="False" Width="60px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Serial No" DataField="SERIALNUMBER" SortExpression ="SERIALNUMBER">
                                                        <ItemStyle Wrap="False" />
                                                        <HeaderStyle Wrap="True" Width="80px" />
                                                    </asp:BoundField>
                                                   
                                                </Columns>
                                                  
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="width: 800px">
                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr class="paddingtop paddingbottom">
                                                <td style="width: 30%" class="left">
                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                        ReadOnly="true"></asp:TextBox></td>
                                                <td style="width: 25%" class="right">
                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                <td style="width: 20%" class="center">
                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList></td>
                                                <td style="width: 25%" class="left">
                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <iframe id="ifrmPrnt" runat="server" frameborder="0" src="INVPrint_BarCodes.aspx"
            height="0" width="0"></iframe>
    </form>
</body>
</html>
