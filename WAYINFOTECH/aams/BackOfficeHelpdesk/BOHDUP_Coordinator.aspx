<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDUP_Coordinator.aspx.vb" Inherits="BOHelpDesk_MSUP_Coordinator" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AAMS::Back Office HelpDesk::Manage Coordinator</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
    <script language ="javascript" type="text/javascript">
    function Delete()
	{
		 			 
		 if (confirm("Are you sure you want to delete?") != true)
            {    
              return false;
            }
            
	}
    function PopupAgencyPage()
         {
         var type;
          
          var AofficeVar = document.getElementById("drpAoffice").value
         if (AofficeVar != "")
         {
            
            var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
                if (strEmployeePageName.toUpperCase()=="MSSR_EMPLOYEE.ASPX")
                {
                    strEmployeePageName="BOHDSR_Employee.aspx";
                }
                else
                {
                    strEmployeePageName="BOHDSR_EmployeeR.aspx";
                }
                document.getElementById("hdAOffice").value = AofficeVar
                  type = "../BackOfficeHelpDesk/" + strEmployeePageName+ "?Popup=T&Aoffice="+AofficeVar ;
                 //type = "../HelpDesk/HDSR_Employee.aspx?Popup=T&Aoffice="+AofficeVar ;
   	            window.open(type,"aa","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
               // document.forms(0).submit();
                return false;
            }
         }
         else
         {
            document.getElementById("lblError").innerText = "Please select AOffice."
            return false;
         }
           
     }
   

    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Back Office HelpDesk-&gt;</span><span class="sub_menu">Manage Coordinator</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Coordinator </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">   <table style="width: 100%" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                            <tr>
                                                <td style="width: 16%; height: 18px;">
                                                </td>
                                                <td class="textbold" style="height: 18px; text-align: center;" colspan="6">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                <td style="width: 34%; height: 18px;">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            Coordinator<span class="Mandatory" >*</span></td>
                                        <td colspan="4">
                                            <asp:RadioButton ID="rbtCord1" runat="server" Checked="True" CssClass="dropdownlist" Text="Coordinator1" GroupName="Coordinator" />
                                            &nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtCord2" runat="server" CssClass="dropdownlist" Text="Coordinator2" GroupName="Coordinator" /></td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                        AOffice<span class="Mandatory" >*</span>
                                            </td>
                                        <td colspan="4" class="textbold">
                                            <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" TabIndex="4"
                                                Width="206px" onkeyup="gotop(this.id)">
                                            </asp:DropDownList></td>
                                        <td>
                                                                    </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td style="width: 18%" class="textbold">
                                            </td>
                                        <td style="width: 25%; text-align: center;" class="textbold" colspan="4" >
                                            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add Employee" TabIndex="11" Width="135px" OnClientClick="return PopupAgencyPage();" /></td>
                                        
                                        <td>
                                            </td>
                                                                    <td> <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                            </td>
                                        <td colspan="4" style="height: 23px; text-align: center;"></td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                            &nbsp;
                                                                    </td>
                                    </tr>
                                    
                                        <tr>
                                        <td style="width: 16%; height: 21px;">
                                        </td>
                                            <td colspan="6">
                                                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                                    EnableViewState="True" TabIndex="7" Width="100%" AllowSorting="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="Employee Id" HeaderStyle-CssClass="displayNone" ItemStyle-CssClass="displayNone"/>
                                                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Employee" SortExpression="EMPLOYEE_NAME"   />
                                                        
                                                                                                  
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("EMPLOYEE_ID") %>'
                                                                    CommandName="Deletex" Text="Delete" CssClass ="LinkButtons"></asp:LinkButton>
                                                                    <asp:HiddenField ID="hdEmpID" runat="server" Value=' <%#Eval("EMPLOYEE_ID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                </asp:GridView>
                                            </td>
                                        <td style="width: 34%; height: 21px;">
                                                                    </td>
                                    </tr>    
                                            
                                        <tr>                                        
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                           </td>
                                        <td colspan="4" style="height: 23px">
                                         </td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                                                    </td>
                                    </tr>       
                                           
                                             
                                        <tr>                                        
                                        <td style="width: 16%; height: 23px;">
                                        </td>
                                        <td style="width: 18%; height: 23px;" class="textbold">
                                          </td>
                                        <td colspan="4" style="height: 23px">
                                         </td>
                                        <td style="height: 23px">
                                        </td>
                                        <td style="width: 34%; height: 23px;">
                                                                    </td>
                                    </tr>          
                                            <tr>
                                                <td style="width: 16%; height: 23px">
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                                </td>
                                                <td class="textbold" style="width: 18%; height: 23px">
                                        <asp:HiddenField ID="hdEmpID" runat ="server" />
                                                </td>
                                                <td colspan="4" style="height: 23px">
                                        <asp:HiddenField ID="hdEmpName" runat ="server" />
                                                </td>
                                                <td style="height: 23px"><asp:HiddenField ID="hdAOffice" runat ="server" />
                                                </td>
                                                <td style="width: 34%; height: 23px"><asp:HiddenField ID="hdEmpData" runat ="server" />
                                                <asp:HiddenField ID="hdFinalVal" runat ="server" />
                                                </td>
                                            </tr>
                                           
                                            
                                       
                                    <tr>
                                        <td style="width: 16%">
                                        </td>
                                        <td class="ErrorMsg" colspan="5">
                                            Field Marked * are Mandatory</td>
                                        <td>
                                        </td>
                                        <td style="width: 34%">
                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
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
