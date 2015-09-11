<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOTAUP_FeedBack.aspx.vb" Inherits="TravelAgency_BOTAUP_FeedBack" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>AAMS::Agency::Manage User Name</title>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">

        function PopupPage(id)
{
var type;
if (id=="1")
{
type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
window.open(type,"aaFeedBack","height=600,width=900,top=30,left=20,scrollbars=1");
}
}
        
                
        function EditFunction(FeedbackID,HD_RE_ID,HD_QUERY_GROUP_ID,LCode,AOFFICE,Status)
        {         
          var strStatus="";
          if (HD_QUERY_GROUP_ID=="1")
          {
          strStatus=Status;
          type = "../BackOfficeHelpDesk/BOHDUP_helpDeskFeedBack.aspx?Action=U&Popup=T&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&FeedBackId=" + FeedbackID + "&AOFFICE="+ AOFFICE + "&QueryGroup=" +strStatus ;               
          window.open(type,"TaaAgencyFunctionalCallLog","height=600,width=900,top=30,left=20,scrollbars=1");	
          //window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&FeedBackId=" + FeedbackID + "&AOFFICE="+ AOFFICE + "&QueryGroup=" +strStatus ;               
          }
          else
          {strStatus=Status;         
          type = "../BackOfficeHelpDesk/BOHDUP_helpDeskFeedBack.aspx?Action=U&Popup=T&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus + "&FeedBackId=" + FeedbackID + "&AOFFICE="+ AOFFICE + "&QueryGroup=" +strStatus ;               
          window.open(type,"TaaAgencyTechnicalCallLog","height=600,width=900,top=30,left=20,scrollbars=1");	
          }
          return false;
        }
                     
        
        function DeleteFunction(hdFeedbackID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=hdFeedbackID;       
           
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                 return false;
           }
        }
 
  function fnAgencyCallLogID()
    {   
    
        window.close();
        return false;
  
    }
    </script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     
    <!-- Code by Rakesh -->
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" style="width:80%; height: 21px;">
                            <span class="menu">Agency-&gt;</span><span class="sub_menu">FeedBack History</span></td>
                              <td class="right" style="width:20%; height: 21px;">
                                        <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnAgencyCallLogID()" >Close</asp:LinkButton> &nbsp; &nbsp;&nbsp;
                                        </td>

                        </tr>
                        <tr >
                            <td colspan="2" class="heading" align="center" valign="top">
                                FeedBack History</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 277px" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                    <td class="top" colspan="2" style="width:100%">
                                    <uc1:MenuControl ID="MenuControl1" runat="server" />
                                    </td>
                                    </tr>
                                    <tr>
                                        <td class="redborder center" colspan="2">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td >
                                            <asp:HiddenField ID="hdDeleteId" runat="server" />
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                            <input type="hidden" id="hdLCode" runat="server" style="width:1px" />
                                            </td>
                                            <td >       </td>    <td  colspan="4">
                                                                                </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <asp:GridView  ID="gvFeedBack" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="False" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="FeedBack ID" DataField="FEEDBACK_ID" SortExpression="FEEDBACK_ID" />
                                                                                    <asp:BoundField HeaderText="LTR No" DataField="HD_RE_ID" SortExpression="HD_RE_ID" />
                                                                                    <asp:BoundField HeaderText="Office Id" DataField="OFFICEID" SortExpression="OFFICEID"/>
                                                                                    <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE"/>
                                                                                    <asp:BoundField HeaderText="Date" DataField="DATETIME" SortExpression="DATETIME"/>
                                                                                    <asp:BoundField HeaderText="LoggedBy" DataField="LoggedBy" SortExpression="LoggedBy"/>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="displayNone"></asp:LinkButton>
                                                                  
                                                               <asp:HiddenField ID="hdFeedbackID" runat="server" Value='<%#Eval("FEEDBACK_ID")%>' />   
                                                             </ItemTemplate>
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
                                             <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="860px">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
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
                                              
     
    </form>
    
   
   
    
    
     
</body>
</html>
