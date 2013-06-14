using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Items : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        LoadItems();
    }

    private void LoadItems(){
        sql.SetStoredProcName("LoadCharacterItems");
        sql.AddVariable("@characterID", DnDSession.ID);
        sql.AddVariable("@edition", DnDSession.Edition_Raw);
        AllItems.DataSource = sql.ExecReturnTable();
        AllItems.DataBind();
    }

    protected void TTT(string id){
        sql.SetStoredProcName("LoadItem");
        sql.AddVariable("@id", id);
        DnDSession.DetailedItem = sql.ExecReturnTable().Rows[0];

        EditGeneral(null, null);//needed to load in values to the textboxes
        EditWeapon(null, null);//needed to load in values to the textboxes
        EditArmor(null, null);//needed to load in values to the textboxes
        ViewGeneral(null, null);
        ViewWeapon(null, null);
        ViewArmor(null, null);
    }


    protected void ViewAsDetailed(object sender, EventArgs e){
        int repeaterItemID = h.ToInt(((Button)(sender)).CommandArgument) - 1;
        if (repeaterItemID < 0)
            return;
        SaveItemShort(repeaterItemID);

        sql.SetStoredProcName("LoadItem");
        sql.AddVariable("@id", ((DataTable)AllItems.DataSource).Rows[repeaterItemID]["ID"]);
        DnDSession.DetailedItem = sql.ExecReturnTable().Rows[0];

        EditGeneral(null, null);//needed to load in values to the textboxes
        EditWeapon(null, null);//needed to load in values to the textboxes
        EditArmor(null, null);//needed to load in values to the textboxes
        ViewGeneral(null, null);
        ViewWeapon(null, null);
        ViewArmor(null, null);
    }

    protected void TagButtons_IDB(Object Sender, RepeaterItemEventArgs e){
        //no header of footer
        if (e.Item.ItemIndex < 0)
            return;
        Button b = (Button)e.Item.FindControl("DetailedView");
        if (b != null)
            b.CommandArgument = e.Item.ItemIndex + 1 + "";
        ImageButton ib = (ImageButton)e.Item.FindControl("Delete");
        if (ib != null)
            ib.CommandArgument = e.Item.ItemIndex + 1 + "";

        DropDownList dd = (DropDownList)e.Item.FindControl("BodyPart_dd");
        if (dd != null){
            ListItem li = new ListItem("None", "0");
            dd.Items.Add(li);
            for (int i = BodyPart.minValue; i <= BodyPart.maxValue; i++){
                li = new ListItem(BodyPart.Text(i), i + "");
                dd.Items.Add(li);
            }
            dd.SelectedValue = ((DataRowView)e.Item.DataItem)["BodyPart"].ToString();
        }
    }

    protected string GetAttack(string type){
        string abilityMod = "";
        if (type == "Melee")
            abilityMod = "Str";
        else if (type == "Ranged")
            abilityMod = "Dex";
        else
            return DnDSession.BaseAttack;
        int bonus = DnDSession.AbilityMod(abilityMod) + SizeMod(SizeType.Number(DnDSession.Size));

        string attack = "";
        string[] attacks = DnDSession.BaseAttack.Split('/');
        foreach (string s in attacks)
            attack += "+" + (h.ToInt(s) + bonus) + "/";
        attack = attack.Remove(attack.Length - 1);
        return attack;
    }
    
    private bool IsMasterwork(){//may change MW to bit in DB
        if (h.ToInt(DnDSession.DetailedItemProp("CraftType")) == 1)
            return true;
        return false;
    }

    protected void ViewGeneral(object sender, EventArgs e){
        GeneralEdit.Visible = false;
        GeneralView.Visible = true;
        DnDSession.DetailedItemProp("ID", h.ToInt(DetailedID_hf.Value));
        DnDSession.DetailedItemProp("Equiped", DetailedEquiped_cb.Checked);
        DnDSession.DetailedItemProp("Proficient", DetailedProficient_cb.Checked);
        DnDSession.DetailedItemProp("ItemName", DetailedName_tb.Text);
        DnDSession.DetailedItemProp("Weight", h.ToInt(DetailedWeight_tb));
        if (DetailedMithral_cb.Checked || h.ToInt(DetailedEnchancement_tb) > 0)//change into radio button and add mithral and adaman
            DnDSession.DetailedItemProp("CraftType", 1);
        DnDSession.DetailedItemProp("MagicEnhancement", h.ToInt(DetailedEnchancement_tb.Text));
        DnDSession.DetailedItemProp("Weapon", DetailedWeapon_cb.Checked);
        DnDSession.DetailedItemProp("Armor", DetailedArmor_cb.Checked);
        DnDSession.DetailedItemProp("Shield", DetailedShield_cb.Checked);
        DnDSession.DetailedItemProp("Hardness", h.ToInt(DetailedHardness_tb));
        DnDSession.DetailedItemProp("HP", h.ToInt(DetailedHP_tb));
        DnDSession.DetailedItemProp("Notes", DetailedNotes_tb.Text);
        DnDSession.DetailedItemProp("Bonus", DetailedBonus_tb.Text);
    }
    protected void EditGeneral(object sender, EventArgs e){
        GeneralEdit.Visible = true;
        GeneralView.Visible = false;
        DetailedID_hf.Value = DnDSession.DetailedItemProp("ID").ToString() + "";
        DetailedEquiped_cb.Checked = h.ToBool(DnDSession.DetailedItemProp("Equiped"));
        DetailedProficient_cb.Checked = h.ToBool(DnDSession.DetailedItemProp("Proficient"));
        DetailedName_tb.Text = DnDSession.DetailedItemProp("ItemName").ToString();
        DetailedWeight_tb.Text = DnDSession.DetailedItemProp("Weight").ToString();
        if (h.ToInt(DnDSession.DetailedItemProp("CraftType")) == 1)//change into radio button and add mithral and adaman
            DetailedMasterwork_cb.Checked = true;
        DetailedEnchancement_tb.Text = DnDSession.DetailedItemProp("MagicEnhancement").ToString();
        DetailedWeapon_cb.Checked = h.ToBool(DnDSession.DetailedItemProp("Weapon"));
        DetailedArmor_cb.Checked = h.ToBool(DnDSession.DetailedItemProp("Armor"));
        DetailedShield_cb.Checked = h.ToBool(DnDSession.DetailedItemProp("Shield"));
        DetailedHardness_tb.Text = DnDSession.DetailedItemProp("Hardness").ToString();
        DetailedHP_tb.Text = DnDSession.DetailedItemProp("HP").ToString();
        DetailedNotes_tb.Text = DnDSession.DetailedItemProp("Notes").ToString();
        DetailedBonus_tb.Text = DnDSession.DetailedItemProp("Bonus").ToString();
    }
    protected string ViewGeneralText(){
        return "Name: " + DnDSession.DetailedItemProp("ItemName") + 
         "<br />Weight: " + DnDSession.DetailedItemProp("Weight") + " lbs\n" +
         "<br />Notes: " + DnDSession.DetailedItemProp("Notes");
    }

    protected void ViewWeapon(object sender, EventArgs e){
        WeaponEdit.Visible = false;
        WeaponView.Visible = true;
///////////change to radio buttons
        if (DetailedRanged_cb.Checked)
            DnDSession.DetailedItemProp("CombatType", 1);//ranged weapon
        else if (DetailedMelee_cb.Checked)
            DnDSession.DetailedItemProp("CombatType", 2);//melee weapon
//////////
        DnDSession.DetailedItemProp("DmgMultiplier", h.ToFloat(DetailedDmgMultiplier_tb.Text));
        DnDSession.DetailedItemProp("Damage", DetailedDamage_tb.Text);
        DnDSession.DetailedItemProp("DamageType", DetailedDamageType_tb.Text);
        DnDSession.DetailedItemProp("Crit", DetailedCrit_tb.Text);
        DnDSession.DetailedItemProp("AttackRange", DetailedRange_tb.Text);
    }
    protected void EditWeapon(object sender, EventArgs e){
        WeaponEdit.Visible = true;
        WeaponView.Visible = false;
///////////change to radio buttons
        if (DnDSession.DetailedItemProp("CombatType").ToString() == "1")//ranged weapon
            DetailedRanged_cb.Checked = true;
        else if (DnDSession.DetailedItemProp("CombatType").ToString() == "2")//melee weapon
            DetailedMelee_cb.Checked = true;
//////////
        DetailedDmgMultiplier_tb.Text = DnDSession.DetailedItemProp("DmgMultiplier").ToString();
        DetailedDamage_tb.Text = DnDSession.DetailedItemProp("Damage").ToString();
        DetailedDamageType_tb.Text = DnDSession.DetailedItemProp("DamageType").ToString();
        DetailedCrit_tb.Text = DnDSession.DetailedItemProp("Crit").ToString();
        DetailedRange_tb.Text = DnDSession.DetailedItemProp("AttackRange").ToString();
    }
    protected string ViewWeaponText(){
        if (!h.ToBool(DnDSession.DetailedItemProp("Weapon")))
            return "";
        string attackBonus = "", extraDamage = "";
        int attBonus = 0, extraDmg = 0; 
//should be in a switch statement
        if (h.ToInt(DnDSession.DetailedItemProp("CombatType")) == 1){//Ranged
            attBonus = DnDSession.AbilityMod("Dex") + SizeMod(SizeType.Number(DnDSession.Size));     
        }
        else if (h.ToInt(DnDSession.DetailedItemProp("CombatType")) == 2){//Melee
            extraDmg = (int)(DnDSession.AbilityMod("Str") * h.ToFloat(DnDSession.DetailedItemProp("DmgMultiplier")));
            attBonus = DnDSession.AbilityMod("Str") + SizeMod(SizeType.Number(DnDSession.Size));
        }
        int enhanced = h.ToInt(DnDSession.DetailedItemProp("MagicEnhancement"));
        if (enhanced > 0){
            attBonus += enhanced;
            extraDmg += enhanced;
        }
        if (DetailedMasterwork_cb.Checked)
            attBonus++;

        string[] attackBAB = DnDSession.BaseAttack.Split('/');
        foreach (string s in attackBAB){
            int totalBonus = h.ToInt(s) + attBonus;
            if (totalBonus > -1)
                attackBonus += "+";
            attackBonus += totalBonus + "/";
        }
        attackBonus = attackBonus.Remove(attackBonus.Length - 1);

        if (extraDmg > -1)
            extraDamage += " + ";
        extraDamage += extraDmg + " ";

        return "Attack Bonus: " + attackBonus +
         "<br />Damage: " + DnDSession.DetailedItemProp("Damage") + extraDamage + DnDSession.DetailedItemProp("DamageType") +
         "<br />Crit: " + DnDSession.DetailedItemProp("Crit") +
         "<br />Range: " + DnDSession.DetailedItemProp("AttackRange") + " ft";
    }

    protected void ViewArmor(object sender, EventArgs e){
        ArmorEdit.Visible = false;
        ArmorView.Visible = true;
        DnDSession.DetailedItemProp("SpellFailure", h.ToInt(DetailedSpellFailure_tb.Text));
        DnDSession.DetailedItemProp("ArmorCheckPenalty", h.ToInt(DetailedArmorCheckPenalty_tb.Text));
        DnDSession.DetailedItemProp("ArmorBonus", h.ToInt(DetailedArmorBonus_tb.Text));
        DnDSession.DetailedItemProp("ShieldBonus", h.ToInt(DetailedShieldBonus_tb.Text));
        DnDSession.DetailedItemProp("MaxDex", h.ToInt(DetailedMaxDex_tb.Text));
        DnDSession.DetailedItemProp("MaxSpeed", h.ToInt(DetailedMaxSpeed_tb.Text));
    }
    protected void EditArmor(object sender, EventArgs e){
        ArmorEdit.Visible = true;
        ArmorView.Visible = false;
        DetailedSpellFailure_tb.Text = DnDSession.DetailedItemProp("SpellFailure").ToString();
        DetailedArmorCheckPenalty_tb.Text= DnDSession.DetailedItemProp("ArmorCheckPenalty").ToString();
        DetailedArmorBonus_tb.Text = DnDSession.DetailedItemProp("ArmorBonus").ToString();
        DetailedShieldBonus_tb.Text = DnDSession.DetailedItemProp("ShieldBonus").ToString();
        DetailedMaxDex_tb.Text = DnDSession.DetailedItemProp("MaxDex").ToString();
        DetailedMaxSpeed_tb.Text = DnDSession.DetailedItemProp("MaxSpeed").ToString();
    }
    protected string ViewArmorText(){
        if (!h.ToBool(DnDSession.DetailedItemProp("Armor")) && !h.ToBool(DnDSession.DetailedItemProp("Shield")))
            return "";
        int acPenalty = h.ToInt(DnDSession.DetailedItemProp("ArmorCheckPenalty"));
        string acArmor = "", acShield = "";
        if (IsMasterwork())
            acPenalty ++;

        int enhanced = h.ToInt(DnDSession.DetailedItemProp("MagicEnhancement"));
        if (h.ToBool(DnDSession.DetailedItemProp("Armor")))
            acArmor = "<br />Armor Bonus: " + (h.ToInt(DnDSession.DetailedItemProp("ArmorBonus")) + enhanced);
        if (h.ToBool(DnDSession.DetailedItemProp("Shield")))
            acShield = "<br />Shield Bonus: " + (h.ToInt(DnDSession.DetailedItemProp("ShieldBonus")) + enhanced);



        return "Arcane Spell Failure: " + DnDSession.DetailedItemProp("SpellFailure") +
         "<br />Armor Check Penalty: " + acPenalty + acArmor + acShield;
    }

    protected void NewItem(object sender, EventArgs e){
        SaveItems(null, null);

        sql.SetStoredProcName("AddItem");
        sql.AddVariable("@owner", DnDSession.ID);
        sql.AddVariable("@edition", DnDSession.Edition_Raw);
        sql.AddVariable("@bodyPart", -1);
        sql.Exec();
        LoadItems();
    }

    private void SaveItemShort(int repeaterID){
        sql.SetStoredProcName("UpdateItemShort");
        sql.AddVariable("@itemID", ((DataTable)AllItems.DataSource).Rows[repeaterID]["ID"]);
        sql.AddVariable("@bodyPart", ((DataTable)AllItems.DataSource).Rows[repeaterID]["BodyPart"]);
        sql.AddVariable("@equiped", ((CheckBox)AllItems.Items[repeaterID].FindControl("Equiped_cb")).Checked);
        sql.AddVariable("@name", ((TextBox)AllItems.Items[repeaterID].FindControl("ItemName_tb")).Text);
        sql.AddVariable("@weight", h.ToFloat(((TextBox)AllItems.Items[repeaterID].FindControl("Weight_tb"))));
        sql.Exec();
    }
    protected void SaveItems(object sender, EventArgs e){
        sql.SetStoredProcName("UpdateItemShort");
        for (int i = 0; i < AllItems.Items.Count; i++){
            sql.ClearParameters();
            sql.AddVariable("@itemID", ((DataTable)AllItems.DataSource).Rows[i]["ID"]);
            sql.AddVariable("@bodyPart", ((DataTable)AllItems.DataSource).Rows[i]["BodyPart"]);
            sql.AddVariable("@equiped", ((CheckBox)AllItems.Items[i].FindControl("Equiped_cb")).Checked);
            sql.AddVariable("@name", ((TextBox)AllItems.Items[i].FindControl("ItemName_tb")).Text);
            sql.AddVariable("@weight", h.ToFloat(((TextBox)AllItems.Items[i].FindControl("Weight_tb"))));
            sql.Exec();
        }
    }

    protected void SaveItem(object sender, EventArgs e){
        ViewGeneral(null, null);//saves changes to datarow
        ViewWeapon(null, null);
        ViewArmor(null, null);

        int id = h.ToInt(DetailedID_hf.Value);
        if (id < 1)
            return;

        sql.SetStoredProcName("UpdateItem");
        sql.AddVariable("@id", id);
        sql.AddVariable("@equiped", h.ToBool(DnDSession.DetailedItemProp("Equiped")));
        sql.AddVariable("@proficient", h.ToBool(DnDSession.DetailedItemProp("Proficient")));
        sql.AddVariable("@name", DnDSession.DetailedItemProp("ItemName").ToString());
        sql.AddVariable("@weight", h.ToFloat(DnDSession.DetailedItemProp("Weight")));
        sql.AddVariable("@craftType", h.ToInt(DnDSession.DetailedItemProp("CraftType")));
        sql.AddVariable("@magicEnhancement", DnDSession.DetailedItemProp("MagicEnhancement").ToString());
        sql.AddVariable("@weapon", h.ToBool(DnDSession.DetailedItemProp("Weapon")));
        sql.AddVariable("@armor", h.ToBool(DnDSession.DetailedItemProp("Armor")));
        sql.AddVariable("@shield", h.ToBool(DnDSession.DetailedItemProp("Shield")));
        sql.AddVariable("@bodyPart", h.ToInt(DnDSession.DetailedItemProp("BodyPart")));////////not implemented in yet//////////////////////
        sql.AddVariable("@hardness", h.ToInt(DnDSession.DetailedItemProp("Hardness")));
        sql.AddVariable("@hp", h.ToInt(DnDSession.DetailedItemProp("HP")));
        sql.AddVariable("@notes", DnDSession.DetailedItemProp("Notes").ToString());
        sql.AddVariable("@bonus", DnDSession.DetailedItemProp("Bonus").ToString());

        sql.AddVariable("@combatType", h.ToInt(DnDSession.DetailedItemProp("CombatType")));
        sql.AddVariable("@dmgMultiplier", h.ToFloat(DnDSession.DetailedItemProp("DmgMultiplier")));
        sql.AddVariable("@damage", DnDSession.DetailedItemProp("Damage").ToString());
        sql.AddVariable("@damageType", DnDSession.DetailedItemProp("DamageType").ToString());
        sql.AddVariable("@crit", DnDSession.DetailedItemProp("Crit").ToString());
        sql.AddVariable("@attackRange", h.ToInt(DnDSession.DetailedItemProp("AttackRange")));

        sql.AddVariable("@spellFailure", h.ToInt(DnDSession.DetailedItemProp("SpellFailure")));
        sql.AddVariable("@armorCheckPenalty", h.ToInt(DnDSession.DetailedItemProp("ArmorCheckPenalty")));
        sql.AddVariable("@armorBonus", h.ToInt(DnDSession.DetailedItemProp("ArmorBonus")));
        sql.AddVariable("@shieldBonus", h.ToInt(DnDSession.DetailedItemProp("ShieldBonus")));
        sql.AddVariable("@maxDex", h.ToInt(DnDSession.DetailedItemProp("MaxDex")));
        sql.AddVariable("@maxSpeed", h.ToInt(DnDSession.DetailedItemProp("MaxSpeed")));
        sql.Exec();
        LoadItems();
    }

    //should only be one delete method
    protected void DeleteItemShort(object sender, EventArgs e){
        int repeaterItemID = h.ToInt(((ImageButton)(sender)).CommandArgument) - 1;
        if (repeaterItemID < 0)
            return;
        sql.SetStoredProcName("DeleteItem");
        sql.AddVariable("@ID", h.ToInt(((DataTable)AllItems.DataSource).Rows[repeaterItemID]["ID"]));
        sql.Exec();
        LoadItems();
    }

    protected void DeleteItem(object sender, EventArgs e){
        int id = h.ToInt(DetailedID_hf.Value);
        if (id < 1)
            return;
        sql.SetStoredProcName("DeleteItem");
        sql.AddVariable("@ID", id);
        sql.Exec();
        LoadItems();
    }
}

