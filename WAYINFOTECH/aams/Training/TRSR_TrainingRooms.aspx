<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_TrainingRooms.aspx.vb" Inherits="Training_TRSR_TrainingRooms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
   <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <title>AAMS::Training::Search Training Rooms</title>
    <script language="javascript" type="text/javascript">
     function EditFunction(Location_ID)
        {           
          window.location.href="TRUP_TrainingRooms.aspx?Action=U&Location_ID="+Location_ID ;               
          return false;
        }
        
        
         function SelectFunction(Location_ID,LOCATION_NAME,MaxNo)
        {           
        
         if (window.opener.document.forms['form1']['hdTrainingRoomPage']!=null)
        { 
        window.opener.document.forms['form1']['hdTrainingRoomPage'].value=Location_ID;
        //window.opener.document.forms['form1']['hdPageStatus'].value=strStatus;
         window.opener.document.forms['form1']['txtTrainingRoom'].value=LOCATION_NAME;
      window.opener.document.forms['form1']['txtParticipantMaxNo'].value=MaxNo;
        window.close();
            
        }
        
        // used in Course Session

if (window.opener.document.forms['form1']['hdRoomID']!=null)
{
window.opener.document.forms['form1']['hdRoomID'].value=Location_ID;
window.opener.document.forms['form1']['txtTrainingRoom'].value=LOCATION_NAME;
window.close();

}

        
        }
        
       
        
        function DeleteFunction(hdLocation_ID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=hdLocation_ID;       
           
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                 return false;
           }
        }
    </script>
    
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="ddlAOffice">
    <div>
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Training Rooms Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Training Rooms</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td class="textbold" style="width: 15%">
                                                AOffice</td>
                                            <td style="width: 30%">
                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" TabIndex="2" Width="176px" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="width: 15%">
                                                Name</td>
                                            <td style="width: 30%">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="170px"  TabIndex="2" MaxLength="50"></asp:TextBox></td>
                                            <td>
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">  </td>
                                            <td style="width: 30%"><input type="hidden" id="hdDeleteId" runat="server" style="width: 4px" />
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                            <td class="textbold" > </td>    <td>
                                                </td> <td>    
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td >   </td>    <td>
                                                &nbsp;</td><td > </td><td>
                                                &nbsp;</td>
                                            <td>
                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" />
                                            
                                            
                                            </td>
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
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="gap" colspan="6">
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">

                                                                                <asp:GridView  ID="gvTrainingRooms" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="False" AllowSorting="True" HeaderStyle-ForeColor="white"   >
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="AOffice" DataField="AOFFICE" SortExpression="AOFFICE" />
                                                                                    <asp:BoundField HeaderText="City" DataField="CITY" SortExpression="CITY" />
                                                                                    <asp:BoundField HeaderText="Location" DataField="TR_CLOCATION_NAME" SortExpression="TR_CLOCATION_NAME" />
                                                                                    <asp:BoundField HeaderText="Max NB Part" DataField="TR_CLOCATION_MAXNBPART" SortExpression="TR_CLOCATION_MAXNBPART"/>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandName ="SelectX" Text ="Select" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                              <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                               <asp:HiddenField ID="hdLocation_ID" runat="server" Value='<%#Eval("TR_CLOCATION_ID")%>' />   
                                                               
                                                             </ItemTemplate>
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                    <pagersettings  
                                                      pagebuttoncount="5"/>
                                                   
                                                    
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
