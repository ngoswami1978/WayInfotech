<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_ParticipantBasket.aspx.vb" Inherits="Training_TRUP_ParticipantBasket" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Participant Basket</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script language="javascript" type="text/javascript">

      
 function PopupPage(id)
         {
         var type;
         
         if (id=="1")
         {
                type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaParticipantBasketAgencySearch","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
         }
    
         if (id=="2")
         {
                var strAgencyName=document.getElementById("txtAgencyName").value;
                strAgencyName=strAgencyName.replace("&","%26")
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName;
               window.open(type,"aaParticipantBasketAgencyStaffSearch","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
          }
               
     }
    
  
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="txtDate">
    <div>
      <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Participant Basket</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                               Manage Participant Basket</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 1%" rowspan="16">
                                            </td>
                                            <td class="subheading" colspan="4">
                                                Agency Details</td>
                                            <td class="center" style="width: 12%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold"> Agency Name<span class="Mandatory" >*</span></td>                                                                               
                                            <td colspan="3">
                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="2" Width="534px" ReadOnly="True"></asp:TextBox>
                                            <img id="Img2" TabIndex="2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"  src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td style="width: 12%;" class="center top" rowspan="3">
                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" OnClientClick="return ValidateParticipantBasketPage()" AccessKey="s" /><br />
                                                <asp:Button ID="btnNew" CssClass="button topMargin" runat="server" Text="New" TabIndex="3" AccessKey="n" />
                                                <br />
                                                <asp:Button ID="btnReset" CssClass="button topMargin" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width:15%">
                                                Address</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="50px"
                                                    ReadOnly="True" Rows="5" TabIndex="20" TextMode="MultiLine" Width="534px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Country</td>
                                            <td style="width: 27%">
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                City</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="20"
                                                    Width="170px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Phone</td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                Fax</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="20"
                                                    Width="170px"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Online Status</td>
                                            <td>
                                                <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 15%">
                                                Priority</td>
                                            <td style="width: 26%">
                                                <asp:TextBox ID="txtPriority" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                            <td class="center" style="width: 13%">
                                                <input id="hdAgencyNameParticipantBasket" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdAgencyStaffNameParticipantBasket" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageBasketID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdStatus" runat="server" style="width: 1px" type="hidden" />
                                                
                                                </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" style="width: 15%">
                                                Agency Staff<span class="Mandatory" >*</span></td>
                                            <td>
                                                <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                    TabIndex="20" Width="170px"></asp:TextBox>
                                                <img id="Img1" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"  src="../Images/lookup.gif" TabIndex="2" style="cursor:pointer;" /></td>
                                            <td class="textbold" style="width: 15%">
                                            </td>
                                            <td style="width: 26%">
                                            </td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold" colspan="4">
                                            </td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="subheading" colspan="4">
                                                Basket</td>
                                            <td class="center" style="width: 13%">
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" >
                                                                                Request ID</td>
                                                                            <td>             
                                                                                <asp:TextBox ID="txtRequestID" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td class="textbold" style="width:15%">     
                                                                                Logged By</td>
                                                                            <td style="width:26%">
                                                                                <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="2" Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                                            <td style="width:13%" class="center"> </td>
                                                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                                Date </td>
                                            <td>
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="textboxgrey" ReadOnly="true" Width="170px" TabIndex="2"></asp:TextBox>&nbsp;
                                               
                                                
                                                </td>
                                            <td class="textbold">
                                                Preferred Date <span class="Mandatory" ></span></td>
                                            <td>
                                                <asp:TextBox ID="txtPreferredDate" runat="server" CssClass="textbox" Width="170px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgPreferredDate" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" runat="server" />
                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPreferredDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgPreferredDate",
                                                                                                    //align          :    "Tl",
                                                                                                     singleClick    :    true
                                                                                                    
                                                                                                                                                                                                      });
                                                                                                  </script>
                                                
                                                </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                                Aoffice<span class="Mandatory" >*</span></td>
                                            <td>
                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="visibility: hidden">
                                                Status<span class="Mandatory" >*</span>
                                                </td>
                                            <td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist  displayNone" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                            </asp:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                                Course<span class="Mandatory" >*</span></td>
                                            <td colspan="3"><asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="537px" TabIndex="2" onkeyup="gotop(this.id)">
                                            </asp:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="textbold">  Remarks</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Height="50px"
                                                    Rows="5" TabIndex="2" TextMode="MultiLine" Width="532px"></asp:TextBox></td>
                                            <td>    </td>
                                        </tr>
                                        <tr>
                                            <td class="textbold">
                                            </td>
                                            <td colspan="3">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ErrorMsg" colspan="4">
                                                Field Marked * are Mandatory</td>
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
    </div>
    </form>
</body>
</html>
