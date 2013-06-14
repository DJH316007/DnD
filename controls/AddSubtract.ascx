<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddSubtract.ascx.cs" Inherits="controls_AddSubtract" %>

<div style="float:left; margin:1px 5px 1px 1px;">
    <div style="float:left" >
        <div><asp:Button ID="Add_tb" runat="server" CssClass="Add" OnClick="Add_Click" /></div>
        <div><asp:Button ID="Subtract_tb" runat="server" CssClass="Subtract" OnClick="Subtract_Click" /></div>
    </div>
    <div style="float:left" >
        <asp:TextBox ID="Value_tb" runat="server" CssClass="AddSub_tb" />
    </div>
</div>