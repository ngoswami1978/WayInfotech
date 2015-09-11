<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_Challan.aspx.vb" Inherits="Inventory_INVUP_Challan"
    ValidateRequest="false" EnableEventValidation="False" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Inventory::Manage Challan </title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
function YesNoPrint()
{
iframeBarCode.focus();
iframeBarCode.print();
}
 
 // ashish work on officeID Search for challan Module
function FillAgencyDetailsChallanForOfficeIDSearch()
         {
        
        
                    var officeId;
                    officeId=  document.getElementById('txtOfficeId1').value;
                    var officeIdClassName=document.getElementById('txtOfficeId1').className;
         
                                if (officeId != ""  && officeIdClassName !="textboxgrey")
                                        {
                                                document.getElementById('txtAgencyName').value="Searching...";
                                                if  (document.getElementById("ddlChallanType").value=="")
                                                {
                                                    document.getElementById('lblError').innerText ="Challan Type  is Mandatory";
                                                    document.getElementById('txtAgencyName').value=""
                                                    document.getElementById('txtAddress').value=""
                                                    document.getElementById('txtCity').value=""
                                                    document.getElementById('txtCountry').value=""
                                                    document.getElementById('txtPhone').value=""
                                                    document.getElementById('txtFax').value=""
                                                    document.getElementById('txtOfficeId').value=""
                                                    document.getElementById('ddlChallanType').focus();
                                                    
                                                }
                                                else
                                                {
                                                    CallServerChallanOfficeIDSearch(officeId+'|OF',"This is context from client"); // Server view function
                                                }
                                                
                                         }
                      
           return false;
           }

// ashish work on officeID Search for challan Module

