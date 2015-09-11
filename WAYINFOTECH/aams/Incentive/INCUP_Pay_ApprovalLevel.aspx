<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_Pay_ApprovalLevel.aspx.vb" Inherits="Incentive_INCUP_Pay_ApprovalLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Manage Approval Level</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
</head>
<body onload="HideShowLevel('txtNoOfLevel')">
    <form id="form1" runat="server" defaultbutton="btnSave">
    
         <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Incentive-&gt;</span><span class="sub_menu">Manage Payment &nbsp;Approval Level</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Payment Approval Level</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 100px">
                                            </td>
                                            <td class="gap" colspan="3" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td class="gap" colspan="1" style="width: 162px; text-align: center">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 147px">
                                                                                Aoffice<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 214px">
                                                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    TabIndex="1" Width="198px">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 162px">
                                                                            </td>
                                                                            <td rowspan="2">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" AccessKey="s" /><br /><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="n" /><br /><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="r" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 147px">
                                                                                No. Of
                                                                                Level<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 214px">
                                                                            
                                                                            <asp:TextBox ID="txtNoOfLevel" runat="server" CssClass="textbox" MaxLength="1" TabIndex="1"
                                                                                        Width="190px" onblur="HideShowLevel(this.id)" onkeyup="checknumeric(this.id)" ></asp:TextBox>
                                                                    </td>
                                                                            <td style="width: 162px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trLevel1"  class ="displayNone" >
                                                                            <td style="width: 100px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px; width: 147px;">
                                                                                 Level1<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 214px; height: 26px;">
                                                                            <asp:TextBox ID="txtLevel1" runat="server" CssClass="textboxgrey"  TabIndex="1"
                                                                                        Width="192px" ReadOnly="True" ></asp:TextBox>
                                                                                <img id="Img1" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel1')"
                                                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved1" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                                                            </td>
                                                                            <td style="height: 26px">
                                                                    </td>
                                                                        </tr>
                                        <tr id="trLevel2"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;"> Level2<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel2" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img2" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel2')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved2" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel3"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px"> 
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;">Level3<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel3" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img3" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel3')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved3" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel4"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px"> 
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;">Level4<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel4" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img4" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel4')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved4" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel5"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;"> Level5<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel5" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img5" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel5')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved5" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel6"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px"> 
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;">Level6<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel6" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img6" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel6')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved6" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel7"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px"> 
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;">Level7<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel7" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img7" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel7')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved7" Text="Finally Approved"  runat="server" CssClass="textbox" />
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel8"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px"> 
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;">Level8<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel8" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img8" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel8')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved8" Text="Finally Approved"  runat="server" CssClass="textbox" /></td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr id="trLevel9"  class ="displayNone" >
                                            <td style="width: 100px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px; width: 147px;"> Level9<span class="Mandatory" >*</span>
                                            </td>
                                            <td style="width: 214px; height: 26px">
                                            <asp:TextBox ID="txtLevel9" runat="server" CssClass="textboxgrey"  TabIndex="1" Width="192px" ReadOnly="True" ></asp:TextBox>
                                                <img id="Img9" runat="server" alt="Select & Add Employee" onclick="PopupPageApprovalLevel(1,'txtLevel9')"
                                                    src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                            <td style="width: 162px; height: 26px"><asp:CheckBox ID="ChkFinApproved9" Text="Finally Approved"  runat="server" CssClass="textbox" /></td>
                                            <td style="height: 26px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 26px">
                                            </td>
                                            <td colspan="2" style="height: 26px" class="ErrorMsg">
                                                &nbsp;Field Marked * are Mandatory</td>
                                            <td class="ErrorMsg" colspan="1" style="width: 162px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 66px">
                                              </td>
                                            <td class="ErrorMsg" colspan="2" style="height: 66px">
                                                <input id="hdEmployeePageName" runat="server" style="width: 1px" type="hidden" />
                                                
                                                <input style="WIDTH: 1px" id="hdLevel1" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel2" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel3" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel4" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel5" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel6" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel7" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel8" type="hidden" runat="server" />
                                                <input style="WIDTH: 1px" id="hdLevel9" type="hidden" runat="server" />
                                                 <input style="WIDTH: 1px" id="hdCtrlId" type="hidden" runat="server" />
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                                
                                                </td>
                                            <td class="ErrorMsg" colspan="1" style="width: 162px; height: 66px;">
                                            </td>
                                            <td style="height: 66px" >
                                            </td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" style="text-align: center">
                                            &nbsp;</td>
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
 
      document.getElementById('<%=lblError.ClientId%>').innerText=''
      
        var cboGroup=document.getElementById('ddlAOffice');
   if(cboGroup.selectedIndex ==0)
        {
         document.getElementById('lblError').innerText ='Aoffice is mandatory.'
         cboGroup.focus();
         return false;
            
        }
      
      
     if(document.getElementById('<%=txtNoOfLevel.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='No. of level is mandatory.'
            document.getElementById('<%=txtNoOfLevel.ClientId%>').focus();
            return false;
        }
        else
        {
               var strValue = document.getElementById('<%=txtNoOfLevel.ClientId%>').value
               reg = new RegExp("^[0-9]+$"); 
               if(reg.test(strValue) == false) 
                {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='No. of level should contain only digits.'
                    document.getElementById('<%=txtNoOfLevel.ClientId%>').focus()
                    return false;
               }
               else
               {
               
                   if(document.getElementById('<%=txtNoOfLevel.ClientId%>').value =='0') 
                    {
                        document.getElementById('<%=lblError.ClientId%>').innerText ='No. of level should be greater than zero.'
                        document.getElementById('<%=txtNoOfLevel.ClientId%>').focus()
                        return false;
                   }             
               
               }
               
               // Code For Validating Level
               
               var days=document.getElementById("txtNoOfLevel").value;
                if (days=='')
                {
                days=0
                }
                if (days==0)
                {
                document.getElementById(id).value='';
                //document.getElementById(id).focus();
                }
                var CeilDays=Math.ceil(days);
                if (CeilDays>9)
                {
                document.getElementById("lblError").innerText="No of Level Exceeds Maximum No. Of Level";
                document.getElementById(id).value='';
                document.getElementById(id).focus();
                return false;
                }
                    for (i=1;i<=9;i++)
                    {
                        if (i<=CeilDays)
                        {
                            if (document.getElementById("txtLevel" + i).value == '')
                            {
                                document.getElementById("lblError").innerText="Level" + i +" is mandatory.";
                                document.getElementById("txtLevel" + i).focus();
                                return false;
                            }   
                        }
                        
                    }
                    try
                    {
                    document.getElementById("trLevel1").focus();
                    }
                    catch(err)
                    {
                    
                    }
               
               
               // Code End
    
        }

       return true; 
        
    }
    
    function PopupPageApprovalLevel(id,ctrlid)
        {
        
             if (id=="1")
             {
                
                var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                document.getElementById("hdCtrlId").value = ctrlid
                if (strEmployeePageName!="")
                {
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T&ctrlId="+ctrlid;
                 
                   //type = "../Setup/MSSR_Employee.aspx?Popup=T&Dept=Training&ctrlId="+ctrlid;
   	                window.open(type,"aaApprovalLevelEmp","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	                return false;
   	            }
             }
        }
        
function HideShowLevel(id)
{

var days=document.getElementById(id).value;
if (days=='')
{
days=0
}
if (days==0)
{
document.getElementById(id).value='';
//document.getElementById(id).focus();
}
var CeilDays=Math.ceil(days);
if (CeilDays>9)
{
document.getElementById("lblError").innerText="No of Level Exceeds Maximum No. Of Level";
document.getElementById(id).value='';
document.getElementById(id).focus();
return false;
}
    for (i=1;i<=9;i++)
    {
        if (i<=CeilDays)
        {
            document.getElementById("trLevel" + i).style.display="block";   
        }
        else
        {
            document.getElementById("trLevel" + i).style.display="none";
        }
    }
    try
    {
    document.getElementById("trLevel1").focus();
    }
    catch(err)
    {
    
    }
}
    
  
    </script>
</body>
</html>
