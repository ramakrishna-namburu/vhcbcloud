﻿using DataAccessLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class EntityMaintenance : System.Web.UI.Page
    {
        string Pagename = "EntityMaintenance";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            //var ctrlName = Request.Params[Page.postEventSourceID];
            //var args = Request.Params[Page.postEventArgumentID];

            //HandleCustomPostbackEvent(ctrlName, args);

            if (!IsPostBack)
            {
                BindControls();
                DisplayPanels();

                if (Request.QueryString["ApplicantId"] != null && Request.QueryString["ApplicantId"] != "")
                {
                    PopulateEntity(DataUtils.GetInt(Request.QueryString["ApplicantId"]), DataUtils.GetInt(Request.QueryString["Role"]));
                    cbAddnotes.Checked = true;
                }
                CheckW9Access();
                CheckTear1Access();
                CheckNewEntityAccess();
            }
        }

        private void CheckTear1Access()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            cbTear1.Enabled = false;
            cbFileHold.Enabled = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "30926")
                {
                    cbTear1.Enabled = true;
                    cbFileHold.Enabled = true;
                }
            }
        }

        private void CheckW9Access()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "27448")
                {
                    ckbW9.Enabled = true;
                    ckbACHActive.Enabled = true;
                }
            }
        }

        private void CheckNewEntityAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "27453")
                    rdBtnAction.Items[0].Enabled = true;
            }
        }

        //private void HandleCustomPostbackEvent(string ctrlName, string args)
        //{

        //    if (ctrlName == txtEntityDDL.UniqueID && args == "OnBlur")
        //    {
        //        EntitySelectionChanged();
        //    }
        //}

        //private void EntitySelectionChanged()
        //{
        //    try
        //    {
        //        if (txtEntityDDL.Text != "")
        //        {
        //            BindEntityMilestoneGrid();
        //        }
        //        else
        //        {

        //            BindEntityMilestoneGrid();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(Pagename, "EntitySelectionChanged", "", ex.Message);
        //    }
        //}

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
            //if (ProjectNotesData.IsNotesExist(PageId))
            //    btnProjectNotes1.ImageUrl = "~/Images/currentpagenotes.png";

            ifProjectNotes.Src = "EntityNotes.aspx?EntityId=" + ddlEntityName.SelectedValue.ToString() +
                "&PageId=" + PageId;
        }

        private void DisplayPanels()
        {
            if (rdBtnAction.SelectedValue.ToLower() == "existing")
            {
                if (ddlEntityRole.SelectedIndex != 0)
                {
                    ae_txtApplicantName.ContextKey = ddlEntityRole.SelectedItem.ToString();
                    string SelectedRole = ddlEntityRole.SelectedValue.ToString();

                    dvExistingEntities.Visible = true;
                    BindApplicants(DataUtils.GetInt(SelectedRole), ddlEntityRole.SelectedItem.ToString(), ddlEntityName);
                }
                else
                {
                    dvExistingEntities.Visible = false;
                }

                dvCommonForm.Visible = false;
                dvIndividual.Visible = false;
                dvFarm.Visible = false;
                dvNewAddress.Visible = false;
                dvNewEntirySubmit.Visible = false;
                dvNewAttribute.Visible = false;
                dvNewProduct.Visible = false;
                dvAttachEntities.Visible = false;
                //dvNewProjectEvent.Visible = false;
                dvNewMilestone.Visible = false;
                dvAttachedProjects.Visible = false;
            }
            else
            {
                if (ddlEntityRole.SelectedIndex == 0)
                {
                    dvCommonForm.Visible = false;
                    dvIndividual.Visible = false;
                    dvFarm.Visible = false;
                    dvNewAddress.Visible = false;
                    dvExistingEntities.Visible = false;
                    dvNewEntirySubmit.Visible = false;
                    dvNewAttribute.Visible = false;
                    dvNewProduct.Visible = false;
                    dvAttachEntities.Visible = false;
                    //dvNewProjectEvent.Visible = false;
                    dvNewMilestone.Visible = false;
                    dvAttachedProjects.Visible = false;
                }
                else
                {
                    ae_txtApplicantName.ContextKey = ddlEntityRole.SelectedItem.ToString();
                    DisplayPanelsBasedOnEntityRole();
                }
            }
        }

        private void DisplayPanelsBasedOnEntityRole()
        {
            string SelectedRole = ddlEntityRole.SelectedItem.ToString();
            dvCommonForm.Visible = true;
            dvNewEntirySubmit.Visible = true;
            btnEntitySubmit.Text = "Submit";

            if (SelectedRole.ToLower() == "individual")
            {
                foreach (ListItem item in ddlEntityType.Items)
                {
                    if (item.Text.ToString() == "Individual")
                    {
                        ddlEntityType.ClearSelection();
                        item.Selected = true;
                    }
                }
                CommonFormHeader.InnerText = "Entity";
                dvIndividual.Visible = true;
                dvFarm.Visible = false;
                dvNewAddress.Visible = false;
                dvNewAttribute.Visible = false;
                dvAttachEntities.Visible = false;
                //dvNewProjectEvent.Visible = false;
                dvNewMilestone.Visible = false;
                ddlDefaultRole.Visible = false;
                spnDefaultRole.Visible = false;
                dvAttachedProjects.Visible = false;
                //cbMilestoneActive.Visible = false;
            }
            else if (SelectedRole.ToLower() == "organization")
            {
                CommonFormHeader.InnerText = "Entity Organization";
                dvIndividual.Visible = false;
                dvFarm.Visible = false;
                dvNewAddress.Visible = false;
                dvNewAttribute.Visible = false;
                dvAttachEntities.Visible = false;
                //dvNewProjectEvent.Visible = false;
                dvNewMilestone.Visible = false;
                ddlDefaultRole.Visible = true;
                spnDefaultRole.Visible = true;
                dvAttachedProjects.Visible = false;
                //cbMilestoneActive.Visible = true;
            }
            else if (SelectedRole.ToLower() == "farm")
            {
                CommonFormHeader.InnerText = "Entity";
                dvIndividual.Visible = false;
                dvFarm.Visible = true;
                dvNewAddress.Visible = false;
                dvNewAttribute.Visible = false;
                dvAttachEntities.Visible = false;
                ddlDefaultRole.Visible = true;
                spnDefaultRole.Visible = true;
                //cbMilestoneActive.Visible = true;
                dvAttachedProjects.Visible = false;
            }
            else
            {
                CommonFormHeader.InnerText = "Entity";
                dvCommonForm.Visible = false;
                dvIndividual.Visible = false;
                dvFarm.Visible = false;
                dvNewAddress.Visible = false;
                dvNewAttribute.Visible = false;
                dvAttachEntities.Visible = false;
                //dvNewProjectEvent.Visible = false;
                dvNewMilestone.Visible = false;
                dvAttachedProjects.Visible = false;
            }
        }

        private void BindControls()
        {
            BindLookUP(ddlEntityRole, 170);
            BindLookUP(ddlEntityType, 103);
            BindLookUP(ddlPosition, 56);
            BindLookUP(ddlFarmType, 106);
            BindLookUP(ddlAddressType, 1);
            BindLookUP(ddlAttribute, 169);
            BindLookUP(ddlProduct, 12);
            BindLookUP(ddlDefaultRole, 56);
            BindLookUP(ddlEntityRole1, 170);
            //Project Events
            //BindLookUP(ddlEventProgram, 34);
            //BindProjects(ddlEventProject);
            //BindLookUP(ddlEventSubCategory, 163);
            // BindApplicantsForCurrentProject(ddlEntityName);
            //BindPrjectEventGrid();

            BindLookUP(ddlEntityMilestone, 206);

            //EventProgramSelection();
            BindTown();
            BindStates();
            BindLookUP(ddlFYEnd, 172);
        }

        private void BindStates()
        {
            try
            {
                ddlState.Items.Clear();
                ddlState.DataSource = ProjectMaintenanceData.GetStates();
                ddlState.DataValueField = "Abbrev";
                ddlState.DataTextField = "Abbrev";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindStates", "Control ID:" + ddlState.ID, ex.Message);
            }
        }

        private void BindTown()
        {
            try
            {
                ddlTown.Items.Clear();
                ddlTown.DataSource = ProjectMaintenanceData.GetTowns();
                ddlTown.DataValueField = "Town";
                ddlTown.DataTextField = "Town";
                ddlTown.DataBind();
                ddlTown.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindTown", "Control ID:" + ddlTown.ID, ex.Message);
            }
        }


        protected void BindProjects(DropDownList ddList)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = ProjectCheckRequestData.GetData("getprojectslist"); ;
                ddList.DataValueField = "projectid";
                ddList.DataTextField = "Proj_num";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
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

        protected void rdBtnAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonSelectionChanged();
        }

        private void RadioButtonSelectionChanged()
        {
            ddlEntityRole.SelectedIndex = -1;
            dvCommonForm.Visible = false;
            dvIndividual.Visible = false;
            dvFarm.Visible = false;
            dvNewAddress.Visible = false;
            dvNewAttribute.Visible = false;
            dvNewProduct.Visible = false;
            dvExistingEntities.Visible = false;
            dvNewEntirySubmit.Visible = false;
            dvAttachEntities.Visible = false;
            //dvNewProjectEvent.Visible = false;
            dvNewMilestone.Visible = false;

            ClearForm();
            hfApplicatId.Value = "";

            if (hfIsCreated.Value != "true" && rdBtnAction.SelectedValue.ToLower() == "existing" && Request.QueryString["IsSearch"] == null)
            {
                cbActiveOnly.Visible = true;
                Response.Redirect("EntitySearch.aspx", true);
            }
            if (rdBtnAction.SelectedValue.ToLower() == "new" && Request.QueryString["IsSearch"] != null)
            {
                Response.Redirect("EntityMaintenance.aspx", true);
            }
            hfIsCreated.Value = "fasle";
        }

        protected void ddlEntityRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();
            DisplayPanels();
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

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            if (!IsAddressValid()) return;

            int ApplicantId = Convert.ToInt32(hfApplicatId.Value);

            string town = "", county = "", village = "";

            if (ddlState.SelectedValue == "VT")
            {
                town = ddlTown.SelectedValue;
                county = ddlCounty.SelectedValue;
                village = ddlVillage.SelectedValue;
            }
            else
            {
                town = txtTown.Text;
                county = txtCounty.Text;
            }

            if (btnAddAddress.Text.ToLower() == "add")
            {
                EntityMaintResult objEntityMaintResult = EntityMaintenanceData.AddNewEntityAddress(ApplicantId,
                    txtStreetNo.Text, txtAddress1.Text, txtAddress2.Text,
                    town, village, ddlState.SelectedValue, txtZip.Text,
                    county, int.Parse(ddlAddressType.SelectedValue.ToString()),
                    DataUtils.GetDecimal(txtLattitude.Text), DataUtils.GetDecimal(txtLongitude.Text),
                    cbDefaultAddress.Checked);

                btnAddAddress.Text = "Add";

                if (objEntityMaintResult.IsDuplicate && !objEntityMaintResult.IsActive)
                    LogMessage("Address already exist as in-active");
                else if (objEntityMaintResult.IsDuplicate)
                    LogMessage("Address already exist");
                else
                    LogMessage("New Address added successfully");
            }
            else
            {
                int AddressId = Convert.ToInt32(hfAddressId.Value);

                EntityMaintenanceData.UpdateEntityAddress(ApplicantId, AddressId, int.Parse(ddlAddressType.SelectedValue.ToString()),
                    txtStreetNo.Text, txtAddress1.Text, txtAddress2.Text, town, village,
                    ddlState.SelectedValue, txtZip.Text, county, cbActive.Checked,
                    DataUtils.GetDecimal(txtLattitude.Text), DataUtils.GetDecimal(txtLongitude.Text), cbDefaultAddress.Checked);

                hfAddressId.Value = "";
                btnAddAddress.Text = "Add";
                LogMessage("Address Updated Successfully");
            }
            gvAddress.EditIndex = -1;
            BindAddressGrid();
            ClearAddressForm();
            dvAddressGrid.Visible = true;
            cbAddAddress.Checked = false;
        }

        private void BindGrids()
        {
            BindAddressGrid();
            BindAttributeGrid();
            BindProductGrid();
            BindAttachEntitiesGrid();
            //BindPrjectEventGrid();
            BindEntityMilestoneGrid();
            BindAttachedProjects();
            BindEntityNotesGrid();
        }

        private void BindAddressGrid()
        {
            try
            {
                DataTable dtAddress = EntityMaintenanceData.GetEntityAddressDetailsList(DataUtils.GetInt(hfApplicatId.Value), cbActiveOnly.Checked);

                if (dtAddress.Rows.Count > 0)
                {
                    dvAddressGrid.Visible = true;
                    gvAddress.DataSource = dtAddress;
                    gvAddress.DataBind();
                }
                else
                {
                    dvAddressGrid.Visible = false;
                    gvAddress.DataSource = null;
                    gvAddress.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAddressGrid", "", ex.Message);
            }
        }

        private void ClearAddressForm()
        {
            txtStreetNo.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtTown.Text = "";
            ddlTown.SelectedIndex = -1;

            ddlCounty.Items.Clear();
            ddlVillage.Items.Clear();
            ddlCounty.SelectedIndex = -1;
            txtCounty.Text = "";
            ddlState.SelectedIndex = -1;
            //txtState.Text = "";
            txtZip.Text = "";
            txtCounty.Text = "";
            ddlVillage.SelectedIndex = -1;
            ddlAddressType.SelectedIndex = -1;
            txtLattitude.Text = "";
            txtLongitude.Text = "";
            cbActive.Checked = true;
            cbActive.Enabled = true;
            cbDefaultAddress.Checked = true;
            cbDefaultAddress.Enabled = true;
            ddlState.SelectedIndex = -1;
            dvAddress.Visible = false;
        }

        protected bool IsAddressValid()
        {
            if (ddlAddressType.Items.Count > 1 && ddlAddressType.SelectedIndex == 0)
            {
                LogMessage("Select Address Type");
                ddlAddressType.Focus();
                return false;
            }
            if (cbReqStreetNo.Checked && txtStreetNo.Text.Trim() == "")
            {
                LogMessage("Enter Street#");
                txtStreetNo.Focus();
                return false;
            }
            if (txtAddress1.Text.Trim() == "")
            {
                LogMessage("Enter Address1");
                txtAddress1.Focus();
                return false;
            }
            if (txtZip.Text.Trim() == "")
            {
                LogMessage("Enter Zip");
                txtZip.Focus();
                return false;
            }

            if (ddlState.SelectedIndex == 0)
            {
                LogMessage("Enter State");
                ddlState.Focus();
                return false;
            }
            else if (ddlState.SelectedValue == "VT")
            {
                if (ddlTown.SelectedIndex == 0)
                {
                    LogMessage("Enter Town");
                    ddlTown.Focus();
                    return false;
                }
            }
            else
            {
                if (txtTown.Text.Trim() == "")
                {
                    LogMessage("Enter Town");
                    txtTown.Focus();
                    return false;
                }
            }
            return true;
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetAddress1(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt = ProjectMaintenanceData.GetAddressDetails(prefixText);

            //List<string> ProjNames = new List<string>();
            List<string> items = new List<string>(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["Street#"].ToString()
                    + ' ' + dt.Rows[i]["Address1"].ToString() + ' ' + dt.Rows[i]["Town"].ToString(),
                    dt.Rows[i]["Street#"].ToString()
                    + '~' + dt.Rows[i]["Address1"].ToString()
                    + '~' + dt.Rows[i]["Address2"].ToString()
                    + '~' + dt.Rows[i]["State"].ToString()
                    + '~' + dt.Rows[i]["Zip"].ToString()
                    + '~' + dt.Rows[i]["Town"].ToString()
                    + '~' + dt.Rows[i]["County"].ToString()
                    + '~' + dt.Rows[i]["latitude"].ToString()
                    + '~' + dt.Rows[i]["longitude"].ToString()
                    + '~' + dt.Rows[i]["Village"].ToString()
                    );
                items.Add(str);
                //ProjNames.Add(dt.Rows[i][0].ToString());
            }
            //return ProjNames.ToArray();
            return items.ToArray();
        }

        protected void btnEntitySubmit_Click(object sender, EventArgs e)
        {
            if (IsEntityFormValid())
            {
                hfIsCreated.Value = "true";

                if (txtUEI.Text.Length > 0 && txtUEI.Text.Length != 12)
                {
                    LogMessage("Invalid UEI field.");
                }
                else
                {
                    if (btnEntitySubmit.Text == "Submit")
                    {
                        string HomePhoneNumber = new string(txtHomePhone.Text.Where(c => char.IsDigit(c)).ToArray());
                        string WorkPhoneNumber = new string(txtWorkPhone.Text.Where(c => char.IsDigit(c)).ToArray());
                        string CellPhoneNumber = new string(txtCellPhone.Text.Where(c => char.IsDigit(c)).ToArray());

                        if (ddlEntityRole.SelectedItem.ToString().ToLower() == "individual")
                        {
                            EntityMaintResult objEntityMaintResult = EntityMaintenanceData.AddNewEntity(DataUtils.GetInt(ddlEntityType.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()), DataUtils.GetInt(ddlFYEnd.SelectedValue.ToString()), txtWebsite.Text,
                                txtEmail.Text, HomePhoneNumber, WorkPhoneNumber, CellPhoneNumber, txtStateVendorId.Text, txtApplicantName.Text, txtFirstName.Text, txtLastName.Text, DataUtils.GetInt(ddlPosition.SelectedValue.ToString()),
                                txtTitle.Text, null, 0, 0, 0,
                                0, 0, 0, false, null, null,
                                0, null, 1, ckbW9.Checked, cbTear1.Checked, cbFileHold.Checked, null, null, txtLegalName.Text, txtUEI.Text, ckbACHActive.Checked, txtACHContact.Text, txtACHEmail.Text); //1=Individual

                            if (objEntityMaintResult.IsDuplicate)
                            {
                                if (objEntityMaintResult.DuplicateId == 1)
                                    LogMessage("Entity with same Email already exist");
                                else if (objEntityMaintResult.DuplicateId == 2)
                                    LogMessage("Entity with same First Name and Last Name already exist");
                                else if (objEntityMaintResult.DuplicateId == 3)
                                    LogMessage("Entity with same First Name, Last Name and Email already exist");
                            }
                            else
                            {
                                ClearForm();
                                PopulateEntity(objEntityMaintResult.ApplicantId, DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()));
                                LogMessage("New Entity Added Successfully");
                            }
                        }
                        else if (ddlEntityRole.SelectedItem.ToString().ToLower() == "organization")
                        {
                            EntityMaintResult objEntityMaintResult = EntityMaintenanceData.AddNewEntity(DataUtils.GetInt(ddlEntityType.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()), DataUtils.GetInt(ddlFYEnd.SelectedValue.ToString()),
                                txtWebsite.Text, txtEmail.Text, HomePhoneNumber, WorkPhoneNumber, CellPhoneNumber, txtStateVendorId.Text, txtApplicantName.Text, null, null, 0,
                               null, null, 0, 0, 0,
                               0, 0, 0, false, null, null,
                               0, DataUtils.GetInt(ddlDefaultRole.SelectedValue.ToString()), 2, ckbW9.Checked, cbTear1.Checked, cbFileHold.Checked,
                               txtEIN.Text, txtDUNS.Text, txtLegalName.Text, txtUEI.Text, ckbACHActive.Checked, txtACHContact.Text, txtACHEmail.Text); //2=Organization
                            ClearForm();
                            PopulateEntity(objEntityMaintResult.ApplicantId, DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()));
                            LogMessage("New Entity Added Successfully");
                        }
                        else if (ddlEntityRole.SelectedItem.ToString().ToLower() == "farm")
                        {
                            EntityMaintResult objEntityMaintResult = EntityMaintenanceData.AddNewEntity(DataUtils.GetInt(ddlEntityType.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()), DataUtils.GetInt(ddlFYEnd.SelectedValue.ToString()), txtWebsite.Text,
                               null, HomePhoneNumber, WorkPhoneNumber, CellPhoneNumber, txtStateVendorId.Text, txtApplicantName.Text, null, null, 0,
                               null, txtFarmName.Text, DataUtils.GetInt(ddlFarmType.SelectedValue.ToString()), DataUtils.GetInt(txtAcresInProduction.Text), DataUtils.GetInt(txtAcresOwned.Text),
                               DataUtils.GetInt(txtAcresLeased.Text), DataUtils.GetInt(txtAcresLeasedOut.Text), DataUtils.GetInt(txtTotalAcres.Text), cbIsNoLongerBusiness.Checked, txtNotes.Text, txtAgrEdu.Text,
                               DataUtils.GetInt(txtYearsManagingForm.Text), DataUtils.GetInt(ddlDefaultRole.SelectedValue.ToString()), 3, ckbW9.Checked, cbTear1.Checked, cbFileHold.Checked, null, null, txtLegalName.Text, txtUEI.Text, ckbACHActive.Checked, txtACHContact.Text, txtACHEmail.Text); //3=Farm
                            ClearForm();
                            PopulateEntity(objEntityMaintResult.ApplicantId, DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()));
                            LogMessage("New Entity Added Successfully");
                        }
                    }
                    else
                    {
                        int Operation = 0;

                        if (ddlEntityRole.SelectedItem.Text.ToLower() == "individual")
                            Operation = 1;
                        else if (ddlEntityRole.SelectedItem.Text.ToLower() == "organization")
                            Operation = 2;
                        else if (ddlEntityRole.SelectedItem.Text.ToLower() == "farm")
                            Operation = 3;

                        string HomePhoneNumber = new string(txtHomePhone.Text.Where(c => char.IsDigit(c)).ToArray());
                        string WorkPhoneNumber = new string(txtWorkPhone.Text.Where(c => char.IsDigit(c)).ToArray());
                        string CellPhoneNumber = new string(txtCellPhone.Text.Where(c => char.IsDigit(c)).ToArray());

                        EntityMaintenanceData.UpdateEntity(DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityType.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()), DataUtils.GetInt(ddlFYEnd.SelectedValue.ToString()), txtWebsite.Text,
                               txtEmail.Text, HomePhoneNumber, WorkPhoneNumber, CellPhoneNumber, txtStateVendorId.Text, txtApplicantName.Text, txtFirstName.Text, txtLastName.Text, DataUtils.GetInt(ddlPosition.SelectedValue.ToString()),
                               txtTitle.Text, txtFarmName.Text, DataUtils.GetInt(ddlFarmType.SelectedValue.ToString()), DataUtils.GetInt(txtAcresInProduction.Text), DataUtils.GetInt(txtAcresOwned.Text),
                               DataUtils.GetInt(txtAcresLeased.Text), DataUtils.GetInt(txtAcresLeasedOut.Text), DataUtils.GetInt(txtTotalAcres.Text), cbIsNoLongerBusiness.Checked, txtNotes.Text, txtAgrEdu.Text,
                               DataUtils.GetInt(txtYearsManagingForm.Text), DataUtils.GetInt(ddlDefaultRole.SelectedValue.ToString()), Operation, ckbW9.Checked, cbTear1.Checked, cbFileHold.Checked, cbMilestoneActive.Checked,
                               txtEIN.Text, txtDUNS.Text, txtLegalName.Text, txtUEI.Text, ckbACHActive.Checked, txtACHContact.Text, txtACHEmail.Text);
                        ClearForm();
                        PopulateEntity(DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()));
                        LogMessage("Entity Updated Successfully");
                    }
                }
            }
        }

        private void PopulateEntity(int ApplicantId, int EntityRole)
        {
            rdBtnAction.SelectedIndex = 1;
            RadioButtonSelectionChanged();
            dvExistingEntities.Visible = true;

            PopulateDropDown(ddlEntityRole, EntityRole.ToString());

            BindApplicants(DataUtils.GetInt(ddlEntityRole.SelectedValue.ToString()), ddlEntityRole.SelectedItem.ToString(), ddlEntityName);
            PopulateDropDown(ddlEntityName, ApplicantId.ToString());
            EntityNameChanged();
        }

        protected void ddlEntityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityNameChanged();
        }

        private void EntityNameChanged()
        {
            ClearForm();
            ClearAddressForm();
            //EntityName.InnerHtml = "";
            btnProjectNotes1.Visible = false;
            if (ddlEntityName.SelectedIndex != 0)
            {
                DisplayPanelsBasedOnEntityRole();
                //EntityName.InnerHtml = ddlEntityName.SelectedItem.ToString();
                btnProjectNotes1.Visible = true;
                ProjectNotesSetUp();

                hfApplicatId.Value = ddlEntityName.SelectedValue.ToString();

                DataRow drEntityData = EntityMaintenanceData.GetEntityData(DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()));
                if (drEntityData != null)
                {
                    PopulateForm(drEntityData);
                    dvNewAddress.Visible = true;
                    //dvNewProjectEvent.Visible = true;
                    dvNewMilestone.Visible = true;
                    dvAttachedProjects.Visible = true;

                    if (ddlEntityRole.SelectedItem.ToString().ToLower() == "farm")
                    {
                        dvNewAttribute.Visible = true;
                        dvNewProduct.Visible = true;
                        dvAttachEntities.Visible = true;
                        //BindApplicants(DataUtils.GetInt(ddlEntityRole.SelectedItem.Value), ddlEntityRole.SelectedItem.ToString(), ddlIndividualApplicant);
                    }
                    else
                    {
                        if (ddlEntityRole.SelectedItem.ToString().ToLower() == "organization")
                        {
                            dvAttachEntities.Visible = true;
                            //BindApplicants(DataUtils.GetInt(ddlEntityRole.SelectedItem.Value), ddlEntityRole.SelectedItem.ToString(), ddlIndividualApplicant);
                            //BindApplicants(26243, "individual", ddlIndividualApplicant);
                        }
                        else
                        {
                            dvAttachEntities.Visible = true;
                        }

                        dvNewAttribute.Visible = false;
                        dvNewProduct.Visible = false;
                    }

                    BindGrids();
                    btnEntitySubmit.Text = "Update";
                }
            }
            else
            {
                dvCommonForm.Visible = false;
                dvIndividual.Visible = false;
                dvFarm.Visible = false;
                dvNewAddress.Visible = false;
                dvNewEntirySubmit.Visible = false;
                dvNewAttribute.Visible = false;
                dvNewProduct.Visible = false;
                //dvNewProjectEvent.Visible = false;
                dvNewMilestone.Visible = false;
                dvAttachedProjects.Visible = false;
                cbAddAddress.Checked = false;
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

        private void PopulateForm(DataRow drEntityData)
        {
            hfFarmId.Value = drEntityData["FarmId"].ToString();
            PopulateDropDown(ddlEntityType, drEntityData["LkEntityType"].ToString());
            PopulateDropDown(ddlFYEnd, drEntityData["FYend"].ToString());
            txtWebsite.Text = drEntityData["website"].ToString();
            txtDUNS.Text = drEntityData["DUNS"].ToString();
            txtEIN.Text = drEntityData["EIN"].ToString();
            txtLegalName.Text = drEntityData["LegalName"].ToString();
            txtUEI.Text = drEntityData["UEI"].ToString();

            if (drEntityData["WorkPhone"].ToString().Trim() == "")
                txtWorkPhone.Text = "";
            else
                txtWorkPhone.Text = String.Format("{0:(###)###-####}", double.Parse(drEntityData["WorkPhone"].ToString()));

            if (drEntityData["CellPhone"].ToString().Trim() == "")
                txtCellPhone.Text = "";
            else
                txtCellPhone.Text = String.Format("{0:(###)###-####}", double.Parse(drEntityData["CellPhone"].ToString()));

            if (drEntityData["HomePhone"].ToString().Trim() == "")
                txtHomePhone.Text = "";
            else
                txtHomePhone.Text = String.Format("{0:(###)###-####}", double.Parse(drEntityData["HomePhone"].ToString()));

            spnAcctNumber.InnerHtml = drEntityData["AppNameID"].ToString();
            txtStateVendorId.Text = drEntityData["Stvendid"].ToString();
            txtApplicantName.Text = drEntityData["Applicantname"].ToString();
            txtFirstName.Text = drEntityData["Firstname"].ToString();
            txtLastName.Text = drEntityData["Lastname"].ToString();
            PopulateDropDown(ddlPosition, drEntityData["LkPosition"].ToString());
            txtTitle.Text = drEntityData["Title"].ToString();
            txtEmail.Text = drEntityData["email"].ToString();
            txtFarmName.Text = drEntityData["FarmName"].ToString();
            PopulateDropDown(ddlFarmType, drEntityData["LkFVEnterpriseType"].ToString());
            txtAcresInProduction.Text = drEntityData["AcresInProduction"].ToString(); ;
            txtAcresOwned.Text = drEntityData["AcresOwned"].ToString(); ;
            txtAcresLeased.Text = drEntityData["AcresLeased"].ToString(); ;
            txtAcresLeasedOut.Text = drEntityData["AcresLeasedOut"].ToString(); ;
            txtTotalAcres.Text = drEntityData["TotalAcres"].ToString(); ;
            cbIsNoLongerBusiness.Checked = DataUtils.GetBool(drEntityData["OutOFBiz"].ToString());
            txtNotes.Text = drEntityData["Notes"].ToString();
            txtAgrEdu.Text = drEntityData["AgEd"].ToString();
            txtYearsManagingForm.Text = drEntityData["YearsManagingFarm"].ToString();
            PopulateDropDown(ddlDefaultRole, drEntityData["AppRole"].ToString());
            ckbW9.Checked = DataUtils.GetBool(drEntityData["w9"].ToString());
            cbTear1.Checked = DataUtils.GetBool(drEntityData["Tier1"].ToString());
            cbFileHold.Checked = DataUtils.GetBool(drEntityData["FileHold"].ToString());
            ckbACHActive.Checked= DataUtils.GetBool(drEntityData["ACHActive"].ToString());

            if (ckbACHActive.Checked)
            {
                txtACHContact.Enabled = true;
                txtACHEmail.Enabled = true;
                txtACHContact.Text = drEntityData["ACHContact"].ToString();
                txtACHEmail.Text = drEntityData["ACHEmail"].ToString();
            }
            else
            {
                txtACHContact.Enabled = false;
                txtACHEmail.Enabled = false;
                txtACHContact.Text = "";
                txtACHEmail.Text = "";
            }

            if (drEntityData["LKApplicantRole"].ToString() == "358")//Primary Applicant
                cbMilestoneActive.Enabled = false;
            else
                cbMilestoneActive.Enabled = true;

            cbMilestoneActive.Checked = DataUtils.GetBool(drEntityData["RowIsActive"].ToString()); ;
        }

        private void ClearForm()
        {
            ddlEntityType.SelectedIndex = -1;
            spnAcctNumber.InnerHtml = "";
            ddlFYEnd.SelectedIndex = -1;
            txtWebsite.Text = "";
            txtHomePhone.Text = "";
            txtWorkPhone.Text = "";
            txtCellPhone.Text = "";
            txtStateVendorId.Text = "";
            txtApplicantName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            ddlPosition.SelectedIndex = -1;
            txtTitle.Text = "";
            txtEmail.Text = "";
            txtFarmName.Text = "";
            ddlFarmType.SelectedIndex = -1;
            txtAcresInProduction.Text = "";
            txtAcresOwned.Text = "";
            txtAcresLeased.Text = "";
            txtAcresLeasedOut.Text = "";
            txtTotalAcres.Text = "";
            cbIsNoLongerBusiness.Checked = false;
            txtNotes.Text = "";
            txtAgrEdu.Text = "";
            txtYearsManagingForm.Text = "";
            ddlDefaultRole.SelectedIndex = -1;
            txtEIN.Text = "";
            txtDUNS.Text = "";
            txtLegalName.Text = "";
            txtUEI.Text = "";
        }

        protected void gvAddress_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cbAddAddress.Checked = false;
            ClearAddressForm();
            btnAddAddress.Text = "Add";
            gvAddress.EditIndex = -1;
            BindAddressGrid();
        }

        protected void gvAddress_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);
                    btnAddAddress.Text = "Update";
                    cbAddAddress.Checked = true;

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[10].Controls[0].Visible = false;
                        Label lblAddressId = e.Row.FindControl("lblAddressId") as Label;
                        DataRow dr = EntityMaintenanceData.GetEntityAddressDetailsById(DataUtils.GetInt(hfApplicatId.Value), Convert.ToInt32(lblAddressId.Text));

                        hfAddressId.Value = lblAddressId.Text;

                        PopulateDropDown(ddlAddressType, dr["LkAddressType"].ToString());
                        txtStreetNo.Text = dr["Street#"].ToString();
                        txtAddress1.Text = dr["Address1"].ToString();
                        txtAddress2.Text = dr["Address2"].ToString();

                        if (dr["State"].ToString() == "VT")
                        {
                            BindCounty(dr["Town"].ToString());
                            BindVillages(dr["Town"].ToString());

                            PopulateDropDown(ddlTown, dr["Town"].ToString());
                            ddlTown.Visible = true;
                            txtTown.Visible = false;

                            PopulateDropDown(ddlCounty, dr["County"].ToString());
                            ddlCounty.Visible = true;
                            txtCounty.Visible = false;

                            PopulateDropDown(ddlVillage, dr["Village"].ToString());
                            ddlVillage.Visible = true;
                            spnVillage.Visible = true;
                        }
                        else
                        {
                            txtTown.Text = dr["Town"].ToString();
                            ddlTown.Visible = false;
                            txtTown.Visible = true;

                            txtCounty.Text = dr["Town"].ToString();
                            ddlCounty.Visible = false;
                            txtCounty.Visible = true;

                            ddlVillage.Visible = false;
                            spnVillage.Visible = false;
                        }
                        //txtState.Text = dr["State"].ToString();
                        txtZip.Text = dr["Zip"].ToString();
                        txtCounty.Text = dr["County"].ToString();
                        txtLattitude.Text = dr["latitude"].ToString();
                        txtLongitude.Text = dr["longitude"].ToString();
                        PopulateDropDown(ddlState, dr["State"].ToString());
                        dvAddress.Visible = true;

                        cbActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        cbDefaultAddress.Checked = DataUtils.GetBool(dr["DefAddress"].ToString());

                        if (cbDefaultAddress.Checked)
                        {
                            cbDefaultAddress.Enabled = false;
                            cbActive.Enabled = false;
                        }
                        else
                        {
                            cbDefaultAddress.Enabled = true;
                            cbActive.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvAddress_RowDataBound", "", ex.Message);
            }
        }

        protected void gvAddress_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAddress.EditIndex = e.NewEditIndex;
            BindAddressGrid();
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrids();
        }

        protected bool IsEntityFormValid()
        {
            if (ddlEntityType.Items.Count > 1 && ddlEntityType.SelectedIndex == 0)
            {
                LogMessage("Select Legal Structure");
                ddlEntityType.Focus();
                return false;
            }

            if (ddlEntityRole.SelectedItem.ToString().ToLower() == "individual")
            {
                if (txtFirstName.Text.Trim() == "")
                {
                    LogMessage("Enter First Name");
                    txtFirstName.Focus();
                    return false;
                }
                if (txtLastName.Text.Trim() == "")
                {
                    LogMessage("Enter Last Name");
                    txtLastName.Focus();
                    return false;
                }
            }
            else if (ddlEntityRole.SelectedItem.ToString().ToLower() == "organization")
            {
                if (txtApplicantName.Text.Trim() == "")
                {
                    LogMessage("Enter Applicant Name");
                    txtApplicantName.Focus();
                    return false;
                }
            }
            else if (ddlEntityRole.SelectedItem.ToString().ToLower() == "farm")
            {
                if (ddlFarmType.Items.Count > 1 && ddlFarmType.SelectedIndex == 0)
                {
                    LogMessage("Select Farm Type");
                    ddlFarmType.Focus();
                    return false;
                }
                if (txtFarmName.Text.Trim() == "")
                {
                    LogMessage("Enter Farm Name");
                    txtFarmName.Focus();
                    return false;
                }
            }

            return true;
        }

        protected void AddAttribute_Click(object sender, EventArgs e)
        {
            if (ddlAttribute.SelectedIndex == 0)
            {
                LogMessage("Select Attribute");
                ddlAttribute.Focus();
                return;
            }

            FormAttributeResult obAttributeResult = EntityMaintenanceData.AddFarmAttribute(DataUtils.GetInt(hfFarmId.Value),
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
                DataTable dt = EntityMaintenanceData.GetFarmAttributesList(DataUtils.GetInt(hfFarmId.Value), cbActiveOnly.Checked);

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

            int FarmAttributeID = DataUtils.GetInt(((Label)gvAttribute.Rows[rowIndex].FindControl("lblFarmAttributeID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAttribute.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            EntityMaintenanceData.UpdateFarmAttribute(FarmAttributeID, RowIsActive);
            gvAttribute.EditIndex = -1;

            BindAttributeGrid();

            LogMessage("Attribute updated successfully");
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedIndex == 0)
            {
                LogMessage("Select Product");
                ddlProduct.Focus();
                return;
            }

            if (txtStartDate.Text.Trim() == "")
            {
                LogMessage("Enter Start Date");
                txtStartDate.Focus();
                return;
            }
            else
            {
                if (!DataUtils.IsDateTime(txtStartDate.Text.Trim()))
                {
                    LogMessage("Enter Valid Start Date");
                    txtStartDate.Focus();
                    return;
                }
            }

            FormAttributeResult obAttributeResult = EntityMaintenanceData.AddFarmProducts(DataUtils.GetInt(hfFarmId.Value),
                DataUtils.GetInt(ddlProduct.SelectedValue.ToString()), DataUtils.GetDate(txtStartDate.Text));
            ddlProduct.SelectedIndex = -1;
            txtStartDate.Text = "";
            cbAddProduct.Checked = false;

            BindProductGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Product already exist as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Product already exist");
            else
                LogMessage("New Product added successfully");
        }

        protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProduct.EditIndex = e.NewEditIndex;
            BindProductGrid();
        }

        protected void gvProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProduct.EditIndex = -1;
            BindProductGrid();
        }

        protected void gvProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int FarmProductsID = DataUtils.GetInt(((Label)gvProduct.Rows[rowIndex].FindControl("lblFarmProductsID")).Text);
            DateTime StartDate = Convert.ToDateTime(((TextBox)gvProduct.Rows[rowIndex].FindControl("txtStartDate")).Text);
            //int LkDisp = DataUtils.GetInt(((DropDownList)gvMajor.Rows[rowIndex].FindControl("ddlMjrDispositionE")).SelectedValue.ToString());
            //DateTime DispDate = Convert.ToDateTime(((TextBox)gvMajor.Rows[rowIndex].FindControl("txtDispDate")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvProduct.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            EntityMaintenanceData.UpdateFarmProducts(FarmProductsID, RowIsActive, StartDate);

            gvProduct.EditIndex = -1;

            BindProductGrid();

            LogMessage("Product Updated successfully");
        }

        private void BindProductGrid()
        {
            try
            {
                DataTable dt = EntityMaintenanceData.GetFarmProductsList(DataUtils.GetInt(hfFarmId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvProductGrid.Visible = true;
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                }
                else
                {
                    dvProductGrid.Visible = false;
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindProductGrid", "", ex.Message);
            }
        }

        protected void btnAttachEntities_Click(object sender, EventArgs e)
        {
            if (ddlEntityName1.SelectedIndex == 0)
            {
                LogMessage("Select Applicant");
                ddlEntityName1.Focus();
                return;
            }

            FormAttributeResult obAttributeResult = EntityMaintenanceData.AddApplicantApplicant(DataUtils.GetInt(hfApplicatId.Value),
                DataUtils.GetInt(ddlEntityName1.SelectedValue.ToString()));

            ddlEntityName1.SelectedIndex = -1;
            cbAttachEntities.Checked = false;

            BindAttachEntitiesGrid();

            if (obAttributeResult.IsDuplicate && !obAttributeResult.IsActive)
                LogMessage("Entity already attached as in-active");
            else if (obAttributeResult.IsDuplicate)
                LogMessage("Entity already attached");
            else
                LogMessage("Entity added successfully");
        }

        private void BindAttachEntitiesGrid()
        {
            try
            {
                DataTable dt = EntityMaintenanceData.GetApplicantApplicantList(DataUtils.GetInt(hfApplicatId.Value), cbActiveOnly.Checked);

                if (dt.Rows.Count > 0)
                {
                    dvAttachEntitiesGrid.Visible = true;
                    gvAttachEntities.DataSource = dt;
                    gvAttachEntities.DataBind();
                }
                else
                {
                    dvAttachEntitiesGrid.Visible = false;
                    gvAttachEntities.DataSource = null;
                    gvAttachEntities.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAttachEntitiesGrid", "", ex.Message);
            }
        }

        protected void gvAttachEntities_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAttachEntities.EditIndex = -1;
            BindAttachEntitiesGrid();
        }

        protected void gvAttachEntities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAttachEntities.EditIndex = e.NewEditIndex;
            BindAttachEntitiesGrid();
        }

        protected void gvAttachEntities_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ApplicantApplicantId = DataUtils.GetInt(((Label)gvAttachEntities.Rows[rowIndex].FindControl("lblApplicantApplicantId")).Text);
            //DateTime StartDate = Convert.ToDateTime(((TextBox)gvProduct.Rows[rowIndex].FindControl("txtStartDate")).Text);
            //int LkDisp = DataUtils.GetInt(((DropDownList)gvAttachEntities.Rows[rowIndex].FindControl("ddlMjrDispositionE")).SelectedValue.ToString());
            //DateTime DispDate = Convert.ToDateTime(((TextBox)gvMajor.Rows[rowIndex].FindControl("txtDispDate")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAttachEntities.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            EntityMaintenanceData.UpdateApplicantApplicant(ApplicantApplicantId, RowIsActive);

            gvAttachEntities.EditIndex = -1;

            BindAttachEntitiesGrid();

            LogMessage("Attached Entity Updated successfully");
        }

        //private void BindPrjectEventGrid()
        //{
        //    try
        //    {
        //        DataTable dtProjectEvents = ProjectMaintenanceData.GetEventListByEntity(DataUtils.GetInt(hfApplicatId.Value), cbActiveOnly.Checked);
        //        Session["dtProjectEvents"] = dtProjectEvents;

        //        if (dtProjectEvents.Rows.Count > 0)
        //        {
        //            dvProjectEventGrid.Visible = true;
        //            gvProjectEvent.DataSource = dtProjectEvents;
        //            gvProjectEvent.DataBind();
        //        }
        //        else
        //        {
        //            dvProjectEventGrid.Visible = false;
        //            gvProjectEvent.DataSource = null;
        //            gvProjectEvent.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(Pagename, "BindPrjectEventGrid", "", ex.Message);
        //    }
        //}

        //private bool IsProjectEventFormValid()
        //{
        //    if (ddlEventProgram.Items.Count > 1 && ddlEventProgram.SelectedIndex == 0)
        //    {
        //        LogMessage("Select Event Program");
        //        ddlEventProgram.Focus();
        //        return false;
        //    }


        //    if (ddlEvent.Items.Count > 1 && ddlEvent.SelectedIndex == 0)
        //    {
        //        LogMessage("Select Event");
        //        ddlEvent.Focus();
        //        return false;
        //    }

        //    if (txtEventDate.Text.Trim() == "")
        //    {
        //        LogMessage("Enter Event Date");
        //        txtEventDate.Focus();
        //        return false;
        //    }
        //    else
        //    {
        //        if (!DataUtils.IsDateTime(txtEventDate.Text.Trim()))
        //        {
        //            LogMessage("Enter valid Event Date");
        //            txtEventDate.Focus();
        //            return false;
        //        }
        //    }
        //    return true;
        //}

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

        //protected void ddlEventProject_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    hfProjectId.Value = "";
        //    if (ddlEventProject.SelectedIndex != 0)
        //    {
        //        hfProjectId.Value = ddlEventProject.SelectedValue.ToString();
        //    }

        //    //BindApplicantsForCurrentProject(ddlEntityName);
        //}

        private void BindApplicantsForCurrentProject(DropDownList ddlEventEntity)
        {
            try
            {
                ddlEventEntity.Items.Clear();

                if ((DataUtils.GetInt(hfProjectId.Value) == 0))
                    ddlEventEntity.DataSource = ApplicantData.GetApplicants();
                else
                    ddlEventEntity.DataSource = ProjectMaintenanceData.GetCurrentProjectApplicants(DataUtils.GetInt(hfProjectId.Value));

                ddlEventEntity.DataValueField = "appnameid";
                ddlEventEntity.DataTextField = "applicantname";
                ddlEventEntity.DataBind();
                ddlEventEntity.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindApplicantsForCurrentProject", "", ex.Message);
            }
        }

        //private void ClearProjectEventForm()
        //{
        //    cbAddProjectEvent.Checked = false;

        //    //SetEventProjectandProgram();
        //    //ddlEventEntity.SelectedIndex = -1;
        //    ddlEventProject.SelectedIndex = -1;
        //    ddlEventProgram.SelectedIndex = -1;
        //    ddlEvent.SelectedIndex = -1;
        //    ddlEventSubCategory.SelectedIndex = -1;
        //    txtEventDate.Text = "";
        //    txtEventNotes.Text = "";
        //    //ddlEventProgram.Enabled = true;
        //    //ddlEventProject.Enabled = true;
        //    chkProjectEventActive.Enabled = false;
        //}

        //protected void gvProjectEvent_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvProjectEvent.EditIndex = e.NewEditIndex;
        //    BindPrjectEventGrid();
        //}

        //protected void gvProjectEvent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvProjectEvent.EditIndex = -1;
        //    BindPrjectEventGrid();
        //    ClearProjectEventForm();
        //    hfProjectEventID.Value = "";
        //    btnAddEvent.Text = "Add";
        //    cbAddProjectEvent.Checked = false;
        //}

        //protected void gvProjectEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        //        {
        //            CommonHelper.GridViewSetFocus(e.Row);
        //            btnAddEvent.Text = "Update";
        //            cbAddProjectEvent.Checked = true;

        //            //Checking whether the Row is Data Row
        //            if (e.Row.RowType == DataControlRowType.DataRow)
        //            {
        //                e.Row.Cells[7].Controls[0].Visible = false;

        //                Label lblProjectEventID = e.Row.FindControl("lblProjectEventID") as Label;
        //                DataRow dr = ProjectMaintenanceData.GetProjectEventById(DataUtils.GetInt(lblProjectEventID.Text));

        //                hfProjectEventID.Value = lblProjectEventID.Text;

        //                PopulateDropDown(ddlEventProject, dr["ProjectID"].ToString());
        //                PopulateDropDown(ddlEventProgram, dr["Prog"].ToString());
        //                //PopulateDropDown(ddlEventEntity, dr["ApplicantID"].ToString());
        //                PopulateDropDown(ddlEvent, dr["EventID"].ToString());
        //                PopulateDropDown(ddlEventSubCategory, dr["SubEventID"].ToString());
        //                txtEventDate.Text = dr["Date"].ToString() == "" ? "" : Convert.ToDateTime(dr["Date"].ToString()).ToShortDateString();
        //                txtEventNotes.Text = dr["Note"].ToString();
        //                chkProjectEventActive.Enabled = true;

        //                //ddlEventProgram.Enabled = false;
        //                //ddlEventProject.Enabled = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(Pagename, "gvAppraisalInfo_RowDataBound", "", ex.Message);
        //    }
        //}

        //protected void ddlEventProgram_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    EventProgramSelection();
        //}

        //private void EventProgramSelection()
        //{
        //    if (ddlEventProgram.SelectedItem.ToString() == "Admin")
        //        BindLookUP(ddlEvent, 157);
        //    else if (ddlEventProgram.SelectedItem.ToString() == "Housing")
        //        BindLookUP(ddlEvent, 160);
        //    else if (ddlEventProgram.SelectedItem.ToString() == "Conservation")
        //        BindLookUP(ddlEvent, 159);
        //    else if (ddlEventProgram.SelectedItem.ToString() == "Lead")
        //        BindLookUP(ddlEvent, 158);
        //    else if (ddlEventProgram.SelectedItem.ToString() == "Americorps")
        //        BindLookUP(ddlEvent, 161);
        //    else if (ddlEventProgram.SelectedItem.ToString() == "Viability")
        //        BindLookUP(ddlEvent, 162);
        //    //else if (ddlEventProgram.SelectedItem.ToString() == "Healthy Homes")
        //    //    BindLookUP(ddlEvent, 159);
        //    else
        //    {
        //        ddlEvent.Items.Clear();
        //        ddlEvent.Items.Insert(0, new ListItem("Select", "NA"));
        //    }
        //}

        //protected void btnAddEvent_Click(object sender, EventArgs e)
        //{
        //    //if (IsProjectEventFormValid())
        //    //{
        //    if (btnAddEvent.Text == "Add")
        //    {
        //        ProjectMaintResult obProjectMaintResult = ProjectMaintenanceData.AddProjectMilestone(ddlEventProject.SelectedValue.ToString(),
        //            DataUtils.GetInt(ddlEventProgram.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()),
        //            DataUtils.GetInt(ddlEvent.SelectedValue.ToString()), DataUtils.GetInt(ddlEventSubCategory.SelectedValue.ToString()),
        //            DataUtils.GetDate(txtEventDate.Text), txtEventNotes.Text, GetUserId());

        //        ClearProjectEventForm();
        //        cbAddProjectEvent.Checked = false;

        //        BindPrjectEventGrid();

        //        if (obProjectMaintResult.IsDuplicate && !obProjectMaintResult.IsActive)
        //            LogMessage("Project Event already exist as in-active");
        //        else if (obProjectMaintResult.IsDuplicate)
        //            LogMessage("Project Event already exist");
        //        else
        //            LogMessage("New Project Event added successfully");
        //    }
        //    else
        //    {
        //        ProjectMaintenanceData.UpdateProjectEvent(DataUtils.GetInt(hfProjectEventID.Value), DataUtils.GetInt(ddlEventProject.SelectedValue.ToString()),
        //          DataUtils.GetInt(ddlEventProgram.SelectedValue.ToString()), DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()),
        //          DataUtils.GetInt(ddlEvent.SelectedValue.ToString()), DataUtils.GetInt(ddlEventSubCategory.SelectedValue.ToString()),
        //          DataUtils.GetDate(txtEventDate.Text), txtEventNotes.Text, GetUserId(), chkProjectEventActive.Checked);

        //        gvProjectEvent.EditIndex = -1;
        //        BindPrjectEventGrid();
        //        ClearProjectEventForm();
        //        btnAddEvent.Text = "Add";
        //        LogMessage("Project Milestone Updated Successfully");
        //    }
        //    //}
        //}

        //protected void gvProjectEvent_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    GridViewSortExpression = e.SortExpression;
        //    int pageIndex = 0;
        //    gvProjectEvent.DataSource = SortDataTable((DataTable)Session["dtProjectEvents"], false);
        //    gvProjectEvent.DataBind();
        //    gvProjectEvent.PageIndex = pageIndex;
        //}

        #region GridView Sorting Functions

        //======================================== GRIDVIEW EventHandlers END

        protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
        {

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                if (GridViewSortExpression != string.Empty)
                {
                    if (isPageIndexChanging)
                    {
                        Session["SortExp"] = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                        dataView.Sort = Session["SortExp"].ToString();
                    }
                    else
                    {
                        Session["SortExp"] = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                        dataView.Sort = Session["SortExp"].ToString();
                    }
                }
                return dataView;
            }
            else
            {
                return new DataView();
            }
        } //eof SortDataTable
        //===========================SORTING PROPERTIES START
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }

        private string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;

                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }

        //===========================SORTING PROPERTIES END
        #endregion

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetEntityNames(string prefixText, int count, string contextKey)
        {
            List<string> EntityNames = new List<string>();

            if (contextKey.ToLower() == "organization")
            {
                DataTable dt = new DataTable();
                dt = EntityMaintenanceData.GetEntityNames(prefixText);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EntityNames.Add("'" + dt.Rows[i][0].ToString() + "'");
                }
            }
            return EntityNames.ToArray();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetIndividualFirstNames(string prefixText, int count)
        {
            List<string> EntityNames = new List<string>();


            DataTable dt = new DataTable();
            dt = EntityMaintenanceData.GetFirstNames(prefixText);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EntityNames.Add("'" + dt.Rows[i][0].ToString() + "'");
            }

            return EntityNames.ToArray();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetIndividualLastNames(string prefixText, int count)
        {
            List<string> EntityNames = new List<string>();


            DataTable dt = new DataTable();
            dt = EntityMaintenanceData.GetLastNames(prefixText);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EntityNames.Add("'" + dt.Rows[i][0].ToString() + "'");
            }

            return EntityNames.ToArray();
        }

        protected void gvEntityMilestone_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEntityMilestone.EditIndex = e.NewEditIndex;
            BindEntityMilestoneGrid();
        }

        protected void gvEntityMilestone_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEntityMilestone.EditIndex = -1;
            cbAddMilestone.Checked = false;
            BindEntityMilestoneGrid();
            EntityMilestoneChanged();
        }

        protected void gvEntityMilestone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            int ProjectEventID = DataUtils.GetInt(((Label)gvEntityMilestone.Rows[rowIndex].FindControl("lblProjectEventID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvEntityMilestone.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            MilestoneData.UpdateMilestone(ProjectEventID, RowIsActive);
            gvEntityMilestone.EditIndex = -1;

            BindEntityMilestoneGrid();

            LogMessage("Milestone updated successfully");
        }

        private void BindEntityMilestoneGrid()
        {
            try
            {
                DataTable dtMilestones = null;

                dtMilestones = MilestoneData.GetEventMilestonesList1(DataUtils.GetInt(ddlEntityName.SelectedItem.Value.ToString()), cbActiveOnly.Checked);


                if (dtMilestones.Rows.Count > 0)
                {
                    dvEntityMilestoneGrid.Visible = true;
                    gvEntityMilestone.DataSource = dtMilestones;
                    gvEntityMilestone.DataBind();
                }
                else
                {
                    dvEntityMilestoneGrid.Visible = false;
                    gvEntityMilestone.DataSource = null;
                    gvEntityMilestone.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindEntityMilestoneGrid", "", ex.Message);
            }
        }

        protected void btnAddMilestone_Click(object sender, EventArgs e)
        {
            string URL = txtURL.Text;

            if (URL != "")
                URL = URL.Split('/').Last();

            if (btnAddMilestone.Text == "Add")
            {
                MilestoneData.MilestoneResult obMilestoneResult = MilestoneData.AddMilestone1(
                DataUtils.GetInt(hfProjectId.Value),
                0,
                ddlEntityName.SelectedItem.Value.ToString(),
                //txtEntityDDL.Text,
                0, 0,
                0, 0,
                DataUtils.GetInt(ddlEntityMilestone.SelectedValue.ToString()), DataUtils.GetInt(ddlEntitySubMilestone.SelectedValue.ToString()),
                DataUtils.GetDate(txtEventDate.Text), txtEntityMilestoneComments.Text, URL, GetUserId());

                cbAddMilestone.Checked = false;
                BindEntityMilestoneGrid();

                if (obMilestoneResult.IsDuplicate && !obMilestoneResult.IsActive)
                    LogMessage("Milestone Event already exist as in-active");
                else if (obMilestoneResult.IsDuplicate)
                    LogMessage("Milestone already exist");
                else
                    LogMessage("New milestone added successfully");
            }
            else
            {
                ProjectMaintenanceData.UpdateProjectEvent3(DataUtils.GetInt(hfProjectEventID.Value),
                   DataUtils.GetInt(hfProjectId.Value), 0,
               ddlEntityName.SelectedItem.Value.ToString(),
               0, null,
               0, 0,
               DataUtils.GetInt(ddlEntityMilestone.SelectedValue.ToString()),
               DataUtils.GetInt(ddlEntitySubMilestone.SelectedValue.ToString()),
               DataUtils.GetDate(txtEventDate.Text), txtEntityMilestoneComments.Text, URL, GetUserId(), true);

                LogMessage("Milestone updated successfully");
                hfProjectEventID.Value = "";
                btnAddMilestone.Text = "Add";
                gvEntityMilestone.EditIndex = -1;
            }
            cbAddMilestone.Checked = false;
            BindEntityMilestoneGrid();
            EntityMilestoneChanged();
        }

        private void ClearEntityAndCommonForm()
        {
            ddlEntityMilestone.SelectedIndex = -1;
            ddlEntitySubMilestone.SelectedIndex = -1;
            txtEventDate.Text = "";
            txtURL.Text = "";
            //txtNotes.Text = "";
            txtEntityMilestoneComments.Text = "";
            EntityMilestoneChanged();
        }

        private void EntityMilestoneChanged()
        {
            ddlEntitySubMilestone.Items.Clear();

            if (ddlEntityMilestone.SelectedIndex != 0)
            {

                BindSubLookUP(ddlEntitySubMilestone, DataUtils.GetInt(ddlEntityMilestone.SelectedValue.ToString()));

                if (ddlEntitySubMilestone.Items.Count > 1)
                    dvSubEntityMilestone.Visible = true;
                else
                    dvSubEntityMilestone.Visible = false;
            }
            else
            {
                dvSubEntityMilestone.Visible = false;
            }
        }

        private void BindSubLookUP(DropDownList ddList, int LookupType)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = LookupValuesData.GetSubLookupValues(LookupType);
                ddList.DataValueField = "SubTypeID";
                ddList.DataTextField = "SubDescription";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindSubLookUP", "Control ID:" + ddList.ID, ex.Message);
            }
        }

        protected void ddlEntityMilestone_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityMilestoneChanged();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetPrimaryApplicant(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt = ApplicantData.GetSortedApplicants(prefixText);

            List<string> ProjNumbers = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProjNumbers.Add("'" + dt.Rows[i][0].ToString() + "'");
            }
            return ProjNumbers.ToArray();
        }

        protected void ddlEntityRole1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedRole = ddlEntityRole1.SelectedValue.ToString();

            //dvExistingEntities.Visible = true;
            BindApplicants(DataUtils.GetInt(SelectedRole), ddlEntityRole1.SelectedItem.ToString(), ddlEntityName1);
        }

        protected void ddlEntityName1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvEntityMilestone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string URL = "";
                HtmlAnchor anchorDocument = e.Row.FindControl("hlurl") as HtmlAnchor;
                string DocumentId = anchorDocument.InnerHtml;

                if (CommonHelper.IsVPNConnected() && DocumentId != "")
                {
                    URL = "fda://document/" + DocumentId;
                    anchorDocument.InnerHtml = "Click";
                    anchorDocument.HRef = URL;
                }
                else if (DocumentId != "")
                {
                    URL = "http://581720-APP1/FH/FileHold/WebClient/LibraryForm.aspx?docId=" + DocumentId;
                    anchorDocument.InnerHtml = "Click";
                    anchorDocument.HRef = URL;
                }
                else
                {
                    anchorDocument.InnerHtml = "";
                }
            }
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                ClearEntityAndCommonForm();
                CommonHelper.GridViewSetFocus(e.Row);
                btnAddMilestone.Text = "Update";
                cbAddMilestone.Checked = true;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[8].Controls[0].Visible = false;

                    Label lblProjectEventID = e.Row.FindControl("lblProjectEventID") as Label;
                    int ProjectEventID = DataUtils.GetInt(lblProjectEventID.Text);

                    DataRow dr = ProjectMaintenanceData.GetProjectEventById(ProjectEventID);

                    hfProjectEventID.Value = lblProjectEventID.Text;
                    //Populate Edit Form

                    PopulateDropDown(ddlEntityMilestone, dr["EntityMSID"].ToString());
                    EntityMilestoneChanged();
                    PopulateDropDown(ddlEntitySubMilestone, dr["EntitySubMSID"].ToString());

                    txtEventDate.Text = dr["Date"].ToString() == "" ? "" : Convert.ToDateTime(dr["Date"].ToString()).ToShortDateString();
                    txtURL.Text = dr["URL"].ToString();

                    txtEntityMilestoneComments.Text = dr["Note"].ToString();

                }
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvAddress.Visible = true;

            if (ddlState.SelectedValue == "VT")
            {
                txtTown.Visible = false;
                txtCounty.Visible = false;
                ddlTown.Visible = true;
                ddlCounty.Visible = true;
                ddlVillage.Visible = true;
                spnVillage.Visible = true;
            }
            else
            {
                ddlTown.Visible = false;
                txtTown.Visible = true;

                ddlCounty.Visible = false;
                txtCounty.Visible = true;

                ddlVillage.Visible = false;
                spnVillage.Visible = false;
            }
        }

        protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounty(ddlTown.SelectedValue);
            BindVillages(ddlTown.SelectedValue);
        }

        private void BindCounty(string Town)
        {
            try
            {
                DataTable dt = ProjectMaintenanceData.GetCountysByTown(Town);

                ddlCounty.Items.Clear();
                ddlCounty.DataSource = dt;
                ddlCounty.DataValueField = "County";
                ddlCounty.DataTextField = "County";
                ddlCounty.DataBind();
                ddlCounty.Items.Insert(0, new ListItem("Select", "NA"));

                if (dt.Rows.Count == 1)
                    ddlCounty.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindCounty", "Control ID:" + ddlCounty.ID, ex.Message);
            }
        }

        private void BindVillages(string Town)
        {
            try
            {
                DataTable dt = ProjectMaintenanceData.GetVillagesByTown(Town);
                ddlVillage.Items.Clear();
                ddlVillage.DataSource = dt;
                ddlVillage.DataValueField = "village";
                ddlVillage.DataTextField = "village";
                ddlVillage.DataBind();
                ddlVillage.Items.Insert(0, new ListItem("Select", "NA"));

                if (dt.Rows.Count == 1)
                    ddlVillage.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindVillages", "Control ID:" + ddlVillage.ID, ex.Message);
            }
        }

        protected void btnGetLatLong_Click(object sender, EventArgs e)
        {
            if (txtStreetNo.Text.Trim() == "" && cbReqStreetNo.Checked)
            {
                LogMessage("Enter Street#");
                txtStreetNo.Focus();
            }
            else if (txtAddress1.Text.Trim() == "")
            {
                LogMessage("Enter Address1");
                txtAddress1.Focus();
            }
            //else if (txtZip.Text.Trim() == "")
            //{
            //    LogMessage("Enter Zip");
            //    txtZip.Focus();
            //}
            else if (ddlState.SelectedValue == "VT" && ddlTown.SelectedIndex == 0)
            {
                LogMessage("Select Town");
                ddlTown.Focus();
            }
            else if (ddlState.SelectedValue != "VT" && txtTown.Text == "")
            {
                LogMessage("Enter Town");
                txtTown.Focus();
            }
            //else if (txtTown.Text.Trim() == "")
            //{
            //    LogMessage("Enter Town");
            //    txtTown.Focus();
            //}
            else
            {
                //https://www.friism.com/c-and-google-geocoding-web-service-v3/
                txtLattitude.Text = "";
                txtLongitude.Text = "";

                string address = string.Format("{0} {1}, {2}, {3}, {4}", txtStreetNo.Text, txtAddress1.Text, ddlTown.SelectedValue,// txtTown.Text, 
                    ddlState.SelectedValue, txtZip.Text);
                string url = string.Format("https://maps.google.com/maps/api/geocode/json?key=AIzaSyCm3xOguaZV1P3mNL0ThK7nv-H9jVyMjSU&address={0}&region=dk&sensor=false", HttpUtility.UrlEncode(address));

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
                var res = (GeoResponse)serializer.ReadObject(request.GetResponse().GetResponseStream());

                if (res.Status == "OK")
                {
                    txtLattitude.Text = "";
                    txtLongitude.Text = "";
                    //txtCounty.Text = "";
                    ddlCounty.SelectedIndex = -1;
                    txtZip.Text = "";
                    txtLattitude.Text = "";
                    txtLongitude.Text = "";

                    for (var ii = 0; ii < res.Results[0].AddressComponents.Length; ii++)
                    {
                        var types = string.Join(",", res.Results[0].AddressComponents[ii].Type.Select(x => x));
                        if (types == "postal_code" || types == "postal_code_prefix,postal_code")
                        {
                            txtZip.Text = res.Results[0].AddressComponents[ii].LongName;
                        }
                        if (types == "administrative_area_level_2,political")
                        {
                            //txtCounty.Text = res.Results[0].AddressComponents[ii].ShortName.Replace("County", "");
                            PopulateDropDown(ddlCounty, res.Results[0].AddressComponents[ii].ShortName.Replace("County", ""));
                        }
                        if (types == "neighborhood" || types == "neighborhood,political")
                        {
                            //txtVillage.Text = res.Results[0].AddressComponents[ii].ShortName;
                            PopulateDropDown(ddlVillage, res.Results[0].AddressComponents[ii].ShortName);
                        }
                    }
                    txtLattitude.Text = res.Results[0].Geometry.Location.Latitude.ToString();
                    txtLongitude.Text = res.Results[0].Geometry.Location.Longitude.ToString();
                }
            }
        }

        protected void btnGetAddress_Click(object sender, EventArgs e)
        {
            if (txtLattitude.Text.Trim() == "")
            {
                LogMessage("Enter Lattitude");
                txtLattitude.Focus();
            }
            else if (txtLongitude.Text.Trim() == "")
            {
                LogMessage("Enter Longitude");
                txtLongitude.Focus();
            }
            else
            {
                txtStreetNo.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                //txtTown.Text = "";
                //txtState.Text = "";
                //txtCounty.Text = "";
                txtZip.Text = "";
                //txtVillage.Text = "";

                string LatLong = string.Format("{0}, {1}", txtLattitude.Text, txtLongitude.Text);
                string url = string.Format("https://maps.google.com/maps/api/geocode/json?key=AIzaSyCm3xOguaZV1P3mNL0ThK7nv-H9jVyMjSU&latlng={0}&region=dk&sensor=false", HttpUtility.UrlEncode(LatLong));

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
                var res = (GeoResponse)serializer.ReadObject(request.GetResponse().GetResponseStream());

                if (res.Status == "OK")
                {
                    for (var ii = 0; ii < res.Results[0].AddressComponents.Length; ii++)
                    {
                        var types = string.Join(",", res.Results[0].AddressComponents[ii].Type.Select(x => x));

                        if (types == "street_number")
                        {
                            txtStreetNo.Text = res.Results[0].AddressComponents[ii].ShortName;
                        }
                        if (types == "route" || types == "point_of_interest,establishment")
                        {
                            txtAddress1.Text = res.Results[0].AddressComponents[ii].ShortName;
                        }
                        if (types == "neighborhood" || types == "neighborhood,political")
                        {
                            //txtVillage.Text = res.Results[0].AddressComponents[ii].ShortName;
                        }
                        if (types == "sublocality,political" || types == "locality,political" || types == "neighborhood,political" || types == "administrative_area_level_3,political")
                        {
                            //txtTown.Text = res.Results[0].AddressComponents[ii].LongName;
                        }
                        if (types == "administrative_area_level_1,political")
                        {
                            //txtState.Text = res.Results[0].AddressComponents[ii].ShortName;
                        }
                        if (types == "postal_code" || types == "postal_code_prefix,postal_code")
                        {
                            txtZip.Text = res.Results[0].AddressComponents[ii].LongName;
                        }
                        if (types == "administrative_area_level_2,political")
                        {
                            //txtCounty.Text = res.Results[0].AddressComponents[ii].ShortName.Replace("County", "");
                            PopulateDropDown(ddlCounty, res.Results[0].AddressComponents[ii].ShortName.Replace("County", ""));
                        }
                    }
                }
            }
        }

        private void BindAttachedProjects()
        {
            try
            {
                DataTable dtAttachedProjects = null;

                dtAttachedProjects = MilestoneData.GetAttachedProjectsList(DataUtils.GetInt(hfApplicatId.Value));


                if (dtAttachedProjects.Rows.Count > 0)
                {
                    dvAttchedProjectsGrid.Visible = true;
                    gvAttachedProjects.DataSource = dtAttachedProjects;
                    gvAttachedProjects.DataBind();
                }
                else
                {
                    dvAttchedProjectsGrid.Visible = false;
                    gvAttachedProjects.DataSource = null;
                    gvAttachedProjects.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAttachedProjects", "", ex.Message);
            }
        }

        protected void ImgAttchedprojectsReport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                   "script", Helper.GetAttachedProjectsReport(hfApplicatId.Value, "Entities Attached to Projects"));
        }

        protected void ImgNotesReport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                    "script", Helper.GetAttachedProjectsReportPDF(hfApplicatId.Value, "Entity Notes"));
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddNotes.Text.ToLower() == "submit")
                {
                    if (IsEntityNotesValid())
                    {
                        EntityNotesData.AddEntityNotes(DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()), 0,
                               Context.User.Identity.GetUserName().Trim(), txtEntityNotes.Text, "");

                        LogMessage("Entity Noted Added Successfully");
                    }
                }
                else
                {
                    EntityNotesData.UpdateEntityNotes(DataUtils.GetInt(hfEntityNotesId.Value),
                        0, txtEntityNotes.Text, "", cbNotesActive.Checked);
                    hfEntityNotesId.Value = "";

                    gvNotes.EditIndex = -1;
                    LogMessage("Entity Noted Updated Successfully");
                    btnAddNotes.Text = "Submit";
                }
                BindEntityNotesGrid();
                cbAddnotes.Checked = false;
                cbNotesActive.Enabled = false;
                cbNotesActive.Checked = true;
                txtEntityNotes.Text = "";
            }
            catch (Exception ex)
            {
                LogError(Pagename, "btnAddNotes_Click", null, ex.Message);
            }
        }

        private bool IsEntityNotesValid()
        {
            if (txtEntityNotes.Text.Trim() == "")
            {
                LogMessage("Enter Notes");
                txtNotes.Focus();
                return false;
            }

            return true;
        }

        protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    Label lblUserName = (Label)e.Row.FindControl("lbluserName");

                //    if (lblUserName.Text.ToLower().Trim() != Context.User.Identity.GetUserName().Trim())
                //    {
                //        LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                //        lnkEdit.Visible = false;
                //    }
                //}

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    CommonHelper.GridViewSetFocus(e.Row);

                    //Checking whether the Row is Data Row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //e.Row.Cells[5].Controls[0].Visible = false;
                        btnAddNotes.Text = "Update";

                        Label lblEntityNotesID = e.Row.FindControl("lblEntityNotesID") as Label;
                        DataRow dr = EntityNotesData.GetProjectNotesById(DataUtils.GetInt(lblEntityNotesID.Text));

                        hfEntityNotesId.Value = lblEntityNotesID.Text;
                        txtEntityNotes.Text = dr["Notes"].ToString();
                        cbAddnotes.Checked = true;
                        cbNotesActive.Checked = DataUtils.GetBool(dr["RowIsActive"].ToString());
                        cbNotesActive.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "gvNotes_RowDataBound", "", ex.Message);
            }
        }

        protected void gvNotes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvNotes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNotes.EditIndex = e.NewEditIndex;
            BindEntityNotesGrid();
        }

        protected void gvNotes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            btnAddNotes.Text = "Submit";
            txtEntityNotes.Text = "";
            txtURL.Text = "";
            hfEntityNotesId.Value = "";
            cbAddnotes.Checked = false;
            gvNotes.EditIndex = -1;
            BindEntityNotesGrid();
        }

        private void BindEntityNotesGrid()
        {
            DataTable dt = EntityNotesData.GetEntityNotesList(DataUtils.GetInt(ddlEntityName.SelectedValue.ToString()), cbActiveOnly.Checked);

            if (dt.Rows.Count > 0)
            {
                dvNotesGrid.Visible = true;
                gvNotes.DataSource = dt;
                gvNotes.DataBind();
            }
            else
            {
                dvNotesGrid.Visible = false;
                gvNotes.DataSource = null;
                gvNotes.DataBind();
            }

        }

        protected void ckbACHActive_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbACHActive.Checked)
            {
                txtACHContact.Enabled = true;
                txtACHEmail.Enabled = true;
            }
            else
            {
                txtACHContact.Enabled = false;
                txtACHEmail.Enabled = false;
                txtACHContact.Text = "";
                txtACHEmail.Text = "";
            }
        }
    }
}