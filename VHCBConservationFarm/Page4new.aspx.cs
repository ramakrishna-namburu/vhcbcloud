using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;

namespace VHCBConservationFarm
{
    public partial class Page4new : System.Web.UI.Page
    {
        string Pagename = "Page4new";
        string projectNumber = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ProjectNumber"] == null)
                Response.Redirect("Login.aspx");
            else
                projectNumber = Session["ProjectNumber"].ToString();

            if (!IsPostBack)
            {
               
                BindControls();
                BindForm();
                BindGrids();
            }
        }

        private void BindForm()
        {
            DataRow drPage4tDetails = ConservationApplicationData.GetConserveFarmPage4New(projectNumber);

            if (drPage4tDetails != null)
            {
                hfProjectId.Value = drPage4tDetails["ProjectId"].ToString();
                hfConserveId.Value = drPage4tDetails["ConserveID"].ToString();
                hfTotaAcres.Value = drPage4tDetails["ConservedAcres"].ToString();

                //txtSurfaceWaterDesc.Text = drPage4tDetails["SurfaceWaterDesc"].ToString();
                PopulateDropDown(ddlTacticalBasin, drPage4tDetails["TacticalBasin"].ToString());
                PopulateWaterShedDropDown(ddlWaterShedNew, drPage4tDetails["Watershed1"].ToString());
                PopulateWaterShedDropDown(ddlWaterShedNewSec, drPage4tDetails["Watershed2"].ToString());
                BuildWaterShed();
                BuildWaterShedSec();
                PopulateDropDownText(ddlSubWatershedNew, drPage4tDetails["SubBasinHUC8"].ToString());
                PopulatedHUC12(ddlHUC12Primary, drPage4tDetails["SubWatershedHUC12"].ToString());


                PopulateDropDownText(ddlSubWatershedNewSec, drPage4tDetails["SubBasin2HUC8"].ToString());
                PopulatedHUC12(ddlHUC12Secondary, drPage4tDetails["SubWatershed2HUC12"].ToString());

                txtWetlands.Text = drPage4tDetails["Wetlands"].ToString();
                txtFloodPlain.Text = drPage4tDetails["FloodPlain"].ToString();
                txtWaterQualityIssues.Text = drPage4tDetails["WaterQualityIssues"].ToString();
                txtWQManagepractices.Text = drPage4tDetails["WQManagepractices"].ToString();
               // txtin.Text = drPage4tDetails["WasteInfrastructure"].ToString();
                txtInfrastuctureDescription.Text = drPage4tDetails["InfrastuctureDescription"].ToString();



                //DrainageBasin, , , , , , 
                //ForestManagementPlan, 


                if (DataUtils.GetBool(drPage4tDetails["SurfaceWaters"].ToString()))
                {
                    dvNewSurfaceWaters.Visible = true;
                    rdBtnSurfaceWaters.SelectedIndex = 0;
                    //divPrimary.Visible = true;
                }
                else
                {
                    dvNewSurfaceWaters.Visible = false;
                    rdBtnSurfaceWaters.SelectedIndex = 1;
                    //divPrimary.Visible = false;
                }

                if (DataUtils.GetBool(drPage4tDetails["DrainageDitches"].ToString()))
                    rdbtnDrainageDitches.SelectedIndex = 0;
                else
                    rdbtnDrainageDitches.SelectedIndex = 1;

                if (DataUtils.GetBool(drPage4tDetails["DrainageTiles"].ToString()))
                    rdbtnDrainageTiles.SelectedIndex = 0;
                else
                    rdbtnDrainageTiles.SelectedIndex = 1;

                if (DataUtils.GetBool(drPage4tDetails["WasteWaterManage"].ToString()))
                {
                    spnInfrastuctureDescription.Visible = true;
                    txtInfrastuctureDescription.Visible = true;
                    rdbtnWastewatermanage.SelectedIndex = 0;
                }

                else
                {
                    spnInfrastuctureDescription.Visible = false;
                    txtInfrastuctureDescription.Visible = false;
                    rdbtnWastewatermanage.SelectedIndex = 1;
                }

                if (DataUtils.GetBool(drPage4tDetails["LivestockExcluded"].ToString()))
                    rdbtnLivestockExcluded.SelectedIndex = 0;
                else
                    rdbtnLivestockExcluded.SelectedIndex = 1;

                if (DataUtils.GetBool(drPage4tDetails["ForestManagement"].ToString()))
                {
                    txtForestplandate.Text = drPage4tDetails["ForestPlanDate"].ToString();
                    txtForestplandate.Visible = true;
                    spnForestMgtPlan.Visible = true;
                    rdbtnManagedTimber.SelectedIndex = 0;
                }

                else
                {
                    txtForestplandate.Text = drPage4tDetails["ForestPlanDate"].ToString();
                    txtForestplandate.Visible = false;
                    spnForestMgtPlan.Visible = false;
                    rdbtnManagedTimber.SelectedIndex = 1;

                }
            }

            DataRow drConserver = ConservationApplicationData.GetConserveFarmPage4Newconserve(projectNumber);

            if(drConserver != null)
            {
                txtTillable.Text = drConserver["Tillable"].ToString();

                txtPasture.Text = drConserver["Pasture"].ToString();
                txtHay.Text = drConserver["Hay"].ToString();
                txtUnManaged.Text = drConserver["Unmanaged"].ToString();
                txtFarmResident.Text = drConserver["FarmResident"].ToString();
                txtNaturalRec.Text = drConserver["Naturalrec"].ToString();
                txtWooded.Text = drConserver["Wooded"].ToString();
                txtSugarbush.Text = drConserver["Sugarbush"].ToString();
                txtTaps.Text = drConserver["Taps"].ToString();


                txtPrimefoot.Text = drConserver["Primefoot"].ToString();
                txtPrimefootPC.Text = drConserver["PrimefootPC"].ToString();
                txtPrimeNonFoot.Text = drConserver["PrimeNonFoot"].ToString();
                txtPrimeNonFootPC.Text = drConserver["PrimeNonFootPC"].ToString();
                spnPrimaryfootTotal.InnerHtml = drConserver["PrimeTotal"].ToString();

                txtStatewideFoot.Text = drConserver["StatewideFoot"].ToString();
                txtStatewideFootPC.Text = drConserver["StatewideFootPC"].ToString();
                txtStatewideNonFoot.Text = drConserver["StatewideNonFoot"].ToString();
                txtStatewideNonFootPC.Text = drConserver["StatewideNonFootPC"].ToString();
                spnStatewidefootTotal.InnerHtml = drConserver["StatewideTotal"].ToString();

            }
        }
        private void PopulatedHUC12(DropDownList ddl, string DBSelectedvalue)
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


