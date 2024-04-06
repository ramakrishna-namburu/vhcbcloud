using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;

namespace vhcbcloud.WaterQuality
{
    public partial class WQSummary : System.Web.UI.Page
    {
        string Pagename = "WQSummary";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            hfProjectId.Value = "0";
            hfProjectType.Value = "";

            ProjectNotesSetUp();

            GenerateTabs();

            if (!IsPostBack)
            {
                PopulateProjectDetails();

                BindControls();
                BindForm();
                BindLookupSubWatershed(26582);

            }
            //GetRoleAuth();
        }

        private void BindGrids()
        {
            BindWatershedGrid();
            BindOTGrid();
            BindPerformanceMeasuresGrid();
            BindMilestoneGrd();
            BindDeliverablesGrid();
        }

        private void BindMilestones(int ProjectTypeID)
        {
            try
            {
                ddlMilestone.Items.Clear();
                ddlMilestone.DataSource = LookupValuesData.GetWQProjectMilestonesByProjectTypeId(ProjectTypeID);
                ddlMilestone.DataValueField = "WQProjectMSTypeID";
                ddlMilestone.DataTextField = "Milestone";
                ddlMilestone.DataBind();
                ddlMilestone.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindPerformanceMeasures", "Control ID:" + ddlPerformance.ID, ex.Message);
            }
        }

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
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


        private void BindControls()
        {

            BindTacticalBasinLookUP(ddlTacticalBasin, 2284);
            BindLookUP(ddlProjectType, 2289);
            //BindLookUP(ddlWaterShedNew, 143);
            //BindHUC12CheckBoxList();
            BindLookUP(ddlOT, 45);
        }

