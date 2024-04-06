using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace VHCBConservationFarm
{
    public partial class TownPlaning : System.Web.UI.Page
    {
        string Pagename = "TownPlaning";
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
                DataRow dr = ConservationApplicationData.GetTownPlaning(projectNumber);

                if (dr != null)
                {
                    txtZoningDistrict.Text = dr["ZoningDistrict"].ToString();
                    txtMinLotSize.Text = dr["MinLotSize"].ToString();
                    txtFrontageFeet.Text = dr["FrontageFeet"].ToString();

                    if (DataUtils.GetBool(dr["PublicWater"].ToString()))
                        rdbtnPublicWater.SelectedIndex = 0;
                    else
                        rdbtnPublicWater.SelectedIndex = 1;

                    if (DataUtils.GetBool(dr["PublicSewer"].ToString()))
                        rdbtnPublicSewer.SelectedIndex = 0;
                    else
                        rdbtnPublicSewer.SelectedIndex = 1;

                    if (DataUtils.GetBool(dr["EnrolledUseValue"].ToString()))
                        rdbtnEnrolledUseValue.SelectedIndex = 0;
                    else
                        rdbtnEnrolledUseValue.SelectedIndex = 1;


                 
                    txtAcresExcluded.Text = dr["AcresExcluded"].ToString();
                    txtAcresDerived.Text = dr["AcresDerived"].ToString();
                    txtExcludedLand.Text = dr["ExcludedLand"].ToString();
                    txtDeedMatch.Text = dr["DeedMatch"].ToString();

                    if (DataUtils.GetBool(dr["SurveyRequired"].ToString()))
                        rdbtnSurveyRequired.SelectedIndex = 0;
                    else
                        rdbtnSurveyRequired.SelectedIndex = 1;

                    if (DataUtils.GetBool(dr["RestrictiveCovenants"].ToString()))
                        rdbtnRestrictiveCovenants.SelectedIndex = 0;
                    else
                        rdbtnRestrictiveCovenants.SelectedIndex = 1;


                    txtDeedRestrictions.Text = dr["DeedRestrictions"].ToString();
                    txtNoLeverage.Text = dr["NoLeverage"].ToString();
                 
                    txtEndorsements.Text = dr["Endorsements"].ToString();
                    txtConformancePlans.Text = dr["ConformancePlans"].ToString();

                    foreach (ListItem li in cblPlanCommisionsInformed.Items)
                    {
                        if (dr["PlanCommisionsInformed"].ToString().Split(',').ToList().Contains(li.Value))
                        {
                            li.Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("EasementTerms.aspx");
        }

        private void saveData()
        {
            string LetterSentToList = string.Empty;

            foreach (ListItem listItem in cblPlanCommisionsInformed.Items)
            {
                if (listItem.Selected == true)
                {
                    if (LetterSentToList == string.Empty)
                    {
                        LetterSentToList = listItem.Value;
                    }
                    else
                    {
                        LetterSentToList = LetterSentToList + ',' + listItem.Value;
                    }
                }
            }

            ConservationApplicationData.TownPlaning(projectNumber, txtZoningDistrict.Text, txtMinLotSize.Text, DataUtils.GetDecimal(txtFrontageFeet.Text), DataUtils.GetBool(rdbtnPublicWater.SelectedValue), DataUtils.GetBool(rdbtnPublicSewer.SelectedValue),
                DataUtils.GetBool(rdbtnEnrolledUseValue.SelectedValue), DataUtils.GetDecimal(txtAcresExcluded.Text), txtAcresDerived.Text, txtExcludedLand.Text, txtDeedMatch.Text, DataUtils.GetBool(rdbtnSurveyRequired.SelectedValue),
                DataUtils.GetBool(rdbtnRestrictiveCovenants.SelectedValue),
                txtDeedRestrictions.Text, txtConformancePlans.Text, txtNoLeverage.Text, LetterSentToList, txtEndorsements.Text
                );


            LogMessage("Conservation Form Data Added Successfully");
        }

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("Additionalinfo.aspx");
        }

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }
    }
}