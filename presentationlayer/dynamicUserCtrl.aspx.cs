using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresentationLayer.UserControls;

namespace PresentationLayer
{
    public partial class dynamicUserCtrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
                BindRpt();
          
        }

        private void BindRpt()
        {
            List<Test> test = new List<Test>()
            {
                new Test { DocumentTypeId=1, DocumentName="Neeraj" },
                new Test { DocumentTypeId=2, DocumentName="Pankaj" },
                new Test { DocumentTypeId=3, DocumentName="Rinku" }
            };
            rpt.DataSource = test;
            rpt.DataBind();
        }

        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType ==ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
            {
                string id = ((HiddenField)e.Item.FindControl("hfDocumentTypeId")).Value;
                if (id == "1")
                {
                    Panel pnl = (Panel)e.Item.FindControl("pnl");
                    UsrAccidentRecordDetail usracc = (UsrAccidentRecordDetail)LoadControl("~/UserControls/UsrAccidentRecordDetail.ascx");
                    usracc.BindAccidentRecordDetails(1, 1, 1, 1);

                    usracc.ID = "usracc";
                    pnl.Controls.Add(usracc);
                }
                else if (id == "2")
                {
                    Panel pnl = (Panel)e.Item.FindControl("pnl");
                    UsrDriverLicenseStatus usracc = (UsrDriverLicenseStatus)LoadControl("~/UserControls/UsrDriverLicenseStatusDetail.ascx");

                    usracc.ID = "drv";
                    pnl.Controls.Add(usracc);

                    UsrDriverLicenceDetail dracc = (UsrDriverLicenceDetail)LoadControl("~/UserControls/UsrDriverLicenceDetail.ascx");
                    dracc.ID = "dracc";
                    pnl.Controls.Add(dracc);

                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rpt.Items)
            {

                string id = ((HiddenField)item.FindControl("hfDocumentTypeId")).Value;
                if (id == "1")
                {                   
                    var usracc = item.FindControl("usracc");
                    if (Session["AccidentRecordCount"] != null)
                    {
                        int count = Convert.ToInt32(Session["AccidentRecordCount"]);

                        for (int i = 1; i <= count; i++)
                        {
                            string str1 = "txt1" + i.ToString();
                            string str2 = "txt2" + i.ToString();
                            string str3 = "txt3" + i.ToString();
                            string str4 = "txt4" + i.ToString();

                            string txt1 = ((TextBox)usracc.FindControl(str1)).Text;
                            string txt2 = ((TextBox)usracc.FindControl(str2)).Text;
                            string txt3 = ((TextBox)usracc.FindControl(str3)).Text;
                            string txt4 = ((TextBox)usracc.FindControl(str4)).Text;
                            lbl1.Text = lbl1.Text + txt1+ txt2+ txt3+ txt4;
                        }
                    }

                }
                else if (id == "2")
                {
                  
                    var usracc = item.FindControl("drv");
                    foreach (Control ctrl in usracc.Controls)
                    {
                        TextBox txt = (TextBox)ctrl.FindControl("txt11");
                        lbl2.Text = lbl2.Text + txt.Text;

                    }

                }

            }
        }
    }

    public class Test
    {
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
    }
}