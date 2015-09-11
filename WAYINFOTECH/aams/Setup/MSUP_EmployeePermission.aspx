<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_EmployeePermission.aspx.vb"
    Inherits="Setup_MSUP_EmployeePermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script language="javascript" type="text/javascript">

function showenlargeimage(obj)
{
    obj.style.height="200";
    obj.style.width="200";
}
function showenSmallimage(obj)
{
    obj.style.height="30px";
    obj.style.width="50px";
}


function Large(obj)  
 {  
  var imgbox=document.getElementById("imgbox");  
  imgbox.style.visibility='visible';  
  var img = document.createElement("img");  
  img.src=obj.src;  
  img.style.width=obj.width * 15;  
  img.style.height=obj.height * 10;
  imgbox.innerHTML='';  
  imgbox.appendChild(img);  
 }   

    

 function Out(obj)  
 {  
     document.getElementById("imgbox").style.visibility='hidden';  
 }  

 function Move(obj,e)  
 {  
   var mouseY=e.clientY;  
   // alert(e.x)  
  var mouseX=e.clientX;  
  var scrollTop=document.documentElement.scrollTop;  
  var scrollLeft=document.documentElement.scrollLeft;
  var y=scrollTop+mouseY+20;  
  var x=scrollLeft+mouseX+20; 
  document.getElementById("imgbox").style.left=x + "px";  
  document.getElementById("imgbox").style.top=y + "px";  
 }  



function openPopup()
{
 
    var strEnEmployeeID=document.getElementById("hdEnEmployeeID").value;
    var type = "MSHT_Permission.aspx?EMPLOYEEID=" + strEnEmployeeID;
   	window.open(type,"aaEmployeeHistory","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	               	    
    return false;
   
}


function ColorMethod(id,total)
{   
        document.getElementById("lblError").innerHTML='';
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
        }
        
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       
       if('<%=Session("Action").ToString().Split("|").GetValue(0)%>'=='U')
       {      
       if (id == (ctextFront +  "00" + ctextBack))
       {   
           
           window.location.href="MSUP_Employee.aspx"
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
           
           window.location.href="MSUP_EmployeePermission.aspx" 
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {       
       
           window.location.href="MSUP_EmployeeGroup.aspx"
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {
           
           window.location.href="MSUP_EmployeeIP.aspx"
           return false;
          
       }
       else if (id == (ctextFront +  "04" + ctextBack))
       {
           
            window.location.href="MSUP_EmployeeSupervisory.aspx"
            return false;
           
       }
        else if (id == (ctextFront +  "05" + ctextBack))
       {
           
            window.location.href="MSUP_EmployeeHelpDesk.aspx"
            return false;
           
       }
       
       }                               
       
}
function PermissionGrantAll()
{
    for(intcnt=1;intcnt<=document.getElementById('<%=gvPermission.ClientID%>').rows.length-1;intcnt++)
    {        
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[3].children[0].checked=true;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[4].children[0].checked=true;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[5].children[0].checked=true;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[6].children[0].checked=true;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[7].children[0].checked=true;
       
    }
     return false;
}
function PermissionRevokeAll()
{
    for(intcnt=1;intcnt<=document.getElementById('<%=gvPermission.ClientID%>').rows.length-1;intcnt++)
    {        
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[3].children[0].checked=false;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[4].children[0].checked=false;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[5].children[0].checked=false;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[6].children[0].checked=false;
        document.getElementById('<%=gvPermission.ClientID%>').rows[intcnt].cells[7].children[0].checked=false;
       
    }
     return false;
}


//Code Written by Mukund on 5th Feb 2008
//rdCopyPermissionDesignation
//rdCopyPermissionEmployee
//drpDesignation
//drpPermissionEmployee

 function optionSelection()
    {
    if (document.getElementById("rdCopyPermissionEmployee").checked==true)
    {
      document.getElementById("drpDesignation").style.display ="none";
      document.getElementById("drpPermissionEmployee").style.display ="block";   
    }
   else  if (document.getElementById("rdCopyPermissionDesignation").checked==true)
    {
       document.getElementById("drpDesignation").style.display ="block";
       document.getElementById("drpPermissionEmployee").style.display ="none";   
    }
    else 
   {
      document.getElementById("drpDesignation").style.display ="block";
       document.getElementById("drpPermissionEmployee").style.display ="none";   
    }

    }



</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnPermissionReset">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee Permission</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading" align="center">
                                            Manage User</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    <asp:RadioButton ID="rdCopyPermissionDesignation" runat="server" CssClass="textfield"
                                                                        GroupName="Copy" Checked="true" Text="Copy Permission from Designation" Width="216px" /></td>
                                                                <td>
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="drpDesignation" runat="server" Width="137px"
                                                                        CssClass="dropdown">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <asp:Button ID="btnApply" runat="server" CssClass="button" Text="Apply" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 35%;">
                                                                    <asp:RadioButton ID="rdCopyPermissionEmployee" CssClass="textfield" runat="server"
                                                                        GroupName="Copy" Text="Copy Permission from Employee" Width="216px" /></td>
                                                                <td style="width: 25%;">
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="drpPermissionEmployee" CssClass="dropdown"
                                                                        runat="server" Width="137px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 25%;">
                                                                    <asp:Button ID="btnPermissionSave" runat="server" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPermissionReset" runat="server" CssClass="button" Text="Reset"
                                                                        AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPerGrantAll" runat="server" CssClass="button" Text="Grant All" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPerRevokeAll" runat="server" CssClass="button" Text="Revoke All" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="hidden" id="hdEnEmployeeID" runat="server" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="bntHistory" runat="server" CssClass="button" Text="History" OnClientClick="return openPopup()" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="3">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                <div id="imgbox" style="position:absolute;border:3px solid #999;filter: Alpha(Opacity=85);visibility:hidden; "></div>
                                                                
                                                                    <asp:GridView ID="gvPermission" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                        Width="90%" AllowSorting="True">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Control" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>                                                                                                                                                  
                                                                                  <asp:Image ID="ImgPermission" Width="50px"  Height="30px" runat="server" ImageUrl='<%# "Handler.ashx?ID=" + Eval("SecurityOptionID")%>' />                                                                                
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Sec_Group" HeaderText="Category" HeaderStyle-Width="20%"
                                                                                HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                                            <asp:BoundField DataField="SecurityOptionSubName" HeaderText="Sub Category" HeaderStyle-Width="20%"
                                                                                HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkView" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkAdd" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Modify" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkModify" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkPrint" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="SecurityOptionID" HeaderText="Hidden" HeaderStyle-Width="1%"
                                                                                HeaderStyle-CssClass="displayNone" ItemStyle-CssClass="displayNone"></asp:BoundField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <RowStyle CssClass="textbold" />
                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                    </asp:GridView>
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
            </tr>
        </table>
    </form>
</body>
</html>
