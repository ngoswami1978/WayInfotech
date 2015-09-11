<%@ Page Language="VB"   AutoEventWireup="false" CodeFile="MSSR_ManageAgencyGroup.aspx.vb" Inherits="Setup_MSSR_ManageAgency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Manage Agency Group</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    
       function SelectFunction(str3,CompVertical)
    {   
        //alert(str3);
        var pos=str3.split('|'); 
        
         if (window.opener.document.forms['form1']['TxtCompVertical']!=null) 
         {
          window.opener.document.forms['form1']['TxtCompVertical'].value =CompVertical;
         }
        
         if (window.opener.document.forms['form1']['hdFromIncentive']!=null) 
         {
                if (window.opener.document.forms['form1']['hdFromIncentive'].value=="1")
                {
                  window.opener.document.forms['form1']['hdChainCodeFromInc'].value =pos[0];
                    window.opener.document.forms['form1'].submit();
                  window.close();
                }
         }
        
        
        if (window.opener.document.forms['form1']['hdChainId']!=null)
        {
        window.opener.document.forms['form1']['hdChainId'].value=pos[0];
        }
         if (window.opener.document.forms['form1']['txtAgencyGroup']!=null)
        {
        window.opener.document.forms['form1']['txtAgencyGroup'].value=pos[1];
            if (window.opener.document.forms['form1']['txtChaincode']!=null)
                {                
                window.opener.document.forms['form1']['txtChaincode'].value='';
                window.opener.document.forms['form1']['txtChaincode'].disabled=true;      
                window.opener.document.forms['form1']['txtChaincode'].className="textboxgrey";     
                
                }
        }
        if (window.opener.document.forms['form1']['txtGroupClassification']!=null)
        {
        window.opener.document.forms['form1']['txtGroupClassification'].value=pos[2];
        }
          if (window.opener.document.forms['form1']['txtGroupName']!=null)
        {
        window.opener.document.forms['form1']['txtGroupName'].value=pos[1];
        }
        
         if (window.opener.document.forms['form1']['txtChainCode']!=null)
        {
        window.opener.document.forms['form1']['txtChainCode'].value=pos[0];
        }
        
        window.close();
   }
   function  NewMSUPManageAgencyGroup()
   {    
      // window.location="MSUP_ManageAgencyGroup.aspx?Action=I";
       window.location="MSUP_AgencyGroup.aspx?Action=I";
       return false;
   }
      function DeleteFunction(CheckBoxObj)
          {   
//            if (confirm("Are you sure you want to delete?")==true)
//            {          
//                //window.location.href="MSSR_ManageAgencyGroup.aspx?Action=D|"+CheckBoxObj;    
//                 window.location.href="MSSR_ManageAgencyGroup.aspx?Action=D|"+ CheckBoxObj +"|"+  document.getElementById("<%=txtGroupName.ClientID%>").value+"|"+ document.getElementById("drpCity").value +"|"+ document.getElementById("<%=drpLstGroupType.ClientID%>").selectedIndex +"|"+ document.getElementById("<%=drpLstAoffice.ClientID%>").selectedIndex +"|"+ document.getElementById("<%=txtChainCode.ClientID%>").value;                         
//                return false;
//            }
              if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteAgroup").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteAgroup").value="";
                 return false;
                }
        }
          function DeleteFunction2(CheckBoxObj)
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {          
                //window.location.href="MSSR_ManageAgencyGroup.aspx?Action=D|"+CheckBoxObj;    
                 window.location.href="MSSR_ManageAgencyGroup.aspx?PopUp=T&Action=D|"+ CheckBoxObj +"|"+  document.getElementById("<%=txtGroupName.ClientID%>").value+"|"+ document.getElementById("drpCity").value +"|"+ document.getElementById("<%=drpLstGroupType.ClientID%>").selectedIndex +"|"+ document.getElementById("<%=drpLstAoffice.ClientID%>").selectedIndex +"|"+ document.getElementById("<%=txtChainCode.ClientID%>").value;                         
                return false;
            }
        }
   
      function EditFunction(CheckBoxObj)
    {           
      //  window.location ="MSUP_ManageAgencyGroup.aspx?Action=U&Chain_Code=" + CheckBoxObj; 
         window.location ="MSUP_AgencyGroup.aspx?Action=U&Chain_Code=" + CheckBoxObj;       
        
          return false;
    }   
     
    function AGroupReset()
    {
        document.getElementById("txtGroupName").value="";
        document.getElementById("txtChainCode").value="";
        document.getElementById("drpCity").selectedIndex=0;      
        document.getElementById("drpLstGroupType").selectedIndex=0;
        document.getElementById("drpLstAoffice").selectedIndex=0;
        document.getElementById("chkMainGroup").checked=false
        document.getElementById("lblError").innerHTML="";    
        if ( document.getElementById("gvManageAgencyGroup")!=null)       
        document.getElementById("gvManageAgencyGroup").style.display ="none"; 
        document.getElementById("txtChainCode").focus();  
        return false;
    }
   function AGroupMandatory()
    {
        if (  document.getElementById("txtChainCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtChainCode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Chain code is not valid.";
            document.getElementById("txtChainCode").focus();
            return false;
            } 
         }
//          if (  document.getElementById("txtGroupName").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtGroupName").value,7)==false)
//            {
//            document.getElementById("lblError").innerHTML="Group name is not valid.";
//            document.getElementById("txtGroupName").focus();
//            return false;
//            } 
//         }
         
         return true;
     }
    </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="frmAgency" runat="server"  defaultfocus ="txtChainCode" >
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Travel Agency-></span><span class="sub_menu">Agency Group</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Search Agency Group</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Chain Code</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textfield" TabIndex="1" MaxLength="6" EnableViewState="False"></asp:TextBox></td>
                                                                <td width="12%" ></td>
                                                                <td width="21%"></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="7"  AccessKey="A"/></td>
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
                                                                     <asp:TextBox ID="txtGroupName" runat="server" CssClass="textfield" TabIndex="2" MaxLength="40"  Width="485px" EnableViewState="False"></asp:TextBox></td>
                                                              
                                                                <td width="18%" style="height: 22px">
                                                                     <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="8" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px">
                                                                    City</td>
                                                                <td style="height: 22px">
                                                                    <%--<asp:TextBox ID="txtCity" runat="server" CssClass="textfield" TabIndex="3" MaxLength="30" EnableViewState="False"></asp:TextBox>--%><asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" CssClass="dropdown" TabIndex="3"  runat ="server" ></asp:DropDownList></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Group Type</td>
                                                                <td style="height: 22px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstGroupType" runat="server" CssClass="dropdown" TabIndex="4">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 22px"><asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="9"  AccessKey="E"/></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Aoffice</td>
                                                                <td>
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstAoffice" runat="server" CssClass="dropdown" TabIndex="5">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="height: 22px">
                                                                    Main Group&nbsp;</td>
                                                                <td style="height: 22px">
                                                                    &nbsp;<asp:CheckBox ID="chkMainGroup" runat="server" TabIndex="6" /></td>
                                                                <td>
                                                                   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="10" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr height="20px" >
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvManageAgencyGroup" runat="server" AutoGenerateColumns="False" 
                                                                     AllowSorting ="true"  HeaderStyle-ForeColor="White"     Width="100%" TabIndex="10" EnableViewState="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Chain Code" SortExpression="Chain_Code">                                                                                
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Chain_Code")%> 
                                                                                    <asp:HiddenField ID="hdStatusCode" runat="server" Value='<%#Eval("Chain_Code")%>' />                                                                          
                                                                                </ItemTemplate>                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Name" SortExpression="Chain_Name">                                                                                
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Chain_Name")%>                                                                                                                                                                                                                                               
                                                                                </ItemTemplate>                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField ="PANNO" HeaderText ="PAN No." SortExpression ="PANNO" ItemStyle-Width="110px" />
                                                                            <asp:TemplateField HeaderText="City" SortExpression="City_Name">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("City_Name")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Type" SortExpression="GroupTypeName">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("GroupTypeName")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Aoffice" SortExpression="Aoffice">                                                                                
                                                                                <ItemTemplate>
                                                                                <%#Eval("Aoffice")%>
                                                                                </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">                                                                                
                                                                                <ItemTemplate>
                                                                                  <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Chain_Code") + "|" + DataBinder.Eval(Container.DataItem, "Chain_Name")+ "|" + DataBinder.Eval(Container.DataItem, "Group_Classification_Name") %>'>Select</asp:LinkButton>&nbsp;<a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton><%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                                  <asp:HiddenField ID="CompVertical" runat="server" Value ='<%#Eval("COMP_VERTICAL") %>' />
                                                                                  </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                               <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" >
                                                        <asp:HiddenField ID="hdDeleteAgroup" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
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
