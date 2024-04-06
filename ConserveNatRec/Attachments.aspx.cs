using NodaTime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using WebReports.Api.Data;
using WebReports.Api.Reports;
using WebReports.Api.Scheduler;
using WebReports.Api;

namespace ConserveNatRec
{
    public partial class Attachments : System.Web.UI.Page
    {
        string Pagename = "Attachments";
        string projectNumber = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            if (Session["ProjectNumber"] == null)
                Response.Redirect("Login.aspx");
            else
                projectNumber = Session["ProjectNumber"].ToString();

            if (!IsPostBack)
            {
                LoadPage();
            }

            if (!IsPostBack)
            {
                UploadLink.HRef = "https://server3.vhcb.org/sharing/a2ABfrvpt";
            }
        }
        private void LoadPage()
        {
            if (projectNumber != "")
            {
                DataRow dr = ConservationApplicationData.GetAdditionalInfo(projectNumber);

                if (dr != null)
                {
                    txtSignature.Text = dr["Signature"].ToString();
                    txtMissingDocs.Text = dr["MissingDocs"].ToString();
                    if (dr["Sig_Date"].ToString() != "")
                        txtSig_Date.Text = Convert.ToDateTime(dr["Sig_Date"].ToString()).ToShortDateString();
                }
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("Additionalinfo.aspx");
        }

        protected void Save()
        {
            if (projectNumber != "")
            {
                ConservationApplicationData.ConservationFarmAttachments(projectNumber, txtSignature.Text, DataUtils.GetDate(txtSig_Date.Text), txtMissingDocs.Text);
                LogMessage("Successfully Saved Data");
            }
        }
        protected void ddlGoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(ddlGoto.SelectedItem.Value);
        }
        private void LogMessage(string message)
        {
            dvMessage.Visible = true;
            lblErrorMsg.Text = message;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (projectNumber != "")
            {
                Save();

                List<string> EmailList = ViabilityApplicationData.GetMailAddressesForPDFEmail(projectNumber).Rows.OfType<DataRow>().Select(dr => dr.Field<string>("EmailAddress")).ToList();

                if (EmailList.Count > 0)
                    GetExagoURLForReport(projectNumber, "Conservation - Farm", EmailList);

                ViabilityApplicationData.SubmitApplication(projectNumber);

                LogMessage("Conservation Farm Application Submitted Successfully");

                Response.Redirect("Login.aspx");
            }
        }
        public static void GetExagoURLForReport(string Projnum, string ReportName, List<string> EmailList)
        {
            string URL = string.Empty;
            Api api = new Api(@"/eWebReports");

            DataSource ds = api.DataSources.GetDataSource("VHCB");
            ds.DataConnStr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            // Set the action to execute the report
            api.Action = wrApiAction.ExecuteReport;
            WebReports.Api.Common.Parameter parameter = api.Parameters.GetParameter("Projnum");
            parameter.Value = Projnum;
            parameter.IsHidden = false;
            parameter.PromptText = "";

            api.SetupData.StorageMgmtConfig.SetIdentity("userId", "Dherman");
            api.SetupData.StorageMgmtConfig.SetIdentity("companyId", "VHCB");


            ReportObject report = api.ReportObjectFactory.LoadFromRepository(@"Conservation\Farm Online App\" + ReportName);

            //api.Action = wrApiAction.ExecuteReport;
            //WebReports.Api.Common.Parameter parameter = api.Parameters.GetParameter("ProjID");
            //parameter.Value = "10161";
            //parameter.IsHidden = true;
            //ReportName = "Grid Project Milestone";
            //ReportObject report = api.ReportObjectFactory.LoadFromRepository(@"Utility\Grid Reports\" + ReportName);

            if (report != null)
            {
                report.ExportType = wrExportType.Pdf;
                api.ReportObjectFactory.SaveToApi(report);
            }
            // URL = ConfigurationManager.AppSettings["ExagoURL"] + api.GetUrlParamString("ExagoHome", true);


            // Run-once, immediately save to disk
            string jobId;           // Use to retrieve schedule info later for editing
            int hostIdx;            // Assigned execution host id

            string subject = $"Online Conservation Farm Application for Project ({Projnum})";

            ReportScheduleInfo newSchedule = new ReportScheduleInfoOnce()
            {
                ScheduleName = "Online Conservation Farm",             // Schedule name
                ReportType = wrReportType.Advanced,            // Report type
                RangeStartDate = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),                   // Start date
                ScheduleTime = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute), // Start time
                SendReportInEmail = true,                             // Email or save
                EmailSubject = subject,
                EmailBody = "PDF of your Online Conservation Farm Application"
            };
            newSchedule.EmailToList.AddRange(EmailList);

            //newSchedule.EmailToList.Add("dan@vhcb.org");
            //newSchedule.EmailToList.Add("aaron @vhcb.org");
            //newSchedule.EmailToList.Add("b.mcgavisk @vhcb.org");
            //newSchedule.EmailToList.Add("Marcy @vhcb.org");

            // Send to the scheduler; wrap in try/catch to handle exceptions
            try
            {
                api.ReportScheduler.AddReport(
                  new ReportSchedule(api.PageInfo) { ScheduleInfo = newSchedule }, out jobId, out hostIdx);
            }
            catch (Exception) { }
        }
        protected void btnSaveExit_Click(object sender, EventArgs e)
        {
            Save();
            Session["ProjectNumber"] = null;
            Response.Redirect("login.aspx");
        }
    }
}