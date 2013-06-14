using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class DnDBO{
	public DnDBO(){
	}
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="part"></param>
    /// <returns></returns>
    public DataTable GetItems(int owner, int part){
        SQLDatabase getItems = new SQLDatabase();
        getItems.SetStoredProcName("GetItemsOnBodyPart");
        getItems.AddVariable("@owner", owner);
        getItems.AddVariable("@part", part);
        return getItems.ExecReturnTable();
    }

    public void AddItem(int owner, string imagePath, int bodyPart, int type){
        SQLDatabase InsertItem = new SQLDatabase();
        InsertItem.SetStoredProcName("AddItem");
        InsertItem.AddVariable("@owner", owner);
        InsertItem.AddVariable("@imagePath", imagePath);
        InsertItem.AddVariable("@bodyPart", bodyPart);
        InsertItem.AddVariable("@type", type);
        InsertItem.Exec();
    }
    public void UpdateItem(int itemID, int owner, string imagePath, int bodyPart, int type, bool equiped,
     string name, string notes, string category, float weight, int spellFailure, 
     bool strDmg, bool dexDmg, int enhance, string damage, string crit, int range, int acBonus, int acCheck, int maxDex, int maxSpeed){
        SQLDatabase updateItem = new SQLDatabase();
        updateItem.SetStoredProcName("UpdateItem");
        updateItem.AddVariable("@itemID", itemID);
        updateItem.AddVariable("@owner", owner);
        updateItem.AddVariable("@imagePath", imagePath);
        updateItem.AddVariable("@bodyPart", bodyPart);
        updateItem.AddVariable("@type", type);

        updateItem.AddVariable("@equiped", equiped);
        updateItem.AddVariable("@name", name);
        updateItem.AddVariable("@notes", notes);
        updateItem.AddVariable("@classified", category);
        updateItem.AddVariable("@weight", weight);
        updateItem.AddVariable("@spellFailure", spellFailure);

        updateItem.AddVariable("@strDmg", strDmg);
        updateItem.AddVariable("@dexDmg", dexDmg);
        updateItem.AddVariable("@enhance", enhance);
        updateItem.AddVariable("@damage", damage);
        updateItem.AddVariable("@crit", crit);
        updateItem.AddVariable("@attackRange", range);

        updateItem.AddVariable("@acBonus", acBonus);
        updateItem.AddVariable("@acCheck", acCheck);
        updateItem.AddVariable("@maxDex", maxDex);
        updateItem.AddVariable("@maxSpeed", maxSpeed);

        updateItem.Exec();
    }
    public void DeleteItem(int id){
        SQLDatabase DelItem = new SQLDatabase();
        DelItem.SetStoredProcName("DeleteItem");
        DelItem.AddVariable("@ID", id);
        DelItem.Exec();
    }
    // used in CharacterInfo
    public DataTable GetEquipment(int id){
        SQLDatabase GetEquip = new SQLDatabase();
        GetEquip.SetStoredProcName("GetAllItems");
        GetEquip.AddVariable("@Index", id);
        return GetEquip.ExecReturnTable();
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="c"></param>
    /// <param name="lvl"></param>
    /// <returns></returns>












    public DataRow GetClassStats(int c, int lvl){
        SQLDatabase getStats = new SQLDatabase();
        getStats.SetStoredProcName("Get" + ClassType.Text(c) + "Stats");
        getStats.AddVariable("@Level", lvl);
        return getStats.ExecReturnTable().Rows[0];
    }

    public void AddSpell(string Name, string School, string Comp, string Time, string Range,
     string Duration, string Saving, string SpellRes, string Target, string Area, string Materials, string Focus, string Description,
     int Brd, int Clr, int Drd, int Pal, int Rgr, int Sor, int Wiz, int Chaos, int Good, int Law, int Evil, int Air, 
     int Water, int Earth, int Fire, int Animal, int Death, int Destruction, int Healing, int Know, int Luck, int Magic, 
     int Plant, int Protection, int Strength, int Sun, int Travel, int Trickery, int War){
        SQLDatabase AddSpell = new SQLDatabase();
        AddSpell.SetStoredProcName("AddSpell");
        AddSpell.AddVariable("@Name", Name);
        AddSpell.AddVariable("@School", School);
        AddSpell.AddVariable("@Comp", Comp);
        AddSpell.AddVariable("@Time", Time);
        AddSpell.AddVariable("@Range", Range);
        AddSpell.AddVariable("@Duration", Duration);
        AddSpell.AddVariable("@Saving", Saving);
        AddSpell.AddVariable("@SpellRes", SpellRes);
        AddSpell.AddVariable("@Target", Target);
        AddSpell.AddVariable("@Area", Area);
        AddSpell.AddVariable("@Materials", Materials);
        AddSpell.AddVariable("@Focus", Focus);
        AddSpell.AddVariable("@Description", Description);

        AddSpell.AddVariable("@Brd", Brd);
        AddSpell.AddVariable("@Clr", Clr);
        AddSpell.AddVariable("@Drd", Drd);
        AddSpell.AddVariable("@Pal", Pal);
        AddSpell.AddVariable("@Rgr", Rgr);
        AddSpell.AddVariable("@Sor", Sor);
        AddSpell.AddVariable("@Wiz", Wiz);
        AddSpell.AddVariable("@Chaos", Chaos);
        AddSpell.AddVariable("@Good", Good);
        AddSpell.AddVariable("@Law", Law);
        AddSpell.AddVariable("@Evil", Evil);
        AddSpell.AddVariable("@Air", Air);
        AddSpell.AddVariable("@Water", Water);
        AddSpell.AddVariable("@Earth", Earth);
        AddSpell.AddVariable("@Fire", Fire);
        AddSpell.AddVariable("@Animal", Animal);
        AddSpell.AddVariable("@Death", Death);
        AddSpell.AddVariable("@Destruction", Destruction);
        AddSpell.AddVariable("@Healing", Healing);
        AddSpell.AddVariable("@Know", Know);
        AddSpell.AddVariable("@Luck", Luck);
        AddSpell.AddVariable("@Magic", Magic);
        AddSpell.AddVariable("@Plant", Plant);
        AddSpell.AddVariable("@Protection", Protection);
        AddSpell.AddVariable("@Strength", Strength);
        AddSpell.AddVariable("@Sun", Sun);
        AddSpell.AddVariable("@Travel", Travel);
        AddSpell.AddVariable("@Trickery", Trickery);
        AddSpell.AddVariable("@War", War);
        AddSpell.Exec();
    }

    public void UpdateSkill(int id, int charID, string name, bool skilled, bool untrained, int acPenMulti, string ability, int ranks, int misc){
        SQLDatabase updateSkill = new SQLDatabase();
        updateSkill.SetStoredProcName("UpdateCharacterSkill");
        updateSkill.AddVariable("@id", id);
        updateSkill.AddVariable("@charID", charID);
        updateSkill.AddVariable("@name", name);
        updateSkill.AddVariable("@skilled", skilled);
        updateSkill.AddVariable("@untrained", untrained);
        updateSkill.AddVariable("@ACPenMulti", acPenMulti);
        updateSkill.AddVariable("@ability", ability);
        updateSkill.AddVariable("@ranks", ranks);
        updateSkill.AddVariable("@misc", misc);
        updateSkill.Exec();
    }
}
