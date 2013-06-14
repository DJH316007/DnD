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

public partial class Stats_Pathfinder : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        CMD_tb.Text = 10 + DnDSession.BaseAttackHighest + DnDSession.AbilityMod("Str") +
        DnDSession.AbilityMod("Dex") + SizeMod(SizeType.Number(DnDSession.Size)) + "";
    }
    /*
    private void SetSkillsTable(){
        CharacterSkills.SetAbilityMods(DnDSession.GetAbilityModCurrent("Str"), DnDSession.GetAbilityModCurrent("Dex"), DnDSession.GetAbilityModCurrent("Con"), DnDSession.GetAbilityModCurrent("Int"), DnDSession.GetAbilityModCurrent("Wis"), DnDSession.GetAbilityModCurrent("Cha"));
        CharacterSkills.LoadCharacter(DnDSession.ID);
    }*/
}
