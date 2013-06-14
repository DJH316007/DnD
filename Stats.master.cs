using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Stats : DnDMaster{
    Helper help = new Helper();

    protected void Page_Load(object sender, EventArgs e){
        if (!Page.IsPostBack){
            Name_tb.Text = DnDSession.Name;
            Level_tb.Text = DnDSession.Level.ToString();
            Race_tb.Text = DnDSession.Race;
            Class_tb.Text = DnDSession.Class;
            Alignment_tb.Text = DnDSession.Align;
            Size_tb.Text = DnDSession.Size;
            Age_tb.Text = DnDSession.Age.ToString();
            Sex_tb.Text = DnDSession.Sex;
            Height_tb.Text = DnDSession.Height;
            Weight_tb.Text = DnDSession.Weight;
            Deity_tb.Text = DnDSession.Deity;
            Notes_tb.Text = DnDSession.Notes;
            ///////////////////////////////////////
            Speed_tb.Text = DnDSession.Speed;
            ///////////////////////////////////////////////////

            XP_tb.Text = DnDSession.XP.ToString();
            Platinum_tb.Text = DnDSession.PP.ToString();
            Gold_tb.Text = DnDSession.GP.ToString();
            Silver_tb.Text = DnDSession.SP.ToString();
            Copper_tb.Text = DnDSession.CP.ToString();
            CalculateWeight();

            ClassAbilities.Text = DnDSession.ClassSpecial;

            Str_TextChanged(null, null);
            Dex_TextChanged(null, null);
            Con_TextChanged(null, null);
            Int_TextChanged(null, null);
            Wis_TextChanged(null, null);
            Cha_TextChanged(null, null);

            SetSkillsTable();


        }
//////should be moved somewhere else on character load

        DnDSession.ClearBonus();
        DnDSession.CalculateArmorAC();
        DnDSession.ItemBonus();



        //LevelUpCheck();//only works for 3.5 now


        /*//////////////////////////////////////////////////
        int skillMod = 2;//most classes have 2
        int c = ClassType.Number(DnDSession.Class);
        if (c == ClassType.ROGUE) skillMod = 8;
        if (c == ClassType.BARD || c == ClassType.RANGER) skillMod = 6;
        if (c == ClassType.BARBARIAN || c == ClassType.DRUID || c == ClassType.MONK) skillMod = 4;
        Rank_tb.Text = (skillMod + DnDSession.GetAbilityModCurrent("Int")) * (DnDSession.Level + 3) + "";
       */
        /////////////////////////////////////////////////////
        KDK.InnerHtml = "details<span class=\"custom critical\">" + DnDSession.BonusString("Str") + "</span>";
        Str_tb.ToolTip = DnDSession.BonusString("Str");
        Dex_tb.ToolTip = DnDSession.BonusString("Dex");
        Con_tb.ToolTip = DnDSession.BonusString("Con");
        Int_tb.ToolTip = DnDSession.BonusString("Int");
        Wis_tb.ToolTip = DnDSession.BonusString("Wis");
        Cha_tb.ToolTip = DnDSession.BonusString("Cha");
        Fortitude_tb.ToolTip = DnDSession.BonusString("Fortitude");
        Reflex_tb.ToolTip = DnDSession.BonusString("Reflex");
        Will_tb.ToolTip = DnDSession.BonusString("Will");
        TotalAC_tb.ToolTip = DnDSession.BonusString("AC");

        StrChange.outputTB = StrTemp_tb.UniqueID;
        DexChange.outputTB = DexTemp_tb.UniqueID;
        ConChange.outputTB = ConTemp_tb.UniqueID;
        IntChange.outputTB = IntTemp_tb.UniqueID;
        WisChange.outputTB = WisTemp_tb.UniqueID;
        ChaChange.outputTB = ChaTemp_tb.UniqueID;

        FortitudeChange.outputTB = FortitudeTemp_tb.UniqueID;
        ReflexChange.outputTB = ReflexTemp_tb.UniqueID;
        WillChange.outputTB = WillTemp_tb.UniqueID;

        HPChange.outputTB = CurrHP_tb.UniqueID; HPChange.Clear = true;
        CoinsPP.outputTB = Platinum_tb.UniqueID; CoinsPP.Clear = true;
        CoinsGP.outputTB = Gold_tb.UniqueID; CoinsGP.Clear = true;
        CoinsSP.outputTB = Silver_tb.UniqueID; CoinsSP.Clear = true;
        CoinsCP.outputTB = Copper_tb.UniqueID; CoinsCP.Clear = true;
        XPChange.outputTB = XP_tb.UniqueID; XPChange.Clear = true;
    }

    private void GenInfo_TextChanged(object sender, EventArgs e){
        DnDSession.Name = Name_tb.Text;
        DnDSession.Level = h.ToInt(Level_tb.Text);
        DnDSession.Align = Alignment_tb.Text;
        DnDSession.Age = h.ToInt(Age_tb.Text);
        DnDSession.Sex = Sex_tb.Text;
        DnDSession.Height = Height_tb.Text;
        DnDSession.Weight = Weight_tb.Text;
        DnDSession.Deity = Deity_tb.Text;
        DnDSession.Notes = Notes_tb.Text;
    }

    private void SetAbility(string abr){
        TextBox ability, abilityMod, abilityTemp;
        if (abr == "Str"){
            ability = Str_tb;
            abilityMod = StrMod_tb;
            abilityTemp = StrTemp_tb;
        }
        else if (abr == "Dex"){
            ability = Dex_tb;
            abilityMod = DexMod_tb;
            abilityTemp = DexTemp_tb;
        }
        else if (abr == "Con"){
            ability = Con_tb;
            abilityMod = ConMod_tb;
            abilityTemp = ConTemp_tb;
        }
        else if (abr == "Int"){
            ability = Int_tb;
            abilityMod = IntMod_tb;
            abilityTemp = IntTemp_tb;
        }
        else if (abr == "Wis"){
            ability = Wis_tb;
            abilityMod = WisMod_tb;
            abilityTemp = WisTemp_tb;
        }
        else if (abr == "Cha"){
            ability = Cha_tb;
            abilityMod = ChaMod_tb;
            abilityTemp = ChaTemp_tb;
        }
        else
            return;
        if (abilityTemp.Text != "")//shouldn't be in here
            //DnDSession.SetAbilityTemp(abr, help.ToInt(abilityTemp));

            DnDSession.AddBonus(abr, "Temp", help.ToInt(abilityTemp), true, true);


        ability.Text = DnDSession.Ability(abr).ToString();
        abilityMod.Text = DnDSession.AbilityMod(abr).ToString();
        abilityTemp.Text = "";//control adds to this so it needs to reset

        if (Page.IsPostBack)
            SetSkillsTable();
    }

    protected void Str_TextChanged(object sender, EventArgs e){
        SetAbility("Str");

        string[] baseAttacks = DnDSession.BaseAttack.Split('/');
////////put into 3.5
        //////Grapple_tb.Text = h.ToInt(baseAttacks[0]) + DnDSession.GetAbilityModCurrent("Str") + "";

        CalculateCarryCapacity();
    }
    protected void Dex_TextChanged(object sender, EventArgs e){
        SetAbility("Dex");

        //AC
        int ac = 10 + DnDSession.AbilityMod("Dex") - SizeType.Number(DnDSession.Size) + DnDSession.BonusValue("AC");
        int armorDiff = DnDSession.BonusValue("AC", "NA") + DnDSession.BonusValue("AC", "Armor") + DnDSession.BonusValue("AC", "Shield");

        TotalAC_tb.Text = ac + "";
        //no armor, shield, or NA
        TouchAC_tb.Text = ac - armorDiff + "";
        //no dex
        FlatFootedAC_tb.Text = ac - DnDSession.AbilityMod("Dex") + "";

        //Init
        Init_tb.Text = DnDSession.AbilityMod("Dex") + DnDSession.BonusValue("Init") + "";
    }
    protected void Con_TextChanged(object sender, EventArgs e){
        SetAbility("Con");
        //HP

        int tempHPs = (DnDSession.AbilityMod("Con") - DnDSession.AbilityBaseMod("Con")) * DnDSession.Level;
        DnDSession.HPCurrent += HPChange.Changed;
        //if (!(CurrHP_tb.Text == null || CurrHP_tb.Text == ""))
        //    DnDSession.HPCurrent = h.ToInt(CurrHP_tb) - tempHPs;
        CurrHP_tb.Text = DnDSession.HPCurrent + tempHPs + "";
        TotalHP_tb.Text = DnDSession.HPTotal + tempHPs + "";
    }
    protected void Int_TextChanged(object sender, EventArgs e){
        SetAbility("Int");
    }
    protected void Wis_TextChanged(object sender, EventArgs e){
        SetAbility("Wis");
    }
    protected void Cha_TextChanged(object sender, EventArgs e){
        SetAbility("Cha");
    }

/////////no temps right now
    protected void Fort_TextChanged(object sender, EventArgs e){//triggered by ConChange or FortChange controls
        Fortitude_tb.Text = DnDSession.Fortitude + "";
    }
    protected void Reflex_TextChanged(object sender, EventArgs e){//triggered by DexChange or ReflexChange controls
        Reflex_tb.Text = DnDSession.Reflex + "";
    }
    protected void Will_TextChanged(object sender, EventArgs e){//triggered by WisChange or WillChange controls
        Will_tb.Text = DnDSession.Will + "";
    }

    private void SetSkillsTable(){
        CharacterSkills.SetAbilityMods(DnDSession.AbilityMod("Str"), DnDSession.AbilityMod("Dex"), DnDSession.AbilityMod("Con"), DnDSession.AbilityMod("Int"), DnDSession.AbilityMod("Wis"), DnDSession.AbilityMod("Cha"));
        CharacterSkills.LoadCharacter(DnDSession.ID);
    }

    protected void SkillsReload(object sender, EventArgs e){
        SetSkillsTable();
    }

    protected void Damage(object sender, EventArgs e){
        //DnDSession.HPCurrent = h.ToInt(CurrHP_tb.Text);
        //CurrHP_tb.Text = DnDSession.HPCurrent + "";


       // int dmg = h.ToInt(Damage_tb);
       // DnDSession.HPCurrent += dmg;
       // int ConMod_tb = DnDSession.GetAbilityModCurrent("Con") - DnDSession.GetAbilityMod("Con");
       // CurrHP_tb.Text = (DnDSession.HPCurrent + ConMod_tb * DnDSession.Level).ToString();
       // Damage_tb.Text = "";
    }

    private void CalculateCarryCapacity(){
        sql.SetStoredProcName("GetCarryCapacity");
        sql.AddVariable("@Strength", DnDSession.Ability("Str"));
        sql.AddVariable("@Size", SizeType.Number(DnDSession.Size));
        DataRow dr = sql.ExecReturnTable().Rows[0];
        LightLoad_tb.Text = dr["LightLoad"].ToString();
        MediumLoad_tb.Text = dr["MediumLoad"].ToString();
        HeavyLoad_tb.Text = dr["HeavyLoad"].ToString();
        //change textbox color of TotalWeight_tb
    }

    private void CalculateWeight(){
        sql.SetStoredProcName("GetItemsWeight");
        sql.AddVariable("@ownerID", DnDSession.ID);
        sql.AddVariable("@characterEdition", DnDSession.Edition);
        TotalWeight_tb.Text = sql.ExecReturnFloat("@TotalWeight") + ((float)(DnDSession.PP + DnDSession.GP + DnDSession.SP + DnDSession.CP)) / 50.0 + "";
    }

    protected void CoinChanged(object sender, EventArgs e){
        DnDSession.PP = h.ToInt(Platinum_tb);
        DnDSession.GP = h.ToInt(Gold_tb);
        DnDSession.SP = h.ToInt(Silver_tb);
        DnDSession.CP = h.ToInt(Copper_tb);
        CalculateWeight();
    }

    protected void XPChanged(object sender, EventArgs e){
        DnDSession.XP = h.ToInt(XP_tb);
        //LevelUpCheck();
    }

    private void LevelUpCheck()
    {//only works for 3.5
        /*
        if (DnDSession.XP >= 500 * (DnDSession.Level + 1) * DnDSession.Level){
            Level_panel.Visible = true;
        }
        else{
            Level_panel.Visible = false;
        }
        */




        Level_panel.Visible = true;



    }
    protected void LevelUpCharacter(object sender, EventArgs e)
    {
        DnDSession.Save();
        sql.SetStoredProcName("LevelUp");
        sql.AddVariable("@index", DnDSession.ID);
        sql.AddVariable("@edition", DnDSession.Edition);
        sql.AddVariable("@HPAdd", h.ToInt(LevelUpHP_tb));
        sql.AddVariable("@ranksTotal", h.ToInt(LevelUpRanks_tb));
        sql.Exec();
        Response.Redirect("~/CharacterInfo.aspx?Select=" + DnDSession.ID.ToString() + "&Edition=Pathfinder");
    }

    protected void SaveStats(object sender, EventArgs e){
        DnDSession.Notes = Notes_tb.Text;
        DnDSession.Save();
    }









    protected void UpdateBonuses(object sender, EventArgs e){
        DnDSession.BonusInputString(StatsEnter.Text);
        DnDSession.BonusInputString(SkillsEnter.Text);
        DnDSession.BonusInputString(OtherEnter.Text);
    }
}
