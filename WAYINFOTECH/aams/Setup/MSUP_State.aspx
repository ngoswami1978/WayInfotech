<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_State.aspx.vb" Inherits="Setup_MSUP_State" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>AAMS: State</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    /*********************************************************************
                        Method for Reset
    *********************************************************************/
    function StateReset()
    {
        document.getElementById("lblError").innerHTML="";
        document.getElementById("txtStateName").value="";       
        document.getElementById("drpCountry").selectedIndex=0;
        return false;
    }
     /*********************************************************************
                        Method for check Mandatory Field
    *********************************************************************/
    function StateMandatory()
    {
        if(document.getElementById("txtStateName").value=="")
        {
            document.getElementById("lblError").innerHTML="Please enter state name.";
            document.getElementById("txtStateName").focus();
            return false;
        }
        if(IsDataValid(document.getElementById("txtStateName").value,2)==false)
        {
            document.getElementById("lblError").innerHTML="Please enter valid state name.";
            document.getElementById("txtStateName").focus();
            return false;
        }       
        if(document.getElementById("drpCountry").selectedIndex==0)
        {
            document.getElementById("lblError").innerHTML="Please select country.";
            document.getElementById("drpCountry").focus();
            return false;
        }            
        return true;
    }
     function NewFunction()
    {   
        window.location.href="MSUP_State.aspx?Action=I";       
        return false;
    }
    </script>
</head>
<body >
    <form id="frmManageState" runat="server">
    <div>
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                <table width="100%" align="left" >
                          <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Setup-></span><span class="sub_menu">State</span>
                            </td>
                        </tr>                      
                                   
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage State
                            </td>
                        </tr>
                       
                        
                        <tr>
                            <td  >
                                <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                                   
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                              
                                                <tr>
                                                    
                                                    <td class="textbold" style="height: 28px;width:100%" colspan="4" valign="top" >
                                                          <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <%--<tr>
                                                                <td width="20px"  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>--%>
                                                              <tr>
                                                                  <td align="center" class="textbold" colspan="6" height="20px" valign="top">
                                                                      <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                              </tr>
                                                            <tr>
                                                                <td width="10%" class="textbold" style="height: 25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" width="10%">
                                                                    </td>
                                                                <td width="20%" align=left >
                                                                    State Name<span class="Mandatory">* </span> </td>
                                                                <td width="20%">
                                                                    <span class="textbold">
                                                                    <asp:TextBox ID="txtStateName" runat="server" MaxLength="30" CssClass="textfield" TabIndex="1" Width="177px"></asp:TextBox>
                                                                </td>
                                                                <td width="15%" >
                                                                    </td>
                                                                <td width="25%" >
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="3" AccessKey="S" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    </td>
                                                                <td align=left>
                                                                    Country<span class="Mandatory">* </span></td>
                                                                <td class="textbold">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCountry" runat="server"  CssClass="dropdown" TabIndex="2" Width="182px">
                                                                    </asp:DropDownList></td>
                                                                <td >
                                                                </td>
                                                                <td >
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="4"  AccessKey="N"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" >
                                                                </td>
                                                                <td align=left>
                                                                    </td>
                                                                <td>
                                                                    </td>
                                                                <td >
                                                                    &nbsp;</td>
                                                                <td >
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                               <td colspan="2" class="ErrorMsg">
                                                                    </td>
                                                                <td >
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                    <td></td>
                                                                <td colspan="2" class="ErrorMsg" >
                                                                   Field Marked * are Mandatory</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 4px">
                                                                    </td>
                                                            </tr>
                                                            <tr height="20px">
                                                                <td colspan="6" height="4">
                                                                     </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                           
                                                     </td>
                                                    <td width="18%" rowspan="1" valign="top" >
                                                        </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td class="textbold" colspan="5">                                                        
                                                          
                                                        </td>
                                                </tr>
                                                <tr style="font-size: 12pt; font-family: Times New Roman">
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
            <tr>
                <td  valign="top" style="height: 23px">
                
                  
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
