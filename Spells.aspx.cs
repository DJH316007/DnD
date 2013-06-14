using System;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Spells : DnDPage{
    protected void Page_Load(object sender, EventArgs e){
        int classNum = ClassType.Number(DnDSession.Class);
        if (classNum == ClassType.BARBARIAN || classNum == ClassType.FIGHTER || classNum == ClassType.ROGUE || classNum == ClassType.MONK)
            return;

        /*
                spells.SetStoredProcName("GetClericSpells");
        DataTable dt = spells.ExecReturnTable();
        //        DataTable dtr = spells.ExecReturnTable();
        //        dt.Merge(dtr);

        spells.SetStoredProcName("GetClassSpells");
        spells.AddVariable("@class", "Clr");
        //spells.AddVariable("@class", "Wiz");
        DataTable dt = spells.ExecReturnTable();
//        DataTable dtr = spells.ExecReturnTable();
//        dt.Merge(dtr);
 //       dt.Rows[4]['SpellName'];
        */


        sql.SetStoredProcName("GetClassSpells");
        sql.AddVariable("@class", ClassType.Abbr(classNum));





        DataTable dt = sql.ExecReturnTable();
        SpellTable.DataSource = dt;
        SpellTable.DataBind();
/*
        for (int i = 0; i < 10; i++){
            dt.DefaultView.RowFilter = "Level = " + i;//<> is not equal
            if (dt.DefaultView.Count != 0){
                switch (i){
                    case 0:
                        SpellTable0.DataSource = dt;
                        SpellTable0.DataBind();
                        break;
                    case 1:
                        SpellTable1.DataSource = dt;
                        SpellTable1.DataBind();
                        break;
                    case 2:
                        SpellTable2.DataSource = dt;
                        SpellTable2.DataBind();
                        break;
                    case 3:
                        SpellTable3.DataSource = dt;
                        SpellTable3.DataBind();
                        break;
                    case 4:
                        SpellTable4.DataSource = dt;
                        SpellTable4.DataBind();
                        break;
                    case 5:
                        SpellTable5.DataSource = dt;
                        SpellTable5.DataBind();
                        break;
                    case 6:
                        SpellTable6.DataSource = dt;
                        SpellTable6.DataBind();
                        break;
                    case 7:
                        SpellTable7.DataSource = dt;
                        SpellTable7.DataBind();
                        break;
                    case 8:
                        SpellTable8.DataSource = dt;
                        SpellTable8.DataBind();
                        break;
                    case 9:
                        SpellTable9.DataSource = dt;
                        SpellTable9.DataBind();
                        break;
                }
            }
        }
 */
    }

    private DataTable SpellsPerDay(){
        DataTable dt = null;



        return dt;
    }

    protected string SpellsPerDay(int spellLevel){

        return "-";
    }



    protected void SpellLevel_Click(object sender, EventArgs e){
        Button butt = (Button)sender;
        if (butt.BackColor == Color.YellowGreen)
            butt.BackColor = Color.Red;
        else
            butt.BackColor = Color.YellowGreen;
        UpdateList();
    }

    private void UpdateList(){
        DataTable dt = ((DataTable)(SpellTable.DataSource));
        string filter = "";
        if (Spells0Button.BackColor == Color.Red)
            filter += "Level <> 0";
        if (Spells1Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 1";
        }
        if (Spells2Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 2";
        }
        if (Spells3Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 3";
        }
        if (Spells4Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 4";
        }
        if (Spells5Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 5";
        }
        if (Spells6Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 6";
        }
        if (Spells7Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 7";
        }
        if (Spells8Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 8";
        }
        if (Spells9Button.BackColor == Color.Red){
            if (filter != "")
                filter += " AND ";
            filter += "Level <> 9";
        }
        
        if (filter != "")
            dt.DefaultView.RowFilter = filter;
        SpellTable.DataSource = dt;
        SpellTable.DataBind();
    }
}