using DataAccessLayer;
using Microsoft.AspNet.Identity;
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
    public partial class FundMaintenance : System.Web.UI.Page
    {
        string Pagename = "FundMaintenance";

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            if (!IsPostBack)
            {
                BindControls();
                BindEntityNotesGrid();

                CheckNewProjectAccess();
                hfIsVisibleBasedOnRole.Value = "true";
            }
        }

        private void CheckNewProjectAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "40394")
                    RoleReadOnly();
            }
        }

        protected bool GetIsVisibleBasedOnRole()
        {
            return DataUtils.GetBool(hfIsVisibleBasedOnRole.Value);
        }

        protected void RoleReadOnly()
        {
            hfIsVisibleBasedOnRole.Value = "false";
            cbAddFund.Enabled = false;
            btnSubmitFund.Visible = false;
            btnCancel.Visible = false;
            cbAddAttachedGrants.Enabled = false;
            btnAddAttachGrant.Visible = false;
            cbAddnotes.Enabled = false;
            btnAddNotes.Visible = false;
        }

        protected int GetUserId()
        {
            try
            {
                DataTable dtUser = ProjectCheckRequestData.GetUserByUserName(Context.User.Identity.GetUserName());
                return dtUser != null ? Convert.ToInt32(dtUser.Rows[0][0].ToString()) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private void BindControls()
        {
            LoadFundNames();
            LoadFundNumbers();

            BindLookUP(ddlSOVFundCode, 207);
            //BindLookUP(ddlAcctMethod, 129);
            BindLookUP(ddlSOVDeptId, 209);

            BindFundType();
            LoadVHCBGrantNames();
        }

        private void LoadVHCBGrantNames()
        {
            try
            {
                ddlGrantName.DataSource = FundTypeData.GetFundType("GetFundTypeData");
                ddlGrantName.DataValueField = "typeid";
                ddlGrantName.DataTextField = "description";
                ddlGrantName.DataBind();
                ddlGrantName.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void BindFundType()
        {
            try
            {
                ddlFundType.DataSource = FundTypeData.GetFundType("GetFundTypeData");
                ddlFundType.DataValueField = "typeid";
                ddlFundType.DataTextField = "description";
                ddlFundType.DataBind();
                ddlFundType.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
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

        private void LoadFundNumbers()
        {
            try
            {
                ddlAcctNum.DataSource = FundMaintenanceData.GetFundNumbers(cbActiveOnly.Checked);
                ddlAcctNum.DataValueField = "fundid";
                ddlAcctNum.DataTextField = "account";
                ddlAcctNum.DataBind();
                ddlAcctNum.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
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

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            ClearForm();
            BindControls();
            cbAddFund.Checked = false;
        }

        private void PopulateForm(DataRow dr)
        {
            if (dr != null)
            {
                ClearForm();

                //cbAddFund.Checked = true;
                dvfundf.Visible = true;
                dvFundForm.Visible = true;
                txtFundName.Text = dr["name"].ToString();
                txtAbbrev.Text = dr["abbrv"].ToString();
                PopulateDropDown(ddlFundType, dr["LkFundType"].ToString());
                txtFundNum.Text = dr["account"].ToString();
                PopulateDropDown(ddlSOVFundCode, dr["VHCBCode"].ToString());
                txtMIPFundNo.Text = dr["MIPFundnum"].ToString() == "0"? "" : dr["MIPFundnum"].ToString();
                //PopulateDropDown(ddlAcctMethod, dr["LkAcctMethod"].ToString());
                PopulateDropDown(ddlSOVDeptId, dr["DeptID"].ToString());
                cbMitFund.Checked = DataUtils.GetBool(dr["MitFund"].ToString());
                cbFundActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                cbFundActive.Enabled = true;
                cbSecondapproval.Checked = DataUtils.GetBool(dr["Secondapproval"].ToString());

                //txtFundName.Enabled = false;

                btnSubmitFund.Text = "Update";
            }
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

        private void ClearForm()
        {
            txtFundName.Enabled = true;
            txtFundName.Text = "";
            txtAbbrev.Text = "";
            ddlFundType.SelectedIndex = -1;
            txtFundNum.Text = "";
            ddlSOVFundCode.SelectedIndex = -1;
            //ddlAcctMethod.SelectedIndex = -1;
            txtMIPFundNo.Text = "";
            ddlSOVDeptId.SelectedIndex = -1;
            cbFundActive.Checked = true;
            cbFundActive.Enabled = false;
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

        protected void btnSubmitFund_Click(object sender, EventArgs e)
        {
            if (txtFundName.Text == "")
            {
                LogMessage("Please Enter Fund Name");
                txtFundName.Focus();
                return;
            }
            if (ddlFundType.SelectedIndex == 0)
            {
                LogMessage("Please Select Fund Type");
                ddlFundType.Focus();
                return;
            }
            if (txtFundNum.Text == "")
            {
                LogMessage("Please Enter Fund Number");
                txtFundNum.Focus();
                return;
            }

            if (btnSubmitFund.Text.ToLower() == "update")
            {
                FundMaintenanceData.UpdateFund(Convert.ToInt32(ddlFundName.SelectedValue.ToString()), txtAbbrev.Text,
                    Convert.ToInt32(ddlFundType.SelectedValue.ToString()),
                    txtFundNum.Text, txtMIPFundNo.Text == "" ? 0 : Convert.ToInt32(txtMIPFundNo.Text),
                    ddlSOVDeptId.SelectedValue.ToString(), ddlSOVFundCode.SelectedValue?.ToString(), cbFundActive.Checked, 
                    cbMitFund.Checked, cbSecondapproval.Checked, txtFundName.Text);

                LogMessage("Fund updated successfully");
            }
            else
            {
                AddFund objAddFund = FundMaintenanceData.AddFund(txtFundName.Text, txtAbbrev.Text,
                    Convert.ToInt32(ddlFundType.SelectedValue?.ToString()),
                    txtFundNum.Text, txtMIPFundNo.Text == "" ? 0 : Convert.ToInt32(txtMIPFundNo.Text),
                    ddlSOVDeptId.SelectedValue.ToString(), ddlSOVFundCode.SelectedValue?.ToString(), 
                    cbMitFund.Checked, cbSecondapproval.Checked);

                if (objAddFund.IsDuplicate && !objAddFund.IsActive)
                    LogMessage("Fund already exist as in-active");
                else if (objAddFund.IsDuplicate)
                    LogMessage("Fund already exist");
                else
                    LogMessage("New Fund added successfully");

                ClearForm();
                btnSubmitFund.Text = "Add";
            }
            BindControls();
            cbAddFund.Checked = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlFundName.SelectedIndex = -1;
            ddlAcctNum.SelectedIndex = -1;
            btnSubmitFund.Text = "Add";
            ClearForm();
            BindControls();
            cbAddFund.Checked = false;
        }

        protected void ddlFundName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDown(ddlAcctNum, ddlFundName.SelectedValue);
            ClearForm();
            cbAddFund.Checked = false;
            dvfundf.Visible = false;
            SearchFund();
            BindEntityNotesGrid();
        }

        protected void ddlAcctNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDown(ddlFundName, ddlAcctNum.SelectedValue);
            ClearForm();
            cbAddFund.Checked = false;
            dvfundf.Visible = false;
            SearchFund();
            BindEntityNotesGrid();
        }

        private void SearchFund()
        {
            cbAddFund.Checked = true;

            if (ddlFundName.SelectedIndex != 0)
                PopulateForm(FundMaintenanceData.SearchFund(Convert.ToInt32(ddlFundName.SelectedValue.ToString())));
            else if (ddlAcctNum.SelectedIndex != 0)
                PopulateForm(FundMaintenanceData.SearchFund(Convert.ToInt32(ddlAcctNum.SelectedValue.ToString())));
        }

        protected void ImgFundDataReport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
            "script", Helper.GetExagoURL("6588", "Fund Data"));
            
        }

        protected void btnAddAttachGrant_Click(object sender, EventArgs e)
        {

        }

        protected void gvAttachedGrants_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvAttachedGrants_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvAttachedGrants_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvAttachedGrants_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ImgNotesReport_Click(object sender, ImageClickEventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(),
            //        "script", Helper.GetAttachedProjectsReportPDF(hfApplicatId.Value, "Entity Notes"));
        }

        private bool IsEntityNotesValid()
        {
            if (txtEntityNotes.Text.Trim() == "")
            {
                LogMessage("Enter Notes");
                txtEntityNotes.Focus();
                return false;
            }

            return true;
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddNotes.Text.ToLower() == "submit")
                {
                    if (IsEntityNotesValid())
                    {
                        EntityNotesData.AddFundNotes(Convert.ToInt32(ddlFundName.SelectedValue.ToString()),
                               Context.User.Identity.GetUserName().Trim(), txtEntityNotes.Text);

                        LogMessage("Fund Notes Added Successfully");
                    }
                }
                else
                {
                    EntityNotesData.UpdateFundNotes(DataUtils.GetInt(hfFundNoteID.Value), txtEntityNotes.Text, cbNotesActive.Checked);
                    hfEntityNotesId.Value = "";

                    gvNotes.EditIndex = -1;
                    LogMessage("Fund Notes Updated Successfully");
                    btnAddNotes.Text = "Submit";
                }
                BindEntityNotesGrid();
                cbAddnotes.Checked = false;
                cbNotesActive.Enabled = false;
                cbNotesActive.Checked = true;
                txtEntityNotes.Text = "";
            }
            catch (Exception ex)
            {
                LogError(Pagename, "btnAddNotes_Click", null, ex.Message);
            }
        }

        private void BindEntityNotesGrid()
        {
            DataTable dt = EntityNotesData.GetFundNotesList(DataUtils.GetInt(ddlFundName.SelectedValue.ToString()), cbActiveOnly.Checked);

            if (dt.Rows.Count > 0)
            {
                dvNotesGrid.Visible = true;
                gvNotes.DataSource = dt;
                gvNotes.DataBind();
            }
            else
            {
                dvNotesGrid.Visible = false;
                gvNotes.DataSource = null;
                gvNotes.DataBind();
            }

        }

        protected void gvNotes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            btnAddNotes.Text = "Submit";
            txtEntityNotes.Text = "";
            hfFundNoteID.Value = "";
            cbAddnotes.Checked = false;
            gvNotes.EditIndex = -1;
            BindEntityNotesGrid();
        }

        protected void gvNotes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNotes.EditIndex = e.NewEditIndex;
            BindEntityNotesGrid();
        }

        protected void gvNotes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //e.Row.Cells[5].Controls[0].Visible = false;
                        btnAddNotes.Text = "Update";

                        Label lblFundNoteID = e.Row.FindControl("lblFundNoteID") as Label;
                        DataRow dr = EntityNotesData.GetFundNotesById(DataUtils.GetInt(lblFundNoteID.Text));

                        hfFundNoteID.Value = lblFundNoteID.Text;
                        txtEntityNotes.Text = dr["Note"].ToString();
                        cbAddnotes.Checked = true;
                        cbNotesActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        cbNotesActive.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvNotes_RowDataBound", "", ex.Message);
            }
        }
    }
}