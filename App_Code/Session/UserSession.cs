using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class UserSession{
    private DataRow characerInfo;
    private DataRow classInfo;
    private DataRow detailedItem;

    private int maxSpeed, maxDex;//add total weight and get from db in general info

    private Helper h = new Helper();
    private SQLDatabase sql = new SQLDatabase();

    private int []currentItem;
    private string bonuses = "";//, BonusStats = "", BonusSkills = "", BonusOther = "";

    public UserSession() {
        currentItem = new int[14];
        Reset();
    }

    public void Reset(){
        maxSpeed = 1200; maxDex = 1200;
        bonuses = "";
    }

    public void ClearBonus(){
        bonuses = "";
    }

    public DataRow CharacterInfo{
        get { return characerInfo; }
        set { characerInfo = value; Reset(); }
    }

    public DataRow ClassInfo{
        set { classInfo = value; }
    }


/// ///////////////////////////////////////

    public string Speed{
        get { return CharacterInfo["Speed"].ToString(); }
        set { CharacterInfo["Speed"] = value; }
    }

///////////////////////////////////////////////////////////////////////////////////


    public int ID{
        get { return Int32.Parse(CharacterInfo["ID"].ToString()); }
    }
    public string Edition{
        get { return CharacterInfo["Edition"].ToString().Replace('_', '.'); }
    }
    public string Edition_Raw{
        get { return CharacterInfo["Edition"].ToString(); }
    }
    public string Name{
        get { return CharacterInfo["Name"].ToString(); }
        set { CharacterInfo["Name"] = value; }
    }
    public int Level{
        get { return Int32.Parse(CharacterInfo["Lvl"].ToString()); }
        set { CharacterInfo["Lvl"] = value; }
    }
    public string Race{
        get { Helper h = new Helper(); return RaceType.Text(h.ToInt(CharacterInfo["Race"].ToString())); }
        set { CharacterInfo["Race"] = value; }
    }
    public string Class{
        get { Helper h = new Helper(); return ClassType.Text(h.ToInt(CharacterInfo["Class"].ToString())); }
        set { CharacterInfo["Class"] = value; }
    }
    public string Align{
        get { return CharacterInfo["Align"].ToString(); }
        set { CharacterInfo["Align"] = value; }
    }
    public string Size{
        get { Helper h = new Helper(); return SizeType.Text(h.ToInt(CharacterInfo["Size"].ToString())); }
        set { CharacterInfo["Size"] = value; }
    }
    public int Age{
        get { Helper h = new Helper(); return h.ToInt(CharacterInfo["Age"].ToString()); }
        set { CharacterInfo["Age"] = value; }
    }
    public string Sex{
        get { return CharacterInfo["Sex"].ToString(); }
        set { CharacterInfo["Sex"] = value; }
    }
    public string Height{
        get { return CharacterInfo["Height"].ToString(); }
        set { CharacterInfo["Height"] = value; }
    }
    public string Weight{
        get { return CharacterInfo["Weight"].ToString(); }
        set { CharacterInfo["Weight"] = value; }
    }
    public string Deity{
        get { return CharacterInfo["Deity"].ToString(); }
        set { CharacterInfo["Deity"] = value; }
    }
    public string Notes{
        get { return characerInfo["Notes"].ToString(); }
        set { characerInfo["Notes"] = value; }
    }




    public void CalculateArmorAC(){
        sql.SetStoredProcName("GetArmorBonus");
        sql.AddVariable("@ownerID", ID);
        sql.AddVariable("@characterEdition", Edition);
        AddBonus("AC", "Armor", sql.ExecReturnInt("ArmorBonus"), true, true);

        sql.ClearParameters();
        sql.SetStoredProcName("GetShieldBonus");
        sql.AddVariable("@ownerID", ID);
        sql.AddVariable("@characterEdition", Edition);
        AddBonus("AC", "Shield", sql.ExecReturnInt("ShieldBonus"), true, true); 
    }

    public void ItemBonus(){
        sql.SetStoredProcName("GetItemBonus");
        sql.AddVariable("@ownerID", ID);
        sql.AddVariable("@characterEdition", Edition);
        DataTable dt = sql.ExecReturnTable();
        foreach (DataRow dr in dt.Rows)
            BonusInputString(dr["Bonus"].ToString());
    }

    public int ACPenalty(){
        sql.SetStoredProcName("GetACPenalty");
        sql.AddVariable("@ownerID", ID);
        sql.AddVariable("@characterEdition", Edition_Raw);//will always be pathfinder
        return sql.ExecReturnInt("@ACPenalty");
    }

    public bool IsClass(string compareClass){
        Helper h = new Helper();
        if (h.ToInt(CharacterInfo["Class"].ToString()) == ClassType.Number(compareClass))
            return true;
        return false;
    }

    public bool IsClass(int compareClass){
        Helper h = new Helper();
        if (h.ToInt(CharacterInfo["Class"].ToString()) == compareClass)
            return true;
        return false;
    }

    private int CalculateMod(int stat){
        return (int)Math.Floor((stat - 10) / 2.0);
    }

    //Ability Functions
    public int AbilityBase(string ability){
        return h.ToInt(CharacterInfo[ability].ToString());
    }
    public void AbilityBase(string ability, int value){
        CharacterInfo[ability] = value;
    }
    public int AbilityBaseMod(string ability){
        return CalculateMod(AbilityBase(ability));
    }

    public int Ability(string ability){
        return h.ToInt(CharacterInfo[ability].ToString()) + BonusValue(ability);
    }
    public int AbilityMod(string ability){
        return CalculateMod(AbilityBase(ability) + BonusValue(ability));
    }

    public void BonusInputString(string s){
        string[] sArray = s.Split('/');
        foreach (string ss in sArray){//split into bonuses
            string[] parts = ss.Split(':', '!');
            if (parts.Length == 3)
                AddBonus(parts[0], parts[1], h.ToInt(parts[2]));
        }
    }
    
    public string BonusString(string searchFor){
        string output = "";
        if (bonuses.Length < 1)//checks to make sure string is not empty
            return output;
        string[] sArray = bonuses.Split('/');
        int findLocation, findColon;
        foreach (string s in sArray){//split into bonuses
            findLocation = s.IndexOf(searchFor + ":");
            if (findLocation > -1){//found the string
                findColon = s.IndexOf(':');
                string ss = s.Substring(findColon + 1, s.Length - findColon - 1);
                ss = ss.Replace('!', ':');
                ss += '\n';
                output += ss + "  ";
            }
        }
        return output;
    }

    public int BonusValue(string searchFor){
        int bonusTotal = 0;
        if (bonuses.Length < 1)//checks to make sure string is not empty
            return bonusTotal;
        string[] sArray = bonuses.Split('/');
        int findLocation, findEx;
        foreach (string s in sArray) {//split into bonuses
            findLocation = s.IndexOf(searchFor);
            if (findLocation > -1){//found the string
                findEx = s.IndexOf('!');
                bonusTotal += h.ToInt(s.Substring(findEx + 1, s.Length - findEx - 1));
            }
        }
        return bonusTotal;
    }
    public int BonusValue(string b, string type){
        return BonusValue(b + ":" + type + "!");
    }


    public void AddBonus(string name, string type, int value, bool replace = false, bool zeroDelete = false){
        string bonus = name + ":" + type + "!";
        int startHere = bonuses.IndexOf(bonus), endHere;
        if (startHere > -1){//bonus and type already in
            endHere = bonuses.IndexOf('/', startHere);
            if (value == 0 && zeroDelete){//bonus is zero and should be deleted
                bonuses = bonuses.Remove(startHere, endHere - startHere + 1);
                return;
            }
            int currentValue = h.ToInt(bonuses.Substring(startHere + bonus.Length, endHere - startHere - bonus.Length));
            if (value > currentValue || replace)
                bonuses = bonuses.Replace(bonus + currentValue + "/", bonus + value + "/");
            return;
        }
        if (value == 0)//no point in adding a bonus of 0
            return;
        bonuses += bonus + value + "/";//new bonus stat  
    }

    //each stat will be in the form of Stat:Type!+-#/   replace with automatically replace the value and zeroDelete will delete the bonus if value is zero



/*
    public bool BonusStatFind(string stat){
        string output = "";
        if (BonusStats.Length < 1)//checks to make sure string is not empty
            return output;
        string[] stats = BonusStats.Split('/');
        int findLocation, findColon;
        foreach (string s in stats){//split into stat bonuses
            findLocation = s.IndexOf(stat + ":");
            if (findLocation > -1){//found the stat
                findColon = s.IndexOf(':');
                string ss = s.Substring(findColon + 1, s.Length - findColon - 1);
                ss = ss.Replace('!', ' ');
                output += ss + "  ";
            }
        }
        return output;
    }
    */






    public int HPCurrent{
        get { return h.ToInt(characerInfo["CurrHP"].ToString()); }
        set { if (value > HPTotal) characerInfo["CurrHP"] = HPTotal; else characerInfo["CurrHP"] = value; }
    }
    public int HPTotal{
        get { return h.ToInt(characerInfo["TotalHP"].ToString()); }
        set { characerInfo["TotalHP"] = value; }
    }

    public int XP{
        get { return h.ToInt(characerInfo["XP"].ToString()); }
        set { characerInfo["XP"] = value; }
    }
    public int PP{
        get { return h.ToInt(characerInfo["PP"].ToString()); }
        set { characerInfo["PP"] = value; }
    }
    public int GP{
        get { return h.ToInt(characerInfo["GP"].ToString()); }
        set { characerInfo["GP"] = value; }
    }
    public int SP{
        get { return h.ToInt(characerInfo["SP"].ToString()); }
        set { characerInfo["SP"] = value; }
    }
    public int CP{
        get { return h.ToInt(characerInfo["CP"].ToString()); }
        set { characerInfo["CP"] = value; }
    }



    public int BaseFortSave{
        get{
            Helper h = new Helper();
            if (IsClass(ClassType.BARBARIAN) || IsClass(ClassType.CLERIC) || IsClass(ClassType.DRUID) || IsClass(ClassType.FIGHTER) || 
             IsClass(ClassType.MONK) || IsClass(ClassType.PALADIN) || IsClass(ClassType.RANGER))
                return h.ToInt(classInfo["BSB_Good"].ToString());
            if (IsClass(ClassType.BARD) || IsClass(ClassType.ROGUE) || IsClass(ClassType.SORCERER) || IsClass(ClassType.WIZARD) || IsClass(ClassType.WITCH))
                return h.ToInt(classInfo["BSB_Poor"].ToString());
            return 0;
        }
    }
    public int BaseReflexSave{
        get{
            Helper h = new Helper();
            if (IsClass(ClassType.BARD) || IsClass(ClassType.MONK) || IsClass(ClassType.RANGER) || IsClass(ClassType.ROGUE))
                return h.ToInt(classInfo["BSB_Good"].ToString());
            if (IsClass(ClassType.BARBARIAN) || IsClass(ClassType.CLERIC) || IsClass(ClassType.DRUID) || IsClass(ClassType.FIGHTER) ||
             IsClass(ClassType.PALADIN) || IsClass(ClassType.SORCERER) || IsClass(ClassType.WIZARD) || IsClass(ClassType.WITCH))
                return h.ToInt(classInfo["BSB_Poor"].ToString());
            return 0;
        }
    }
    public int BaseWillSave{
        get{
            Helper h = new Helper();
            if (IsClass(ClassType.BARD) || IsClass(ClassType.CLERIC) || IsClass(ClassType.DRUID) || IsClass(ClassType.MONK) ||
             IsClass(ClassType.SORCERER) || IsClass(ClassType.WIZARD) || IsClass(ClassType.WITCH))
                return h.ToInt(classInfo["BSB_Good"].ToString());
            if (IsClass(ClassType.BARBARIAN) || IsClass(ClassType.FIGHTER) || IsClass(ClassType.PALADIN) ||
             IsClass(ClassType.RANGER) || IsClass(ClassType.ROGUE))
                return h.ToInt(classInfo["BSB_Poor"].ToString());
            return 0;
        }
    }

    public int Fortitude{
        get { return BaseFortSave + AbilityMod("Con") + BonusValue("Fortitude"); }
    }
    public int Reflex{
        get { return BaseReflexSave + AbilityMod("Dex") + BonusValue("Reflex"); }
    }
    public int Will{
        get { return BaseWillSave + AbilityMod("Wis") + BonusValue("Will"); }
    }

    public string BaseAttack{
        get{
            if (IsClass(ClassType.BARBARIAN) || IsClass(ClassType.FIGHTER) || IsClass(ClassType.PALADIN) || IsClass(ClassType.RANGER))
                return classInfo["BAB_Good"].ToString();
            if (IsClass(ClassType.BARD) || IsClass(ClassType.CLERIC) || IsClass(ClassType.DRUID) ||
             IsClass(ClassType.MONK) || IsClass(ClassType.ROGUE))
                return classInfo["BAB_Avg"].ToString();
            if (IsClass(ClassType.SORCERER) || IsClass(ClassType.WIZARD) || IsClass(ClassType.WITCH))
                return classInfo["BAB_Poor"].ToString();
            return "";
        }
    }

    public int BaseAttackHighest{
        get{
            Helper h = new Helper();
            string[] attacks = BaseAttack.Split('/');
            return h.ToInt(attacks[0]);
        }
    }

    public int RanksTotal{
        get { return h.ToInt(characerInfo["SkillRanksTotal"]); }
    }

    public string ClassSpecial{
        get{
            string classStr = Class + "Special";
            return classInfo[classStr].ToString();
        }
    }

///////Item Stuff
    public DataRow DetailedItem{
        set { detailedItem = value; }
    }

    public object DetailedItemProp(string prop){
        object itemProp = null;
        try { itemProp = detailedItem[prop]; }
        catch { }
        return itemProp;
    }
    public void DetailedItemProp(string prop, object value){
        detailedItem[prop] = value;
    }

/// <summary>
/// //////////////////////////////////////////////////////////
/// </summary>
/// <param name="bodypart"></param>
/// <returns></returns>
    public int GetCurrItem(int bodypart){
        return currentItem[bodypart - 1];
    }

    public void SetCurrItem(int bodypart, int val){
        currentItem[bodypart - 1] = val;
    }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////
/// </summary>
    public void Save(){
        SQLDatabase characterInfo = new SQLDatabase();
        characterInfo.SetStoredProcName("SaveCharacter");
        characterInfo.AddVariable("@index", ID);
        characterInfo.AddVariable("@edition", Edition_Raw);
        characterInfo.AddVariable("@name", Name);
        characterInfo.AddVariable("@level", Level);
        characterInfo.AddVariable("@race", RaceType.Number(Race));
        characterInfo.AddVariable("@class", ClassType.Number(Class));
        characterInfo.AddVariable("@align", Align);
        characterInfo.AddVariable("@size", SizeType.Number(Size));
        characterInfo.AddVariable("@age", Age);
        characterInfo.AddVariable("@sex", Sex);
        characterInfo.AddVariable("@height", Height);
        characterInfo.AddVariable("@weight", Weight);
        characterInfo.AddVariable("@deity", Deity);
        characterInfo.AddVariable("@str", AbilityBase("Str"));
        characterInfo.AddVariable("@dex", AbilityBase("Dex"));
        characterInfo.AddVariable("@con", AbilityBase("Con"));
        characterInfo.AddVariable("@int", AbilityBase("Int"));
        characterInfo.AddVariable("@wis", AbilityBase("Wis"));
        characterInfo.AddVariable("@cha", AbilityBase("Cha"));
        characterInfo.AddVariable("@currHP", HPCurrent.ToString());
        characterInfo.AddVariable("@totalHP", HPTotal.ToString());
        characterInfo.AddVariable("@speed", Speed);
        characterInfo.AddVariable("@ranksTotal", RanksTotal);
        characterInfo.AddVariable("@xp", XP);
        characterInfo.AddVariable("@cp", CP);
        characterInfo.AddVariable("@sp", SP);
        characterInfo.AddVariable("@gp", GP);
        characterInfo.AddVariable("@pp", PP);
        characterInfo.AddVariable("@notes", Notes);
        characterInfo.Exec();
    }
}
