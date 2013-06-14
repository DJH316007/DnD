<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Skills.ascx.cs" Inherits="controls_Skills" %>
    <link rel="stylesheet" href="./css/Skills.css" type="text/css" />
    
    Your total rank number is <asp:TextBox ID="Rank_tb" runat="server" />
    <asp:Repeater id="Skills" runat="server" EnableViewState="False" OnItemDataBound="Skills_IDB" >
        <HeaderTemplate>
            <asp:ImageButton ID="SaveSkills" runat="server" ImageUrl="~/images/SkillSave.jpg" OnClick="SaveAllSkills" />
            <asp:Image ID="Image7" runat=server ImageUrl="~/images/SkillHeader.jpg" />
        </HeaderTemplate>                
        <ItemTemplate>
            <div class=<%#Eval("Ability")%> " style="border:1px #666666 solid; height:22px;"><div class="SkillRow">
                <asp:HiddenField ID="SkillID" runat="server" Visible=false />
                <asp:CheckBox ID="Skilled_cb" runat="server" Checked=<%#Eval("ClassSkill")%> Width="30px" />
                <asp:TextBox ID="SkillName_tb" runat="server" CssClass="SkillsInactive_tbL" Text=<%#Eval("SkillName")%> Width="175px" ReadOnly=true />
                <asp:TextBox ID="SkillTotal_tb" runat="server" CssClass="SkillsInactive_tbS" ReadOnly=true />
                <asp:TextBox ID="SkillRanks_tb" runat="server" CssClass="SkillsActive_tbS" Text=<%#Eval("Ranks")%> />
                <asp:TextBox ID="SkillMisc_tb" runat="server" CssClass="SkillsActive_tbS" Text=<%#Eval("Misc")%> />
                <asp:TextBox ID="SkillACPenMulti_tb" runat="server" CssClass="SkillsInactive_tbS" Text=<%#Eval("ACPenaltyMultiplier")%> />
            </div></div>
        </ItemTemplate>
    </asp:Repeater>
          
    <asp:Button ID="AddNewSkill" runat="server" Text="Add New Skill" OnClick="NewSkill" />
    <div>Class Skill / Name / Ranks / Misc / AC Penalty Multi / Ability / Untrained</div>
    <asp:CheckBox ID="NewSkilled_cb" runat="server" />
    <asp:TextBox ID="NewSkillName_tb" runat="server" CssClass="InactiveTextBoxLg" />      
    <asp:TextBox ID="NewSkillRanks_tb" runat="server" CssClass="ActiveTextBoxSm" Width="25px" />
    <asp:TextBox ID="NewSkillMisc_tb" runat="server" CssClass="InactiveTextBoxSm" Width="25px" />
    <asp:TextBox ID="NewSkillACPenMulti_tb" runat="server" CssClass="InactiveTextBoxSm" Width="25px" />
    <asp:DropDownList ID="NewSkillAbility_dd" runat="server" />
    <asp:CheckBox ID="NewSkillUntrained_cb" runat="server" /> 