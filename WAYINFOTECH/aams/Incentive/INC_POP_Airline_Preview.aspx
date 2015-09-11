<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INC_POP_Airline_Preview.aspx.vb" Inherits="Incentive_INC_POP_Airline_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Airline Preview</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%"  height="100%" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Airline Data Preview</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td>
                                                                <table width="99%" border="0" align="center">
                                                                    <tr>
                                                                    <td   colspan="6" align="center" class="heading" Width="99%">
                                                                    <asp:Label ID="adjType" Text="" runat="server"  ></asp:Label> 
                                                                    </td>                                                             
                                                                    </tr>
                                                                </table>
                                                            </td>                                                            
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6"  align="center">
                                                                    <asp:GridView ID="grdvAirlinePrv" runat="server" BorderWidth="1px" BorderColor="#D4D0C8" AutoGenerateColumns="true" 
                                                                        Width="99%" TabIndex="9" AllowSorting="True">
                                                                                                                                                                                                          
                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="right"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="right" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                          <tr>
                                                                <td colspan="6" height="12" align ="left" >&nbsp;
                                                                   <table style ="width:100%">
                                                                      <tr id="trUpfront" runat ="server" visible ="false"  >
                                                                         <td class ="textbold">Upfront</td> <td></td> 
                                                                         <td><asp:Textbox ID="txtUpfront" runat ="server" ReadOnly ="true" CssClass ="textboxgrey right"></asp:Textbox> </td><td></td>  
                                                                         <td class ="textbold">Upfront type</td> <td></td> 
                                                                         <td><asp:Textbox ID="txtUpfrotntType" runat ="server" ReadOnly ="true" CssClass ="textboxgrey"></asp:Textbox> </td>
                                                                                                                                                  
                                                                         <td class ="textbold"><asp:Label ID="lblUpfrontPeriod" runat="server"></asp:Label></td> <td></td> 
                                                                         <td><asp:Textbox ID="TexUpfrontPeriod" runat ="server" ReadOnly ="true" CssClass ="textboxgrey right"></asp:Textbox> </td>
                                                                         
                                                                      </tr>
                                                                        <tr>
                                                                         <td></td> <td></td> <td></td>
                                                                      </tr>
                                                                        <tr>
                                                                         <td class ="textbold">Signup</td> <td></td> <td><asp:Textbox ID="txtSignup" runat ="server" ReadOnly ="true" CssClass ="textboxgrey right"  ></asp:Textbox> </td><td></td>  <td class ="textbold">
                                Signup adjustable</td> <td></td> <td><asp:Textbox ID="txtSignupAdjustable" runat ="server" ReadOnly ="true" CssClass ="textboxgrey"></asp:Textbox> </td>
                                                                      </tr>
                                                                   </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
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
        </table>
    </form>
</body>
</html>
