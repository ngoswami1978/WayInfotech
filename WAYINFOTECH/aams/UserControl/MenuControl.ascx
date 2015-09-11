<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MenuControl.ascx.vb" Inherits="UserControl_MenuControl"  %>
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
       if (id == (ctextFront +  "00" + ctextBack))
       {
       
           window.location.href="TAUP_Agency.aspx?Id=0"
           return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {            
           window.location.href="TAUP_AgencyCRS.aspx?Id=1" 
           return false;         
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {        
           window.location.href="TAUP_AgencyCompetition.aspx?Id=2"
           return false;
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {        
           window.location.href="TAUP_AgencyStaff.aspx?Id=3"
           return false;          
       }
       else if (id == (ctextFront +  "04" + ctextBack))
       {         
            window.location.href="TAUP_AgencyPcInstallation.aspx?Id=4"
            return false;           
       }
       else if (id == (ctextFront +  "05" + ctextBack))
       {         
            window.location.href="TAUP_AgencyMiscInstallation.aspx?Id=5"
            return false;           
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {         
            window.location.href="TAUP_AgencyProducts.aspx?Id=6"
            return false;           
       }
       else if (id == (ctextFront +  "07" + ctextBack))
       {         
            window.location.href="TAUP_AgencyOrder.aspx?Id=7&Mode=F"
            return false;           
       }
       else if (id == (ctextFront +  "08" + ctextBack))
       {         
            window.location.href="TAUP_AgencyNotes.aspx?Id=8"
            return false;           
       }
       else if (id == (ctextFront +  "09" + ctextBack))
       {         
            window.location.href="TASR_Training.aspx?Id=9"
            return false;           
       }
       else if (id == (ctextFront +  "10" + ctextBack))
       {         
            window.location.href="TAUP_FeedBack.aspx?Id=10"
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
