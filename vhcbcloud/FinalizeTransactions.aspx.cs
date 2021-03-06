﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class FinalizeTransactions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //CompareEndTodayValidator.ValueToCompare = DateTime.Now.ToShortDateString();
            lblErrorMsg.Text = "";
            if (!IsPostBack)
            {
                BindProjects();
                BindFinancialTrans();
                txtTransDateTo.Text = DateTime.Now.ToString("M/dd/yyyy", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Bind Projects dropdown
        /// </summary>
        protected void BindProjects()
        {
            try
            {
                DataTable dtProjects = new DataTable();
                dtProjects = Project.GetProjects("GetProjects");
                ddlProjFilter.DataSource = dtProjects;
                ddlProjFilter.DataValueField = "projectId";
                ddlProjFilter.DataTextField = "Proj_num";
                ddlProjFilter.DataBind();
                ddlProjFilter.Items.Insert(0, new ListItem("Select", "NA"));
                ddlProjFilter.Items.Insert(1, new ListItem("All", "-1"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        /// <summary>
        /// Bind financial transactions drop down
        /// </summary>
        protected void BindFinancialTrans()
        {
            try
            {
                ddlFinancialTrans.DataSource = FinancialTransactions.GetBoardFinancialTrans();
                ddlFinancialTrans.DataValueField = "TypeID";
                ddlFinancialTrans.DataTextField = "Description";
                ddlFinancialTrans.DataBind();
                ddlFinancialTrans.Items.Insert(0, new ListItem("Select Financial Transaction", "0"));
                ddlFinancialTrans.Items.Insert(1, new ListItem("All", "-1"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
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
        /// <summary>
        /// Project Fiter dropdown changed this event will fire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";

            gvTransactions.DataSource = null;
            gvTransactions.DataBind();

            pnlTranDetails.Visible = false;

            if (ddlProjFilter.SelectedIndex != 0)
            {
                int ProjectNo = Convert.ToInt32(ddlProjFilter.SelectedValue.ToString());
                lblProjNameText.Visible = true;

                if (ProjectNo == -1)
                    lblProjName.Text = "All";
                else
                {
                    DataTable dtProjects = FinancialTransactions.GetBoardCommitmentsByProject(ProjectNo);
                    lblProjName.Text = dtProjects.Rows[0]["Description"].ToString();
                }
            }
            else
            {
                lblProjNameText.Visible = false;
                lblProjName.Text = "";
            }
        }

        protected void hdnValue_ValueChanged(object sender, EventArgs e)
        {
            string projNum = ((HiddenField)sender).Value;
            DataTable dt = new DataTable();

            dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());
            ///populate the form based on retrieved data
            if (dt.Rows.Count > 0)
            {
                lblProjNameText.Visible = true;

                if (txtFromCommitedProjNum.Text.ToLower() == "all")
                {
                    lblProjName.Text = "All";
                    BindFinancialTrans();
                    lbtnShowAll.Visible = false;
                }
                else
                {
                    lbtnShowAll.Visible = true;
                    DataTable dtProjects = FinancialTransactions.GetBoardCommitmentsByProject(Convert.ToInt32(dt.Rows[0][0].ToString()));
                    lblProjName.Text = dtProjects.Rows[0]["Description"].ToString();

                    hfProjId.Value = dt.Rows[0][0].ToString();
                    ddlFinancialTrans.Items.Remove(ddlFinancialTrans.Items.FindByValue("26552"));
                }
            }
            else
            {
                lblProjNameText.Visible = false;
                lblProjName.Text = "";
            }
        }

        /// <summary>
        /// When Submit button click, doing field validation
        /// Populating Transactions based on selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubit_click(object sender, EventArgs e)
        {
            DateTime tranFromDate;
            DateTime tranToDate;

            #region Validation
            //if (ddlProjFilter.Items.Count > 1 && ddlProjFilter.SelectedIndex == 0)
            //{
            //    lblErrorMsg.Text = "Select Project";
            //    ddlProjFilter.Focus();
            //    return;
            //}
            //            else
            if (ddlFinancialTrans.Items.Count > 1 && ddlFinancialTrans.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Select financial transaction";
                ddlFinancialTrans.Focus();
                return;
            }
            else if (txtTransDateFrom.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Select Transaction From Date";
                txtTransDateFrom.Focus();
                return;
            }
            else if (txtTransDateTo.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Select Transaction End Date";
                txtTransDateTo.Focus();
                return;

            }
            else
            {
                if (!DateTime.TryParse(txtTransDateFrom.Text.Trim(), out tranFromDate))
                {
                    lblErrorMsg.Text = "Select valid transaction From date";
                    txtTransDateFrom.Focus();
                    return;
                }
                if (!DateTime.TryParse(txtTransDateTo.Text.Trim(), out tranToDate))
                {
                    lblErrorMsg.Text = "Select valid transaction To date";
                    txtTransDateTo.Focus();
                    return;
                }
                if (tranFromDate > DateTime.Today)
                {
                    lblErrorMsg.Text = "From Transaction date should be less than or equal to today";
                    txtTransDateFrom.Focus();
                    return;
                }
                if (tranFromDate > tranToDate)
                {
                    lblErrorMsg.Text = "From Transaction date should be less than End date";
                    txtTransDateFrom.Focus();
                    return;
                }
            }
            #endregion

            ViewState["FromDate"] = tranFromDate;
            ViewState["EndDate"] = tranToDate;

            lblProjNameText.Visible = true;
            lblProjName.Text = "";

            if (hfProjId.Value != "")
            {
                PopulateTransactions(Convert.ToInt32(hfProjId.Value), tranFromDate, tranToDate, Convert.ToInt32(ddlFinancialTrans.SelectedValue.ToString()));
                DataTable dtProjects = FinancialTransactions.GetBoardCommitmentsByProject(Convert.ToInt32(hfProjId.Value));
                lblProjName.Text = dtProjects.Rows[0]["Description"].ToString();
                lbtnShowAll.Visible = true;
            }
            else
            {
                lblProjName.Text = "All";
                PopulateTransactions(-1, tranFromDate, tranToDate, Convert.ToInt32(ddlFinancialTrans.SelectedValue.ToString()));
                lbtnShowAll.Visible = false;
            }
        }

        /// <summary>
        /// Populate Transactions based on user selection
        /// GetFinancialTransactions method fetch data from database 
        /// Grid data binding
        /// </summary>
        /// <param name="Projectid"></param>
        /// <param name="TranFromDate"></param>
        /// <param name="TranToDate"></param>
        /// <param name="TransType"></param>
        private void PopulateTransactions(int Projectid, DateTime TranFromDate, DateTime TranToDate, int TransType)
        {
            DataTable dtable = FinancialTransactions.GetFinancialTransactions(Projectid, TranFromDate, TranToDate, TransType);

            if (dtable.Rows.Count > 0)
            {
                pnlTranDetails.Visible = true;
            }
            else
            {
                lblErrorMsg.Text = "No transactions found during this period.";
                pnlTranDetails.Visible = false;
            }

            gvTransactions.DataSource = dtable;
            gvTransactions.DataBind();
        }
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            DataTable dt = UserSecurityData.GetUserId(Context.User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                //this.MasterPageFile = "SiteNonAdmin.Master";
            }
        }

        /// <summary>
        /// Transaction grid population on databound
        /// calculating total fund amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int transId = Convert.ToInt32(gvTransactions.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;

                DataTable dtDetails = FinancialTransactions.GetFinancialTransactionDetails(transId);
                gvDetails.DataSource = dtDetails;
                gvDetails.DataBind();

                decimal totFundAmt = 0;

                if (dtDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDetails.Rows.Count; i++)
                    {
                        totFundAmt += Convert.ToDecimal(dtDetails.Rows[i]["Amount"].ToString());
                    }
                    Label lblTotAmt = (Label)gvDetails.FooterRow.FindControl("lblFooterAmount");
                    lblTotAmt.Text = CommonHelper.myDollarFormat(totFundAmt);
                }

                DataRowView drv = e.Row.DataItem as DataRowView;
                if (DataUtils.GetBool(drv["Adjust"].ToString()))
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    int transId = Convert.ToInt32(gvTransactions.DataKeys[e.Row.RowIndex].Value.ToString());
            //    GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            //    gvDetails.DataSource = FinancialTransactions.GetFinancialTransactionDetails(transId);
            //    gvDetails.DataBind();
            //}
        }

        /// <summary>
        /// Selected transactions submittion
        /// calling UpdateFinancialTransactionStatus method to comple finalization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTranSubmit_click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTransactions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkTrans") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int transId = Convert.ToInt32(gvTransactions.DataKeys[row.RowIndex].Value.ToString());

                        FinancialTransactions.UpdateFinancialTransactionStatus(transId);
                    }
                }
            }
            if (hfProjId.Value != "")
            {
                PopulateTransactions(Convert.ToInt32(hfProjId.Value), DateTime.Parse(ViewState["FromDate"].ToString()), DateTime.Parse(ViewState["EndDate"].ToString()),
                Convert.ToInt32(ddlFinancialTrans.SelectedValue.ToString()));
                lblErrorMsg.Text = "Transaction finalized successfully";
            }
            else
            {
                lblProjName.Text = "All";
                PopulateTransactions(-1, DateTime.Parse(ViewState["FromDate"].ToString()), DateTime.Parse(ViewState["EndDate"].ToString()), Convert.ToInt32(ddlFinancialTrans.SelectedValue.ToString()));
                lblErrorMsg.Text = "Transaction finalized successfully";
            }
        }

        protected void lbtnShowAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("finalizetransactions.aspx");
        }
    }
}