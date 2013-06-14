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

public partial class _Default : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        sql.SetStoredProcName("LoadCharactersShort_3_5");
        DataTable dt = sql.ExecReturnTable();
        sql.SetStoredProcName("LoadCharactersShort_Pathfinder");
        DataTable dt_path = sql.ExecReturnTable();
        dt.Merge(dt_path);
        CharShortInfo.DataSource = dt;
        CharShortInfo.DataBind();
    }
    protected void CharShortInfo_ItemDataBound(Object Sender, RepeaterItemEventArgs e){
        //no header of footer
        if (e.Item.ItemIndex < 0)
            return;
        HyperLink select = (HyperLink)e.Item.FindControl("CharacterLink");
        if (select != null)
            select.NavigateUrl = "~/CharacterInfo.aspx?Select=" + ((DataRowView)(e.Item.DataItem)).Row["ID"] + "&Edition=" + ((DataRowView)(e.Item.DataItem)).Row["Edition"];

        Label l = (Label)e.Item.FindControl("RaceText");
        if (l != null)
            l.Text = "| " + RaceType.Text(Int32.Parse(((DataRowView)(e.Item.DataItem)).Row["Race"].ToString()));
        l = (Label)e.Item.FindControl("ClassText");
        if (l != null)
            l.Text = "| " + ClassType.Text(Int32.Parse(((DataRowView)(e.Item.DataItem)).Row["Class"].ToString()));
    }
}
