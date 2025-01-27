using DataAccessLayer;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Housing;
using VHCBCommon.DataAccessLayer.Lead;
using System.IO;

namespace vhcbcloud.Housing
{
    public partial class HousingAttributes : System.Web.UI.Page
    {
        string Pagename = "HousingAttributes";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            hfProjectId.Value = "0";
            ProjectNotesSetUp();
            GenerateTabs();

            if (!IsPostBack)
            {
                PopulateProjectDetails();

                BindControls();
                GetRoleAccess();
                BindGrids();
            }
            //GetRoleAuth();
        }

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            DataTable dt = UserSecurityData.GetUserId(Context.User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                //this.MasterPageFile = "SiteNonAdmin.Master";
            }
        }

        protected bool GetIsVisibleBasedOnRole()
        {
            return DataUtils.GetBool(hfIsVisibleBasedOnRole.Value);
        }

        protected void GetRoleAccess()
        {

            DataRow dr = UserSecurityData.GetUserSecurity(Context.User.Identity.Name);
            DataRow drProjectDetails = ProjectMaintenanceData.GetprojectDetails(DataUtils.GetInt(hfProjectId.Value));

            if (dr != null)
            {
                bool IsUserHasSameProgram = UserSecurityData.IsUserHasSameProgramId(DataUtils.GetInt(dr["userid"].ToString()), DataUtils.GetInt(Request.QueryString["ProjectId"]));

                if (dr["usergroupid"].ToString() == "0") // Admin Only
                {
                    hfIsVisibleBasedOnRole.Value = "true";
                }
                else if (dr["usergroupid"].ToString() == "1") // Program Admin Only
                {
                    //if (dr["dfltprg"].ToString() != drProjectDetails["LkProgram"].ToString())
                    if (!IsUserHasSameProgram)
                    {
                        RoleViewOnly();
                        hfIsVisibleBasedOnRole.Value = "false";
                    }
                    else
                    {
                        hfIsVisibleBasedOnRole.Value = "true";
                    }
                }
                else if (dr["usergroupid"].ToString() == "2") //2. Program Staff  
                {
                    //if (dr["dfltprg"].ToString() != drProjectDetails["LkProgram"].ToString())
                    if (!IsUserHasSameProgram)
                    {
                        RoleViewOnly();
                        hfIsVisibleBasedOnRole.Value = "false";
                    }
                    else
                    {
                        //    if (Convert.ToBoolean(drProjectDetails["verified"].ToString()))
                        //    {
                        //        RoleViewOnlyExceptAddNewItem();
                        //        hfIsVisibleBasedOnRole.Value = "false";
                        //    }
                        //    else
                        //    {
                                hfIsVisibleBasedOnRole.Value = "true";
                        //    }
                    }
                }
                else if (dr["usergroupid"].ToString() == "3") // View Only
                {
                    RoleViewOnly();
                    hfIsVisibleBasedOnRole.Value = "false";
                }

                if (Convert.ToBoolean(drProjectDetails["verified"].ToString()))
                {
                    RoleViewOnlyExceptAddNewItem();
                    hfIsVisibleBasedOnRole.Value = "false";
                }
            }
        }

        protected void RoleViewOnlyExceptAddNewItem()
        {
            cbAddCPP.Enabled = true;
            cbAddInterAgency.Enabled = true;
            cbAddVHCB.Enabled = true;
        }

        protected void RoleViewOnly()
        {
            cbAddCPP.Enabled = false;
            cbAddInterAgency.Enabled = false;
            cbAddVHCB.Enabled = false;

            btnAddInterAgency.Enabled = false;
            btnAddVHCB.Enabled = false;
        }

        private void ProjectNotesSetUp()
        {
            int PageId = ProjectNotesData.GetPageId(Path.GetFileName(Request.PhysicalPath));

            if (Request.QueryString["ProjectId"] != null)
            {
                hfProjectId.Value = Request.QueryString["ProjectId"];
                ifProjectNotes.Src = "../ProjectNotes.aspx?ProjectId=" + Request.QueryString["ProjectId"] +
                    "&PageId=" + PageId;
                if (ProjectNotesData.IsNotesExist(PageId, DataUtils.GetInt(hfProjectId.Value)))
                    btnProjectNotes.ImageUrl = "~/Images/currentpagenotes.png";
            }
        }

        //protected bool GetRoleAuth()
        //{
        //    bool checkAuth = UserSecurityData.GetRoleAuth(Context.User.Identity.Name, DataUtils.GetInt(Request.QueryString["ProjectId"]));
        //    if (!checkAuth)
        //        RoleReadOnly();
        //    return checkAuth;
        //}


        private void GenerateTabs()
        {
            string ProgramId = null;

            if (Request.QueryString["ProgramId"] != null)
                ProgramId = Request.QueryString["ProgramId"];

            //Active Tab
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "RoundedCornerTop");
            Tabs.Controls.Add(li);

            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", "../ProjectMaintenance.aspx?ProjectId=" + hfProjectId.Value);
            anchor.InnerText = "Project Maintenance";
            anchor.Attributes.Add("class", "RoundedCornerTop");

            li.Controls.Add(anchor);

            DataTable dtTabs = TabsData.GetProgramTabs(DataUtils.GetInt(ProgramId));

            foreach (DataRow dr in dtTabs.Rows)
            {
                HtmlGenericControl li1 = new HtmlGenericControl("li");
                if (dr["URL"].ToString().Contains("HousingAttributes.aspx"))
                    li1.Attributes.Add("class", "RoundedCornerTop selected");
                else
                    li1.Attributes.Add("class", "RoundedCornerTop");

                Tabs.Controls.Add(li1);
                HtmlGenericControl anchor1 = new HtmlGenericControl("a");
                anchor1.Attributes.Add("href", "../" + dr["URL"].ToString() + "?ProjectId=" + hfProjectId.Value + "&ProgramId=" + ProgramId);
                anchor1.InnerText = dr["TabName"].ToString();
                anchor1.Attributes.Add("class", "RoundedCornerTop");
                li1.Controls.Add(anchor1);
            }
        }

        private void PopulateProjectDetails()
        {
            DataRow dr = ProjectMaintenanceData.GetProjectNameById(DataUtils.GetInt(hfProjectId.Value));
            ProjectNum.InnerText = dr["ProjNumber"].ToString();
            ProjName.InnerText = dr["ProjectName"].ToString();
        }

        private void BindControls()
        {
            //BindLookUP(ddlPriorities, 60);
            BindCheckBoxList(cblPriorities, 60);
            //BindLookUP(ddlInterAgencyPriorities, 79);
            BindCheckBoxList(cblInterAgencyPriorities, 79);
            //BindLookUP(ddlVHCBPriorities, 90);
            BindCheckBoxList(cblVHCBPriorities, 90);
            //BindLookUP(ddlOutcomes, 81);
        }

        private void BindCheckBoxList(CheckBoxList ddList, int LookupType)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = LookupValuesData.Getlookupvalues(LookupType);
                ddList.DataValueField = "typeid";
                ddList.DataTextField = "description";
                ddList.DataBind();
                //ddList.Items.Insert(0, new ListItem("Select", "NA"));
                //ddList.Items.Attributes.Add("onclick", "changeColor(this)");
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindLookUP", "Control ID:" + ddList.ID, ex.Message);
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

        private void BindGrids()
        {
            BindConsolidatedPlanPrioritiesGrid();
            BindInterAgencyGrid();
            BindVHCBGrid();
            //BindOtherGrid();
        }

        private void BindConsolidatedPlanPrioritiesGrid()
        {
            try
            {
                DataTable dt = HousingAttributesData.GetConsolidatedPlanPrioritiesList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvCPPFormGrid.Visible = true;
                    gvCPPForm.DataSource = dt;
                    gvCPPForm.DataBind();
                }
                else
                {
                    dvCPPFormGrid.Visible = false;
                    gvCPPForm.DataSource = null;
                    gvCPPForm.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindConsolidatedPlanPrioritiesGrid", "", ex.Message);
            }
        }

        #region Logs
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
        #endregion Logs

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrids();
        }

        protected void AddPriorities_Click(object sender, EventArgs e)
        {
            if(!cblPriorities.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                LogMessage("Select Priorities");
                cblPriorities.Focus();
                return;
            }

            foreach (ListItem item in cblPriorities.Items)
            {
                if (item.Selected)
                {
                    HousingAttributesResult objHousingAttributesResult = HousingAttributesData.AddConsolidatedPlanPriorities(DataUtils.GetInt(hfProjectId.Value),
                                    DataUtils.GetInt(item.Value), GetUserId());
                    item.Selected = false;
                }
            }

            cbAddCPP.Checked = false;

            BindConsolidatedPlanPrioritiesGrid();

            //if (objHousingAttributesResult.IsDuplicate && !objHousingAttributesResult.IsActive)
            //    LogMessage("Consolidated Plan Priority already exists as in-active");
            //else if (objHousingAttributesResult.IsDuplicate)
            //    LogMessage("Consolidated Plan Priority already exists");
            //else
                LogMessage("Consolidated Plan Priority added successfully");
        }

        protected void gvCPPForm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCPPForm.EditIndex = e.NewEditIndex;
            BindConsolidatedPlanPrioritiesGrid();
        }

        protected void gvCPPForm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCPPForm.EditIndex = -1;
            BindConsolidatedPlanPrioritiesGrid();
        }

        protected void gvCPPForm_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ProjectConPlanPrioritiesID = DataUtils.GetInt(((Label)gvCPPForm.Rows[rowIndex].FindControl("lblProjectConPlanPrioritiesID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvCPPForm.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            HousingAttributesData.UpdateConsolidatedPlanPriorities(ProjectConPlanPrioritiesID, GetUserId(), RowIsActive);
            gvCPPForm.EditIndex = -1;

            BindConsolidatedPlanPrioritiesGrid();

            LogMessage("Consolidated Plan Priority updated successfully");
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

        private void BindInterAgencyGrid()
        {
            try
            {
                DataTable dt = HousingAttributesData.GetProjectInteragencyList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvInterAgencyGrid.Visible = true;
                    gvInterAgency.DataSource = dt;
                    gvInterAgency.DataBind();
                }
                else
                {
                    dvInterAgencyGrid.Visible = false;
                    gvInterAgency.DataSource = null;
                    gvInterAgency.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindInterAgencyGrid", "", ex.Message);
            }
        }

        protected void btnAddInterAgency_Click(object sender, EventArgs e)
        {
            if (!cblInterAgencyPriorities.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                LogMessage("Select Priorities");
                cblInterAgencyPriorities.Focus();
                return;
            }

            foreach (ListItem item in cblInterAgencyPriorities.Items)
            {
                if (item.Selected)
                {
                    HousingAttributesResult objHousingAttributesResult = HousingAttributesData.AddProjectInteragency(DataUtils.GetInt(hfProjectId.Value),
                        DataUtils.GetInt(item.Value), 0);
                    item.Selected = false;
                }
            }
            cbAddInterAgency.Checked = false;

            BindInterAgencyGrid();

            //if (objHousingAttributesResult.IsDuplicate && !objHousingAttributesResult.IsActive)
            //    LogMessage("InterAgency Priority already exists as in-active");
            //else if (objHousingAttributesResult.IsDuplicate)
            //    LogMessage("InterAgency Priority already exists");
            //else
                LogMessage("InterAgency Priority added successfully");
        }

        protected void gvInterAgency_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInterAgency.EditIndex = e.NewEditIndex;
            BindInterAgencyGrid();
        }

        protected void gvInterAgency_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInterAgency.EditIndex = -1;
            BindInterAgencyGrid();
        }

        protected void gvInterAgency_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ProjectInteragencyID = DataUtils.GetInt(((Label)gvInterAgency.Rows[rowIndex].FindControl("lblProjectInteragencyID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvInterAgency.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            HousingAttributesData.UpdateProjectInteragency(ProjectInteragencyID, 0, RowIsActive);
            gvInterAgency.EditIndex = -1;

            BindInterAgencyGrid();

            LogMessage("InterAgency Priority updated successfully");
        }

        protected void btnAddVHCB_Click(object sender, EventArgs e)
        {
            if (!cblVHCBPriorities.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                LogMessage("Select Priorities");
                cblInterAgencyPriorities.Focus();
                return;
            }

            foreach (ListItem item in cblVHCBPriorities.Items)
            {
                if (item.Selected)
                {
                    HousingAttributesResult objHousingAttributesResult = HousingAttributesData.AddProjectVHCBPriorities(DataUtils.GetInt(hfProjectId.Value),
                        DataUtils.GetInt(item.Value));
                    item.Selected = false;
                }
            }
            cbAddVHCB.Checked = false;

            BindVHCBGrid();

            //if (objHousingAttributesResult.IsDuplicate && !objHousingAttributesResult.IsActive)
            //    LogMessage("VHCB Priority already exists as in-active");
            //else if (objHousingAttributesResult.IsDuplicate)
            //    LogMessage("VHCB Priority already exists");
            //else
                LogMessage("VHCB Priority added successfully");
        }

        private void BindVHCBGrid()
        {
            try
            {
                DataTable dt = HousingAttributesData.GetProjectVHCBPrioritiesList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvVHCBGrid.Visible = true;
                    gvVHCB.DataSource = dt;
                    gvVHCB.DataBind();
                }
                else
                {
                    dvVHCBGrid.Visible = false;
                    gvVHCB.DataSource = null;
                    gvVHCB.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindVHCBGrid", "", ex.Message);
            }
        }

        protected void gvVHCB_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvVHCB.EditIndex = e.NewEditIndex;
            BindVHCBGrid();
        }

        protected void gvVHCB_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvVHCB.EditIndex = -1;
            BindVHCBGrid();
        }

        protected void gvVHCB_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ProjectVHCBPrioritiesID = DataUtils.GetInt(((Label)gvVHCB.Rows[rowIndex].FindControl("lblProjectVHCBPrioritiesID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvVHCB.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            HousingAttributesData.UpdateProjectVHCBPriorities(ProjectVHCBPrioritiesID, RowIsActive);
            gvVHCB.EditIndex = -1;

            BindVHCBGrid();

            LogMessage("VHCB Priority updated successfully");
        }

        protected void ImgConsolidated_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
             "script", Helper.GetExagoURL(hfProjectId.Value, "Grid Housing Consolidated Plan Priorities"));
        }

        protected void ImgInteragency_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
             "script", Helper.GetExagoURL(hfProjectId.Value, "Grid Housing InterAgency Priorities"));
        }

        protected void ImgVHCBpriorities_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
             "script", Helper.GetExagoURL(hfProjectId.Value, "Grid Housing VHCB Priorities"));
        }
        /*
protected void btnAddOutcomes_Click(object sender, EventArgs e)
{
if (ddlOutcomes.SelectedIndex == 0)
{
LogMessage("Select Outcomes");
ddlOutcomes.Focus();
return;
}

HousingAttributesResult objHousingAttributesResult = HousingAttributesData.AddProjectOtherOutcomes(DataUtils.GetInt(hfProjectId.Value),
DataUtils.GetInt(ddlOutcomes.SelectedValue.ToString()), 0);

ddlOutcomes.SelectedIndex = -1;
cbAddOther.Checked = false;

BindOtherGrid();

if (objHousingAttributesResult.IsDuplicate && !objHousingAttributesResult.IsActive)
LogMessage("Other Outcome already exist as in-active");
else if (objHousingAttributesResult.IsDuplicate)
LogMessage("Other Outcome already exist");
else
LogMessage("New Other Outcome added successfully");
}

protected void gvOther_RowEditing(object sender, GridViewEditEventArgs e)
{
gvOther.EditIndex = e.NewEditIndex;
BindOtherGrid();
}

protected void gvOther_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
{
gvOther.EditIndex = -1;
BindOtherGrid();
}

protected void gvOther_RowUpdating(object sender, GridViewUpdateEventArgs e)
{
int rowIndex = e.RowIndex;

int ProjectOtherOutcomesID = DataUtils.GetInt(((Label)gvOther.Rows[rowIndex].FindControl("lblProjectOtherOutcomesID")).Text);
bool RowIsActive = Convert.ToBoolean(((CheckBox)gvOther.Rows[rowIndex].FindControl("chkActive")).Checked); ;

HousingAttributesData.UpdateProjectOtherOutcomes(ProjectOtherOutcomesID, 0, RowIsActive);
gvOther.EditIndex = -1;

BindOtherGrid();

LogMessage("Other Outcome updated successfully");

}

private void BindOtherGrid()
{
try
{
DataTable dt = HousingAttributesData.GetProjectOtherOutcomesList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

if (dt.Rows.Count > 0)
{
 dvOtherGrid.Visible = true;
 gvOther.DataSource = dt;
 gvOther.DataBind();
}
else
{
 dvOtherGrid.Visible = false;
 gvOther.DataSource = null;
 gvOther.DataBind();
}
}
catch (Exception ex)
{
LogError(Pagename, "BindOtherGrid", "", ex.Message);
}
}
*/
    }
}