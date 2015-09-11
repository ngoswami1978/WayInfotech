<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_helpDeskFeedBack_new.aspx.vb" Inherits="HelpDesk_HDUP_helpDeskFeedBack_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::HelpDesk::Manage FeedBack</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    //   alert("hi1")
      
   function ColorMethodHelpDeskFeedBackNew(id,total,itemIndex)
{   
try{
     debugger;
        alert("hi")
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
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
      
       document.getElementById('lblPanelClick').value =id; 


       document.getElementById("hdTabType").value=itemIndex
   
     
       if (id == (ctextFront +  "00" + ctextBack))
       {   
         
//           window.location.href="HelpDesk_HDUP_helpDeskFeedBack_new?Action=U&TabType=0&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;    
            window.location.href="HDUP_helpDeskFeedBack_new.aspx";
           return false;
         
     
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
         
           //window.location.href="HelpDesk_HDUP_helpDeskFeedBack_new?Action=U&TabType=1&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + EnStatus + "&FeedBackId="+ FeedBackId;                
        window.location.href="HDUP_helpDeskFeedBack_new.aspx";
          return false;
                  
       }
}catch(err){alert(err)}     
                            
       
}
    function ValidatHelpDeskFeedback()
{

    
    if (document.getElementById('grdvFeedback')!=null)
    {
        
                
        for(intcnt=1;intcnt<=document.getElementById('grdvFeedback').rows.length-1;intcnt++)
        {        
           if(document.getElementById('grdvFeedback').rows[intcnt].cells[2].children[0].selectedIndex=='0')
           {
           document.getElementById('grdvFeedback').rows[intcnt].cells[2].children[0].focus();
            document.getElementById("lblError").innerHTML="Questions Status is Mandatory";
            return false;
           }
       }
   }
   else
   {
            document.getElementById("lblError").innerHTML="No Question Defined";
            return false;
   }

    var intCount=0;
    //for HelpDEsk
   try
   {
   var strSuggestionHelpDesk=document.getElementById("txtSuggestionHelpDesk").value;
   var strdSuggestionHelpDesk=document.getElementById("ddlSuggestionHelpDesk").value;
   if (strSuggestionHelpDesk!="" && strdSuggestionHelpDesk=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionHelpDesk").disabled==false)
       {
         document.getElementById("ddlSuggestionHelpDesk").focus();
       }
		return false; 
   }
   if (strSuggestionHelpDesk=="" && strdSuggestionHelpDesk!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for helpdesk is Mandatory ";
       document.getElementById("txtSuggestionHelpDesk").focus();
		return false; 
   }
   if (strSuggestionHelpDesk!="" && strdSuggestionHelpDesk!="" )
   {
   intCount=1;
   }	
      
   }
   catch(err)
   {
   }
   
   //for Technical
   try
   {
   var strSuggestionTechnical=document.getElementById("txtSuggestionTechnical").value;
   var strdSuggestionTechnical=document.getElementById("ddlSuggestionTechnical").value;
   if (strSuggestionTechnical!="" && strdSuggestionTechnical=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionTechnical").disabled==false)
       {
        document.getElementById("ddlSuggestionTechnical").focus();
       }
		return false; 
   }
   if (strSuggestionTechnical=="" && strdSuggestionTechnical!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for technical is Mandatory ";
       document.getElementById("txtSuggestionTechnical").focus();
		return false; 
   }
   if (strSuggestionTechnical!="" && strdSuggestionTechnical!="" )
   {
   intCount=1;
   }	    
   }
   catch(err)
   {
   }
   
    //for Sales
   try
   {
   var strSuggestionSales=document.getElementById("txtSuggestionSales").value;
   var strdSuggestionSales=document.getElementById("ddlSuggestionSales").value;
   if (strSuggestionSales!="" && strdSuggestionSales=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionSales").disabled==false)
       {
         document.getElementById("ddlSuggestionSales").focus();
       }
		return false; 
   }
   if (strSuggestionSales=="" && strdSuggestionSales!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for sales is Mandatory ";
       document.getElementById("txtSuggestionSales").focus();
		return false; 
   }
   if (strSuggestionSales!="" && strdSuggestionSales!="" )
   {
   intCount=1;
   }	    
   }
   catch(err)
   {
   }
   
   try
   {
   var strSuggestionTraining=document.getElementById("txtSuggestionTraining").value;
   var strdSuggestionTraining=document.getElementById("ddlSuggestionTraining").value;
   if (strSuggestionTraining!="" && strdSuggestionTraining=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionTraining").disabled==false)
       {
            document.getElementById("ddlSuggestionTraining").focus();
       }
		return false; 
   }
   if (strSuggestionTraining=="" && strdSuggestionTraining!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for training is Mandatory ";
       document.getElementById("txtSuggestionTraining").focus();
		return false; 
   }
   if (strSuggestionTraining!="" && strdSuggestionTraining!="" )
   {
   intCount=1;
   }	    
   }
   catch(err)
   {
   }
   
   try
   {
   var strSuggestionProduct=document.getElementById("txtSuggestionProduct").value;
   var strdSuggestionProduct=document.getElementById("ddlSuggestionProduct").value;
   if (strSuggestionProduct!="" && strdSuggestionProduct=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionProduct").disabled==false)
       {
             document.getElementById("ddlSuggestionProduct").focus();
             
        }
		return false; 
   }
   if (strSuggestionProduct=="" && strdSuggestionProduct!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for product is Mandatory ";
       document.getElementById("ddlSuggestionProduct").focus();
		return false; 
   }
   if (strSuggestionProduct!="" && strdSuggestionProduct!="" )
   {
   intCount=1;
   }	    
   }
   catch(err)
   {
   }
   
   
   //////////////////
       //for CustomerFeedback
   try
   {
   var strSuggestionCustomerFeedback=document.getElementById("txtSuggestionCustomerFeedback").value;
   var strdSuggestionCustomerFeedback=document.getElementById("ddlSuggestionCustomerFeedback").value;
   if (strSuggestionCustomerFeedback!="" && strdSuggestionCustomerFeedback=="" )
   {    
       document.getElementById("lblError").innerText = "Assinged to is Mandatory ";
       if(document.getElementById("ddlSuggestionCustomerFeedback").disabled==false)
       {
         document.getElementById("ddlSuggestionCustomerFeedback").focus();
       }
		return false; 
   }
   if (strSuggestionCustomerFeedback=="" && strdSuggestionCustomerFeedback!="" )
   {    
       document.getElementById("lblError").innerText = "Suggestion for Customer Feedback is Mandatory ";
       document.getElementById("txtSuggestionCustomerFeedback").focus();
		return false; 
   }
   if (strSuggestionCustomerFeedback!="" && strdSuggestionCustomerFeedback!="" )
   {
   intCount=1;
   }	
      
   }
   catch(err)
   {
   }

   
   ////////////////////////////////////
   //for Helpdesk
   try
   {
   var struSuggestionHelpDesk=document.getElementById("txtHelpDeskAction").value;
   var strudSuggestionHelpDesk=document.getElementById("ddlHelpDeskAction").value;
   
   if (struSuggestionHelpDesk=="" && strudSuggestionHelpDesk=="2" )
   {    
       document.getElementById("lblError").innerText = "Action for helpdesk is Mandatory ";
       
       document.getElementById("txtHelpDeskAction").focus();
		return false; 
   }
   
   if (strudSuggestionHelpDesk=="" )
   {    
      document.getElementById("lblError").innerText = "Status for helpdesk is Mandatory ";
       document.getElementById("ddlHelpDeskAction").focus();
		return false; 
   }
   
   
   
        
   }
   catch(err)
   {
   }
   
   //for Technical
   try
   {
   var struTechnicalHelpDesk=document.getElementById("txtTechnicalHelpDeskAction").value;
   var strudTechnicalHelpDesk=document.getElementById("ddlTechnicalHelpDeskAction").value;
   if (struTechnicalHelpDesk=="" && strudTechnicalHelpDesk=="2" )
   {    
       document.getElementById("lblError").innerText = "Action for technical is Mandatory ";
       
       document.getElementById("txtTechnicalHelpDeskAction").focus();
		return false; 
   }
   if (strudTechnicalHelpDesk=="" )
   {    
      document.getElementById("lblError").innerText = "Status for technical is Mandatory ";
       document.getElementById("ddlTechnicalHelpDeskAction").focus();
		return false; 
   }
        
   }
   catch(err)
   {
   }
   
    //for Sales
   try
   {
   var struSalesHelpDesk=document.getElementById("txtSalesHelpDeskAction").value;
   var strudSalesHelpDesk=document.getElementById("ddlSalesHelpDeskAction").value;
   if (struSalesHelpDesk=="" && strudSalesHelpDesk=="2" )
   {    
       document.getElementById("lblError").innerText = "Action for sales is Mandatory ";
       
       document.getElementById("txtSalesHelpDeskAction").focus();
		return false; 
   }
   if (strudSalesHelpDesk=="" )
   {    
      document.getElementById("lblError").innerText = "Status for sales is Mandatory ";
       document.getElementById("ddlSalesHelpDeskAction").focus();
		return false; 
   }
        
   }
   catch(err)
   {
   }
   //for training
   try
   {
   var struTrainingHelpDesk=document.getElementById("txtTrainingHelpDeskAction").value;
   var strudTrainingHelpDesk=document.getElementById("ddlTrainingHelpDeskAction").value;
   if (struTrainingHelpDesk=="" && strudTrainingHelpDesk=="2")
   {    
       document.getElementById("lblError").innerText = "Action for training is Mandatory ";
       
       document.getElementById("txtTrainingHelpDeskAction").focus();
		return false; 
   }
   if (strudTrainingHelpDesk=="" )
   {    
      document.getElementById("lblError").innerText = "Status for training is Mandatory ";
       document.getElementById("ddlTrainingHelpDeskAction").focus();
		return false; 
   }
        
   }
   catch(err)
   {
   }
   // for product
   try
   {
   var struProductHelpDesk=document.getElementById("txtProductHelpDeskAction").value;
   var strudProductHelpDesk=document.getElementById("ddlProductHelpDeskAction").value;
   if (struProductHelpDesk=="" && strudProductHelpDesk=="2")
   {    
       document.getElementById("lblError").innerText = "Action for product is Mandatory ";
       
       document.getElementById("txtProductHelpDeskAction").focus();
		return false; 
   }
   if (strudProductHelpDesk=="" )
   {    
      document.getElementById("lblError").innerText = "Status for product is Mandatory ";
       document.getElementById("ddlProductHelpDeskAction").focus();
		return false; 
   }
        
   }
   catch(err)
   {
   }
  
   
//for Customer Feedback
   try
   {
	   var struCustomerFeedback=document.getElementById("txtCustomerFeedbackAction").value;
	   var strudCustomerFeedback=document.getElementById("ddlCustomerFeedbackAction").value;
	   if (struCustomerFeedback=="" && strudCustomerFeedback=="2")
		   {    
	       document.getElementById("lblError").innerText = "Action for Customer Feedback is Mandatory ";
       
       		document.getElementById("txtCustomerFeedbackAction").focus();
		return false; 
		   }
	   if (strudCustomerFeedback=="" )
		{    
	      document.getElementById("lblError").innerText = "Status for Customer Feedback is Mandatory ";
	      document.getElementById("ddlCustomerFeedbackAction").focus();
	      return false; 
		 }
        
     }
   catch(err)
   {
   }
   
   
}


    	 function PopupPage(id)
         {
        
             var type;
             if (id=="1")
             {
                    type = "../TravelAgency/TAUP_FeedBack.aspx?Popup=T&LCode="+document.getElementById("hdEnPageLCode").value;
   	                window.open(type,"aaCallLogAgencyFeedBack","height=600,width=900,top=30,left=20,scrollbars=1,status=1");            
              }	
              return false;
          }
          function fnClose()
          {
          try
          {
            if (window.opener.document.forms['form1']['hdEnFeedBackId']!=null)
            { 
                   window.opener.document.forms['form1']['hdEnFeedBackId'].value=document.getElementById("hdEnFeedBackId").value;
                   window.opener.document.forms['form1']['hdFeedBackId'].value=document.getElementById("hdFeedBackId").value;
            }
          }
          catch(err){}
            window.close();
            return false;
          }
          
          function FillValue()
          {
          try
          {
            if (window.opener.document.forms['form1']['hdEnFeedBackId']!=null)
            { 
                   window.opener.document.forms['form1']['hdEnFeedBackId'].value=document.getElementById("hdEnFeedBackId").value;
                   window.opener.document.forms['form1']['hdFeedBackId'].value=document.getElementById("hdFeedBackId").value;
            }
          }
          catch(err){}
                
          }
          function f12()
          {
          debugger;
          var c;
          }
    </script>
    
