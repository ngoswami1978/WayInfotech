<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_MailGroup.aspx.vb" Inherits="Order_MSUP_MailGroup" %>

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Email Group</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript">  
 
    function checkemail()
    {
    if (echeck() == false)
    {
        document.getElementById("lblError").innerText="Enter Correct Email Id";
        return false;
    }
    }
    function echeck() 
    {
        var str = document.getElementById('<%=txtEmail.ClientID %>').value
		var at="@"
		var dot="."
		var lat=str.indexOf(at)
		var lstr=str.length
		var ldot=str.indexOf(dot)
		if (str.indexOf(at)==-1){
		
		   return false
		}

		if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr){
		   return false
		}
		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr){
		    return false
		}

		 if (str.indexOf(at,(lat+1))!=-1){
		    return false
		 }
		 if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot){
		    return false
		 }

		 if (str.indexOf(dot,(lat+2))==-1){
		    return false
		 }
		
		 if (str.indexOf(" ")!=-1){
		    return false
		 }

 		 return true					
	}
    function hidedisplay()
    {
    if (document.getElementById('<%=ddlGrpType.ClientID %>').selectedIndex >2)
    {
        document.getElementById ("div1").style.setAttribute('display', 'block')
        document.getElementById ("ddlOffice").style.setAttribute('display', 'block')
    }
    else
    {
      document.getElementById ("div1").style.setAttribute('display', 'none')
      document.getElementById ("ddlOffice").style.setAttribute('display', 'none')
    }
    }  
        function f1(type1,type2)
            {
                    if (type1==1)
                    {
                    document.getElementById('<%=hdnTree1.ClientID %>').value=type1;
                    document.getElementById('<%=hdnTree2.ClientID %>').value=type2;
                    }
                    else
                    {
                    document.getElementById('<%=hdnTree1.ClientID %>').value=type1;
                    document.getElementById('<%=hdnTree2.ClientID %>').value=type2;
                    }
            return false;
            }  
            function f2()
            {            
            return false;
            }
            function f3()
            {
            document.getElementById('<%=hdnTree3.ClientID %>').value=document.getElementById('<%=ListBox1.ClientID %>').value;
            return false;
            }
            function checkselectedemail()
            {
                    if (document.getElementById('<%=hdnTree1.ClientID %>').value.trim()=="")
                    {
                    document.getElementById("lblError").innerText="Please select either Office group or email to add";
                    return false;
                    }
            }
            function checkselectedemail2()
            {
                    if (document.getElementById('<%=hdnTree3.ClientID %>').value.trim()=="")
                    {
                    document.getElementById("lblError").innerText="Please select email to Remove from list";
                    return false;
                    }
            }
     function validatepage()
            {
                    

             if (document.getElementById('<%=txtGroupName.ClientID %>').value.trim()=="")
                    {
                    document.getElementById("lblError").innerText="Group name is Mandatory !";
                    document.getElementById("txtGroupName").focus();
                    return false;
                    }
            if (document.getElementById('<%=ddlGrpType.ClientID %>').selectedIndex==0)
                    {
                    document.getElementById("lblError").innerText="Group type is Mandatory !";
                    document.getElementById("ddlGrpType").focus();
                    return false;
                    }
             if (document.getElementById('<%=ddlGrpType.ClientID %>').selectedIndex>2)
                    {
                    if (document.getElementById('<%=ddlOffice.ClientID %>').selectedIndex==0)
                        {
                        document.getElementById("lblError").innerText="Office is Mandatory !";
                        document.getElementById('<%=ddlOffice.ClientID %>').focus();
                        return false;
                        }
                    } 
                    
                
                var listboxcount = document.getElementById('<%=ListBox1.ClientID %>').options.length;
                    if(listboxcount==0)
                    {
                    document.getElementById("lblError").innerText="Enter atleast one email Id!";
                    return false;
                    }
            }
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff"  onload="hidedisplay()" >
    <form id="frmCity" runat="server"  defaultfocus="txtCtyName">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top"><input type ="hidden" runat ="server" name ="hdnTree" id="hdnTree" />
                <input type ="hidden" runat ="server" name ="hdnTree1" id="hdnTree1" />
                <input type ="hidden" runat ="server" name ="hdnTree2" id="hdnTree2" />
                <input type ="hidden" runat ="server" name ="hdnTree3" id="hdnTree3" />
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Email Group</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage <span style="font-family: Microsoft Sans Serif">Email Group</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                
                                    <tr>
                                        <td align="LEFT" class="redborder">   
                                        <table style="width: 100%" border="0" cellpadding="1" cellspacing="2">   
                                        <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>                                     
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 15%" class="textbold">
                                            </td>
                                        <td colspan="3">
                                                                    </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 15%" class="textbold">
                                            Group Name <span class="Mandatory">*</span></td>
                                        <td colspan="3" class ="textbox">
                                                                   <asp:TextBox ID="txtGroupName" runat ="server" CssClass ="textbox" Width="212px" MaxLength="30" Height="19px" ></asp:TextBox></td>
                                        <td style="width: 10%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" style="position: relative" AccessKey="S" /></td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 15%" class="textbold">
                                            Group Type <span class="Mandatory">*</span></td>
                                        <td colspan="3" class="dropdownlist"><asp:DropDownList ID="ddlGrpType" runat="server" CssClass="dropdownlist" Width="214px" onchange="hidedisplay();">
                                        <asp:ListItem Value ="0">Select One</asp:ListItem>
                                        <asp:ListItem Value ="1">MNC</asp:ListItem>
                                        <asp:ListItem Value ="2">ISP</asp:ListItem>
                                        <asp:ListItem Value ="3">1A Office</asp:ListItem>
                                        <asp:ListItem Value ="4">Training</asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td style="width: 10%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" style="position: relative" AccessKey="N" /></td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                    <tr id="troffice">
                                        <td style="width: 13%; ">
                                        </td>
                                        <td style="width: 15%;" class="textbold" >
                                            <div id="div1" runat ="server">Office</div>
                                            </td>
                                        <td colspan="3" class="dropdownlist"><asp:DropDownList ID="ddlOffice" runat="server" CssClass="dropdownlist" Width="214px" >
                                        </asp:DropDownList></td>
                                        <td style="width: 10%; ">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" style="position: relative" AccessKey="R" /></td>
                                        <td style="width: 34%;">
                                                                    </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Email Id</td>
                                        <td colspan="3" class ="textbox">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="212px" MaxLength="30" Height="19px"></asp:TextBox>
                                            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" style="position: relative; left: 11px; top: 0px;" /></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                    <tr height ="10px">
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td colspan="3">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%; height: 21px;">
                                        </td>
                                        <td class="textbold" style="height: 21px">
                                            &nbsp; &nbsp;
                                            Select Emailid from the
                                            List</td>
                                        <td style="width: 10%; height: 21px;">
                                        </td>
                                        <td class="textbold" colspan="2" align ="left" style="height: 21px; width: 221px;">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; Selected Emails&nbsp;</td>
                                        <td class="textbold" style="width: 10%; height: 21px;">
                                        </td>
                                        <td style="width: 34%; height: 21px;">
                                        </td>
                                    </tr>
                                   <tr>
                                   <td></td>
                                                                <td colspan="2" valign ="top">
                                                                
                                                                <asp:XmlDataSource ID="xmldatasource1" runat ="server" EnableCaching="false"></asp:XmlDataSource>
                                                                <asp:Panel ScrollBars="Vertical"  ID="pnlMailFrom" runat="server" Width="260px" height ="448px" BorderWidth="1px" >
                                                                <asp:TreeView  ID="treeview1" runat ="server" Width="273px" height ="448px" AutoGenerateDataBindings="true" ExpandDepth="2"  
                                                                DataSourceID ="xmldatasource1" MaxDataBindDepth="3" NodeIndent="0"  ForeColor="#000000" RootNodeStyle-CssClass="tree_menu" ParentNodeStyle-CssClass="tree_menu" NodeStyle-CssClass="tree_menu">
                                                               <DataBindings>
                                                                    <asp:TreeNodeBinding DataMember="MS_LISTEMPLOYEE_EMAIL_OUTPUT" Text="OFFICE" Value="OFFICE"/>
                                                                    <asp:TreeNodeBinding DataMember="AOFFICE_DETAILS" TextField="AOFFICE" ValueField="ID"/>
                                                                    <asp:TreeNodeBinding DataMember="EMPLOYEE_DETAIL" TextField="EMPLOYEE_NAME" ValueField="EMPLOYEEID" />
                                                                </DataBindings>
                                                                    <SelectedNodeStyle BackColor="ActiveBorder" />
                                                                </asp:TreeView>
                                                                </asp:Panel>
                                                                    </td>
                                                                    <td colspan="3" height="4" align ="center" style="vertical-align:top">
                                                                    <table width ="100%">
                                                                    <tr>
                                                                    <td  valign ="top">
                                                                    <table>
                                                                    <tr valign ="top">
                                                                    <td><asp:Button ID="btnAddemail" runat ="server" Text=">>" Width="35px"/>
                                                                    </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td><asp:Button ID="btnRemove" runat ="server" Text="<<" Width="36px"/>
                                                                    </td>
                                                                    </tr>
                                                                    </table>
                                                                    </td>
                                                                    <td valign ="top">
                                                                        <asp:ListBox ID="ListBox1" runat="server" Height="248px"  Width="212px" OnClick="f3();" ForeColor ="#000000" style="left: 2px; position: relative; top: 0px">
                                                                      </asp:ListBox> </td>
                                                                    </tr>
                                                                    </table>
                                                                    </td>
                                                                    
                                                              
                                                                    
                                                                      <td colspan="1" height="4">
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
</body>
</html>
