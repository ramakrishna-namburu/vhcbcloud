using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;

namespace ConserveNatRec
{
    public partial class Page5 : System.Web.UI.Page
    {
        string Pagename = "FarmManagement";
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

                LoadPage();

                BindCMGrid();
            }
        }

        private void BindControls()
        {
            BindLookUP(ddlFarmSize, 2277);
            BindLookUP(ddlCM, 2288);

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
        private void LoadPage()
        {
            if (projectNumber != "")
            {
                DataRow dr = ConservationApplicationData.GetFarmOperationFarmManagement(projectNumber);

                if (dr != null)
                {
                    hfConserveId.Value = dr["ConserveID"].ToString();

                    PopulateDropDownByText(ddlFarmSize, dr["FarmSize"].ToString());

                    if (DataUtils.GetBool(dr["RAPCompliance"].ToString()))
                        rdbtnRAPCompliance.SelectedIndex = 0;
                    else
                        rdbtnRAPCompliance.SelectedIndex = 1;

                    txtRentedLand.Text = dr["RentedLand"].ToString();
                    txtFullTime.Text = dr["FullTime"].ToString();
                    txtPartTime.Text = dr["PartTime"].ToString();
                    txtFullTimeSeasonal.Text = dr["FullTimeSeasonal"].ToString();
                    txtPartTimeSeasonal.Text = dr["PartTimeSeasonal"].ToString();

                    txtGrossIncome.Text = Regex.Replace(dr["GrossIncome"].ToString(), "[^0-9a-zA-Z.]+", ""); 
                    txtGrossIncomeDescription.Text = dr["GrossIncomeDescription"].ToString();

                    txtProductsSold.Text = dr["ProductsSold"].ToString();

                    if (DataUtils.GetBool(dr["WorkedWithVHCB"].ToString()))
                    {
                        rdbtWorkedWithVHCB.SelectedIndex = 0;
                        spanVHCBDesc.Visible = true;
                        txtVHCBWorkDescription.Visible = true;

                    }
                       
                    else
                    {
                        rdbtWorkedWithVHCB.SelectedIndex = 1;
                        spanVHCBDesc.Visible = false;
                        txtVHCBWorkDescription.Visible = false;
                    }
                        

                    txtVHCBWorkDescription.Text = dr["VHCBWorkDescription"].ToString();

                    if (DataUtils.GetBool(dr["WrittenLease"].ToString()))
                        rdbtWrittenLease.SelectedIndex = 0;
                    else
                        rdbtWrittenLease.SelectedIndex = 1;

                    //if (DataUtils.GetBool(dr["CompletedBusinessPlan"].ToString()))
                    //{
                    //    rdbtCompletedBusinessPlan.SelectedIndex = 0;
                    //    //tblOptinalQuestions.Visible = false;
                    //}
                    //else
                    //{
                    //    rdbtCompletedBusinessPlan.SelectedIndex = 1;
                    //    //tblOptinalQuestions.Visible = true;
                    //}

                    //if (DataUtils.GetBool(dr["ShareBusinessPlan"].ToString()))
                    //    rdbtShareBusinessPlan.SelectedIndex = 0;
                    //else
                    //    rdbtShareBusinessPlan.SelectedIndex = 1;

                    txtMitigateClimate.Text = dr["MitigateClimate"].ToString();

                    if (DataUtils.GetBool(dr["HEL"].ToString()))
                        rdbtnHEL.SelectedIndex = 0;
                    else
                        rdbtnHEL.SelectedIndex = 1;

                    if (DataUtils.GetBool(dr["NutrientPlan"].ToString()))
                        rdbtnNutrientPlan.SelectedIndex = 0;
                    else
                        rdbtnNutrientPlan.SelectedIndex = 1;

                    PopulateDropDown(ddlDumps, dr["Dumps"].ToString());

                    if (DataUtils.GetBool(dr["ExistingInfastructure"].ToString()))
                    {
                        spanExistingInfra.Visible = true;
                        txtExistingInfrastuctureDescription.Visible = true;
                        rdbExistingInfrastructure.SelectedIndex = 0;
                    }   
                    else
                    {
                        spanExistingInfra.Visible = false;
                        txtExistingInfrastuctureDescription.Visible = false;
                        rdbExistingInfrastructure.SelectedIndex = 1;
                    }
                       

                    txtExistingInfrastuctureDescription.Text = dr["ExistingInfrastuctureDescription"].ToString();
                    txtFarmOperation.Text = dr["FarmOperation"].ToString();

                    //if (DataUtils.GetBool(dr["OtherTechnicalAdvisors"].ToString()))
                    //    rdbtOtherTechnicalAdvisors.SelectedIndex = 0;
                    //else
                    //    rdbtOtherTechnicalAdvisors.SelectedIndex = 1;

                    //if (DataUtils.GetBool(dr["CurrentBusinessPlan"].ToString()))
                    //    rdbtnCurrentBusinessPlan.SelectedIndex = 0;
                    //else
                    //    rdbtnCurrentBusinessPlan.SelectedIndex = 1;


                    //foreach (ListItem li in cblConservationMeasures.Items)
                    //{
                    //    if (dr["ConservationMeasures"].ToString().Split(',').ToList().Contains(li.Text))
                    //    {
                    //        li.Selected = true;
                    //    }
                    //}

                }
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("Page4New.aspx");
        }

        private void saveData()
        {
            string conservationMeasuresList = string.Empty;

            //foreach (ListItem listItem in cblConservationMeasures.Items)
            //{
            //    if (listItem.Selected == true)
            //    {
            //        if (conservationMeasuresList == string.Empty)
            //        {
            //            conservationMeasuresList = listItem.Text;
            //        }
            //        else
            //        {
            //            conservationMeasuresList = conservationMeasuresList + ',' + listItem.Text;
            //        }
            //    }
            //}


            ConservationApplicationData.FarmOperationFarmManagement(projectNumber, ddlFarmSize.SelectedItem.Text, DataUtils.GetInt(ddlFarmSize.SelectedValue), DataUtils.GetBool(rdbtnRAPCompliance.SelectedValue),
                DataUtils.GetDecimal(txtRentedLand.Text), DataUtils.GetDecimal(txtFullTime.Text), DataUtils.GetDecimal(txtPartTime.Text),  DataUtils.GetDecimal(txtFullTimeSeasonal.Text), DataUtils.GetDecimal(txtPartTimeSeasonal.Text),
                txtGrossIncome.Text,
                txtGrossIncomeDescription.Text, txtProductsSold.Text, DataUtils.GetBool(rdbtWorkedWithVHCB.SelectedValue), txtVHCBWorkDescription.Text, 
                DataUtils.GetBool(rdbtWrittenLease.SelectedValue), txtMitigateClimate.Text, DataUtils.GetBool(rdbtnHEL.SelectedValue), DataUtils.GetBool(rdbtnNutrientPlan.SelectedValue), ddlDumps.SelectedValue, DataUtils.GetBool(rdbExistingInfrastructure.SelectedValue),
                txtExistingInfrastuctureDescription.Text, txtFarmOperation.Text);

            LogMessage("Conservation Application Data Added Successfully");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("EasementTerms.aspx");
        }
        private void PopulateDropDown(DropDownList ddl, string DBSelectedvalue)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedvalue.Trim() == item.Value.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }
        private void PopulateDropDownByText(DropDownList ddl, string DBSelectedText)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedText.Trim() == item.Text.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
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

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }

        protected void rdbtWorkedWithVHCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbtWorkedWithVHCB.SelectedValue.Trim() == "No")
            {
                spanVHCBDesc.Visible = false;
                txtVHCBWorkDescription.Visible = false;
            }
            else
            {
                spanVHCBDesc.Visible = true;
                txtVHCBWorkDescription.Visible = true;
            }
        }

        protected void rdbExistingInfrastructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbExistingInfrastructure.SelectedValue.Trim() == "No")
            {
                spanExistingInfra.Visible = false;
                txtExistingInfrastuctureDescription.Visible = false;
            }
            else
            {
                spanExistingInfra.Visible = true;
                txtExistingInfrastuctureDescription.Visible = true;
            }
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

        protected void gvCM_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCM.EditIndex = e.NewEditIndex;
            BindCMGrid();
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindCMGrid();
        }
    }
}