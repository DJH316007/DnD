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

public partial class controls_SkillsBasic : DnDControl{
    private int characterID = 0;
    private int[] abilityMods = new int[6];

    protected void Page_Load(object sender, EventArgs e){
    }

    public void LoadClassSkills(string rawEdition, string classAbr){
        sql.SetStoredProcName("LoadClassSkills_" + Request.QueryString["edition"].Replace('.','_'));
        sql.AddVariable("@class", classAbr);
        Skills.DataSource = sql.ExecReturnTable();
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

            sql.SetStoredProcName("GetACPenalty");
            sql.AddVariable("@ownerID", 0);
            sql.AddVariable("@characterEdition", Request.QueryString["edition"].Replace('.', '_'));
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

         total.Text = abilityMods[abilMod] + h.ToInt(dr["Ranks"]) + h.ToInt(dr["Misc"]) + 
          sql.ExecReturnInt("@ACPenalty") * h.ToInt(dr["ACPenaltyMultiplier"]) + "";
        }
    }

    private void AddSkill(string name, string edition, bool classSkill, bool untrained, int acPenalty, string ability, int ranks, int misc){
        /////////////////////////////////change ability to int in future?
        if (characterID < 1)
            return;
        sql.SetStoredProcName("AddCharacterSkill");
        sql.AddVariable("@characterID", characterID);
        sql.AddVariable("@name", name);
        sql.AddVariable("@edition", edition);
        sql.AddVariable("@classSkill", classSkill);
        sql.AddVariable("@untrained", untrained);
        sql.AddVariable("@acPenaltyMultiplier", acPenalty);
        sql.AddVariable("@ability", ability);
        sql.AddVariable("@ranks", ranks);
        sql.AddVariable("@misc", misc);
        sql.Exec();
    }

    public void AddCharacterSkills(int charID, string edition){
        characterID = charID;
        DataRow dr;
        RepeaterItem ri;
        for (int i = 0; i < Skills.Items.Count; i++)
        {
            dr = ((DataTable)Skills.DataSource).Rows[i];
            ri = Skills.Items[i];

            AddSkill(dr["SkillName"].ToString(), edition, h.ToBool(dr["ClassSkill"]), h.ToBool(dr["Untrained"]),
             h.ToInt(dr["ACPenaltyMultiplier"]), dr["Ability"].ToString(),
             h.ToInt((TextBox)ri.FindControl("SkillRanks_tb")), h.ToInt((TextBox)ri.FindControl("SkillMisc_tb")));
        }
    }
}

