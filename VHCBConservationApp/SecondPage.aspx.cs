﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace VHCBConservationApp
{
    public partial class SecondPage : System.Web.UI.Page
    {
        string Pagename = "SecondPage";
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
                DataRow drPage2tDetails = ConservationApplicationData.GetConservationApplicationPage2(projectNumber);

                if (drPage2tDetails != null)
                {
                   // ZoningDistrict, MinLotSize, FrontageFeet, PublicWater, PublicSewer, EnrolledUseValue, AcresExcluded, AcresDerived, ExcludedLand, DeedMatch, SurveyRequired, DeedRestrictions
                    txtZoningDistrict.Text = drPage2tDetails["ZoningDistrict"].ToString();
                    txtMinLotSize.Text = drPage2tDetails["MinLotSize"].ToString();
                    txtFrontageFeet.Text = drPage2tDetails["FrontageFeet"].ToString();
                    cbPublicWater.Checked = DataUtils.GetBool(drPage2tDetails["PublicWater"].ToString());
                    cbPublicSewer.Checked = DataUtils.GetBool(drPage2tDetails["PublicSewer"].ToString());
                    cbEnrolledUseValue.Checked = DataUtils.GetBool(drPage2tDetails["EnrolledUseValue"].ToString());
                    txtAcresExcluded.Text = drPage2tDetails["AcresExcluded"].ToString();
                    PopulateDropDown(ddlAcresDerived, drPage2tDetails["AcresDerived"].ToString());
                    txtExcludedLand.Text = drPage2tDetails["ExcludedLand"].ToString();
                    txtDeedMatch.Text = drPage2tDetails["DeedMatch"].ToString();
                    PopulateDropDown(ddlSurveyRequired, drPage2tDetails["SurveyRequired"].ToString());
                    txtDeedRestrictions.Text = drPage2tDetails["DeedRestrictions"].ToString(); 
                }
            }
        }

        private void PopulateDropDown(DropDownList ddl, string DBSelectedvalue)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedvalue.Trim() == item.Value.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }

        private void LogError(string pagename, string method, string message, string error)
        {
            dvMessage.Visible = true;
            if (message == "")
            {
                lblErrorMsg.Text = Pagename + ": " + method + ": Error Message: " + error;
            }
            else
                lblErrorMsg.Text = Pagename + ": " + method + ": Message :" + message + ": Error Message: " + error;
        }

        private void saveData()
        {

            if (projectNumber != "")
            {

                ConservationApplicationData.ConservationApplicationPage2(projectNumber, txtZoningDistrict.Text, txtMinLotSize.Text, DataUtils.GetDecimal(txtFrontageFeet.Text), cbPublicWater.Checked, cbPublicSewer.Checked, cbEnrolledUseValue.Checked, DataUtils.GetDecimal(txtAcresExcluded.Text),
                    ddlAcresDerived.SelectedValue, txtExcludedLand.Text, txtDeedMatch.Text, ddlSurveyRequired.SelectedValue, txtDeedRestrictions.Text);


                LogMessage("Conservation Application Data Added Successfully");

                //Response.Redirect("#");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            saveData();
            ClientScript.RegisterStartupScript(this.GetType(),
                  "script", Helper.GetExagoURL(projectNumber, "Conservation Online Application"));
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();

            Response.Redirect("#");
        }

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("FirstPage.aspx");
        }
    }
}