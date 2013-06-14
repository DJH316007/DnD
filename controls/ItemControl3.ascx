<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemControl3.ascx.cs" Inherits="controls_EquipControl" %>

<div class="ItemBlock" style="float:left">
    <div>
        <div>
            <asp:CheckBox ID="EquipedCheck" runat="server" />
            <asp:Label ID="PartName" runat="server" Width="75px" CssClass="ItemButtonIcon"><%=GetBodyPartName()%></asp:Label>
            <asp:ImageButton ID="GeneralInfo" runat="server" ImageUrl="~/images/GeneralInfo.jpg" CssClass="ItemButtonIcon" OnClick="General_Click" />
            <asp:ImageButton ID="AttackInfo" runat="server" ImageUrl="~/images/Attack.jpg" CssClass="ItemButtonIcon" OnClick="Attack_Click" />
            <asp:ImageButton ID="DefenseInfo" runat="server" ImageUrl="~/images/Defense.jpg" CssClass="ItemButtonIcon" OnClick="Defense_Click" />
            <asp:ImageButton ID="Save" runat="server" ImageUrl="~/images/Save.jpg" CssClass="ItemButtonIcon" OnClick="Save_Click" />
            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/images/Delete.jpg" CssClass="ItemButtonIcon" OnClick="Delete_Click" /> 
            <asp:ImageButton ID="New" runat="server" ImageUrl="~/images/NewItem.jpg" CssClass="ItemButtonIcon" OnClick="New_Click" />
        </div>
        
        <div id="ItemImage">
            <asp:ImageButton ID="Prev" runat="server" ImageUrl="~/images/PrevItem.jpg" OnClick="PrevItem_Click" />
        </div>
        
        <div id="ItemImage">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/item.jpg" />
        </div>
        
        <div style="float:left">
            <asp:Panel id="General" runat="server" Visible="true"><div id="ItemInfoBlock">
                <div>Name<asp:TextBox ID="NameText" runat="server" CssClass="ItemL" /></div>
                <div>Notes<asp:TextBox ID="NotesText" runat="server" CssClass="ItemL" /></div>
                Type<asp:TextBox ID="ClassifiedText" runat="server" CssClass="ItemM" />
                Weight<asp:TextBox ID="WeightText" runat="server" CssClass="ItemS" />
                Arcane Failure<asp:TextBox ID="SpellFailureText" runat="server" CssClass="ItemS" />
            </div></asp:Panel>  
            <asp:Panel id="Attack" runat="server" Visible="false"><div id="ItemInfoBlock">
                <div>
                    <asp:CheckBox ID="StrDmg" runat="server" Text="Str Dmg" />
                    <asp:CheckBox ID="DexDmg" runat="server" Text="Dex Dmg" />
                    +? Item<asp:TextBox ID="Enhance" runat="server" CssClass="ItemM" />
                </div>
                <div>
                    Attack Bonus<asp:TextBox ID="AttackBonusText" runat="server" CssClass="ItemM" />
                    Damage<asp:TextBox ID="DamageText" runat="server" CssClass="ItemM" />
                    <asp:TextBox ID="ExtraDmg" runat="server" CssClass="ItemM" BackColor="#CCCCDD" />
                </div>
                <div>
                    Crit<asp:TextBox ID="CritText" runat="server" CssClass="ItemM" />
                    Range<asp:TextBox ID="RangeText" runat="server" CssClass="ItemM" />
                </div>
            </div></asp:Panel>      
            <asp:Panel id="Defense" runat="server" Visible="false"><div id="ItemInfoBlock">
                <div>
                    AC Bonus<asp:TextBox ID="ACBonusText" runat="server" CssClass="ItemM" />
                    AC Check<asp:TextBox ID="ACCheckText" runat="server" CssClass="ItemM" />
                </div>    
                <div>
                    Max Dex<asp:TextBox ID="MaxDexText" runat="server" CssClass="ItemM" />
                    Max Speed<asp:TextBox ID="MaxSpeedText" runat="server" CssClass="ItemM" />
                </div>
            </div></asp:Panel>
        </div>
        
        <div id="ItemImage">
            <asp:ImageButton ID="Next" runat="server" ImageUrl="~/images/NextItem.jpg" OnClick="NextItem_Click" />
        </div> 
    </div>

    <div style="clear:both"></div>     
</div>