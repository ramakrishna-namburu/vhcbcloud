﻿using DataAccessLayer;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Housing;
using WebReports.Api;
using WebReports.Api.Data;
using WebReports.Api.Reports;
using WebReports.Api.Scheduler;

namespace Portfolio
{
    public partial class PortfolioData : System.Web.UI.Page
    {
        string Pagename = "PortfolioData";
        string projectNumber = "";
        int ProjectId;
        int Year = 0;
        int PortfolioType = 0;
        string PortfolioTypeName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMessage.Visible = false;
            lblErrorMsg.Text = "";

            if (Session["ProjectNumber"] == null || Session["ProjectNumber"].ToString() == "" || Session["UserId"] == null || Session["UserId"].ToString() == "")
                Response.Redirect("Login.aspx");
            else
            {
                projectNumber = Session["ProjectNumber"].ToString();
                ProjectId = ProjectMaintenanceData.GetProjectId(projectNumber);
                ProjectNum.InnerText = projectNumber;
            }

            if (!IsPostBack)
            {
                PopulateProjectDetails();
                PopulateYear();

            }
        }

        private void PopulateYear()
        {
            try
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = PortfolioDataData.GetPortfolioYearsbyLoginProj(Session["UserId"].ToString(), projectNumber);
                ddlYear.DataValueField = "Year";
                ddlYear.DataTextField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "PopulateYear", "Control ID:" + ddlYear.ID, ex.Message);
            }

        }

        private void PopulateForm()
        {
            hfPortfolioType.Value = "";
            hfYear.Value = "";
            
            DataRow drPortfolioData = PortfolioDataData.GetPortfolioDataForOnLineApp(Session["UserId"].ToString(), projectNumber, ddlYear.SelectedValue, ddlPortfolioType.SelectedValue);
            ClearForm();
            if (drPortfolioData != null)
            {
                hfProjectPortfolioID.Value = drPortfolioData["ProjectPortfolioID"].ToString();
                //spnYear.InnerHtml = drPortfolioData["Year"].ToString();
                PortfolioTypeName = drPortfolioData["PortfolioTypeName"].ToString();
                //spnPortfolioType.InnerHtml = PortfolioTypeName;
                PortfolioType = DataUtils.GetInt(drPortfolioData["PortfolioType"].ToString());
                Year = DataUtils.GetInt(drPortfolioData["Year"].ToString());

                hfPortfolioType.Value = PortfolioType.ToString();
                hfPortfolioTypeName.Value = PortfolioTypeName;
                hfYear.Value = Year.ToString();

                    int Totalunits = DataUtils.GetInt(drPortfolioData["TotalUnits"].ToString());
                PopulateDropDown(ddlPortfolioType, drPortfolioData["Year"].ToString());
                PopulateDropDown(ddlPortfolioType, drPortfolioData["PortfolioType"].ToString());
                    txtTotalUnits.Text = drPortfolioData["TotalUnits"].ToString();

                    txtMGender.Text = drPortfolioData["MGender"].ToString();
                  

                    txtFGender.Text = drPortfolioData["FGender"].ToString();
                   

                    txtUGender.Text = drPortfolioData["UGender"].ToString();
                   

                    txtWhite.Text = drPortfolioData["White"].ToString();
                    

                    txtBlack.Text = drPortfolioData["Black"].ToString();
                   

                    txtAsian.Text = drPortfolioData["Asian"].ToString();
                    

                    txtIndian.Text = drPortfolioData["Indian"].ToString();
                    

                    txtHawaiian.Text = drPortfolioData["Hawaiian"].ToString();
                txtMultiRacial.Text = drPortfolioData["MultiRacial"].ToString();


                txtUnknownRace.Text = drPortfolioData["UnknownRace"].ToString();
                   

                    //txtHispanic.Text = drPortfolioData["Hispanic"].ToString();
                   

                    //txtNonHisp.Text = drPortfolioData["NonHisp"].ToString();
                   

                    txtUnknownEthnicity.Text = drPortfolioData["UnknownEthnicity"].ToString();
                    

                    txtHomeless.Text = drPortfolioData["Homeless"].ToString();


                    txtMarketRate.Text = drPortfolioData["MarketRate"].ToString();


                    txtI100.Text = drPortfolioData["I100"].ToString();


                    txtI80.Text = drPortfolioData["I80"].ToString();


                    txtI75.Text = drPortfolioData["I75"].ToString();


                    txtI60.Text = drPortfolioData["I60"].ToString();
    

                    txtI50.Text = drPortfolioData["I50"].ToString();
          

                    txtI30.Text = drPortfolioData["I30"].ToString();
    

                    txtI20.Text = drPortfolioData["I120"].ToString();
      

                if (DataUtils.GetInt(drPortfolioData["TotalUnits"].ToString()) != 0)
                {
                    decimal perMale = Math.Round((DataUtils.GetDecimal(drPortfolioData["MGender"].ToString()) / Totalunits) * 100, 2);
                    spnMale.InnerText = perMale.ToString() + " %";

                    decimal perFeMale = Math.Round((DataUtils.GetDecimal(drPortfolioData["FGender"].ToString()) / Totalunits) * 100, 2);
                    spnFeMale.InnerText = perFeMale.ToString() + " %";

                    decimal perUGender = Math.Round((DataUtils.GetDecimal(drPortfolioData["UGender"].ToString()) / Totalunits) * 100, 2);
                    spnUGender.InnerText = perUGender.ToString() + " %";

                    decimal perWhite = Math.Round((DataUtils.GetDecimal(drPortfolioData["White"].ToString()) / Totalunits) * 100, 2);
                    spnWhite.InnerText = perWhite.ToString() + " %";

                    decimal perBlack = Math.Round((DataUtils.GetDecimal(drPortfolioData["Black"].ToString()) / Totalunits) * 100, 2);
                    spnBlack.InnerText = perBlack.ToString() + " %";

                    decimal perAsian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Asian"].ToString()) / Totalunits) * 100, 2);
                    spnAsian.InnerText = perAsian.ToString() + " %";

                    decimal perIndian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Indian"].ToString()) / Totalunits) * 100, 2);
                    spnIndian.InnerText = perIndian.ToString() + " %";

                    decimal perHawaiian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Hawaiian"].ToString()) / Totalunits) * 100, 2);
                    spnHawaiian.InnerText = perHawaiian.ToString() + " %";

                    decimal perMultiRacial = Math.Round((DataUtils.GetDecimal(drPortfolioData["MultiRacial"].ToString()) / Totalunits) * 100, 2);
                    spnMultiRacial.InnerText = perMultiRacial.ToString() + " %";

                    decimal perUnknownRace = Math.Round((DataUtils.GetDecimal(drPortfolioData["UnknownRace"].ToString()) / Totalunits) * 100, 2);
                    spnUnknownRace.InnerText = perUnknownRace.ToString() + " %";

                    //decimal perHispanic = Math.Round((DataUtils.GetDecimal(drPortfolioData["Hispanic"].ToString()) / Totalunits) * 100, 2);
                    //spnHispanic.InnerText = perHispanic.ToString() + " %";

                    //decimal perNonHisp = Math.Round((DataUtils.GetDecimal(drPortfolioData["NonHisp"].ToString()) / Totalunits) * 100, 2);
                    //spnNonHisp.InnerText = perNonHisp.ToString() + " %";

                    decimal perUnknownEthnicity = Math.Round((DataUtils.GetDecimal(drPortfolioData["UnknownEthnicity"].ToString()) / Totalunits) * 100, 2);
                    spnUnknownEthnicity.InnerText = perUnknownEthnicity.ToString() + " %";

                    decimal perHomeless = Math.Round((DataUtils.GetDecimal(drPortfolioData["Homeless"].ToString()) / Totalunits) * 100, 2);
                    spnHomeless.InnerText = perHomeless.ToString() + " %";


                    decimal perMarketRate = Math.Round((DataUtils.GetDecimal(drPortfolioData["MarketRate"].ToString()) / Totalunits) * 100, 2);
                    spntMarketRate.InnerText = perMarketRate.ToString() + " %";


                    decimal perI100 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I100"].ToString()) / Totalunits) * 100, 2);
                    spnI100.InnerText = perI100.ToString() + " %";


                    decimal perI80 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I80"].ToString()) / Totalunits) * 100, 2);
                    spnI80.InnerText = perI80.ToString() + " %";


                    decimal perI75 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I75"].ToString()) / Totalunits) * 100, 2);
                    spnI75.InnerText = perI75.ToString() + " %";


                    decimal perI60 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I60"].ToString()) / Totalunits) * 100, 2);
                    spnI60.InnerText = perI60.ToString() + " %";


                    decimal perI50 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I50"].ToString()) / Totalunits) * 100, 2);
                    spnI50.InnerText = perI50.ToString() + " %";


                    decimal perI30 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I30"].ToString()) / Totalunits) * 100, 2);
                    spnI30.InnerText = perI30.ToString() + " %";


                    decimal perI120 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I120"].ToString()) / Totalunits) * 100, 2);
                    spnI20.InnerText = perI120.ToString() + " %";

                    txtOtherGender.Text = drPortfolioData["OtherGender"].ToString();
                    decimal perOtherGender = Math.Round((DataUtils.GetDecimal(drPortfolioData["OtherGender"].ToString()) / Totalunits) * 100, 2);
                    spnOtherGender.InnerText = perOtherGender.ToString() + " %";

                    txtVacanciesGender.Text = drPortfolioData["VacanciesGender"].ToString();
                    decimal perVacanciesGender = Math.Round((DataUtils.GetDecimal(drPortfolioData["VacanciesGender"].ToString()) / Totalunits) * 100, 2);
                    spnVacanciesGender.InnerText = perVacanciesGender.ToString() + " %";

                    txtVacanciesRace.Text = drPortfolioData["VacanciesRace"].ToString();
                    decimal perVacanciesRace = Math.Round((DataUtils.GetDecimal(drPortfolioData["VacanciesRace"].ToString()) / Totalunits) * 100, 2);
                    spnVacanciesRace.InnerText = perVacanciesRace.ToString() + " %";

                    txtLatinx.Text = drPortfolioData["Latinx"].ToString();
                    decimal perLatinx = Math.Round((DataUtils.GetDecimal(drPortfolioData["Latinx"].ToString()) / Totalunits) * 100, 2);
                    spnLatinx.InnerText = perLatinx.ToString() + " %";

                    txtNonLatinx.Text = drPortfolioData["NonLatinx"].ToString();
                    decimal perNonLatinx = Math.Round((DataUtils.GetDecimal(drPortfolioData["NonLatinx"].ToString()) / Totalunits) * 100, 2);
                    spnNonLatinx.InnerText = perNonLatinx.ToString() + " %";

                    txtVacanciesEthnicity.Text = drPortfolioData["VacanciesEthnicity"].ToString();
                    decimal perVacanciesEthnicity = Math.Round((DataUtils.GetDecimal(drPortfolioData["VacanciesEthnicity"].ToString()) / Totalunits) * 100, 2);
                    spnVacanciesEthnicity.InnerText = perVacanciesEthnicity.ToString() + " %";

                    txtNonbinary.Text = drPortfolioData["Nonbinary"].ToString();
                    decimal nonBinary = Math.Round((DataUtils.GetDecimal(drPortfolioData["Nonbinary"].ToString()) / Totalunits) * 100, 2);
                    spnNonbinary.InnerText = nonBinary.ToString() + " %";
                }

            }
               // btnSubmit.Text = "Update";
        }

        private void PopulateProjectDetails()
        {
            DataRow dr = ProjectMaintenanceData.GetProjectNameById(ProjectId);
            if (dr != null)
            {
                //ProjectNum.InnerText = dr["ProjNumber"].ToString();
                ProjName.InnerText = dr["ProjectName"].ToString();
            }
        }

        private void BindControls()
        {
            //BindLookUP(ddlyear, 76);
            //BindLookUP(ddlPortfolioType, 2287);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text.ToLower() == "save")
                {
                    Save();
                    LogMessage("Portfolio data updated successfully");
                }
            }
            catch (Exception ex)
            {
                LogError(Pagename, "btnSave_Click", "", ex.Message);
            }
        }

        public static void GetExagoURLForReport(string Projnum, string ReportName, List<string> EmailList, string PortfolioType, string Year)
        {
            string URL = string.Empty;
            Api api = new Api(@"/eWebReports");

            DataSource ds = api.DataSources.GetDataSource("VHCB");
            ds.DataConnStr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            // Set the action to execute the report
            api.Action = wrApiAction.ExecuteReport;
            WebReports.Api.Common.Parameter parameter = api.Parameters.GetParameter("Projnum");
            parameter.Value = Projnum;
            parameter.IsHidden = true;

            WebReports.Api.Common.Parameter parameter1 = api.Parameters.GetParameter("PortfolioType");
            parameter1.Value = PortfolioType;
            parameter1.IsHidden = true;

            WebReports.Api.Common.Parameter parameter2 = api.Parameters.GetParameter("Year");
            parameter2.Value = Year;
            parameter2.IsHidden = true;

            api.SetupData.StorageMgmtConfig.SetIdentity("userId", "Dherman");
            api.SetupData.StorageMgmtConfig.SetIdentity("companyId", "VHCB");


            ReportObject report = api.ReportObjectFactory.LoadFromRepository(@"Housing\" + ReportName);

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

            string subject = $"Online Housing Portfolio Application ({Projnum})";

            ReportScheduleInfo newSchedule = new ReportScheduleInfoOnce()
            {
                ScheduleName = "Online Housing Portfolio Application",             // Schedule name
                ReportType = wrReportType.Advanced,            // Report type
                RangeStartDate = new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),                   // Start date
                ScheduleTime = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute), // Start time
                SendReportInEmail = true,                             // Email or save
                EmailSubject = subject,
                EmailBody = "PDF of your Online Housing Portfolio Application"
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

        private void Save()
        {
            PortfolioDataData.UpdateProjectPortfolio(DataUtils.GetInt(hfProjectPortfolioID.Value), DataUtils.GetInt(ddlPortfolioType.SelectedValue), ddlYear.SelectedItem.Text, DataUtils.GetInt(txtTotalUnits.Text),
                     DataUtils.GetInt(txtMGender.Text), DataUtils.GetInt(txtFGender.Text), DataUtils.GetInt(txtUGender.Text), DataUtils.GetInt(txtWhite.Text),
                     DataUtils.GetInt(txtBlack.Text), DataUtils.GetInt(txtAsian.Text), DataUtils.GetInt(txtIndian.Text), DataUtils.GetInt(txtHawaiian.Text), DataUtils.GetInt(txtMultiRacial.Text),
                     DataUtils.GetInt(txtUnknownRace.Text),0,0,// DataUtils.GetInt(txtHispanic.Text), DataUtils.GetInt(txtNonHisp.Text), 
                     DataUtils.GetInt(txtUnknownEthnicity.Text),
                     DataUtils.GetInt(txtHomeless.Text), DataUtils.GetInt(txtMarketRate.Text), DataUtils.GetInt(txtI100.Text), DataUtils.GetInt(txtI80.Text),
                     DataUtils.GetInt(txtI75.Text), DataUtils.GetInt(txtI60.Text), DataUtils.GetInt(txtI50.Text), DataUtils.GetInt(txtI30.Text), DataUtils.GetInt(txtI20.Text), ProjectId,
                      DataUtils.GetInt(txtOtherGender.Text), DataUtils.GetInt(txtNonbinary.Text), DataUtils.GetInt(txtVacanciesGender.Text), DataUtils.GetInt(txtVacanciesRace.Text), DataUtils.GetInt(txtLatinx.Text), DataUtils.GetInt(txtNonLatinx.Text), DataUtils.GetInt(txtVacanciesEthnicity.Text));
                      ClearForm();
        }

        private void ClearForm()
        {
            //ddlYear.SelectedIndex = -1;
            //ddlPortfolioType.SelectedIndex = -1;
            txtTotalUnits.Text = "";
            txtMGender.Text = ""; txtFGender.Text = ""; txtUGender.Text = ""; txtWhite.Text = "";
            txtBlack.Text = ""; txtAsian.Text = ""; txtIndian.Text = ""; txtHawaiian.Text = ""; txtMultiRacial.Text = "";
            txtUnknownRace.Text = ""; //txtHispanic.Text = ""; txtNonHisp.Text = ""; 
            txtUnknownEthnicity.Text = "";
            txtHomeless.Text = ""; txtMarketRate.Text = ""; txtI100.Text = ""; txtI80.Text = "";
            txtI75.Text = ""; txtI60.Text = ""; txtI50.Text = ""; txtI30.Text = ""; txtI20.Text = "";
            txtNonbinary.Text = "";
            txtOtherGender.Text = "";
            txtVacanciesGender.Text = "";

            txtVacanciesRace.Text = "";
            txtLatinx.Text = "";
            txtNonLatinx.Text = "";
            txtVacanciesEthnicity.Text = "";

            spnAsian.InnerHtml = "";
            spnBlack.InnerHtml = "";
            spnFeMale.InnerHtml = "";
            spnHawaiian.InnerHtml = "";
            //spnHispanic.InnerHtml = "";
            spnHomeless.InnerHtml = "";
            spnI100.InnerHtml = "";
            spnI20.InnerHtml = "";
            spnI30.InnerHtml = "";
            spnI60.InnerHtml = "";
            spnI50.InnerHtml = "";
            spnI75.InnerHtml = "";
            spnI80.InnerHtml = "";
            spnIndian.InnerHtml = "";
            spnMale.InnerHtml = "";
            //spnNonHisp.InnerHtml = "";
            spntMarketRate.InnerHtml = "";
            spnUGender.InnerHtml = "";
            spnVacanciesGender.InnerHtml = "";
            spnUnknownEthnicity.InnerHtml = "";
            spnUnknownRace.InnerHtml = "";
            spnWhite.InnerHtml = "";
            spnMultiRacial.InnerHtml = "";

            spnVacanciesRace.InnerHtml = "";
            spnLatinx.InnerHtml = "";
            spnNonLatinx.InnerHtml = "";
            spnVacanciesEthnicity.InnerHtml = "";
            spnOtherGender.InnerHtml = "";
            spnNonbinary.InnerHtml = "";
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

        //protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string year = ddlyear.SelectedItem.Text.ToString();
        //    hfProjectPortfolioID.Value = "";
        //    btnSubmit.Text = "Submit";
        //    ClearForm();

        //    if (ddlyear.SelectedIndex != 0)
        //    {
        //        DataRow drPortfolioData = PortfolioDataData.GetPortfolioData(DataUtils.GetInt(hfProjectId.Value), year);

        //        if (drPortfolioData != null)
        //        {
        //            hfProjectPortfolioID.Value = drPortfolioData["ProjectPortfolioID"].ToString();
        //            int Totalunits = DataUtils.GetInt(drPortfolioData["TotalUnits"].ToString());
        //            PopulateDropDown(ddlPortfolioType, drPortfolioData["PortfolioType"].ToString());
        //            txtTotalUnits.Text = drPortfolioData["TotalUnits"].ToString();

        //            txtMGender.Text = drPortfolioData["MGender"].ToString();
        //            decimal perMale = Math.Round((DataUtils.GetDecimal(drPortfolioData["MGender"].ToString()) / Totalunits) * 100, 2);
        //            spnMale.InnerText = perMale.ToString() + " %";

        //            txtFGender.Text = drPortfolioData["FGender"].ToString();
        //            decimal perFeMale = Math.Round((DataUtils.GetDecimal(drPortfolioData["FGender"].ToString()) / Totalunits) * 100, 2);
        //            spnFeMale.InnerText = perFeMale.ToString() + " %";

        //            txtUGender.Text = drPortfolioData["UGender"].ToString();
        //            decimal perUGender = Math.Round((DataUtils.GetDecimal(drPortfolioData["UGender"].ToString()) / Totalunits) * 100, 2);
        //            spnUgender.InnerText = perUGender.ToString() + " %";

        //            txtWhite.Text = drPortfolioData["White"].ToString();
        //            decimal perWhite = Math.Round((DataUtils.GetDecimal(drPortfolioData["White"].ToString()) / Totalunits) * 100, 2);
        //            spnWhite.InnerText = perWhite.ToString() + " %";

        //            txtBlack.Text = drPortfolioData["Black"].ToString();
        //            decimal perBlack = Math.Round((DataUtils.GetDecimal(drPortfolioData["Black"].ToString()) / Totalunits) * 100, 2);
        //            spnBlack.InnerText = perBlack.ToString() + " %";

        //            txtAsian.Text = drPortfolioData["Asian"].ToString();
        //            decimal perAsian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Asian"].ToString()) / Totalunits) * 100, 2);
        //            spnAsian.InnerText = perAsian.ToString() + " %";

        //            txtIndian.Text = drPortfolioData["Indian"].ToString();
        //            decimal perIndian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Indian"].ToString()) / Totalunits) * 100, 2);
        //            spnIndian.InnerText = perIndian.ToString() + " %";

        //            txtHawaiian.Text = drPortfolioData["Hawaiian"].ToString();
        //            decimal perHawaiian = Math.Round((DataUtils.GetDecimal(drPortfolioData["Hawaiian"].ToString()) / Totalunits) * 100, 2);
        //            spnHawaiian.InnerText = perHawaiian.ToString() + " %";

        //            txtUnknownRace.Text = drPortfolioData["UnknownRace"].ToString();
        //            decimal perUnknownRace = Math.Round((DataUtils.GetDecimal(drPortfolioData["UnknownRace"].ToString()) / Totalunits) * 100, 2);
        //            spnUnknownRace.InnerText = perUnknownRace.ToString() + " %";

        //            txtHispanic.Text = drPortfolioData["Hispanic"].ToString();
        //            decimal perHispanic = Math.Round((DataUtils.GetDecimal(drPortfolioData["Hispanic"].ToString()) / Totalunits) * 100, 2);
        //            spnHispanic.InnerText = perHispanic.ToString() + " %";

        //            txtNonHisp.Text = drPortfolioData["NonHisp"].ToString();
        //            decimal perNonHisp = Math.Round((DataUtils.GetDecimal(drPortfolioData["NonHisp"].ToString()) / Totalunits) * 100, 2);
        //            spnNonHisp.InnerText = perNonHisp.ToString() + " %";

        //            txtUnknownEthnicity.Text = drPortfolioData["UnknownEthnicity"].ToString();
        //            decimal perUnknownEthnicity = Math.Round((DataUtils.GetDecimal(drPortfolioData["UnknownEthnicity"].ToString()) / Totalunits) * 100, 2);
        //            spnUnknownEthnicity.InnerText = perUnknownEthnicity.ToString() + " %";

        //            txtHomeless.Text = drPortfolioData["Homeless"].ToString();
        //            decimal perHomeless = Math.Round((DataUtils.GetDecimal(drPortfolioData["Homeless"].ToString()) / Totalunits) * 100, 2);
        //            spnHomeless.InnerText = perHomeless.ToString() + " %";

        //            txtMarketRate.Text = drPortfolioData["MarketRate"].ToString();
        //            decimal perMarketRate = Math.Round((DataUtils.GetDecimal(drPortfolioData["MarketRate"].ToString()) / Totalunits) * 100, 2);
        //            spntMarketRate.InnerText = perMarketRate.ToString() + " %";

        //            txtI100.Text = drPortfolioData["I100"].ToString();
        //            decimal perI100 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I100"].ToString()) / Totalunits) * 100, 2);
        //            spnI100.InnerText = perI100.ToString() + " %";

        //            txtI80.Text = drPortfolioData["I80"].ToString();
        //            decimal perI80 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I80"].ToString()) / Totalunits) * 100, 2);
        //            spnI80.InnerText = perI80.ToString() + " %";

        //            txtI75.Text = drPortfolioData["I75"].ToString();
        //            decimal perI75 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I75"].ToString()) / Totalunits) * 100, 2);
        //            spnI75.InnerText = perI75.ToString() + " %";

        //            txtI60.Text = drPortfolioData["I60"].ToString();
        //            decimal perI60 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I60"].ToString()) / Totalunits) * 100, 2);
        //            spnI60.InnerText = perI60.ToString() + " %";

        //            txtI50.Text = drPortfolioData["I50"].ToString();
        //            decimal perI50 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I50"].ToString()) / Totalunits) * 100, 2);
        //            spnI50.InnerText = perI50.ToString() + " %";

        //            txtI30.Text = drPortfolioData["I30"].ToString();
        //            decimal perI30 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I30"].ToString()) / Totalunits) * 100, 2);
        //            spnI30.InnerText = perI30.ToString() + " %";

        //            txtI20.Text = drPortfolioData["I120"].ToString();
        //            decimal perI120 = Math.Round((DataUtils.GetDecimal(drPortfolioData["I120"].ToString()) / Totalunits) * 100, 2);
        //            spnI20.InnerText = perI120.ToString() + " %";

        //            btnSubmit.Text = "Update";
        //        }
        //    }
        //}

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();

            List<string> EmailList = ViabilityApplicationData.GetMailAddressesForPDFEmail(projectNumber).Rows.OfType<DataRow>().Select(dr => dr.Field<string>("EmailAddress")).ToList();

            if (EmailList.Count > 0)
                GetExagoURLForReport(projectNumber, "Housing Portfolio Data-Portrait", EmailList, hfPortfolioTypeName.Value.ToString(), hfYear.Value.ToString());

            ViabilityApplicationData.SubmitApplication(projectNumber, ddlYear.SelectedValue, DataUtils.GetInt(ddlPortfolioType.SelectedValue));

            LogMessage("Porfolio Online Application Submitted Successfully");

            Response.Redirect("Login.aspx");
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex != 0)
                PopulatePortfolioTypes();
            else
            {
                ddlPortfolioType.Items.Clear();
                ClearForm();
            }
        }

        private void PopulatePortfolioTypes()
        {
            try
            {
                ddlPortfolioType.Items.Clear();
                ddlPortfolioType.DataSource = PortfolioDataData.GetPopulatePortfolioTypes(Session["UserId"].ToString(), projectNumber, ddlYear.SelectedValue);
                ddlPortfolioType.DataValueField = "PortfolioTypeID";
                ddlPortfolioType.DataTextField = "PortfolioType";
                ddlPortfolioType.DataBind();
                ddlPortfolioType.Items.Insert(0, new ListItem("Select", "NA"));
            }
            catch (Exception ex)
            {
                LogError(Pagename, "PopulatePortfolioTypes", "Control ID:" + ddlPortfolioType.ID, ex.Message);
            }
        }

        protected void ddlPortfolioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPortfolioType.SelectedIndex != 0)
                PopulateForm();
            else
                ClearForm();
        }
    }
}