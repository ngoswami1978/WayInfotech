<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProductMenu.ascx.vb" Inherits="UserControl_ProductMenu" %>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

function ColorMethod(id,total,strAction)
{                  
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
      // alert(strAction);
        ctextFront = id.substring(0,28);        
        ctextBack = id.substring(30,38);   
         
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;           
            document.getElementById(Hcontrol).className="headingtabactive";
        }        
        
       document.getElementById(id).className="headingtab";
       document.getElementById('<%=lblPanelClick.ClientID %>').value =id;                   
       if (id == (ctextFront +  "00" + ctextBack))
       {      
           window.location.href="MSUP_Order.aspx?Id=0&" + strAction;
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {            
           window.location.href="TAUP_MailingList.aspx?Id=1&" + strAction;
           return false;         
       }
     
     
}
</script>
<asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
<asp:Repeater ID="theTabStrip" runat="server">
    <ItemTemplate>
        <asp:Button ID="Button1" runat="server" Width="100px" CssClass="headingtabactive" CommandName='<%# Container.DataItem %>' Text='<%# Container.DataItem %>' />&nbsp;
    </ItemTemplate>
</asp:Repeater>
