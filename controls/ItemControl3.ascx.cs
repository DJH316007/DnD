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

public partial class controls_EquipControl : DnDControl{
    private int bodyPart;
    private DataTable itemData = null;
    protected void Page_Load(object sender, EventArgs e){
    }

    private void SetData(){
        DnDBO dBO = new DnDBO();
        itemData = dBO.GetItems(DnDSession.ID, bodyPart);
        if (!Page.IsPostBack)
            DisplayItem();
    }

    public void SetBodyPart(int part){
        bodyPart = part;
        SetData();
    }
    public string GetBodyPartName(){
        return BodyPart.Text(bodyPart);
    }

    private void ClearFields(){
        EquipedCheck.Checked = false;
        NameText.Text = "";
        NotesText.Text = "";
        ClassifiedText.Text = "";//change to category
        WeightText.Text = "";
        SpellFailureText.Text = "";
        Enhance.Text = "";
        AttackBonusText.Text = "";
        DamageText.Text = "";
        CritText.Text = "";
        RangeText.Text = "";
        ACBonusText.Text = "";
        ACCheckText.Text = "";
        MaxDexText.Text = "";
        MaxSpeedText.Text = "";       
    }

    protected void DisplayItem(){
        int currItem = DnDSession.GetCurrItem(bodyPart);
        if (itemData.Rows.Count <= 0){
            return;
        }
        if (currItem < 0)
            DnDSession.SetCurrItem(bodyPart, itemData.Rows.Count - 1);
        else if (currItem >= itemData.Rows.Count)
            DnDSession.SetCurrItem(bodyPart, 0);
        currItem = DnDSession.GetCurrItem(bodyPart);

        ClearFields();

        Helper h = new Helper();
        DataRow item = itemData.Rows[currItem];

        //add range penatly?
        string attBonus = "";
        int attBonusAdd =  h.ToInt(item["Enhance"]);
        if (StrDmg.Checked) attBonusAdd += DnDSession.AbilityBaseMod("Str");
        if (DexDmg.Checked) attBonusAdd += DnDSession.AbilityBaseMod("Dex");
        ExtraDmg.Text = "+ " + attBonusAdd;
        attBonusAdd += SizeType.Number(DnDSession.Size);

        string[] attacks = DnDSession.BaseAttack.Split('/');
        foreach (string s in attacks){
            attBonus +="+" + (h.ToInt(s) + attBonusAdd) + "/";
        }
        attBonus = attBonus.Remove(attBonus.Length - 1);

        EquipedCheck.Checked = h.ToBool(item["Equiped"]);
        NameText.Text = item["ItemName"].ToString();
        NotesText.Text = item["Notes"].ToString();
        ClassifiedText.Text = item["Classified"].ToString();
        WeightText.Text = item["Weight"].ToString();
        SpellFailureText.Text = item["SpellFailure"].ToString();

        StrDmg.Checked = h.ToBool(item["StrDmg"]);
        DexDmg.Checked = h.ToBool(item["DexDmg"]);
        Enhance.Text = item["Enhance"].ToString();
        AttackBonusText.Text = attBonus;
        DamageText.Text = item["Damage"].ToString();
        CritText.Text = item["Crit"].ToString();
        RangeText.Text = item["AttackRange"].ToString();

        ACBonusText.Text = item["ACBonus"].ToString();
        ACCheckText.Text = item["ACCheck"].ToString();
        MaxDexText.Text = item["MaxDex"].ToString();
        MaxSpeedText.Text = item["MaxSpeed"].ToString();
    }

    protected void PrevItem_Click(object sender, EventArgs e){
        DnDSession.SetCurrItem(bodyPart, DnDSession.GetCurrItem(bodyPart) - 1);
        DisplayItem();
    }
    protected void NextItem_Click(object sender, EventArgs e){
        DnDSession.SetCurrItem(bodyPart, DnDSession.GetCurrItem(bodyPart) + 1);
        DisplayItem();
    }

    protected void Save_Click(object sender, EventArgs e){
        int currItem = DnDSession.GetCurrItem(bodyPart);
        DnDBO dBO = new DnDBO();
       
        Helper h = new Helper();
        DataRow item = itemData.Rows[currItem];
        dBO.UpdateItem(h.ToInt(item["ID"]), DnDSession.ID, item["ImagePath"].ToString(), bodyPart, h.ToInt(item["ItemType"]),
         EquipedCheck.Checked, NameText.Text, NotesText.Text, ClassifiedText.Text, h.ToFloat(WeightText), h.ToInt(SpellFailureText),
         StrDmg.Checked, DexDmg.Checked, h.ToInt(Enhance), DamageText.Text, CritText.Text, h.ToInt(RangeText),
         h.ToInt(ACBonusText), h.ToInt(ACCheckText), h.ToInt(MaxDexText), h.ToInt(MaxSpeedText));
        SetData();
        DisplayItem();
    }
    protected void Delete_Click(object sender, EventArgs e){
        int currItem = DnDSession.GetCurrItem(bodyPart);
        DnDBO dBO = new DnDBO();
        if (currItem >= 0)
            dBO.DeleteItem(Int32.Parse(itemData.Rows[currItem]["ID"].ToString()));
        DnDSession.SetCurrItem(bodyPart, currItem++);
        SetData();
        DisplayItem();
    }

    protected void New_Click(object sender, EventArgs e){
        DnDBO dBO = new DnDBO();
//        dBO.AddItem(DnDSession.ID, "", bodyPart, ItemType.BASIC);
        DnDSession.SetCurrItem(bodyPart, DnDSession.GetCurrItem(bodyPart) + 1);
        SetData();
        DisplayItem();
    }

    protected void General_Click(object sender, EventArgs e){
        General.Visible = true;
        Attack.Visible = false;
        Defense.Visible = false;
    }
    protected void Attack_Click(object sender, EventArgs e){
        General.Visible = false;
        Attack.Visible = true;
        Defense.Visible = false;
    }
    protected void Defense_Click(object sender, EventArgs e){
        General.Visible = false;
        Attack.Visible = false;
        Defense.Visible = true;
    }
}
