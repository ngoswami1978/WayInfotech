<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_CityAverageTerminal.aspx.vb" Inherits="Market_MTSR_CityAverageTerminal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS: Market</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script type="text/javascript" language="javascript" >
    
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
    </script>
   
</head>



<body>
    <form id="form1" runat="server">
      <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px; height: 245px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-></span><span class="sub_menu">City Average Terminal</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                City Average Terminal&nbsp;</td>
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
                                                        Country</td>
                                                                  <td style="width: 172px" ><asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                  <td  class="textbold" style="width: 115px">
                                                        City</td>
                                                                  <td style="width: 236px" ><asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="2">
                                                                  </asp:DropDownList></td>
                                                                  <td style="width: 176px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search"  AccessKey="A"/></td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 162px; height: 25px;">
                                                                      1a Office</td>
                                                                    <td style="width: 172px; height: 25px;">
                                                                      <asp:DropDownList ID="drpOneAoffice" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                      </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 115px; height: 25px;" >
                                                        Region</td>
                                                                    <td style="width: 236px; height: 25px;">
                                                                         

                                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="18">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 25px">
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export"  AccessKey="E"/></td>
                                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px;">
                                                                      </td>
                                                                  <td style="width: 172px; height: 25px;">
                                                                      <asp:CheckBox ID="chkShowBr" runat="server" Text="NBS " Width="176px" CssClass="textbold" /></td>
                                                                  <td class="textbold" style="width: 115px; height: 25px;">
                                                                      Group Type</td>
                                                                  <td style="width: 236px; height: 25px;" class="textbold"><asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="18">
                                                                  </asp:DropDownList></td>
                                                                  <td style="height: 25px">
                                                                  <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 16px">
                                                                    </td>
                                                                    <td class="subheading" colspan="2" style="height: 16px" >
                                                                        Comparision Date</td>
                                                                        <td class="subheading" colspan="2" style="height: 16px" >
                                                                        </td>
                                                                        
                                                                        <td style="height: 16px">
                                                                  </td>
                                                                </tr>
                                                              
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px">
                                                                      Month &nbsp;</td>
                                                                  <td style="width: 172px; height: 25px">
                                                                        <asp:DropDownList ID="drpMonthF" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="19">
                                                                        </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                      Year</td>
                                                                  <td style="width: 236px; height: 25px">
                                                                        <asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="20">
                                                                        </asp:DropDownList></td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                                <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px">
                                                                       &nbsp;</td>
                                                                  <td style="width: 172px; height: 25px">
                                                                       </td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                      </td>
                                                                  <td style="width: 236px; height: 25px">
                                                                       </td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                                <tr>
                                                                
                                                                <td colspan="6">
                                                                
                                                                 <asp:GridView EnableViewState ="false" AllowSorting="true" HeaderStyle-ForeColor="white" ID="grdvCityAvgTerminal"   runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="752px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                              <Columns>
                                                              
                                                              <asp:TemplateField SortExpression="CITY" HeaderText="City">
                                                              <ItemTemplate>
                                                              <asp:Label ID="lblAirlineNameVal" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHTotal" runat="server" Text="Total"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                               
                                                                <asp:TemplateField SortExpression="PRODUCTIVITY"  HeaderText="Productivity">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAMADEUSVal" runat="server" Text='<%# Eval("PRODUCTIVITY") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAMADEUS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression="PASSIVE"  HeaderText="NBS">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVEVal" runat="server" Text='<%# Eval("PASSIVE") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAMADEUS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression="WITHPASSIVE"  HeaderText="With NBS">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblWITHPASSIVEval" runat="server" Text='<%# Eval("WITHPASSIVE") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAMADEUS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                            <asp:TemplateField SortExpression="PCCOUNT" HeaderText="PC Count">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblABACUSVal" runat="server" Text='<%# Eval("PCCOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHABACUS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="TAVERAGE" HeaderText="Average Per PC">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblGALILEOVal" runat="server" Text='<%# Eval("TAVERAGE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHGALILEO" runat="server"></asp:Label>
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
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
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
                                 <asp:HiddenField ID="hdAMADEUS" runat="server" />
                                                                        <asp:HiddenField ID="hdABACUS" runat="server" />
                                                                        <asp:HiddenField ID="hdGALILEO" runat="server" />
                                                                        <asp:HiddenField ID="hdWORLDSPAN" runat="server" />
                                                                        <asp:HiddenField ID="hdSABREDOMESTIC" runat="server" />
                                                                        <asp:HiddenField ID="hdNETBOOKINGS" runat="server" />
                                     
                                                     
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
