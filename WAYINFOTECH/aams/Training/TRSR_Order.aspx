<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_Order.aspx.vb" Inherits="Training_TRSR_Order" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>AAMS: Training Order</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script type="text/javascript" language="javascript" >
  function validateDateSent()
  {

  var ApprovedDatea=document.getElementById("txtEMailSentFrom").value;
  if(ApprovedDatea != "")
{
    if (isDate(ApprovedDatea.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Please enter correct date";
    document.getElementById("txtEMailSentFrom").focus();
    return false;
    }
}
  
    
  
  document.getElementById("lblError").innerText="";
  var ApprovedDate=document.getElementById("txtEMailSentTo").value;
  if(ApprovedDate != "")
{
    if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Please enter correct date";
    document.getElementById("txtEMailSentTo").focus();
    return false;
    }
}
    
  
  document.getElementById("lblError").innerText="";
  var ApprovedDate=document.getElementById("txtofficeIDOnlineFrom").value;
  if(ApprovedDate != "")
{
    if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Please enter correct date";
    document.getElementById("txtofficeIDOnlineFrom").focus();
    return false;
    }
}


    document.getElementById("lblError").innerText="";
    var ApprovedDate=document.getElementById("txtofficeIDOnlineTo").value;
    if(ApprovedDate != "")
    {
        if (isDate(ApprovedDate.trim(),"dd/MM/yyyy")==false)
            {
                document.getElementById("lblError").innerText="Please enter correct date";
                document.getElementById("txtofficeIDOnlineTo").focus();
                return false;
            }
    }

//------------------------------------------------------------------------------------------------------------//
  document.getElementById("lblError").innerText="";
  var lentxtEMailSentFrom=document.getElementById("txtEMailSentFrom").value;
  if (lentxtEMailSentFrom!="")
    {
          var lenofficeIDOnlineFrom=document.getElementById("txtofficeIDOnlineFrom").value;
          if (lenofficeIDOnlineFrom!="") 
            {
                document.getElementById("lblError").innerText="Please select only one date criteria";
                return false;
            }
  
    }
  
    document.getElementById("lblError").innerText="";
    var lentxtEMailSentTo=document.getElementById("txtEMailSentTo").value;
    if (lentxtEMailSentTo!="")
    {
            var lentxtofficeIDOnlineTo=document.getElementById("txtofficeIDOnlineTo").value;
            if (lentxtofficeIDOnlineTo!="") 
            {
                document.getElementById("lblError").innerText="Please select only one criteria";
                return false;
            }
 
    }
 
  //-------------------------------------------------------------------------------------------------------//
    document.getElementById("lblError").innerText="";
    var lentxtEMailSentFrom=document.getElementById("txtEMailSentFrom").value;
    if (lentxtEMailSentFrom!="")
    {
            var lentxtofficeIDOnlineTo=document.getElementById("txtofficeIDOnlineTo").value;
            if (lentxtofficeIDOnlineTo!="") 
            {
                document.getElementById("lblError").innerText="Please select only one date criteria";
                return false;
            }
  
    }
  
  
    document.getElementById("lblError").innerText="";
    var lentxtEMailSentTo=document.getElementById("txtEMailSentTo").value;
    if (lentxtEMailSentTo!="")
    {
            var lentxtofficeIDOnlineFrom=document.getElementById("txtofficeIDOnlineFrom").value;
            if (lentxtofficeIDOnlineFrom!="") 
            {
                document.getElementById("lblError").innerText="Please select only one date criteria";
                return false;
            }
  
     }
  
  }//function end
  


     function OrderWholeGroup()
 {

try
    {
            if(document.getElementById("txtAgencyName").value=='')
            {
                document.getElementById("chbWholeGroup").checked=false;
                document.getElementById("chbWholeGroup").disabled =true;
            }
            else
            {
                 if(document.getElementById("hdtxtAgencyName").value.trim()==document.getElementById("txtAgencyName").value.trim())
                {
                document.getElementById("chbWholeGroup").disabled =false;
                }
                else
                {
                
                document.getElementById("hdAgencyNameId").value='';
                document.getElementById("hdtxtAgencyName").value='';
                document.getElementById("chbWholeGroup").checked=false;
                document.getElementById("chbWholeGroup").disabled =true;
                }
            
            }
            
        }
      catch(err){}
      
	    
 } //method end
    </script>
