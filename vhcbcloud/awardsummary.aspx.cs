﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class AwardSummary : System.Web.UI.Page
    {
        bool isReallocation = false;
        /// <summary>
        /// Loading Project details and Loading Award Summary grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string reallocarionFlag = Request.QueryString["Reallocations"];
            isReallocation = reallocarionFlag == "true";

            if (!IsPostBack)
            {
                string projId = Request.QueryString["projectid"].ToString();

                DataTable dtProjects = GetProjects();
                BindProjects(dtProjects);

                if (projId != "")
                {
                    lblProjId.Text = GetProjectName(dtProjects, projId);
                    ddlProj.Items.FindByValue(projId).Selected = true;
                    txtFromCommitedProjNum.Text = ddlProj.SelectedItem.Text;
                    hfProjId.Value = projId;
                }
            }
            BindAwardSummary(Convert.ToInt32(hfProjId.Value));
            ddlProj.Visible = false;

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
        /// UI Project dropdown filtering the projects
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
        /// Setting the Project Name for a selected Project
        /// </summary>
        /// <param name="dtProjects"></param>
        /// <param name="projId"></param>
        /// <returns></returns>
        private string GetProjectName(DataTable dtProjects, string projId)
        {
            DataRow[] dr = dtProjects.Select("projectid = '" + projId + "'");
            return dr[0]["Description"].ToString();
        }
        /// <summary>
        /// Get projects from database 
        /// </summary>
        /// <returns></returns>
        private DataTable GetProjects()
        {
            DataTable dtProjects = new DataTable();
            dtProjects = Project.GetProjects("GetProjects");
            return dtProjects;
        }
        /// <summary>
        /// Binding Projects to dropdown
        /// </summary>
        /// <param name="dtProjects"></param>
        private void BindProjects(DataTable dtProjects)
        {
            ddlProj.DataSource = dtProjects;
            ddlProj.DataValueField = "projectId";
            ddlProj.DataTextField = "Proj_num";
            ddlProj.DataBind();
            ddlProj.Items.Insert(0, new ListItem("Select", "NA"));
        }
        /// <summary>
        /// Pre populating the typed project numbers and setting the Award summary grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdnValue_ValueChanged(object sender, EventArgs e)
        {
            string projNum = ((HiddenField)sender).Value;

            DataTable dt = new DataTable();

            dt = Project.GetProjects("GetProjectIdByProjNum", projNum.ToString());

            ///populate the form based on retrieved data
            if (dt.Rows.Count > 0)
            {
                DataTable dtProjects = GetProjects();
                int projId = Convert.ToInt32(dt.Rows[0][0].ToString());
                lblProjId.Text = GetProjectName(dtProjects, projId.ToString());
                txtFromCommitedProjNum.Text = projNum;
                hdnValue.Value = projId.ToString();
                hfProjId.Value = projId.ToString();
                BindAwardSummary(projId);
            }
        }
        /// <summary>
        /// Loading Award Summary and details grids
        /// </summary>
        /// <param name="projectid"></param>
        private void BindAwardSummary(int projectid)
        {
            try
            {
                lblErrorMsg.Text = ""; DataTable dtAwdStatus = null; DataTable dtTransDetail = null;
                dtAwdStatus = FinancialTransactions.GetAwardSummary(projectid).Tables[0];
                dtTransDetail = FinancialTransactions.GetAwardSummary(projectid).Tables[1];

                gvCurrentAwdStatus.DataSource = dtAwdStatus;
                gvCurrentAwdStatus.DataBind();

                DataView detailView = dtTransDetail.DefaultView;
                detailView.Sort = "DetailId DESC";

                gvTransDetail.DataSource = detailView;
                gvTransDetail.DataBind();
                SetSummaryGridTotals(dtAwdStatus);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        /// <summary>
        /// Setting Summary and Transaction grid 's
        /// Calculating the totals
        /// </summary>
        /// <param name="dtAwdStatus"></param>
        private void SetSummaryGridTotals(DataTable dtAwdStatus)
        {
            decimal totCommitAmt = 0;
            decimal totPendAmt = 0;
            decimal totDisbursedAmt = 0;
            decimal totBalanceAmt = 0;

            if (dtAwdStatus.Rows.Count > 0)
            {
                Label lblCommit = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblCommited");
                Label lblDisbursed = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblDisbursed");
                Label lblBalance = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblBalance");
                Label lblPending = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblPending");

                if (dtAwdStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAwdStatus.Rows.Count; i++)
                    {
                        //09/29/2016 - modified the totals of commitment amount to !=0 from >0
                        if (Convert.ToDecimal(dtAwdStatus.Rows[i]["FinalCommited"].ToString()) != 0)
                            totCommitAmt += Convert.ToDecimal(dtAwdStatus.Rows[i]["FinalCommited"].ToString());

                        if (Convert.ToDecimal(dtAwdStatus.Rows[i]["Disbursed"].ToString()) != 0)
                            totDisbursedAmt += Convert.ToDecimal(dtAwdStatus.Rows[i]["Disbursed"].ToString());

                        if (Convert.ToDecimal(dtAwdStatus.Rows[i]["Balanced"].ToString()) != 0)
                            totBalanceAmt += Convert.ToDecimal(dtAwdStatus.Rows[i]["Balanced"].ToString());

                        if (Convert.ToDecimal(dtAwdStatus.Rows[i]["Pending"].ToString()) != 0)
                            totPendAmt += Convert.ToDecimal(dtAwdStatus.Rows[i]["Pending"].ToString());

                    }
                }

                lblCommit.Text = CommonHelper.myDollarFormat(totCommitAmt);
                lblPending.Text = CommonHelper.myDollarFormat(totPendAmt);
                lblDisbursed.Text = CommonHelper.myDollarFormat(totDisbursedAmt);
                lblBalance.Text = CommonHelper.myDollarFormat(totBalanceAmt);
            }
        }

        protected void gvCurrentAwdStatus_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;//isReallocation;

            if (!isReallocation && e.Row.RowType == DataControlRowType.Footer)
            {
                Label l = new Label();
                l.Text = "Totals :";
                e.Row.Cells[2].Controls.Add(l);
            }
        }

        protected void ddlProj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProj.SelectedIndex > 0)
            {
                DataTable dtProjects = GetProjects();
                lblProjId.Text = GetProjectName(dtProjects, ddlProj.SelectedValue.ToString());// +" - " + ddlProj.SelectedValue.ToString();
                BindAwardSummary(Convert.ToInt32(ddlProj.SelectedValue.ToString()));
            }
        }

        protected void gvTransDetail_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            int pageIndex = 0;
            DataTable dtTransDetail = new DataTable();

            //if (hdnValue.Value.ToString() == "")
            //    dtTransDetail = FinancialTransactions.GetFinancialFundDetailsByProjectId(Convert.ToInt32(ddlProj.SelectedValue.ToString()), isReallocation).Tables[1];
            //else
            //    dtTransDetail = FinancialTransactions.GetFinancialFundDetailsByProjectId(Convert.ToInt32(hdnValue.Value.ToString()), isReallocation).Tables[1];

            dtTransDetail = FinancialTransactions.GetAwardSummary(DataUtils.GetInt(hfProjId.Value)).Tables[1];

            gvTransDetail.DataSource = SortDataTable(dtTransDetail, false);
            gvTransDetail.DataBind();
            gvTransDetail.PageIndex = pageIndex;
        }

        /// <summary>
        /// Sorting Grid
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="isPageIndexChanging"></param>
        /// <returns></returns>
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

        protected void gvCurrentAwdStatus_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            int pageIndex = 0;

            DataTable dtAwdStatus = new DataTable();
            //if (hdnValue.Value.ToString() == "")
            //    dtTransDetail = FinancialTransactions.GetFinancialFundDetailsByProjectId(Convert.ToInt32(ddlProj.SelectedValue.ToString()), isReallocation).Tables[0];
            //else
            //    dtTransDetail = FinancialTransactions.GetFinancialFundDetailsByProjectId(Convert.ToInt32(hdnValue.Value.ToString()), isReallocation).Tables[0];

            dtAwdStatus = FinancialTransactions.GetAwardSummary(DataUtils.GetInt(hfProjId.Value)).Tables[0];

            gvCurrentAwdStatus.DataSource = SortDataTable(dtAwdStatus, false);
            gvCurrentAwdStatus.DataBind();
            gvCurrentAwdStatus.PageIndex = pageIndex;

            SetSummaryGridTotals(dtAwdStatus);

            //decimal totCommitAmt = 0;
            //decimal totPendAmt = 0;
            //decimal totExpendAmt = 0;
            //decimal totFinalExpendAmt = 0;
            //decimal totBalanceAmt = 0;

            //if (dtTransDetail.Rows.Count > 0)
            //{
            //    Label lblCommit = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblCommit");
            //    Label lblPending = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblPending");
            //    Label lblExpend = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblExpend");
            //    Label lblFinalExpend = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblFinalExpend");
            //    Label lblBalance = (Label)gvCurrentAwdStatus.FooterRow.FindControl("lblBalance");
            //    if (dtTransDetail.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtTransDetail.Rows.Count; i++)
            //        {
            //            //09/29/2016 - modified the totals of commitment amount to !=0 from >0
            //            if (Convert.ToDecimal(dtTransDetail.Rows[i]["commitmentamount"].ToString()) != 0)
            //                totCommitAmt += Convert.ToDecimal(dtTransDetail.Rows[i]["commitmentamount"].ToString());

            //            if (Convert.ToDecimal(dtTransDetail.Rows[i]["expendedamount"].ToString()) != 0)
            //                totExpendAmt += Convert.ToDecimal(dtTransDetail.Rows[i]["expendedamount"].ToString());

            //            if (Convert.ToDecimal(dtTransDetail.Rows[i]["finaldisbursedamount"].ToString()) != 0)
            //                totFinalExpendAmt += Convert.ToDecimal(dtTransDetail.Rows[i]["finaldisbursedamount"].ToString());

            //            if (Convert.ToDecimal(dtTransDetail.Rows[i]["pendingamount"].ToString()) != 0)
            //                totPendAmt += Convert.ToDecimal(dtTransDetail.Rows[i]["pendingamount"].ToString());

            //            if (Convert.ToDecimal(dtTransDetail.Rows[i]["balance"].ToString()) != 0)
            //                totBalanceAmt += Convert.ToDecimal(dtTransDetail.Rows[i]["balance"].ToString());
            //        }
            //    }

            //    lblCommit.Text = CommonHelper.myDollarFormat(totCommitAmt);
            //    lblPending.Text = CommonHelper.myDollarFormat(totPendAmt);
            //    lblExpend.Text = CommonHelper.myDollarFormat(totExpendAmt);
            //    lblBalance.Text = CommonHelper.myDollarFormat(totBalanceAmt);
            //    lblFinalExpend.Text = CommonHelper.myDollarFormat(totFinalExpendAmt);
            //}
        }
        /// <summary>
        /// Exago Link for Award Summary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AwardSummaryReport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                    "script", Helper.GetExagoURLForAwardSummary(txtFromCommitedProjNum.Text, "Award_Summary_Complete"));
            //"script", Helper.GetExagoURLForAwardSummary(ddlProj.SelectedItem.Text, "Award_Summary_Complete"));
            
        }
        /// <summary>
        /// Transaction details row data bound
        /// Setting the Light Blue color for different project
        /// If it is Adjustment, setting the Orange color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTransDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["IsDifferentProject"].ToString().Equals("1"))
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }

                if (DataUtils.GetBool(drv["Adjust"].ToString()))
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                //else
                //{
                //    e.Row.BackColor = System.Drawing.Color.Green;
                //}
            }
            }
    }
}