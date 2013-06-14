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

public partial class NewCharacter : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        if (Request.QueryString["edition"] == null)
            Response.Redirect("Default.aspx");
        if (!Page.IsPostBack){
            for (int i = RaceType.minValue; i <= RaceType.maxValue; i++){
                ListItem li = new ListItem(RaceType.Text(i), i.ToString());
                race_dd.Items.Add(li);
            }
            for (int i = ClassType.minValue; i <= ClassType.maxValue; i++){
                ListItem li = new ListItem(ClassType.Text(i), i.ToString());
                class_dd.Items.Add(li);
            }
            for (int i = SizeType.maxValue; i >= SizeType.minValue; i--){
                ListItem li = new ListItem(SizeType.Text(i), i.ToString());
                sizeDD.Items.Add(li);
            }
            sizeDD.SelectedValue = "0";
        }


        CharacterSkills.SetAbilityMods(GetModifier(h.ToInt(Str_tb)), GetModifier(h.ToInt(Dex_tb)), GetModifier(h.ToInt(Con_tb)), GetModifier(h.ToInt(Int_tb)), GetModifier(h.ToInt(Wis_tb)), GetModifier(h.ToInt(Cha_tb)));
        ClassUpdated(null, null);
    }

    protected void ClassUpdated(Object sender, EventArgs e){
        CharacterSkills.LoadClassSkills(Request.QueryString["edition"].Replace('.', '_'), ClassType.Abbr(h.ToInt(class_dd.SelectedValue)));
    }

    protected void AddCharacter_Click(object sender, EventArgs e){
        sql.SetStoredProcName("AddCharacter");
        sql.AddVariable("@edition", Request.QueryString["edition"].Replace('.','_'));
        sql.AddVariable("@Name", nameText.Text);
        sql.AddVariable("@Level", h.ToInt(levelText, 1));
        sql.AddVariable("@Race", h.ToInt(race_dd.SelectedValue));
        sql.AddVariable("@Class", h.ToInt(class_dd.SelectedValue));
        sql.AddVariable("@Align", alignText.Text);
        sql.AddVariable("@Size", h.ToInt(sizeDD.SelectedValue));
        sql.AddVariable("@Age", h.ToInt(ageText));
        sql.AddVariable("@Sex", sexText.Text);
        sql.AddVariable("@Height", heightText.Text);
        sql.AddVariable("@Weight", weightText.Text);
        sql.AddVariable("@Deity", deityText.Text);

        sql.AddVariable("@NaturalAC", h.ToInt(NaturalAC_tb));
        sql.AddVariable("@DodgeAC", h.ToInt(DodgeAC_tb));
        sql.AddVariable("@DeflectAC", h.ToInt(DeflectAC_tb));
        sql.AddVariable("@MiscAC", h.ToInt(MiscAC_tb));

        sql.AddVariable("@Str", h.ToInt(Str_tb, 6));
        sql.AddVariable("@Dex", h.ToInt(Dex_tb, 6));
        sql.AddVariable("@Con", h.ToInt(Con_tb, 6));
        sql.AddVariable("@Int", h.ToInt(Int_tb, 6));
        sql.AddVariable("@Wis", h.ToInt(Wis_tb, 6));
        sql.AddVariable("@Cha", h.ToInt(Cha_tb, 6));

        sql.AddVariable("@HP", h.ToInt(Hp_tb, 1));
        sql.AddVariable("@XP", h.ToInt(Xp_tb));
        sql.AddVariable("@CP", h.ToInt(Copper_tb));
        sql.AddVariable("@SP", h.ToInt(Silver_tb));
        sql.AddVariable("@GP", h.ToInt(Gold_tb));
        sql.AddVariable("@PP", h.ToInt(Platinum_tb));
        sql.AddVariable("@SkillRanks", h.ToInt(SkillRanks_tb));
        sql.AddVariable("@Notes", Notes.Text);

        int charID = sql.ExecReturnID();
        CharacterSkills.AddCharacterSkills(charID, Request.QueryString["edition"].Replace('.','_'));
        Response.Redirect("Default.aspx");
    }
}
