using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VHCBCommon.DataAccessLayer
{
    public class ConserveNatRecData
    {
        public static void ConserveNatRecPage1(string ProjNumber, DateTime BoardMeetDate, decimal ConservedAcres, string Funds_Requested, string Total_Expenses,
       string App_Organ, string Project_Manager, string App_Phone, string App_Email, string Landowner_Names,
       string LOStreet, string LOAdd1, string LOAdd2, string LOTown, string LOZip, string LOCounty, string LOEmail, string LOHomephone, string LOCell,
       string FarmerName, //string FarmerStreet, string FarmerAdd1, string FarmerAdd2, string FarmerTown, string FarmerZip, string FarmerCounty, string FarmerEmail, string FarmerHomePhone, string FarmerCell,
       string PropertyStreet, string PropertyAdd1, string PropertyTown, string PropertyZip, string Propertycounty,
                    //string ProposedStreet, string ProposedAdd1, string ProposedAdd2, string ProposedTown, string ProposedZip, string PropsedCounty,
                    string ProposedContact, string ProposedEmail, string ProposedHomePhone, string ProposedCellPhone, string ProposedRelation,
            bool Notify, DateTime App_Date, string FarmerTransfer, string AppCellPhone

            )
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ConserveNatRecPage1";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                //command.Parameters.Add(new SqlParameter("DateSubmit", DateSubmit.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : DateSubmit));
                command.Parameters.Add(new SqlParameter("BoardMeetDate", BoardMeetDate.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : BoardMeetDate));
                command.Parameters.Add(new SqlParameter("ConservedAcres", ConservedAcres));
                command.Parameters.Add(new SqlParameter("Funds_Requested", Funds_Requested));
                command.Parameters.Add(new SqlParameter("Total_Expenses", Total_Expenses));
                command.Parameters.Add(new SqlParameter("App_Organ", App_Organ));
                command.Parameters.Add(new SqlParameter("Project_Manager", Project_Manager));
                command.Parameters.Add(new SqlParameter("App_Phone", App_Phone));
                command.Parameters.Add(new SqlParameter("App_Email", App_Email));
                command.Parameters.Add(new SqlParameter("Landowner_Names", Landowner_Names));
                command.Parameters.Add(new SqlParameter("LOStreet", LOStreet));
                command.Parameters.Add(new SqlParameter("LOAdd1", LOAdd1));
                command.Parameters.Add(new SqlParameter("LOAdd2", LOAdd2));
                command.Parameters.Add(new SqlParameter("LOTown", LOTown));
                command.Parameters.Add(new SqlParameter("LOZip", LOZip));
                // command.Parameters.Add(new SqlParameter("LOVillage", LOVillage));
                command.Parameters.Add(new SqlParameter("LOCounty", LOCounty));
                command.Parameters.Add(new SqlParameter("LOEmail", LOEmail));
                command.Parameters.Add(new SqlParameter("LOHomephone", LOHomephone));
                command.Parameters.Add(new SqlParameter("LOCell", LOCell));
                command.Parameters.Add(new SqlParameter("FarmerName", FarmerName));
                //command.Parameters.Add(new SqlParameter("FarmerStreet", FarmerStreet));
                //command.Parameters.Add(new SqlParameter("FarmerAdd1", FarmerAdd1));
                //command.Parameters.Add(new SqlParameter("FarmerAdd2", FarmerAdd2));
                //command.Parameters.Add(new SqlParameter("FarmerTown", FarmerTown));
                //command.Parameters.Add(new SqlParameter("FarmerZip", FarmerZip));
                ////command.Parameters.Add(new SqlParameter("FarmerVillage", FarmerVillage));
                //command.Parameters.Add(new SqlParameter("FarmerCounty", FarmerCounty));
                //command.Parameters.Add(new SqlParameter("FarmerEmail", FarmerEmail));
                //command.Parameters.Add(new SqlParameter("FarmerHomePhone", FarmerHomePhone));
                //command.Parameters.Add(new SqlParameter("FarmerCell", FarmerCell));
                command.Parameters.Add(new SqlParameter("PropertyStreet", PropertyStreet));
                command.Parameters.Add(new SqlParameter("PropertyAdd1", PropertyAdd1));
                command.Parameters.Add(new SqlParameter("PropertyTown", PropertyTown));
                command.Parameters.Add(new SqlParameter("Propertycounty", Propertycounty));
                command.Parameters.Add(new SqlParameter("PropertyZip", PropertyZip));

                //command.Parameters.Add(new SqlParameter("ProposedStreet", ProposedStreet));
                //command.Parameters.Add(new SqlParameter("ProposedAdd1", ProposedAdd1));
                //command.Parameters.Add(new SqlParameter("ProposedAdd2", ProposedAdd2));
                //command.Parameters.Add(new SqlParameter("ProposedTown", ProposedTown));
                //command.Parameters.Add(new SqlParameter("ProposedZip", ProposedZip));
                //command.Parameters.Add(new SqlParameter("PropsedCounty", PropsedCounty));
                command.Parameters.Add(new SqlParameter("ProposedContact", ProposedContact));
                command.Parameters.Add(new SqlParameter("ProposedEmail", ProposedEmail));
                command.Parameters.Add(new SqlParameter("ProposedHomePhone", ProposedHomePhone));
                command.Parameters.Add(new SqlParameter("ProposedCellPhone", ProposedCellPhone));
                command.Parameters.Add(new SqlParameter("ProposedRelation", ProposedRelation));


                command.Parameters.Add(new SqlParameter("App_Date", App_Date.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : App_Date));

                command.Parameters.Add(new SqlParameter("FarmerTransfer", FarmerTransfer));

                command.Parameters.Add(new SqlParameter("AppCellPhone", AppCellPhone));
                command.Parameters.Add(new SqlParameter("Notify", Notify));

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataRow GetConserveNatRecPage1(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetConserveNatRecPage1";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static void ConserveNatRecPage3(string ProjNumber, string ExecSummary)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ConserveNatRecPage3";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("ExecSummary", ExecSummary));
                //command.Parameters.Add(new SqlParameter("SellorConvey", SellorConvey));
                //// command.Parameters.Add(new SqlParameter("FarmerTransfer", FarmerTransfer));
                //command.Parameters.Add(new SqlParameter("FarmerPlans", FarmerPlans));
                //command.Parameters.Add(new SqlParameter("SellorPlans", SellorPlans));

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataRow GetConserveNatRecPage3(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetConserveNatRecPage3";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static DataRow GetConserveNarRecPage4New(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetConserveNarRecPage4New";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static void ConserveNarRecPage4(string ProjNumber, bool SurfaceWaters, string TacticalBasin, string Watershed1,
    string SubBasinHUC8, string SubWatershedHUC12, string Watershed2, string SubBasin2HUC8, string SubWatershed2HUC12, decimal Wetlands, decimal FloodPlain,

     //string WaterQualityIssues,
     //string WQManagepractices, string DrainageBasin, bool DrainageDitches, bool DrainageTiles, bool WasteWaterManage, string WasteInfrastructure, string InfrastuctureDescription,
     //bool LivestockExcluded, string ForestManagementPlan, 
     bool ForestManagement,
    DateTime ForestPlanDate,
    //decimal Primefoot, decimal PrimefootPC, decimal PrimeNonFoot, decimal PrimeNonFootPC, decimal PrimeTotal,
    //decimal StatewideFoot, decimal StatewideFootPC, decimal StatewideNonFoot, decimal StatewideNonFootPC, decimal StatewideTotal,
    int PrimWatershedInt, int PrimSubBasinHUC8Int, int PHucId,
    int SecWatershedInt, int SecSubBasinHUC8Int, int SHucId, bool EnrolledUseValue
    )
        {


            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ConserveNarRecPage4";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("SurfaceWaters", SurfaceWaters));
                //command.Parameters.Add(new SqlParameter("SurfaceWaterDesc", SurfaceWaterDesc));
                command.Parameters.Add(new SqlParameter("TacticalBasin", TacticalBasin));
                command.Parameters.Add(new SqlParameter("Watershed1", Watershed1));
                command.Parameters.Add(new SqlParameter("SubBasinHUC8", SubBasinHUC8));
                command.Parameters.Add(new SqlParameter("SubWatershedHUC12", SubWatershedHUC12));
                command.Parameters.Add(new SqlParameter("Watershed2", Watershed2));
                command.Parameters.Add(new SqlParameter("SubBasin2HUC8", SubBasin2HUC8));
                command.Parameters.Add(new SqlParameter("SubWatershed2HUC12", SubWatershed2HUC12));
                command.Parameters.Add(new SqlParameter("Wetlands", Wetlands));
                command.Parameters.Add(new SqlParameter("FloodPlain", FloodPlain));
                //command.Parameters.Add(new SqlParameter("WaterQualityIssues", WaterQualityIssues));
                //command.Parameters.Add(new SqlParameter("WQManagepractices", WQManagepractices));
                //command.Parameters.Add(new SqlParameter("DrainageBasin", DrainageBasin));
                //command.Parameters.Add(new SqlParameter("DrainageDitches", DrainageDitches));
                //command.Parameters.Add(new SqlParameter("DrainageTiles", DrainageTiles));
                //command.Parameters.Add(new SqlParameter("WasteWaterManage", WasteWaterManage));
                //command.Parameters.Add(new SqlParameter("WasteInfrastructure", WasteInfrastructure));
                //command.Parameters.Add(new SqlParameter("InfrastuctureDescription", InfrastuctureDescription));
                //command.Parameters.Add(new SqlParameter("LivestockExcluded", LivestockExcluded));
                command.Parameters.Add(new SqlParameter("ForestManagement", ForestManagement));
                //command.Parameters.Add(new SqlParameter("ForestManagementPlan", ForestManagementPlan));
                command.Parameters.Add(new SqlParameter("ForestPlanDate", ForestPlanDate.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : ForestPlanDate));

                //command.Parameters.Add(new SqlParameter("Primefoot", Primefoot));
                //command.Parameters.Add(new SqlParameter("PrimefootPC", PrimefootPC));
                //command.Parameters.Add(new SqlParameter("PrimeNonFoot", PrimeNonFoot));
                //command.Parameters.Add(new SqlParameter("PrimeNonFootPC", PrimeNonFootPC));
                //command.Parameters.Add(new SqlParameter("PrimeTotal", PrimeTotal));

                //command.Parameters.Add(new SqlParameter("StatewideFoot", StatewideFoot));
                //command.Parameters.Add(new SqlParameter("StatewideFootPC", StatewideFootPC));
                //command.Parameters.Add(new SqlParameter("StatewideNonFoot", StatewideNonFoot));
                //command.Parameters.Add(new SqlParameter("StatewideNonFootPC", StatewideNonFootPC));
                //command.Parameters.Add(new SqlParameter("StatewideTotal", StatewideTotal));

                command.Parameters.Add(new SqlParameter("PrimWatershedInt", PrimWatershedInt));
                command.Parameters.Add(new SqlParameter("PrimSubBasinHUC8Int", PrimSubBasinHUC8Int));
                command.Parameters.Add(new SqlParameter("PHucId", PHucId));

                command.Parameters.Add(new SqlParameter("SecWatershedInt", SecWatershedInt));
                command.Parameters.Add(new SqlParameter("SecSubBasinHUC8Int", SecSubBasinHUC8Int));
                command.Parameters.Add(new SqlParameter("SHucId", SHucId));
                command.Parameters.Add(new SqlParameter("EnrolledUseValue", EnrolledUseValue));
                

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }




        public static void ConserveNatRecPage4NewConserve(string ProjNumber, decimal Pasture, decimal Hay, decimal Unmanaged, decimal FarmResident, decimal Naturalrec, decimal Wooded,
    decimal Sugarbush, decimal Tillable)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ConserveNatRecPage4NewConserve";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("Pasture", Pasture));
                command.Parameters.Add(new SqlParameter("Hay", Hay));
                command.Parameters.Add(new SqlParameter("Unmanaged", Unmanaged));
                command.Parameters.Add(new SqlParameter("FarmResident", FarmResident));
                command.Parameters.Add(new SqlParameter("Naturalrec", Naturalrec));
                command.Parameters.Add(new SqlParameter("Wooded", Wooded));
                command.Parameters.Add(new SqlParameter("Sugarbush", Sugarbush));
                //command.Parameters.Add(new SqlParameter("Taps", Taps));
                command.Parameters.Add(new SqlParameter("Tillable", Tillable));
                //command.Parameters.Add(new SqlParameter("PrimeTotal", PrimeTotal));
                //command.Parameters.Add(new SqlParameter("StatewideTotal", StatewideTotal));


                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataRow GetTempConserveNatRecEasementTerm(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetTempConserveNatRecEasementTerm";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static void TempConserveNatRecEasementTerm(string ProjNumber, string SubDivisionReason, string HistoricSignificance, string EasementTermsOther)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TempConserveNatRecEasementTerm";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("SubDivisionReason", SubDivisionReason));
                command.Parameters.Add(new SqlParameter("HistoricSignificance", HistoricSignificance));
                command.Parameters.Add(new SqlParameter("EasementTermsOther", EasementTermsOther));

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataRow GetTempConserveNatRecTownPlaning(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetTempConserveNatRecTownPlaning";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static void TempConserveNatRecTownPlaning(string ProjNumber, string ZoningDistrict, string MinLotSize, decimal FrontageFeet, bool PublicWater, bool PublicSewer, 
            //bool EnrolledUseValue, decimal AcresExcluded, string AcresDerived, string ExcludedLand,
           string DeedMatch, //bool SurveyRequired, 
           bool RestrictiveCovenants, string DeedRestrictions, string ConformancePlans, string NoLeverage, string PlanCommisionsInformed, string Endorsements)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TempConserveNatRecTownPlaning";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("ZoningDistrict", ZoningDistrict));
                command.Parameters.Add(new SqlParameter("MinLotSize", MinLotSize));
                command.Parameters.Add(new SqlParameter("FrontageFeet", FrontageFeet));
                command.Parameters.Add(new SqlParameter("PublicWater", PublicWater));
                command.Parameters.Add(new SqlParameter("PublicSewer", PublicSewer));
                //command.Parameters.Add(new SqlParameter("EnrolledUseValue", EnrolledUseValue));
                //command.Parameters.Add(new SqlParameter("AcresExcluded", AcresExcluded));
                //command.Parameters.Add(new SqlParameter("AcresDerived", AcresDerived));
                //command.Parameters.Add(new SqlParameter("ExcludedLand", ExcludedLand));
                command.Parameters.Add(new SqlParameter("DeedMatch", DeedMatch));
               // command.Parameters.Add(new SqlParameter("SurveyRequired", SurveyRequired));
                command.Parameters.Add(new SqlParameter("RestrictiveCovenants", RestrictiveCovenants));
                command.Parameters.Add(new SqlParameter("DeedRestrictions", DeedRestrictions));
                command.Parameters.Add(new SqlParameter("ConformancePlans", ConformancePlans));
                command.Parameters.Add(new SqlParameter("NoLeverage", NoLeverage));
                command.Parameters.Add(new SqlParameter("PlanCommisionsInformed", PlanCommisionsInformed));
                command.Parameters.Add(new SqlParameter("Endorsements", Endorsements));


                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        public static DataRow GetTempConserveNatRecAdditionalInfo(string ProjNumber)
        {
            DataRow dr = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetTempConserveNatRecAdditionalInfo";
                        command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public static void TempConserveNatRecAdditionalInfo(string ProjNumber, string DualGoals, string AdditionalInfo)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TempConserveNatRecAdditionalInfo";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("DualGoals", DualGoals));
                command.Parameters.Add(new SqlParameter("AdditionalInfo", AdditionalInfo));


                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void TempConserveNatRecAttachments(string ProjNumber, string Signature, DateTime Sig_Date, string MissingDocs)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TempConserveNatRecAttachments";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("Signature", Signature));

                command.Parameters.Add(new SqlParameter("Sig_Date", Sig_Date.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : Sig_Date));
                command.Parameters.Add(new SqlParameter("MissingDocs", MissingDocs));

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }



}
