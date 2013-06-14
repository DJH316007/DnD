<%@ Page Language="C#" MasterPageFile="~/Stats.master" AutoEventWireup="true" CodeFile="StatsPathfinder.aspx.cs" Inherits="Stats_Pathfinder" Title="Untitled Page" %>


<asp:Content ID="Content2" ContentPlaceHolderID="GeneralInfo" Runat="Server">
    <div class="Neutral">
        <div class="StatName" style="font-size:75%; color:#FFFFFF;">Combat Maneuver Defense</div>
        <asp:TextBox ID="CMD_tb" runat="server" CssClass="Stat_tb" ReadOnly="true" />
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Skills" Runat="Server">
</asp:Content>

