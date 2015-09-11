<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_TravelAgencySalesStaff.aspx.vb" Inherits="TravelAgency_TASR_TravelAgencySalesStaff" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Agency Staff</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
</head>

<script language="javascript" type="text/javascript">  
 
   
       function SelectFunction(str,Fname,MName,SurName,Desig)
        {  
        var pos=str.split('|'); 
       // alert(Fname);
       // alert(MName);
       // alert(SurName);

// used in Sales Module
if (window.opener.document.forms['form1']['hdPersonMetStaff']!=null)
{
window.opener.document.forms['form1']['hdPersonMetStaff'].value=pos[0];
window.opener.document.forms['form1']['txtPersonMet'].value=pos[1];
window.opener.document.forms['form1']['txtDesignation'].value=Desig;

window.close();
}

 // used in course session
if (window.opener.document.forms['form1']['hdCourseStaff']!=null)
{
window.opener.document.forms['form1']['hdCourseStaff'].value=pos[0];
window.opener.document.forms['form1']['txtAgencyStaff'].value=pos[1];
window.close();
}
    if (window.opener.document.forms['form1']['hdCourseStaff1']!=null)
{
window.opener.document.forms['form1']['hdCourseStaff1'].value=pos[0];
window.opener.document.forms['form1']['txtAgencyStaff'].value=pos[1];
window.close();
}
        
         if (window.opener.document.forms['form1']['hdCourseSessionPeoplePopup']!=null)
        { 
        window.opener.document.forms['form1']['hdCourseSessionPeoplePopup'].value='S' + "|" + pos[0] + "|" + pos[3] + "|" + pos[1] + "|" + pos[2];
        window.opener.document.forms['form1'].submit();
        window.close();
        return false;
            
        }
        
        
         // Code For Training Module
        
         if (window.opener.document.forms['form1']['hdTrainingStaffID']!=null)
        { 
        window.opener.document.forms['form1']['hdTrainingStaffID'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyStaff'].value=pos[1];
        window.close();
        }
        //For update
        if (window.opener.document.forms['form1']['hdAgencyStaffNameParticipantBasket']!=null)
        { 
        window.opener.document.forms['form1']['hdAgencyStaffNameParticipantBasket'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyStaff'].value=pos[1];
        window.close();
        }
        //


        if (window.opener.document.forms['form1']['hdCallCallerName']!=null)
        {
        window.opener.document.forms['form1']['txtCallerName'].value=pos[1];
        window.opener.document.forms['form1']['hdCallCallerName'].value=str;
         if (window.opener.document.forms['form1']['hdCallerName']!=null)
            {
              window.opener.document.forms['form1']['hdCallerName'].value=pos[1];
            }
            if (window.opener.document.forms['form1']['DlstCallerName']!=null)
            {
              window.opener.document.forms['form1']['DlstCallerName'].value=Fname + SurName + "$" + pos[4] ;
            }
            
        }
       	window.close();
        }
     function StaffReset()
    {
        document.getElementById("txtStaffName").value="";       
        document.getElementById("txtAgencyName").value="";       
        //return false;
    }
    function EditFunction(AGENCYSTAFFID)
    {   
                  
        window.location.href="TAUP_TravelAgencySalesStaff.aspx?Action=U&ID="+AGENCYSTAFFID;       
        return false;
    }          

    function DeleteFunction(CheckBoxObj)
    {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {          
//            window.location.href="TASR_AgencyStaff.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=txtStaffName.ClientID%>").value +"|"+ document.getElementById("<%=txtAgencyName.ClientID%>").value + "|"+ document.getElementById("<%=txtOfficeId.ClientID%>").value;                   
//            return false;
//        }
        if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('hdStaffID').value = CheckBoxObj;
               document.forms['form1'].submit();     
            }
            else
            {
                return false;
            }
    }
    
    function NewFunction()
    {           
        window.location.href="TAUP_AgencyStaff1.aspx?Action=I";       
        return false;
    }
    
    function PopupAgencyPage()
    {
   var type;
 // type = "../Popup/PUSR_Agency.aspx" ;
    type = "TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aarr","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
   return false;
    }   

</script>

<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency Staff</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Agency Staff
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 346px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table width="80%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px" width="10%">
                                                                </td>
                                                                <td class="textbold" style="height: 22px" width="20%">Sign In
                                                                </td>
                                                                <td style="height: 22px" width="25%"> <asp:TextBox ID="TxtSignInNum" CssClass="textbox" runat="server" MaxLength="4" TabIndex="1"
                                                                        Width="60px"></asp:TextBox>&nbsp; <asp:TextBox ID="TxtSignInChar" CssClass="textbox" runat="server" MaxLength="2" TabIndex="1"  onkeyup="checkalphbets(this.id)"
                                                                        Width="40px"></asp:TextBox> 
                                                                </td>
                                                                <td style="height: 22px" width="20%" align="center" >
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                        AccessKey="A" /></td>
                                                            </tr>
                                                              <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" class="textbold">
                                                                    &nbsp;</td>
                                                                <td width="10%" class="textbold">
                                                                    Name<strong><span  class="Mandatory" >*</span></strong></td>
                                                                <td width="30%">
                                                                    <asp:TextBox ID="txtStaffName" CssClass="textbox" runat="server" MaxLength="50" TabIndex="1"
                                                                        Width="293px"></asp:TextBox></td>
                                                                <td width="20%"  align="center">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2"
                                                                        AccessKey="N" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;<input id="hdAgencyStaffAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Agency Name<strong><span class="Mandatory" >*</span></strong></td>
                                                                <td style="height: 22px" nowrap="nowrap">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" runat="server" MaxLength="40"
                                                                        TabIndex="1" Width="293px"></asp:TextBox>
                                                                    <img id="ImgAgency" runat="server" alt="" onclick="PopupAgencyPage();" style="cursor: pointer;"
                                                                        src="../Images/lookup.gif" tabindex="1" /></td>
                                                                <td style="height: 22px"  align="center">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export"
                                                                        AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 205px">
                                                                </td>
                                                                <td class="textbold">
                                                                    Office ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1"
                                                                        Width="293px"></asp:TextBox>
                                                                </td>
                                                                <td  align="center">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                        AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 205px">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 205px">
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                    <asp:HiddenField ID="hdStaffID" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="840px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td height="12">
                                                                    &nbsp;<asp:GridView ID="grdStaff" BorderWidth="1" AllowSorting="true" HeaderStyle-ForeColor="white"
                                                                        BorderColor="#d4d0c8" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        EnableViewState="false" TabIndex="3">
                                                                        <Columns>
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdStaffID" Value='<%#Eval("AGENCYSTAFFID")%>' runat="server" />
                                                                                    <asp:HiddenField ID="hdLCode" Value='<%#Eval("LCODE")%>' runat="server" />
                                                                                    <asp:HiddenField ID="HdSigninId" Value='<%#Eval("SIGNINID")%>' runat="server" />
                                                                                    <input type="hidden" runat="server" id="hdSelect" />
                                                                                    <asp:LinkButton ID="lnkSelect" runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AGENCYSTAFFID") + "|" + DataBinder.Eval(Container.DataItem, "STAFFNAME") %> '>Select</asp:LinkButton>
                                                                                    &nbsp;<a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;<a href="#"
                                                                                        class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                              <asp:BoundField DataField="SIGNINID" SortExpression="SIGNINID" HeaderText="Sign In">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="STAFFNAME" SortExpression="STAFFNAME" HeaderText="Name">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            
                                                                            <asp:BoundField DataField="DESIGNATION" SortExpression="DESIGNATION" HeaderText="Designation">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>  
                                                                              <asp:BoundField DataField="EMAIL" SortExpression="EMAIL" HeaderText="Email Id">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                              <asp:BoundField DataField="MOBILENO" SortExpression="MOBILENO" HeaderText="Mobile No.">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>                                                                        
                                                                            <asp:BoundField DataField="AgencyName" SortExpression="AgencyName" HeaderText="Agency Name"   Visible ="false"     >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Address" SortExpression="Address" HeaderText="Address" Visible ="false">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Address1" SortExpression="Address1" HeaderText="Address1"  Visible ="false" >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="City"  Visible ="false" >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Country" SortExpression="Country" HeaderText="Country"  Visible ="false" >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Phone" SortExpression="Phone" HeaderText="Phone"  Visible ="false">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Fax" SortExpression="Fax" HeaderText="Fax"  Visible ="false" >
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" valign="top">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="96%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="90%">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 26%; height: 21px;" class="left" nowrap="nowrap">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                        TabIndex="4"></asp:TextBox></td>
                                                                                <td style="width: 17%; height: 21px;" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                                                        TabIndex="4"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 17%; height: 21px;" class="center">
                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="4">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="width: 25%; height: 21px;" class="left">
                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                                                        TabIndex="4">Next >></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                           
                                                        </table>
                                                    </td>
                                                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                        Visible="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hdPageSource" runat ="server" />
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
      if(document.getElementById('<%=txtStaffName.ClientId%>').value =='' && document.getElementById('<%=txtAgencyName.ClientId%>').value =='' && document.getElementById('<%=txtOfficeId.ClientId%>').value =='' && document.getElementById('<%=TxtSignInNum.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Please Enter either StaffName or AgencyName or OfficeId or SignIn ID.'
            document.getElementById('<%=txtStaffName.ClientId%>').focus();
            return false;
        }
       return true;         
    }
      
  function next(currentControl, maxLength, nextControl)
   {
         checknumeric(currentControl);
        if(document.getElementById(currentControl).value.length >= maxLength)
        {  
            document.getElementById(nextControl).focus();
        }
  } 

 function checkalphbets(objCtrlid)
   {
            var tempVal=document.getElementById(objCtrlid).value;
            var validVal="";
            for(var i=0;i<tempVal.length;i++)
            {
                 vAscii = tempVal.charCodeAt(i) ;
                 if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
                    {
                        validVal+=tempVal.substr(i,1);
                    }
                 else
                    {
                    }
            }
            document.getElementById(objCtrlid).value=validVal;
   }
 
   
  
  
    </script>

</body>
</html>
