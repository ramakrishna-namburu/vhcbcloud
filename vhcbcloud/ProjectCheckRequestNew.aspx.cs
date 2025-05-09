﻿using DataAccessLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class ProjectCheckRequestNew : System.Web.UI.Page
    {
        string Pagename = "ProjectCheckRequest";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;

            if (!IsPostBack)
            {
                BindStatus();
                BindMatchingGrant();
                BindNODData();
                BindPCRItemsData();
                BindCRDates();
                pnlDisbursement.Visible = false;
            }
            //CheckNewCRAccess();
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

            HandleCustomPostbackEvent(ctrlName, args);
        }

        private void CheckNewCRAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "27578")
                    rdBtnSelect.Items[0].Enabled = false;
            }
            if (!rdBtnSelect.Items[0].Enabled)
                rdBtnSelect.SelectedIndex = 1;
        }

        protected bool GetIsVisibleBasedOnRole()
        {
            bool IsShow = DataUtils.GetBool(hfIsVisibleBasedOnRole.Value);

            if (IsShow)
            {
                IsShow = !DataUtils.GetBool(hfIsAllApproved.Value); ;
            }

            return IsShow;
        }

        /// <summary>
        /// Role based access logic
        /// </summary>
        protected void GetRoleAccess()
        {

            DataRow dr = UserSecurityData.GetUserSecurity(Context.User.Identity.Name);
            DataRow drProjectDetails = ProjectMaintenanceData.GetprojectDetails(DataUtils.GetInt(hfProjId.Value));

            if (dr != null)
            {
                bool IsUserHasSameProgram = UserSecurityData.IsUserHasSameProgramId(DataUtils.GetInt(dr["userid"].ToString()), DataUtils.GetInt(hfProjId.Value));

                if (dr["usergroupid"].ToString() == "0") // Admin Only
                {
                    hfIsAdmin.Value = "true";
                    hfIsVisibleBasedOnRole.Value = "true";
                }
                else if (dr["usergroupid"].ToString() == "1") // Program Admin Only
                {
                    hfIsprgramAdmin.Value = "true";

                    //if (dr["dfltprg"].ToString() != drProjectDetails["LkProgram"].ToString())
                    if (!IsUserHasSameProgram)
                    {
                        RoleViewOnly(); 

                        if(DataUtils.GetBool(dr["ChReqMod"].ToString()))
                            ddlCRDate.Enabled = true;

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
                        DataTable dtEPCR = ProjectCheckRequestData.GetExistingPCRByProjId(hfProjId.Value.ToString());
                        if (dtEPCR.Rows.Count > 0)
                        {
                            if (dtEPCR.Rows[0]["CreatedById"].ToString() != GetUserId().ToString())
                            {
                                RoleViewOnly();
                                hfIsVisibleBasedOnRole.Value = "false";
                            }
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

        protected void RoleViewOnly()
        {
            btnCRSubmit.Visible = false;
            updateSpan.Visible = false;

            btnDelete.Visible = false;
            btnPCRTransDetails.Visible = false;
            btnApprovalsSubmit.Visible = false;
            btnAddVoucher.Visible = false;
            ddlCRDate.Enabled = false;
        }

        /// <summary>
        /// Bind CR data to dropdown
        /// </summary>
        protected void BindCRDates()
        {
            try
            {
                ddlCRDate.DataSource = ProjectCheckRequestData.GetCRDates(); ;
                ddlCRDate.DataValueField = "CRDate";
                ddlCRDate.DataTextField = "CRDate";
                ddlCRDate.DataBind();
                ddlCRDate.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: BindStatus: " + ex.Message);
                lblErrorMsg.Focus();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtProjNum, "OnBlur");
            txtProjNum.Attributes.Add("onblur", onBlurScript);
        }

        private void HandleCustomPostbackEvent(string ctrlName, string args)
        {
            if (ctrlName == txtProjNum.UniqueID && args == "OnBlur")
            {
                ProjectSelectionChanged();
            }
        }

        /// <summary>
        /// Project selection changed
        /// Clear hidden fields and Populating PCR form
        /// </summary>
        private void ProjectSelectionChanged()
        {
            try
            {
                if (txtProjNum.Text != "")
                {
                    //hfProjId.Value = GetProjectID(txtProjNum.Text).ToString();
                    ClearHiddenFieldValues();
                    PopulatePCRForm();
                }
                else
                {
                    hfProjId.Value = "";
                }

            }
            catch (Exception ex)
            {
                LogError(Pagename, "ProjectSelectionChanged", "", ex.Message);
            }
        }

        /// <summary>
        /// Populate PCR form fields
        /// Setting available funds
        /// Enabling PCR
        /// </summary>
        private void PopulatePCRForm()
        {
            DataTable dt = Project.GetProjects("GetProjectIdByProjNum", txtProjNum.Text.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                hfProjId.Value = dt.Rows[0][0].ToString();
                GetRoleAccess();
                ClearPCRForm();
                SetAvailableFunds();

                DataTable dtFundInfo = new DataTable();
                dtFundInfo = ProjectCheckRequestData.GetExistingPCRByProjId(hfProjId.Value.ToString());

                if (rdBtnSelect.SelectedIndex == 0 && dtFundInfo.Rows.Count == 0)
                {
                    EnablePCR();
                    lblProjName.Text = dt.Rows[0][1].ToString();
                    hfProjName.Value = dt.Rows[0][1].ToString();
                    lblProjectType.Text = dt.Rows[0][2].ToString();
                    BindApplicantName(int.Parse(hfProjId.Value));
                    ifProjectNotes.Src = "ProjectNotes.aspx?ProjectId=" + hfProjId.Value;

                    BindFundTypeCommitments(int.Parse(hfProjId.Value));
                    txtTransDate.Text = DateTime.Now.ToShortDateString();
                    // txtCRDate.Text = DateTime.Now.ToShortDateString();
                    ddlCRDate.SelectedIndex = -1;

                    if (ddlPayee.Items.Count == 0)
                    {
                        dvMessage.Visible = true;
                        LogMessage("Add a payee to this project before proceeding with disbursement");
                        return;
                    }
                    btnCRSubmit.Text = "Submit";
                    btnCRSubmit.ToolTip = "";
                    updateSpan.Visible = false;
                    spnCreatedBy.InnerHtml = Context.User.Identity.GetUserName();
                }
                else
                {
                    if (rdBtnSelect.SelectedIndex == 0)
                    {
                        rdBtnSelect.SelectedIndex = 1;
                    }
                    PopulateExistingData(dt);
                }
                if (ddlProgram.SelectedItem != null)
                    DisplayControls(ddlProgram.SelectedItem.ToString());
            }
            else
            {
                hfProjId.Value = "";
                hfProjName.Value = "";
                ClearPCRForm();
            }
        }

        /// <summary>
        /// Binding data FundType Commitments dropdown
        /// </summary>
        /// <param name="projId"></param>
        protected void BindFundTypeCommitments(int projId)
        {
            try
            {
                DataTable dtFundType;
                dtFundType = new DataTable();
                dtFundType = FinancialTransactions.GetCommittedFundAccounts(projId);

                ddlFundTypeCommitments.DataSource = dtFundType;
                ddlFundTypeCommitments.DataValueField = "FundId";
                ddlFundTypeCommitments.DataTextField = "name";
                ddlFundTypeCommitments.DataBind();
                ddlFundTypeCommitments.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Binding data to UsePermit drop down
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="FundId"></param>
        protected void BindUsePermitNew(int ProjectId, int FundId)
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = FinancialTransactions.GetAllLandUsePermitForDecommitment(ProjectId, FundId);
                ddlUsePermit.DataSource = dtable;
                ddlUsePermit.DataValueField = "Act250FarmId";
                ddlUsePermit.DataTextField = "UsePermit";
                ddlUsePermit.DataBind();
                if (ddlUsePermit.Items.Count > 1)
                    ddlUsePermit.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }

        }

        /// <summary>
        /// dropdown fund type commitments data selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFundTypeCommitments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFundTypeCommitments.SelectedIndex != 0)
            {
                lblErrorMsg.Text = "";
                ddlTransType.Items.Clear();
                ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));
                ddlUsePermit.Items.Clear();
                lblUsePermit.Visible = false;
                ddlUsePermit.Visible = false;
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");

                DataTable dtable = FinancialTransactions.GetFundDetailsByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));

                //string account = FinancialTransactions.GetAccountNumberByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));


                ddlTransType.DataSource = FinancialTransactions.GetAvailableTransTypesPerProjFundId(Convert.ToInt32(hfProjId.Value.ToString()),
                    Convert.ToInt32(ddlFundTypeCommitments.SelectedValue.ToString())); ;

                ddlTransType.DataValueField = "typeid";
                ddlTransType.DataTextField = "fundtype";
                ddlTransType.DataBind();

                if (ddlTransType.Items.Count > 1)
                    ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));
                else
                {
                    if (ddlTransType.Items.Count < 1)
                    {
                        lblErrorMsg.Text = "No transaction types found for this fund and hence this fund can't be used for disbursement";
                        return;
                    }
                    lblErrorMsg.Text = "";

                    if (dtable.Rows[0]["mitfund"].ToString().ToLower() == "true")
                    {
                        lblUsePermit.Visible = true;
                        ddlUsePermit.Visible = true;
                        string account = FinancialTransactions.GetAccountNumberByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));

                        PopulateUsePermit(DataUtils.GetInt(hfProjId.Value), account, DataUtils.GetInt(ddlTransType.SelectedValue.ToString()));
                    }
                    else
                    {
                        ddlUsePermit.Items.Clear();
                        lblUsePermit.Visible = false;
                        ddlUsePermit.Visible = false;
                        SetAvailableFundsLabel();
                    }
                }
            }
            else
            {
                ddlTransType.Items.Clear();
                ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));
                lblErrorMsg.Text = "";
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
                //ddlAcctNum.SelectedIndex = 0;
                ddlUsePermit.Items.Clear();
                lblUsePermit.Visible = false;
                ddlUsePermit.Visible = false;
            }
        }

        /// <summary>
        /// TransType drop down selection changed
        /// re-populating and cleanup data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
            if (ddlTransType.SelectedIndex > 0)
            {
                DataTable dtable = FinancialTransactions.GetFundDetailsByFundId(Convert.ToInt32(ddlFundTypeCommitments.SelectedValue.ToString()));
                string account = FinancialTransactions.GetAccountNumberByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));

                if (dtable.Rows[0]["mitfund"].ToString().ToLower() == "true")
                {
                    lblUsePermit.Visible = true;
                    ddlUsePermit.Visible = true;
                    PopulateUsePermit(DataUtils.GetInt(hfProjId.Value), account,
                               DataUtils.GetInt(ddlTransType.SelectedValue.ToString()));
                }
                else
                {
                    ddlUsePermit.Items.Clear();
                    lblUsePermit.Visible = false;
                    ddlUsePermit.Visible = false;

                    if (ddlTransType.SelectedIndex > 0)
                    {
                        SetAvailableFundsLabel();
                    }
                }
            }
            else
            {
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
                ddlUsePermit.Items.Clear();
                lblUsePermit.Visible = false;
                ddlUsePermit.Visible = false;
            }
        }

        /// <summary>
        /// UsePermit dropdown selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUsePermit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
            if (ddlUsePermit.SelectedIndex > 0)
            {
                SetAvailableFundsLabel();
            }
        }

        /// <summary>
        /// Use Permit dropdown populate
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="AccountId"></param>
        /// <param name="FundTransType"></param>
        protected void PopulateUsePermit(int ProjectId, string AccountId, int FundTransType)
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = FinancialTransactions.GetLandUsePermit(ProjectId, AccountId, FundTransType);
                ddlUsePermit.DataSource = dtable;
                ddlUsePermit.DataValueField = "Act250FarmId";
                ddlUsePermit.DataTextField = "UsePermit";
                ddlUsePermit.DataBind();

                if (ddlUsePermit.Items.Count > 1)
                    ddlUsePermit.Items.Insert(0, new ListItem("Select", "NA"));
                else
                {
                    SetAvailableFundsLabel();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }

        }

        #region gvPTransDetails
        protected void gvPTransDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim() != "")
                {
                    decimal n;
                    bool isDecimal = decimal.TryParse(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim(), out n);

                    if (!isDecimal || Convert.ToDecimal(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim()) <= 0)
                    {
                        LogMessage("Select a valid transaction amount");
                        ((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Focus();
                        return;
                    }
                }

                decimal amount = Convert.ToDecimal(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text);
                int transType = Convert.ToInt32(((DropDownList)gvPTransDetails.Rows[rowIndex].FindControl("ddlTransType")).SelectedValue.ToString());
                int detailId = Convert.ToInt32(((Label)gvPTransDetails.Rows[rowIndex].FindControl("lblDetId")).Text);

                decimal old_amount = -Convert.ToDecimal(FinancialTransactions.GetTransDetails(detailId).Rows[0]["Amount"].ToString());
                decimal bal_amount = Convert.ToDecimal(hfBalAmt.Value);
                decimal allowed_amount = old_amount + bal_amount;

                if (amount == allowed_amount)
                {
                    LogMessage("Transaction is complete, more funds not allowed");
                }
                else if (amount > allowed_amount)
                {
                    //amount = allowed_amount;
                    LogMessage("Detail amount can't be more than transaction amount");
                    return;
                }
                else if (amount < allowed_amount)
                {
                    if (!btnPCRTransDetails.Enabled)
                    {
                        EnableButton(btnPCRTransDetails);
                        //DisableButton(btnSubmit);
                    }
                }
                FinancialTransactions.UpdateTransDetails(detailId, transType, -amount);

                gvPTransDetails.EditIndex = -1;
                BindPCRTransDetails();
                SetAvailableFunds();
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: gvPTransDetails_RowUpdating: " + ex.Message); lblErrorMsg.Focus();
            }

        }

        protected void gvPTransDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPTransDetails.EditIndex = e.NewEditIndex;
            BindPCRTransDetails();
            //if (btnNewPCR.Visible == true)
            //{
            //    btnNewPCR.Visible = false;
            //}
        }

        protected void gvPTransDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPTransDetails.EditIndex = -1;
            BindPCRTransDetails();
        }

        protected void gvPTransDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
            {
                //Checking whether the Row is Data Row
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTrans = (e.Row.FindControl("ddlTransType") as DropDownList);
                    TextBox txtTransType = (e.Row.FindControl("txtTransType") as TextBox);
                    if (txtTransType != null)
                    {
                        DataTable dtable = new DataTable();
                        dtable = FinancialTransactions.GetLookupDetailsByName("LkTransType");
                        ddlTrans.DataSource = dtable;
                        ddlTrans.DataValueField = "typeid";
                        ddlTrans.DataTextField = "Description";
                        ddlTrans.DataBind();
                        string itemToCompare = string.Empty;
                        foreach (ListItem item in ddlTrans.Items)
                        {
                            itemToCompare = item.Value.ToString();
                            if (txtTransType.Text.ToLower() == itemToCompare.ToLower())
                            {
                                ddlTrans.ClearSelection();
                                item.Selected = true;
                            }
                        }
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void gvPTransDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                Label lblDetailId = (Label)gvPTransDetails.Rows[rowIndex].FindControl("lblDetId");
                if (lblDetailId != null)
                    FinancialTransactions.DeleteTransactionDetail(Convert.ToInt32(lblDetailId.Text));
                BindPCRTransDetails();
                SetAvailableFunds();
                LogMessage("Transaction detail was successfully deleted");
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: Delete detail: " + ex.Message); lblErrorMsg.Focus();
            }
        }
        #endregion
        protected void btnPCRTransDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string str = txtTransDetailAmt.Text;
                string tmp = Regex.Replace(str, "[^0-9a-zA-Z.]+", "");
                txtTransDetailAmt.Text = tmp.ToString();


                #region Validations
                if (ddlFundTypeCommitments.Items.Count > 1 && ddlFundTypeCommitments.SelectedIndex == 0)
                {
                    LogMessage("Select Source");
                    ddlFundTypeCommitments.Focus();
                    return;
                }
                if (ddlTransType.Items.Count > 1 && ddlTransType.SelectedIndex == 0)
                {
                    LogMessage("Select Grant/Loan/Contract");
                    ddlTransType.Focus();
                    return;
                }

                string account = FinancialTransactions.GetAccountNumberByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));

                if (account == "420" || account == "415")
                {
                    if (ddlUsePermit.Items.Count > 1 && ddlUsePermit.SelectedIndex == 0)
                    {
                        LogMessage("Select Use Permit");
                        ddlUsePermit.Focus();
                        return;
                    }
                }

                if (txtTransDetailAmt.Text.Trim() == "")
                {
                    LogMessage("Select Amount");
                    txtTransDetailAmt.Focus();
                    return;
                }
                if (txtTransDetailAmt.Text.Trim() != "")
                {
                    decimal n;
                    bool isDecimal = decimal.TryParse(txtTransDetailAmt.Text.Trim(), out n);

                    if (!isDecimal || Convert.ToDecimal(txtTransDetailAmt.Text) <= 0)
                    {
                        LogMessage("Select a valid Amount");
                        txtTransDetailAmt.Focus();
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtTransDetailAmt.Text) > Convert.ToDecimal(hfAvFunds.Value.ToString()))
                        {
                            LogMessage("Disbursement amount can not be more than Available funds.");
                            txtTransDetailAmt.Focus();
                            return;
                        }
                    }
                }

                #endregion

                decimal currentTranAmount = 0;
                decimal currentTranFudAmount = 0;
                decimal currentBalAmount = 0;

                currentTranAmount = Convert.ToDecimal(hfTransAmt.Value);
                currentTranFudAmount = decimal.Parse(txtTransDetailAmt.Text);
                currentBalAmount = Convert.ToDecimal(hfBalAmt.Value);

                hfTransId.Value = this.hfTransId.Value;

                if (hfTransId.Value != null)
                {
                    int transId = Convert.ToInt32(hfTransId.Value);

                    if (gvPTransDetails.Rows.Count == 0)
                        currentBalAmount = currentTranAmount;

                    if (currentBalAmount == 0 && gvPTransDetails.Rows.Count > 0)
                    {
                        LogMessage("This transaction details are all set. No more funds allowed to add for the transaction.");
                        ClearTransactionDetailForm();
                        DisableButton(btnPCRTransDetails);
                        return;
                    }
                    else if (currentTranFudAmount > currentBalAmount)
                    {
                        //currentTranFudAmount = currentBalAmount
                        LogMessage("Amount entered is greater than available balance.");
                        return;
                    }

                    if (account == "420" || account == "415")
                    {
                        ProjectCheckRequestData.AddPCRTransactionFundDetailsWithLandUsePermit(transId, Convert.ToInt32(ddlFundTypeCommitments.SelectedValue.ToString()),
                        Convert.ToInt32(ddlTransType.SelectedValue.ToString()), currentTranFudAmount, Convert.ToInt32(hfProjId.Value),
                        ddlUsePermit.SelectedItem.Text, ddlUsePermit.SelectedValue.ToString());
                    }
                    else
                        ProjectCheckRequestData.AddPCRTransactionFundDetails(int.Parse(hfTransId.Value.ToString()), int.Parse(ddlFundTypeCommitments.SelectedValue.ToString()),
                         int.Parse(ddlTransType.SelectedValue.ToString()), currentTranFudAmount, Convert.ToInt32(hfProjId.Value));

                    //AddDefaultPCRQuestions();
                    BindPCRTransDetails();
                    ClearTransactionDetailForm();
                    SetAvailableFunds();
                }
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: btnPCRTransDetails_Click: " + ex.Message); lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Bind PCR Transactions details
        /// </summary>
        private void BindPCRTransDetails()
        {
            try
            {
                DataTable dtPCRTranDetails = new DataTable();
                dtPCRTranDetails = ProjectCheckRequestData.GetPCRTranDetails(this.hfTransId.Value);

                gvPTransDetails.DataSource = dtPCRTranDetails;
                gvPTransDetails.DataBind();
                hfBalAmt.Value = "0";
                decimal tranAmount = 0;
                decimal totFundAmt = 0;
                decimal totBalAmt = 0;
                hfSecondapproval.Value = "false";

                if (dtPCRTranDetails.Rows.Count > 0)
                {
                    //tranAmount = Convert.ToDecimal(dtFundDet.Rows[0]["TransAmt"].ToString());
                    tranAmount = Convert.ToDecimal(this.hfTransAmt.Value);

                    Label lblTotAmt = (Label)gvPTransDetails.FooterRow.FindControl("lblFooterAmount");
                    Label lblBalAmt = (Label)gvPTransDetails.FooterRow.FindControl("lblFooterBalance");

                    if (dtPCRTranDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPCRTranDetails.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dtPCRTranDetails.Rows[i]["Secondapproval"].ToString()))
                                hfSecondapproval.Value = "true";

                            totFundAmt += Convert.ToDecimal(dtPCRTranDetails.Rows[i]["Amount"].ToString());
                        }
                        totFundAmt = -totFundAmt;
                    }

                    totBalAmt = tranAmount - totFundAmt;
                    hfBalAmt.Value = totBalAmt.ToString();

                    lblTotAmt.Text = CommonHelper.myDollarFormat(totFundAmt);
                    lblBalAmt.Text = CommonHelper.myDollarFormat(totBalAmt);

                    gvPTransDetails.Columns[0].Visible = false;
                    gvPTransDetails.FooterRow.Visible = true;

                    //lblTransDetHeader.Text = "Transaction Detail";

                    if (totBalAmt == 0)
                    {
                        tblFundDetails.Visible = false;
                        DisableButton(btnPCRTransDetails);
                        //EnableButton(btnSubmit);
                        AddDefaultPCRQuestions();
                        BindPCRQuestionsForApproval();
                        ImgPrintPCR.Visible = true;
                    }
                    else
                    {
                        tblFundDetails.Visible = true;
                        EnableButton(btnPCRTransDetails);
                        pnlApprovals.Visible = false;
                        ImgPrintPCR.Visible = false;
                    }

                    if (lblBalAmt.Text != "$0.00")
                    {
                        LogMessage("The transaction balance amount must be zero prior to leaving this page");
                        //btnNewPCR.Visible = false;
                    }
                    else if (lblBalAmt.Text == "$0.00")
                    {
                        //btnNewPCR.Visible = true;
                    }
                }
                else
                {
                    tblFundDetails.Visible = true;
                    EnableButton(btnPCRTransDetails);
                }
            }
            catch (Exception ex)
            {
                LogMessage(Pagename + ": BindPCRTransDetails: " + ex.Message);
                lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Default PCR Questions to database
        /// </summary>
        protected void AddDefaultPCRQuestions()
        {
            try
            {
                ProjectCheckRequestData.AddDefaultPCRQuestions(chkLegalReview.Checked, int.Parse(this.hfPCRId.Value),
                    GetUserId(), DataUtils.GetBool(hfSecondapproval.Value));
                BindPCRQuestionsForApproval();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Focus();
                lblErrorMsg.Text = "Exception arised while adding default PCR questions: " + ex.Message;
            }
        }

        /// <summary>
        /// clar transactions details form
        /// </summary>
        private void ClearTransactionDetailForm()
        {
            if (ddlFundTypeCommitments.Items.Count >= 0) ddlFundTypeCommitments.SelectedIndex = 0;

            ddlTransType.Items.Clear();
            ddlUsePermit.Items.Clear();
            ddlTransType.DataSource = null;
            ddlTransType.DataBind();

            txtTransDetailAmt.Text = "";
            lblCommittedAvailFunds.Text = "";
        }

        /// <summary>
        /// populating existing data
        /// </summary>
        /// <param name="dt"></param>
        private void PopulateExistingData(DataTable dt)
        {
            //txtCRDate.Text = "";
            ddlCRDate.SelectedIndex = -1;

            DataTable dtEPCR = ProjectCheckRequestData.GetExistingPCRByProjId(hfProjId.Value.ToString());
            if (dtEPCR.Rows.Count > 0)
            {
                CheckDeletePCRAccess();
                hfIsAllApproved.Value = dtEPCR.Rows[0]["AllApproved"].ToString();

                this.hfPCRId.Value = dtEPCR.Rows[0]["ProjectCheckReqId"].ToString();
                this.hfTransId.Value = dtEPCR.Rows[0]["transid"].ToString();
                this.hfTransAmt.Value = dtEPCR.Rows[0]["TransAmt"].ToString();
                this.hfProjId.Value = dtEPCR.Rows[0]["ProjectID"].ToString();
                this.hfCreatedById.Value = dtEPCR.Rows[0]["CreatedById"].ToString();

                ifProjectNotes.Src = "ProjectNotes.aspx?pcrid=" + hfPCRId.Value + "&ProjectId=" + hfProjId.Value;
                this.lblProjName.Text = dtEPCR.Rows[0]["Project_name"].ToString();
                hfProjName.Value = dtEPCR.Rows[0]["Project_name"].ToString();

                PopulateDropDown(ddlCRDate, dtEPCR.Rows[0]["CRDate"].ToString());

                spnCreatedBy.InnerHtml = dtEPCR.Rows[0]["CreatedBy"].ToString();

                //this.txtCRDate.Text = String.IsNullOrEmpty(dtEPCR.Rows[0]["CRDate"].ToString()) ? "" : DateTime.Parse(dtEPCR.Rows[0]["CRDate"].ToString()).ToShortDateString();
                lblProjectType.Text = dt.Rows[0][2].ToString();

                btnCRSubmit.Text = "Update";
                btnCRSubmit.ToolTip = "Clicking on update will clear all approvals as well as updating this panel";

                if(!btnDelete.Visible)
                    updateSpan.Visible = true;

                ckbACHActive.Checked = DataUtils.GetBool(dtEPCR.Rows[0]["ACH"].ToString());
                //btnCRSubmit.Visible = true;

                //EnableButton(btnPCRTransDetails);
                //DisableButton(btnCRSubmit);
                BindPCRTransDetails();
                //BindPCRQuestionsForApproval();
                ddlPCRQuestions.SelectedIndex = -1;
                //BindPCRData(int.Parse(hfProjId.Value));

                ////fillPCRDetails(Convert.ToInt32(hfPCRId.Value), dtEPCR.Rows[0]["project_name"].ToString());
                DisablePCR();
                BindFundTypeCommitments(Convert.ToInt32(hfProjId.Value));

                PopulateForm();
            }
        }

        /// <summary>
        /// generaic code for dropdown population
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="DBSelectedvalue"></param>
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

        /// <summary>
        /// Populate Form
        /// </summary>
        protected void PopulateForm()
        {
            try
            {
                //ClearPCRForm();
                ClearPCRDetails();
                ClearTransactionDetailForm();
                //EnableButton(btnPCRTransDetails);
                //DisableButton(btnSubmit);
                //GetPCRSelectedRecord(gvFund);

                var x = hfPCRId.Value;
                var y = hfTransId.Value;
                var z = hfTransAmt.Value;
                var a = hfProjName.Value;

                BindPCRTransDetails();
                //BindPCRQuestionsForApproval();
                ddlPCRQuestions.SelectedIndex = -1;

                lblAmtEligibleForMatch.Visible = false;
                txtEligibleAmt.Visible = false;
                ddlMatchingGrant.Visible = false;
                lblMatchingGrant.Visible = false;
                //txtCRDate.Text = "";
                ddlCRDate.SelectedIndex = -1;
                //pnlFund.Visible = true;
                //pnlApprovals.Visible = true;
                pnlDisbursement.Visible = true;
                ifProjectNotes.Src = "ProjectNotes.aspx?pcrid=" + hfPCRId.Value + "&ProjectId=" + hfProjId.Value;

                #region Fill PCR Entry

                DataSet ds = new DataSet();
                DataTable dtable = new DataTable();
                ds = ProjectCheckRequestData.GetPCRDetails(int.Parse(hfPCRId.Value));

                DataRow drPCR = ds.Tables[0].Rows[0];
                DataRow drTrans = ds.Tables[1].Rows[0];

                DataTable dtNOD = new DataTable();
                dtNOD = ds.Tables[4];

                DataTable dtItems = new DataTable();
                dtItems = ds.Tables[7];

                lblProjName.Text = hfProjName.Value.ToString();

                BindApplicantName(int.Parse(drPCR["ProjectID"].ToString()));

                PopulateDropDown(ddlCRDate, drPCR["CRDate"].ToString());

                spnCreatedBy.InnerHtml = drPCR["CreatedBy"].ToString();

                //txtCRDate.Text = String.IsNullOrEmpty(drPCR["CRDate"].ToString()) ? "" : DateTime.Parse(drPCR["CRDate"].ToString()).ToShortDateString();
                txtTransDate.Text = String.IsNullOrEmpty(drPCR["InitDate"].ToString()) ? "" : DateTime.Parse(drPCR["InitDate"].ToString()).ToShortDateString();

                foreach (ListItem item in ddlPayee.Items)
                {
                    if (drTrans["PayeeApplicant"].ToString() == item.Value.ToString())
                    {
                        ddlPayee.ClearSelection();
                        item.Selected = true;
                    }
                }

                foreach (ListItem item in ddlProgram.Items)
                {
                    if (drPCR["LkProgram"].ToString() == item.Value.ToString())
                    {
                        ddlProgram.ClearSelection();
                        item.Selected = true;
                        DisplayControls(item.Text);
                    }
                }

                foreach (ListItem item in ddlStatus.Items)
                {
                    if (drTrans["LkStatus"].ToString() == item.Value.ToString())
                    {
                        ddlStatus.ClearSelection();
                        item.Selected = true;
                    }
                }

                txtVendorId.Text = String.IsNullOrEmpty(drTrans["VendorId"].ToString()) ? "" : drTrans["VendorId"].ToString(); //String.IsNullOrEmpty(drTrans["PayeeApplicant"].ToString()) ? "" : drTrans["VendorId"].ToString();
                //chkLCB.Checked = String.IsNullOrEmpty(drPCR["LCB"].ToString()) ? false : bool.Parse(drPCR["LCB"].ToString());
                chkLegalReview.Checked = String.IsNullOrEmpty(drPCR["LegalReview"].ToString()) ? false : bool.Parse(drPCR["LegalReview"].ToString());
                txtEligibleAmt.Text = String.IsNullOrEmpty(drPCR["MatchAmt"].ToString()) ? "" : Decimal.Round(Decimal.Parse(drPCR["MatchAmt"].ToString()), 2).ToString();
                txtNotes.Text = String.IsNullOrEmpty(drPCR["Notes"].ToString()) ? "" : drPCR["Notes"].ToString();
                txtDisbursementAmt.Text = String.IsNullOrEmpty(drTrans["TransAmt"].ToString()) ? "" : Decimal.Round(Decimal.Parse(drTrans["TransAmt"].ToString()), 2).ToString();

                BindPCRQuestions(chkLegalReview.Checked);

                foreach (ListItem item in ddlMatchingGrant.Items)
                {
                    if (drPCR["LkFVGrantMatch"].ToString() == item.Value.ToString())
                    {
                        ddlMatchingGrant.ClearSelection();
                        item.Selected = true;
                    }
                }

                foreach (ListItem item in lbNOD.Items)
                {
                    foreach (DataRow dr in dtNOD.Rows)
                        if (dr["LKNOD"].ToString() == item.Value.ToString())
                            item.Selected = true;
                }
                foreach (ListItem item in lbItems.Items)
                {
                    foreach (DataRow dr in dtItems.Rows)
                        if (dr["LKCRItems"].ToString() == item.Value.ToString())
                            item.Selected = true;
                }

                #endregion


                chkLegalReview.Enabled = true;
                //CheckLegalReviewAccess();
                //chkLCB.Enabled = true;
                lbNOD.Enabled = true;
                txtNotes.Enabled = true;

                //pnlApprovals.Visible = false;
                //pnlDisbursement.Visible = false;
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: rdBtnSelect_CheckedChanged: " + ex.Message); lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Clear PCR form fields
        /// </summary>
        private void ClearPCRForm()
        {
            ddlApplicantName.Items.Clear();
            ddlPayee.Items.Clear();
            ddlProgram.Items.Clear();
            ddlStatus.Items.Clear();
            lblProjName.Text = "--";
            txtTransDate.Text = "";

            //EnableButton(btnCRSubmit);
            //chkLCB.Checked = false;
            chkLegalReview.Checked = false;

            if (txtEligibleAmt.Visible)
            {
                txtEligibleAmt.Text = "";
                if (ddlMatchingGrant.Items.Count > 0) ddlMatchingGrant.SelectedIndex = 0;
            }
            else
            {
                lblAmtEligibleForMatch.Visible = true;
                txtEligibleAmt.Visible = true;
                ddlMatchingGrant.Visible = true;
                lblMatchingGrant.Visible = true;

                txtEligibleAmt.Text = "";
                if (ddlMatchingGrant.Items.Count > 0) ddlMatchingGrant.SelectedIndex = 0;
            }
            ddlDate.Visible = false;
            txtTransDate.Visible = true;
            lbNOD.SelectedIndex = -1;
            lbItems.SelectedIndex = -1;
            txtNotes.Text = "";
            txtDisbursementAmt.Text = "";

            lblAvailFund.Text = "0.00";
            lblAvailVisibleFund.Text = "0.00";

            ckbACHActive.Checked = false;
            ckbACHActive.Visible = false;
            spnACHActive.Visible = false;
        }

        /// <summary>
        /// Clear PCR details
        /// </summary>
        private void ClearPCRDetails()
        {
            gvPTransDetails.DataSource = null;
            gvPTransDetails.DataBind();

            lbNOD.SelectedIndex = -1;
            ddlPCRQuestions.SelectedIndex = -1;

        }

        /// <summary>
        /// Enablin PCR
        /// </summary>
        private void EnablePCR()
        {
            ddlApplicantName.Enabled = true;
            txtTransDate.Enabled = true;
            ddlPayee.Enabled = true;
            ddlProgram.Enabled = true;
            ddlStatus.Enabled = false;
            lbNOD.Enabled = true;
            //chkLCB.Enabled = true;
            chkLegalReview.Enabled = true;

            if (txtEligibleAmt.Visible)
            {
                txtEligibleAmt.Enabled = true;
                ddlMatchingGrant.Enabled = true;
            }
            else
            {
                txtEligibleAmt.Enabled = true;
                ddlMatchingGrant.Enabled = true;

                txtEligibleAmt.Enabled = true;
                ddlMatchingGrant.Enabled = true;
            }
            txtNotes.Enabled = true;
            txtDisbursementAmt.Enabled = true;
        }

        /// <summary>
        /// Bind Applicant Name data
        /// </summary>
        /// <param name="ProjectId"></param>
        protected void BindApplicantName(int ProjectId)
        {
            try
            {
                ddlApplicantName.DataSource = null;
                ddlApplicantName.DataBind();

                DataTable dtApplicantname;
                dtApplicantname = new DataTable();
                dtApplicantname = ProjectCheckRequestData.GetApplicantName(ProjectId);

                ddlApplicantName.DataSource = dtApplicantname;
                ddlApplicantName.DataValueField = "Applicantname";
                ddlApplicantName.DataTextField = "Applicantname";
                ddlApplicantName.DataBind();
                //ddlApplicantName.Items.Insert(0, new ListItem("Select", "NA"));

                BindPayee(ProjectId);
                BindProgram(ProjectId);
                BindStatus();

                DataRow drProjectDetails = ProjectMaintenanceData.GetprojectDetails(ProjectId);
                CommonHelper.PopulateDropDown(ddlProgram, drProjectDetails["LkProgram"].ToString());
                if (ddlProgram.Items.Count > 1)
                    DisplayControls(ddlProgram.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

        /// <summary>
        /// Display controls based on roles
        /// </summary>
        /// <param name="SelectedText"></param>
        private void DisplayControls(string SelectedText)
        {
            lblAmtEligibleForMatch.Visible = false;
            txtEligibleAmt.Visible = false;
            ddlMatchingGrant.Visible = false;
            lblMatchingGrant.Visible = false;

            //if (SelectedText != "Viability")
            //{
            //    lblAmtEligibleForMatch.Visible = false;
            //    txtEligibleAmt.Visible = false;
            //    ddlMatchingGrant.Visible = false;
            //    lblMatchingGrant.Visible = false;
            //}
            //else
            //{
            //    lblAmtEligibleForMatch.Visible = true;
            //    txtEligibleAmt.Visible = true;
            //    ddlMatchingGrant.Visible = true;
            //    lblMatchingGrant.Visible = true;
            //}
        }

        /// <summary>
        /// Check and setting available funds
        /// </summary>
        private void SetAvailableFunds()
        {
            // DataRow dr = ProjectCheckRequestData.GetAvailableFundsByProject(DataUtils.GetInt(hfProjId.Value));
            DataTable dtAvailFunds = FinancialTransactions.GetAvailableFundAmountByProjectId(Convert.ToInt32(hfProjId.Value));

            if (dtAvailFunds != null && dtAvailFunds.Rows[0] != null)
            {
                DataRow dr = dtAvailFunds.Rows[0];

                if (dr["Balanced"].ToString() != "" && Convert.ToDecimal(dr["Balanced"].ToString()) > 0)
                {
                    lblAvailFund.Text = Convert.ToDecimal(dr["Balanced"].ToString()).ToString("#.##");
                    lblAvailVisibleFund.Text = CommonHelper.myDollarFormat(Convert.ToDecimal(dr["Balanced"].ToString()));
                    //.ToString("#.##");
                }
                else
                {
                    lblAvailFund.Text = "0.00";
                    lblAvailVisibleFund.Text = "0.00";
                }
            }
            else
            {
                lblAvailFund.Text = "0.00";
                lblAvailVisibleFund.Text = "0.00";
            }
        }

        /// <summary>
        /// Get Project ID
        /// </summary>
        /// <param name="ProjectNum"></param>
        /// <returns></returns>
        private int GetProjectID(string ProjectNum)
        {
            return ProjectMaintenanceData.GetProjectId(ProjectNum);
        }

        /// <summary>
        /// Bind payee data
        /// </summary>
        /// <param name="projId"></param>
        protected void BindPayee(int projId)
        {
            try
            {
                ddlPayee.DataSource = null;
                ddlPayee.DataBind();
                DataTable dtPayee = new DataTable();
                dtPayee = new DataTable();
                dtPayee = ProjectCheckRequestData.GetProjectFinLegalApplicant(projId);

                ddlPayee.DataSource = dtPayee;
                ddlPayee.DataValueField = "ApplicantId";
                ddlPayee.DataTextField = "Applicantname";
                ddlPayee.DataBind();
                if (ddlPayee.Items.Count > 1)
                    ddlPayee.Items.Insert(0, new ListItem("Select", "NA"));
                else
                    GetPayeeACHActiveVal();
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: BindPayee: " + ex.Message);
                lblErrorMsg.Focus();
            }

        }

        /// <summary>
        /// Bind Program
        /// </summary>
        /// <param name="projId"></param>
        protected void BindProgram(int projId)
        {
            try
            {
                ddlProgram.DataSource = null;
                ddlProgram.DataBind();
                DataTable dtProgram = new DataTable();
                dtProgram = new DataTable();
                dtProgram = ProjectCheckRequestData.PCR_Program(projId);

                ddlProgram.DataSource = dtProgram;
                ddlProgram.DataValueField = "typeid";
                ddlProgram.DataTextField = "Description";
                ddlProgram.DataBind();
                if (ddlProgram.Items.Count > 1)
                {
                    ddlProgram.Items.Insert(0, new ListItem("Select", "NA"));
                    DisplayControls(ddlProgram.SelectedItem.ToString());
                }
                else
                    DisplayControls("");
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }

        }

        /// <summary>
        /// Bind Status
        /// </summary>
        protected void BindStatus()
        {
            try
            {
                DataTable dtStatus = new DataTable();
                dtStatus = ProjectCheckRequestData.GetData("PCR_Trans_Status");

                ddlStatus.DataSource = dtStatus;
                ddlStatus.DataValueField = "typeid";
                ddlStatus.DataTextField = "Description";
                ddlStatus.DataBind();

            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: BindStatus: " + ex.Message);
                lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Bind Matching Grant data
        /// </summary>
        protected void BindMatchingGrant()
        {
            try
            {
                ddlMatchingGrant.DataSource = null;
                ddlMatchingGrant.DataBind();

                DataTable dtMatchingGrant;
                dtMatchingGrant = new DataTable();
                dtMatchingGrant = ProjectCheckRequestData.GetData("PCR_MatchingGrant");

                ddlMatchingGrant.DataSource = dtMatchingGrant;
                ddlMatchingGrant.DataValueField = "typeid";
                ddlMatchingGrant.DataTextField = "Description";
                ddlMatchingGrant.DataBind();
                ddlMatchingGrant.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Bind NOD Data dropdown
        /// </summary>
        protected void BindNODData()
        {
            try
            {
                DataTable dtNOD;
                dtNOD = new DataTable();
                dtNOD = ProjectCheckRequestData.GetData("PCR_NOD_Load");

                lbNOD.DataSource = dtNOD;
                lbNOD.DataValueField = "typeid";
                lbNOD.DataTextField = "Description";
                lbNOD.DataBind();
                //ddlMatchingGrant.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }
        }

        /// <summary>
        /// Clear hidden fields
        /// </summary>
        private void ClearHiddenFieldValues()
        {
            hfTransId.Value = "";
            hfTransAmt.Value = "";
            hfBalAmt.Value = "";
            hfPCRId.Value = "";
            hfProjName.Value = "";
            hfProjId.Value = "";
            hfAvFunds.Value = "";
            hfCreatedById.Value = "";
            hfIsAllApproved.Value = "false";
        }

        /// <summary>
        /// Bind PCR Item Data
        /// </summary>
        protected void BindPCRItemsData()
        {
            try
            {
                DataTable dtItems;
                dtItems = new DataTable();
                dtItems = ProjectCheckRequestData.GetData("PCR_Items_Load");

                lbItems.DataSource = dtItems;
                lbItems.DataValueField = "typeid";
                lbItems.DataTextField = "Description";
                lbItems.DataBind();
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }
        }

        protected void rdBtnSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdBtnNewExistChanged();
        }

        private void rdBtnNewExistChanged()
        {
            //pnlFund.Visible = false;
            pnlApprovals.Visible = false;
            pnlDisbursement.Visible = false;
            //BindProjects();
            ClearPCRForm();
            ddlDate.Visible = false;
            txtTransDate.Visible = true;
            ClearHiddenFieldValues();
            DisplayControls("");


            txtProjNum.Text = "";
            //txtCRDate.Text = "";
            ddlCRDate.SelectedIndex = -1;
            if (rdBtnSelect.SelectedIndex == 0)
            {
                EnablePCR();
                txtProjNum.Visible = true;
                btnCRSubmit.Visible = true;
                Response.Redirect("projectcheckrequestnew.aspx");
            }
            else
            {
                //BindExistingPCR();
                DisablePCR();
                //txtProjNum.Visible = false;
                btnCRSubmit.Visible = false;
                updateSpan.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkLegalReview_CheckedChanged(object sender, EventArgs e)
        {
            BindPCRQuestions(chkLegalReview.Checked);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCRSubmit_Click(object sender, EventArgs e)
        {
            string PCRID = this.hfPCRId.Value;

            string strEAmt = txtEligibleAmt.Text;
            string tmpE = Regex.Replace(strEAmt, "[^0-9a-zA-Z.]+", "");
            txtEligibleAmt.Text = tmpE.ToString();

            string strDAmt = txtDisbursementAmt.Text;
            string tmpD = Regex.Replace(strDAmt, "[^0-9a-zA-Z.]+", "");
            txtDisbursementAmt.Text = tmpD.ToString();

            #region Validations
            //if (ddlProjFilter.Items.Count > 1 && ddlProjFilter.SelectedIndex == 0)
            //{
            //    LogMessage("Select Project#";
            //    ddlProjFilter.Focus();
            //    return;
            //}
            if (txtProjNum.Text == "")
            {
                LogMessage("Select Project#");
                return;
            }
            if (txtTransDate.Text == "")
            {
                LogMessage("Select Transaction Date");
                txtTransDate.Focus();
                return;
            }

            if (ddlCRDate.SelectedIndex == 0)
            {
                LogMessage("Select Check Request Date");
                ddlCRDate.Focus();
                return;
            }

            //if (ddlCRDate.SelectedIndex == -1)
            //{
            //    LogMessage("Select Check Request Date");
            //    ddlCRDate.Focus();
            //    return;
            //}

            if (txtTransDate.Text.Trim() != "")
            {
                DateTime dt;
                bool isDateTime = DateTime.TryParse(txtTransDate.Text.Trim(), out dt);

                if (!isDateTime)
                {
                    LogMessage("Select a valid Transaction Date");
                    txtTransDate.Focus();
                    return;
                }
            }

            //if (txtCRDate.Text.Trim() != "")
            //{
            //    DateTime dt;
            //    bool isDateTime = DateTime.TryParse(txtCRDate.Text.Trim(), out dt);

            //    if (!isDateTime)
            //    {
            //        LogMessage("Select valid Check Request Date";
            //        txtCRDate.Focus();
            //        return;
            //    }
            //}

            if (ddlPayee.Items.Count > 1 && ddlPayee.SelectedIndex == 0)
            {
                LogMessage("Select Payee");
                ddlPayee.Focus();
                return;
            }
            if (ddlPayee.Items.Count == 0)
            {
                LogMessage("Add a payee to this project before proceed with disbursement");
                return;
            }
            if (ddlProgram.Items.Count > 1 && ddlProgram.SelectedIndex == 0)
            {
                LogMessage("Select Program");
                ddlProgram.Focus();
                return;
            }
            if (ddlProgram.Items.Count == 0)
            {
                LogMessage("Add a program to this project before proceed with disbursement");
                return;
            }
            //else if (ddlStatus.Items.Count > 1 && ddlStatus.SelectedIndex == 0)
            //{
            //    LogMessage("Select Status";
            //    ddlStatus.Focus();
            //    return;
            //}
            //else if (lbNOD.Items.Count > 1 && lbNOD.SelectedIndex == -1)
            //{
            //    LogMessage("Select NOD";
            //    lbNOD.Focus();
            //    return;
            //}

            if (txtEligibleAmt.Visible)
            {
                if (txtEligibleAmt.Text.Trim() == "")
                {
                    LogMessage("Select Eligible Amount");
                    txtEligibleAmt.Focus();
                    return;
                }
                if (txtEligibleAmt.Text.Trim() != "")
                {
                    decimal n;
                    bool isDecimal = decimal.TryParse(txtEligibleAmt.Text.Trim(), out n);

                    if (!isDecimal || Convert.ToDecimal(txtEligibleAmt.Text) <= 0)
                    {
                        LogMessage("Select a valid Eligible amount");
                        txtEligibleAmt.Focus();
                        return;
                    }
                }
            }

            if (txtEligibleAmt.Visible && ddlMatchingGrant.Items.Count > 1 && ddlMatchingGrant.SelectedIndex == 0)
            {
                LogMessage("Select Matching Grant");
                ddlMatchingGrant.Focus();
                return;
            }

            if (txtDisbursementAmt.Text.Trim() == "")
            {
                LogMessage("Select Disbursement Amount");
                txtDisbursementAmt.Focus();
                return;
            }
            if (txtDisbursementAmt.Text.Trim() != "")
            {
                decimal n;
                bool isDecimal = decimal.TryParse(txtDisbursementAmt.Text.Trim(), out n);

                if (!isDecimal || Convert.ToDecimal(txtDisbursementAmt.Text) <= 0)
                {
                    LogMessage("Select a valid Disbursement amount");
                    txtDisbursementAmt.Focus();
                    return;
                }
                bool availFunds = decimal.TryParse(lblAvailFund.Text.Trim(), out n);
                if (!availFunds || Convert.ToDecimal(txtDisbursementAmt.Text) > Convert.ToDecimal(lblAvailFund.Text))
                {
                    if (!availFunds)
                        LogMessage("Disbursement amount can't be more than available funds (" + CommonHelper.myDollarFormat(0) + ") for the selected project");
                    else
                        LogMessage("Disbursement amount can't be more than available funds (" + CommonHelper.myDollarFormat(lblAvailFund.Text) + ") for the selected project");

                    txtDisbursementAmt.Focus();
                    return;
                }
            }
            #endregion

            try
            {
                // string[] ProjectTokens = ddlProjFilter.SelectedValue.ToString().Split('|');
                string lbNODS = string.Empty;
                DateTime TransDate = DateTime.Parse(txtTransDate.Text);
                DateTime CRDate = DateTime.Parse(ddlCRDate.SelectedValue.ToString());

                int MatchingGrant = 0;
                decimal EligibleAmt = 0;

                if (txtEligibleAmt.Visible)
                {
                    MatchingGrant = int.Parse(ddlMatchingGrant.SelectedValue.ToString());
                    EligibleAmt = decimal.Parse(txtEligibleAmt.Text);
                }

                //foreach (ListItem listItem in lbNOD.Items)
                //{
                //    if (listItem.Selected == true)
                //    {
                //        if (lbNODS.Length == 0)
                //            lbNODS = listItem.Value;
                //        else
                //            lbNODS = lbNODS + "|" + listItem.Value;
                //    }
                //}

                DataTable dtPCR = new DataTable();
                PCRDetails pcr = new PCRDetails();

                if (PCRID == "")
                {
                    dtPCR = ProjectCheckRequestData.SubmitPCR(int.Parse(hfProjId.Value), TransDate, int.Parse(ddlProgram.SelectedValue.ToString()),
                        chkLegalReview.Checked, txtVendorId.Text, EligibleAmt, MatchingGrant,
                        decimal.Parse(txtDisbursementAmt.Text), ddlPayee.Items.Count > 0 ? int.Parse(ddlPayee.SelectedValue.ToString()) : 0, int.Parse(ddlStatus.SelectedValue.ToString()),
                        txtNotes.Text, GetUserId(), lbNODS, CRDate, ckbACHActive.Checked);

                    if (dtPCR.Rows.Count > 0)
                    {
                        pcr.TransID = Convert.ToInt32(dtPCR.Rows[0]["TransID"].ToString());
                        pcr.ProjectCheckReqID = Convert.ToInt32(dtPCR.Rows[0]["ProjectCheckReqId"].ToString());
                        pcr.pcrDetails = dtPCR.Rows[0]["pcq"].ToString();

                        foreach (ListItem listItem in lbNOD.Items)
                        {
                            if (listItem.Selected == true)
                            {
                                ProjectCheckRequestData.PCR_Submit_NOD(pcr.ProjectCheckReqID, Convert.ToInt32(listItem.Value));
                            }
                        }

                        foreach (ListItem listItem in lbItems.Items)
                        {
                            if (listItem.Selected == true)
                            {
                                ProjectCheckRequestData.pcr_submit_items(pcr.ProjectCheckReqID, Convert.ToInt32(listItem.Value));
                            }
                        }

                        //BindTransDate(dtPCR);
                    }
                    LogMessage("Successfully Saved Check Request");
                    rdBtnSelect.SelectedIndex = 1;
                    PopulatePCRForm();
                }
                else
                {
                    //Get PCR Disbursement Details Total
                    decimal TotalDisbursementDetail = ProjectCheckRequestData.GetPCRDisbursemetDetailTotal(int.Parse(PCRID));

                    if (decimal.Parse(txtDisbursementAmt.Text) >= TotalDisbursementDetail)
                    {
                        dtPCR = ProjectCheckRequestData.UpdatePCR(int.Parse(PCRID), int.Parse(hfProjId.Value), TransDate, int.Parse(ddlProgram.SelectedValue.ToString()),
                        chkLegalReview.Checked, txtVendorId.Text, EligibleAmt, MatchingGrant,
                        decimal.Parse(txtDisbursementAmt.Text), int.Parse(ddlPayee.SelectedValue.ToString()), int.Parse(ddlStatus.SelectedValue.ToString()),
                        txtNotes.Text, GetUserId(), lbNODS, CRDate, ckbACHActive.Checked);

                        hfTransAmt.Value = txtDisbursementAmt.Text;

                        if (dtPCR.Rows.Count > 0)
                        {
                            pcr.TransID = Convert.ToInt32(dtPCR.Rows[0]["TransID"].ToString());
                            pcr.ProjectCheckReqID = Convert.ToInt32(dtPCR.Rows[0]["ProjectCheckReqId"].ToString());
                            pcr.pcrDetails = dtPCR.Rows[0]["pcq"].ToString();
                            ProjectCheckRequestData.ClearNODAndItems(pcr.ProjectCheckReqID);

                            foreach (ListItem listItem in lbNOD.Items)
                            {
                                if (listItem.Selected == true)
                                {
                                    ProjectCheckRequestData.PCR_Submit_NOD(pcr.ProjectCheckReqID, Convert.ToInt32(listItem.Value));
                                }
                            }
                            foreach (ListItem listItem in lbItems.Items)
                            {
                                if (listItem.Selected == true)
                                {
                                    ProjectCheckRequestData.pcr_submit_items(pcr.ProjectCheckReqID, Convert.ToInt32(listItem.Value));
                                }
                            }
                            //BindTransDate(dtPCR);
                            BindPCRTransDetails();
                            LogMessage("Successfully Updated Check Request");
                        }
                        else
                        {
                            LogMessage("Disbursement value cannot be less than total disbursement detail " + TotalDisbursementDetail + " value");
                            txtDisbursementAmt.Focus();
                            return;
                        }
                    }

                    this.hfTransId.Value = pcr.TransID.ToString();
                    this.hfPCRId.Value = pcr.ProjectCheckReqID.ToString();
                    this.hfTransAmt.Value = txtDisbursementAmt.Text;

                    //DisablePCR();
                    //ClearPCRForm();
                    //ClearPCRDetails();
                    this.hfEditPCRId.Value = "";
                    pnlDisbursement.Visible = true;
                    //pnlApprovals.Visible = true;
                    //pnlFund.Visible = false;
                    ddlDate.Visible = false;
                    txtTransDate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: btnCRSubmit_Click: " + ex.Message); lblErrorMsg.Focus();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        private void DisablePCR()
        {
            // ddlProjFilter.Enabled = false;
            ddlApplicantName.Enabled = false;
            txtTransDate.Enabled = false;
            ddlPayee.Enabled = false;
            ddlProgram.Enabled = false;
            ddlStatus.Enabled = false;
            //DisableButton(btnCRSubmit);
            //chkLCB.Enabled = false;
            chkLegalReview.Enabled = false;
            lbNOD.Enabled = false;

            if (txtEligibleAmt.Visible)
            {
                txtEligibleAmt.Enabled = false;
                ddlMatchingGrant.Enabled = false;
            }
            else
            {
                txtEligibleAmt.Enabled = false;
                ddlMatchingGrant.Enabled = false;

                txtEligibleAmt.Enabled = false;
                ddlMatchingGrant.Enabled = false;
            }
            txtNotes.Enabled = false;
            //txtDisbursementAmt.Enabled = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetProjectsByFilter(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt = Project.GetProjects("GetProjectsByFilter", prefixText);

            List<string> ProjNames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProjNames.Add("'" + dt.Rows[i][0].ToString() + "'");
            }
            return ProjNames.ToArray();
        }

        private void LogError(string pagename, string method, string message, string error)
        {
            dvMessage.Visible = true;
            if (message == "")
            {
                LogMessage(Pagename + ": " + method + ": Error Message: " + error);
            }
            else
                LogMessage(Pagename + ": " + method + ": Message :" + message + ": Error Message: " + error);
        }

        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }

        public static void DisableButton(Button btn)
        {
            btn.Enabled = false;
            btn.CssClass = "btn btn-info";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        public static void EnableButton(Button btn)
        {
            btn.Enabled = true;
            btn.CssClass = "btn btn-info";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsLegal"></param>
        protected void BindPCRQuestions(bool IsLegal)
        {
            try
            {
                DataTable dtPCRQuestions;
                dtPCRQuestions = new DataTable();
                dtPCRQuestions = ProjectCheckRequestData.GetPCRQuestions(IsLegal);

                ddlPCRQuestions.DataSource = dtPCRQuestions;
                ddlPCRQuestions.DataValueField = "typeid";
                ddlPCRQuestions.DataTextField = "Description";
                ddlPCRQuestions.DataBind();
                ddlPCRQuestions.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                lblErrorMsg.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApprovalsSubmit_Click(object sender, EventArgs e)
        {
            if (ddlPCRQuestions.Items.Count > 1 && ddlPCRQuestions.SelectedIndex == 0)
            {
                LogMessage("Select PCR Question");
                ddlPCRQuestions.Focus();
                return;
            }

            ProjectCheckRequestData.SubmitPCRForm(int.Parse(this.hfPCRId.Value), int.Parse(ddlPCRQuestions.SelectedValue.ToString()), GetUserId());

            LogMessage("PCR Approvals Saved Successfully");

            ddlPCRQuestions.SelectedIndex = -1;

            BindPCRQuestionsForApproval();
        }
        /// <summary>
        /// 
        /// </summary>
        protected void BindPCRQuestionsForApproval()
        {
            pnlApprovals.Visible = true;

            try
            {
                DataTable dtPCRQuestionsForApproval = new DataTable();
                dtPCRQuestionsForApproval = ProjectCheckRequestData.GetDefaultPCRQuestions(chkLegalReview.Checked, int.Parse(this.hfPCRId.Value));

                gvQuestionsForApproval.DataSource = dtPCRQuestionsForApproval;
                gvQuestionsForApproval.DataBind();

                BindVoucher();

                int approvals = 0;
                DataTable dt = new DataTable();
                dt = ProjectCheckRequestData.GetQuestionsForApproval(int.Parse(this.hfPCRId.Value));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["approved"].ToString() == "True")
                    {
                        approvals += 1;
                    }
                }

                if (approvals != 0 && approvals == dt.Rows.Count)
                {
                    btnCRSubmit.Visible = false;
                    updateSpan.Visible = false;
                    //btnDelete.Visible = false;
                    btnPCRTransDetails.Visible = false;
                    btnApprovalsSubmit.Visible = false;
                    //ddlCRDate.Enabled = false;

                    CheckVoucherAccess();
                }
                else
                {
                    pnlVoucherDet.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: BindPCRQuestionsForApproval: " + ex.Message); lblErrorMsg.Focus();
            }
            finally
            {
                //if (btnNewPCR.Visible == false)
                //{
                //    btnNewPCR.Visible = true;
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CheckVoucherAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "26807")
                {
                    pnlVoucherDet.Visible = true;
                    btnAddVoucher.Visible = true;
                }
            }
        }

        #region gvQuestionsForApproval
        protected void gvQuestionsForApproval_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuestionsForApproval.EditIndex = -1;
            BindPCRQuestionsForApproval();
        }

        protected void gvQuestionsForApproval_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuestionsForApproval.EditIndex = e.NewEditIndex;
            BindPCRQuestionsForApproval();
            //if (btnNewPCR.Visible == true)
            //{
            //    btnNewPCR.Visible = false;
            //}
        }

        protected void gvQuestionsForApproval_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                bool isPC = false;

                bool isApproved = Convert.ToBoolean(((CheckBox)gvQuestionsForApproval.Rows[rowIndex].FindControl("cbApproved")).Checked);


                int ProjectCheckReqQuestionid = Convert.ToInt32(((HiddenField)gvQuestionsForApproval.Rows[rowIndex].FindControl("hfProjectCheckReqQuestionID")).Value);
                int lkPCRQId = Convert.ToInt32(((HiddenField)gvQuestionsForApproval.Rows[rowIndex].FindControl("hfLKPCRQId")).Value);

                if(lkPCRQId ==7)
                {
                    isPC = Convert.ToBoolean(((CheckBox)gvQuestionsForApproval.Rows[rowIndex].FindControl("cbPC")).Checked);
                }

                if (!isApproved)
                {
                    ProjectCheckRequestData.UpdatePCRQuestionsApproval(ProjectCheckReqQuestionid, isApproved, isPC,
                        GetUserId());
                    return;
                }

                DataTable dtPCRQuestionsForApproval = new DataTable();
                dtPCRQuestionsForApproval = ProjectCheckRequestData.GetDefaultPCRQuestions(chkLegalReview.Checked, int.Parse(this.hfPCRId.Value));


                int approvals = 0, approvedUser = 0;
                foreach (DataRow row in dtPCRQuestionsForApproval.Rows)
                {
                    if (row["LkPCRQuestionsID"].ToString() == "3" || row["LkPCRQuestionsID"].ToString() == "5")
                    {
                        if (row["approved"].ToString() == "Yes")
                        {
                            approvals += 1;
                            if (row["userid"].ToString() != "")
                                approvedUser = Convert.ToInt32(row["userid"].ToString());
                        }
                    }
                }

                foreach (DataRow row in dtPCRQuestionsForApproval.Rows)
                {
                    if ((row["LkPCRQuestionsID"].ToString() == "3" || row["LkPCRQuestionsID"].ToString() == "5") && (row["LkPCRQuestionsID"].ToString() == lkPCRQId.ToString()))
                        if (row["userid"].ToString() != "")
                            if (approvedUser == GetUserId() && row["Approved"].ToString() == "No" && approvals == 1)
                            {
                                LogMessage("You cannot approve BOTH the 1st and 2nd questions");
                                return;
                            }
                }

                if (isApproved)
                    ProjectCheckRequestData.UpdatePCRQuestionsApproval(ProjectCheckReqQuestionid, isApproved, isPC, GetUserId());


                //if (((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim() != "")
                //{
                //    decimal n;
                //    bool isDecimal = decimal.TryParse(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim(), out n);

                //    if (!isDecimal || Convert.ToDecimal(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text.Trim()) <= 0)
                //    {
                //        LogMessage("Select a valid transaction amount";
                //        ((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Focus();
                //        return;
                //    }
                //}

                //decimal amount = Convert.ToDecimal(((TextBox)gvPTransDetails.Rows[rowIndex].FindControl("txtAmount")).Text);
                //int transType = Convert.ToInt32(((DropDownList)gvPTransDetails.Rows[rowIndex].FindControl("ddlTransType")).SelectedValue.ToString());
                //int detailId = Convert.ToInt32(((Label)gvPTransDetails.Rows[rowIndex].FindControl("lblDetId")).Text);

                //decimal old_amount = Convert.ToDecimal(FinancialTransactions.GetTransDetails(detailId).Rows[0]["Amount"].ToString());
                //decimal bal_amount = Convert.ToDecimal(hfBalAmt.Value);
                //decimal allowed_amount = old_amount + bal_amount;

                //if (amount == allowed_amount)
                //{
                //    LogMessage("Transaction is complete, more funds not allowed";
                //}
                //else if (amount > allowed_amount)
                //{
                //    amount = allowed_amount;
                //    LogMessage("Amount auto adjusted to available fund amount";
                //}
                //else if (amount < allowed_amount)
                //{
                //    if (!btnPCRTransDetails.Enabled)
                //    {
                //        EnableButton(btnPCRTransDetails);
                //        DisableButton(btnSubmit);
                //    }
                //}
                //FinancialTransactions.UpdateTransDetails(detailId, transType, amount);
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: gvPTransDetails_RowUpdating: " + ex.Message);
                lblErrorMsg.Focus();
            }
            finally
            {
                gvQuestionsForApproval.EditIndex = -1;
                BindPCRQuestionsForApproval();
            }
        }

        protected void gvQuestionsForApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                //Checking whether the Row is Data Row
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbApproved = (e.Row.FindControl("cbApproved") as CheckBox);
                    Label lblApproved = (e.Row.FindControl("lbleditApproved") as Label);

                    if (lblApproved.Text != "")
                    {
                        if (cbApproved != null)

                            if (lblApproved.Text.ToLower() == "no")
                                cbApproved.Checked = false;
                            else
                                cbApproved.Checked = true;
                    }

                    CheckBox cbPC = (e.Row.FindControl("cbPC") as CheckBox);
                    HiddenField hfLKPCRQId = (e.Row.FindControl("hfLKPCRQId") as HiddenField);

                    if (hfLKPCRQId.Value == "7" && CheckFxnAccess("27471"))
                    {
                        cbPC.Visible = true;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkButton = (e.Row.FindControl("lbEdit") as LinkButton);
                HiddenField hfRowNumber = (e.Row.FindControl("hfRowNumber") as HiddenField);
                HiddenField hfLKPCRQId = (e.Row.FindControl("hfLKPCRQId") as HiddenField);
               
                if (hfLKPCRQId != null && linkButton != null && linkButton.Visible)
                {
                    if (DataUtils.GetBool(hfIsAdmin.Value))
                        linkButton.Visible = true;
                    else if (hfLKPCRQId.Value == "3")
                    {
                        if (GetUserId().ToString() == hfCreatedById.Value && CheckFxnAccess("27453") && CheckFxnAccess("27454") && CheckFxnAccess("30926"))
                            linkButton.Visible = false;
                        else if (GetUserId().ToString() == hfCreatedById.Value || DataUtils.GetBool(hfIsVisibleBasedOnRole.Value))
                            linkButton.Visible = true;
                    }
                    else if (hfLKPCRQId.Value == "5" && CheckFxnAccess("26820"))
                        linkButton.Visible = true;
                    else if (hfLKPCRQId.Value == "7" && CheckFxnAccess("27471"))
                        linkButton.Visible = true;
                    //else if (hfLKPCRQId.Value == "8" && GetUserId().ToString() != hfCreatedById.Value
                    //    && DataUtils.GetBool(hfIsVisibleBasedOnRole.Value))
                    //    linkButton.Visible = true;
                    else if (hfLKPCRQId.Value == "8" && (DataUtils.GetBool(hfIsprgramAdmin.Value) || CheckFxnAccess("27645")))
                        linkButton.Visible = true;
                    else
                        linkButton.Visible = false;
                }

                //if (linkButton != null && linkButton.Visible)
                //{
                //    if (hfRowNumber.Value == "1" && GetUserId().ToString() == hfCreatedById.Value)
                //        linkButton.Visible = true;
                //    else if (hfRowNumber.Value == "2" && CheckFxnAccess("26820"))
                //        linkButton.Visible = true;
                //    else if (hfRowNumber.Value == "3" && CheckFxnAccess("26821"))
                //        linkButton.Visible = true;
                //    else
                //        linkButton.Visible = false;
                //}

            }
        }
        #endregion

        protected void btnAddVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVoucherDt.Text.Trim() != "")
                {
                    DateTime dtime;
                    bool isDateTime = DateTime.TryParse(txtVoucherDt.Text.Trim(), out dtime);

                    if (!isDateTime)
                    {
                        LogMessage("Select valid Check Request Date");
                        txtVoucherDt.Focus();
                        return;
                    }
                    else if (dtime > DateTime.Today)
                    {
                        LogMessage("You cannot select a day greater than today!");
                        txtVoucherDt.Focus();
                        return;
                    }
                }

                DataTable dtVouchers = new DataTable();
                dtVouchers = ProjectCheckRequestData.UpdateVoucherNumber(int.Parse(this.hfPCRId.Value), txtVoucher.Text, Convert.ToDateTime(txtVoucherDt.Text), GetUserId());

                BindVoucher();

                LogMessage("Check Request Finalized Successfully");

                btnCRSubmit.Visible = false;
                updateSpan.Visible = false;
                btnPCRTransDetails.Visible = false;
                btnApprovalsSubmit.Visible = false;
                btnAddVoucher.Visible = false;
                btnDelete.Visible = false;

                btnNewPCR.Visible = true;

                foreach (ListItem item in ddlStatus.Items)
                {
                    if (item.Value.ToString() == "262")
                    {
                        ddlStatus.ClearSelection();
                        item.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: Add voucher: " + ex.Message);
                lblErrorMsg.Focus();
            }
        }

        protected void BindVoucher()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ProjectCheckRequestData.GetVoucherDet(int.Parse(this.hfPCRId.Value));

                if (dt.Rows.Count > 0)
                {
                    gvVoucher.Visible = true;
                    gvVoucher.DataSource = dt;
                    gvVoucher.DataBind();
                }
                else
                {
                    gvVoucher.Visible = false;
                    gvVoucher.DataSource = null;
                    gvVoucher.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogMessage("ProjectCheckRequest: Bind voucher: " + ex.Message); lblErrorMsg.Focus();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPCR_Click(object sender, EventArgs e)
        {
            Response.Redirect("projectcheckrequestnew.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectCheckRequestData.PCR_Delete(Convert.ToInt32(hfPCRId.Value));

                rdBtnSelect.SelectedIndex = 0;
                pnlApprovals.Visible = false;
                pnlDisbursement.Visible = false;
                ClearPCRForm();
                ddlDate.Visible = false;
                txtTransDate.Visible = true;
                ClearHiddenFieldValues();
                DisplayControls("");
                txtProjNum.Text = "";
                ddlCRDate.SelectedIndex = -1;

                EnablePCR();
                txtProjNum.Visible = true;
                btnCRSubmit.Visible = true;

                LogMessage("Project check request was successfully deleted");
                lblErrorMsg.Focus();
            }
            catch (Exception ex)
            {
                LogMessage("btnDelete_Click" + ex.Message);
                lblErrorMsg.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CheckDeletePCRAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "26816")
                {
                    btnDelete.Visible = true;
                    updateSpan.Visible = false;
                }
                if (row["FxnID"].ToString() == "26820")
                    hfSecondQuestionAccess.Value = "true";

                if (row["FxnID"].ToString() == "26821")
                    hfLegalQuestionAccess.Value = "true";
            }

            if (!btnDelete.Visible)
            {
                if (GetUserId().ToString() == hfCreatedById.Value)
                    btnDelete.Visible = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FxnID"></param>
        /// <returns></returns>
        private bool CheckFxnAccess(string FxnID)
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == FxnID)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCRDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PCRID = this.hfPCRId.Value;
            DateTime CRDate = DateTime.Parse(ddlCRDate.SelectedValue.ToString());

            if (PCRID != null && PCRID != "" && int.Parse(PCRID) != 0)
                ProjectCheckRequestData.PCR_Update_CheckReqDate(int.Parse(PCRID), CRDate);
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetAvailFundsByProjectAccountPermitTransType()
        {
            string account = FinancialTransactions.GetAccountNumberByFundId(DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()));

            DataTable dtAvailFunds = FinancialTransactions.GetAvailableFundsPerProjAcctFundtype(Convert.ToInt32(hfProjId.Value),
                 account, Convert.ToInt32(ddlTransType.SelectedValue.ToString()),
                 ddlUsePermit.SelectedValue.ToString() == "NA" ? "" : ddlUsePermit.SelectedValue.ToString());

            if (dtAvailFunds != null && dtAvailFunds.Rows.Count > 0)
            {
                hfAvFunds.Value = dtAvailFunds.Rows[0]["availFunds"].ToString();
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat(dtAvailFunds.Rows[0]["availFunds"].ToString());
            }
            else
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool CheckIsVisible()
        {

            return !DataUtils.GetBool(hfIsAllApproved.Value) && !CheckCheckRequestApprovalAccess();
        }
        private bool CheckCheckRequestApprovalAccess()
        {
            DataTable dt = new DataTable();
            dt = UserSecurityData.GetUserFxnSecurity(GetUserId());

            foreach (DataRow row in dt.Rows)
            {
                if (row["FxnID"].ToString() == "39526") // Do NOT Allow Check Request Approval” - TypeID=39526
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Print PCR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgPrintPCR_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                   "script", Helper.GetExagoURLForPCR(hfPCRId.Value, "Check_Request_Final"));
        }

        /// <summary>
        /// Available funds data set to label
        /// </summary>
        private void SetAvailableFundsLabel()
        {
            hfAvFunds.Value = "0";
            decimal currentTransAmount = 0;

            DataTable dtAvailFunds = FinancialTransactions.GetAvailableFundAmount(Convert.ToInt32(hfProjId.Value),
                                 DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()),
                                 DataUtils.GetInt(ddlTransType.SelectedValue.ToString()),
                                 ddlUsePermit.SelectedValue.ToString());

            DataTable dtCurrentTransAmount = FinancialTransactions.GetCurrentTranAvailableFundAmount(Convert.ToInt32(hfTransId.Value),
                                DataUtils.GetInt(ddlFundTypeCommitments.SelectedValue.ToString()),
                                DataUtils.GetInt(ddlTransType.SelectedValue.ToString()),
                                ddlUsePermit.SelectedValue.ToString());

            if (dtCurrentTransAmount != null && dtCurrentTransAmount.Rows.Count > 0)
                currentTransAmount = DataUtils.GetDecimal(dtCurrentTransAmount.Rows[0]["amount"].ToString());

            if (dtAvailFunds != null && dtAvailFunds.Rows.Count > 0)
            {
                decimal AvailFunds = DataUtils.GetDecimal(dtAvailFunds.Rows[0]["Balanced"].ToString());
                decimal CurrentAvailFunds = AvailFunds - currentTransAmount;

                hfAvFunds.Value = CurrentAvailFunds.ToString(); // dtAvailFunds.Rows[0]["Balanced"].ToString();
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat(CurrentAvailFunds.ToString());
            }
            else
                lblCommittedAvailFunds.Text = CommonHelper.myDollarFormat("0.00");
        }

        protected void ddlPayee_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPayeeACHActiveVal();
        }

        private void GetPayeeACHActiveVal()
        {
            spnACHActive.Visible = false;
            ckbACHActive.Visible = false;

            if (ddlPayee.SelectedValue != "NA")
            {
                bool IsACHActive = ProjectCheckRequestData.GetACHActiveVal(int.Parse(ddlPayee.SelectedValue.ToString()));
                if (IsACHActive)
                {
                    spnACHActive.Visible = true;
                    ckbACHActive.Visible = true;
                }
            }
        }
    }
}