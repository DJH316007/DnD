<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Skills_Pathfinder.ascx.cs" Inherits="controls_Skills" %>
    <%@ Register Src="~/controls/AddSubtract.ascx" TagName="AddSub" TagPrefix="uc"%>
    <link rel="stylesheet" href="./css/Skills.css" type="text/css" />



<asp:Repeater id="SkillList" runat="server" OnItemDataBound="Skills_IDB" >
    <HeaderTemplate>
        <div class="Blank">
            <%= TotalRanks()%>/<%= DnDSession.RanksTotal%> skill ranks have been used
            <asp:ImageButton ID="SaveSkills" runat="server" ImageUrl="~/images/SkillSave.jpg" OnClick="SaveAllSkills" />
        </div>
        <div class="Blank" style="vertical-align:bottom;">
            <div style="float:left; width:35px; text-align:center; font-size:65%;">Class Skill</div>
            <div style="float:left; width:175px; text-align:center; font-size:145%;">Skill Name</div>
            <div style="float:left; width:38px; text-align:center; font-size:80%;">Total</div>
            <div style="float:left; width:38px; text-align:center; font-size:80%;">Ranks</div>
            <div style="float:left; width:38px; text-align:center; font-size:80%;">Misc</div>
            <div class="clear"></div>
        </div>
    </HeaderTemplate>                
    <ItemTemplate>
        <asp:UpdatePanel ID="Skill" runat="server" UpdateMode="Conditional" OnUnload="SaveSkill">
            <ContentTemplate>
                <div class=<%#Eval("Ability")%> " style="padding-left:8px">
                    <div style="float:left" >
                        <asp:HiddenField ID="SkillID" runat="server" Value=<%#Eval("ID")%> Visible=false />
                        <asp:CheckBox ID="Skilled_cb" runat="server" Checked=<%#Eval("ClassSkill")%> Width="15px" Enabled="false" />
                        <asp:TextBox ID="SkillName_tb" runat="server" CssClass="SkillsL_tb" Text=<%#Eval("SkillName")%> ReadOnly="true" />
                        <asp:TextBox ID="SkillTotal_tb" runat="server" CssClass="SkillsSInactive_tb" ReadOnly="true" />
                    </div>

                        <!-- -->
                    <div style="float:left" >
                        <div><asp:Button ID="AddRank1" runat="server" CssClass="Add" OnClick="AddRank" /></div>
                        <div><asp:Button ID="SubtractRank1" runat="server" CssClass="Subtract" OnClick="SubtractRank" /></div>
                    </div>
                    <div style="float:left" >
                        <asp:TextBox ID="SkillRanks_tb" runat="server" CssClass="SkillsSActive_tb" Text=<%#Eval("Ranks")%> ReadOnly="true" />
                    </div>     
                    <div style="float:left" >
                        <div><asp:Button ID="AddMisc1" runat="server" CssClass="Add" OnClick="AddMisc" /></div>
                        <div><asp:Button ID="SubtractMisc1" runat="server" CssClass="Subtract" OnClick="SubtractMisc" /></div>
                    </div>
                    <div style="float:left" >   
                        <asp:TextBox ID="SkillMisc_tb" runat="server" CssClass="SkillsSActive_tb" Text=<%#Eval("Misc")%> ReadOnly="true" />

                    </div>
                    <!-- -->
                    <!--
                    <uc:AddSub ID="SkillRanks" runat="server" OnInit="RankIt" />
                    <uc:AddSub ID="SkillMisc" runat="server" OnInit="RankIt" />


                    <asp:TextBox ID="SkillACPenMulti_tb" runat="server" CssClass="SkillsInactive_tbS" Text=<%#Eval("ACPenaltyMultiplier")%> />
                    <asp:TextBox ID="FFFF" runat="server" CssClass="SkillsInactive_tbS" />
                    -->
                    <div class="clear"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ItemTemplate>
</asp:Repeater>
    
    
<div style="font-size:70%;">Class Skill / Name / Ranks / Misc / AC Penalty Multi / Ability / Untrained</div>
<div>
    <asp:CheckBox ID="NewSkilled_cb" runat="server" />
    <asp:TextBox ID="NewSkillName_tb" runat="server" CssClass="InactiveTextBoxLg" />      
    <asp:TextBox ID="NewSkillRanks_tb" runat="server" CssClass="ActiveTextBoxSm" Width="25px" />
    <asp:TextBox ID="NewSkillMisc_tb" runat="server" CssClass="InactiveTextBoxSm" Width="25px" />
    <asp:TextBox ID="NewSkillACPenMulti_tb" runat="server" CssClass="InactiveTextBoxSm" Width="25px" />
    <asp:DropDownList ID="NewSkillAbility_dd" runat="server" />
    <asp:CheckBox ID="NewSkillUntrained_cb" runat="server" />
</div>
<asp:Button ID="AddNewSkill" runat="server" Text="Add New Skill" OnClick="NewSkill" />