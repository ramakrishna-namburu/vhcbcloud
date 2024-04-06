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
    public partial class ThirdPage : System.Web.UI.Page
    {
        string Pagename = "ThirdPage";
        string projectNumber = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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
                DataRow drPage3tDetails = ConserveNatRecData.GetConserveNatRecPage3(projectNumber);

                if (drPage3tDetails != null)
                {
                    txtExecSummary.Text = drPage3tDetails["ExecSummary"].ToString();
                }
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("FirstPage.aspx");
        }

        private void saveData()
        {
            if (projectNumber != "")
            {
                string strFarmerTransfer = string.Empty;

                //foreach (ListItem listItem in cblFarmerTransfer.Items)
                //{
                //    if (listItem.Selected == true)
                //    {
                //        if (strFarmerTransfer == string.Empty)
                //            strFarmerTransfer = listItem.Value;
                //        else
                //            strFarmerTransfer = strFarmerTransfer + ',' + listItem.Value;
                //    }
                //}

                //if (!DataUtils.GetBool(rdBtnSellorConvey.SelectedValue.Trim()))
                //{
                //    txtFarmerPlans.Text = "";
                //    strFarmerTransfer = string.Empty;
                //}

                ConserveNatRecData.ConserveNatRecPage3(projectNumber, txtExecSummary.Text);


                LogMessage("Conservation Application Data Added Successfully");

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("Page4New.aspx");
        }

        //protected void rdBtnSellorConvey_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rdBtnSellorConvey.SelectedItem.Value.Trim() == "Yes")
        //        txtSellorPlans.Visible = true;
        //    else
        //        txtSellorPlans.Visible = false;
        //}
        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }
    }
}