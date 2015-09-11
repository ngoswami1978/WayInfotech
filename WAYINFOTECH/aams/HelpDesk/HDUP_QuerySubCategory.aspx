<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_QuerySubCategory.aspx.vb" Inherits="HelpDesk_HDUP_QuerySubCategory" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::HelpDesk::Manage Query Sub Category</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
    var st;
   function fillCategoryName(s)
   {
      document.getElementById('<%=ddlCategoryName.ClientId%>').options.length=0;
      document.getElementById('<%=ddlCategoryName.ClientId%>').disabled=true;
      document.getElementById('<%=ddlCategoryName.ClientId%>').options[0]=new Option("Loading...","0");  
      id=document.getElementById('<%=ddlQuerySubGroup.ClientId%>').value;
      CallServer(id,"This is context from client");
      return false;
   }
   
    function ReceiveServerData(args, context)
    {        
            var obj = new ActiveXObject("MsXml2.DOMDocument");
         	var codes='';
			var names="--Select One--";
			var ddlCategoryName = document.getElementById('<%=ddlCategoryName.ClientId%>');
			ddlCategoryName.disabled=false;
			if (args=="") 
            {
             listItem = new Option(names, codes );
             ddlCategoryName.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(args);
			    var dsRoot=obj.documentElement;    			
			    var orders = dsRoot.getElementsByTagName('CALL_CATEGORY');
			    var text;     			
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlCategoryName.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("CALL_CATEGORY_ID"); 
			        names=orders[count].getAttribute("CALL_CATEGORY_NAME"); 
				    listItem = new Option(names, codes);
				    ddlCategoryName.options[ddlCategoryName.length] = listItem;
			    }
			}
			
    }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body  >
    <form id="form1" runat="server"  defaultfocus="txtSubCategoryName" defaultbutton="btnSave">
    
    
      
    <!-- Code by Rakesh -->
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">HelpDesk-&gt;</span><span class="sub_menu">Manage Query Sub Category</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Query Sub Category</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Sub Category <span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                                                                <asp:TextBox ID="txtSubCategoryName" runat="server" CssClass="textbold" MaxLength="60"
                                                                                    TabIndex="2" Width="208px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" OnClientClick="return QuerySubCategoryFunctionalPage()" TabIndex="3" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Sub Group<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                                                                <asp:DropDownList ID="ddlQuerySubGroup" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                                                </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td class="textbold">
                                                Category Name<span class="Mandatory" >*</span></td>
                                            <td style="width: 308px">
                                                                                <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                                                <asp:ListItem Text ="--Select One--" Selected="True"></asp:ListItem>
                                                                                </asp:DropDownList></td>
                                            <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                                                Default
                                                                                Contact Type</td>
                                                                            <td style="width: 308px; height: 26px;"><asp:DropDownList ID="ddlContactType" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                                            </asp:DropDownList></td>
                                                                            <td style="height: 26px">
                                                                    </td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                                Default
                                                Query Status</td>
                                            <td style="width: 308px; height: 26px">
                                                <asp:DropDownList ID="ddlQueryStatus" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                                Default
                                                Query Priority</td>
                                            <td style="width: 308px; height: 26px">
                                                <asp:DropDownList ID="ddlQueryPriority" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                                Default
                                                Team</td>
                                            <td style="width: 308px; height: 26px">
                                                <asp:DropDownList ID="ddlTeam" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px;" >
                                            </td>
                                            <td class="textbold" >
                                                Title</td>
                                            <td style="width: 308px; ">
                                             <asp:TextBox ID="txtTitle" runat="server" CssClass="textbold" MaxLength="100" 
                                                                            TabIndex="2" Width="208px"></asp:TextBox>
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td class="textbold">
                                                Description</td>
                                            <td style="width: 308px">
                                                </td>
                                            <td >
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="width: 176px">
                                            </td>
                                        <td colspan="2">
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass="textbold" TextMode="multiLine" Rows="10"  TabIndex="2"
                                                    Width="508px"></asp:TextBox>
                                        </td>
                                         <td >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="2" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                            <td style="height: 26px">
                                                &nbsp;</td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center; height: 21px;">
                                                                                &nbsp;<input id="hdCategoryName" runat="server" style="width: 1px" type="hidden" />
                                                                                <input id="hdID" runat="server" style="width: 1px" type="hidden" /></td>
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
