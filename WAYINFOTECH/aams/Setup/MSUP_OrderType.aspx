<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_OrderType.aspx.vb" Inherits="Setup_MSUP_OrderType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Type</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">  
    </script>
    
</head>
<body>
    <form id="frmOrderType" runat="server">
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 519px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Order Type</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage Order Type
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="4" height="25px" align="center" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr >
                                                </tr>
                                                <tr>
                                                    <td class="textbold" width="20%">
                                                    </td>
                                                    <td class="textbold" width="10%">
                                                    </td>
                                                    <td style="width: 157px">
                                                    </td>
                                                    <td style="text-align: right; width: 97px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold" style="height: 22px">
                                                    </td>
                                                    <td  nowrap=nowrap width = "15%" style="height: 22px">
                                                        Order Type Category <span class="Mandatory">*</span>
                                                    </td>
                                                    <td style="width: 157px; height: 22px">
                                                        <asp:TextBox ID="txtOrderTypeCat" CssClass="textbox" runat="server" MaxLength="100" Width="192px"></asp:TextBox>
                                                     </td>
                                                    <td align="left" style="height: 22px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" AccessKey="S" />
                                                     </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width ="20%" style="height: 21px">
                                                    </td>
                                                    <td width = "15%" style="height: 21px">
                                                        Order Type <span class="Mandatory">*</span>
                                                    </td>
                                                    <td style="width: 157px">
                                                    <asp:TextBox ID="txtOrderType" CssClass="textbox" runat="server" MaxLength="100" Width="192px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New"  AccessKey="N"/>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    
                                                    <td>
                                                        Descriptions
                                                    </td>
                                                    
                                                    <td style="width: 157px">
                                                    <asp:TextBox ID="txtDescription" CssClass="textbox" runat="server" MaxLength="100" Width="192px"></asp:TextBox>
                                                    </td>
                                                    
                                                    <td align="left">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" />
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                <td >
                                                </td>
                                                    <td valign="middle" >
                                                       No.of Days Required </td>
                                                       <td style="width: 157px">
                                                    <asp:TextBox ID="txtDaysReq" CssClass="textbox" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </td>
                                                   
                                                   
                                                    <td   >
                                                    
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                <td >
                                                </td>
                                                    <td nowrap=nowrap>
                                                    Order Tracking Required &nbsp;&nbsp;&nbsp;
                                                       </td>
                                                       
                                                       <td style="width: 157px" >
                                                     <asp:CheckBox ID="CheckBox1" runat="server"  />
                                                   
                                                       </td>
                                                                                                     
                                                    <td   >
                                                    
                                                    </td>
                                                    
                                                    
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px">
                                                    </td>
                                                    <td style="height: 20px" >
                                                        <asp:RadioButton ID="rdBtnNewOrder" runat="server" Text="New Order" GroupName="ordr" TextAlign="Left"  />
                                                         </td>
                                                         <td style="height: 20px; width: 157px;">
                                                        <asp:RadioButton ID="rdBtnCancelOrder" runat="server" Text="Cancellation" GroupName="ordr" TextAlign="Left"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP" style="height: 15px">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="height: 20px">
                                                    </td>
                                                    <td style="height: 20px" >
                                                        <asp:CheckBox ID="chkIspOrdr" runat="server" Text="ISP Order" TextAlign="Left"/>
                                                           </td>
                                                           <td style="height: 20px; width: 157px;">
                                                       <asp:CheckBox ID="chkDeleted" runat="server" Text="Deleted" TextAlign="Left"/>
                                                        </td>
                                                  
                                                    </tr>
                                                    
                                                    <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td class="txtbold" style="height: 19px">
                                                        <strong>
                                                    PC Type </strong>
                                                   </td>
                                                    <td style="height: 19px; width: 157px;">
                                                    
                                                    </td>
                                                    <td style="height: 19px; width: 97px;"></td>
                                                </tr>
                                                                                                                                              
                                                <tr valign="top">
                                                    <td >
                                                    </td>
                                                    <td nowrap="nowrap" >
                                                      <asp:Panel ID="Panel1" runat="server" >
                                                     <asp:RadioButton ID="RadioButton1" runat="server" Text="Amadeus" GroupName="PC" TextAlign="Left"  />
                                                 
                                                    <asp:RadioButton ID="RadioButton2" runat="server" Text="Agency" GroupName="PC" TextAlign="Left" />
                                                 
                                                    <asp:RadioButton ID="RadioButton3" runat="server" Text="None" GroupName="PC" TextAlign="Left" />
                                                    
                                                    </asp:Panel>                                            
                                                    </td>
                                                    <td style="width: 157px">
                                                    
                                                    </td>
                                                    <td  >
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center">
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td style="height: 22px" >
                                                    </td>
                                                    <td style="height: 22px">
                                                   Old Connectivity
                                                   </td>
                                                   <td style="height: 22px; width: 157px;">
                                                    <asp:DropDownList ID="drplstOldConn" runat="server" Width="200px"  ></asp:DropDownList>
                                                   </td>
                                                  <td style="height: 22px" ></td>                                                            
                                                     
                                                </tr>
                                                <tr height="5px">
                                                    <td class="textbold" colspan="4" align="center" valign="TOP" >
                                                    </td>
                                                </tr>
                                                
                                                 <tr valign="top">
                                                    <td >
                                                    </td>
                                                    <td  >
                                                        New Connectivity                                           
                                                    </td>
                                                    <td valign="middle" style="width: 157px">
                                                     <asp:DropDownList ID="drpLstNewConn" runat="server" Width="200px"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                <td></td>
                                                <td colspan="2" class="ErrorMsg" style="height: 19px">
                                                        Field Marked * are Mandatory
                                                    </td>
                                                    
                                                <td>
                                                
                                                </td>
                                                
                                                </tr>
                                                <tr >
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
