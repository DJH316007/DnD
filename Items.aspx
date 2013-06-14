<%@ Page Language="C#" MasterPageFile="~/DnD.master" AutoEventWireup="true" CodeFile="Items.aspx.cs" Inherits="Items" Title="Untitled Page" %>
<%@ Register Src="~/controls/ItemControl3.ascx" TagName="ItemControl3" TagPrefix="uc"%>


<asp:Content ID="Content1" ContentPlaceHolderID="TabSwitch" Runat="Server">
    <link rel="stylesheet" href="./css/Items.css" type="text/css" />

    <div>
        <div class="SideBlock">
            Melee Attack: <%=GetAttack("Melee")%><br />
            Ranged Attack: <%=GetAttack("Ranged")%><br />
            <asp:Repeater id="AllItems" runat="server" OnItemDataBound="TagButtons_IDB" EnableViewState="False" >
                <HeaderTemplate>
                    <asp:Button ID="SaveAllItems" runat="server" Text="Save Items" OnClick="SaveItems" />
                    <br />Name / lbs / Equiped / Detailed View / Delete  
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="ItemArea" runat="server" OnClick="TTT('9')">
                        <asp:TextBox ID="ItemName_tb" runat="server" CssClass="ItemL" Text=<%#Eval("ItemName")%> />
                        <asp:TextBox ID="Weight_tb" runat="server" CssClass="ItemS" Text=<%#Eval("Weight")%> />
                        <asp:CheckBox ID="Equiped_cb" runat="server" Checked=<%#Eval("Equiped")%> />
                       <!-- <asp:DropDownList ID="BodyPart_dd" runat="server" Width="75px" /> -->
                        <asp:Button ID="DetailedView" runat="server" Text="Detailed View" OnClick="ViewAsDetailed" />
                        <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/images/Delete.jpg" CssClass="ItemButtonIcon" OnClick="DeleteItemShort" /> 
                    </div>
                </ItemTemplate>
                <FooterTemplate><asp:Button ID="NewItem" runat="server" Text="Add New Item" OnClick="NewItem" /></FooterTemplate>
            </asp:Repeater>
        </div>
        <!--<div  class="CenterBlock"></div>-->
        <div class="SideBlock" style="text-align:left">
            <div>
                <asp:HiddenField ID="DetailedID_hf" runat="server" />
                <asp:CheckBox ID="DetailedEquiped_cb" runat="server" Text="Equip" />
                <!--<asp:CheckBox ID="DetailedProficient_cb" runat="server" Text="Proficient" />-->
                <asp:ImageButton ID="DetailedSave" runat="server" ImageUrl="~/images/Save.jpg" CssClass="ItemButtonIcon" OnClick="SaveItem" />
                <asp:ImageButton ID="DetailedDelete" runat="server" ImageUrl="~/images/Delete.jpg" CssClass="ItemButtonIcon" OnClick="DeleteItem" /> 
            </div>
            <div style="float:left">
                <asp:UpdatePanel ID="update" runat="server" ><ContentTemplate>
                    <div><p></p>GENERAL INFO</div>
                    <asp:Panel ID="GeneralView" runat="server" Visible=false >
                        <div id="GeneralView" class="InfoViewBlock">
                            <%= ViewGeneralText()%>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="EditGeneral" />
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="GeneralEdit" runat="server" Visible=false >
                        <div class="InfoEditBlock">
                        <!--BodyPart-->
                            <div>
                                Name<asp:TextBox ID="DetailedName_tb" runat="server" CssClass="ItemL" />
                                Weight<asp:TextBox ID="DetailedWeight_tb" runat="server" CssClass="ItemS" /> lbs
                            </div>
                            <div>
                                <asp:CheckBox  ID="DetailedMasterwork_cb" runat="server" Text="Masterwork" />
                                <!--<asp:CheckBox ID="DetailedMithral_cb" runat="server" Text="Mithral" />
                                <asp:CheckBox ID="DetailedAdamantine_cb" runat="server" Text="Adamantine" />-->
                                Magical Enhancement<asp:TextBox ID="DetailedEnchancement_tb" runat="server" CssClass="ItemS" />
                            </div>
                            <div>
                                <asp:CheckBox ID="DetailedWeapon_cb" runat="server" Text="Weapon Item" />
                                <asp:CheckBox ID="DetailedArmor_cb" runat="server" Text="Armor Item" />
                                <asp:CheckBox ID="DetailedShield_cb" runat="server" Text="Shield Item" />
                            </div>
                            <div>
                                Hardness<asp:TextBox ID="DetailedHardness_tb" runat="server" CssClass="ItemS" />
                                HP<asp:TextBox ID="DetailedHP_tb" runat="server" CssClass="ItemS" />
                            </div>
                            <div>Notes<asp:TextBox ID="DetailedNotes_tb" runat="server" CssClass="ItemL" /></div>
                            <div>Bonus<asp:TextBox ID="DetailedBonus_tb" runat="server" CssClass="ItemL" /></div>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="EditPic" runat="server" ImageUrl="~/images/View.jpg" OnClick="ViewGeneral" />
                        </div>
                    </asp:Panel>           
                <!--<div>
                    + Enhancement to Item<asp:TextBox ID="DetailedEnhance_tb" runat="server" CssClass="ItemM" />
                </div>-->

                
                    <div><p></p>WEAPON INFO</div>
                <div>
             <!--
                    Attack Bonus<asp:TextBox ID="DetailedAttackBonus_tb" runat="server" CssClass="ItemM" />
                    
                    
                    <asp:TextBox ID="ExtraDmg_tb" runat="server" CssClass="ItemS" BackColor="#CCCCDD" />-->
                </div>
                    <asp:Panel ID="WeaponView" runat="server" Visible=false >
                        <div id="Div1" class="InfoViewBlock">
                            <%= ViewWeaponText()%>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="EditWeapon" />
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="WeaponEdit" runat="server" Visible=false >
                        <div class="InfoEditBlock">
                            <div>
                                <asp:CheckBox ID="DetailedRanged_cb" runat="server" Text="Ranged" />
                                <asp:CheckBox ID="DetailedMelee_cb" runat="server" Text="Melee" />
                                 Damage Multiplier<asp:TextBox ID="DetailedDmgMultiplier_tb" runat="server" CssClass="ItemS" />
                            </div>
                            <div>
                                Damage<asp:TextBox ID="DetailedDamage_tb" runat="server" CssClass="ItemS" />
                                Damage Type<asp:TextBox ID="DetailedDamageType_tb" runat="server" CssClass="ItemM" />
                            </div>
                            <div>
                                Crit<asp:TextBox ID="DetailedCrit_tb" runat="server" CssClass="ItemM" />
                                Range<asp:TextBox ID="DetailedRange_tb" runat="server" CssClass="ItemS" /> ft
                            </div>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/View.jpg" OnClick="ViewWeapon" />
                        </div>
                    </asp:Panel> 

                    <div><p></p>ARMOR INFO</div>
                    <asp:Panel ID="ArmorView" runat="server" Visible=false >
                        <div id="Div2" class="InfoViewBlock">
                            <%= ViewArmorText()%>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="EditArmor" />
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="ArmorEdit" runat="server" Visible=false >
                        <div class="InfoEditBlock">
                            <div>
                                Arcane Spell Failure<asp:TextBox ID="DetailedSpellFailure_tb" runat="server" CssClass="ItemS" />
                                Armor Check Penalty<asp:TextBox ID="DetailedArmorCheckPenalty_tb" runat="server" CssClass="ItemS" />
                            </div>    
                            <div>    
                                Armor Bonus<asp:TextBox ID="DetailedArmorBonus_tb" runat="server" CssClass="ItemS" />
                                Shield Bonus<asp:TextBox ID="DetailedShieldBonus_tb" runat="server" CssClass="ItemS" />
                            </div>    
                            <div>
                                Max Dex<asp:TextBox ID="DetailedMaxDex_tb" runat="server" CssClass="ItemS" />
                                Max Speed<asp:TextBox ID="DetailedMaxSpeed_tb" runat="server" CssClass="ItemS" />
                            </div>
                        </div>
                        <div style="float:left">
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/View.jpg" OnClick="ViewArmor" />
                        </div>
                    </asp:Panel>
                </ContentTemplate></asp:UpdatePanel>  
                    
                    
                    
                
                
                
                
                
            </div>
        </div>
        
        
        

           
        
        <!--
        <uc:ItemControl3 ID="CombatItem" runat="server" />
        -->
        
        
        <div style="clear:both"></div>
    </div>
</asp:Content>