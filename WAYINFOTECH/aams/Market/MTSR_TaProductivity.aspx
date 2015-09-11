<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_TaProductivity.aspx.vb" Inherits="Market_MTSR_TaProductivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS: Market</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script type="text/javascript" language="javascript" >
    function ValidateSearch()
    {
    if(document.getElementById("drpProviders").selectedIndex=='0')
    {
    document.getElementById("lblError").innerHTML="Provider is Mandatory";
    return false;
    }
    
    }
    
    
     function CallPrint( strid )
{

var prtContent = document.getElementById( strid );
prtContent.border = 0; //set no border here
var strOldOne=prtContent.innerHTML;
var WinPrint = window.open('','','letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
WinPrint.document.write(prtContent.outerHTML);
WinPrint.document.close();
WinPrint.focus();
WinPrint.print();
WinPrint.close();

}
    function ResetControls()
    {
  
   
     document.getElementById("drpProviders").selectedIndex='0';
     document.getElementById("drpCity").selectedIndex='0';
     document.getElementById("drpCountry").selectedIndex='0';
     document.getElementById("drpRegion").selectedIndex='0';
     document.getElementById("drpOneAoffice").selectedIndex='0';
     //document.getElementById("grdvTaProductivity").style.display='none'
     return false;
    }
    </script>
   
</head>


<body >
    <form id="form1" defaultbutton="btnSearch" defaultfocus="drpProviders" runat="server">
      <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px; height: 245px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-></span><span class="sub_menu">TA Productivity</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                TA Productivity
                            </td>
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
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" height="25" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                                      Providers</td>
                                                                  <td style="width: 172px" ><asp:DropDownList ID="drpProviders" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                  
                                                                  </asp:DropDownList></td>
                                                                  <td  class="textbold" style="width: 115px">
                                                        City</td>
                                                                  <td style="width: 236px" ><asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="1">
                                                                  </asp:DropDownList></td>
                                                                  <td style="width: 176px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"  AccessKey="A"/></td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 162px">
                                                        Country</td>
                                                                    <td style="width: 172px"><asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 115px" >
                                                        Region</td>
                                                                    <td style="width: 236px">
                                                                         

                                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="2" AccessKey="E" /></td>
                                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                                      1a Office</td>
                                                                  <td style="width: 172px">
                                                                      <asp:DropDownList ID="drpOneAoffice" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px">
                                                                      </td>
                                                                  <td style="width: 236px" class="textbold">
                                                                      <asp:CheckBox ID="chkShowBr" runat="server" Text="Show Average" Width="120px" TabIndex="1" /></td>
                                                                  <td>
                                                                  <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"  AccessKey="R"/></td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                                      Group Type</td>
                                                                  <td style="width: 172px">
                                                                      <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px">
                                                                      Company Vertical</td>
                                                                  <td class="textbold" style="width: 236px">
                                                                      <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                          TabIndex="1" Width="144px">
                                                                      </asp:DropDownList></td>
                                                                  <td>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 20px">
                                                                  </td>
                                                                  <td class="subheading" align="left" colspan="3">
                                                                    Summary Options  
                                                                  </td>
                                                                  <td style="width: 236px">
                                                                  </td>
                                                                  <td>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 22px">
                                                                  </td>
                                                                  <td align="left"  class="textbold" colspan="3" style="height: 22px">
                                                                      <asp:RadioButtonList ID="rdSummaryOption" runat="server" CssClass="textbold" RepeatDirection="Horizontal" Width="416px" TabIndex="1">
                                                                          <asp:ListItem Value="1">City</asp:ListItem>
                                                                          <asp:ListItem Value="2" Selected="True">Country</asp:ListItem>
                                                                          <asp:ListItem Value="3">Region</asp:ListItem>
                                                                          <asp:ListItem Value="4">Amadeus Office</asp:ListItem>
                                                                          <asp:ListItem Value="5">Provider</asp:ListItem>
                                                                      </asp:RadioButtonList></td>
                                                                  <td style="width: 236px; height: 22px;">
                                                                  </td>
                                                                  <td style="height: 22px">
                                                                  </td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 20px">
                                                                    </td>
                                                                    <td class="subheading" colspan="2" >
                                                                        Date From</td>
                                                                        <td class="subheading" colspan="2" >
                                                                        Date To</td>
                                                                        
                                                                        <td>
                                                                       
                                                                        
                                                                        </td>
                                                                </tr>
                                                              
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px">
                                                                      Month &nbsp;</td>
                                                                  <td style="width: 172px; height: 25px">
                                                                        <asp:DropDownList ID="drpMonthF" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                      Month</td>
                                                                  <td style="width: 236px; height: 25px">
                                                                        <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 162px;">
                                                                        Year</td>
                                                                    <td style="height: 25px; width: 172px;">
                                                                    <asp:DropDownList ID="drpYearF" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 25px; width: 115px;">
                                                                        Year</td>
                                                                    <td style="height: 25px; width: 236px;" >
                                                                        <asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="1">
                                                                        </asp:DropDownList>&nbsp;</td>
                                                                    <td style="height: 25px" >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                
                                                                <td colspan="6">
                                                                
                                                                 <asp:GridView EnableViewState ="False" AllowSorting="true" HeaderStyle-ForeColor="white" ID="grdvTaProductivity"   runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="860px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" TabIndex="3">
                                                              <Columns>
                                                              
                                                              <asp:TemplateField SortExpression="SELECTBY"  HeaderText="Selectd By">
                                                              <ItemTemplate>
                                                              <asp:Label ID="lblSELECTBYVal" runat="server" Text='<%# Eval("SELECTBY") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression="MONTH"  HeaderText="Month">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblMONTHVal" runat="server" Text='<%# Eval("MONTH") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            <asp:TemplateField SortExpression="BAL"  HeaderText="BAL">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblBALVal" runat="server" Text='<%# Eval("BAL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="CMS"  HeaderText="CMS">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblCMSVal" runat="server" Text='<%# Eval("CMS") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                           
                                                            <asp:TemplateField SortExpression="ICI"  HeaderText="ICI">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblICIVal" runat="server" Text='<%# Eval("ICI") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="REL"  HeaderText="REL">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblRELVal" runat="server" Text='<%# Eval("REL") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="AIG"  HeaderText="AIG">
                                                                  <ItemTemplate>
                                                                    <asp:Label ID="lblAIGVal" runat="server" Text='<%# Eval("AIG") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                              <FooterTemplate>                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                             <asp:TemplateField SortExpression="CHR"  HeaderText="CHR">
                                                                  <ItemTemplate>
                                                                    <asp:Label ID="lblCHRVal" runat="server" Text='<%# Eval("CHR") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                              <FooterTemplate>                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>

                                                              
                                                                <asp:TemplateField SortExpression="BOOKING"  HeaderText="BOOKING">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblBOOKINGVal" runat="server" Text='<%# Eval("BOOKING") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression="TOTAL"  HeaderText="TOTAL">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblTOTALVal" runat="server" Text='<%# Eval("TOTAL") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                               </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <FooterStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>
                                                                </td>
                                                                </tr>
                                                                
                                                                
                                                                  <!-- code for paging----->
                                            <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly="true"></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            <!-- code for paging----->
            
                                                                
                                               </table>
                                                        </td>
                                                </tr>
                                                           
                                        </table>
                                        
                                          
                                        </td>
                                    </tr>
                                </table>
                                            <asp:HiddenField ID="hdMonth" runat="server" />
                                            <asp:HiddenField ID="hdBal" runat="server" />
                                            <asp:HiddenField ID="hdCms" runat="server" />
                                            <asp:HiddenField ID="hdIci" runat="server" />
                                            <asp:HiddenField ID="hdRel" runat="server" />
                                            <asp:HiddenField ID="hdAig" runat="server" />
                                            <asp:HiddenField ID="hdChr" runat="server" />
                                            <asp:HiddenField ID="hdBookings" runat="server" />
                                            <asp:HiddenField ID="hdTotal" runat="server" />
                                &nbsp;&nbsp;
                                     
                                                     
                                                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top" >
            <td valign="top" style="padding-left:4px; height: 21px;" >
            
            </td>
            </tr>
            <tr>
            
            
            </tr>
        </table>
    </form>
</body>
</html>