function ReceiveServerDataChallanOfficeIDSearch(args, context)
       {   
        
            document.getElementById('lblError').innerText ="";
            var obj = new ActiveXObject("MsXml2.DOMDocument");
            var dsRoot=obj.documentElement; 
          
                if (args =="")
                {
                
                        document.getElementById('hdChallanLCodeTemp').value="";    
                        document.getElementById('hdChallanLCode').value="";
                        document.getElementById('txtAgencyName').value=""
			            document.getElementById('txtAddress').value=""
			            document.getElementById('txtCity').value=""
			            document.getElementById('txtCountry').value=""
			            document.getElementById('txtPhone').value=""
			            document.getElementById('txtFax').value=""
			            document.getElementById('txtOfficeId').value=""
			            document.getElementById('ddlGodown').selectedIndex=0;         
			   
                }
          
                else
                {
                        var parts = args.split("$");
                        obj.loadXML(parts[0]);
                        var dsRoot=obj.documentElement; 
                        var strAddress ; 
                        
                    
        		    if (dsRoot !=null)
			                {
			                    document.getElementById('txtAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME")
			                    strAddress = dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + '\n ' + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS1")
			                    if (dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CHAIN_CODE")=="2145")
			                    {
			                        strAddress = strAddress + "**";
			                    }
			                    document.getElementById('txtAddress').value=strAddress
			                    document.getElementById('txtCity').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY")
			                    document.getElementById('txtCountry').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")
			                    document.getElementById('txtPhone').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("PHONE")
			                    document.getElementById('txtFax').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("FAX")
			                    document.getElementById('txtOfficeId').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("OfficeID")
			                    document.getElementById('hdChallanLCodeTemp').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE")
			                    document.getElementById('hdChallanLCode').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE")			                    
			                    document.forms['form1'].submit();
			                    
        		              }
            
               }
               
        }

    </script>

</head>
<body onload="HideShowChallanInventoryChallan()">
    <form id="form1" runat="server" defaultfocus="ddlChallanCategory">
        <table width="860px" class="border_rightred left">
            <tr>
                <td class="top" style="width: 790px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Inventory-></span><span class="sub_menu">Challan </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Manage Challan</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center gap">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold top" style="width: 90%; height: 2124px;">
                                                                    <asp:Panel ID="pnlDetails" runat="server" Width="100%">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" style="width: 130px">
                                                                                    Challan Category&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlChallanCategory" onchange="changeChallanCategoryInventoryChallan()"
                                                                                        runat="server" CssClass="dropdownlist" Width="174px" TabIndex="2" AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Challan Type <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlChallanType" runat="server" CssClass="dropdownlist"
                                                                                        Width="174px" TabIndex="2" onchange="changeChallanTypeInventoryChallan()">
                                                                                        <asp:ListItem Selected="True" Value="">--Select One--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Issue</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Receive</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" colspan="4">
                                                                                    <asp:Panel ID="pnlPurchaseOrder" runat="server" Width="100%">
                                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="subheading" colspan="4">
                                                                                                    Purchase Order</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold" style="width: 17%">
                                                                                                </td>
                                                                                                <td style="width: 27%">
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Purchase Order</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtPurchaseOrder" runat="server" CssClass="textboxgrey" Width="150px"
                                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                                                    <img id="Img1" runat="server" tabindex="2" alt="Select & Add Purchase Order" onclick="PopupPageInventoryChallan(1)"
                                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                                <td class="textbold">
                                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Order Date</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Supplier</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                        TabIndex="20" Width="525px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Description</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textboxgrey" Height="50px"
                                                                                                        ReadOnly="True" Rows="5" TabIndex="20" TextMode="MultiLine" Width="525px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlStockTransfer" runat="server" Width="100%" CssClass="displayNone">
                                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="subheading" colspan="4">
                                                                                                    Stock Transfer</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 17%">
                                                                                                </td>
                                                                                                <td style="width: 27%">
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Godown</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtChallanGodownName" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                        TabIndex="20" Width="500px"></asp:TextBox>
                                                                                                    <img id="Img4" runat="server" alt="Select & Add Godown" onclick="PopupPageInventoryChallan(7)"
                                                                                                        tabindex="2" src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Address</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtGodownAddress" runat="server" CssClass="textboxgrey" Height="50px"
                                                                                                        ReadOnly="True" Rows="5" TabIndex="20" TextMode="MultiLine" Width="525px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlCustomer" runat="server" Width="100%" CssClass="displayNone">
                                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="subheading" colspan="4">
                                                                                                    Customer</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 17%;">
                                                                                                </td>
                                                                                                <td style="width: 27%;">
                                                                                                </td>
                                                                                                <td style="width: 18%;">
                                                                                                </td>
                                                                                                <td style="width: 30%;">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 17%">
                                                                                                    Office Id</td>
                                                                                                <td style="width: 27%">
                                                                                                    <asp:TextBox ID="txtOfficeID1" runat="server" CssClass="textbox" MaxLength="20" ReadOnly="false"
                                                                                                        TabIndex="2" Width="170px"></asp:TextBox></td>
                                                                                                <td style="width: 18%">
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    <input id="hdChallanLCode" style="width: 1px" type="hidden" runat="server" />
                                                                                                    Agency Name
                                                                                                </td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" Width="500px"
                                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                                                    <img id="Img2" runat="server" tabindex="2" alt="Select & Add Agency Name" onclick="PopupPageInventoryChallan(2)"
                                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Address</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Width="520px"
                                                                                                        ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="20"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Country</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                        TabIndex="20" Width="165px"></asp:TextBox></td>
                                                                                                <td class="textbold">
                                                                                                    City</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="20"
                                                                                                        Width="170px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Phone</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" Width="165px" ReadOnly="True"
                                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                                                <td class="textbold">
                                                                                                    Fax</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" Width="170px" ReadOnly="True"
                                                                                                        TabIndex="20"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Office Id</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                        TabIndex="20" Width="165px"></asp:TextBox></td>
                                                                                                <td class="sub_menu" id="tdReplacementChallan" runat="server">
                                                                                                    Replacement Challan
                                                                                                </td>
                                                                                                <td>
                                                                                                    <table width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkReplacementChallan" runat="server" onclick="hideClearInventoryChallan(1)" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtRplIssueChallanNo" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td class="sub_menu" id="tdOrderAmadeusIndia" runat="server" width="200">
                                                                                                    Order for Amadeus India
                                                                                                </td>
                                                                                                <%--<td>
                                                                                                    <asp:CheckBox ID="chkOrderAmadeusIndia" runat="server" />
                                                                                                </td>--%>
                                                                                                <td>
                                                                                                    <table width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkOrderAmadeusIndia" runat="server" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td runat="server" class="textbold">
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td class="sub_menu" id="tdMiscChallan" runat="server">
                                                                                                    Misc H/W Challan
                                                                                                </td>
                                                                                                <%--<td>
                                                                                                    <asp:CheckBox ID="chkMiscChallan" runat="server" onclick="hideClearInventoryChallan(2)" />
                                                                                                </td>--%>
                                                                                                <td>
                                                                                                    <table width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="chkMiscChallan" runat="server" onclick="hideClearInventoryChallan(2)" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlReplacement" runat="server" Width="100%" CssClass="displayNone">
                                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="subheading" colspan="4">
                                                                                                    Replacement</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold" style="width: 17%">
                                                                                                </td>
                                                                                                <td style="width: 27%">
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Supplier Name
                                                                                                </td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textboxgrey" Width="500px"
                                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                                                    <img id="Img3" runat="server" tabindex="2" alt="Select & Add Supplier Name" onclick="PopupPageInventoryChallan(6)"
                                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="textbold">
                                                                                                    Address</td>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtChallanSupplierAddress" runat="server" CssClass="textboxgrey"
                                                                                                        Width="520px" ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="20"></asp:TextBox>
                                                                                                    <input id="hdChallanSupplierName" style="width: 1px" type="hidden" runat="server" /></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="subheading" colspan="4">
                                                                                    Challan Details</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Challan No</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Creation Date</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCreationDate" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Godown <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlGodown" runat="server" TabIndex="2"
                                                                                        Width="174px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Execution Date</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtExecutionDate" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="hdSubCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                                <td class="textbold">
                                                                                    Challan Date&nbsp;<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanDate" runat="server" CssClass="textbox" TabIndex="2" Width="170px"></asp:TextBox>
                                                                                    <img id="imgChallanDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                        tabindex="2" title="Date selector" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtChallanDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgChallanDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                                <td class="textbold displayNone" id="tdIssueChallanNo">
                                                                                    Issue Challan No <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtIssueChallanNo" runat="server" Width="168px" MaxLength="50" TabIndex="2"
                                                                                        CssClass="textbox displayNone"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Requested By<span class="Mandatory"></span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanRequestedBy" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox>
                                                                                    <img id="Img6" tabindex="2" runat="server" alt="Select & Add Requested By" onclick="PopupPageInventoryChallan(3)"
                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                <td class="textbold">
                                                                                    Requested Date&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="textbox" TabIndex="2"
                                                                                        Width="170px"></asp:TextBox>
                                                                                    <img id="imgRequestedDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                        tabindex="2" title="Date selector" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtRequestedDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgRequestedDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Approved By</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanApprovedBy" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox>
                                                                                    <img id="Img7" runat="server" alt="Select & Add Approved By" onclick="PopupPageInventoryChallan(4)"
                                                                                        src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                                <td class="textbold">
                                                                                    Approved Date</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="textbox" MaxLength="10"
                                                                                        Width="170px" TabIndex="2"></asp:TextBox>&nbsp;<img id="imgApprovedDate" alt="" src="../Images/calender.gif"
                                                                                            style="cursor: pointer" tabindex="2" title="Date selector" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtApprovedDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgApprovedDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td class="textbold">
                                                                                    Logged By
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanLoggedBy" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Received By <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChallanReceivedBy" runat="server" CssClass="textboxgrey" Width="170px"
                                                                                        TabIndex="20" MaxLength="50"></asp:TextBox>
                                                                                    <img id="Img5" runat="server" alt="Select & Add Received By" onclick="PopupPageInventoryChallan(5)"
                                                                                        tabindex="2" src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 129px">
                                                                                </td>
                                                                                <td class="textbold" colspan="4" style="height: 129px">
                                                                                    <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Literal ID="RadioButtonMarkup" runat="server"></asp:Literal>
                                                                                                    <input type="hidden" id="hdRadioButtonMarkup" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ORDER_NUMBER") + "|" + DataBinder.Eval(Container.DataItem, "APC") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="ORDER_NUMBER" HeaderText="Order Number" HeaderStyle-Width="18%" />
                                                                                            <asp:BoundField DataField="ORDER_TYPE_NAME" HeaderText="Order Type" HeaderStyle-Width="25%" />
                                                                                            <asp:BoundField DataField="ORDER_STATUS_NAME" HeaderText="Order Status" HeaderStyle-Width="15%" />
                                                                                            <asp:BoundField DataField="APC" HeaderText="PC" HeaderStyle-Width="15%" />
                                                                                            <asp:BoundField DataField="APPROVAL_DATE" HeaderText="Approval Date" HeaderStyle-Width="16%" />
                                                                                            <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received" HeaderStyle-Width="16%" />
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trInstalledPC" runat="server" style="display: none">
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold" colspan="4">
                                                                                    <asp:GridView ID="gvInstalledPC" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:HiddenField ID="hdRowID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ROWID") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ControlStyle CssClass="displayNone" />
                                                                                                <FooterStyle CssClass="displayNone" />
                                                                                                <HeaderStyle CssClass="displayNone" />
                                                                                                <ItemStyle CssClass="displayNone" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkCPUTYPECHECK" runat="server" Checked='<%#Eval("CPUTYPECHECK") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="CPUTYPE" HeaderText="CPU TYPE">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CPUNO" HeaderText="CPU NO">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkMONTYPECHECK" runat="server" Checked='<%#Eval("MONTYPECHECK") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="MONTYPE" HeaderText="MON TYPE">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MONNO" HeaderText="MONNO">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkKBDTYPECHECK" runat="server" Checked='<%#Eval("KBDTYPECHECK") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="KBDTYPE" HeaderText="KBD TYPE">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="KBDNO" HeaderText="KBD NO">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkMSETYPECHECK" runat="server" Checked='<%#Eval("MSETYPECHECK") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="MSETYPE" HeaderText="MSE TYPE">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MSENO" HeaderText="MSE NO">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CPUEGROUP" HeaderText="CPUEGROUP">
                                                                                                <HeaderStyle Width="10%" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="hdInstalledPCXML" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdProductList" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdChallanID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdQueryString" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdChallanReceivedBy" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdChallanRequestedBy" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdChallanApprovedBy" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdSelectEmployeeType" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdSNo" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                                    <input id="hdChallanGodownId" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPurchaseOrder" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdProductCount" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdQuantity" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdMandatoryOrder" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdAmadeusOrderNoRights" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdSerial" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdType" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdProductListPopUpPage" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdOrderQuantity" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdChallanLCodeTemp" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdOrderList" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdProductIDANDQuantity" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnChallanLCode" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnChallanID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' />
                                                                                    <input type="hidden" id="hdOrderTypeValue" style="width: 1px" runat='server' />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlProductList" runat="server" Width="100%" CssClass="displayNone">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Hardware Type
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlHardwareType" runat="server" TabIndex="2"
                                                                                        Width="315px" CssClass="textbold" AutoPostBack="True">
                                                                                        <asp:ListItem Value="" Selected="True">--ALL--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">1 A HW</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Misc HW</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Product <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlProduct" onchange="DisableQuantityInventoryChallan()"
                                                                                        runat="server" TabIndex="2" Width="315px" CssClass="textbold">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">
                                                                                    Quantity <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox" TabIndex="2" Width="75px"
                                                                                        MaxLength="4"></asp:TextBox>&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="btnAdd" runat="server" TabIndex="2" CssClass="button" Text="Add"
                                                                                        Width="100px" OnClientClick="return AddProductPage()" /></td>
                                                                            </tr>
                                                                            <tr class="gap">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td style="width: 14%">
                                                                                </td>
                                                                                <td style="width: 44%">
                                                                                </td>
                                                                                <td style="width: 10%">
                                                                                </td>
                                                                                <td style="width: 15%">
                                                                                </td>
                                                                                <td style="width: 15%">
                                                                                    <asp:Button ID="btnSelect" runat="server" TabIndex="2" CssClass="button" Text="Select"
                                                                                        Width="100px" OnClientClick="return AddSelectProductPageInventoryChallan()" /></td>
                                                                            </tr>
                                                                            <tr class="gap">
                                                                                <td style="width: 2%; height: 8pt;">
                                                                                </td>
                                                                                <td style="width: 14%; height: 8pt;">
                                                                                    <asp:Button ID="btnDelete" runat="server" TabIndex="2" CssClass="button" Text="Delete"
                                                                                        Width="100px" /></td>
                                                                                <td style="width: 44%; height: 8pt;">
                                                                                </td>
                                                                                <td style="width: 10%; height: 8pt;">
                                                                                </td>
                                                                                <td style="width: 15%; height: 8pt;">
                                                                                </td>
                                                                                <td style="width: 15%; height: 8pt;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="5">
                                                                                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAllProduct();" />
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <input type="checkbox" id="chkSelect" name="chkSelect" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField HeaderText="SNo" DataField="LineNumber" />
                                                                                            <asp:BoundField HeaderText="Product" DataField="ProductName">
                                                                                                <HeaderStyle Width="30%" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtQty" Width="40"  Text='<% #DataBinder.Eval(Container.DataItem, "Qty") %>'
                                                                                                        runat="server" CssClass="textbox" MaxLength="3"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Amadeus Serial">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtAmadeusSerial" Text='<% #DataBinder.Eval(Container.DataItem, "SerialNumber") %>'
                                                                                                        runat="server" CssClass="textbox"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Vender Serial">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtVenderSerial" Text='<% #DataBinder.Eval(Container.DataItem, "VenderSerialNo") %>'
                                                                                                        runat="server" CssClass="textbox"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Action">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
                                                                                                        CommandArgument='<% #DataBinder.Eval(Container.DataItem, "LineNumber") %>' CssClass="LinkButtons"></asp:LinkButton>
                                                                                                    <asp:HiddenField ID="hdProductID" runat="server" Value='<%#Eval("ProductID")%>' />
                                                                                                    <asp:HiddenField ID="hdManintainBy" runat="server" Value='<%#Eval("MAINTAIN_BALANCE_BY")%>' />
                                                                                                    <asp:HiddenField ID="hdManintain" runat="server" Value='<%#Eval("MAINTAIN_BALANCE")%>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue center" />
                                                                                        <RowStyle CssClass="textbold center" />
                                                                                        <HeaderStyle CssClass="Gridheading center" />
                                                                                    </asp:GridView>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlNotes" runat="server" Width="100%" CssClass="displayNone">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold" colspan="4">
                                                                                    <asp:TextBox ID="txtNote" runat="server" CssClass="textbox" Height="230px" Rows="5"
                                                                                        TextMode="MultiLine" Width="622px" TabIndex="2"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="gap" colspan="4">
                                                                                    <input style="width: 1px" id="hdSol" type="hidden" runat="server" value="1" />
                                                                                    <input style="width: 1px" id="hdChallanDetails" type="hidden" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    &nbsp;
                                                                </td>
                                                                <td class="center top " colspan="2" rowspan="1" style="height: 2124px">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Save" Width="100px" OnClientClick="return ManageChallanPage()" AccessKey="s" /><br />
                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New"
                                                                        Width="100px" AccessKey="n" /><br />
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                    <asp:Button ID="btnExecute" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Execute" Width="100px" OnClientClick="return challanExecuteButtonPage()"
                                                                        AccessKey="e" /><br />
                                                                    <asp:Button ID="btnPrintLabel" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Print Label" Width="100px" /><br />
                                                                    <asp:Button ID="btnPrint" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Print" Width="100px" AccessKey="p" /><br />
                                                                    <asp:Button ID="btnModifyChallan" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Update Rec.Challan" Font-Size="XX-Small" Width="100px" AccessKey="m" /><br />
                                                                    <input id="hdPrintLabel" runat="server" style="width: 1px" type="hidden" value="0" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Literal ID="ltrPrint" runat="server"></asp:Literal></td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ErrorMsg" style="width: 10%">
                                                                    Field Marked * are Mandatory</td>
                                                                <td>
                                                                </td>
                                                                <td>
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
                    <asp:HiddenField ID="hdTabType" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
