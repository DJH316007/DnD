using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class DnDMaster : System.Web.UI.MasterPage{
    public UserSession DnDSession{
        get{
            if (Session["userSession"] == null)
                DnDSession = new UserSession();
            Session.Timeout = 180;
            return (UserSession)Session["userSession"];
        }
        set{
            Session["userSession"] = value;
        }
	}

    public int GetModifier(int ability){
        return (int)Math.Floor(((ability) - 10) / 2.0);
    }

    public DnDBO bo = new DnDBO();
    public Helper h = new Helper();
    public SQLDatabase sql = new SQLDatabase();


    public int SizeMod(int size){
        bool isNeg = false;
        if (size == 0)
            return 0;
        else if (size < 0){
            isNeg = true;
            size *= -1;
        }
        int mod = (int)Math.Pow(2, size - 1);
        if (!isNeg)
            mod *= -1;
        return mod;
    }
}