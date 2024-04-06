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
    public partial class Deliverables : System.Web.UI.Page
    {
        string Pagename = "Deliverables";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLookUP(ddlProjectType, 2289);
                BindWQDeliverables();
            }
        }
        protected void BindWQDeliverables()
        {
            try
            {
                gvWQDeliverable.DataSource = LookupValuesData.GetWQDeliverables(DataUtils.GetInt(ddlProjectType.SelectedValue), cbActiveOnly.Checked);
                gvWQDeliverable.DataBind();
               // txtDeliverable.Text = "";
                //ddlProjectType.SelectedIndex = -1;
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

            if (txtDeliverable.Text == "")
            {
                LogMessage("Enter Deliverable");
                txtDeliverable.Focus();
                return;
            }

            Result objResult = LookupValuesData.AddWQDeliverables(DataUtils.GetInt(ddlProjectType.SelectedValue), txtDeliverable.Text);

            //ddlProjectType.SelectedIndex = -1;
            txtDeliverable.Text = "";

            BindWQDeliverables();

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Deliverable already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Deliverable already exist");
            else
                LogMessage("New Deliverable added successfully");
        }

        protected void gvWQDeliverable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWQDeliverable.EditIndex = e.NewEditIndex;
            BindWQDeliverables();
        }

        protected void gvWQDeliverable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int WQDeliverablesID = Convert.ToInt32(((Label)gvWQDeliverable.Rows[rowIndex].FindControl("lblWQDeliverablesID")).Text);
                string Deliverable = ((TextBox)gvWQDeliverable.Rows[rowIndex].FindControl("txtDeliverable")).Text;
                bool RowIsActive = Convert.ToBoolean(((CheckBox)gvWQDeliverable.Rows[rowIndex].FindControl("chkActive")).Checked);

                LookupValuesData.UpdateWQDeliverables(WQDeliverablesID, Deliverable, RowIsActive);
                gvWQDeliverable.EditIndex = -1;
                BindWQDeliverables();
                lblErrorMsg.Text = "Performance Measures updated successfully";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error updating the Performance Measures: " + ex.Message;
                lblErrorMsg.Visible = true;
            }
        }

        protected void gvWQDeliverable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
        }

        protected void gvWQDeliverable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWQDeliverable.EditIndex = -1;
            BindWQDeliverables();
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindWQDeliverables();
        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWQDeliverables();
        }
    }
}