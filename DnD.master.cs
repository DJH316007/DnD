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

public partial class MasterPage : DnDMaster{
    protected void Page_Load(object sender, EventArgs e){
        if (CharID.Value == "")
            CharID.Value = DnDSession.ID + "";
        if (h.ToInt(CharID.Value) != DnDSession.ID)
            Response.Redirect("Default.aspx");

        Stats.PostBackUrl = "~/Stats" + DnDSession.Edition + ".aspx";

        int classNum = ClassType.Number(DnDSession.Class);
        if (classNum == ClassType.BARBARIAN || classNum == ClassType.FIGHTER || classNum == ClassType.MONK || classNum == ClassType.ROGUE)
            Spells.Enabled = false;
    }
}
