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

public class BodyPart{
    public const int HEAD = 1;
    public const int FACE = 2;
    public const int THROAT = 3;
    public const int SHOULDERS = 4;
    public const int TORSO = 5;
    public const int BODY = 6;
    public const int ARMS = 7;
    public const int HANDS = 8;
    public const int WAIST = 9;
    public const int FINGER = 10;
    public const int COMBAT = 11;
    public const int FEET = 12;

    public const int minValue = 1;
    public const int maxValue = 12;

    public static string Text(int part){
        switch (part){
            case HEAD:
                return "HEAD";
            case FACE:
                return "FACE";
            case THROAT:
                return "THROAT";
            case SHOULDERS:
                return "SHOULDERS";
            case TORSO:
                return "TORSO";
            case BODY:
                return "BODY";
            case ARMS:
                return "ARMS";
            case HANDS:
                return "HANDS";
            case WAIST:
                return "WAIST";
            case FINGER:
                return "FINGER";
            case COMBAT:
                return "COMBAT";
            case FEET:
                return "FEET";
        }
        return "";
    }
}
