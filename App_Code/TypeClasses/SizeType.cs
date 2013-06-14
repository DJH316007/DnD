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

public class SizeType{
    public const int FINE = -4;
    public const int DIMINUTIVE = -3;
    public const int TINY = -2;
    public const int SMALL = -1;
    public const int MEDIUM = 0;
    public const int LARGE = 1;
    public const int HUGE = 2;
    public const int GARGANTUAN = 3;
    public const int COLOSSAL = 4;

    public const int minValue = -4;
    public const int maxValue = 4;

    public static string Text(int type){
        switch (type){
            case FINE:
                return "Fine";
            case DIMINUTIVE:
                return "Diminutive";
            case TINY:
                return "Tiny";
            case SMALL:
                return "Small";
            case MEDIUM:
                return "Medium";
            case LARGE:
                return "Large";
            case HUGE:
                return "Huge";
            case GARGANTUAN:
                return "Gargantuan";
            case COLOSSAL:
                return "Colossal";
        }
        return "";
    }
    public static int Number(string text){
        if (text == "Fine")
            return FINE;
        if (text == "Diminutive")
            return DIMINUTIVE;
        if (text == "Tiny")
            return TINY;
        if (text == "Small")
            return SMALL;
        if (text == "Medium")
            return MEDIUM;
        if (text == "Large")
            return LARGE;
        if (text == "Huge")
            return HUGE;
        if (text == "Gargantuan")
            return GARGANTUAN;
        if (text == "Colossal")
            return COLOSSAL;
        return 0;
    }
}
