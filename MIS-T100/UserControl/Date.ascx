﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Date.ascx.vb" Inherits="MIS_T100._Date" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<style type="text/css">
    .auto-style1 {
        width: 20px;
        height: 18px;
    }
</style>
<asp:TextBox ID="tbDate" runat="server" Width="100px" placeholder="__/__/____"  ></asp:TextBox>
<asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server"
    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
</asp:CalendarExtender>
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Images/calendar.gif" />
<asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd/MM/yyyy " 
    PopupButtonID="ImageButton1" TargetControlID="tbDate">
</asp:CalendarExtender>

