<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDUP_BDRLetter.aspx.vb" Inherits="BOHelpDesk_HDUP_BDRLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
      <title>BDR Letter</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"Agency","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
        }
     function  NewHDUPBDRLetter()
       {    
           window.location="BOHDUP_BDRLetter.aspx?Action=I";
           return false;
       }  
    function  BDRLetterReset()
    {       
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtAgencyName").value="";  
        document.getElementById("drpReqType").value=""; 
        document.getElementById("txtBDrId").value=""; 
        document.getElementById("txtBDRTicket").value=""; 
        document.getElementById("txtLtrNo").value=""; 
        document.getElementById("drpBdrSentBy").value=""; 
        document.getElementById("drpAirLine").value=""; 
        document.getElementById("drp1Aoffice").value=""; 
        document.getElementById("txtAirLineoffice").value=""; 
        document.getElementById("txtBDRLoggedDateFrom").value=""; 
        document.getElementById("txtBDRLoggedDateTo").value=""; 
        document.getElementById("chkWholeGroup").checked=false;
        return false;
    }
     function BDRLetterMandatory()
    {
//        if (  document.getElementById("txtTypeToPrint").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtTypeToPrint").value,2)==false)
//            {
//            document.getElementById("lblError").innerHTML="Type To Print is not valid.";
//            document.getElementById("txtTypeToPrint").focus();
//            return false;
//            } 
//         }   
         return true;
     }
  
    </script>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <!-- import the calendar script -->
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
</head>
<body>
    <form id="form1" runat="server"  defaultfocus ="txtAgencyName">
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Back Office Help Desk-></span><span class="sub_menu">BDR Letter</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage BDR Letter</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder" >                                 
                                                        <table width="100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="8" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="subheading" colspan="5">
                                                                        Agency Details</td>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="8" align="center" style="height:5px;" valign="TOP"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td  style="width:18%;" class="textbold" ><span class="textbold">Agency</span></td>
                                                                <td colspan="4" style="width:50%;" class="textbold"><asp:TextBox ID="txtAgencyName" runat ="server" CssClass ="textboxgrey" Width="437px" MaxLength="30" TabIndex="1" ReadOnly="True" ></asp:TextBox></td>                                                                
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="18" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td  style="width:18%;" class="textbold" ><span class="textbold">Address</span></td>
                                                                <td colspan="4" style="width:50%;" class="textbold"><asp:TextBox ID="txtAddress" runat ="server" CssClass ="textboxgrey" Width="437px" MaxLength="30" TabIndex="1" Height="58px" TextMode="MultiLine" ReadOnly="True" ></asp:TextBox></td>                                                                
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ><asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="18" /></td>
                                                            </tr>
                                                              <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">City&nbsp;</span></td>
                                                                <td style="width:17%;" class="textbold" >
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="143px" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold">Country</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtCountry" runat ="server" CssClass ="textboxgrey"  MaxLength="30" TabIndex="4" Width="143px" ReadOnly="True" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="20" /></td>                                                              
                                                            </tr>
                                                               <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Phone&nbsp;</span></td>
                                                                <td style="width:17%;" class="textbold" >
                                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="143px" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold">Fax</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtFax" runat ="server" CssClass ="textboxgrey"  MaxLength="30" TabIndex="4" Width="143px" ReadOnly="True" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                                   <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Priority&nbsp;</span></td>
                                                                <td style="width:17%;" class="textbold" >
                                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="143px" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold">Online Status</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtOnlineStatus" runat ="server" CssClass ="textboxgrey"  MaxLength="30" TabIndex="4" Width="143px" ReadOnly="True" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                                   <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Office Id&nbsp;</span></td>
                                                                <td style="width:17%;" class="textbold" >
                                                                    <asp:ListBox ID="lstOfficeId" runat="server" Width="148px"></asp:ListBox></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold"></span></td> 
                                                                <td style="width:17%;" class="textbold" ></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                               <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                             <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="subheading" colspan="5">
                                                                        BDR Details</td>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                              <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%; height: 19px;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%; height: 19px;" class="textbold" ><span class ="textbold">BDR Letter Id</span></td>
                                                                <td style="width:17%; height: 19px;" class="textbold" >
                                                                    <asp:TextBox ID="txtBDrId" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="143px" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width:1%; height: 19px;" class="textbold"></td>
                                                                <td style="width:15%; height: 19px;"><span class="textbold">LTR No</span></td> 
                                                                <td style="width:17%; height: 19px;" class="textbold" ><asp:TextBox id="txtLtrNo" tabIndex=6 runat="server" CssClass="textboxgrey" MaxLength="30" Width="143px" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width:8%; height: 19px;" class="textbold" ></td>
                                                                <td style="width:22%; height: 19px;" ></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class="textbold">BDR Sent by </span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList id="drpBdrSentBy" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="7">
                                                                </asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;" ><span class="textbold">BDR Ticket</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtBDRTicket" runat ="server" CssClass ="textboxgrey"  MaxLength="30" TabIndex="5" Width="143px" ReadOnly="True" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class="textbold">BDR Send Date &nbsp;</span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtBDRLoggedDateFrom" runat="server" MaxLength="10" TabIndex="13" CssClass="textboxgrey" Width="118px" ReadOnly="True"></asp:TextBox>
                                                                         <img id="Img1" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" /></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;" ><span class="textbold">Airlines Office</span></td> 
                                                                <td style="width:17%;" class="textbold" ><asp:TextBox ID="txtAirLineoffice" runat ="server" CssClass ="textboxgrey"  MaxLength="30" TabIndex="10" Width="143px" ReadOnly="True" ></asp:TextBox></td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%; " class="textbold" ><span class="textbold">Airline Office Address&nbsp;</span></td>
                                                                <td colspan="4" style="width:50%;" class="textbold" >
                                                                    <asp:TextBox ID="txtAirLineOfficeAdd" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="437px" ReadOnly="True"></asp:TextBox></td>
                                                              
                                                                <td style="width:8%; " class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>                                                          
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                               <td style="width:18%; " class="textbold" ><span  class="textbold" >BDR Letter </span></td>
                                                                <td colspan="4" style="width:50%;" class="textbold" >
                                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="4"
                                                                        Width="437px" ReadOnly="True"></asp:TextBox></td>
                                                              
                                                                <td style="width:8%; " class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                                 <tr>
                                                                <td style="height:2px;" class="textbold" colspan="8" align="center" valign="TOP" ></td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ><span class ="textbold">Authorized Signature</span></td>
                                                                <td style="width:17%;" class="textbold" ><asp:DropDownList id="drpAuthSig" runat="server" CssClass="dropdownlist" Width="150px" TabIndex="7">
                                                                </asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:15%;"><span class="textbold"></span></td> 
                                                                <td style="width:17%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height: 15px" class="textbold" colspan="8" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:6%; " class="textbold" >&nbsp;</td>
                                                                <td style="width:18%;" class="textbold" ></td>
                                                                <td style="width:33%;" colspan="3" class="ErrorMsg" >Field Marked * are Mandatory</td>
                                                                <td style="width:17%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:8%;" class="textbold" ></td>
                                                                <td style="width:22%;" ></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="8" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    
                                                                  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                            </td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
        </table>
    <!-- Code by Dev Abhishek -->
    </form>
</body>
</html>