<body>

   <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch" >
        <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Training -&gt;<span class="sub_menu">&nbsp; Search Order</span></span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order
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
                                                    <td width="6%" height="25px">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                        Agency Name
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                        </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" Width="93%" TabIndex="1" ></asp:TextBox> <img id="Img2" runat="server"  src="../Images/lookup.gif" alt="Select & Add Agency Name" onclick="PopupPageCourseSession(1)" style="cursor:pointer;"  /></td>
                                                    <td style="width: 176px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="24" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td  width="6%" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" width="18%" style="height: 25px">
                                                        Whole Group</td>
                                                    <td width="20%" style="height: 25px">
                                                        <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="textbold" TabIndex="2"   /></td>
                                                    <td width="18%" class="textbold" style="height: 25px">
                                                        Country</td>
                                                    <td width="20%" style="height: 25px">
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCountry" runat="server" CssClass="dropdownlist" Width="139px" TabIndex="3">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 25px; width: 176px;">
                                                        <asp:Button ID="btnExport" runat="server" AccessKey="E" CssClass="button" TabIndex="25"
                                                            Text="Export" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        City</td>
                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCity" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="4">
                                                    </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        Region</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlRegion" runat="server" CssClass="dropdownlist" Width="139px" TabIndex="5">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 176px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="26" AccessKey="R" /></td>
                                                </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      AOffice</td>
                                                                  <td style="height: 25px">
                                                                      <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAoffice" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="6">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="height: 25px">
                                                        Order Number</td>
                                                                  <td style="height: 25px">
                                                        <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textbox" MaxLength="20" Wrap="False" TabIndex="7" Width="133px"></asp:TextBox></td>
                                                                  <td style="width: 176px; height: 25px;">
                                                                  </td>
                                                              </tr>
                                                <tr>
                                                    <td style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px">
                                                        Order Type</td>
                                                    <td colspan="3" style="height: 25px">
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOrderType" runat="server" CssClass="dropdownlist" Width="94%" TabIndex="8">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 176px; height: 25px;">
                                                        </td>
                                                </tr>
                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 288px;">Order Status</td>
                                                                    <td style="height: 25px; width: 257px;"> <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOrderStatus" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="9"></asp:DropDownList> </td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        Training Status</td>
                                                                    <td style="height: 25px">
                                                                        <asp:DropDownList ID="ddlTrainingStatus" onkeyup="gotop(this.id)" runat="server" CssClass="dropdownlist" Width="138px" TabIndex="10"/>                                                                    </td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      E-Mail Sent From</td>
                                                                  <td style="width: 257px; height: 25px">
                                                                        <asp:TextBox ID="txtEMailSentFrom" runat="server" CssClass="textbox" MaxLength="10" TabIndex="11"></asp:TextBox>
                                                                        <img id="imgEmailFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtEMailSentFrom.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "imgEmailFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                                                                    </script>
                                                                        </td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      E-Mail Sent To</td>
                                                                  <td style="height: 25px">
                                                                        <asp:TextBox ID="txtEMailSentTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="12"></asp:TextBox>
                                                                        <img id="imgEmailTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtEMailSentTo.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "imgEmailTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                          </script>
                                                                        </td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      OfficeID OnLine&nbsp; From</td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      <asp:TextBox ID="txtofficeIDOnlineFrom" runat="server" CssClass="textbox" MaxLength="10" TabIndex="21"></asp:TextBox>
                                                                      <img id="ImgOfficeIdOnlineFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                      <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtofficeIDOnlineFrom.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "ImgOfficeIdOnlineFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                 </script>
                                                                       </td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      OfficeID OnLine To</td>
                                                                  <td style="height: 25px">
                                                                      <asp:TextBox ID="txtofficeIDOnlineTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="22"></asp:TextBox>
                                                                      <img id="ImgOfficeIdOnlineTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                      <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtofficeIDOnlineTo.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "ImgOfficeIdOnlineTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                 </script>
                                                                    
                                                                      
                                                                      </td>
                                                              </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      Email Status</td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      <asp:DropDownList onkeyup="gotop(this.id)" ID="drpEmailStatus" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="23">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>



                                                <tr>
                                                    <td >
                                                        &nbsp;<input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    
                                                    
                                                    <td style="width: 176px" >
                                                                    <asp:CheckBox ID="chkLetterSentStatus" runat="server" CssClass="textbold" Text="Email Status" Width="120px" TabIndex="23" Visible="False"   /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch" style="display:none"  runat="server" Width="100%">
                                                            &nbsp;</asp:Panel>
                                                            <asp:HiddenField ID="hdOrderID" runat="server" />
                                                            <asp:HiddenField ID="hdAdvanceSearch" runat="server" Value="" />
                                                            <asp:HiddenField ID="hdtxtAgencyName" runat="server" />
                                                            
                                                            
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
                </td>
                <td></td>
            </tr>
        </table>
        <script>
        document.getElementById("chbWholeGroup").disabled=true;
        </script>
    </form>


</body>
</html>
