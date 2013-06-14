using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Threading;


public partial class CharacterInfo : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        if (Request.QueryString["Select"] == null || Request.QueryString["Edition"] == null){
            Response.End();
            Response.Redirect("Default.aspx");
        }

        string edition = (Request.QueryString["Edition"]).Replace('.', '_');
        sql.SetStoredProcName("LoadCharacter");
        sql.AddVariable("@index", h.ToInt(Request.QueryString["Select"]));
        sql.AddVariable("@edition", edition);
        DnDSession.CharacterInfo = sql.ExecReturnTable().Rows[0];
        sql.ClearParameters();

        sql.SetStoredProcName("LoadClassInfo");
        sql.AddVariable("@edition", edition);
        sql.AddVariable("@class", DnDSession.Class);
        sql.AddVariable("@level", DnDSession.Level);
        DnDSession.ClassInfo = sql.ExecReturnTable().Rows[0];

        Response.Redirect("Stats" + edition + ".aspx");

        //LoadItems Stats
    }
}