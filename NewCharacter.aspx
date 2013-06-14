<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewCharacter.aspx.cs" Inherits="NewCharacter" %>
<%@ Register Src="~/controls/Skills_Basic.ascx" TagName="Skills" TagPrefix="uc"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Character Sheet</title>
    <link rel="stylesheet" href="~/css/DnD.css" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptMgr" runat="server" />
        <div>
            <div>
                <asp:Button ID="BackToHome" runat="server" Text="Back to Character Select" PostBackUrl="~/Default.aspx" />
            </div>
            <div class="BlockLg">
                Name
                <asp:TextBox ID="nameText" runat=server CssClass="ActiveTextBoxLg" />
            </div>
            <div class="BlockSm">
                Level
                <asp:TextBox ID="levelText" runat=server CssClass="ActiveTextBoxSm" />
            </div>
            <div class="BlockLg">
                <div>Race</div>
                <asp:DropDownList ID="race_dd" runat="server" />           
            </div>
            <div class="BlockLg">
                <div>Class</div>
                <asp:DropDownList ID="class_dd" runat="server" OnTextChanged="ClassUpdated" /> 
            </div>
            <div class="BlockMd">
                Alignment
                <asp:TextBox ID="alignText" runat=server CssClass="ActiveTextBoxMd" />
            </div>
            <div class="BlockLg">
                <div>Size</div>
                <asp:DropDownList ID="sizeDD" runat="server" /> 
            </div>
            <div class="BlockSm">
                Age
                <asp:TextBox ID="ageText" runat=server CssClass="ActiveTextBoxSm" />
            </div>
            <div class="BlockMd">
                Sex
                <asp:TextBox ID="sexText" runat=server CssClass="ActiveTextBoxMd" />
            </div>
            <div class="BlockMd">
                Height
                <asp:TextBox ID="heightText" runat=server CssClass="ActiveTextBoxMd" />
            </div>
            <div class="BlockMd">
                Weight
                <asp:TextBox ID="weightText" runat=server CssClass="ActiveTextBoxMd" />
            </div>
            <div class="BlockLg">
                Deity
                <asp:TextBox ID="deityText" runat=server CssClass="ActiveTextBoxLg" />
            </div>
            <div style="clear:both"></div>
        </div>
        <div style="float:left;">
            <div class="LeftBlock">
                <div class="Blank"></div>
                <div style="clear:both"></div>
                <div class="Str">
                    <div class="VariableImage"><asp:Image ID="Image4" runat=server ImageUrl="images/Ability_Str.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Str_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div class="Dex">
                    <div class="VariableImage"><img id="Img1" runat="server" src="images/Ability_Dex.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Dex_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div class="Con">
                    <div class="VariableImage"><img id="Img2" runat="server" src="images/Ability_Con.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Con_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div class="Int">
                    <div class="VariableImage"><img id="Img3" runat="server" src="images/Ability_Int.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Int_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div class="Wis">
                    <div class="VariableImage"><img id="Img4" runat="server" src="images/Ability_Wis.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Wis_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div class="Cha">
                    <div class="VariableImage"><img id="Img5" runat="server" src="images/Ability_Cha.jpg" style="float:left" /></div>
                    <asp:TextBox ID="Cha_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div class="StackableBlock"></div>
                <div style="clear:both"></div>
            </div>

            <div class="LeftBlock">
                <div class="Blank"></div>
                <div>
                    <div class="VariableImage">HP</div>
                    <asp:TextBox ID="Hp_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div style="clear:both"></div>
                <div>
                    <div class="VariableImage">Natural Armor</div>
                    <asp:TextBox ID="NaturalAC_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div>
                    <div class="VariableImage">Dodge AC</div>
                    <asp:TextBox ID="DodgeAC_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div>
                    <div class="VariableImage">Deflect AC</div>
                    <asp:TextBox ID="DeflectAC_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
                <div>
                    <div class="VariableImage">Misc AC</div>
                    <asp:TextBox ID="MiscAC_tb" runat=server CssClass="ActiveTextBoxMd" />
                </div>
            </div>
            <div style="clear:both;"></div>
            <div>
                XP<asp:TextBox ID="Xp_tb" runat="server" CssClass="ActiveTextBoxLg" />
                PP<asp:TextBox ID="Platinum_tb" runat="server" CssClass="ActiveTextBoxMd" />
                GP<asp:TextBox ID="Gold_tb" runat="server" CssClass="ActiveTextBoxMd" />
                SP<asp:TextBox ID="Silver_tb" runat="server" CssClass="ActiveTextBoxMd" /> 
                CP<asp:TextBox ID="Copper_tb" runat="server" CssClass="ActiveTextBoxMd" />   
            </div>
            Notes:<asp:TextBox ID="Notes" runat="server" Width="550px" Height="200px" TextMode="MultiLine" />    
            
            <div>
                <asp:Button ID="AddNewCharacter" runat="server" Text="Add New Character" OnClick="AddCharacter_Click" />
            </div>
        </div>
        <div class="LeftBlock" style="width:400px">
            Total Ranks: <asp:TextBox ID="SkillRanks_tb" runat="server" CssClass="ActiveTextBoxMd" /> 
             <asp:Button ID="Button1" runat="server" Text="Update Class Ranks" />
            <uc:Skills ID="CharacterSkills" runat="server" />
        </div>
        <div style="clear:both;"></div>
    </form>
</body>
</html>