        private void BindGrids()
        {

            BindSurfacewatersGrid();
            BindFarmProductsGrid();
            BindProjectAttributeGrid();
            BindPAGrid();
            BindAltEnergyGrid();
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
            //BindLookUP(ddlConservationTrack, 7);
            ////BindPrimaryStewardOrganization();
            //BindEasementHolder(ddlEasementHolder);
            //BindLookUP(ddlAcreageDescription, 97);
            //BindLookUP(ddlWatershed, 143);
            BindLookUP(ddlWaterShedNew, 143);
            BindLookUP(ddlWaterShedNewSec, 143);
            BindLookUP(ddlWaterBody, 140);
            //BindLookUP(ddlGeoSignificance, 255);
            //BindLookUP(ddlSubwatershed, 261);
            //BindLookUP(ddlTrail, 1270);
            //BindLookUP(ddlAllowedSpecialUses, 1271);
            BindHUC12CheckBoxList();
            BindcblHUC12SecCheckBoxList();
            BindLookUP(ddlTacticalBasin, 2284);
            BindLookUP(ddlFormProducts, 106);
            BindLookUP(ddlProjectAttribute, 260);
            BindLookUP(ddlPA, 28);
            BindLookUP(ddlAltEnergy, 259);
        }

        protected void rdBtnSurfaceWaters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdBtnSurfaceWaters.SelectedItem.Value.Trim() == "Yes")
            {
                dvNewSurfaceWaters.Visible = true;
                //divPrimary.Visible = true;
            }
            else
            {
                dvNewSurfaceWaters.Visible = false;
                //divPrimary.Visible = false;
            }
            }

        //protected void ImgSurfaceWaters_Click(object sender, ImageClickEventArgs e)
        //{

        //}
        private void BindHUC12CheckBoxList()
        {
            string order = "";
            try
            {
                //if (rdHUC12order.SelectedItem != null)
                //    order = rdHUC12order.SelectedItem.Text;
                order = "Name";
                ddlHUC12Primary.Items.Clear();
                ddlHUC12Primary.DataSource = LookupValuesData.GetHUCList(order);
                ddlHUC12Primary.DataValueField = "HUCID";
                ddlHUC12Primary.DataTextField = "HUC12Name";
                ddlHUC12Primary.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void BindcblHUC12SecCheckBoxList()
        {
            string order = "";
            try
            {
                //if (rdHUC12order.SelectedItem != null)
                //    order = rdHUC12order.SelectedItem.Text;
                order = "Name";
                ddlHUC12Secondary.Items.Clear();
                ddlHUC12Secondary.DataSource = LookupValuesData.GetHUCList(order);
                ddlHUC12Secondary.DataValueField = "HUCID";
                ddlHUC12Secondary.DataTextField = "HUC12Name";
                ddlHUC12Secondary.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        private void ClearSurfaceWatersForm()
        {

            ddlWaterBody.SelectedIndex = -1;
            txtOtherStream.Text = "";
            txtFrontageFeet.Text = "";


            cbAddSurfaceWaters.Checked = false;
        }
        private void BindSurfacewatersGrid()
        {
            try
            {
                DataTable dtSurfaceWaters = ConservationSummaryData.GetSurfaceWatersList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dtSurfaceWaters.Rows.Count > 0)
                {
                    dvSurfaceWatersGrid.Visible = true;
                    gvSurfaceWaters.DataSource = dtSurfaceWaters;
                    gvSurfaceWaters.DataBind();
                }
                else
                {
                    dvSurfaceWatersGrid.Visible = false;
                    gvSurfaceWaters.DataSource = null;
                    gvSurfaceWaters.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindSurfacewatersGrid", "", ex.Message);
            }
        }
        protected void gvSurfaceWaters_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cbActive.Checked = true;
            cbActive.Enabled = false;
            cbAddSurfaceWaters.Checked = false;
            ClearSurfaceWatersForm();
            btnAddSurfaceWaters.Text = "Add";

            gvSurfaceWaters.EditIndex = -1;
            BindSurfacewatersGrid();

            btnAddSurfaceWaters.Visible = true;
        }

        protected void gvSurfaceWaters_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSurfaceWaters.EditIndex = e.NewEditIndex;
            BindSurfacewatersGrid();
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

        private void PopulateDropDownText(DropDownList ddl, string DBSelectedvalue)
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

        private void PopulateWaterShedDropDown(DropDownList ddl, string DBSelectedvalue)
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

        protected void gvSurfaceWaters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);
                    btnAddSurfaceWaters.Text = "Update";

                    //if (DataUtils.GetBool(hfIsVisibleBasedOnRole.Value))
                    //    btnAddSurfaceWaters.Visible = true;
                    //else
                    //    btnAddSurfaceWaters.Visible = false;

                    cbAddSurfaceWaters.Checked = true;

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[5].Controls[1].Visible = false;
                        Label lblSurfaceWatersID = e.Row.FindControl("lblSurfaceWatersID") as Label;
                        DataRow dr = ConservationSummaryData.GetProjectSurfaceWatersById(Convert.ToInt32(lblSurfaceWatersID.Text));


                        PopulateDropDown(ddlWaterBody, dr["LKWaterBody"].ToString());
                        txtFrontageFeet.Text = dr["FrontageFeet"].ToString();
                        txtOtherStream.Text = dr["OtherWater"].ToString();

                        cbActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        cbActive.Enabled = true;
                        hfSurfaceWatersId.Value = lblSurfaceWatersID.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvSurfaceWaters_RowDataBound", "", ex.Message);
            }
        }
        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }
        protected void btnAddSurfaceWaters_Click(object sender, EventArgs e)
        {

            if (ddlWaterBody.SelectedIndex == 0)
            {
                LogMessage("Select Water Body");
                ddlWaterBody.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFrontageFeet.Text.ToString()) == true)
            {
                LogMessage("Enter Frontage");
                txtFrontageFeet.Focus();
                return;
            }
            if (DataUtils.GetDecimal(txtFrontageFeet.Text) <= 0)
            {
                LogMessage("Enter valid Frontage");
                txtFrontageFeet.Focus();
                return;
            }

            if (btnAddSurfaceWaters.Text.ToLower() == "update")
            {
                int SurfaceWatersId = Convert.ToInt32(hfSurfaceWatersId.Value);

                ConservationSummaryData.UpdateProjectSurfaceWaters(SurfaceWatersId,
                    0,
                    DataUtils.GetInt(ddlWaterBody.SelectedValue.ToString()),
                    DataUtils.GetInt(txtFrontageFeet.Text), txtOtherStream.Text, 0, cbActive.Checked);

                hfSurfaceWatersId.Value = "";
                btnAddSurfaceWaters.Text = "Add";
                cbActive.Checked = true;
                cbActive.Enabled = false;
                LogMessage("Surface Waters updated successfully");
            }
            else
            {
                Result objResult = ConservationSummaryData.AddProjectSurfaceWaters(DataUtils.GetInt(hfProjectId.Value),
              0,
               0,
                DataUtils.GetInt(ddlWaterBody.SelectedValue.ToString()), DataUtils.GetInt(txtFrontageFeet.Text), txtOtherStream.Text,
               0);

                if (objResult.IsDuplicate && !objResult.IsActive)
                    LogMessage("Surface Waters already exist as in-active");
                else if (objResult.IsDuplicate)
                    LogMessage("Surface Waters already exist");
                else
                    LogMessage("New Surface Waters added successfully");
            }

            gvSurfaceWaters.EditIndex = -1;
            BindSurfacewatersGrid();
            ClearSurfaceWatersForm();
            //dvSurfaceWatersGrid.Visible = true;
            cbAddSurfaceWaters.Checked = false;
        }
        private void BindLookupSubWatershed(int TypeId)
        {
            try
            {
                ddlSubWatershedNew.Items.Clear();
                ddlSubWatershedNew.DataSource = LookupMaintenanceData.GetLkLookupSubValues(TypeId, true);
                ddlSubWatershedNew.DataValueField = "subtypeid";// "subtypeid";
                ddlSubWatershedNew.DataTextField = "SubDescription";
                ddlSubWatershedNew.DataBind();
                ddlSubWatershedNew.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        private void BindLookupSubWatershedSec(int TypeId)
        {
            try
            {
                ddlSubWatershedNewSec.Items.Clear();
                ddlSubWatershedNewSec.DataSource = LookupMaintenanceData.GetLkLookupSubValues(TypeId, true);
                ddlSubWatershedNewSec.DataValueField = "subtypeid";
                ddlSubWatershedNewSec.DataTextField = "SubDescription";
                ddlSubWatershedNewSec.DataBind();
                ddlSubWatershedNewSec.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        protected void ddlWaterShedNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildWaterShed();
        }

        protected void BuildWaterShed()
        {
            if (ddlWaterShedNew.SelectedIndex > 0)
                BindLookupSubWatershed(DataUtils.GetInt(ddlWaterShedNew.SelectedValue));
            else
                ddlSubWatershedNew.Items.Clear();
        }
        protected void ddlWaterShedNewSec_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BuildWaterShedSec();
        }

        protected void BuildWaterShedSec()
        {
            if (ddlWaterShedNewSec.SelectedIndex > 0)
                BindLookupSubWatershedSec(DataUtils.GetInt(ddlWaterShedNewSec.SelectedValue));
            else
                ddlSubWatershedNewSec.Items.Clear();
        }
        protected void rdbtnWastewatermanage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbtnWastewatermanage.SelectedItem.Value.Trim() == "Yes")
            {
                spnInfrastuctureDescription.Visible = true;
                txtInfrastuctureDescription.Visible = true;
            }
            else
            {
                spnInfrastuctureDescription.Visible = false;
                txtInfrastuctureDescription.Visible = false;
            }
        }

        protected void rdbtnManagedTimber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbtnManagedTimber.SelectedItem.Value.Trim() == "Yes")
            {
                txtForestplandate.Visible = true;
                spnForestMgtPlan.Visible = true;
            }
            else
            {
                txtForestplandate.Visible = false;
                spnForestMgtPlan.Visible = false;
            }
        }

        protected void gvFarmProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFarmProducts.EditIndex = e.NewEditIndex;
            BindFarmProductsGrid();
        }

        protected void gvFarmProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            string strAcres = ((TextBox)gvFarmProducts.Rows[rowIndex].FindControl("txtAcres")).Text;

            //if (string.IsNullOrWhiteSpace(strAcres) == true)
            //{
            //    LogMessage("Enter Acres");
            //    return;
            //}
            //if (DataUtils.GetInt(strAcres) <= 0)
            //{
            //    LogMessage("Enter valid Acres");
            //    return;
            //}

            int ConserveProductID = DataUtils.GetInt(((Label)gvFarmProducts.Rows[rowIndex].FindControl("lblConserveProductID")).Text);
            int Acres = DataUtils.GetInt(strAcres);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvFarmProducts.Rows[rowIndex].FindControl("chkActive")).Checked);
            bool Organic = Convert.ToBoolean(((CheckBox)gvFarmProducts.Rows[rowIndex].FindControl("chkOrganic")).Checked);

            ConservationSummaryData.UpdateConserveProducts(ConserveProductID, Acres, Organic, RowIsActive);
            gvFarmProducts.EditIndex = -1;

            BindFarmProductsGrid();

            LogMessage("Farm Product updated successfully");
        }

        protected void gvFarmProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFarmProducts.EditIndex = -1;
            BindFarmProductsGrid();
        }

        private void BindFarmProductsGrid()
        {
            try
            {
                DataTable dtTrails = ConservationSummaryData.GetConserveProductsList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dtTrails.Rows.Count > 0)
                {
                    dvFarmProductsGrid.Visible = true;
                    gvFarmProducts.DataSource = dtTrails;
                    gvFarmProducts.DataBind();
                }
                else
                {
                    dvFarmProductsGrid.Visible = false;
                    gvFarmProducts.DataSource = null;
                    gvFarmProducts.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindFarmProductsGrid", "", ex.Message);
            }
        }
        protected void btnFarmProducts_Click(object sender, EventArgs e)
        {
            if (ddlFormProducts.SelectedIndex == 0)
            {
                LogMessage("Select Farm Product");
                ddlFormProducts.Focus();

                return;
            }

            //if (string.IsNullOrWhiteSpace(txtProductAcres.Text) == true)
            //{
            //    LogMessage("Enter Farm Product Acres");
            //    txtProductAcres.Focus();
            //    return;
            //}
            //if (DataUtils.GetInt(txtProductAcres.Text) <= 0)
            //{
            //    LogMessage("Enter valid Farm Product Acres");
            //    txtProductAcres.Focus();
            //    return;
            //}

            Result objResult = ConservationSummaryData.AddConserveProducts(DataUtils.GetInt(hfProjectId.Value),
             DataUtils.GetInt(ddlFormProducts.SelectedValue.ToString()),
             DataUtils.GetInt(txtProductAcres.Text),
             cbOrganic.Checked);

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Farm Product already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Farm Product already exist");
            else
                LogMessage("New Farm Product added successfully");

            ddlFormProducts.SelectedIndex = -1;
            txtProductAcres.Text = "";
            cbOrganic.Checked = false;
            BindFarmProductsGrid();
        }

        private void BindProjectAttributeGrid()
        {
            try
            {
                DataTable dt = ConservationAttributeData.GetConserveAttribProjList(DataUtils.GetInt(hfConserveId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvProjectAttributeGrid.Visible = true;
                    gvProjAttribute.DataSource = dt;
                    gvProjAttribute.DataBind();
                }
                else
                {
                    dvProjectAttributeGrid.Visible = false;
                    gvProjAttribute.DataSource = null;
                    gvProjAttribute.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindProjectAttributeGrid", "", ex.Message);
            }
        }

        protected void AddProjectAttribute_Click(object sender, EventArgs e)
        {
            if (ddlProjectAttribute.SelectedIndex == 0)
            {
                LogMessage("Select Project Attribute");
                ddlProjectAttribute.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddConserveAttribProj(DataUtils.GetInt(hfConserveId.Value),
                DataUtils.GetInt(ddlProjectAttribute.SelectedValue.ToString()));
            ddlProjectAttribute.SelectedIndex = -1;
            cbAddProjectAttribute.Checked = false;

            BindProjectAttributeGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Project Attribute already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("ProjectAttribute already exist");
            else
                LogMessage("New Project Attribute added successfully");
        }

        protected void gvProjAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProjAttribute.EditIndex = e.NewEditIndex;
            BindProjectAttributeGrid();
        }

        protected void gvProjAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProjAttribute.EditIndex = -1;
            BindProjectAttributeGrid();
        }

        protected void gvProjAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ConserveAttribID = DataUtils.GetInt(((Label)gvProjAttribute.Rows[rowIndex].FindControl("lblConserveAttribID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvProjAttribute.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateConserveAttribProj(ConserveAttribID, RowIsActive);
            gvProjAttribute.EditIndex = -1;

            BindProjectAttributeGrid();

            LogMessage("Project Attribute updated successfully");
        }

        private void BindPAGrid()
        {
            try
            {
                DataTable dt = ConservationAttributeData.GetPublicAccessList(DataUtils.GetInt(hfConserveId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvPAGrid.Visible = true;
                    gvPA.DataSource = dt;
                    gvPA.DataBind();
                }
                else
                {
                    dvPAGrid.Visible = false;
                    gvPA.DataSource = null;
                    gvPA.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindPAGrid", "", ex.Message);
            }
        }

        protected void btnAddPA_Click(object sender, EventArgs e)
        {
            if (ddlPA.SelectedIndex == 0)
            {
                LogMessage("Select Public Access");
                ddlPA.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddPublicAccess(DataUtils.GetInt(hfConserveId.Value),
                DataUtils.GetInt(ddlPA.SelectedValue.ToString()));
            ddlPA.SelectedIndex = -1;
            cbAddPA.Checked = false;

            BindPAGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Type of Public Access already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Type of Public Access already exist");
            else
                LogMessage("New Type of Public Access added successfully");
        }

        protected void gvPA_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPA.EditIndex = e.NewEditIndex;
            BindPAGrid();
        }

        protected void gvPA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPA.EditIndex = -1;
            BindPAGrid();
        }

        protected void gvPA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ConservePAcessID = DataUtils.GetInt(((Label)gvPA.Rows[rowIndex].FindControl("lblConservePAcessID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvPA.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdatePublicAccess(ConservePAcessID, RowIsActive);
            gvPA.EditIndex = -1;

            BindPAGrid();

            LogMessage("Public Access updated successfully");
        }

        protected void gvAltEnergy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAltEnergy.EditIndex = e.NewEditIndex;
            BindAltEnergyGrid();
        }

        protected void gvAltEnergy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAltEnergy.EditIndex = -1;
            BindAltEnergyGrid();
        }

        protected void gvAltEnergy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ConsserveAltEnergyID = DataUtils.GetInt(((Label)gvAltEnergy.Rows[rowIndex].FindControl("lblConsserveAltEnergyID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAltEnergy.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateAltEnergy(ConsserveAltEnergyID, RowIsActive);
            gvAltEnergy.EditIndex = -1;

            BindAltEnergyGrid();

            LogMessage("Alternative Energy updated successfully");
        }

        private void BindAltEnergyGrid()
        {
            try
            {
                DataTable dt = ConservationAttributeData.GetAltEnergyList(DataUtils.GetInt(hfConserveId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvAltEnergyGrid.Visible = true;
                    gvAltEnergy.DataSource = dt;
                    gvAltEnergy.DataBind();
                }
                else
                {
                    dvAltEnergyGrid.Visible = false;
                    gvAltEnergy.DataSource = null;
                    gvAltEnergy.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAltEnergyGrid", "", ex.Message);
            }
        }

        protected void btnAddAltEnergy_Click(object sender, EventArgs e)
        {
            if (ddlAltEnergy.SelectedIndex == 0)
            {
                LogMessage("Select Alternative Energy");
                ddlAltEnergy.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddAltEnergy(DataUtils.GetInt(hfConserveId.Value),
               DataUtils.GetInt(ddlAltEnergy.SelectedValue.ToString()));
            ddlAltEnergy.SelectedIndex = -1;
            cbAddAltEnergy.Checked = false;

            BindAltEnergyGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Alternative Energy already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Alternative Energy already exist");
            else
                LogMessage("New Alternative Energy added successfully");
        }

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }

        private void saveData()
        {
            string trailName = string.Empty;
            string trailNameIds = string.Empty;

            decimal totAcres = DataUtils.GetDecimal(txtUnManaged.Text) + DataUtils.GetDecimal(txtSugarbush.Text) + DataUtils.GetDecimal(txtPasture.Text) + DataUtils.GetDecimal(txtFarmResident.Text)
                + DataUtils.GetDecimal(txtHay.Text) + DataUtils.GetDecimal(txtWooded.Text) + DataUtils.GetDecimal(txtNaturalRec.Text) + DataUtils.GetDecimal(txtTillable.Text);


            decimal pf = DataUtils.GetDecimal(txtPrimefoot.Text);
            decimal pnf = DataUtils.GetDecimal(txtPrimeNonFoot.Text);
            decimal totalprime = pf + pnf;

            decimal pfc = 0;
            decimal pnfc = 0;

            if (totAcres > 0)
            {
                pfc = pf / totAcres * 100;
                pnfc = pnf / totAcres * 100;
            }

            decimal spf = DataUtils.GetDecimal(txtStatewideFoot.Text);
            decimal spnf = DataUtils.GetDecimal(txtStatewideNonFoot.Text);
            decimal totalState = spf + spnf;

            decimal spfc = 0;
            decimal spnfc = 0;

            if (totalState > 0)
            {
                spfc = spf / totAcres * 100;
                spnfc = spnf / totAcres * 100;
            }

            string SubWatershedNew = "";
            string SubWatershedNewSec = "";
            int SubWatershedNewInt = 0;
            int SubWatershedNewSecInt = 0;

            if (ddlSubWatershedNew.SelectedItem != null)
            {
                SubWatershedNew = ddlSubWatershedNew.SelectedItem.ToString();
                SubWatershedNewInt = DataUtils.GetInt(ddlSubWatershedNew.SelectedValue);
            }

            if (ddlSubWatershedNewSec.SelectedItem != null)
            {
                SubWatershedNewSec = ddlSubWatershedNewSec.SelectedItem.ToString();
                SubWatershedNewSecInt = DataUtils.GetInt(ddlSubWatershedNewSec.SelectedValue);
            }

            ConservationApplicationData.ConservationFarmApplicationPage4(projectNumber, DataUtils.GetBool(rdBtnSurfaceWaters.SelectedValue.Trim()), ddlTacticalBasin.SelectedValue.ToString(),
                ddlWaterShedNew.SelectedItem.ToString(), SubWatershedNew, ddlHUC12Primary.SelectedItem.ToString(),
                ddlWaterShedNewSec.SelectedItem.ToString(), SubWatershedNewSec, ddlHUC12Secondary.SelectedItem.ToString(),
                DataUtils.GetDecimal(txtWetlands.Text), DataUtils.GetDecimal(txtFloodPlain.Text), txtWaterQualityIssues.Text, txtWQManagepractices.Text, "", //DrainageBasin
                DataUtils.GetBool(rdbtnDrainageDitches.SelectedValue.Trim()), DataUtils.GetBool(rdbtnDrainageTiles.SelectedValue.Trim()), DataUtils.GetBool(rdbtnWastewatermanage.SelectedValue.Trim()), "", //WasteInfrastucture
                txtInfrastuctureDescription.Text, DataUtils.GetBool(rdbtnLivestockExcluded.SelectedValue.Trim()), DataUtils.GetBool(rdbtnManagedTimber.SelectedValue.Trim()), "",//ForestManagementPlan
                DataUtils.GetDate(txtForestplandate.Text),
                pf, pfc, pnf, pnfc, totalprime,
                spf, spfc, spnf, spnfc, totalState,
                DataUtils.GetInt(ddlWaterShedNew.SelectedValue), SubWatershedNewInt, DataUtils.GetInt(ddlHUC12Primary.SelectedValue),
                DataUtils.GetInt(ddlWaterShedNewSec.SelectedValue), SubWatershedNewSecInt, DataUtils.GetInt(ddlHUC12Secondary.SelectedValue)
                );

            // BindTrailCheckBoxList();
            LogMessage("Conservation Form Data Added Successfully");
 
            ConservationApplicationData.ConserveFarmPage4NewConserve(projectNumber, DataUtils.GetDecimal(txtPasture.Text), DataUtils.GetDecimal(txtHay.Text), DataUtils.GetDecimal(txtUnManaged.Text), DataUtils.GetDecimal(txtFarmResident.Text),
                DataUtils.GetDecimal(txtNaturalRec.Text), DataUtils.GetDecimal(txtWooded.Text), DataUtils.GetDecimal(txtSugarbush.Text), DataUtils.GetDecimal(txtTillable.Text),  DataUtils.GetInt(txtTaps.Text),
                totalprime, totalState);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("ThirdPage.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("FarmManagement.aspx");
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrids();
        }
    }
}