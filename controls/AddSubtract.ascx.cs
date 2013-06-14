using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/*

                    <uc:AddSub ID="SkillRanks" runat="server" />
                    <uc:AddSub ID="SkillMisc" runat="server" />


*/
public partial class controls_AddSubtract : DnDControl{
    public String outputTB;
    public int Changed { get; set; }
    public int Max { get; set; }
    public int Min { get; set; }
    public bool Clear { get; set; }

    public int Value{
        get { return h.ToInt(Value_tb);}
        set { Value_tb.Text = value + ""; }  
     }   

    public controls_AddSubtract(){
        Changed = 0;
    }

    protected void Page_Load(object sender, EventArgs e){
        if (!Page.IsPostBack){
            Clear = false;
            Value_tb.Text = Value + "";
        }
    }

    protected void Add_Click(object sender, EventArgs e){
        if (outputTB == null || outputTB == "")
            Value_tb.Text = h.ToInt(Value_tb) + 1 + "";
        else{
            TextBox tb = (TextBox)Page.FindControl(outputTB);
            tb.Text = h.ToInt(tb.Text) + h.ToInt(Value_tb.Text) + "";
        }
        Changed = h.ToInt(Value_tb);
        if (Clear)
            Value_tb.Text = "";
    }

    protected void Subtract_Click(object sender, EventArgs e){
        if (outputTB == null || outputTB == "")
            Value_tb.Text = h.ToInt(Value_tb.Text) - 1 + "";
        else{
            TextBox tb = (TextBox)Page.FindControl(outputTB);
            tb.Text = h.ToInt(tb.Text) - h.ToInt(Value_tb.Text) + "";
        }
        Changed = h.ToInt(Value_tb) * -1;
        if (Clear)
            Value_tb.Text = "";
    }
}