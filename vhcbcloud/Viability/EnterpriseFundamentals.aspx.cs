﻿using DataAccessLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;
using VHCBCommon.DataAccessLayer.Viability;

namespace vhcbcloud.Viability
{
    public partial class EnterpriseFundamentals : System.Web.UI.Page
    {
        string Pagename = "EnterpriseFundamentals";
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
                LoadEnterpriseFundamentals();
            }
            //GetRoleAuth();
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
                        if (Convert.ToBoolean(drProjectDetails["verified"].ToString()))
                        {
                            RoleViewOnlyExceptAddNewItem();
                            hfIsVisibleBasedOnRole.Value = "false";
                        }
                        else
                        {
                            hfIsVisibleBasedOnRole.Value = "true";
                        }
                    }
                }
                else if (dr["usergroupid"].ToString() == "3") // View Only
                {
                    RoleViewOnly();
                    hfIsVisibleBasedOnRole.Value = "false";
                }
            }
        }

        protected void RoleViewOnlyExceptAddNewItem()
        {
            cbAddAttribute.Enabled = true;
            cbAddMilestone.Enabled = true;

            btnAddPlanInfo.Visible = false;
        }

        protected void RoleViewOnly()
        {
            btnAddAttribute.Visible = false;
            btnAddMilestone.Visible = false;
            btnAddPlanInfo.Visible = false;

            cbAddAttribute.Enabled = false;
            cbAddMilestone.Enabled = false;
        }

        //protected bool GetRoleAuth()
        //{
        //    bool checkAuth = UserSecurityData.GetRoleAuth(Context.User.Identity.Name, DataUtils.GetInt(Request.QueryString["ProjectId"]));
        //    if (!checkAuth)
        //        RoleReadOnly();
        //    return checkAuth;
        //}
        //protected void RoleReadOnly()
        //{
        //    btnAddAttribute.Visible = false;
        //    btnAddMilestone.Visible = false;
        //    btnAddPlanInfo.Visible = false;
        //    cbAddAttribute.Enabled = false;
        //    cbAddMilestone.Enabled = false;
        //    //cbBusplan.Enabled = false;
        //}

        private void LoadEnterpriseFundamentals()
        {
            DataRow drEntFunDetails = EnterpriseFundamentalsData.GetEnterpriseFundamentals(DataUtils.GetInt(hfProjectId.Value));
            if (drEntFunDetails != null)
            {
                hfEnterFundamentalID.Value = drEntFunDetails["EnterFundamentalID"].ToString();
                hfProjectProgram.Value = drEntFunDetails["ProjectProgram"].ToString();
                EventProgramSelection();

                PopulateDropDown(ddlPlanType, drEntFunDetails["PlanType"].ToString());
                PopulateDropDown(ddlServiceProvOrg, drEntFunDetails["ServiceProvOrg"].ToString());
                PopulateDropDown(ddlLeadAdvisor, drEntFunDetails["LeadAdvisor"].ToString());
                //PopulateDropDown(ddlHearViability, drEntFunDetails["HearAbout"].ToString());
                //txtYearMangBusiness.Text = drEntFunDetails["YrManageBus"].ToString();
                txtProjectDesc.Text = drEntFunDetails["ProjDesc"].ToString();
                txtBusinessDesc.Text = drEntFunDetails["BusDesc"].ToString();
                //cbBusplan.Checked = DataUtils.GetBool(drEntFunDetails["BusPlan"].ToString());
                //cbGrantApp.Checked = DataUtils.GetBool(drEntFunDetails["GrantApp"].ToString());
                PopulateDropDown(ddlFiscalYear, drEntFunDetails["FiscalYr"].ToString());

                btnAddPlanInfo.Text = "Update";
                CheckFinancialJobAccess();
                BindAttributeGrid();
            }
            else
            {
                btnAddPlanInfo.Text = "Add";
                dvNewFinJobs.Visible = false;
            }
        }

        private void CheckFinancialJobAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "28686")
                {
                    dvNewFinJobs.Visible = true;
                    BindFinJobsGrid();
                }
            }
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

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            DataTable dt = UserSecurityData.GetUserId(Context.User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                //this.MasterPageFile = "SiteNonAdmin.Master";
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

        private void PopulateProjectDetails()
        {
            DataRow dr = ProjectMaintenanceData.GetProjectNameById(DataUtils.GetInt(hfProjectId.Value));
            ProjectNum.InnerText = dr["ProjNumber"].ToString();
            ProjName.InnerText = dr["ProjectName"].ToString();
        }

        private void BindControls()
        {
            BindLookUP(ddlPlanType, 216);
            BindApplicants(26242, "organization", ddlServiceProvOrg);
            BindApplicants(26243, "individual", ddlLeadAdvisor);
            //BindLookUP(ddlHearViability, 215);
            BindLookUP(ddlAttribute, 230);
            BindLookUP(ddlFiscalYear, 76);
            
        }

        protected void BindApplicants(int RoleId, string RoleName, DropDownList ddList)
        {
            try
            {
                int Operation = 0;

                if (RoleName.ToLower() == "individual")
                    Operation = 1;
                else if (RoleName.ToLower() == "organization")
                    Operation = 2;
                else if (RoleName.ToLower() == "farm")
                    Operation = 3;

                ddList.Items.Clear();
                ddList.DataSource = EntityMaintenanceData.GetEntitiesByRole(RoleId, Operation);
                ddList.DataValueField = "ApplicantId";
                ddList.DataTextField = "Applicantname";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindApplicants", "", ex.Message);
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

        private void GenerateTabs()
        {
            string ProgramId = null;
            if (Request.QueryString["ProgramId"] != null)
                ProgramId = Request.QueryString["ProgramId"];

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "RoundedCornerTop");
            Tabs.Controls.Add(li);

            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", "../ProjectMaintenance.aspx?ProjectId=" + hfProjectId.Value);
            anchor.InnerText = "Project Maintenance";
            anchor.Attributes.Add("class", "RoundedCornerTop");

            li.Controls.Add(anchor);

            DataTable dtTabs = TabsData.GetProgramTabsForViability(DataUtils.GetInt(hfProjectId.Value), DataUtils.GetInt(ProgramId));

            foreach (DataRow dr in dtTabs.Rows)
            {
                HtmlGenericControl li1 = new HtmlGenericControl("li");
                if (dr["URL"].ToString().Contains("EnterpriseFundamentals.aspx"))
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
            BindFinJobsGrid();
            BindAttributeGrid();
        }

        protected void btnAddPlanInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int ProjectId = DataUtils.GetInt(hfProjectId.Value);

                if (btnAddPlanInfo.Text.ToLower() == "update")
                {
                    int EnterFundamentalID = DataUtils.GetInt(hfEnterFundamentalID.Value);
                    EnterpriseFundamentalsData.UpdateEnterpriseFundamentals(EnterFundamentalID, DataUtils.GetInt(ddlFiscalYear.SelectedValue.ToString()), 
                        DataUtils.GetInt(ddlPlanType.SelectedValue.ToString()),
                        DataUtils.GetInt(ddlServiceProvOrg.SelectedValue.ToString()), DataUtils.GetInt(ddlLeadAdvisor.SelectedValue.ToString()),
                       txtProjectDesc.Text, txtBusinessDesc.Text,
                        true);

                    LogMessage("Plan Info updated successfully");
                }
                else //add
                {
                    ViabilityMaintResult objViabilityMaintResult = EnterpriseFundamentalsData.AddEnterpriseFundamentals(ProjectId, DataUtils.GetInt(ddlFiscalYear.SelectedValue.ToString()), 
                        DataUtils.GetInt(ddlPlanType.SelectedValue.ToString()),
                        DataUtils.GetInt(ddlServiceProvOrg.SelectedValue.ToString()), DataUtils.GetInt(ddlLeadAdvisor.SelectedValue.ToString()),
                        txtProjectDesc.Text, txtBusinessDesc.Text);

                    if (objViabilityMaintResult.IsDuplicate && !objViabilityMaintResult.IsActive)
                        LogMessage("Plan Info already exist as in-active");
                    else if (objViabilityMaintResult.IsDuplicate)
                        LogMessage("Plan Info already exist");
                    else
                        LogMessage("Plan Info added successfully");
                }
                LoadEnterpriseFundamentals();
            }
            catch (Exception ex)
            {
                LogError(Pagename, "btnAddPlanInfo_Click", "", ex.Message);
            }
        }

        protected void btnAddMilestone_Click(object sender, EventArgs e)
        {
            try
            {
                int ProjectId = DataUtils.GetInt(hfProjectId.Value);

                if (btnAddMilestone.Text.ToLower() == "update")
                {
                    int EnterFinancialJobsID = DataUtils.GetInt(hfEnterFinancialJobsID.Value);
                    EnterpriseFundamentalsData.UpdateEnterpriseFinancialJobs(EnterFinancialJobsID, 
                        DataUtils.GetInt(ddlMilestone.SelectedValue.ToString()), DataUtils.GetDate(txtMSDate.Text), 
                        "", //txtYear.Text, 
                        DataUtils.GetDecimal(txtGrossSales.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtNetIncome.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtGrossPayroll.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtFamilyFTEmp.Text), 
                        DataUtils.GetDecimal(txtNonFamilyFTEmp.Text),
                        DataUtils.GetDecimal(txtNetworth.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtAccessFTE.Text),
                        chkActive.Checked);

                    gvFiniceJobs.EditIndex = -1;

                    LogMessage("Finalcial Job updated successfully");
                }
                else //add
                {
                    ViabilityMaintResult objViabilityMaintResult = EnterpriseFundamentalsData.AddEnterpriseFinancialJobs(ProjectId,
                        DataUtils.GetInt(ddlMilestone.SelectedValue.ToString()), DataUtils.GetDate(txtMSDate.Text), 
                        "",//txtYear.Text, 
                        DataUtils.GetDecimal(txtGrossSales.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtNetIncome.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtGrossPayroll.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtFamilyFTEmp.Text), 
                        DataUtils.GetDecimal(txtNonFamilyFTEmp.Text),
                        DataUtils.GetDecimal(txtNetworth.Text.Replace("$", "")),
                        DataUtils.GetDecimal(txtAccessFTE.Text));


                    if (objViabilityMaintResult.IsDuplicate && !objViabilityMaintResult.IsActive)
                        LogMessage("Financial Job already exist as in-active");
                    else if (objViabilityMaintResult.IsDuplicate)
                        LogMessage("Financial Job already exist");
                    else
                        LogMessage("Financial Job added successfully");
                }
                ClearFinJobsForm();
                BindFinJobsGrid();
            }
            catch (Exception ex)
            {
                LogError(Pagename, "btnAddMilestone_Click", "", ex.Message);
            }
        }

        private void EventProgramSelection()
        {
            ddlMilestone.Items.Clear();
            BindLookUP(ddlMilestone, 247);

            //if (hfProjectProgram.Value == "Admin")
            //    BindLookUP(ddlMilestone, 157);
            //else if (hfProjectProgram.Value == "Housing")
            //    BindLookUP(ddlMilestone, 160);
            //else if (hfProjectProgram.Value == "Conservation")
            //    BindLookUP(ddlMilestone, 159);
            //else if (hfProjectProgram.Value == "Lead")
            //    BindLookUP(ddlMilestone, 158);
            //else if (hfProjectProgram.Value == "Americorps")
            //    BindLookUP(ddlMilestone, 161);
            //else if (hfProjectProgram.Value == "Viability")
            //    BindLookUP(ddlMilestone, 162);
            //else if (ddlEventProgram.SelectedItem.ToString() == "Healthy Homes")
            //    BindLookUP(ddlEvent, 159);
            //else
            //{
            //    ddlMilestone.Items.Clear();
            //    ddlMilestone.Items.Insert(0, new ListItem("Select", "NA"));
            //}
        }

        private void BindFinJobsGrid()
        {
            try
            {
                DataTable dt = EnterpriseFundamentalsData.GetEnterpriseFinancialJobsList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvFiniceJobsGrid.Visible = true;
                    gvFiniceJobs.DataSource = dt;
                    gvFiniceJobs.DataBind();
                }
                else
                {
                    dvFiniceJobsGrid.Visible = false;
                    gvFiniceJobs.DataSource = null;
                    gvFiniceJobs.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindFinJobsGrid", "", ex.Message);
            }
        }

        protected void gvFiniceJobs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFiniceJobs.EditIndex = e.NewEditIndex;
            BindFinJobsGrid();
        }

        protected void gvFiniceJobs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFiniceJobs.EditIndex = -1;
            BindFinJobsGrid();
            ClearFinJobsForm();
            
            btnAddMilestone.Visible = true;
            btnAddMilestone.Text = "Submit";
        }

        private void ClearFinJobsForm()
        {
            btnAddMilestone.Text = "Add";
            cbAddMilestone.Checked = false;
            ddlMilestone.SelectedIndex = -1;
            txtMSDate.Text = "";
            //txtYear.Text = "";
            txtGrossSales.Text = "";
            txtNetIncome.Text = "";
            txtGrossPayroll.Text = "";
            txtNetworth.Text = "";
            txtFamilyFTEmp.Text = "";
            txtNonFamilyFTEmp.Text = "";
            txtAccessFTE.Text = "";
            chkActive.Enabled = false;
            spnTotalFulltime.InnerHtml = "";
        }

        protected void gvFiniceJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);
                    btnAddMilestone.Text = "Update";
                    cbAddMilestone.Checked = true;

                    if (DataUtils.GetBool(hfIsVisibleBasedOnRole.Value))
                        btnAddMilestone.Visible = true;
                    else
                        btnAddMilestone.Visible = false;

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[5].Controls[1].Visible = false;

                        Label lblEnterFinancialJobsID = e.Row.FindControl("lblEnterFinancialJobsID") as Label;
                        DataRow dr = EnterpriseFundamentalsData.GetEnterpriseFinancialJobsById(DataUtils.GetInt(lblEnterFinancialJobsID.Text));

                        hfEnterFinancialJobsID.Value = lblEnterFinancialJobsID.Text;
                        
                        PopulateDropDown(ddlMilestone, dr["StatusPt"].ToString());
                        
                        //cbVHFAInv.Checked = DataUtils.GetBool(dr["vhfa"].ToString()); ;
                        //cbRDLoan.Checked = DataUtils.GetBool(dr["RDLoan"].ToString()); ;

                        txtMSDate.Text = dr["MSDate"].ToString() == "" ? "" : Convert.ToDateTime(dr["MSDate"].ToString()).ToShortDateString();
                        //txtYear.Text = dr["Year"].ToString() ?? "";
                        txtGrossSales.Text = dr["GrossSales"].ToString() ?? "";
                        txtNetIncome.Text = dr["Netincome"].ToString() ?? "";
                        txtGrossPayroll.Text = dr["GrossPayroll"].ToString() ?? "";
                        txtFamilyFTEmp.Text = dr["FamilyFTE"].ToString() ?? "";
                        txtNonFamilyFTEmp.Text = dr["NonFamilyFTE"].ToString() ?? "";
                        txtNetworth.Text = dr["Networth"].ToString() ?? "";
                        txtAccessFTE.Text = dr["AccessFTE"].ToString() ?? "";
                        spnTotalFulltime.InnerText = (DataUtils.GetDecimal(dr["FamilyFTE"].ToString()) + DataUtils.GetDecimal(dr["NonFamilyFTE"].ToString())).ToString();

                        chkActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        chkActive.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvFiniceJobs_RowDataBound", "", ex.Message);
            }
        }

        protected void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (ddlAttribute.SelectedIndex == 0)
            {
                LogMessage("Select Attribute");
                ddlAttribute.Focus();
                return;
            }

            int EnterImpGrantID = DataUtils.GetInt(hfEnterFundamentalID.Value);

            AttributeResult obAttributeResult = EnterpriseFundamentalsData.AddEnterpriseEngagementAttributes(EnterImpGrantID,
               DataUtils.GetInt(ddlAttribute.SelectedValue.ToString()));
            ddlAttribute.SelectedIndex = -1;
            cbAddAttribute.Checked = false;

            BindAttributeGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Attribute already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Attribute already exist");
            else
                LogMessage("New Attribute added successfully");
        }

        private void BindAttributeGrid()
        {
            try
            {
                DataTable dt = EnterpriseFundamentalsData.GetEnterpriseEngagementAttributesList(DataUtils.GetInt(hfEnterFundamentalID.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvAttributeGrid.Visible = true;
                    gvAttribute.DataSource = dt;
                    gvAttribute.DataBind();
                }
                else
                {
                    dvAttributeGrid.Visible = false;
                    gvAttribute.DataSource = null;
                    gvAttribute.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAttributeGrid", "", ex.Message);
            }
        }

        protected void gvAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAttribute.EditIndex = e.NewEditIndex;
            BindAttributeGrid();
        }

        protected void gvAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAttribute.EditIndex = -1;
            BindAttributeGrid();
        }

        protected void gvAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int EnterEngageAttrID = DataUtils.GetInt(((Label)gvAttribute.Rows[rowIndex].FindControl("lblEnterEngageAttrID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAttribute.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            EnterpriseFundamentalsData.UpdateEnterpriseEngagementAttributes(EnterEngageAttrID, RowIsActive);
            gvAttribute.EditIndex = -1;

            BindAttributeGrid();

            LogMessage("Attribute updated successfully");
        }
    }
}