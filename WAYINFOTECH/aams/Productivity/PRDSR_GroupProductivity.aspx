<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="PRDSR_GroupProductivity.aspx.vb"
    Inherits="Productivity_PRDSR_GroupProductivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Productivity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">

function AgencyValidation()
     {
        document.getElementById("hdAgencyName").value="";
    	
     }
function PopupAgencyGroup()
 {
    var type;
    type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
    window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
     
 }

       function validateNumeric()
        {
        
       var  objdrpProductivity =document.getElementById('drpProductivity').value;
       var  objtxtNumeric1 =document.getElementById('txtNumeric1');
       var  objtxtNumeric2 =document.getElementById('txtNumeric2');       
       if (objdrpProductivity=='') 
       {
          objtxtNumeric1.disabled=true;
          objtxtNumeric2.disabled=true;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value='0';
          objtxtNumeric1.className='textboxgrey';
          objtxtNumeric2.className='textboxgrey';
           document.getElementById("drpAirlineCode").disabled=true;
           document.getElementById("drpAirlineCode").selectedIndex=0;
       } 
    else if (objdrpProductivity=='7') 
       {
         
          objtxtNumeric1.disabled=false;
          objtxtNumeric2.disabled=false;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value='0';
          objtxtNumeric1.className='textbox';
          objtxtNumeric2.className='textbox';  
           document.getElementById("drpAirlineCode").disabled=false;
       }   
       else 
       {
          objtxtNumeric1.disabled=false;
          objtxtNumeric2.disabled=true;
          objtxtNumeric1.value='0';
          objtxtNumeric2.value=''; 
          objtxtNumeric1.className='textbox';
          objtxtNumeric2.className='textboxgrey';  
             document.getElementById("drpAirlineCode").disabled=false;
       }
      
 
}
    function CheckValidation()
     {
     
              if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Chain code.';             
                    document.getElementById("txtChainCode").focus();
                    return false;
                 }                  
              }
              
           if(document.getElementById('<%=txtNumeric1.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtNumeric1").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtNumeric1").focus();
                    return false;
                 }                  
              }
               if(document.getElementById('<%=txtNumeric2.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtNumeric2").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtNumeric2").focus();
                    return false;
                 }                  
              }
                 if(parseInt(document.getElementById("txtNumeric1").value) >parseInt(document.getElementById("txtNumeric2").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Productivity range is not valid.';          
                    document.getElementById("txtNumeric1").focus();
                    return false;
                    } 
                       if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {     
                                                  
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
              
     }
       
    </script>

</head>
<body >
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtAgencyGroup" runat="server">
    
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
                                <span class="menu">Productivity--&gt;</span><span class="sub_menu">Group Productivity</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Search Group &nbsp;Productivity</td>
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
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px;">
                                                                    Country</td>
                                                                <td style="width: 234px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCountry" TabIndex="1" runat="server"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px;">
                                                                    City</td>
                                                                <td style="width: 223px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCity" runat="server" TabIndex="1"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 176px; height: 25px;">
                                                                    <asp:Button ID="btnSearch" TabIndex="2" CssClass="button" runat="server" Text="Search"  AccessKey="A"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    1 AOffice</td>
                                                                <td style="width: 234px; height: 25px">
                                                                    <asp:DropDownList TabIndex="1" ID="drpAOffice" runat="server" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative"  onkeyup="gotop(this.id)">
                                                                        <asp:ListItem>---Select One---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                        <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                        <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    Region</td>
                                                                <td style="width: 223px; height: 25px">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" TabIndex="1"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 176px; height: 25px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="2" Text="Export"  AccessKey="E"/></td>
                                                            </tr>
                                                      
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Chain Name</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtAgencyGroup" runat="server" CssClass="textbox" MaxLength="40" Style="position: relative"
                                                                        TabIndex="1" Width="94%"></asp:TextBox>
                                                                    <img alt="" onclick="javascript:return PopupAgencyGroup();" 
                                                                        src="../Images/lookup.gif" style="cursor:pointer;"  tabindex="6" /></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="2" Text="Reset"  AccessKey="R"/></td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td class="textbold" style="width: 82px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Chain Code</td>
                                                                <td style="width: 234px; height: 25px">
                                                                    <asp:TextBox ID="txtChaincode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"
                                                                        Width="176px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
                                                                </td>
                                                                <td style="width: 176px; height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                   </td>
                                                                <td class="textbold" style="width: 116px; height: 25px;">
                                                                    Total productivity</td>
                                                                <td style="height: 25px; width: 234px;">
                                                                    <asp:DropDownList TabIndex="1" ID="drpProductivity" runat="server" CssClass="dropdownlist"
                                                                        Width="184px"   >
                                                                        <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                        <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                        <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                        <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox TabIndex="1" ID="txtNumeric1" runat="server" CssClass="textboxgrey"
                                                                        MaxLength="9" Width="60px" Enabled="False">0</asp:TextBox>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                                                    <asp:TextBox ID="txtNumeric2" TabIndex="1" runat="server" CssClass="textboxgrey"
                                                                        MaxLength="9" Width="60px" Enabled="False">0</asp:TextBox>&nbsp; &nbsp;&nbsp;<asp:DropDownList ID="drpAirlineCode"
                                                                            Enabled="false" runat="server" CssClass="dropdownlist" Width="96px" TabIndex="1">
                                                                            <asp:ListItem Value="1">Total</asp:ListItem>
                                                                            <asp:ListItem Value="2">1A</asp:ListItem>
                                                                            <asp:ListItem Value="3">1B</asp:ListItem>
                                                                            <asp:ListItem Value="4">1G</asp:ListItem>
                                                                            <asp:ListItem Value="5">1P</asp:ListItem>
                                                                            <asp:ListItem Value="6">1W</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    From&nbsp;
                                                                </td>
                                                                <td style="width: 234px; height: 25px">
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
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    To</td>
                                                                <td style="height: 25px; width: 223px;">
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
                                                                <td class="textbold" style="height: 15px; width: 82px;">
                                                                </td>
                                                                 <td colspan="2" style="height: 15px">
                                                                     <asp:CheckBox ID="chkShowGroupClassification" runat="server" CssClass="textbold"
                                                                         TabIndex="1" Text="Show Agency Group Category" Width="198px" /></td>
                                                                <td class="textbold" style="width: 132px; height: 15px">
                                                                    Company Vertical</td>
                                                                <td style="width: 223px; height: 15px">
                                                                    <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        TabIndex="1" Width="104px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 15px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" colspan="2" style="height: 25px">
                                                                    <asp:RadioButtonList ID="rbl_CarrierType" runat="server" Height="8px" RepeatColumns="3"
                                                                        RepeatDirection="Horizontal" Style="left: 0px; position: relative; top: 0px"
                                                                        TabIndex="1" Width="97%">
                                                                        <asp:ListItem Selected="True" Value ="">All </asp:ListItem>
                                                                        <asp:ListItem Value="1">MNC</asp:ListItem>
                                                                        <asp:ListItem Value="2">Non MNC</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
                                                                </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 15px; width: 82px;">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 15px">
                                                                </td>
                                                                <td style="width: 234px; height: 15px">
                                                                </td>
                                                                <td class="textbold" style="width: 132px; height: 15px">
                                                                </td>
                                                                <td style="width: 223px; height: 15px">
                                                                </td>
                                                                <td style="height: 15px">
                                                                </td>
                                                            </tr>
                                                            <%-- <asp:Panel ID="pnlCount" runat="server" Visible ="false">
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td  id="a" class="textbold" style="height: 25px;" colspan="2">
                                                                    <strong>No of Records Found</strong><asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        ReadOnly="True" Style="position: relative" TabIndex="18" Text="0" Width="160px"></asp:TextBox></td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                </td>
                                                                <td style="height: 25px; width: 223px;">
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
                                <input type="hidden" id="hdChainId" runat="server" value="" style="width: 5px" />
                                <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                 <asp:HiddenField ID="hd1A" runat="server" />
                                <asp:HiddenField ID="hd1B" runat="server" />
                                <asp:HiddenField ID="hd1G" runat="server" />
                                <asp:HiddenField ID="hd1P" runat="server" />
                                <asp:HiddenField ID="hd1W" runat="server" />
                                <asp:HiddenField ID="hdTotal" runat="server" />
                                 <asp:HiddenField ID="hdNoOfPc" runat="server" />
                                <asp:HiddenField ID="hdNoOfPrinter" runat="server" />
                                <asp:HiddenField ID="hdNoOfTicket" runat="server" />
                                
                               
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
                    <table width="1000px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <asp:GridView EnableViewState="false" TabIndex="3" ID="grdProductivity" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue" ShowFooter="true" AllowSorting="True" HeaderStyle-ForeColor="white">
                                    <Columns>
                                        <asp:BoundField DataField="Chain_Code" HeaderText="Chain Code" SortExpression="Chain_Code">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="Chain Name" SortExpression="Chain_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChainName" runat="server" Text='<%# Eval("Chain_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHChainName" Text="Totals" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Wrap="True" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Type" SortExpression="Group_Classification_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpClassification" runat="server" Text='<%# Eval("Group_Classification_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblGrpClassification" Text="Totals" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Wrap="True" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="1A" SortExpression="AMADEUS" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblA" runat="server" Text='<%# Eval("AMADEUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHA" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="1B" SortExpression="ABACUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblB" runat="server" Text='<%# Eval("ABACUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHB" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="1G" SortExpression="GALILEO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblG" runat="server" Text='<%# Eval("GALILEO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHG" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="1P" SortExpression="WORLDSPAN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblP" runat="server" Text='<%# Eval("WORLDSPAN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHP" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="1W" SortExpression="SABREDOMESTIC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW" runat="server" Text='<%# Eval("SABREDOMESTIC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHW" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="TOTAL" SortExpression="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTOTAL" runat="server" Text='<%# Eval("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHTOTAL" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="No Of PC's" SortExpression="No_of_PC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo_of_PC" runat="server" Text='<%# Eval("No_of_PC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHNo_of_PC" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No of Printer's" SortExpression="No_of_Printer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo_of_Printer" runat="server" Text='<%# Eval("No_of_Printer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHNo_of_Printer" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="No of Ticket's" SortExpression="No_of_Ticket">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo_of_Ticket" runat="server" Text='<%# Eval("No_of_Ticket") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblHNo_of_Ticket" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
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
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px"  ReadOnly="true" CssClass="textboxgrey" ></asp:TextBox></td>
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
</body>
</html>
