<%@ Page Language="VB" EnableEventValidation = "false" AutoEventWireup="false" CodeFile="PDSR_TravelAssistanceBooking.aspx.vb"
    Inherits="Productivity_PDSR_TravelAssistanceBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Market</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
     function AgencyValidation()
     {
        {debugger;}
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
            {
            document.getElementById("hdAgencyNameId").value="";
            document.getElementById("hdAgencyName").value="";    //Added by Tapan Nath 19/03/2011        
            document.getElementById("chbWholeGroup").disabled=true;
            document.getElementById("chbWholeGroup").checked=false;
    	    }
    	
     }
   function PopupAgencyPage()
 {
    var type;
    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;
 }
 function CheckValidation()
     {
     
                if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Lcode.';             
                    document.getElementById("txtLcode").focus();
                    return false;
                 }                  
              }
              
              if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Chain code.';             
                    document.getElementById("txtChainCode").focus();
                    return false;
                 }                  
              }
              
         if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {     
                                                  
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                               
      }
//      function disableChkbox()
//  {
//     document.getElementById("chbWholeGroup").disabled = true;
//   	     
//  }
   </script>

</head>
<body  >
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtAgencyName" runat="server">
    
    
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td>
    <!-- Code for Search Criteria -->
    <table width="860px" align="left" class="border_rightred" id="TABLE1" language="javascript">
            <tr>
                <td valign="top" style="width: 850px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Productivity-></span><span class="sub_menu">Travel Assistance Booking</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Search Travel Assistance Bookings</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 851px;" class="redborder" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                    &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px;">
                                                                    Country</td>
                                                                <td style="width: 295px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCountry1" TabIndex="1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 322px; height: 25px;">
                                                                    City</td>
                                                                <td style="width: 306px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCity1" runat="server" TabIndex="1" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="185px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 176px; height: 25px;">
                                                                    <asp:Button ID="btnSearch" TabIndex="16" CssClass="button" runat="server" Text="Search" AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Agency</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtAgencyName" TabIndex="1" runat="server" CssClass="textbox" MaxLength="40"
                                                                        Style="position: relative" Width="93%"></asp:TextBox>
                                                                    <img alt="" tabindex="1" onclick="javascript:return PopupAgencyPage();"
                                                                        src="../Images/lookup.gif" style="cursor:pointer;"   /></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="17" Text="Export" AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Whole
                                                                    Group
                                                                </td>
                                                                <td  style="height: 25px">
                                                                    <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="textbold" Style="position: relative"
                                                                        TabIndex="1" TextAlign="Left" Width="144px" /></td>
                                                                        
                                                                        
                                                                        <td class="textbold" style="width: 322px; height: 25px;">
                                                                    Group Type</td>
                                                                <td style="width: 306px; height: 25px;">
                                                                    <asp:DropDownList ID="drpLstGroupType" runat="server" TabIndex="1" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="185px">
                                                                    </asp:DropDownList></td>
                                                                    
                                                                    
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset"  TabIndex="18" runat="server" CssClass="button" Style="position: relative"
                                                                        Text="Reset" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 62px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Lcode</td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 322px; height: 25px">
                                                                    Chain Code</td>
                                                                <td style="width: 306px; height: 25px">
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="178px"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 62px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Agency Group Category</td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 322px; height: 25px">
                                                                    Agency Category</td>
                                                                <td style="width: 306px; height: 25px">
                                                                    <asp:DropDownList TabIndex="7" ID="drpGroupAgencyType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px" Style="position: relative">
                                                                </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Provider Code</td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="ddlCrs" runat="server" CssClass="dropdownlist" Style="position: relative" onkeyup="gotop(this.id)"
                                                                        Width="184px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 322px; height: 25px">
                                                                    1 AOffice</td>
                                                                <td style="width: 306px; height: 25px">
                                                                    <asp:DropDownList TabIndex="1" ID="drpAOffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px" Style="position: relative">
                                                                       
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    Region</td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                        TabIndex="1" Width="184px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 25px; width: 322px;">Company Vertical
                                                                    </td>
                                                                <td style="height: 25px"><asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        TabIndex="1" Width="104px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 62px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    <asp:CheckBox ID="chkGroupClassification" runat="server" CssClass="textbold" Text="Show Agency Category" Width="152px" TabIndex="1"   /></td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:CheckBox ID="chkAverage" Text="Show Average" TabIndex="1" runat="server" CssClass="textbold" Style="position: relative"
                                                                        Width="112px" /></td>
                                                                <td class="textbold" style="width: 322px; height: 25px">
                                                                    <asp:CheckBox ID="chkOfficeID" TabIndex="1" Text="Show Office ID"
                                                                            runat="server"/></td>
                                                                <td style="width: 306px; height: 25px">
                                                                    </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    From&nbsp;
                                                                </td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="drpMonthFrom" TabIndex="1" runat="server" CssClass="dropdownlist"
                                                                        Width="104px" Style="position: relative">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                    <asp:DropDownList TabIndex="1" ID="drpYearFrom" runat="server" CssClass="dropdownlist"
                                                                        Width="64px" Style="position: relative; left: 8px; top: 0px;">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 322px; height: 25px">
                                                                    To</td>
                                                                <td style="height: 25px; width: 306px;">
                                                                    <asp:DropDownList ID="drpMonthTo" runat="server" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="104px" Style="position: relative">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                    <asp:DropDownList ID="drpYearTo" runat="server" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="72px" Style="position: relative; left: 0px; top: 0px;">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="height: 25px;" colspan="2">
                                                                    </td>
                                                                <td class="textbold" style="height: 25px; width: 322px;">
                                                                    </td>
                                                                <td style="height: 25px; width: 306px;"></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="height: 25px;" colspan="2"></td>
                                                                <td class="textbold" style="height: 25px; width: 322px;">
                                                                </td>
                                                                <td style="height: 25px; width: 306px;">
								                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                         <%--     <asp:Panel ID="pnlCount" runat="server" Visible ="false">  
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 62px;">
                                                                </td>
                                                                <td class="textbold" style="width: 214px; height: 25px">
                                                                    <strong>No of Records Found</strong></td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        ReadOnly="True" Style="position: relative" TabIndex="15" Text="0" Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 306px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            </asp:panel>--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                <asp:HiddenField ID="hdMonth" runat="server" />
                                <asp:HiddenField ID="hdBal" runat="server" />
                                <asp:HiddenField ID="hdCms" runat="server" />
                                <asp:HiddenField ID="hdIci" runat="server" />
                                <asp:HiddenField ID="hdRel" runat="server" />
                                <asp:HiddenField ID="hdAig" runat="server" />
                                <asp:HiddenField ID="hdChr" runat="server" />
                                <asp:HiddenField ID="hdProductivity" runat="server" />
                                <asp:HiddenField ID="hdTotal" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </td>
    <td></td>
    
    </tr>
    
    <tr>
    <td colspan="2">
    <!-- Code for Search Result Gridview & Paging -->
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
                <td valign="top" class="top border_rightred">
                    <table width="1200px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder" >
                                <asp:GridView  EnableViewState ="false" TabIndex="20" ID="grdTravelAssistance" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" ShowFooter="true" HeaderStyle-CssClass="Gridheading"
                                    RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" HeaderStyle-ForeColor="white" >
                                    <Columns>
                                    
                                    <asp:TemplateField HeaderText="LCode" SortExpression="LCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCode") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Chain Code" SortExpression="chain_code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("chain_code") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                        
                                    
                                    
                                    
                                    
                                        <asp:TemplateField HeaderText="Name" SortExpression="AgencyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Wrap="True" />
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                         
                                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OfficeID" SortExpression="OfficeID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OfficeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText ="Company Vertical" DataField="COMP_VERTICAL" HeaderStyle-Wrap="true"  ItemStyle-Wrap="false" SortExpression ="COMP_VERTICAL" />
                                        <asp:TemplateField HeaderText="City" SortExpression="CITY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lblHAgencyName" runat="server" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month" SortExpression="MONTH">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BAL" SortExpression="BAL" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblBALVal" runat="server" Text='<%# Eval("BAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHBAL" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CMS" SortExpression="CMS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCMSVal" runat="server" Text='<%# Eval("CMS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHCMS" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ICI" SortExpression="ICI">
                                            <ItemTemplate>
                                                <asp:Label ID="lblICIVal" runat="server" Text='<%# Eval("ICI") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHICI" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REL" SortExpression="REL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRELVal" runat="server" Text='<%# Eval("REL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHREL" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="AIG" SortExpression="AIG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAIGVal" runat="server" Text='<%# Eval("AIG") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHAIG" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="CHR" SortExpression="CHR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHRVal" runat="server" Text='<%# Eval("CHR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHCHR" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Productivity" SortExpression="Productivity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductivity" runat="server" Text='<%# Eval("Productivity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHProductivity" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdAdd" runat="server" Value='<%# Eval("ADDRESS") %>' />
                                                <asp:HiddenField ID="hdCountry" runat="server" Value='<%# Eval("COUNTRY") %>' />
                                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%#Eval("LCODE")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <FooterStyle CssClass="Gridheading" />
                                    <RowStyle CssClass="textbold" />
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                   <asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox>      
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
        <script  type ="text/javascript" language ="javascript" >
        ActDecLcodeChainCode();     
        if (document.getElementById("hdAgencyNameId").value=="")
              {
                  document.getElementById("chbWholeGroup").disabled=true;
                  document.getElementById("chbWholeGroup").checked==false;        
              }	 
        </script>  
</body>
</html>
