<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_Course.aspx.vb" Inherits="Training_TRSR_Course" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js"></script>
  <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Training::Search Course</title>
   
     <script language="javascript" type="text/javascript">
     
   
    
     function SelectFunction(str3)
        {   
          
            var pos=str3.split('|'); 
            
            if (window.opener.document.forms['form1']['hdTest']!=null)
            {
            window.opener.document.forms['form1']['hdTest'].value=pos[0];
            window.opener.document.forms['form1']['txtTestName'].value=pos[1];
                       
            }
//             if (window.opener.document.forms['form1']['hdTestQuestion']!=null)
//            {
//            window.opener.document.forms['form1']['hdCourseID'].value=pos[0];
//                  
//            }
            // CODE ADDED BY PANKAJ
             if (window.opener.document.forms['form1']['hdTestQuestion']!=null)
            {
                 window.opener.document.forms['form1']['hdCourseID'].value = pos[0];
                 window.opener.document.forms['form1'].submit();
                 window.close();
                return false;
                
                 
            }
            //  CODE ENDED
            
            if (window.opener.document.forms['form1']['hdCourseSessionCourseLevel']!=null)
            {
            window.opener.document.forms['form1']['hdCourseID'].value=pos[0];
            window.opener.document.forms['form1']['txtCourseTitle'].value=pos[1];
            window.opener.document.forms['form1']['txtCourseLevel'].value=pos[2];
            
            }
            try
            {
                if (window.opener.document.forms['form1']['hdnCopyCourseId']!=null)
                    {
                                        if ((window.opener.document.forms['form1']['hdCourseID'].value != '') && (window.opener.document.forms['form1']['hdnCopyCourseId'].value ==''))
                                        {
                                                window.opener.GetData(pos[0]);
                                                 return false;  
                                        }
                    }
            }
            catch(err){}
            
           try
            {
       
               if (window.opener.document.forms['form1']['hdCourseID']!=null)
                    {
                                        if ((window.opener.document.forms['form1']['hdCourseID'].value == '') && (window.opener.document.forms['form1']['hdnCopyCourseId'].value ==''))
                                        {
                                                window.opener.GetDataMain(pos[0],pos[1]);
                                        }
                   }
            }
            catch(err){}   
            
            
            window.close();
       }
       
    function Edit(CourseID)
			{
				 window.location.href="TRUP_Course.aspx?Action=U&CourseID=" +CourseID
				 return false;
			}
			
	function Delete(CourseID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdCourseID.ClientId%>').value = CourseID
               return true;        
            }
            return false;
	}
	
 



			
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Course Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Course</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                                </td>
                                            <td class="textbold" style="width: 10%"> Course</td>                                                                               
                                            <td colspan="3">
                                            <asp:TextBox ID="txtCourse" runat="server" CssClass="textbox" MaxLength="100" TabIndex="2" Width="528px"></asp:TextBox>
                                            </td>
                                            <td style="width: 18%;" >
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">  Level</td>
                                            <td style="width: 35%">
                                                <asp:DropDownList ID="ddlLevel" runat="server" CssClass="dropdown" TabIndex="2" Width="174px"  onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td class="textbold" > </td>    <td>
                                                <asp:CheckBox ID="chkShowOnWeb" runat="server" CssClass="textbold" TabIndex="2" Text="Show On Web &nbsp; &nbsp;  &nbsp; &nbsp;     " TextAlign="Left" /></td> <td>    
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td >   </td>    <td>
                                                &nbsp;</td><td > </td><td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="3" AccessKey="e" /></td>
                                        </tr>                           
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td ></td>
                                            <td ><asp:HiddenField ID="hdCourseId" runat ="server" />       </td> 
                                               <td  colspan="4">
                                                   <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <asp:GridView   ID="gvCourse" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="False" AllowSorting ="True"  HeaderStyle-ForeColor="white"   >
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Course" DataField="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME" />
                                                                                    <asp:BoundField HeaderText="Level" DataField="TR_COURSELEVEL_NAME" SortExpression="TR_COURSELEVEL_NAME" >
                                                                                        <ItemStyle Width="15%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Duration" DataField="TR_COURSE_DURATION" SortExpression="TR_COURSE_DURATION"  >
                                                                                        <ItemStyle Width="15%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TR_COURSE_ID") + "|" + DataBinder.Eval(Container.DataItem, "TR_COURSE_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "TR_COURSELEVEL_NAME") %>'></asp:LinkButton>&nbsp;     
                                                              <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                              <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                               <asp:HiddenField ID="hdCourse" runat="server" Value='<%#Eval("TR_COURSE_ID")%>' />   
                                                               
                                                             </ItemTemplate>
                                                             <ItemStyle Width ="15%" />
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                   
                                                   
                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                              <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
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
