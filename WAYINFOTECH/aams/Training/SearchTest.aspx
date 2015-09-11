<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="SearchTest.aspx.vb" Inherits="SearchTest" title="Untitled Page" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Training::Search Test</title>
   
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../JavaScript/Login.js"></script>
<script language ="javascript" type ="text/javascript" >
 function PopupPage(id)
         {
			var type;
			
			if (id=="1")
			{
					type = "TRSR_Course.aspx?Popup=T";
   					window.open(type,"aaCourse","height=600,width=900,top=30,left=20,scrollbars=1,status=1");  
   					return false;  	           
			}
	     }
   </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="BtnSearch" defaultfocus="txtTestName">
      
                    <table width="860px" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Search Test</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Test</td>
                        </tr>
                       
                        <tr>
                            <td valign="top" colspan="2" class="redborder"  >
       <table width="840px"  class="border_rightred" cellspacing="0" cellpadding="0">
       
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="text">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="ErrorMsg"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td align="center" style="height: 123px" valign="top">
                <table align="center" width="100%">
                    <tr>
                        <td align="left" class="textbold" style="width: 3%">
                        </td>
                        <td align="left" class="textbold" style="width: 17%">
                            Course Name</td>
                        <td align="left" width="160" style="width: 65%">
                            <asp:TextBox ID="txtTestName" runat="server" TabIndex="1" Width="491px" MaxLength="50"></asp:TextBox>
                            <img id="Img2" runat="server" alt="Select & Add Course" onclick="PopupPage(1)"
                                src="../Images/lookup.gif" style="cursor:pointer;" /></td> <td align="center" style="width: 15%">
                           <asp:Button ID="BtnSearch" runat="server" Text="Search" TabIndex="2" CssClass="button" AccessKey="a" />
                            
                            </td>
                    </tr>
                    <tr>
                        <td align="left" class="text">
                        </td>
                        <td align="left" class="textbold">
                            Level</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="dropdown" onkeyup="gotop(this.id)"
                                TabIndex="2" Width="174px">
                            </asp:DropDownList></td>
                        <td align="center">
                        <%--<asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="button"  TabIndex=3  Width="91px" />--%>
                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;
                        </td>
                        <td >
                                                    <input type="hidden" id="hdTest" runat="server" style="width: 2px" /></td>
                        <td align="center" >
                            <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="3" Text="Reset" AccessKey="r" /></td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">
                <asp:GridView ID="GrdTestDetails" runat="server" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
                    CellPadding="1" >
                    <Columns>
                         <asp:BoundField  HeaderText="S.No.">
                          <HeaderStyle Wrap="False"  HorizontalAlign="Left"  Width="20px"/>
                            <ItemStyle HorizontalAlign="Left" Width="20px"/>
                         </asp:BoundField>
                    
                        <asp:BoundField DataField="COURSE_NAME" HeaderText="Course Name" SortExpression="COURSE_NAME" >
                            <HeaderStyle   HorizontalAlign="Left"/>
                            <ItemStyle HorizontalAlign="Left" Wrap="true" Width="70%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_OF_QUESTIONS" HeaderText="No Of Question" Visible="false">
                            <HeaderStyle   HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Wrap="true"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="TEST_TIME" HeaderText="Test Time" Visible="false">
                            <HeaderStyle Wrap="False"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TOTAL_MARKS" HeaderText="Total Marks" SortExpression="TOTAL_MARKS">
                            <HeaderStyle Wrap="False"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Action" >
                        <HeaderStyle HorizontalAlign=Left />
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnUpdate" runat="server" CssClass="linkButtons" CommandName="EditX" CommandArgument='<%# Container.DataItem("COURSE_ID")%>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                   
                  
                     <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        
        
        
        <!-- Code for Paging by Mukund-->
        
          <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr  class="paddingtop paddingbottom" >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
        
        
        
        <!-- Code for Paging by Mukund-->
        
    </table>
    </td></tr></table>
    </form>
</body>
</html>
  
   


