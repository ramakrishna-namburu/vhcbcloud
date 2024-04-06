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
    public partial class WQWaterSheds : System.Web.UI.Page
    {
        string Pagename = "WQWaterSheds";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLookUP(ddlWatershed, 143);
                BindWQWatershed();
            }
        }

        protected void BindWQWatershed()
        {
            try
            {
                gvWQSubwatershed.DataSource = LookupValuesData.GetWQSubWatersheds(DataUtils.GetInt(ddlWatershed.SelectedValue), cbActiveOnly.Checked);
                gvWQSubwatershed.DataBind();
                txtSubWatershed.Text = "";
                txtHUC12.Text = "";
               // ddlWatershed.SelectedIndex = -1;
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
        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindWQWatershed();
        }


        protected void ddlWatershed_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWQWatershed();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlWatershed.SelectedIndex == 0)
            {
                LogMessage("Select Watershed");
                ddlWatershed.Focus();
                return;
            }

            if (txtSubWatershed.Text == "")
            {
                LogMessage("Enter Subwatershed");
                txtSubWatershed.Focus();
                return;
            }

            if (txtHUC12.Text == "")
            {
                LogMessage("Enter HUC12");
                txtHUC12.Focus();
                return;
            }

            Result objResult = LookupValuesData.AddWQSubWatersheds(DataUtils.GetInt(ddlWatershed.SelectedValue), txtSubWatershed.Text, txtHUC12.Text);

            //ddlWatershed.SelectedIndex = -1;
            txtSubWatershed.Text = "";
            txtHUC12.Text = "";

            BindWQWatershed();

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Watershed already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Watershed already exist");
            else
                LogMessage("New Watershed added successfully");
        }

        protected void gvWQSubwatershed_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWQSubwatershed.EditIndex = e.NewEditIndex;
            BindWQWatershed();
        }

        protected void gvWQSubwatershed_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int WQSubWatershedID = Convert.ToInt32(((Label)gvWQSubwatershed.Rows[rowIndex].FindControl("lblWQSubWatershedID")).Text);
                string Subwatershed = ((TextBox)gvWQSubwatershed.Rows[rowIndex].FindControl("txtSubWaterhed")).Text;
                string HUC12 = ((TextBox)gvWQSubwatershed.Rows[rowIndex].FindControl("txtHUC12")).Text;
                bool RowIsActive = Convert.ToBoolean(((CheckBox)gvWQSubwatershed.Rows[rowIndex].FindControl("chkActive")).Checked);

                LookupValuesData.UpdateWQSubWatersheds(WQSubWatershedID, Subwatershed, HUC12, RowIsActive);
                gvWQSubwatershed.EditIndex = -1;
                BindWQWatershed();
                lblErrorMsg.Text = "Watershed updated successfully";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error updating the Sub Watershed: " + ex.Message;
                lblErrorMsg.Visible = true;
            }
        }

        protected void gvWQSubwatershed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
        }

        protected void gvWQSubwatershed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWQSubwatershed.EditIndex = -1;
            BindWQWatershed();
        }

        protected void ddlWatershed_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindWQWatershed();
        }
    }
}