using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public class ClassType{
    public const int BARBARIAN = 0;
    public const int BARD = 1;
    public const int CLERIC = 2;
    public const int DRUID = 3;
    public const int FIGHTER = 4;
    public const int MONK = 5;
    public const int PALADIN = 6;
    public const int RANGER = 7;
    public const int ROGUE = 8;
    public const int SORCERER = 9;
    public const int WIZARD = 10;
    public const int WITCH = 11;

    public const int minValue = 0;
    public const int maxValue = 11;
    private const int totalClasses = 12;

    //private string[] classNames = new string[totalClasses];
    

    public ClassType(){
        /*
        classNames[BARBARIAN] = "Barbarian";
        classNames[BARD] = "Bard";
        classNames[CLERIC] = "Cleric";
        classNames[DRUID] = "Druid";
        classNames[FIGHTER] = "Fighter";
        classNames[MONK] = "Monk";
        classNames[PALADIN] = "Paladin";
        classNames[RANGER] = "Ranger";
        classNames[ROGUE] = "Rogue";
        classNames[SORCERER] = "Sorcerer";
        classNames[WIZARD] = "Wizard";
        classNames[WITCH] = "Witch";
        */
    }

    public static string Text(int type){
     //   string st = "cowjumpsthemoon";
    //    try {st = classNames[0]; }
     //   catch { return st; }

      //  return st;


        switch (type){
            case BARBARIAN:
                return "Barbarian";
            case BARD:
                return "Bard";
            case CLERIC:
                return "Cleric";
            case DRUID:
                return "Druid";
            case FIGHTER:
                return "Fighter";
            case MONK:
                return "Monk";
            case PALADIN:
                return "Paladin";
            case RANGER:
                return "Ranger";
            case ROGUE:
                return "Rogue";
            case SORCERER:
                return "Sorcerer";
            case WIZARD:
                return "Wizard";
            case WITCH:
                return "Witch";
        }
        return "";
    }
    public static string Abbr(int type){
        switch (type){
            case BARBARIAN:
                return "Bbn";
            case BARD:
                return "Brd";
            case CLERIC:
                return "Clr";
            case DRUID:
                return "Drd";
            case FIGHTER:
                return "Ftr";
            case MONK:
                return "Mnk";
            case PALADIN:
                return "Pal";
            case RANGER:
                return "Rgr";
            case ROGUE:
                return "Rog";
            case SORCERER:
                return "Sor";
            case WIZARD:
                return "Wiz";
            case WITCH:
                return "Wit";
        }
        return "";
    }
    public static int Number(string text)
    {
        if (text == "Barbarian")
            return BARBARIAN;
        if (text == "Bard")
            return BARD;
        if (text == "Cleric")
            return CLERIC;
        if (text == "Druid")
            return DRUID;
        if (text == "Fighter")
            return FIGHTER;
        if (text == "Monk")
            return MONK;
        if (text == "Paladin")
            return PALADIN;
        if (text == "Ranger")
            return RANGER;
        if (text == "Rogue")
            return ROGUE;
        if (text == "Sorcerer")
            return SORCERER;
        if (text == "Wizard")
            return WIZARD;
        if (text == "Witch")
            return WITCH;
        return 0;
    }
}
