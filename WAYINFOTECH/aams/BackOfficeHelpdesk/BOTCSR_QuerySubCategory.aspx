<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOTCSR_QuerySubCategory.aspx.vb" Inherits="BOHelpDesk_Technical_MSSR_QuerySubCategory" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Back Office Technical::Search Query Sub Category</title>
   
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
    
     <script language="javascript" type="text/javascript">
      
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
			var names="--All--";
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
			    if (dsRoot !=null)
			    {   			
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
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlCategoryName.options[0] = listItem;
			    }
			}
			
    }
           function EditFunction(CategoryID)
    {           
          window.location.href="BOTCUP_QuerySubCategory.aspx?Action=U|"+CategoryID;               
          return false;
    }
 
    function DeleteFunction(CategoryID)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
        //  window.location.href="TCSR_QuerySubCategory.aspx?Action=D|"+ CategoryID +"|"+ document.getElementById("<%=txtSubCategoryName.ClientID%>").value +"|"+ document.getElementById("<%=ddlQuerySubGroup.ClientID%>").value +"|"+ document.getElementById("<%=ddlCategoryName.ClientID%>").value;                                      
            document.getElementById('<%=hdID.ClientId%>').value  ="D|"+CategoryID+"|"+ document.getElementById("<%=txtSubCategoryName.ClientID%>").value +"|"+ document.getElementById("<%=ddlQuerySubGroup.ClientID%>").value +"|"+ document.getElementById("<%=ddlCategoryName.ClientID%>").value;                                      
            return true;

        }
         return false;
         
    }
   
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtSubCategoryName">
   
     <div>
    <table width="860px"  height="486px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu">Back Office  Technical -&gt;</span><span class="sub_menu">Query Sub Category Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Query Sub Category</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 7%">             </td>
                                                                            <td class="textbold">     Sub Category</td>
                                                                            <td>
                                                                    <asp:TextBox ID="txtSubCategoryName" runat="server" CssClass="textbold" Width="208px" MaxLength="60" TabIndex="2"></asp:TextBox></td>
                                                                            <td> <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>    </td>
                                                                            <td class="textbold">   Query Sub Group</td>
                                                                            <td >  
                                                                                <asp:DropDownList ID="ddlQuerySubGroup" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                                                </asp:DropDownList></td>
                                                                            <td>  <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >          </td>
                                                                            <td class="textbold" >   Category Name</td>
                                                                            <td > <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2" onkeyup="gotop(this.id)">
                                                                            <asp:ListItem Text ="--All--" Selected="True" Value =""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                                <input id="hdCategoryName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                            <td >   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                <asp:HiddenField ID="hdID" runat="server" />
                                                </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                        <td></td>
                                                                            <td colspan="3">
                                                                                <asp:GridView EnableViewState="False" ID="gvQuerySubCategory" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="90%" AllowSorting="True">
                                                                                <Columns>
                                                                                     <asp:BoundField DataField="CALL_SUB_GROUP_NAME" HeaderText="Query Sub Group" SortExpression="CALL_SUB_GROUP_NAME" />
                                                                                    <asp:BoundField DataField="CALL_CATEGORY_NAME" HeaderText="Category Name" SortExpression="CALL_CATEGORY_NAME" />
                                                                                    <asp:BoundField DataField="CALL_SUB_CATEGORY_NAME" HeaderText="Sub Category" SortExpression="CALL_SUB_CATEGORY_NAME" />
                                                                                     <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                               <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> 
                                                                 <asp:HiddenField ID="hdQuerySubCategory" runat="server" Value='<%#Eval("CALL_SUB_CATEGORY_ID")%>' />   
                                                             </ItemTemplate>
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white"/>                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                             <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="90%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 35%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="70px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 20%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 25%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center" colspan="4">
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
    </div>
   
    </form>
</body>
</html>
