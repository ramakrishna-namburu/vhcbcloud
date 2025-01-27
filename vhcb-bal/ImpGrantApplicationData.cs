﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHCBCommon.DataAccessLayer;



namespace VHCBCommon.DataAccessLayer
{
    public class ImpGrantApplicationData
    {
        public static void InsertDefaultDataForImpGrants(string ProjNumber)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertDefaultDataForImpGrants";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                

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

        public static DataRow GetViabilityImpGrantApplicationPage1(string ProjNumber)
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
                        command.CommandText = "GetViabilityImpGrantApplicationPage1";
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

        public static void ViabilityImpGrantApplicationPage1(string ProjNumber, string PrimContact, string AllOwners,
           string MAStreet, string MAAdd1, string MAAdd2, string MACity, string MAZip, string MAVillage, string MACounty,
           string PAStreet, string PAAdd1, string PAAdd2, string PACity, string PAZip, string PAVillage, string PACounty,
           string WorkPhone, string CellPhone, string HomePhone, string Email, int HearAbout)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage1";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                //command.Parameters.Add(new SqlParameter("ProjType", ProjType));
                command.Parameters.Add(new SqlParameter("PrimContact", PrimContact));
                command.Parameters.Add(new SqlParameter("AllOwners", AllOwners));
                //command.Parameters.Add(new SqlParameter("MAContact", MAContact));
                command.Parameters.Add(new SqlParameter("MAStreet", MAStreet));
                command.Parameters.Add(new SqlParameter("MAAdd1", MAAdd1));
                command.Parameters.Add(new SqlParameter("MAAdd2", MAAdd2));
                command.Parameters.Add(new SqlParameter("MACity", MACity));
                command.Parameters.Add(new SqlParameter("MAZip", MAZip));
                command.Parameters.Add(new SqlParameter("MAVillage", MAVillage));
                command.Parameters.Add(new SqlParameter("MACounty", MACounty));
                //command.Parameters.Add(new SqlParameter("PAContact", PAContact));
                command.Parameters.Add(new SqlParameter("PAStreet", PAStreet));
                command.Parameters.Add(new SqlParameter("PAAdd1", PAAdd1));
                command.Parameters.Add(new SqlParameter("PAAdd2", PAAdd2));
                command.Parameters.Add(new SqlParameter("PACity", PACity));
                command.Parameters.Add(new SqlParameter("PAZip", PAZip));
                command.Parameters.Add(new SqlParameter("PAVillage", PAVillage));
                command.Parameters.Add(new SqlParameter("PACounty", PACounty));
                command.Parameters.Add(new SqlParameter("WorkPhone", WorkPhone));
                command.Parameters.Add(new SqlParameter("CellPhone", CellPhone));
                command.Parameters.Add(new SqlParameter("HomePhone", HomePhone));
                command.Parameters.Add(new SqlParameter("Email", Email));
                command.Parameters.Add(new SqlParameter("HearAbout", HearAbout));
       
                //command.Parameters.Add(new SqlParameter("PrimeAdvisor", PrimeAdvisor));
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

        public static DataRow GetViabilityImpGrantApplicationPage2(string ProjNumber)
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
                        command.CommandText = "GetViabilityImpGrantApplicationPage2";
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

        public static void ViabilityImpGrantApplicationPage2(string ProjNumber, string OrgName, string Website, string Org_Structure,
          string Cows, string Hogs, string Poultry, string Other, string Milked_Daily, string Primary_Animals, string Herd, string Rolling_Herd,
          string Milk_Pounds, string Cull, string Somatic, string Milk_Sold, string GrossSales, string Netincome, //string GrossPayroll, string Networth, 
          decimal FamilyFTE, decimal NonFamilyFTE, int FiscalYr, decimal AcresInProduction, decimal AcresOwned, decimal AcresLeased, decimal PastureAcres,
          string LandYouOwn, string LandOwnText)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage2";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("OrgName", OrgName));
                command.Parameters.Add(new SqlParameter("Website", Website));
                command.Parameters.Add(new SqlParameter("Org_Structure", Org_Structure));
                command.Parameters.Add(new SqlParameter("Cows", Cows));
                command.Parameters.Add(new SqlParameter("Hogs", Hogs));
                command.Parameters.Add(new SqlParameter("Poultry", Poultry));
                command.Parameters.Add(new SqlParameter("Other", Other));
                command.Parameters.Add(new SqlParameter("Milked_Daily", Milked_Daily));
                command.Parameters.Add(new SqlParameter("Primary_Animals", Primary_Animals));
                command.Parameters.Add(new SqlParameter("Herd", Herd));
                command.Parameters.Add(new SqlParameter("Rolling_Herd", Rolling_Herd));
                command.Parameters.Add(new SqlParameter("Milk_Pounds", Milk_Pounds));
                command.Parameters.Add(new SqlParameter("Cull", Cull));
                command.Parameters.Add(new SqlParameter("Somatic", Somatic));
                command.Parameters.Add(new SqlParameter("Milk_Sold", Milk_Sold));
                //command.Parameters.Add(new SqlParameter("Dairy_Other", Dairy_Other));
                command.Parameters.Add(new SqlParameter("GrossSales", GrossSales));
                command.Parameters.Add(new SqlParameter("Netincome", Netincome));
                //command.Parameters.Add(new SqlParameter("GrossPayroll", GrossPayroll));
                //command.Parameters.Add(new SqlParameter("Networth", Networth));
                command.Parameters.Add(new SqlParameter("FamilyFTE", FamilyFTE));
                command.Parameters.Add(new SqlParameter("NonFamilyFTE", NonFamilyFTE));
                command.Parameters.Add(new SqlParameter("FiscalYr", FiscalYr));
                command.Parameters.Add(new SqlParameter("AcresInProduction", AcresInProduction));
                command.Parameters.Add(new SqlParameter("AcresOwned", AcresOwned));
                command.Parameters.Add(new SqlParameter("AcresLeased", AcresLeased));
                command.Parameters.Add(new SqlParameter("PastureAcres", PastureAcres));
                command.Parameters.Add(new SqlParameter("LandYouOwn", LandYouOwn));
                command.Parameters.Add(new SqlParameter("LandOwnText", LandOwnText));

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

