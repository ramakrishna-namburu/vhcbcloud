﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class CashRefund : System.Web.UI.Page
    {
        DataTable dtProjects;
        private int BOARD_REFUND = 237;
        
        private int TRANS_PENDING_STATUS = 261;
        private int ActiveOnly = 1;
        private string strLandUsePermit = "148";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtProjNum.Focus();
            }
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetProjectsByFilter(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt = Project.GetProjects("getCommittedCashRefundProjectslistByFilter", prefixText);
            //dt = Project.GetProjects("GetProjectsByFilter", prefixText);

            List<string> ProjNames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProjNames.Add("'" + dt.Rows[i][0].ToString() + "'");
            }
            return ProjNames.ToArray();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetCommittedPendingProjectslistByFilter(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            dt = Project.GetProjects("getCommittedPendingCashRefundProjectslistByFilter", prefixText);

            List<string> ProjNames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProjNames.Add("'" + dt.Rows[i][0].ToString() + "'");
            }
            return ProjNames.ToArray();
        }

        protected void BindUsePermit(int projId)
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = FinancialTransactions.GetLandUsePermit(projId);
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

        protected void BindFundAccounts()
        {
            try
            {
                ddlAcctNum.DataSource = null;
                ddlAcctNum.DataBind();
                ddlFundName.DataSource = null;
                ddlFundName.DataBind();

                DataTable dtable = new DataTable();

                dtable = FinancialTransactions.GetCommittedCRFundAccounts(Convert.ToInt32(hfProjId.Value));
                ddlAcctNum.DataSource = dtable;
                ddlAcctNum.DataValueField = "fundid";
                ddlAcctNum.DataTextField = "account";
                ddlAcctNum.DataBind();
                ddlAcctNum.Items.Insert(0, new ListItem("Select", "NA"));

                dtable = new DataTable();
                dtable = FinancialTransactions.GetCommittedCRFundNames(Convert.ToInt32(hfProjId.Value));
                ddlFundName.DataSource = dtable;
                ddlFundName.DataValueField = "fundid";
                ddlFundName.DataTextField = "name";
                ddlFundName.DataBind();
                ddlFundName.Items.Insert(0, new ListItem("Select", "NA"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlFundName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            if (ddlFundName.SelectedIndex != 0)
            {
                dtable = FinancialTransactions.GetFundDetailsByFundId(Convert.ToInt32(ddlFundName.SelectedValue.ToString()));
                lblFundName.Text = dtable.Rows[0]["name"].ToString();

                ddlAcctNum.SelectedValue = ddlFundName.SelectedValue;

                ddlTransType.DataSource = FinancialTransactions.GetAvailableTransTypesPerProjAcct(Convert.ToInt32(hfProjId.Value), ddlAcctNum.SelectedItem.Text);
                ddlTransType.DataValueField = "typeid";
                ddlTransType.DataTextField = "fundtype";
                ddlTransType.DataBind();
                if (ddlTransType.Items.Count > 1)
                    ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));

                BindUsePermit(Convert.ToInt32(hfProjId.Value));

                //if (ddlFundName.SelectedValue.ToString() == strLandUsePermit)
                if (dtable.Rows[0]["mitfund"].ToString().ToLower() == "true")
                {
                    lblUsePermit.Visible = true;
                    ddlUsePermit.Visible = true;
                }
                else
                {
                    lblUsePermit.Visible = false;
                    ddlUsePermit.Visible = false;
                }
            }
            else
            {
                ddlTransType.Items.Clear();
                ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));
                lblFundName.Text = "";
                txtAmt.Text = "";
                //ClearDetailSelection();
            }
        }

        protected void ddlAcctNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            if (ddlAcctNum.SelectedIndex != 0)
            {
                dtable = FinancialTransactions.GetFundDetailsByFundId(Convert.ToInt32(ddlAcctNum.SelectedValue.ToString()));
                lblFundName.Text = dtable.Rows[0]["name"].ToString();

                ddlFundName.SelectedValue = ddlAcctNum.SelectedValue;

                ddlTransType.DataSource = FinancialTransactions.GetAvailableTransTypesPerProjAcct(Convert.ToInt32(hfProjId.Value), ddlAcctNum.SelectedItem.Text);
                ddlTransType.DataValueField = "typeid";
                ddlTransType.DataTextField = "fundtype";
                ddlTransType.DataBind();
                if (ddlTransType.Items.Count > 1)
                    ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));

                BindUsePermit(Convert.ToInt32(hfProjId.Value));

                //if (ddlAcctNum.SelectedValue.ToString() == strLandUsePermit)
                if (dtable.Rows[0]["mitfund"].ToString().ToLower() == "true")
                {
                    lblUsePermit.Visible = true;
                    ddlUsePermit.Visible = true;
                }
                else
                {
                    lblUsePermit.Visible = false;
                    ddlUsePermit.Visible = false;
                }
            }
            else
            {
                ddlTransType.Items.Clear();
                ddlTransType.Items.Insert(0, new ListItem("Select", "NA"));
                lblFundName.Text = "";
                txtAmt.Text = "";
                //ClearDetailSelection();
            }
        }

        private void ClearTransactionDetailForm()
        {
            lblFundName.Text = "";
            txtAmt.Text = "";
            lblUsePermit.Visible = false;
            ddlUsePermit.Visible = false;
            try
            {
                ddlTransType.SelectedIndex = 0;
                ddlAcctNum.SelectedIndex = 0;
                ddlFundName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void BindFundDetails(int transId)
        {
            try
            {
                if (cbActiveOnly.Checked)
                    ActiveOnly = 1;
                else
                    ActiveOnly = 0;

                DataTable dtFundDet = new DataTable();
                dtFundDet = FinancialTransactions.GetCommitmentFundDetailsByProjectId(transId, BOARD_REFUND, ActiveOnly);

                gvBCommit.DataSource = dtFundDet;
                gvBCommit.DataBind();

                decimal tranAmount = 0;
                decimal totFundAmt = 0;
                decimal totBalAmt = 0;

                if (dtFundDet.Rows.Count > 0)
                {
                    //tranAmount = Convert.ToDecimal(dtFundDet.Rows[0]["TransAmt"].ToString());
                    tranAmount = Convert.ToDecimal(this.hfTransAmt.Value);

                    Label lblTotAmt = (Label)gvBCommit.FooterRow.FindControl("lblFooterAmount");
                    Label lblBalAmt = (Label)gvBCommit.FooterRow.FindControl("lblFooterBalance");

                    if (dtFundDet.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtFundDet.Rows.Count; i++)
                        {
                            totFundAmt += Convert.ToDecimal(dtFundDet.Rows[i]["Amount"].ToString());
                        }
                    }

                    totBalAmt = tranAmount + totFundAmt;

                    hfBalAmt.Value = (-totBalAmt).ToString();

                    lblTotAmt.Text = CommonHelper.myDollarFormat(totFundAmt);
                    lblBalAmt.Text = CommonHelper.myDollarFormat(totBalAmt);

                    gvBCommit.Columns[0].Visible = false;
                    gvBCommit.FooterRow.Visible = true;
                    lblTransDetHeader.Text = "Transaction Detail";

                    if (totBalAmt == 0)
                    {
                        tblFundDetails.Visible = false;
                        btnCashRefundSubmit.Visible = false;
                        CommonHelper.DisableButton(btnCashRefundSubmit);
                        CommonHelper.DisableButton(btnTransactionSubmit);
                        btnNewTransaction.Visible = true;
                        if (rdBtnSelection.SelectedIndex == 0)
                        {
                            lblProjName.Text = "";
                            lblGrantee.Text = "";
                        }

                    }
                    else
                    {
                        btnCashRefundSubmit.Visible = true;
                        tblFundDetails.Visible = true;
                        CommonHelper.DisableButton(btnTransactionSubmit);
                        CommonHelper.EnableButton(btnCashRefundSubmit);
                        btnNewTransaction.Visible = false;
                    }
                    if (lblBalAmt.Text != "$0.00")
                    {
                        lblErrorMsg.Text = "The transaction balance amount must be zero prior to leaving this page";
                        btnNewTransaction.Visible = false;
                    }
                    else if (lblBalAmt.Text == "$0.00")
                    {
                        btnNewTransaction.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
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

        private int GetTransId()
        {
            if (hfTransId.Value.ToString() == "")
            {
                return 0;
            }
            else
                return Convert.ToInt32(hfTransId.Value);
        }

        protected void rdBtnSelectTransDetail_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void lbAwardSummary_Click(object sender, EventArgs e)
        {
            if (hfProjId.Value != "")
            {
                string url = "/awardsummary.aspx?projectid=" + hfProjId.Value;
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(),
                        "script", sb.ToString());
            }
        }

        protected void btnCashRefundSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string str = txtAmt.Text;
                string tmp = Regex.Replace(str, "[^0-9a-zA-Z.]+", "");
                txtAmt.Text = tmp.ToString();

                btnNewTransaction.Visible = false;
                lblErrorMsg.Text = "";

                if (ddlAcctNum.Items.Count > 1 && ddlAcctNum.SelectedIndex == 0)
                {
                    lblErrorMsg.Text = "Select Account to add new transaction detail";
                    ddlAcctNum.Focus();
                    return;
                }
                else if (ddlTransType.Items.Count > 1 && ddlTransType.SelectedIndex == 0)
                {
                    lblErrorMsg.Text = "Select a Transaction type";
                    ddlTransType.Focus();
                    return;
                }
                else if (txtAmt.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Select a valid transaction amount";
                    txtAmt.Focus();
                    return;
                }
                else if (txtAmt.Text.Trim() != "")
                {
                    decimal n;
                    bool isNumeric = decimal.TryParse(txtAmt.Text.Trim(), out n);

                    if (!isNumeric || Convert.ToDecimal(txtAmt.Text) <= 0)
                    {
                        lblErrorMsg.Text = "Select a valid transaction amount";
                        txtAmt.Focus();
                        return;
                    }
                }
                DataTable dtAvailFunds = FinancialTransactions.GetAvailableFundsPerProjAcctFundtype(Convert.ToInt32(hfProjId.Value), ddlAcctNum.SelectedItem.Text, Convert.ToInt32(ddlTransType.SelectedValue.ToString()));
                if (dtAvailFunds != null)
                    if (dtAvailFunds.Rows.Count > 0)
                        if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(dtAvailFunds.Rows[0]["availFunds"].ToString()))
                        {
                            lblErrorMsg.Text = "Detail amount can not be more than available funds : " + CommonHelper.myDollarFormat(dtAvailFunds.Rows[0]["availFunds"].ToString()) + " for the selected Fund";
                            return;
                        }

                decimal currentTranAmount = 0;
                decimal currentTranFudAmount = 0;
                decimal currentBalAmount = 0;

                currentTranAmount = Convert.ToDecimal(hfTransAmt.Value);
                currentTranFudAmount = Convert.ToDecimal(txtAmt.Text);
                currentBalAmount = Convert.ToDecimal(hfBalAmt.Value);

                hfTransId.Value = GetTransId().ToString();
                if (hfTransId.Value != null)
                {
                    int transId = Convert.ToInt32(hfTransId.Value);

                    if (currentBalAmount == 0 && gvBCommit.Rows.Count > 0)
                    {
                        lblErrorMsg.Text = "This transaction details are all set. No more funds allowed to add for the transaction.";
                        ClearTransactionDetailForm();
                        CommonHelper.DisableButton(btnCashRefundSubmit);

                        return;
                    }
                    else if (currentTranFudAmount > currentBalAmount)
                    {
                        //currentTranFudAmount = currentBalAmount;
                        //lblErrorMsg.Text = "Amount auto adjusted to available fund amount";

                        lblErrorMsg.Text = "Amount entered is more than the available balance amount. Please enter available funds.";
                        return;
                    }

                    if (FinancialTransactions.IsDuplicateFundDetailPerTransaction(transId, Convert.ToInt32(ddlAcctNum.SelectedValue.ToString()),
                        Convert.ToInt32(ddlTransType.SelectedValue.ToString())))
                    {
                        lblErrorMsg.Text = "Same fund and same transaction type is already submitted for this transaction. Please select different selection";
                        return;
                    }

                    DataTable dtable = FinancialTransactions.GetFundDetailsByFundId(Convert.ToInt32(ddlFundName.SelectedValue.ToString()));
                    //if (ddlAcctNum.SelectedValue.ToString() == strLandUsePermit)

                    if (dtable.Rows[0]["mitfund"].ToString().ToLower() == "true")
                    {
                        if (ddlUsePermit.Items.Count > 1 && ddlUsePermit.SelectedIndex == 0)
                        {
                            lblErrorMsg.Text = "Select Use Permit";
                            ddlUsePermit.Focus();
                            return;
                        }

                        FinancialTransactions.AddProjectFundDetails(DataUtils.GetInt(hfProjId.Value), transId, Convert.ToInt32(ddlAcctNum.SelectedValue.ToString()),
                        Convert.ToInt32(ddlTransType.SelectedValue.ToString()), currentTranFudAmount, ddlUsePermit.SelectedItem.Text, ddlUsePermit.SelectedValue.ToString());
                    }
                    else
                        FinancialTransactions.AddProjectFundDetails(DataUtils.GetInt(hfProjId.Value), transId, Convert.ToInt32(ddlAcctNum.SelectedValue.ToString()),
                            Convert.ToInt32(ddlTransType.SelectedValue.ToString()), currentTranFudAmount);

                    BindFundDetails(transId);
                    ClearTransactionDetailForm();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }


        protected void btnTransactionSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string str = txtTotAmt.Text;
                string tmp = Regex.Replace(str, "[^0-9a-zA-Z.]+", "");
                txtTotAmt.Text = tmp.ToString();

                if (txtProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Select Project to add new transaction";
                    txtProjNum.Focus();
                    return;
                }
                else if (txtTotAmt.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Select a valid transaction amount";
                    txtTotAmt.Focus();
                    return;
                }
                decimal n;
                bool isNumeric = decimal.TryParse(txtTotAmt.Text.Trim(), out n);
                if (!isNumeric)
                {
                    lblErrorMsg.Text = "Must enter numbers in Total Amount";
                    return;
                }
                if (Convert.ToDecimal(txtTotAmt.Text.Trim()) <= 0)
                {
                    lblErrorMsg.Text = "Select a valid transaction amount";
                    return;
                }
                //DataTable dtCommitFund = FinancialTransactions.GetCommittedFundPerProject(txtProjNum.Text);
                //if (dtCommitFund != null)
                //    if (dtCommitFund.Rows.Count > 0)
                //        if (Convert.ToDecimal(dtCommitFund.Rows[0]["availFunds"].ToString()) < Convert.ToDecimal(txtTotAmt.Text.Trim()))
                //        {
                //            lblErrorMsg.Text = "Cash Refund amount can not be more than available funds : " + CommonHelper.myDollarFormat(dtCommitFund.Rows[0]["availFunds"].ToString()) + " for the selected project";
                //            return;
                //        }

                if (txtTotAmt.Text.Trim() != "")
                {
                    bool isDecimal = decimal.TryParse(txtTotAmt.Text.Trim(), out n);

                    if (!isDecimal || Convert.ToDecimal(txtTotAmt.Text) <= 0)
                    {
                        lblErrorMsg.Text = "Select a valid cash refund amount";
                        txtTotAmt.Focus();
                        return;
                    }
                    bool availFunds = decimal.TryParse(lblAvailFund.Text.Trim(), out n);
                    if (!availFunds || Convert.ToDecimal(txtTotAmt.Text) > Convert.ToDecimal(lblAvailFund.Text))
                    {
                        if (!availFunds)
                            lblErrorMsg.Text = "Cash refund amount can't be more than available funds (" + CommonHelper.myDollarFormat(0) + ") for the selected project";
                        else
                            lblErrorMsg.Text = "Cash refund amount can't be more than available funds (" + CommonHelper.myDollarFormat(lblAvailFund.Text) + ") for the selected project";

                        txtTotAmt.Focus();
                        return;
                    }
                }

                lblErrorMsg.Text = "";
                decimal TransAmount = Convert.ToDecimal(txtTotAmt.Text);

                this.hfTransAmt.Value = TransAmount.ToString();
                this.hfBalAmt.Value = TransAmount.ToString();

                gvBCommit.DataSource = null;
                gvBCommit.DataBind();

                pnlTranDetails.Visible = true;
                ClearTransactionDetailForm();

                int? granteeId = null;
                if (hfGrantee.Value != "")
                    granteeId = Convert.ToInt32(hfGrantee.Value);

                DataTable dtTrans = FinancialTransactions.AddBoardFinancialTransaction(Convert.ToInt32(hfProjId.Value), Convert.ToDateTime(txtTransDate.Text),
                    TransAmount, granteeId, "Cash Refund", GetUserId(),
                    TRANS_PENDING_STATUS);

                hfTransId.Value = dtTrans.Rows[0]["transid"].ToString();
                BindTransGrid(GetTransId());
                txtTransDate.Text = DateTime.Now.ToShortDateString();
                txtTotAmt.Text = "";

                CommonHelper.EnableButton(btnCashRefundSubmit);
                CommonHelper.DisableButton(btnTransactionSubmit);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
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

        private void BindTransGrid(int TransId)
        {
            try
            {
                if (cbActiveOnly.Checked)
                    ActiveOnly = 1;
                else
                    ActiveOnly = 0;
                gvPTrans.DataSource = null;
                gvPTrans.DataBind();

                DataTable dtTrans = null;
                if (rdBtnSelection.SelectedIndex == 0)
                {
                    dtTrans = FinancialTransactions.GetFinancialTransByTransId(TransId, ActiveOnly);
                    gvPTrans.DataSource = dtTrans;
                    gvPTrans.DataBind();
                }
                else
                {
                    dtTrans = FinancialTransactions.GetFinancialTransByProjId(Convert.ToInt32(hfProjId.Value), ActiveOnly, BOARD_REFUND);
                    gvPTrans.DataSource = dtTrans;
                    gvPTrans.DataBind();
                }
                if (dtTrans.Rows.Count > 0)
                    CommonHelper.DisableButton(btnTransactionSubmit);
                else
                {
                    CommonHelper.EnableButton(btnTransactionSubmit);
                    btnNewTransaction.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvBCommit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBCommit.EditIndex = -1;
            BindFundDetails(GetTransId());
        }

        protected void gvBCommit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBCommit.EditIndex = e.NewEditIndex;
            BindFundDetails(GetTransId());
            if (btnNewTransaction.Visible == true)
            {
                btnNewTransaction.Visible = false;
            }
        }

        protected void gvBCommit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (((TextBox)gvBCommit.Rows[rowIndex].FindControl("txtAmount")).Text.Trim() != "")
                {
                    decimal n;
                    bool isNumeric = decimal.TryParse(((TextBox)gvBCommit.Rows[rowIndex].FindControl("txtAmount")).Text.Trim(), out n);

                    if (!isNumeric || Convert.ToDecimal(((TextBox)gvBCommit.Rows[rowIndex].FindControl("txtAmount")).Text.Trim()) <= 0)
                    {
                        lblErrorMsg.Text = "Select a valid transaction amount";
                        ((TextBox)gvBCommit.Rows[rowIndex].FindControl("txtAmount")).Focus();
                        return;
                    }
                }

                decimal amount = Convert.ToDecimal(((TextBox)gvBCommit.Rows[rowIndex].FindControl("txtAmount")).Text);
                int transType = Convert.ToInt32(((DropDownList)gvBCommit.Rows[rowIndex].FindControl("ddlTransType")).SelectedValue.ToString());
                int detailId = Convert.ToInt32(((Label)gvBCommit.Rows[rowIndex].FindControl("lblDetId")).Text);
                int fundId = Convert.ToInt32(((Label)gvBCommit.Rows[rowIndex].FindControl("lblFundId")).Text);

                int transId = Convert.ToInt32(((Label)gvBCommit.Rows[rowIndex].FindControl("lblTransId")).Text);

                decimal old_amount = Convert.ToDecimal(FinancialTransactions.GetTransDetails(detailId).Rows[0]["Amount"].ToString());
                decimal bal_amount = Convert.ToDecimal(hfBalAmt.Value);
                decimal allowed_amount = old_amount + bal_amount;



                if (amount == allowed_amount)
                {
                    lblErrorMsg.Text = "Transaction is complete, more funds not allowed";
                }
                else if (amount > allowed_amount)
                {
                    //amount = allowed_amount;
                    //lblErrorMsg.Text = "Amount auto adjusted to available fund amount";

                    lblErrorMsg.Text = "Amount entered is more than the available balance amount. Please enter available funds.";
                    return;
                }
                else if (amount < allowed_amount)
                {
                    if (!btnCashRefundSubmit.Enabled)
                        CommonHelper.EnableButton(btnCashRefundSubmit);
                }

                //if (FinancialTransactions.IsDuplicateFundDetailPerTransaction(transId, fundId, transType))
                //{
                //    lblErrorMsg.Text = "Same fund and same transaction type is already submitted for this transaction. Please change selection";
                //    return;
                //}

                FinancialTransactions.UpdateTransDetails(detailId, transType, amount);


                gvBCommit.EditIndex = -1;
                BindFundDetails(GetTransId());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }

        }

        protected void gvBCommit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                CommonHelper.GridViewSetFocus(e.Row);
            {
                //Checking whether the Row is Data Row
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTrans = (e.Row.FindControl("ddlTransType") as DropDownList);
                    TextBox txtTransType = (e.Row.FindControl("txtTransType") as TextBox);
                    Label lblFName = (e.Row.FindControl("lblFundName") as Label);
                    if (txtTransType != null)
                    {
                        DataTable dtable = new DataTable();
                        if (lblFName.Text.ToLower().Contains("hopwa"))
                        {
                            dtable = FinancialTransactions.GetDataTableByProcName("GetLKTransHopwa");
                        }
                        else
                        {
                            dtable = FinancialTransactions.GetDataTableByProcName("GetLKTransNonHopwa");
                        }

                        // dtable = FinancialTransactions.GetLookupDetailsByName("LkTransType");
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

        protected void gvPTrans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPTrans.EditIndex = e.NewEditIndex;
            BindTransGrid(GetTransId());
        }

        protected void gvPTrans_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                string lblDetId = ((Label)gvBCommit.Rows[rowIndex].FindControl("lblDetId")).Text.Trim();
                if (lblDetId.ToString() != "")
                {
                    FinancialTransactions.ActivateFinancialTransByTransId(Convert.ToInt32(lblDetId));

                    BindFundDetails(GetTransId());

                    lblErrorMsg.Text = "Transaction detail was successfully activated";
                    CommonHelper.EnableButton(btnCashRefundSubmit);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvPTrans_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPTrans.EditIndex = -1;
            BindTransGrid(GetTransId());
        }

        protected void gvBCommit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                Label lblDetailId = (Label)gvBCommit.Rows[rowIndex].FindControl("lblDetId");
                if (lblDetailId != null)
                {
                    FinancialTransactions.DeleteTransactionDetail(Convert.ToInt32(lblDetailId.Text));

                    BindFundDetails(GetTransId());

                    lblErrorMsg.Text = "Transaction detail was successfully deleted";
                    CommonHelper.EnableButton(btnCashRefundSubmit);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvPTrans_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                string TransId = ((Label)gvPTrans.Rows[rowIndex].FindControl("lblTransId")).Text.Trim();
                if (TransId.ToString() != "")
                {
                    FinancialTransactions.InactivateFinancialTransByTransId(Convert.ToInt32(TransId));
                    BindTransGrid(GetTransId());
                    BindFundDetails(GetTransId());

                    CommonHelper.EnableButton(btnTransactionSubmit);
                    lblErrorMsg.Text = "Transaction was successfully inactivated. All details related to this transaction also have been inactivated.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void rdBtnSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPTrans.DataSource = null;
            gvPTrans.DataBind();
            gvBCommit.DataSource = null;
            gvBCommit.DataBind();
            lblProjName.Text = "";
            hfProjId.Value = "";
            ClearTransactionDetailForm();
            pnlTranDetails.Visible = false;
            lblErrorMsg.Text = "";
            txtCommitedProjNum.Text = "";
            txtProjNum.Text = "";
            lblGrantee.Text = "";
            lblAvailVisibleFund.Text = "";
            if (rdBtnSelection.SelectedIndex > 0)
            {
                txtProjNum.Visible = false;
                txtCommitedProjNum.Visible = true;
                imgNewAwardSummary.Visible = true;
                imgExistingAwardSummary.Visible = false;
                divPtransEntry.Visible = false;                
            }
            else
            {
                txtProjNum.Visible = true;
                txtCommitedProjNum.Visible = false;
                imgNewAwardSummary.Visible = false;
                imgExistingAwardSummary.Visible = true;
                Response.Redirect("cashrefund.aspx");
            }
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (cbActiveOnly.Checked)
                ActiveOnly = 1;
            else
                ActiveOnly = 0;
            BindTransGrid(GetTransId());
            BindFundDetails(GetTransId());
        }

        protected void rdBtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            ClearTransactionDetailForm();
            GetSelectedTransId(gvPTrans);
            BindFundDetails(GetTransId());
            pnlTranDetails.Visible = true;
        }

        private void GetSelectedTransId(GridView gvFGM)
        {
            for (int i = 0; i < gvFGM.Rows.Count; i++)
            {
                RadioButton rbGInfo = (RadioButton)gvFGM.Rows[i].Cells[0].FindControl("rdBtnSelect");
                if (rbGInfo != null)
                {
                    if (rbGInfo.Checked)
                    {
                        HiddenField hf = (HiddenField)gvFGM.Rows[i].Cells[0].FindControl("HiddenField1");
                        if (hf != null)
                        {
                            ViewState["SelectedTransId"] = hf.Value;
                            hfTransId.Value = hf.Value;
                        }
                        HiddenField hfAmt = (HiddenField)gvFGM.Rows[i].Cells[2].FindControl("HiddenField2");
                        if (hfAmt != null)
                        {
                            ViewState["TransAmt"] = hfAmt.Value;
                            hfTransAmt.Value = hfAmt.Value;
                            hfBalAmt.Value = hfAmt.Value;
                        }
                        break;
                    }
                }
            }
        }

        protected void gvPTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedTransId(gvPTrans);
        }

        protected void gvPTrans_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (rdBtnSelection.SelectedIndex == 0)
                e.Row.Cells[0].Visible = false;
            else
                e.Row.Cells[0].Visible = true;
        }

        protected void lbAwardSummary_Click(object sender, ImageClickEventArgs e)
        {
            if (hfProjId.Value != "")
            {
                string url = "/awardsummary.aspx?projectid=" + hfProjId.Value;
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(),
                        "script", sb.ToString());
            }
            else
                lblErrorMsg.Text = "Select a project to see the award summary";
        }

        protected void hdnCommitedProjValue_ValueChanged(object sender, EventArgs e)
        {
            string projNum = ((HiddenField)sender).Value;

            DataTable dt = new DataTable();
            if (rdBtnSelection.SelectedIndex > 0)
            {
                if (txtCommitedProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());
            }
            else
            {
                if (txtProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());
            }

            ///populate the form based on retrieved data
            getDetails(dt);
        }

        protected void hdnValue_ValueChanged(object sender, EventArgs e)
        {
            string projNum = ((HiddenField)sender).Value;

            DataTable dt = new DataTable();
            if (rdBtnSelection.SelectedIndex > 0)
            {
                if (txtCommitedProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());
            }
            else
            {
                if (txtProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());
            }

            ///populate the form based on retrieved data
            getDetails(dt);
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (rdBtnSelection.SelectedIndex > 0)
            {
                if (txtCommitedProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", txtCommitedProjNum.Text);
            }
            else
            {
                if (txtProjNum.Text == "")
                {
                    lblErrorMsg.Text = "Please select project number";
                    return;
                }
                dt = Project.GetProjects("GetProjectIdByProjNum", txtProjNum.Text);
            }
            getDetails(dt);
        }

        private void getDetails(DataTable dt)
        {
            try
            {
                lblAvailFund.Text = "";
                lblAvailVisibleFund.Text = "";
                if (dt.Rows.Count != 0)
                {
                    hfProjId.Value = dt.Rows[0][0].ToString();
                    DataRow dr = ProjectCheckRequestData.GetAvailableFundsByProject(int.Parse(hfProjId.Value));
                    if (dr != null)
                        if (Convert.ToDecimal(dr["availFund"].ToString()) > 0)
                        {
                            lblAvailFund.Text = Convert.ToDecimal(dr["availFund"].ToString()).ToString("#.##");
                            lblAvailVisibleFund.Text = CommonHelper.myDollarFormat(Convert.ToDecimal(dr["availFund"].ToString()));
                            //.ToString("#.##");
                        }
                        else
                        {
                            lblAvailFund.Text = "0.00";
                            lblAvailVisibleFund.Text = "0.00";
                        }

                    pnlTranDetails.Visible = false;
                    lblErrorMsg.Text = "";

                    gvPTrans.DataSource = null;
                    gvPTrans.DataBind();

                    ClearTransactionDetailForm();
                    DataTable dtProjects = FinancialTransactions.GetBoardCommitmentsByProject(Convert.ToInt32(hfProjId.Value));

                    lblProjName.Text = dtProjects.Rows[0]["Description"].ToString();
                    dt = new DataTable();
                    dt = FinancialTransactions.GetGranteeByProject(Convert.ToInt32(hfProjId.Value));
                    if (dt.Rows.Count > 0)
                    {
                        lblGrantee.Text = dt.Rows[0]["Applicantname"].ToString();
                        hfGrantee.Value = dt.Rows[0]["applicantid"].ToString();
                    }
                    else
                    {
                        lblGrantee.Text = "";
                        hfGrantee.Value = "";
                    }

                    txtTransDate.Text = DateTime.Now.ToShortDateString();
                    txtTotAmt.Text = "";
                    BindFundAccounts();
                    ifProjectNotes.Src = "ProjectNotes.aspx?ProjectId=" + hfProjId.Value;

                    if (rdBtnSelection.SelectedIndex == 1)
                    {
                        DataTable dtTrans = FinancialTransactions.GetFinancialTransByProjId(Convert.ToInt32(hfProjId.Value), ActiveOnly, BOARD_REFUND);
                        gvPTrans.DataSource = dtTrans;
                        gvPTrans.DataBind();
                        CommonHelper.DisableButton(btnTransactionSubmit);
                    }
                    else if (rdBtnSelection.SelectedIndex == 0)
                    {
                        CommonHelper.EnableButton(btnTransactionSubmit);
                    }
                }
                else
                {
                    lblProjName.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnNewTransaction_Click(object sender, EventArgs e)
        {
            Response.Redirect("cashrefund.aspx");
        }
    }
}