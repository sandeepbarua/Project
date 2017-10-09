using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PresentationLayer
{
    public partial class DummyWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "2010-05-2007";


            //LoadTextBoxControl();
            //BindRepater();
          
        }

        private void BindRepater()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list.Add(1, "A");
            list.Add(2, "B");
            list.Add(3, "C");
            list.Add(4, "D");
            rpt.DataSource = list;
            rpt.DataBind();

        }



        protected void btn_Click(object sender, EventArgs e)
        {
            Response.Write("Index" + rb.SelectedIndex);
            Response.Write("Value" + rb.SelectedValue);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ViewState["count"] != null)
            {
                count = Convert.ToInt32(ViewState["count"]);
            }
            count++;
            ViewState["count"] = count;
            TextBox txt = new TextBox();
            txt.ID = "txt" + count.ToString();
            dvDynamciTextBox.Controls.Add(txt);
            dvDynamciTextBox.Controls.Add(new LiteralControl("<br />"));

        }

        private void LoadTextBoxControl()
        {
           
            int count = 0;
            if (ViewState["count"] == null)
            {
                return;
            }
            else
            {
                count = Convert.ToInt32(ViewState["count"]);
            }
            for (var j = 1; j <= count; j++)
            {
                TextBox txt = new TextBox();
                txt.ID ="txt"+ j.ToString();
                dvDynamciTextBox.Controls.Add(txt);
                dvDynamciTextBox.Controls.Add(new LiteralControl("<br />"));

               
            }
        }



        protected void btnNext_Click(object sender, EventArgs e)
        {
            lbl1.Text = string.Empty;
            int count = 0;
            if (ViewState["count"] != null)
            {
                count = Convert.ToInt32(ViewState["count"]);
            }
            for (var j = 1; j <= count; j++)
            {
                string strTextBoxId= "txt" + j.ToString();
                var txt = ((TextBox)(dvDynamciTextBox.FindControl(strTextBoxId))).Text;
                lbl1.Text += txt;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ViewState["count"] != null)
            {
                count = Convert.ToInt32(ViewState["count"]);
            }
            string strTextBoxId = "txt" + count.ToString();
            var txt = (TextBox)(dvDynamciTextBox.FindControl(strTextBoxId));
            dvDynamciTextBox.Controls.Remove(txt);
            count--;
            ViewState["count"] = count;
           // LoadTextBoxControl();
        }

        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="Add")
            {
               HtmlGenericControl hc=(HtmlGenericControl) e.Item.FindControl("dvRptDynamicTextBox");
                TextBox txt = new TextBox();
                txt.ID = "txt";
                hc.Controls.Add(txt);
                hc.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }
}