        public static void ImpGrantsWaterQualityGrants(string ProjNumber, int Farmsize, string FarmsizeText, int PrimaryProduct, string LKProducts, string SecProducts, bool CompletePlanning)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ImpGrantsWaterQualityGrants";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("Farmsize", Farmsize));
                command.Parameters.Add(new SqlParameter("FarmsizeText", FarmsizeText));
               // command.Parameters.Add(new SqlParameter("LKWatershed", LKWatershed));
                command.Parameters.Add(new SqlParameter("PrimaryProduct", PrimaryProduct));
                command.Parameters.Add(new SqlParameter("LKProducts", LKProducts));
                command.Parameters.Add(new SqlParameter("SecProducts", SecProducts));
                command.Parameters.Add(new SqlParameter("CompletePlanning", CompletePlanning));

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

        public static DataRow GetImpGrantsWaterQualityGrants(string ProjNumber)
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
                        command.CommandText = "GetImpGrantsWaterQualityGrants";
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

        public static void InsertImpGrantRequest(string ProjNumber, string ProjTitle, string ProjDesc, decimal ProjCost, decimal Request, string strProjCost, string strRequest, string strGrantMatch, string ENtGrantMatch, 
            string FarmCash, string FarmInKind, string FarmLoan, string StateGrant, string FedGrant, string Other)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertImpGrantRequest";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("ProjTitle", ProjTitle));
                command.Parameters.Add(new SqlParameter("ProjDesc", ProjDesc));
                command.Parameters.Add(new SqlParameter("ProjCost", ProjCost));
                command.Parameters.Add(new SqlParameter("Request", Request));
                command.Parameters.Add(new SqlParameter("strProjCost", strProjCost));
                command.Parameters.Add(new SqlParameter("strRequest", strRequest));
                command.Parameters.Add(new SqlParameter("strGrantMatch", strGrantMatch));
                command.Parameters.Add(new SqlParameter("ENtGrantMatch", ENtGrantMatch));

                command.Parameters.Add(new SqlParameter("FarmCash", FarmCash));
                command.Parameters.Add(new SqlParameter("FarmInKind", FarmInKind));
                command.Parameters.Add(new SqlParameter("FarmLoan", FarmLoan));
                command.Parameters.Add(new SqlParameter("StateGrant", StateGrant));
                command.Parameters.Add(new SqlParameter("FedGrant", FedGrant));
                command.Parameters.Add(new SqlParameter("Other", Other));

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

        public static DataRow GetImpGrantRequest(string ProjNumber)
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
                        command.CommandText = "GetImpGrantRequest";
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

        public static void ViabilityImpGrantApplicationPage6(string ProjNumber, string Budget)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage6";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("Budget", Budget));
                //command.Parameters.Add(new SqlParameter("NRCSExpensesandStatus", NRCSExpensesandStatus));
                //command.Parameters.Add(new SqlParameter("WaverRequest", WaverRequest));

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

        public static DataRow GetViabilityImpGrantApplicationData(string ProjNumber)
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
                        command.CommandText = "GetViabilityImpGrantApplicationData";
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

        public static void ViabilityImpGrantApplicationPage10(string ProjNumber, bool Confident_Sharing, string Confident_Funding, string Confident_Signature, DateTime Confident_Date)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage10";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("Confident_Sharing", Confident_Sharing));
                command.Parameters.Add(new SqlParameter("Confident_Funding", Confident_Funding));
                command.Parameters.Add(new SqlParameter("Confident_Signature", Confident_Signature));
                command.Parameters.Add(new SqlParameter("Confident_Date", Confident_Date.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : Confident_Date));

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

        public static void ViabilityImpGrantApplicationPage7(string ProjNumber, string BusinessOverview, string ProjectDesc, string PlanCoordination)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage7";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("BusinessOverview", BusinessOverview));
                command.Parameters.Add(new SqlParameter("ProjectDesc", ProjectDesc));
                command.Parameters.Add(new SqlParameter("PlanCoordination", PlanCoordination));

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

        public static void ViabilityImpGrantApplicationPage8(string ProjNumber, string ProjectOutcomes, string ProjectTimeline, string Contingencies)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ViabilityImpGrantApplicationPage8";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                command.Parameters.Add(new SqlParameter("ProjectOutcomes", ProjectOutcomes));
                command.Parameters.Add(new SqlParameter("ProjectTimeline", ProjectTimeline));
                command.Parameters.Add(new SqlParameter("Contingencies", Contingencies));

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

        public static DataRow GetEligibility(string ProjNumber)
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
                        command.CommandText = "GetEligibility";
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

        public static void EligibilitySave(string ProjNumber,  string PrimeAdvisor2, string AdvisorOrg, string OtherAdvisor)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "EligibilitySave";
                command.Parameters.Add(new SqlParameter("ProjNumber", ProjNumber));
                //command.Parameters.Add(new SqlParameter("CompletePlanning", CompletePlanning));
                command.Parameters.Add(new SqlParameter("PrimeAdvisor2", PrimeAdvisor2));
                command.Parameters.Add(new SqlParameter("AdvisorOrg", AdvisorOrg));
                command.Parameters.Add(new SqlParameter("OtherAdvisor", OtherAdvisor));

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
