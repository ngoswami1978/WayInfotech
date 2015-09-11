<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCRPT_IncentivePaid.aspx.vb" Inherits="Incentive_INCRPT_IncentivePaid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Incentive Paid</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    
    
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server"  defaultfocus ="txtChainCode" >
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Incentive-></span><span class="sub_menu">Incentive Paid Report</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                &nbsp;Incentive Paid Report</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                              
                                                            
                                                             <tr>
                                                                <td width="6%" class="textbold">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Chain Code<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana;">*</span></strong></td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textfield" TabIndex="1" MaxLength="6" EnableViewState="False"></asp:TextBox><img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupForIncentive();" id="ImgAGroup" style="cursor:pointer;"  alt="" runat ="server"  /></td>
                                                                <td width="12%" class="textbold" >
                                                                    </td>
                                                                <td width="21%"></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="7"  AccessKey="A"/></td>
                                                            </tr>  
                                                                                                                      
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                        
                                                            
                                                             <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold" style="height: 22px">
                                                                    Group Name</td>
                                                                <td width="63%" colspan ="3" style="height: 22px" >
                                                                     <asp:TextBox ID="txtGroupName" runat="server" CssClass="textfield" TabIndex="2" MaxLength="40"  Width="487px" EnableViewState="False"></asp:TextBox></td>
                                                              
                                                                <td width="18%" style="height: 22px">
                                                                   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="10" AccessKey="R" /></td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    From Month</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="drpMonthsFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="138px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    From Year</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="drpYearsFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="138px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 22px">
                                                                   </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    To Month</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="drpMonthsTo" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="138px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    To Year&nbsp;</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList ID="drpYearsTo" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="138px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 22px">
                                                                   </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td colspan="2" style="height: 22px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                            </tr>
                                                           
                                                               <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                   
                                                        </td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" style="height: 44px" >
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                        &nbsp;
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
    <script language="javascript" type="text/javascript">
       function ValidateForm()
    {
      document.getElementById('lblError').innerText=''
      
      // Validatin Chain Code And Group Name
      
      /*
       if(document.getElementById('<%=txtChainCode.ClientId%>').value =='' &&  document.getElementById('<%=txtGroupName.ClientId%>').value =='')
        {         
            document.getElementById('<%=lblError.ClientId%>').innerText ='Please enter chain code or group name.'
            document.getElementById('<%=txtGroupName.ClientId%>').focus()
            return false;          
        }
      
      */
      
      if(document.getElementById('<%=txtChainCode.ClientId%>').value =='' )
        {         
            document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code is mandatory.'
            document.getElementById('<%=txtChainCode.ClientId%>').focus()
            return false;          
        }
      
      
      // validating Chain Code
      
       if(document.getElementById('<%=txtChainCode.ClientId%>').value !='')
        {         
           var strValue = document.getElementById('<%=txtChainCode.ClientId%>').value
           reg = new RegExp("^[0-9]+$"); 
           if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should contain only digits.'
                document.getElementById('<%=txtChainCode.ClientId%>').focus()
                return false;
           }
           else
           {
                if(document.getElementById('<%=txtChainCode.ClientId%>').value =='0') 
                {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Chain code should be greater than zero.'
                    document.getElementById('<%=txtChainCode.ClientId%>').focus()
                    return false;
               }             
           
           }
        }
      
     
      
       return true; 
        
    }
   
   
  function PopupAgencyGroupForIncentive()
{
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"IncSRptPaid","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}       
    </script>
</body>
</html>
