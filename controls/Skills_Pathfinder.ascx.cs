using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class controls_Skills : DnDControl{
    private int characterID = 0;
    private int[] abilityMods = new int[6];

    protected void Page_Load(object sender, EventArgs e){
        if (!Page.IsPostBack){
            ListItem li = new ListItem("Str", "Str"); NewSkillAbility_dd.Items.Add(li);
            li = new ListItem("Dex", "Dex"); NewSkillAbility_dd.Items.Add(li);
            li = new ListItem("Con", "Con"); NewSkillAbility_dd.Items.Add(li);
            li = new ListItem("Int", "Int"); NewSkillAbility_dd.Items.Add(li);
            li = new ListItem("Wis", "Wis"); NewSkillAbility_dd.Items.Add(li);
            li = new ListItem("Cha", "Cha"); NewSkillAbility_dd.Items.Add(li);
        }
    }

    public int TotalRanks(){//shouldn't this be in dndsession?
        sql.SetStoredProcName("GetTotalSkillRanks");
        sql.AddVariable("@characterID", DnDSession.ID);
        sql.AddVariable("@edition", DnDSession.Edition_Raw);
        return h.ToInt(sql.ExecReturnTable().Rows[0][0]);
    }

    public void LoadCharacter(int charID){
        characterID = charID;
        if (characterID < 1)
            return;
        sql.SetStoredProcName("LoadCharacterSkills");
        sql.AddVariable("@charID", characterID);
        sql.AddVariable("@edition", DnDSession.Edition_Raw);//will always be pathfinder
        /*
        DataTable dt = sql.ExecReturnTable();
        dt.DefaultView.RowFilter = "SkillName = 'Heal'";
        SkillList.DataSource = dt;
        SkillList.DataBind();
        */
        SkillList.DataSource = sql.ExecReturnTable();
        SkillList.DataBind();
    }









    /*
    public void LoadClassSkills(string rawEdition, string classAbr){
        sql.SetStoredProcName("LoadClassSkills_" + DnDSession.Edition_Raw);
        sql.AddVariable("@class", classAbr);
        SkillList.DataSource = sql.ExecReturnTable();
        SkillList.DataBind();
    }
*/
    public void SetAbilityMods(int str, int dex, int con, int intel, int wis, int cha){
        abilityMods[0] = str;
        abilityMods[1] = dex;
        abilityMods[2] = con;
        abilityMods[3] = intel;
        abilityMods[4] = wis;
        abilityMods[5] = cha;
    }

    protected void Skills_IDB(Object Sender, RepeaterItemEventArgs e){
        //no header of footer
        if (e.Item.ItemIndex < 0)
            return;
        TextBox total = (TextBox)e.Item.FindControl("SkillTotal_tb");
        if (total == null)
            return;
        DataRowView dr = ((DataRowView)e.Item.DataItem);
        if (dr != null)
            total.Text = DnDSession.AbilityMod(dr["Ability"].ToString()) + h.ToInt(dr["Ranks"]) +
             h.ToInt(dr["Misc"]) + DnDSession.BonusValue(dr["SkillName"].ToString()) + DnDSession.ACPenalty() * h.ToInt(dr["ACPenaltyMultiplier"]) + "";
        if(!Page.IsPostBack){
            /*
            controls_AddSubtract cAS = (controls_AddSubtract)e.Item.FindControl("SkillRanks");
            if (cAS != null)
                cAS.Value = h.ToInt(dr["Ranks"]);
            cAS = (controls_AddSubtract)e.Item.FindControl("SkillMisc");
            if (cAS != null)
                cAS.Value = h.ToInt(dr["Misc"]);
        */
        }
        //not needed yet
        ((TextBox)e.Item.FindControl("FFFF")).Text = DnDSession.ACPenalty() + "";
    }

    protected void AddRank(object sender, EventArgs e){
        TextBox tb = ((TextBox)(((Button)sender).Parent.FindControl("SkillRanks_tb")));
        int currRanks = h.ToInt(tb);
        if (currRanks >= DnDSession.Level || TotalRanks() >= DnDSession.RanksTotal)
            return;
        tb.Text = currRanks + 1 + "";

        sql.SetStoredProcName("UpdateSkillRank");
        sql.AddVariable("@id", h.ToInt(((HiddenField)(((Button)sender).Parent.FindControl("SkillID"))).Value));
        sql.AddVariable("@value", h.ToInt(tb));
        sql.Exec();
    }
    protected void SubtractRank(object sender, EventArgs e){
        TextBox tb = ((TextBox)(((Button)sender).Parent.FindControl("SkillRanks_tb")));
        if (h.ToInt(tb) < 1)
            return;
        tb.Text = h.ToInt(tb) - 1 + "";

        sql.SetStoredProcName("UpdateSkillRank");
        sql.AddVariable("@id", h.ToInt(((HiddenField)(((Button)sender).Parent.FindControl("SkillID"))).Value));
        sql.AddVariable("@value", h.ToInt(tb));
        sql.Exec();
    }





/// ////////not working yet

    protected void RankIt(object sender, EventArgs e){
        controls_AddSubtract cas = (controls_AddSubtract)sender;
        if (cas == null)
            return;
        sql.SetStoredProcName("UpdateSkillMisc");
        sql.AddVariable("@id", h.ToInt(((HiddenField)(((controls_AddSubtract)sender).Parent.FindControl("SkillID"))).Value));
        sql.AddVariable("@value", cas.Value);
        sql.Exec();
    }





    protected void AddMisc(object sender, EventArgs e){
        TextBox tb = ((TextBox)(((Button)sender).Parent.FindControl("SkillMisc_tb")));
        tb.Text = h.ToInt(tb) + 1 + "";
        
        sql.SetStoredProcName("UpdateSkillMisc");
        sql.AddVariable("@id", h.ToInt(((HiddenField)(((Button)sender).Parent.FindControl("SkillID"))).Value));
        sql.AddVariable("@value", h.ToInt(tb));
        sql.Exec();
    }
    protected void SubtractMisc(object sender, EventArgs e){
        TextBox tb = ((TextBox)(((Button)sender).Parent.FindControl("SkillMisc_tb")));
        tb.Text = h.ToInt(tb) - 1 + "";

        sql.SetStoredProcName("UpdateSkillMisc");
        sql.AddVariable("@id", h.ToInt(((HiddenField)(((Button)sender).Parent.FindControl("SkillID"))).Value));
        sql.AddVariable("@value", h.ToInt(tb));
        sql.Exec();
    }

    private void AddSkill(string name, bool classSkill, bool untrained, int acPenalty, string ability, int ranks, int misc){
/////////////////////////////////change ability to int in future?
        if (characterID < 1)
            return;
        sql.SetStoredProcName("AddCharacterSkill");
        sql.AddVariable("@characterID", characterID);
        sql.AddVariable("@name", name);
        sql.AddVariable("@classSkill", classSkill);
        sql.AddVariable("@untrained", untrained);
        sql.AddVariable("@acPenaltyMultiplier", acPenalty);
        sql.AddVariable("@ability", ability);
        sql.AddVariable("@ranks", ranks);
        sql.AddVariable("@misc", misc);
        sql.Exec();
    }

    protected void NewSkill(object sender, EventArgs e){
        //////////////////////////////////////////////////////does not add misc
        AddSkill(NewSkillName_tb.Text, NewSkilled_cb.Checked, NewSkillUntrained_cb.Checked, h.ToInt(NewSkillACPenMulti_tb),
         NewSkillAbility_dd.Text, h.ToInt(NewSkillRanks_tb), h.ToInt(NewSkillMisc_tb));
    }

    public void AddCharacterSkills(int charID){
        characterID = charID;
        DataRow dr;
        RepeaterItem ri;
        for (int i = 0; i < SkillList.Items.Count; i++){
            dr = ((DataTable)SkillList.DataSource).Rows[i];
            ri = SkillList.Items[i];

            AddSkill(dr["SkillName"].ToString(), h.ToBool(dr["ClassSkill"]), h.ToBool(dr["Untrained"]),
             h.ToInt(dr["ACPenaltyMultiplier"]), dr["Ability"].ToString(),
             h.ToInt((TextBox)ri.FindControl("SkillRanks_tb")), h.ToInt((TextBox)ri.FindControl("SkillMisc_tb")));
        }
    }
    
    protected void SaveSkill(object sender, EventArgs e){
        UpdatePanel up = (UpdatePanel)sender;
        if (up != null){
           // int i = ((controls_AddSubtract)up.FindControl("SkillMisc")).Value;
            sql.SetStoredProcName("UpdateSkill");
            sql.AddVariable("@id", 0);
            sql.AddVariable("@name", "");
            sql.AddVariable("@skilled", true);
            sql.AddVariable("@untrained", true);
            sql.AddVariable("@ACPenMulti", 0);
            sql.AddVariable("@ability", "");
            sql.AddVariable("@ranks", 0);
            sql.AddVariable("@misc", 0);
           // sql.Exec();
        }

 //   ALTER PROC [dbo].[UpdateSkill](@id int, @name varchar(20), @skilled bit, 
// @untrained bit, @ACPenMulti int, @ability varchar(10), @ranks int, @misc int) AS
    
    
    }

    public void SaveSkills(){
        SaveAllSkills(null, null);
    }

    protected void SaveAllSkills(object sender, EventArgs e){
        if (characterID < 1)
            return;
        ///stop using savecharskill
        DataRow dr;
        RepeaterItem ri;
        for (int i = 0; i < SkillList.Items.Count; i++){
            dr = ((DataTable)SkillList.DataSource).Rows[i];
            ri = SkillList.Items[i];

            sql.SetStoredProcName("UpdateSkill");
            sql.AddVariable("@id", dr["ID"]);
            sql.AddVariable("@name", dr["SkillName"]);
            sql.AddVariable("@skilled", dr["ClassSkill"]);
            sql.AddVariable("@untrained", dr["Untrained"]);
            sql.AddVariable("@ACPenMulti", dr["ACPenaltyMultiplier"]);
            sql.AddVariable("@ability", dr["Ability"]);
            sql.AddVariable("@ranks", h.ToInt((TextBox)ri.FindControl("SkillRanks_tb")));
            sql.AddVariable("@misc", h.ToInt((TextBox)ri.FindControl("SkillMisc_tb")));
            sql.Exec();
            sql.ClearParameters();
        }
    }
}
