using System;
using DataAccessLayer;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class UnusedOnlineProjects : System.Web.UI.Page
    {
        string Pagename = "UnusedOnlineProjects";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControls();
            }
        }
        private void BindControls()
        {

            //BindPrograms();
            BindLookUP(ddlProgram, 34);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void gvProjGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProjGrid.EditIndex = -1;
            BindGrid();
        }

        protected void gvProjGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProjGrid.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvProjGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int TempUserID = Convert.ToInt32(((Label)gvProjGrid.Rows[rowIndex].FindControl("lblTempUserID")).Text);
               
                BoarddatesData.DeleteTempUser(TempUserID);
                gvProjGrid.EditIndex = -1;
                BindGrid();
                lblErrorMsg.Text = "Data updated successfully";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error updating the Data: " + ex.Message;
                lblErrorMsg.Visible = true;
            }
        }
        protected void BindGrid()
        {
            try
            {
                gvProjGrid.DataSource = BoarddatesData.GetUnUsedTempUserProject(DataUtils.GetInt(ddlApplication.SelectedValue), DataUtils.GetInt(ddlProgram.SelectedValue));
                gvProjGrid.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvProjGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
        }
    }
}