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

namespace ConserveNatRec
{
    public partial class EasementTerms : System.Web.UI.Page
    {
        string Pagename = "EasementTerms";
        string projectNumber = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            hfProjectId.Value = "0";

            if (Session["ProjectNumber"] == null)
                Response.Redirect("Login.aspx");
            else
                projectNumber = Session["ProjectNumber"].ToString();

            LoadProject();

            if (!IsPostBack)
            {
                BindControls();
                LoadPage();
                BindAcreageGrid();
                BindAttributeGrid();
            }
        }

        private void LoadProject()
        {
            if (projectNumber != "")
            {
                DataRow dr = ConservationApplicationData.GetEasementTerm(projectNumber);

                if (dr != null)
                {
                    hfConserveId.Value = dr["ConserveID"].ToString();
                    hfProjectId.Value = dr["ProjectId"].ToString();
                }
            }
        }

        private void LoadPage()
        {
            if (projectNumber != "")
            {
                DataRow dr = ConservationApplicationData.GetEasementTerm(projectNumber);

                if (dr != null)
                {
                    hfConserveId.Value = dr["ConserveID"].ToString();
                    hfProjectId.Value = dr["ProjectId"].ToString();
                    txtEasementTermsOther.Text = dr["EasementTermsOther"].ToString();
                    txtHistoricSignificance.Text = dr["HistoricSignificance"].ToString();
                    txtSubDivisionReason.Text = dr["SubDivisionReason"].ToString();
                }
            }
        }

        private void BindControls()
        {
            BindLookETUP(ddlAttribute, 6);
            BindLookSPZUP(ddlAcreageDescription, 97);
            
        }
        private void BindLookETUP(DropDownList ddList, int LookupType)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = LookupValuesData.GetlookupvaluesET(LookupType);
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
        private void BindLookSPZUP(DropDownList ddList, int LookupType)
        {
            try
            {
                ddList.Items.Clear();
                ddList.DataSource = LookupValuesData.GetlookupvaluesSPZ(LookupType);
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

        private void BindAcreageGrid()
        {
            try
            {
                DataTable dtAcreage = ConservationSummaryData.GetConserveAcresSPZList(DataUtils.GetInt(hfProjectId.Value), cbActiveOnly.Checked);

                if (dtAcreage.Rows.Count > 0)
                {
                    dvAcreageGrid.Visible = true;
                    gvAcreage.DataSource = dtAcreage;
                    gvAcreage.DataBind();

                    Label lblFooterTotal = (Label)gvAcreage.FooterRow.FindControl("lblFooterTotal");
                    int totAcres = 0;

                    for (int i = 0; i < dtAcreage.Rows.Count; i++)
                    {
                        if (DataUtils.GetBool(dtAcreage.Rows[i]["RowIsActive"].ToString()))
                            totAcres += DataUtils.GetInt(dtAcreage.Rows[i]["Acres"].ToString());
                    }

                    lblFooterTotal.Text = totAcres.ToString();
                }
                else
                {
                    dvAcreageGrid.Visible = false;
                    gvAcreage.DataSource = null;
                    gvAcreage.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "BindAcreageGrid", "", ex.Message);
            }
        }


        protected void gvAcreage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            string strAcres = ((TextBox)gvAcreage.Rows[rowIndex].FindControl("txtAcres")).Text;

            if (string.IsNullOrWhiteSpace(strAcres) == true)
            {
                LogMessage("Enter Acres");
                return;
            }
            if (DataUtils.GetDecimal(strAcres) <= 0)
            {
                LogMessage("Enter valid Acres");
                return;
            }

            int ConserveAcresID = DataUtils.GetInt(((Label)gvAcreage.Rows[rowIndex].FindControl("lblConserveAcresID")).Text);
            decimal Acres = DataUtils.GetDecimal(strAcres);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAcreage.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationSummaryData.UpdateConserveAcres(ConserveAcresID, Acres, RowIsActive);
            gvAcreage.EditIndex = -1;

            BindAcreageGrid();

            LogMessage("Acreage updated successfully");
        }

        protected void gvAcreage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAcreage.EditIndex = -1;
            BindAcreageGrid();
        }

        protected void gvAcreage_RowEditing(object sender, GridViewUpdateEventArgs e)
        {
           
        }

        private void saveData()
        {
            ConservationApplicationData.EasementTerm(projectNumber, txtSubDivisionReason.Text, txtHistoricSignificance.Text, txtEasementTermsOther.Text);

            // BindTrailCheckBoxList();
            LogMessage("Conservation Form Data Added Successfully");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("TownPlaning.aspx");
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect("FarmManagement.aspx");
        }

        protected void btnAddAcreage_Click(object sender, EventArgs e)
        {
            if (ddlAcreageDescription.SelectedIndex == 0)
            {
                LogMessage("Select Description");
                ddlAcreageDescription.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAcres.Text.ToString()) == true)
            {
                LogMessage("Enter Acres");
                txtAcres.Focus();
                return;
            }
            if (DataUtils.GetDecimal(txtAcres.Text) <= 0)
            {
                LogMessage("Enter valid Acres");
                txtAcres.Focus();
                return;
            }

            Result objResult = ConservationSummaryData.AddConserveAcres(DataUtils.GetInt(hfProjectId.Value),
                DataUtils.GetInt(ddlAcreageDescription.SelectedValue.ToString()), DataUtils.GetDecimal(txtAcres.Text));
            CleaAcreageForm();

            BindAcreageGrid();

            if (objResult.IsDuplicate && !objResult.IsActive)
                LogMessage("Acreage already exist as in-active");
            else if (objResult.IsDuplicate)
                LogMessage("Acreage Holder already exist");
            else
                LogMessage("New Acreage added successfully");
        }

        private void CleaAcreageForm()
        {
            ddlAcreageDescription.SelectedIndex = -1;
            cbAddAcreage.Checked = false;
        }

        protected void gvAcreage_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            gvAcreage.EditIndex = e.NewEditIndex;
            BindAcreageGrid();
        }

        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData();
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }

        protected void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            BindAcreageGrid();
            BindAttributeGrid();
        }

        protected void AddAttribute_Click(object sender, EventArgs e)
        {
            if (ddlAttribute.SelectedIndex == 0)
            {
                LogMessage("Select Attribute");
                ddlAttribute.Focus();
                return;
            }

            AttributeResult obAttributeResult = ConservationAttributeData.AddConserveAttribute(DataUtils.GetInt(hfConserveId.Value),
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
                DataTable dt = ConservationAttributeData.GetConserveAttribList(DataUtils.GetInt(hfConserveId.Value), cbActiveOnly.Checked);

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

            int ConserveAttribID = DataUtils.GetInt(((Label)gvAttribute.Rows[rowIndex].FindControl("lblConserveAttribID")).Text);
            bool RowIsActive = Convert.ToBoolean(((CheckBox)gvAttribute.Rows[rowIndex].FindControl("chkActive")).Checked); ;

            ConservationAttributeData.UpdateConserveAttribute(ConserveAttribID, RowIsActive);
            gvAttribute.EditIndex = -1;

            BindAttributeGrid();

            LogMessage("Attribute updated successfully");
        }
    }
}