<%@ Control Language="VB" AutoEventWireup="false" CodeFile="IncAGControl.ascx.vb" Inherits="UserControl_IncAGControl" %>
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

function ColorMethod(id,total)
{              
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
       
        ctextFront = id.substring(0,28);        
        ctextBack = id.substring(30,38);   
         
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
       document.getElementById('<%=lblPanelClick.ClientID %>').value =id; 
       
         else if (id == (ctextFront +  "00" + ctextBack))
       {         
            window.location.href="MSUP_AgencyGroup.aspx?Id=0"
            return false;           
       }
                        
       if (id == (ctextFront +  "01" + ctextBack))
       {
       
           window.location.href="MSUP_AG_CRSDetails.aspx?Id=1"
           return false;
       }       
       else if (id == (ctextFront +  "02" + ctextBack))
       {            
           window.location.href="MSUP_AG_Competition.aspx?Id=2" 
           return false;         
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {        
           window.location.href="MSUP_AG_Staff.aspx?Id=3"
           return false;
       }
       else if (id == (ctextFront +  "04" + ctextBack))
       {        
           window.location.href="MSUP_AG_PC.aspx?Id=4"
           return false;          
       }
       else if (id == (ctextFront +  "05" + ctextBack))
       {         
            window.location.href="MSUP_AG_Contract.aspx.aspx?Id=5"
            return false;           
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {         
            window.location.href="MSUP_AG_BusinessCase.aspx?Id=6"
            return false;           
       }
       
      
}
</script>
<asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
<asp:Repeater ID="theTabStrip" runat="server">
    <ItemTemplate>
        <asp:Button ID="Button1" runat="server" Width="72px" CssClass="headingtabactive" CommandName='<%# Container.DataItem %>' Text='<%# Container.DataItem %>' />&nbsp;
    </ItemTemplate>
</asp:Repeater>
