<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Skills_Basic.ascx.cs" Inherits="controls_SkillsBasic" %>

    <asp:UpdatePanel ID="SkillsUpdate" runat="server" ><ContentTemplate>
        <asp:Repeater id="Skills" runat="server" EnableViewState="False" OnItemDataBound="Skills_IDB" >
            <HeaderTemplate>

            </HeaderTemplate>                
            <ItemTemplate>
                <div class=<%#Eval("Ability")%> " style="border:1px #666666 solid; width:270px; height:22px;"><div class="SkillRow">   
                    <div style="float:left" >
                        <asp:CheckBox ID="Skilled_cb" runat="server" Checked=<%#Eval("ClassSkill")%> Width="30px" Enabled="false" />
                        <asp:TextBox ID="SkillName_tb" runat="server" CssClass="SkillsInactive_tbL" Text=<%#Eval("SkillName")%> Width="175px" ReadOnly=true />
                        <asp:TextBox ID="SkillTotal_tb" runat="server" CssClass="SkillsInactive_tbS" Width="30px" ReadOnly=true />
                    </div>
                </div></div>
            </ItemTemplate>
        </asp:Repeater>
    </ContentTemplate></asp:UpdatePanel>      