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

namespace vhcbcloud.Conservation
{
    public partial class FarmOperationTownPlanning : System.Web.UI.Page
    {
        string Pagename = "FarmOperationTownPlanning";
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
                BindForm();
                BindGrids();
               
            }
        }

        private void BindControls()
        {
            BindLookUP(ddlCM, 2288);
            BindLookUP(ddlFarmClassification, 2277);
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
        private void PopulateProjectDetails()
        {
            DataRow dr = ProjectMaintenanceData.GetProjectNameById(DataUtils.GetInt(hfProjectId.Value));
            ProjectNum.InnerText = dr["ProjNumber"].ToString();
            ProjName.InnerText = dr["ProjectName"].ToString();
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

            DataTable dtTabs = TabsData.GetProgramTabs(DataUtils.GetInt(ProgramId));

            foreach (DataRow dr in dtTabs.Rows)
            {
                HtmlGenericControl li1 = new HtmlGenericControl("li");
                if (dr["URL"].ToString().Contains("FarmOperationTownPlanning.aspx"))
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
        private void BindGrids()
        {
            BindCMGrid();
        }
        protected void btnAddCM_Click(object sender, EventArgs e)
        {
            if (ddlCM.SelectedIndex == 0)
            {
                LogMessage("Select Conservation Measures");
                ddlCM.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddConserveMeasures(DataUtils.GetInt(hfConserveId.Value),
                DataUtils.GetInt(ddlCM.SelectedValue.ToString()));
            ddlCM.SelectedIndex = -1;
            cbAddCM.Checked = false;

            BindCMGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Type of Conserve Measures already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Type of  Conserve Measures already exist");
            else
                LogMessage("New Type of  Conserve Measures added successfully");
        }
        private void BindCMGrid()
        {
            try
            {
                DataTable dt = ConservationAttributeData.GetConserveMeasuresList(DataUtils.GetInt(hfConserveId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvPAGrid.Visible = true;
                    gvCM.DataSource = dt;
                    gvCM.DataBind();
                }
                else
                {
                    dvPAGrid.Visible = false;
                    gvCM.DataSource = null;
                    gvCM.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindCMGrid", "", ex.Message);
            }
        }

        protected void gvCM_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCM.EditIndex = e.NewEditIndex;
            BindCMGrid();
        }

        protected void gvCM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCM.EditIndex = -1;
            BindCMGrid();
        }

        protected void gvCM_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ConserveMeasuresID = DataUtils.GetInt(((Label)gvCM.Rows[rowIndex].FindControl("lblConserveMeasuresID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvCM.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateConserveMeasure(ConserveMeasuresID, RowIsActive);
            gvCM.EditIndex = -1;

            BindCMGrid();

            LogMessage("Public Access updated successfully");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ConservationSummaryData.SubmitFarmOperation(DataUtils.GetInt(hfProjectId.Value), DataUtils.GetInt(ddlFarmClassification.SelectedValue.ToString()), DataUtils.GetBool(rdbtnRAPCompliance.SelectedValue),

               DataUtils.GetInt(txtRentedLand.Text),
               DataUtils.GetDecimal(txtTotalEmployees.Text), DataUtils.GetDecimal(txtPTyear.Text), DataUtils.GetDecimal(txtFTyear.Text),
               DataUtils.GetDecimal(txtFTSeasonal.Text), DataUtils.GetDecimal(txtPTSeasonal.Text), txtProductsSold.Text, DataUtils.GetBool(rdbtnWorkedWithVHCB.SelectedValue), txtMitigateClimate.Text,
               DataUtils.GetBool(rdbtnExistingInfastructure.SelectedValue), txtInfrastuctureDescription.Text, txtZoningDistrict.Text, txtMinLotSize.Text, DataUtils.GetBool(rdbtnEnrolledUseValue.SelectedValue));

            BindForm();

            if (btnSubmit.Text.ToLower() == "update")
                LogMessage("Conservation Farm Operation updated successfully");
            else
                LogMessage("Conservation Farm Operation added successfully");
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
        private void BindForm()
        {
            DataRow drConserve = ConservationSummaryData.GetConserveFarm(DataUtils.GetInt(hfProjectId.Value));
            hfConserveId.Value = "";
         
            if (drConserve != null)
            {
                hfConserveId.Value = drConserve["ConserveID"].ToString();
                PopulateDropDown(ddlFarmClassification, drConserve["FarmClassification"].ToString());
                txtTotalEmployees.Text = drConserve["TotalEmployees"].ToString();             
                txtFTyear.Text = drConserve["FTyear"].ToString();
                txtPTyear.Text = drConserve["PTYear"].ToString();
                txtFTSeasonal.Text = drConserve["FTSeasonal"].ToString();
                txtPTSeasonal.Text = drConserve["PTSeasonal"].ToString();
                txtRentedLand.Text = drConserve["RentedLand"].ToString();
                txtProductsSold.Text = drConserve["ProductsSold"].ToString();

                if (DataUtils.GetBool(drConserve["RAPCompliance"].ToString()))
                    rdbtnRAPCompliance.SelectedIndex = 0;
                else
                    rdbtnRAPCompliance.SelectedIndex = 1;

                if (DataUtils.GetBool(drConserve["WorkedWithVHCB"].ToString()))
                    rdbtnWorkedWithVHCB.SelectedIndex = 0;
                else
                    rdbtnWorkedWithVHCB.SelectedIndex = 1;

                if (DataUtils.GetBool(drConserve["ExistingInfastructure"].ToString()))
                    rdbtnExistingInfastructure.SelectedIndex = 0;
                else
                    rdbtnExistingInfastructure.SelectedIndex = 1;

                txtMitigateClimate.Text = drConserve["MitigateClimate"].ToString();
                txtInfrastuctureDescription.Text = drConserve["InfrastuctureDescription"].ToString();

                txtZoningDistrict.Text = drConserve["ZoningDistrict"].ToString();
                txtMinLotSize.Text = drConserve["MinLotSize"].ToString();

                if (DataUtils.GetBool(drConserve["EnrolledUseValue"].ToString()))
                    rdbtnEnrolledUseValue.SelectedIndex = 0;
                else
                    rdbtnEnrolledUseValue.SelectedIndex = 1;
            }
        }
    }
}