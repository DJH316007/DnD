using System;
using System.Collections;
using System.Configuration;
using System.Data;


using System.IO;


using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_EnterSpell : DnDPage{
    private string name, classLevels, school, components, time, duration, range, target, area, save, sr, description;

    protected void Page_Load(object sender, EventArgs e){
    }

    protected void AddSpell_Click(object sender, EventArgs e){
        DnDBO dBO = new DnDBO();

        dBO.AddSpell(SpellName.Text, SpellSchool.Text, SpellComp.Text, SpellTime.Text, SpellRange.Text,
         SpellDuration.Text, SpellSaving.Text, SpellResist.Text, SpellTarget.Text, SpellArea.Text, SpellMaterials.Text, SpellFocus.Text, SpellDescription.Text,
         GetLevel(BardLevel.Text), GetLevel(ClericLevel.Text), GetLevel(DruidLevel.Text), GetLevel(PaladinLevel.Text), GetLevel(RangerLevel.Text),
         GetLevel(SorcererLevel.Text), GetLevel(WizardLevel.Text), GetLevel(ChaosLevel.Text), GetLevel(GoodLevel.Text), GetLevel(LawLevel.Text),
         GetLevel(EvilLevel.Text), GetLevel(AirLevel.Text), GetLevel(WaterLevel.Text), GetLevel(EarthLevel.Text), GetLevel(FireLevel.Text),
         GetLevel(AnimalLevel.Text), GetLevel(DeathLevel.Text), GetLevel(DestructionLevel.Text), GetLevel(HealingLevel.Text),
         GetLevel(KnowledgeLevel.Text), GetLevel(LuckLevel.Text), GetLevel(MagicLevel.Text), GetLevel(PlantLevel.Text), GetLevel(ProtectionLevel.Text),
         GetLevel(StrengthLevel.Text), GetLevel(SunLevel.Text), GetLevel(TravelLevel.Text), GetLevel(TrickeryLevel.Text), GetLevel(WarLevel.Text));
        Response.Redirect("EditSpell.aspx");
    }

    protected int GetLevel(string classLevel){
        if (classLevel == string.Empty)
            return -1;
        return Int32.Parse(classLevel);
    }

    private void ResetValues(){
        name = "";
        classLevels = "";
        school = "";
        components = "";
        time = "";
        duration = "";
        range = "";
        target = "";
        area = "";
        save = "";
        sr = "";
        description = "";
    }
/*
    private bool NextLineTaged(StreamReader rf){
    rf.
    
    
    }
*/

    private bool CheckString(string checkMe, string checkFor, char seperatedBy){
        string[] checkTokens = checkFor.Split(seperatedBy);
        foreach (string s in checkTokens)
            if (checkMe.StartsWith(s))
                return true;
        return false;
    }


    protected void FileAdd_Click(object sender, EventArgs e){
        string filePath = ConfigurationManager.AppSettings["FolderPath"] + "Spells.txt";
        bool firstNontagLine = true;
        string readLine = "", currBlock = "", lastBlock = "", lastLine = "";
        int pos;
        ResetValues();
/*
        string filePath2 = ConfigurationManager.AppSettings["FolderPath"] + "Spells00.txt";
*/
        if (File.Exists(filePath)){
            StreamReader rFile = new StreamReader(filePath);
/*
            StreamWriter wFile = new StreamWriter(filePath2);
*/
            while ((readLine = rFile.ReadLine()) != null){
/*
                if (currLine.StartsWith("paizo.com")){
                    count++;
                    for (int i = 0; i < 9; i++)
                        currLine = rFile.ReadLine();
                    if (currLine.StartsWith("Spells 10")){
                        currLine = rFile.ReadLine();
                    }
                }
                if (currLine.StartsWith("School"))
                    cs++;
                wFile.WriteLine(currLine);
*/
                if (CheckString(readLine, "School,Components,Casting Time,Duration,Range,Target,Area,Saving Throw", ',')){
                    if (!currBlock.StartsWith("Saving Throw"))
                        currBlock.Replace('@', ' ');

                    if (currBlock.StartsWith("School")){
                        if (name != "")
                            AddSpellFromFile();
                        ResetValues();
                        name = lastLine;
                        pos = currBlock.LastIndexOf("Level");
                        school = currBlock.Substring(7, pos - 9);//7 offset for School_ and 2 for the ;_
                        classLevels = currBlock.Substring(pos + 6);
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Components")){
                        components = currBlock.Substring(11);//11 offset for Components_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Casting Time")){
                        time = currBlock.Substring(12);//12 offset for Casting Time_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Duration")){
                        duration = currBlock.Substring(8);//8 offset for Duration_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Range")){
                        range = currBlock.Substring(5);//5 offset for Range_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Target")){
                        target = currBlock.Substring(6);//6 offset for Target_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Area")){
                        area = currBlock.Substring(4);//4 offset for Area_
                        firstNontagLine = true;
                    }
                    else if (currBlock.StartsWith("Saving Throw")){
                     //   pos = currBlock.LastIndexOf("Spell Resistance");
                     //   save = currBlock.Substring(12, pos - 14);//12 offset for Saving Throw_ and 2 for the ;_
                     //   sr = currBlock.Substring(pos + 16);//12 offset for Spell Resistance_
                     //   firstNontagLine = true;

                        string str = "";
                        int i = 0;
                        string[] lines = currBlock.Split('@');
                        bool hasSR = false;

                        if (currBlock.Contains("Spell Resistance"))
                            hasSR = true;

                        foreach (string s in lines){
                            if (i > 0)
                                str += " ";
                            str += s;
                            if (str.Contains("Saving Throw")){
                                if (hasSR){
                                    if (str.Contains("Spell Resistance") && s.Length > s.IndexOf("Spell Resistance"))
                                        break;
                                }
                                else
                                    break;
                            }
                            i++;
                        }

                        if (hasSR){
                            pos = str.LastIndexOf("Spell Resistance");
                            save = str.Substring(12, pos - 14);//12 offset for Saving Throw_ and 2 for the ;_
                            sr = str.Substring(pos + 16);//12 offset for Spell Resistance_
                        }
                        else
                            save = str.Substring(12);//12 offset for Saving Throw_

                        for (; i < lines.Length; i++)
                            description += lines[i];

                        firstNontagLine = true;

                    }
                    else{
                        if (firstNontagLine)
                            firstNontagLine = false;
                        //else
                        //    description += lastLine + " ";
                    }


                    if (firstNontagLine)
                        firstNontagLine = false;
                    currBlock = "";
                    
                }
                else
                    lastLine = readLine;
              //  lastLine = currLine;
                if (currBlock != "")
                    currBlock += "@";
                currBlock += readLine;
            }

            if (!firstNontagLine)
                AddSpellFromFile();


            if (rFile != null)
                rFile.Close();
/*
            if (wFile != null)
                wFile.Close();
*/
        }
    }

    private void AddSpellFromFile(){
        sql.SetStoredProcName("AddSpell");
        sql.AddVariable("@edition", FileEdition_tb.Text);
        sql.AddVariable("@name", name);
        sql.AddVariable("@school", school);
        sql.AddVariable("@classLevel", classLevels);
        sql.AddVariable("@component", components);
        sql.AddVariable("@time", time);
        sql.AddVariable("@duration", duration);
        sql.AddVariable("@range", range);
        sql.AddVariable("@target", target);
        sql.AddVariable("@area", area);
        sql.AddVariable("@save", save);
        sql.AddVariable("@spellRes", sr);
        sql.AddVariable("@description", description);
        sql.Exec();
    }


        /*

        string filePath = ConfigurationManager.AppSettings["FolderPath"] + "spells1.txt";
        string filePath2 = ConfigurationManager.AppSettings["FolderPath"] + "spells2.txt";
        string temp;
        string line;
        string[] spellComp;
        string var = "Level:";
        string lvlLine = "";
        int lineCount = 1;
        int varNum = 16;

        if (File.Exists(filePath)){
            for (int loop = 0; loop < varNum; loop++){
                switch (loop){
                    case 0: var = "Level:"; break;
                    case 1: var = "Components:"; break;
                    case 2: var = "Casting Time:"; break;
                    case 3: var = "Range:"; break;
                    case 4: var = "Effect:"; break;
                    case 5: var = "Duration:"; break;
                    case 6: var = "Saving Throw:"; break;
                    case 7: var = "Spell Resistance:"; break;
                    case 8: var = "Arcane Material Component:"; break;
                    case 9: var = " Material Component:"; break;
                    case 10: var = "Arcane Focus:"; break;
                    case 11: var = " Focus:"; break;
                    case 12: var = "Area:"; break;
                    case 13: var = "Target:"; break;

                }

                StreamReader rFile = new StreamReader(filePath);
                StreamWriter wFile = new StreamWriter(filePath2);
                while ((line = rFile.ReadLine()) != null){
                    if (lineCount == 1)
                        lvlLine += "";
                    
                    if (loop == (varNum - 2)){
                        if (line.Contains("Level:"))
                            lvlLine += lineCount + "!";
                    }
                    else if (loop == (varNum - 1)){
                        string[] lvlAt = lvlLine.Split('!');
                        for (int s = 0; s < lvlAt.Length - 1; s++){
                            if (lineCount == Int32.Parse(lvlAt[s]) - 2)
                                line = "SpellName: " + line;
                            if (lineCount == Int32.Parse(lvlAt[s]) - 1)
                                line = "SpellSchool: " + line;
                        }
                        wFile.WriteLine(line);
                    }
                    else{
                        if (line.ToString() != ""){
                            if (line.Contains(var)){
                                spellComp = line.Split(new string[] { var }, StringSplitOptions.None);
                                if (loop == 8 || loop == 10)
                                    var = var.Remove(0, 7);
                                wFile.WriteLine(spellComp[0]);
                                for (int i = 1; i < spellComp.Length; i++){
                                    wFile.WriteLine(var + spellComp[i]);
                                }
                                if (loop == 8 || loop == 10)
                                    var = "Arcane " + var;
                            }
                            else
                                wFile.WriteLine(line);
                        }
                    }
                    lineCount++;
                }

                if (rFile != null)
                    rFile.Close();
                if (wFile != null)
                    wFile.Close();

                lineCount = 1;
                if (loop != (varNum - 2)){
                    temp = filePath;
                    filePath = filePath2;
                    filePath2 = temp;
                }
            }
        }
        
        string SpellName = "", SpellSchool = "", SpellComp = "", SpellTime = "", SpellRange = "", Effect = "",//does nothing
         SpellDuration = "", SpellSaving = "", SpellResist = "", SpellTarget = "", SpellArea = "",
         SpellMaterials = "", SpellFocus = "", SpellDescription = "";

        int BardLevel = -1, ClericLevel = -1, DruidLevel = -1, PaladinLevel = -1, RangerLevel = -1, 
         SorcererLevel = -1, WizardLevel = -1, ChaosLevel = -1, GoodLevel = -1, LawLevel = -1, EvilLevel = -1, 
         AirLevel = -1, WaterLevel = -1, EarthLevel = -1, FireLevel = -1, AnimalLevel = -1, DeathLevel = -1,
         DestructionLevel = -1, HealingLevel = -1, KnowledgeLevel = -1, LuckLevel = -1, MagicLevel = -1, PlantLevel = -1, 
         ProtectionLevel = -1, StrengthLevel = -1, SunLevel = -1, TravelLevel = -1, TrickeryLevel = -1, WarLevel = -1;

        DnDBO dBO = new DnDBO();
        bool firstTime = true;
        StreamReader rrFile = new StreamReader(filePath);

        while ((line = rrFile.ReadLine()) != null){
            if (line.Contains("SpellName:")){
                if (!firstTime){
                    dBO.AddSpell(SpellName, SpellSchool, SpellComp, SpellTime, SpellRange, SpellDuration, SpellSaving,
                     SpellResist, SpellTarget, SpellArea, SpellMaterials, SpellFocus, Effect + "!" + SpellDescription,
                     BardLevel, ClericLevel, DruidLevel, PaladinLevel, RangerLevel, SorcererLevel, WizardLevel, ChaosLevel, 
                     GoodLevel, LawLevel, EvilLevel, AirLevel, WaterLevel, EarthLevel, FireLevel, AnimalLevel, DeathLevel, 
                     DestructionLevel, HealingLevel, KnowledgeLevel, LuckLevel, MagicLevel, PlantLevel, ProtectionLevel,
                     StrengthLevel, SunLevel, TravelLevel, TrickeryLevel, WarLevel);
                        
                    SpellName = ""; SpellSchool = ""; SpellComp = ""; SpellTime = ""; SpellRange = ""; Effect = "";//does nothing
                    SpellDuration = ""; SpellSaving = ""; SpellResist = ""; SpellTarget = ""; SpellArea = "";
                    SpellMaterials = ""; SpellFocus = ""; SpellDescription = "";

                    BardLevel = -1; ClericLevel = -1; DruidLevel = -1; PaladinLevel = -1; RangerLevel = -1; 
                    SorcererLevel = -1; WizardLevel = -1; ChaosLevel = -1; GoodLevel = -1; LawLevel = -1; EvilLevel = -1; 
                    AirLevel = -1; WaterLevel = -1; EarthLevel = -1; FireLevel = -1; AnimalLevel = -1; DeathLevel = -1;
                    DestructionLevel = -1; HealingLevel = -1; KnowledgeLevel = -1; LuckLevel = -1; MagicLevel = -1; PlantLevel = -1; 
                    ProtectionLevel = -1; StrengthLevel = -1; SunLevel = -1; TravelLevel = -1; TrickeryLevel = -1; WarLevel = -1;
                }
                firstTime = false;
                SpellName = line.Replace("SpellName: ", "");
            }
            else if (line.Contains("SpellSchool:"))
                SpellSchool = line.Replace("SpellSchool: ", "");
            else if (line.Contains("Components:"))
                SpellComp = line.Replace("Components: ", "");
            else if (line.Contains("Casting Time:"))
                SpellTime = line.Replace("Casting Time: ", "");
            else if (line.Contains("Range:"))
                SpellRange = line.Replace("Range: ", "");
            else if (line.Contains("Effect:"))
                Effect = line;
            else if (line.Contains("Duration:"))
                SpellDuration = line.Replace("Duration: ", "");
            else if (line.Contains("Saving Throw:"))
                SpellSaving = line.Replace("Saving Throw: ", "");
            else if (line.Contains("Spell Resistance:"))
                SpellResist = line.Replace("Spell Resistance: ", "");
            else if (line.Contains("Material Component:"))
                SpellMaterials = line.Replace("Material Component: ", "");
            else if (line.Contains("Focus:"))
                SpellFocus = line.Replace("Focus: ", "");
            else if (line.Contains("Area:"))
                SpellArea = line.Replace("Area: ", "");
            else if (line.Contains("Target:"))
                SpellTarget = line.Replace("Target: ", "");

            else if (line.Contains("Level:")){
                line = line.Replace("Level: ", "");
                line = line.Replace(", ", ",");
                string[] classLevel = line.Split(','), class_level;
                foreach (string s in classLevel){
                    if (s.Contains("Brd")){
                        class_level = s.Split(' ');
                        BardLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Clr")){
                        class_level = s.Split(' ');
                        ClericLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Drd")){
                        class_level = s.Split(' ');
                        DruidLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Pal")){
                        class_level = s.Split(' ');
                        PaladinLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Rgr")){
                        class_level = s.Split(' ');
                        RangerLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Sor")){
                        class_level = s.Split(' ');
                        SorcererLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Wiz")){
                        class_level = s.Split(' ');
                        WizardLevel = Int32.Parse(class_level[1]);
                    }

                    if (s.Contains("Air")){
                        class_level = s.Split(' ');
                        AirLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Animal")){
                        class_level = s.Split(' ');
                        AnimalLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Chaos")){
                        class_level = s.Split(' ');
                        ChaosLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Death")){
                        class_level = s.Split(' ');
                        DeathLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Destruction")){
                        class_level = s.Split(' ');
                        DestructionLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Earth")){
                        class_level = s.Split(' ');
                        EarthLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Evil")){
                        class_level = s.Split(' ');
                        EvilLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Fire")){
                        class_level = s.Split(' ');
                        FireLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Good")){
                        class_level = s.Split(' ');
                        GoodLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Healing")){
                        class_level = s.Split(' ');
                        HealingLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Knowledge")){
                        class_level = s.Split(' ');
                        KnowledgeLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Law")){
                        class_level = s.Split(' ');
                        LawLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Luck")){
                        class_level = s.Split(' ');
                        LuckLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Magic")){
                        class_level = s.Split(' ');
                        MagicLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Plant")){
                        class_level = s.Split(' ');
                        PlantLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Protection")){
                        class_level = s.Split(' ');
                        ProtectionLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Strength")){
                        class_level = s.Split(' ');
                        StrengthLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Sun")){
                        class_level = s.Split(' ');
                        SunLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Travel")){
                        class_level = s.Split(' ');
                        TravelLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Trickery")){
                        class_level = s.Split(' ');
                        TrickeryLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("War")){
                        class_level = s.Split(' ');
                        WarLevel = Int32.Parse(class_level[1]);
                    }
                    if (s.Contains("Water")){
                        class_level = s.Split(' ');
                        WaterLevel = Int32.Parse(class_level[1]);
                    }
                }
            }
            else
                SpellDescription += line + "!";
        }

        if (rrFile != null)
            rrFile.Close();
    }
         */
}
