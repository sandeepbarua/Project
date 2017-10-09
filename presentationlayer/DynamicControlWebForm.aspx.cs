using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PresentationLayer
{
    public partial class DynamicControlWebForm : System.Web.UI.Page
    {
        Dictionary<int, string> list = new Dictionary<int, string>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
                BindRepater();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (var li in list)
            {
                string dvControlId = "dv" + li.Key.ToString();
                HtmlGenericControl hc = (HtmlGenericControl)dvRptDynamicTextBox.FindControl(dvControlId);
                string strTxtParentId = "txt" + li.Key.ToString();
                var txtParent = (TextBox)(hc.FindControl(strTxtParentId));
                lbl.Text += txtParent.Text;

                int count = 0;
                string btnAddID = "btnAdd" + li.Key.ToString();
                if (ViewState[btnAddID] != null)
                {
                    count = Convert.ToInt32(ViewState[btnAddID]);
                }
                if (count > 0)
                {
                  
                    
                    for (int i = 1; i <= count; i++)
                    {
                       
                        string strTextBoxId = "txt" + i.ToString() + btnAddID;
                       
                        var txt = (TextBox)(hc.FindControl(strTextBoxId));
                        lbl.Text += txt.Text;
                    }

                }

            }
        }

        private void BindRepater()
        {
           
            list.Add(1, "A");
            list.Add(2, "B");
            list.Add(3, "C");
            list.Add(4, "D");
          
            foreach(var li in list)
            {
                HtmlGenericControl myDiv = new HtmlGenericControl("div");
                myDiv.ID ="dv"+ li.Key.ToString();

                Label lblText = new Label();
                lblText.ID="lblText"+ li.Key.ToString();
                lblText.Text = li.Value;
                myDiv.Controls.Add(lblText);
                myDiv.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

                Button btnAdd = new Button();
                btnAdd.ID = "btnAdd" + li.Key.ToString();
                btnAdd.Click += BtnAdd_Click;
                btnAdd.Text = "Add";
                myDiv.Controls.Add(btnAdd);

                Button btnRemove = new Button();
                btnRemove.ID = "btnRemove" + li.Key.ToString();
                btnRemove.Text = "Remove";
                btnRemove.Click += BtnRemove_Click;
                myDiv.Controls.Add(btnRemove);
                myDiv.Controls.Add(new LiteralControl("<br/>"));

                string strControlId = btnAdd.ID.Substring(6);
                int Id = Convert.ToInt32(strControlId);
               
                    int count = 0;
                    if (ViewState[btnAdd.ID] != null)
                    {
                        count = Convert.ToInt32(ViewState[btnAdd.ID]);
                    }
                    if (count > 0)
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            TextBox txtChild = new TextBox();
                            txtChild.ID = "txt" + i.ToString() + btnAdd.ID;
                            myDiv.Controls.Add(txtChild);
                        }

                    }

                dvRptDynamicTextBox.Controls.Add(new LiteralControl("<br/>"));

                TextBox txt = new TextBox();
                txt.ID = "txt" + li.Key.ToString();
                myDiv.Controls.Add(txt);


                dvRptDynamicTextBox.Controls.Add(myDiv);
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string buttonId = button.ID;
            int count = 0;

            string strControlId = button.ID.Substring(9);
            string dvControlId = "dv" + strControlId;
            string btnAddId = "btnAdd" + strControlId;

           
                if (ViewState[btnAddId] != null)
                {
                    count = Convert.ToInt32(ViewState[btnAddId]);
                }

            string strTextBoxId = "txt" + count.ToString() + btnAddId;
            HtmlGenericControl hc = (HtmlGenericControl)dvRptDynamicTextBox.FindControl(dvControlId);
            var txt = (TextBox)(hc.FindControl(strTextBoxId));
            hc.Controls.Remove(txt);


            count--;
            ViewState[btnAddId] = count;

         

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string buttonId = button.ID;
            int count = 0;
            if (ViewState[buttonId] != null)
            {
                count = Convert.ToInt32(ViewState[buttonId]);
            }
            count++;
            ViewState[buttonId] = count;
            TextBox txt = new TextBox();
            txt.ID = "txt" + count.ToString()+ buttonId;
           
            
            string strControlId = button.ID.Substring(6);
            string dvControlId = "dv" + strControlId;

            HtmlGenericControl hc=(HtmlGenericControl) dvRptDynamicTextBox.FindControl(dvControlId);

            hc.Controls.Add(txt);



        }

      

    }
}