        private void BindTacticalBasinLookUP(DropDownList ddlTacticalBasin, int LookupType)
        {
            try
            {
                ddlTacticalBasin.Items.Clear();
                ddlTacticalBasin.DataSource = LookupValuesData.Getlookupvalues(LookupType);
                ddlTacticalBasin.DataValueField = "typeid";
                ddlTacticalBasin.DataTextField = "description";
                ddlTacticalBasin.DataBind();
                ddlTacticalBasin.Items.Insert(0, new ListItem("Select", "NA"));

                foreach (ListItem item in ddlTacticalBasin.Items)
                {
                    if (item.Text.ToString() == "Lake Memphremagog")
                    {
                        ddlTacticalBasin.ClearSelection();
                        item.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindTacticalBasinLookUP", "Control ID:" + ddlTacticalBasin.ID, ex.Message);
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

        private void PopulateProjectDetails()
        {
            DataRow dr = ProjectMaintenanceData.GetProjectNameById(DataUtils.GetInt(hfProjectId.Value));
            ProjectNum.InnerText = dr["ProjNumber"].ToString();
            ProjName.InnerText = dr["ProjectName"].ToString();
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

            //DataTable dtTabs = TabsData.GetProgramTabs(DataUtils.GetInt(ProgramId));
            DataTable dtTabs = TabsData.GetProgramTabsForViability(DataUtils.GetInt(hfProjectId.Value), DataUtils.GetInt(ProgramId));

            foreach (DataRow dr in dtTabs.Rows)
            {
                HtmlGenericControl li1 = new HtmlGenericControl("li");
                if (dr["URL"].ToString().Contains("WQSummary.aspx"))
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
        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrids();
        }

        protected void ImgWatershed_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetStepPhase();
        }

        private void ResetStepPhase()
        {
            if (ddlProjectType.SelectedIndex == 0)
                spnStepPhase.InnerHtml = "";
            else
                spnStepPhase.InnerHtml = LookupMaintenanceData.GetLkLookupSubValue(DataUtils.GetInt(ddlProjectType.SelectedItem.Value));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            WaterQualityData.InsertWQProjectType(DataUtils.GetInt(hfProjectId.Value), DataUtils.GetInt(ddlProjectType.SelectedValue.ToString()),
                DataUtils.GetDecimal(txtPhosphorus.Text), DataUtils.GetDecimal(txtAcres.Text), DataUtils.GetInt(ddlTacticalBasin.SelectedValue.ToString()));

            BindForm();

            if (btnSubmit.Text.ToLower() == "update")
                LogMessage("Water Quality Project updated successfully");
            else
                LogMessage("Water Quality Project added successfully");
        }

        private void BindForm()
        {
            DataRow drConserve = WaterQualityData.GetWQProjectType(DataUtils.GetInt(hfProjectId.Value));

            if (drConserve != null)
            {
                PopulateDropDown(ddlProjectType, drConserve["ProjectTypeID"].ToString());
                txtAcres.Text = drConserve["TotalAcres"].ToString();
                txtPhosphorus.Text = drConserve["Phosphorus"].ToString();
                PopulateDropDown(ddlTacticalBasin, drConserve["TacticalBasin"].ToString());
                btnSubmit.Text = "Update";
                BindPerformanceMeasures(DataUtils.GetInt(drConserve["ProjectTypeID"].ToString()));
                BindMilestones(DataUtils.GetInt(drConserve["ProjectTypeID"].ToString()));
                BindDeliverables(DataUtils.GetInt(drConserve["ProjectTypeID"].ToString()));


                //hfProjectType.Value = drConserve["ProjectType"].ToString();
                ResetStepPhase();

            }

            BindGrids();
        }

        private void BindDeliverables(int ProjectTypeID)
        {
            try
            {
                ddlDeliverables.Items.Clear();
                ddlDeliverables.DataSource = LookupValuesData.GetWQDeliverablesByProjectTypeId(ProjectTypeID);
                ddlDeliverables.DataValueField = "WQDeliverablesID";
                ddlDeliverables.DataTextField = "Deliverable";
                ddlDeliverables.DataBind();
                ddlDeliverables.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindDeliverables", "Control ID:" + ddlDeliverables.ID, ex.Message);
            }
        }

        private void BindPerformanceMeasures(int ProjectTypeID)
        {
            try
            {
                    ddlPerformance.Items.Clear();
                    ddlPerformance.DataSource = LookupValuesData.GetWQPerformanceMeasuresByProjectTypeId(ProjectTypeID);
                    ddlPerformance.DataValueField = "WQPerformanceTypeID";
                    ddlPerformance.DataTextField = "PerformanceMeasure";
                    ddlPerformance.DataBind();
                    ddlPerformance.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindPerformanceMeasures", "Control ID:" + ddlPerformance.ID, ex.Message);
            }
        }

        private void PopulateTacticalBasin(DropDownList ddl, string DBSelectedvalue)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedvalue == item.Text.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }
        private void BindLookupSubWatershed(int Watershed)
        {
            try
            {
                ddlSubWatershedNew.Items.Clear();
                ddlSubWatershedNew.DataSource = LookupMaintenanceData.GetWQSubWatershedsForWatershed(Watershed);
                ddlSubWatershedNew.DataValueField = "SubWaterhed";
                ddlSubWatershedNew.DataTextField = "SubWaterhed";
                ddlSubWatershedNew.DataBind();
                ddlSubWatershedNew.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void BindHUC12(string SubWaterhed)
        {
            try
            {
                DataTable dt = LookupMaintenanceData.GetHUC12ForSubWatershed(SubWaterhed);

                if (dt.Rows.Count > 1)
                {
                    ddlHUC12.Visible = true;
                    spnHUC12.Visible = false;
                    ddlHUC12.Items.Clear();
                    ddlHUC12.DataSource = dt;
                    ddlHUC12.DataValueField = "HUC12";
                    ddlHUC12.DataTextField = "HUC12";
                    ddlHUC12.DataBind();
                    ddlHUC12.Items.Insert(0, new ListItem("Select", "NA"));
                }
                else if (dt.Rows.Count == 1)
                {
                    ddlHUC12.Visible = false;
                    spnHUC12.Visible = true;
                    spnHUC12.InnerHtml = dt.Rows[0]["HUC12"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnWatershed_Click(object sender, EventArgs e)
        {
            //if (ddlWaterShedNew.SelectedIndex == 0)
            //{
            //    LogMessage("Select Watershed");
            //    ddlWaterShedNew.Focus();
            //    return;
            //}
            //if (!cblHUC12.Items.Cast<ListItem>().Any(i => i.Selected))
            //{
            //    LogMessage("Select HUC12");
            //    cblHUC12.Focus();
            //    return;
            //}
            //if (ddlSubWatershedNew.SelectedIndex == 0)
            //{
            //    LogMessage("Select Sub-Watershed");
            //    ddlSubWatershedNew.Focus();
            //    return;
            //}


            if (btnWatershed.Text.ToLower() == "update")
            {
                //int ConserveWatershedID = Convert.ToInt32(hfConserveWatershedID.Value);

                //ConservationSummaryData.UpdateWatershed(ConserveWatershedID,
                //    DataUtils.GetInt(ddlWaterShedNew.SelectedValue.ToString()),
                //    DataUtils.GetInt(ddlSubWatershedNew.SelectedValue.ToString()),
                //    cbWatershedActive.Checked);

                //hfConserveWatershedID.Value = "";
                //btnWatershed.Text = "Add";
                //cbWatershedActive.Checked = true;
                //cbWatershedActive.Enabled = false;
                //LogMessage("Watershed updated successfully");
            }
            else
            {
                string strHUC12 ="";

                if (ddlHUC12.Items.Count > 0)
                    strHUC12 = ddlHUC12.SelectedValue.ToString();
                else
                    strHUC12 = spnHUC12.InnerHtml;


                ConservationSummaryData.AddWQWatershed(DataUtils.GetInt(ddlProjectType.SelectedValue.ToString()), ddlSubWatershedNew.SelectedValue.ToString(), strHUC12);

                LogMessage("Watershed added successfully");
            }

            gvWatershed.EditIndex = -1;
            BindWatershedGrid();
            ClearWatershedForm();
            //dvSurfaceWatersGrid.Visible = true;
            cbAddWatershed.Checked = false;
        }
        private void BindWatershedGrid()
        {
            try
            {
                DataTable dtWatershed = ConservationSummaryData.GetWQWatershedList(DataUtils.GetInt(ddlProjectType.SelectedValue), cbActiveOnly.Checked);

                if (dtWatershed.Rows.Count > 0)
                {
                    dvWatershedGrid.Visible = true;
                    gvWatershed.DataSource = dtWatershed;
                    gvWatershed.DataBind();
                }
                else
                {
                    dvWatershedGrid.Visible = false;
                    gvWatershed.DataSource = null;
                    gvWatershed.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindWatershedGrid", "", ex.Message);
            }
        }
        private void ClearWatershedForm()
        {
            ddlSubWatershedNew.SelectedIndex = -1;
            ddlHUC12.SelectedIndex = -1;
            cbAddWatershed.Checked = false;
        }
        protected void gvWatershed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cbWatershedActive.Checked = true;
            cbWatershedActive.Enabled = false;
            cbAddWatershed.Checked = false;
            ClearWatershedForm();
            btnWatershed.Text = "Add";

            gvWatershed.EditIndex = -1;
            BindWatershedGrid();

            btnWatershed.Visible = true;
        }

        protected void gvWatershed_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int WQWatershedID = DataUtils.GetInt(((Label)gvWatershed.Rows[rowIndex].FindControl("lblWQWatershedID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvWatershed.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationSummaryData.UpdateWQWatershed(WQWatershedID, RowIsActive);
            gvWatershed.EditIndex = -1;
            cbAddWatershed.Checked = false;
            BindWatershedGrid();

            LogMessage("Watershed updated successfully");
        }

        protected void gvWatershed_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWatershed.EditIndex = e.NewEditIndex;
            BindWatershedGrid();
        }

        protected void btnAddOT_Click(object sender, EventArgs e)
        {
            if (ddlOT.SelectedIndex == 0)
            {
                LogMessage("Select Owner Type");
                ddlOT.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddWQOwnerType(DataUtils.GetInt(hfProjectId.Value),
                         DataUtils.GetInt(ddlOT.SelectedValue.ToString()));
            ddlOT.SelectedIndex = -1;
            cbAddOT.Checked = false;

            BindOTGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Owner Type already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Owner Type already exist");
            else
                LogMessage("New Owner Type added successfully");
        }

        protected void gvOT_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOT.EditIndex = e.NewEditIndex;
            BindOTGrid();
        }

        protected void gvOT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOT.EditIndex = -1;
            BindOTGrid();
        }

        protected void gvOT_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int WQOTypeID = DataUtils.GetInt(((Label)gvOT.Rows[rowIndex].FindControl("lblWQOTypeID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvOT.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateWQOwnerType(WQOTypeID, RowIsActive);
            gvOT.EditIndex = -1;

            BindOTGrid();

            LogMessage("Owner Type updated successfully");
        }

        private void BindOTGrid()
        {
            try
            {
                DataTable dt = ConservationAttributeData.GetWQOwnerTypeList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvOTGrid.Visible = true;
                    gvOT.DataSource = dt;
                    gvOT.DataBind();
                }
                else
                {
                    dvOTGrid.Visible = false;
                    gvOT.DataSource = null;
                    gvOT.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindOTGrid", "", ex.Message);
            }
        }

        protected void ImgConservationownerType_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnAddPerform_Click(object sender, EventArgs e)
        {
            if (ddlPerformance.SelectedIndex == 0)
            {
                LogMessage("Select Performance Measure");
                ddlPerformance.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationSummaryData.AddWQProjectPerform(DataUtils.GetInt(hfProjectId.Value),
                         DataUtils.GetInt(ddlPerformance.SelectedValue.ToString()), txtDetail.Text);
            ddlPerformance.SelectedIndex = -1;
            cbAddPerformance.Checked = false;

            BindPerformanceMeasuresGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Performance Measure already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Performance Measure already exist");
            else
                LogMessage("New Performance Measure added successfully");
        }

        protected void btnAddDeliverables_Click(object sender, EventArgs e)
        {
            if (ddlDeliverables.SelectedIndex == 0)
            {
                LogMessage("Select Deliverables");
                ddlDeliverables.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationSummaryData.AddWQProjectDeliverables(DataUtils.GetInt(hfProjectId.Value),
                         DataUtils.GetInt(ddlDeliverables.SelectedValue.ToString()), DataUtils.GetDate(txtDateDel.Text));

            ddlDeliverables.SelectedIndex = -1;
            txtDateDel.Text = "";
            cbAddDeliverables.Checked = false;

            BindDeliverablesGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Deliverables exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Deliverables already exist");
            else
                LogMessage("New Deliverables added successfully");
        }

        private void BindDeliverablesGrid()
        {
                try
                {
                    DataTable dtWatershed = ConservationSummaryData.GetWQProjectDeliverables(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                    if (dtWatershed.Rows.Count > 0)
                    {
                    dvDeliverablesGrid.Visible = true;
                    gvDeliverables.DataSource = dtWatershed;
                    gvDeliverables.DataBind();
                    }
                    else
                    {
                    dvDeliverablesGrid.Visible = false;
                    gvDeliverables.DataSource = null;
                    gvDeliverables.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    LogError(Pagename, "BindDeliverablesGrid", "", ex.Message);
                }
            
        }

        protected void gvPerformance_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPerformance.EditIndex = e.NewEditIndex;
            BindPerformanceMeasuresGrid();
        }

        protected void gvPerformance_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPerformance.EditIndex = -1;
            BindPerformanceMeasuresGrid();

        }

        private void BindPerformanceMeasuresGrid()
        {
            try
            {
                DataTable dtWatershed = ConservationSummaryData.GetWQProjectPerformList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dtWatershed.Rows.Count > 0)
                {
                    dvPerformanceGrid.Visible = true;
                    gvPerformance.DataSource = dtWatershed;
                    gvPerformance.DataBind();
                }
                else
                {
                    dvPerformanceGrid.Visible = false;
                    gvPerformance.DataSource = null;
                    gvPerformance.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindPerformanceMeasuresGrid", "", ex.Message);
            }
        }

        protected void gvPerformance_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int WQProjectPerformID = DataUtils.GetInt(((Label)gvPerformance.Rows[rowIndex].FindControl("lblWQProjectPerformID")).Text);
            string Detail = ((TextBox)gvPerformance.Rows[rowIndex].FindControl("txtDetail")).Text;
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvPerformance.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateWQProjectPerform(WQProjectPerformID, Detail, RowIsActive);
            gvPerformance.EditIndex = -1;

            BindPerformanceMeasuresGrid();

            LogMessage("Performance Measure updated successfully");
        }

        protected void btnAddMilestone_Click(object sender, EventArgs e)
        {
            if (ddlMilestone.SelectedIndex == 0)
            {
                LogMessage("Select Milestone");
                ddlMilestone.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationSummaryData.AddWQProjectMilestones(DataUtils.GetInt(hfProjectId.Value),
                         DataUtils.GetInt(ddlMilestone.SelectedValue.ToString()), DataUtils.GetDate(txtDate.Text));

            ddlMilestone.SelectedIndex = -1;
            cbAddMilestone.Checked = false;

            BindMilestoneGrd();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Milestone exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Milestone already exist");
            else
                LogMessage("New Milestone added successfully");
        }
        private void BindMilestoneGrd()
        {
            try
            {
                DataTable dtWatershed = ConservationSummaryData.GetWQProjectMilestonesList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dtWatershed.Rows.Count > 0)
                {
                    dvMilestoneGrid.Visible = true;
                    gvMilestone.DataSource = dtWatershed;
                    gvMilestone.DataBind();
                }
                else
                {
                    dvMilestoneGrid.Visible = false;
                    gvMilestone.DataSource = null;
                    gvMilestone.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindMilestoneGrd", "", ex.Message);
            }
        }
        protected void gvMilestone_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMilestone.EditIndex = e.NewEditIndex;
            BindMilestoneGrd();
        }

        protected void gvMilestone_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMilestone.EditIndex = -1;
            BindMilestoneGrd();
        }

        protected void gvMilestone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int WQProjectMilestoneID = DataUtils.GetInt(((Label)gvMilestone.Rows[rowIndex].FindControl("lblWQProjectMilestoneID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvMilestone.Rows[rowIndex].FindControl("chkActive")).Checked);
            DateTime date = Convert.ToDateTime(((TextBox)gvMilestone.Rows[rowIndex].FindControl("txtDate")).Text);

            ConservationAttributeData.UpdateWQProjectMilestones(WQProjectMilestoneID, date, RowIsActive);
            gvMilestone.EditIndex = -1;

            BindMilestoneGrd();

            LogMessage("Milestone updated successfully");
        }

        protected void ddlSubWatershedNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubWatershedNew.SelectedIndex > 0)
                BindHUC12(ddlSubWatershedNew.SelectedValue);
            else
                ddlHUC12.Items.Clear();
        }

        protected void gvDeliverables_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDeliverables.EditIndex = e.NewEditIndex;
            BindDeliverablesGrid();
        }

        protected void gvDeliverables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDeliverables.EditIndex = -1;
            BindDeliverablesGrid();
        }

        protected void gvDeliverables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int WQProjectDeliverablesID = DataUtils.GetInt(((Label)gvDeliverables.Rows[rowIndex].FindControl("lblWQProjectDeliverablesID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvDeliverables.Rows[rowIndex].FindControl("chkActive")).Checked);
            DateTime date = Convert.ToDateTime(((TextBox)gvDeliverables.Rows[rowIndex].FindControl("txtDate")).Text);

            ConservationAttributeData.UpdateWQProjectDeliverables(WQProjectDeliverablesID, date, RowIsActive);
            gvDeliverables.EditIndex = -1;

            BindDeliverablesGrid();

            LogMessage("Deliverables updated successfully");
        }
    }
}