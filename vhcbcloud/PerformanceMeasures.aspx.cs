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
    public partial class PerformanceMeasures : System.Web.UI.Page
    {
        string Pagename = "PerformanceMeasures";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLookUP(ddlProjectType, 2289);
                BindWQPerformanceMeasures();
            }
        }
        protected void BindWQPerformanceMeasures()
        {
            try
            {
                gvWQPerformanceMeasures.DataSource = LookupValuesData.GetWQPerformanceMeasures(DataUtils.GetInt(ddlProjectType.SelectedValue), cbActiveOnly.Checked);
                gvWQPerformanceMeasures.DataBind();
               // txtPerformanceMeasure.Text = "";
               // ddlProjectType.SelectedIndex = -1;
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

            if(txtPerformanceMeasure.Text == "")
            {
                LogMessage("Enter Performace Measure");
                txtPerformanceMeasure.Focus();
                return;
            }
            
            Result objResult = LookupValuesData.AddWQPerformanceMeasures(DataUtils.GetInt(ddlProjectType.SelectedValue),txtPerformanceMeasure.Text);

            //ddlProjectType.SelectedIndex = -1;
            txtPerformanceMeasure.Text = "";

            BindWQPerformanceMeasures();

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Performace Measure already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Performace Measure already exist");
            else
                LogMessage("New Performace Measure added successfully");
        }

        protected void gvWQPerformanceMeasures_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWQPerformanceMeasures.EditIndex = -1;
            BindWQPerformanceMeasures();
        }

        protected void gvWQPerformanceMeasures_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWQPerformanceMeasures.EditIndex = e.NewEditIndex;
            BindWQPerformanceMeasures();
        }

        protected void gvWQPerformanceMeasures_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int WQPerformanceTypeID = Convert.ToInt32(((Label)gvWQPerformanceMeasures.Rows[rowIndex].FindControl("lblWQPerformanceTypeID")).Text);
                string PerformanceMeasure = ((TextBox)gvWQPerformanceMeasures.Rows[rowIndex].FindControl("txtPerformanceMeasure")).Text;
                bool RowIsActive = Convert.ToBoolean(((CheckBox)gvWQPerformanceMeasures.Rows[rowIndex].FindControl("chkActive")).Checked);

                LookupValuesData.UpdateGetWQPerformanceMeasures(WQPerformanceTypeID, PerformanceMeasure, RowIsActive);
                gvWQPerformanceMeasures.EditIndex = -1;
                BindWQPerformanceMeasures();
                lblErrorMsg.Text = "Performance Measures updated successfully";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error updating the Performance Measures: " + ex.Message;
                lblErrorMsg.Visible = true;
            }
        }

        protected void gvWQPerformanceMeasures_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindWQPerformanceMeasures();
        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWQPerformanceMeasures();
        }
    }
}