</head>
<body onunload="FillValue();" onload="f12()">
    <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">HelpDesk-> FeedBack</span></td>
                                            <td style="width:20%" class="right">
                                            <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnClose()" >Close</asp:LinkButton>
                                             &nbsp; &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage FeedBack</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 10px">
                                            <asp:Repeater ID="theTabStrip" runat="server" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" >
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:100%;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="1">
                                                                        <tr>
                                                                            <td class="subheading" colspan="4">
                                                                                Question Details</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:GridView ID="grdvFeedback" runat="server" AlternatingRowStyle-CssClass="lightblue"
                                                                                    AutoGenerateColumns="False" HeaderStyle-CssClass="Gridheading" HorizontalAlign="Center"
                                                                                    RowStyle-CssClass="ItemColor" TabIndex="8" Width="99%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="QuestionID">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblQuestionID" runat="server" ></asp:Label>
                                                                                                
                                                                                                <asp:HiddenField ID="hdQuestionID" runat="server"  Value='<%# Eval("QUESTION_ID") %>'/>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Questions">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblQuest" runat="server" Text='<%# Eval("QUESTION_TITLE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="600px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FeedBack">
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpQuestStatus" runat="server" CssClass="dropdownlist">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr >
                                                                            <td class="subheading" colspan="4">
                                                                                Suggestions</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionHelpDesk" runat="server">
                                                                            <td class="textbold" style="width: 2%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 40%">
                                                                                Suggestion for HelpDesk</td>
                                                                            <td class="textbold" style="width: 30%">
                                                                                </td>
                                                                            <td class="textbold" >
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionHelpDesk1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" colspan="2">
                                                                                <asp:TextBox ID="txtSuggestionHelpDesk" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'1000')" ></asp:TextBox></td>
                                                                            <td class="top">
                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionHelpDesk" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionHelpDesk" runat="server" Text="  Critical" Width="82px" /></td>
                                                                        </tr>
                                                                        <tr runat="server" id="trHelpDeskHelpDeskAction">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Action Taken <span class="Mandatory" >*</span>
                                                                            </td>
                                                                            <td class="textbold top"></td>
                                                                            <td class="textbold top">
                                                                                Status <span class="Mandatory" >*</span></td>
                                                                        </tr>
                                                                        <tr runat="server" id="trHelpDeskHelpDeskAction1">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtHelpDeskAction" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'5000')"></asp:TextBox></td>
                                                                            <td class="top">
                                                                            <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlHelpDeskAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                            </asp:DropDownList></td>
                                                                        </tr>
                                                                        
                                                                           <tr id="trSuggestionTechnical" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 40%">
                                                                                Suggestion for Technical</td>
                                                                            <td style="width: 25%">
                                                                                </td>
                                                                            <td class="textbold">
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionTechnical1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtSuggestionTechnical" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'1000')"></asp:TextBox></td>
                                                                            <td class="textbold top">
                                                                                &nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionTechnical" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList><br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionTechnical" runat="server" Text="  Critical" /></td>
                                                                        </tr>
                                                                         <tr id="trTechnicalHelpDeskAction" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Action Taken <span class="Mandatory" >*</span></td>
                                                                            <td class="textbold top"><span class="Mandatory" ></span></td>
                                                                            <td class="textbold top">
                                                                                Status <span class="Mandatory" >*</span></td>
                                                                        </tr>
                                                                        <tr id="trTechnicalHelpDeskAction1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtTechnicalHelpDeskAction" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'5000')"></asp:TextBox></td>
                                                                            <td class="top">
                                                                            <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlTechnicalHelpDeskAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                            </asp:DropDownList></td>
                                                                        </tr>
                                                                           <tr id="trSuggestionSales" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 40%">
                                                                                Suggestion for Sales</td>
                                                                            <td  style="width: 25%">
                                                                                </td>
                                                                            <td class="textbold">
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionSales1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtSuggestionSales" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'1000')"></asp:TextBox></td>
                                                                            <td class="textbold top">
                                                                                &nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionSales" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionSales" runat="server" Text="  Critical" /></td>
                                                                        </tr>
                                                                         <tr id="trSalesHelpDeskAction" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Action Taken <span class="Mandatory" >*</span></td>
                                                                            <td class="textbold top"><span class="Mandatory" ></span></td>
                                                                            <td class="textbold top">
                                                                                Status <span class="Mandatory" >*</span></td>
                                                                        </tr>
                                                                        <tr id="trSalesHelpDeskAction1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtSalesHelpDeskAction" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'5000')"></asp:TextBox></td>
                                                                            <td class="top">
                                                                            <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSalesHelpDeskAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                            </asp:DropDownList></td>
                                                                        </tr>
                                                                           <tr id="trSuggestionTraining" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Suggestion for Training</td>
                                                                            <td  style="width: 25%">
                                                                                </td>
                                                                            <td class="textbold">
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionTraining1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtSuggestionTraining" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'1000')"></asp:TextBox></td>
                                                                            <td class="textbold top">
                                                                                &nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionTraining" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionTraining" runat="server" Text="  Critical" /></td>
                                                                        </tr>
                                                                         <tr id="trTrainingHelpDeskAction" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Action Taken <span class="Mandatory" >*</span></td>
                                                                            <td class="textbold top"><span class="Mandatory" ></span></td>
                                                                            <td class="textbold top">
                                                                                Status <span class="Mandatory" >*</span></td>
                                                                        </tr>
                                                                        <tr id="trTrainingHelpDeskAction1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtTrainingHelpDeskAction" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'5000')"></asp:TextBox></td>
                                                                            <td class="top">
                                                                            <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlTrainingHelpDeskAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                            </asp:DropDownList></td>
                                                                        </tr>
                                                                           <tr id="trSuggestionProduct" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Suggestion for Product</td>
                                                                            <td style="width: 25%">
                                                                                </td>
                                                                            <td  class="textbold">
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionProduct1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtSuggestionProduct" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'1000')"></asp:TextBox></td>
                                                                            <td class="textbold top">
                                                                                &nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionProduct" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionProduct" runat="server" Text="  Critical" /></td>
                                                                        </tr>
                                                                 
                                                                <tr id="trProductHelpDeskAction" runat="server">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Action Taken <span class="Mandatory" >*</span></td>
                                                                    <td ></td>
                                                                    <td class="textbold">
                                                                        Status <span class="Mandatory" >*</span></td>
                                                                </tr>
                                                                        <tr id="trProductHelpDeskAction1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtProductHelpDeskAction" runat="server" Height="70px" TextMode="MultiLine" Width="525px" onkeyup="checkMaxLength(this.id,'5000')"></asp:TextBox></td>
                                                                            <td class="top">
                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlProductHelpDeskAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr id="trSuggestionCustomerFeedback" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                Suggestion for Corporate Communication</td>
                                                                            <td class="top">
                                                                                Assigned To</td>
                                                                        </tr>
                                                                        <tr id="trSuggestionCustomerFeedback1" runat="server">
                                                                            <td class="textbold" style="height: 19px">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2" style="height: 19px">
                                                                                <asp:TextBox ID="txtSuggestionCustomerFeedback" runat="server" Height="70px" onkeyup="checkMaxLength(this.id,'1000')"
                                                                                    TextMode="MultiLine" Width="525px"></asp:TextBox></td>
                                                                            <td class="top" style="height: 19px">
                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSuggestionCustomerFeedback" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList><br />
                                                                                <br />
                                                                                <br />
                                                                                <asp:CheckBox ID="chkSuggestionCustomerFeedback" runat="server" Text="  Critical" /></td>
                                                                        </tr>
                                                                        <tr id="trCustomerFeedbackAction" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                Action Taken <span class="Mandatory" >*</span></td>
                                                                            <td class="top">
                                                                                Status <span class="Mandatory" >*</span></td>
                                                                        </tr>
                                                                        <tr id="trCustomerFeedbackAction1" runat="server">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold top" colspan="2">
                                                                                <asp:TextBox ID="txtCustomerFeedbackAction" runat="server" Height="70px" onkeyup="checkMaxLength(this.id,'5000')"
                                                                                    TextMode="MultiLine" Width="525px"></asp:TextBox></td>
                                                                            <td class="top">
                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlCustomerFeedbackAction" runat="server" CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="subheading" colspan="4" style="width: 100%">
                                                                                Others Details</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                FeedBack Number</td>
                                                                            <td class="textbold">
                                                                                DateTime</td>
                                                                            <td>
                                                                                Logged By</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                <asp:TextBox ID="txtFeedBackNo" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                                    ReadOnly="True" TabIndex="9" Width="178px"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                                <asp:TextBox ID="txtFeedbkDt" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                                    ReadOnly="True" TabIndex="10" Width="161px"></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLogedByName" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                                    ReadOnly="True" TabIndex="11" Width="178px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Caller Name</td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                <asp:TextBox ID="txtExecutiveName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="9"
                                                                                    Width="178px"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Remarks</td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" colspan="2">
                                                                                <asp:TextBox ID="txtRemarks" runat="server" Height="70px" TextMode="MultiLine" Width="525px"  onkeyup="checkMaxLength(this.id,'1000')"></asp:TextBox></td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" colspan="2">
                                                                            <input id="hdPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdQueryString" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />                                                                                                                                              
                                                                        <input id="hdOfficeID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" />                                                                      
                                                                        <input id="hdSaveStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdTabType" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEmpID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdLogedByName" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                          <input id="hdAoffice" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdInsertStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        
                                                                        
                                                                        <input id="hdEnPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnFeedBackId" runat="server" style="width: 1px" type="hidden" />
                                                                          <input id="hdEnPageHD_RE_ID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnPageLCode" runat="server" style="width: 1px" type="hidden" />
                                                                          <input id="hdEnAOffice" runat="server" style="width: 1px" type="hidden" />
       
       
       
       
                                                                              
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                   
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="110px" AccessKey="s"   /><br />
                                                                        <asp:Button ID="btnHistory" runat="server" TabIndex="3" CssClass="button topMargin" Text="FeedBack History" Width="110px" OnClientClick="return PopupPage(1)"   /><br />
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="110px" AccessKey="r"  />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="center" colspan="2" rowspan="1">
                                                                    </td>
                                                                </tr>
                                                             
                                                              
                                                                
                                                                
                                                                <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>                     &nbsp; &nbsp;                     </td>
                                                                    <td>
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
                    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
