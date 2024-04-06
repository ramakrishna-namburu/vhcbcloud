using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;

namespace vhcbcloud
{
    public partial class WQMilestones : System.Web.UI.Page
    {
        string Pagename = "WQMilestones";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLookUP(ddlProjectType, 2289);
                BindWQMilestonesGrid();
            }
        }
        protected void BindWQMilestonesGrid()
        {
            try
            {
                gvWQMilestone.DataSource = LookupValuesData.GetWQMilestones(DataUtils.GetInt(ddlProjectType.SelectedValue), cbActiveOnly.Checked);
                gvWQMilestone.DataBind();
                txtMilestone.Text = "";
                ddlProjectType.SelectedIndex = -1;
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlProjectType.SelectedIndex == 0)
            {
                LogMessage("Select Project Type");
                ddlProjectType.Focus();
                return;
            }

            if (txtMilestone.Text == "")
            {
                LogMessage("Enter Milestone");
                txtMilestone.Focus();
                return;
            }

            Result objResult = LookupValuesData.AddWQMilestones(DataUtils.GetInt(ddlProjectType.SelectedValue), txtMilestone.Text);

            //ddlProjectType.SelectedIndex = -1;
            txtMilestone.Text = "";

            BindWQMilestonesGrid();

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Milestone already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Milestone already exist");
            else
                LogMessage("New Milestone added successfully");
        }

        protected void gvWQMilestone_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWQMilestone.EditIndex = e.NewEditIndex;
            BindWQMilestonesGrid();
        
    }

        protected void gvWQMilestone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        try
        {
            int rowIndex = e.RowIndex;
            int WQProjectMSTypeID = Convert.ToInt32(((Label)gvWQMilestone.Rows[rowIndex].FindControl("lblWQProjectMSTypeID")).Text);
            string Milestone = ((TextBox)gvWQMilestone.Rows[rowIndex].FindControl("txtMilestone")).Text;
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvWQMilestone.Rows[rowIndex].FindControl("chkActive")).Checked);

            LookupValuesData.UpdateWQMilestones(WQProjectMSTypeID, Milestone, RowIsActive);
                gvWQMilestone.EditIndex = -1;
            BindWQMilestonesGrid();
            lblErrorMsg.Text = "Milestone updated successfully";

        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = "Error updating the Milestone: " + ex.Message;
            lblErrorMsg.Visible = true;
        }
    }

        protected void gvWQMilestone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
        }

        protected void gvWQMilestone_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWQMilestone.EditIndex = -1;
            BindWQMilestonesGrid();
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindWQMilestonesGrid();
        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWQMilestonesGrid();
        }
    }
}