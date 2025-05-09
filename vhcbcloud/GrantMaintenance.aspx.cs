﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class GrantMaintenance : System.Web.UI.Page
    {
        string Pagename = "GrantMaintenance";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            if (!IsPostBack)
            {
                BindControls();
            }
        }

        private void BindControls()
        {
            BindLookUP(ddlProgramSearch, 34);
            BindLookUP(ddlGrantAgencySearch, 112);
            BindLookUP(ddlGrantingAgency, 112);
            LoadGrantorContact(ddlGrantorContact);
            LoadGrantorContact(ddlGrantor);
            BindLookUP(ddlGrantType, 133);
            LoadStaff();
            BindLookUP(ddlProgram, 34);

            LoadFundNames();
            LoadFundNumbers();
            BindLookUP(ddlFyYear, 136);
            BindLookUP(ddlMilestone, 34);
        }

        private void LoadGrantorContact(DropDownList ddList)
        {
            ddList.DataSource = FinancialTransactions.GetDataTableByProcName("GetContactUsers"); ;
            ddList.DataValueField = "contactid";
            ddList.DataTextField = "name";
            ddList.DataBind();
            ddList.Items.Insert(0, new ListItem("Select", "NA"));
        }

        private void LoadFundNames()
        {
            try
            {
                ddlFundName.DataSource = FundMaintenanceData.GetFundName(cbActiveOnly.Checked);
                ddlFundName.DataValueField = "fundid";
                ddlFundName.DataTextField = "name";
                ddlFundName.DataBind();
                ddlFundName.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void LoadFundNumbers()
        {
            try
            {
                ddlFund.DataSource = FundMaintenanceData.GetFundNumbers(cbActiveOnly.Checked);
                ddlFund.DataValueField = "fundid";
                ddlFund.DataTextField = "account";
                ddlFund.DataBind();
                ddlFund.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void LoadStaff()
        {
            ddlStaff.DataSource = FinancialTransactions.GetDataTableByProcName("GetStaffUsers"); ;
            ddlStaff.DataValueField = "userid";
            ddlStaff.DataTextField = "name";
            ddlStaff.DataBind();
            ddlStaff.Items.Insert(0, new ListItem("Select", "NA"));
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

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrantInfoGrid();
        }

        protected void btnGrantSearch_Click(object sender, EventArgs e)
        {
            BindGrantInfoGrid();
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

        private void BindGrantInfoGrid()
        {
            dvNewAttachedFunds.Visible = false;
            dvNewFyAmounts.Visible = false;
            dvNewMilestones.Visible = false;

            try
            {
                DataTable dt = GrantMaintenanceData.SearchGrantInfo(txtVHCBNameSearch.Text, DataUtils.GetInt(ddlProgramSearch.SelectedValue.ToString()),
                    DataUtils.GetInt(ddlGrantAgencySearch.SelectedValue.ToString()), DataUtils.GetInt(ddlGrantor.SelectedValue.ToString()), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvGrantInfoGrid.Visible = true;
                    gvGrantInfo.DataSource = dt;
                    gvGrantInfo.DataBind();
                    Session["dtAppraisalInfoList"] = dt;
                }
                else
                {
                    dvGrantInfoGrid.Visible = false;
                    gvGrantInfo.DataSource = null;
                    gvGrantInfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindOccupantsGrid", "", ex.Message);
            }
        }

        protected void gvGrantInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGrantInfo.EditIndex = e.NewEditIndex;
            BindGrantInfoGrid();
        }

        protected void gvGrantInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGrantInfo.EditIndex = -1;
            BindGrantInfoGrid();
            ClearGrantInfoForm();
            hfGrantinfoID.Value = "";
            btnGrantInfo.Text = "Add";
        }

        private void ClearGrantInfoForm()
        {
            cbAddGrantInfo.Checked = false;

            txtVHCBName.Text = "";
            txtAwardAmt.Text = "";
            txtBeginDate.Text = "";
            txtEndDate.Text = "";
            ddlGrantingAgency.SelectedIndex = -1;
            txtGrantName.Text = "";
            ddlGrantorContact.SelectedIndex = -1;
            txtAwardNum.Text = "";
            txtCFDANum.Text = "";
            ddlGrantType.SelectedIndex = -1;
            ddlStaff.SelectedIndex = -1;
            ddlProgram.SelectedIndex = -1;
            //cbFederalFunds.Checked = false;
            cbAdmin.Checked = false;
            cbMatch.Checked = false;
            //cbFundsReceived.Checked = false;
            cbFundActive.Checked = true;
            cbFundActive.Enabled = false;
        }

        protected void gvGrantInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[6].Controls[0].Visible = false;

                        Label lblGrantinfoID = e.Row.FindControl("lblGrantinfoID") as Label;
                        PopulateGrantInfo(DataUtils.GetInt(lblGrantinfoID.Text));
                        hfGrantinfoID.Value = lblGrantinfoID.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvGrantInfo_RowDataBound", "", ex.Message);
            }
        }

        private void PopulateGrantInfo(int grantinfoID)
        {
            btnGrantInfo.Text = "Update";
            cbAddGrantInfo.Checked = true;

            DataRow dr = GrantMaintenanceData.GetGrantInfo(grantinfoID);
            txtVHCBName.Text = dr["VHCBName"].ToString() ?? "";
            txtAwardAmt.Text = dr["AwardAmt"].ToString() ?? "";
            txtBeginDate.Text = dr["BeginDate"].ToString() == "" ? "" : Convert.ToDateTime(dr["BeginDate"].ToString()).ToShortDateString();
            txtEndDate.Text = dr["EndDate"].ToString() == "" ? "" : Convert.ToDateTime(dr["EndDate"].ToString()).ToShortDateString();
            PopulateDropDown(ddlGrantingAgency, dr["LkGrantAgency"].ToString());
            txtGrantName.Text = dr["GrantName"].ToString() ?? "";
            PopulateDropDown(ddlGrantorContact, dr["ContactID"].ToString());
            txtAwardNum.Text = dr["AwardNum"].ToString() ?? "";
            txtCFDANum.Text = dr["CFDA"].ToString() ?? "";
            PopulateDropDown(ddlGrantType, dr["LkGrantSource"].ToString());
            PopulateDropDown(ddlStaff, dr["Staff"].ToString());
            PopulateDropDown(ddlProgram, dr["Program"].ToString());
            //cbFederalFunds.Checked = DataUtils.GetBool(dr["FedFunds"].ToString());
            cbAdmin.Checked = DataUtils.GetBool(dr["Admin"].ToString());
            txtAdminAmt.Text = dr["Adminamt"].ToString();
            cbMatch.Checked = DataUtils.GetBool(dr["Match"].ToString());
            txtMatchAmt.Text = dr["MatchAmt"].ToString();
            //cbFundsReceived.Checked = DataUtils.GetBool(dr["Fundsrec"].ToString());
            cbFundActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
            chkDrawDown.Checked = DataUtils.GetBool(dr["DrawDown"].ToString()); 
            cbFundActive.Enabled = true;
        }

        private void PopulateDropDown(DropDownList ddl, string DBSelectedvalue)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedvalue == item.Value.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }

        protected void rdBtnSelectGrantinfo_CheckedChanged(object sender, EventArgs e)
        {
            int GrantInfoID = GetGrantInfoSelectedRecordID(gvGrantInfo);
            PopulateGrantInfo(GrantInfoID);
            hfGrantinfoID.Value = GrantInfoID.ToString();
            dvNewAttachedFunds.Visible = true;
            BindAttachFundGrid();
            dvNewFyAmounts.Visible = true;
            BindFYAmt();
            dvNewMilestones.Visible = true;
            BindMilestones();
        }

        private int GetGrantInfoSelectedRecordID(GridView gvGrantInfo)
        {
            int GrantInfoID = 0;
            //hfSelectedAppraisalTotalCost.Value = "";

            for (int i = 0; i < gvGrantInfo.Rows.Count; i++)
            {
                RadioButton rbAppraisalInfo = (RadioButton)gvGrantInfo.Rows[i].Cells[0].FindControl("rdBtnSelectGrantinfo");
                if (rbAppraisalInfo != null)
                {
                    if (rbAppraisalInfo.Checked)
                    {
                        HiddenField hf = (HiddenField)gvGrantInfo.Rows[i].Cells[0].FindControl("HiddenGrantinfoID");
                        //HiddenField hf1 = (HiddenField)gvAppraisalInfo.Rows[i].Cells[0].FindControl("HiddenAppraisalTotalCost");

                        if (hf != null)
                        {
                            GrantInfoID = DataUtils.GetInt(hf.Value);
                            //hfSelectedAppraisalTotalCost.Value = hf1.Value;
                        }
                        break;
                    }
                }
            }
            return GrantInfoID;
        }

        protected void btnAddAttachFund_Click(object sender, EventArgs e)
        {
            EntityMaintResult objEntityMaintResult = GrantMaintenanceData.AddFundToGrantInfo((DataUtils.GetInt(hfGrantinfoID.Value)),
                   DataUtils.GetInt(ddlFund.SelectedValue.ToString()));

            ddlFund.SelectedIndex = -1;
            ddlFundName.SelectedIndex = -1;
            cbAddAttachedFunds.Checked = false;

            BindAttachFundGrid();

            if (objEntityMaintResult.IsDuplicate && !objEntityMaintResult.IsActive)
                LogMessage("Fund already attached as in-active");
            else if (objEntityMaintResult.IsDuplicate)
                LogMessage("Fund already attached");
            else
                LogMessage("Fund Attached Successfully");
        }

        private void BindAttachFundGrid()
        {
            try
            {
                DataTable dt = GrantMaintenanceData.GetFundGrantinfoList(DataUtils.GetInt(hfGrantinfoID.Value),
                    cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvAttachedFundsGrid.Visible = true;
                    gvAttachedFunds.DataSource = dt;
                    gvAttachedFunds.DataBind();
                }
                else
                {
                    dvAttachedFundsGrid.Visible = false;
                    gvAttachedFunds.DataSource = null;
                    gvAttachedFunds.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAttachFundGrid", "", ex.Message);
            }
        }

        protected void gvAttachedFunds_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAttachedFunds.EditIndex = e.NewEditIndex;
            BindAttachFundGrid();
        }

        protected void gvAttachedFunds_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAttachedFunds.EditIndex = -1;
            BindAttachFundGrid();
        }

        protected void gvAttachedFunds_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAttachedFunds.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            GrantMaintenanceData.UpdateFundGrantinfo(DataUtils.GetInt(hfGrantinfoID.Value), RowIsActive);

            gvAttachedFunds.EditIndex = -1;

            BindAttachFundGrid();

            LogMessage("Attached Funds Updated successfully");
        }

        protected void gvAttachedFunds_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlFundName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDown(ddlFund, ddlFundName.SelectedValue);
            //ClearForm();
        }

        protected void ddlFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDown(ddlFundName, ddlFund.SelectedValue);
            //ClearForm();
        }

        protected void gvFyAmounts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFyAmounts.EditIndex = e.NewEditIndex;
            BindFYAmt();
        }

        protected void gvFyAmounts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFyAmounts.EditIndex = -1;
            BindFYAmt();
        }

        protected void gvFyAmounts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int GrantInfoFYId = DataUtils.GetInt(((Label)gvFyAmounts.Rows[rowIndex].FindControl("lblGrantInfoFY")).Text);
            decimal fyAmount = DataUtils.GetDecimal(Regex.Replace(((TextBox)gvFyAmounts.Rows[rowIndex].FindControl("txtFyAmount")).Text, "[^0-9a-zA-Z.]+", ""));
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvFyAmounts.Rows[rowIndex].FindControl("chkActive")).Checked);

            GrantMaintenanceData.UpdateGrantinfoFYAmt(GrantInfoFYId, fyAmount, RowIsActive);
            gvFyAmounts.EditIndex = -1;

            BindFYAmt();

            LogMessage("Fiscal Year Amount updated successfully");
        }

        private void BindFYAmt()
        {
            try
            {
                DataTable dt = GrantMaintenanceData.GetGrantinfoFYAmtList(DataUtils.GetInt(hfGrantinfoID.Value),
                    cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvFyAmountsGrid.Visible = true;
                    gvFyAmounts.DataSource = dt;
                    gvFyAmounts.DataBind();
                }
                else
                {
                    dvFyAmountsGrid.Visible = false;
                    gvFyAmounts.DataSource = null;
                    gvFyAmounts.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindFYAmt", "", ex.Message);
            }
        }

        protected void btnGrantInfo_Click(object sender, EventArgs e)
        {
            if (txtVHCBName.Text.Trim() == "")
            {
                LogMessage("Enter VHCB Name");
                txtVHCBName.Focus();
                return;
            }

            if (txtGrantName.Text.Trim() == "")
            {
                LogMessage("Enter Grant Name");
                txtGrantName.Focus();
                return;
            }

            if (btnGrantInfo.Text == "Add")
            {
                DBResult objResult = GrantMaintenanceData.AddGrantInfo(txtVHCBName.Text, DataUtils.GetDecimal(Regex.Replace(txtAwardAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    DataUtils.GetDate(txtBeginDate.Text), DataUtils.GetDate(txtEndDate.Text), DataUtils.GetInt(ddlGrantingAgency.SelectedValue.ToString()),
                    txtGrantName.Text, DataUtils.GetInt(ddlGrantorContact.SelectedValue.ToString()), txtAwardNum.Text, txtCFDANum.Text,
                    DataUtils.GetInt(ddlGrantType.SelectedValue.ToString()), DataUtils.GetInt(ddlStaff.SelectedValue.ToString()),
                    DataUtils.GetInt(ddlProgram.SelectedValue.ToString()), 
                    cbAdmin.Checked,
                    DataUtils.GetDecimal(Regex.Replace(txtAdminAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    cbMatch.Checked, 
                    DataUtils.GetDecimal(Regex.Replace(txtMatchAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    chkDrawDown.Checked);

                ClearGrantInfoForm();
                BindGrantInfoGrid();

                if (objResult.IsDuplicate && !objResult.IsActive)
                    LogMessage("Grant Info already exist as in-active");
                else if (objResult.IsDuplicate)
                    LogMessage("Grant Info already exist");
                else
                    LogMessage("Grant Info Added Successfully");
            }
            else
            {
                GrantMaintenanceData.UpdateGrantInfo((DataUtils.GetInt(hfGrantinfoID.Value)),
                    txtVHCBName.Text, DataUtils.GetDecimal(Regex.Replace(txtAwardAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    DataUtils.GetDate(txtBeginDate.Text), DataUtils.GetDate(txtEndDate.Text), DataUtils.GetInt(ddlGrantingAgency.SelectedValue.ToString()),
                    txtGrantName.Text, DataUtils.GetInt(ddlGrantorContact.SelectedValue.ToString()), txtAwardNum.Text, txtCFDANum.Text,
                    DataUtils.GetInt(ddlGrantType.SelectedValue.ToString()), DataUtils.GetInt(ddlStaff.SelectedValue.ToString()),
                    DataUtils.GetInt(ddlProgram.SelectedValue.ToString()), cbAdmin.Checked, 
                    DataUtils.GetDecimal(Regex.Replace(txtAdminAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    cbMatch.Checked, 
                    DataUtils.GetDecimal(Regex.Replace(txtMatchAmt.Text, "[^0-9a-zA-Z.]+", "")),
                    chkDrawDown.Checked, 
                    cbFundActive.Checked);

                gvGrantInfo.EditIndex = -1;
                BindGrantInfoGrid();
                hfGrantinfoID.Value = "";
                ClearGrantInfoForm();
                btnGrantInfo.Text = "Add";

                LogMessage("Grant Info Updated Successfully");
            }
        }

        protected void gvMilestones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMilestones.EditIndex = e.NewEditIndex;
            BindMilestones();
        }

        protected void gvMilestones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMilestones.EditIndex = -1;
            BindMilestones();
            ClearMilestoneInfoForm();
            hfMilestoneID.Value = "";
            btnAddMileStone.Text = "Add";
        }

        protected void gvMilestones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnAddMileStone_Click(object sender, EventArgs e)
        {
            if (ddlMilestone.SelectedIndex == 0)
            {
                LogMessage("Select Milestone");
                ddlMilestone.Focus();
                return;
            }

            string URL = txtURL.Text;

            if (URL != "")
            {
                if (!URL.Contains("http") && !URL.Contains("fda"))
                    URL = "http://" + URL;
            }

            if (btnAddMileStone.Text == "Add")
            {
                GrantMaintenanceData.AddGrantMilestones(DataUtils.GetInt(hfGrantinfoID.Value),
                               DataUtils.GetInt(ddlMilestone.SelectedValue.ToString()), DataUtils.GetDate(txtMilestoneDate.Text),
                               txtNote.Text, URL);

                ddlMilestone.SelectedIndex = -1;
                cbAddMilestone.Checked = false;

                BindMilestones();
                ClearMilestoneInfoForm();
                LogMessage("Milestone Added Successfully");
            }
            else
            {
                GrantMaintenanceData.UpdateGrantMilestones(DataUtils.GetInt(hfMilestoneID.Value),
                               DataUtils.GetInt(ddlMilestone.SelectedValue.ToString()), DataUtils.GetDate(txtMilestoneDate.Text),
                               txtNote.Text, txtURL.Text, cbMilestoneActive.Checked);

                gvMilestones.EditIndex = -1;
                BindMilestones();
                hfMilestoneID.Value = "";
                ClearMilestoneInfoForm();
                btnAddMileStone.Text = "Add";

                LogMessage("Milestone Updated Successfully");
            }
        }

        private void ClearMilestoneInfoForm()
        {
            cbAddMilestone.Checked = false;

            ddlMilestone.SelectedIndex = -1;
            txtMilestoneDate.Text = "";
            txtNote.Text = "";
            txtURL.Text = "";
            cbMilestoneActive.Checked = true;
            cbMilestoneActive.Enabled = false;
        }

        private void BindMilestones()
        {
            try
            {
                DataTable dt = GrantMaintenanceData.GetGrantMilestonesList(DataUtils.GetInt(hfGrantinfoID.Value),
                    cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvMilestonesGrid.Visible = true;
                    gvMilestones.DataSource = dt;
                    gvMilestones.DataBind();
                }
                else
                {
                    dvMilestonesGrid.Visible = false;
                    gvMilestones.DataSource = null;
                    gvMilestones.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindMilestones", "", ex.Message);
            }
        }

        protected void gvMilestones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);
                    btnAddMileStone.Text = "Update";
                    cbAddMilestone.Checked = true;

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[7].Controls[0].Visible = false;

                        Label lblMilestoneGrantID = e.Row.FindControl("lblMilestoneGrantID") as Label;
                        DataRow dr = GrantMaintenanceData.GetGrantMilestone(DataUtils.GetInt(lblMilestoneGrantID.Text));

                        hfMilestoneID.Value = lblMilestoneGrantID.Text;

                        PopulateDropDown(ddlMilestone, dr["MilestoneID"].ToString());
                        txtMilestoneDate.Text = dr["Date"].ToString() == "" ? "" : Convert.ToDateTime(dr["Date"].ToString()).ToShortDateString();
                        txtURL.Text = dr["URL"].ToString() ?? "";
                        txtNote.Text = dr["Note"].ToString() ?? "";
                        cbMilestoneActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        cbMilestoneActive.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvMilestones_RowDataBound", "", ex.Message);
            }
        }

        protected void btnFyAmt_Click(object sender, EventArgs e)
        {
            DBResult objDBResult = GrantMaintenanceData.AddGrantinfoFYAmt((DataUtils.GetInt(hfGrantinfoID.Value)),
                   DataUtils.GetInt(ddlFyYear.SelectedValue.ToString()), DataUtils.GetDecimal(Regex.Replace(txtFyAmt.Text, "[^0-9a-zA-Z.]+", "")));
            
            ddlFyYear.SelectedIndex = -1;
            txtFyAmt.Text="";
            cbAddFyAmounts.Checked = false;

            BindFYAmt();

            if (objDBResult.IsDuplicate && !objDBResult.IsActive)
                LogMessage("Fiscal Year Amount already exist as in-active");
            else if (objDBResult.IsDuplicate)
                LogMessage("Fiscal Year Amount exist");
            else
                LogMessage("Fiscal Year Amount Added Successfully");
        }
    }
}