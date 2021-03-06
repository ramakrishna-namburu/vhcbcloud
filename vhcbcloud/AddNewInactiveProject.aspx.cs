﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class AddNewInactiveProject : System.Web.UI.Page
    {

        string Pagename = "AddNewInactiveProject";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

            HandleCustomPostbackEvent(ctrlName, args);

            if (!IsPostBack)
            {
                BindControls();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtprojectNumber, "OnBlur");
            txtprojectNumber.Attributes.Add("onblur", onBlurScript);
        }

        private void HandleCustomPostbackEvent(string ctrlName, string args)
        {
            if (ctrlName == txtprojectNumber.UniqueID && args == "OnBlur")
            {
                SetProjectName();
            }
        }

        private void SetProjectName()
        {
            DataRow dr = InactiveProjectData.GetProjectNameByProjectNumber(txtprojectNumber.Text);

            if (dr != null)
                spnProjectName.InnerText = dr["ProjectName"].ToString();
            else
            {

                if (ddlProgram.SelectedValue != "NA")
                    spnProjectName.InnerHtml = ddlProgram.SelectedItem.Text;
                else
                    spnProjectName.InnerHtml = "";
            }
        }

        private void BindControls()
        {

            BindPrograms();
            BindLookUP(ddlApplication, 2283);
        }

        private void BindLookUP(DropDownList ddList, int LookupType)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = LookupValuesData.Getlookupvalues(LookupType);
                ddList.DataValueField = "typeid";
                ddList.DataTextField = "description";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindLookUP", "Control ID:" + ddList.ID, ex.Message);
            }
        }

        protected void BindPrograms()
        {
            try
            {
                ddlProgram.Items.Clear();
                ddlProgram.DataSource = InactiveProjectData.BindPrograms();
                ddlProgram.DataValueField = "ProgramType";
                ddlProgram.DataTextField = "Description";
                ddlProgram.DataBind();
                ddlProgram.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindManagers", "", ex.Message);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtprojectNumber.Text == "")
            {
                LogMessage("Enter Project Number");
                txtprojectNumber.Focus();
            }
            else
            {
                string ProjNumber = string.Empty;

                if (ddlProgram.SelectedValue == "9999")
                    ProjNumber = "9999-001-" + txtprojectNumber.Text;
                else
                    ProjNumber = txtprojectNumber.Text;

                InactiveProjectResult objInactiveProjectResult = InactiveProjectData.AddInactiveProject(ProjNumber, txtLoginName.Text, txtPassword.Text, DataUtils.GetInt(ddlApplication.SelectedValue), true);

                if (objInactiveProjectResult.IsDuplicate)
                    LogMessage("Project already exist");

                else
                    LogMessage("New Project added successfully");
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

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtprojectNumber.Text = "";
            txtLoginName.Text = "";
            txtPassword.Text = "";
            //cbActive.Checked = false;
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgram.SelectedValue == "9999") //Viability
                spnViabilityProjectPrefix.Visible = true;
            else
                spnViabilityProjectPrefix.Visible = false;


            SetProjectName();
        }
    }
}