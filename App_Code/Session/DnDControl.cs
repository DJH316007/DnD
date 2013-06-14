using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class DnDControl : System.Web.UI.UserControl{
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
    public DnDBO bo = new DnDBO();
    public Helper h = new Helper();
    public SQLDatabase sql = new SQLDatabase();
}