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

public class Helper{
	public Helper(){
	}

    public bool ToBool(Object o){
        bool value = false;
        try { value = bool.Parse(o.ToString()); }
        catch { }
        return value;
    }
    public bool ToBool(Object o, bool defValue){
        bool value = defValue;
        try { value = bool.Parse(o.ToString()); }
        catch { }
        return value;
    }

    public int ToInt(string s){
        int value = 0;
        try { value = Int32.Parse(s); }
        catch { }
        return value;
    }
    public int ToInt(string s, int defValue){
        int value = defValue;
        try { value = Int32.Parse(s); }
        catch { }
        return value;
    }

    public int ToInt(TextBox tb){
        int value = 0;
        try { value = Int32.Parse(tb.Text.ToString()); }
        catch { }
        return value;
    }
    public int ToInt(TextBox tb, int defValue){
        int value = defValue;
        try { value = Int32.Parse(tb.Text.ToString()); }
        catch { }
        return value;
    }

    public int ToInt(Object o){
        int value = 0;
        try { value = Int32.Parse(o.ToString()); }
        catch { }
        return value;
    }
    public int ToInt(Object o, int defValue){
        int value = defValue;
        try { value = Int32.Parse(o.ToString()); }
        catch { }
        return value;
    }

    public float ToFloat(string s){
        float value = 0;
        try { value = float.Parse(s); }
        catch { }
        return value;
    }
    public float ToFloat(string s, float defValue){
        float value = defValue;
        try { value = float.Parse(s); }
        catch { }
        return value;
    }

    public float ToFloat(TextBox tb){
        float value = 0;
        try { value = float.Parse(tb.Text.ToString()); }
        catch { }
        return value;
    }
    public float ToFloat(TextBox tb, float defValue){
        float value = defValue;
        try { value = float.Parse(tb.Text.ToString()); }
        catch { }
        return value;
    }

    public float ToFloat(Object o){
        float value = 0;
        try { value = float.Parse(o.ToString()); }
        catch { }
        return value;
    }
    public float ToFloat(Object o, int defValue){
        float value = defValue;
        try { value = float.Parse(o.ToString()); }
        catch { }
        return value;
    }
}
