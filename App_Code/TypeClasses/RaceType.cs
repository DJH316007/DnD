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

public class RaceType{
    public const int DWARF = 1;
    public const int ELF = 2;
    public const int GNOME = 3;
    public const int HALFLING = 4;
    public const int HALFELF = 5;
    public const int HALFORC = 6;
    public const int HUMAN = 7;

    public const int minValue = 1;
    public const int maxValue = 7;

    public static string Text(int type){
        switch (type){
            case DWARF:
                return "Dwarf";
            case ELF:
                return "Elf";
            case GNOME:
                return "Gnome";
            case HALFLING:
                return "Halfling";
            case HALFELF:
                return "Half-Elf";
            case HALFORC:
                return "Half-Orc";
            case HUMAN:
                return "Human";
        }
        return "";
    }
    public static int Number(string text){
        if (text == "Dwarf")
            return DWARF;
        if (text == "Elf")
            return ELF;
        if (text == "Gnome")
            return GNOME;
        if (text == "Halfling")
            return HALFLING;
        if (text == "Half-Elf")
            return HALFELF;
        if (text == "Half-Orc")
            return HALFORC;
        if (text == "Human")
            return HUMAN;
        return 0;
    }
}
