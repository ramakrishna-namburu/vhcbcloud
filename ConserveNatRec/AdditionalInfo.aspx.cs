using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace ConserveNatRec
{
    public partial class AdditionalInfo : System.Web.UI.Page
    {
        string Pagename = "AdditionalInfo";
        string projectNumber = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            hfProjectId.Value = "0";

            if (Session["ProjectNumber"] == null)
                Response.Redirect("Login.aspx");
            else
                projectNumber = Session["ProjectNumber"].ToString();


            if (!IsPostBack)
            {

                LoadPage();
            }

        }
        private void LoadPage()
        {
            if (projectNumber != "")
            {
                DataRow dr = ConservationApplicationData.GetAdditionalInfo(projectNumber);

                if (dr != null)
                {
                    txtDualGoals.Text = dr["DualGoals"].ToString();
                    txtAdditionalInfo.Text = dr["AdditionalInfo"].ToString();
                   
                }
            }
        }

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("TownPlaning.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("Attachments.aspx");
        }
        private void saveData()
        {
            ConservationApplicationData.AdditionalInfo(projectNumber,txtDualGoals.Text, txtAdditionalInfo.Text);

            LogMessage("Conservation Form Data Added Successfully");
        }

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }
    }
}