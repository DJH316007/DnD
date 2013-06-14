<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSpell.aspx.cs" Inherits="admin_EnterSpell" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
    <body>
        <form id="form1" runat="server">
                Name<asp:TextBox ID="SpellName" runat=server CssClass="SpellName" />
                School<asp:TextBox ID="SpellSchool" runat=server CssClass="SpellSchool" />
                Comp<asp:TextBox ID="SpellComp" runat=server CssClass="SpellComp" />
                Time<asp:TextBox ID="SpellTime" runat=server CssClass="SpellTime" />
                Range<asp:TextBox ID="SpellRange" runat=server CssClass="SpellRange" />
                Duration<asp:TextBox ID="SpellDuration" runat=server CssClass="SpellDuration" />
                <div> </div>
                Target<asp:TextBox ID="SpellTarget" runat=server CssClass="SpellSaving" />
                Area<asp:TextBox ID="SpellArea" runat=server CssClass="SpellRes" />
                Saving<asp:TextBox ID="SpellSaving" runat=server CssClass="SpellSaving" />
                Resist<asp:TextBox ID="SpellResist" runat=server CssClass="SpellRes" />
                Materials<asp:TextBox ID="SpellMaterials" runat=server CssClass="SpellSaving" />
                Focus<asp:TextBox ID="SpellFocus" runat=server CssClass="SpellSaving" />                
                <div>Description<asp:TextBox ID="SpellDescription" runat=server Width="1185px" Height="100px" TextMode=MultiLine/></div>
            
            <div>
                <div style="float:left">
                    Bard<div><asp:TextBox ID="BardLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Cleric</div>
                    <div><asp:TextBox ID="ClericLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Druid</div>
                    <div><asp:TextBox ID="DruidLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Paladin</div>
                    <div><asp:TextBox ID="PaladinLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Ranger</div>
                    <div><asp:TextBox ID="RangerLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Sorcerer</div>
                    <div><asp:TextBox ID="SorcererLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Wizard</div>
                    <div><asp:TextBox ID="WizardLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Chaos</div>
                    <div><asp:TextBox ID="ChaosLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Good</div>
                    <div><asp:TextBox ID="GoodLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Law</div>
                    <div><asp:TextBox ID="LawLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Evil</div>
                    <div><asp:TextBox ID="EvilLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Air</div>
                    <div><asp:TextBox ID="AirLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Water</div>
                    <div><asp:TextBox ID="WaterLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Earth</div>
                    <div><asp:TextBox ID="EarthLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Fire</div>
                    <div><asp:TextBox ID="FireLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Animal</div>
                    <div><asp:TextBox ID="AnimalLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Death</div>
                    <div><asp:TextBox ID="DeathLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Destruction</div>
                    <div><asp:TextBox ID="DestructionLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Healing</div>
                    <div><asp:TextBox ID="HealingLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Knowledge</div>
                    <div><asp:TextBox ID="KnowledgeLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Luck</div>
                    <div><asp:TextBox ID="LuckLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Magic</div>
                    <div><asp:TextBox ID="MagicLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Plant</div>
                    <div><asp:TextBox ID="PlantLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Protection</div>
                    <div><asp:TextBox ID="ProtectionLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Strength</div>
                    <div><asp:TextBox ID="StrengthLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Sun</div>
                    <div><asp:TextBox ID="SunLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Travel</div>
                    <div><asp:TextBox ID="TravelLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>Trickery</div>
                    <div><asp:TextBox ID="TrickeryLevel" runat=server CssClass="SpellRes" /></div>
                </div>
                <div style="float:left">
                    <div>War</div>
                    <div><asp:TextBox ID="WarLevel" runat=server CssClass="SpellRes" /></div>
                </div>
            </div>
            <asp:Button ID="AddSpell" runat="server" Text="Add Spell"  OnClick="AddSpell_Click" />
            <div style="clear:both"></div>
            <asp:TextBox ID="FileEdition_tb" runat=server CssClass="SpellRes" /></div>
            <asp:Button ID="FileAdd" runat="server" Text="Add Spell From File"  OnClick="FileAdd_Click" />
        </form>
    </body>
</html>
