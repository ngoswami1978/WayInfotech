<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_Employee.aspx.vb" Inherits="BirdresHelpDesk_HDSR_Employee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script language ="javascript" type ="text/javascript" >
   
  
    </script>
</head>
<body>
    <form id="form1" runat="server"  defaultbutton ="btnSearch" defaultfocus ="txtEmployeeName" >
        <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">
Birdres HelpDesk-&gt;</span><span class="sub_menu">Employee</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Employee
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" height="30px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Employee Name</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtEmployeeName" CssClass="textbox" MaxLength="40" runat="server" TabIndex="1"></asp:TextBox></td>
                                                                <td width="12%" >
                                                                    <span class="textbold">Department</span></td>
                                                                <td width="21%">
                                                                    <asp:DropDownList ID="drpDepartment" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="7" AccessKey="a" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" height="25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Aoffice</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" nowrap="nowrap" >
                                                                    Permission</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drplstPermission" CssClass="dropdownlist" runat="server" Width="136px" TabIndex="4" ></asp:DropDownList>
                                                                    </td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="9" AccessKey="r" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" height="30px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                    Designation</td>
                                                                <td class="textbold">
                                                                    <asp:DropDownList ID="drplstDeig" CssClass="dropdownlist"  runat="server" Width="136px" TabIndex="5" ></asp:DropDownList>
                                                                    </td>
                                                                <td nowrap="nowrap" class="textbold">
                                                                    Agreement Signed &nbsp;
                                                                </td>
                                                                <td>
                                                                <asp:DropDownList ID="drpAgreementSigned" CssClass="dropdownlist" runat="server" Width="136px" TabIndex="4" >
                                                                <asp:ListItem Text="Agreement Signed" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Agreement Not Signed" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="All" Value="3" Selected="true" ></asp:ListItem>
                                                                
                                                                </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:Button ID="btnNew" CssClass="button displayNone" runat="server" Text="New" TabIndex="8" AccessKey="n" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" height="30">
                                                                </td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                    Request</td>
                                                                <td class="textbold">
                                                                    <asp:CheckBox ID="chkRequest" runat="server" TabIndex="2" /></td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    <input id="hdStartId" style="width: 1px" type="hidden" runat="server" />
                                                                    <input id="hdEndId" style="width: 1px" type="hidden" runat="server" /></td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;<asp:Button ID="btnSelect" runat="server" CssClass="button" Text="Select" OnClientClick="return ReturnData()" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                 <asp:GridView EnableViewState="False" ID="gvEmployee" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAll();" /> 
                                                                        </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                            
                                                                             <input type="checkbox" id="chkSelect" name="chkSelect" runat="server"  /> 
                                                                             <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("EmployeeID") + "|" + Container.DataItem("Employee_Name") %>' />   
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name" />
                                                                         <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                                                         <asp:BoundField DataField="Aoffice" HeaderText="Aoffice" />
                                                                         <asp:BoundField DataField="City_Name" HeaderText="City" />
                                                                   </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />                                                    
                                                 </asp:GridView>
                                                                   </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6"><asp:HiddenField ID="hdDataEmployee" runat="server"  />
                                                                   <asp:HiddenField ID="hdData" runat="server"  />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12" class="right">
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
    <script language ="javascript" type="text/javascript">
   // Function for checking all check boxes.
  
    function SelectAll() 
    {
       CheckAllDataGridCheckBoxes(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxes(value) 
    {
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
            if (elm.id !='chkRequest')
              {
                elm.checked = value
              }
            }
        }
    }
    
	function ReturnData()
       {
       
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                        if (elm.id !='chkRequest')
                             {
                                
                                 if (elm.checked == true && elm.id != "chkAllSelect")
                                 {
                                    var chkname=elm.id;
                                    var gvname=chkname.split("_")[0];
                                    var ctrlidname=chkname.split("_")[1];
                                     if (document.getElementById("hdData").value == "")
                                     {
                                        document.getElementById("hdData").value ="I|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                                     }
                                     else
                                     {
                                        document.getElementById("hdData").value = document.getElementById("hdData").value + "," + "I|" + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                                     }
                                 
                                 }
                             }
                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
           else
           {
                 if (window.opener.document.forms['form1']['hdEmpData']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdEmpData'].value=="")
                    {
                        window.opener.document.forms['form1']['hdEmpData'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdEmpData'].value=window.opener.document.forms['form1']['hdEmpData'].value + "," + data;
                    }
                    window.opener.document.forms(0).submit();
                    window.close();
                    return false;
                 }
           }
       }   
				
    
    </script>
</body>
</html>
