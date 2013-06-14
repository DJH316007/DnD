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
    private string edition = "3.5";
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

    private string GetEdition(){
        return edition.Replace('.', '_');
    }

    public void LoadClassSkills(string rawEdition, string classAbr){
        edition = rawEdition;
        sql.SetStoredProcName("LoadClassSkills_" + GetEdition());
        sql.AddVariable("@class", classAbr);
        DataTable dt = sql.ExecReturnTable();
        Skills.DataSource = dt;
        Skills.DataBind();
    }

    public void SetAbilityMods(int str, int dex, int con, int intel, int wis, int cha){
        abilityMods[0] = str;
        abilityMods[1] = dex;
        abilityMods[2] = con;
        abilityMods[3] = intel;
        abilityMods[4] = wis;
        abilityMods[5] = cha;
    }

    public void LoadCharacter(int charID){
        characterID = charID;
        if (characterID < 1)
            return;
        sql.SetStoredProcName("LoadCharacterSkills");
        sql.AddVariable("@charID", characterID);
        Skills.DataSource = sql.ExecReturnTable();
        Skills.DataBind();
    }

    protected void Skills_IDB(Object Sender, RepeaterItemEventArgs e){
        //no header of footer
        if (e.Item.ItemIndex < 0)
            return;
        TextBox total = (TextBox)e.Item.FindControl("SkillTotal_tb");
        if (total != null){
            DataRowView dr = ((DataRowView)e.Item.DataItem);
            float classRank = 1;
            if (!h.ToBool(dr["ClassSkill"]))
                classRank = (float).5;

            sql.SetStoredProcName("GetACPenalty");
            sql.AddVariable("@ownerID", characterID);
            sql.AddVariable("@characterEdition", GetEdition());
///////////////////////////////////////////////////////////////////////////////
            int abilMod = -1;
            if (dr["Ability"].ToString() == "Str")
                abilMod = 0;
            if (dr["Ability"].ToString() == "Dex")
                abilMod = 1;
            if (dr["Ability"].ToString() == "Con")
                abilMod = 2;
            if (dr["Ability"].ToString() == "Int")
                abilMod = 3;
            if (dr["Ability"].ToString() == "Wis")
                abilMod = 4;
            if (dr["Ability"].ToString() == "Cha")
                abilMod = 5;

         total.Text = abilityMods[abilMod] + h.ToInt(dr["Ranks"]) * classRank
          + h.ToInt(dr["Misc"]) + sql.ExecReturnInt("@ACPenalty") * h.ToInt(dr["ACPenaltyMultiplier"]) + "";
//////////////////////////////////////////////////////////////////////
   //         total.Text = DnDSession.GetAbilityModCurrent(dr["Ability"].ToString()) + h.ToInt(dr["Ranks"]) * classRank
   //          + h.ToInt(dr["Misc"]) + acPenalty.ExecReturnInt("@ACPenalty") * h.ToInt(dr["ACPenaltyMultiplier"]) + "";
        }
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
        for (int i = 0; i < Skills.Items.Count; i++){
            dr = ((DataTable)Skills.DataSource).Rows[i];
            ri = Skills.Items[i];

            AddSkill(dr["SkillName"].ToString(), h.ToBool(dr["ClassSkill"]), h.ToBool(dr["Untrained"]),
             h.ToInt(dr["ACPenaltyMultiplier"]), dr["Ability"].ToString(),
             h.ToInt((TextBox)ri.FindControl("SkillRanks_tb")), h.ToInt((TextBox)ri.FindControl("SkillMisc_tb")));
        }
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
        for (int i = 0; i < Skills.Items.Count; i++){
            dr = ((DataTable)Skills.DataSource).Rows[i];
            ri = Skills.Items[i];

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
