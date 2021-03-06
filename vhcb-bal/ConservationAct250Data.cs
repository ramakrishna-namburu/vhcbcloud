﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VHCBCommon.DataAccessLayer
{
    public class ConservationAct250Data
    {
        #region Act250 Info
        public static DataTable GetAct250FarmsList(bool IsActiveOnly, int Type)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAct250FarmsList";
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));
                        command.Parameters.Add(new SqlParameter("Type", Type));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static ConservationAct250Result AddAct250Farm(string UsePermit, int LkTownDev, int CDist, int Type, 
            string DevName, decimal Primelost, decimal Statelost, decimal TotalAcreslost, decimal AcresDevelop, 
            int Developer, decimal AntFunds, 
            DateTime MitDate, string URL, int FundID, decimal Expectedfunds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AddAct250Farm";

                        command.Parameters.Add(new SqlParameter("UsePermit", UsePermit));
                        command.Parameters.Add(new SqlParameter("LkTownDev", LkTownDev));
                        command.Parameters.Add(new SqlParameter("CDist", CDist));
                        command.Parameters.Add(new SqlParameter("Type", Type));
                        command.Parameters.Add(new SqlParameter("DevName", DevName));
                        command.Parameters.Add(new SqlParameter("Primelost", Primelost));
                        command.Parameters.Add(new SqlParameter("Statelost", Statelost));
                        command.Parameters.Add(new SqlParameter("TotalAcreslost", TotalAcreslost));
                        command.Parameters.Add(new SqlParameter("AcresDevelop", AcresDevelop));
                        command.Parameters.Add(new SqlParameter("Developer", Developer));
                        command.Parameters.Add(new SqlParameter("AntFunds", AntFunds));
                        command.Parameters.Add(new SqlParameter("MitDate", MitDate.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : MitDate));
                        command.Parameters.Add(new SqlParameter("URL", URL));
                        command.Parameters.Add(new SqlParameter("FundID", FundID));
                        command.Parameters.Add(new SqlParameter("Expectedfunds", Expectedfunds));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        ConservationAct250Result ap = new ConservationAct250Result();

                        ap.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        ap.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return ap;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateAct250Farm(int Act250FarmID, int LkTownDev, int CDist, int Type,
            string DevName, decimal Primelost, decimal Statelost, decimal TotalAcreslost, decimal AcresDevelop, 
            int Developer, decimal AntFunds,
            DateTime MitDate, string URL, int FundID, bool IsRowIsActive, string UsePermit, decimal Expectedfunds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdateAct250Farm";

                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("LkTownDev", LkTownDev));
                        command.Parameters.Add(new SqlParameter("CDist", CDist));
                        command.Parameters.Add(new SqlParameter("Type", Type));
                        command.Parameters.Add(new SqlParameter("DevName", DevName));
                        command.Parameters.Add(new SqlParameter("Primelost", Primelost));
                        command.Parameters.Add(new SqlParameter("Statelost", Statelost));
                        command.Parameters.Add(new SqlParameter("TotalAcreslost", TotalAcreslost));
                        command.Parameters.Add(new SqlParameter("AcresDevelop", AcresDevelop));
                        command.Parameters.Add(new SqlParameter("Developer", Developer));
                        command.Parameters.Add(new SqlParameter("AntFunds", AntFunds));
                        command.Parameters.Add(new SqlParameter("MitDate", MitDate.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : MitDate));
                        command.Parameters.Add(new SqlParameter("IsRowIsActive", IsRowIsActive));
                        command.Parameters.Add(new SqlParameter("URL", URL));
                        command.Parameters.Add(new SqlParameter("FundID", FundID));
                        command.Parameters.Add(new SqlParameter("UsePermit", UsePermit));
                        command.Parameters.Add(new SqlParameter("Expectedfunds", Expectedfunds));

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateAct250FarmAntFunds(int Act250FarmID, decimal AntFunds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdateAct250FarmAntFunds";
                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("AntFunds", AntFunds));
                       
                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataRow GetAct250FarmById(int Act250FarmID)
        {
            DataRow dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAct250FarmById";
                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            dt = ds.Tables[0].Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion Act250 Info

        public static DataTable GetAct250PermitCommitList(int Act250FarmID)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAct250PermitCommitList";
                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #region Developer Payments
        public static DataTable GetAct250DevPayList(int Act250FarmID, bool IsActiveOnly)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAct250DevPayList";
                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static ConservationAct250Result AddAct250DevPay(int Act250FarmID, decimal AmtRec, 
            DateTime DateRec, string Units)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AddAct250DevPay";

                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("AmtRec", AmtRec));
                        command.Parameters.Add(new SqlParameter("DateRec", DateRec.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : DateRec));
                        command.Parameters.Add(new SqlParameter("Units", Units));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        ConservationAct250Result ap = new ConservationAct250Result();

                        ap.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        ap.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return ap;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateAct250DevPay(int Act250PayID, decimal AmtRec, DateTime DateRec, 
            string Units, bool IsRowIsActive)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdateAct250DevPay";

                        command.Parameters.Add(new SqlParameter("Act250PayID", Act250PayID));
                        command.Parameters.Add(new SqlParameter("AmtRec", AmtRec));
                        command.Parameters.Add(new SqlParameter("DateRec", DateRec.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : DateRec));
                        command.Parameters.Add(new SqlParameter("Units", Units));
                        command.Parameters.Add(new SqlParameter("IsRowIsActive", IsRowIsActive));

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Developer Payments

        public static DataTable GetLandUsePermitFinancialsList(int Act250FarmId)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetLandUsePermitFinancialsList";
                        command.Parameters.Add(new SqlParameter("Act250FarmId", Act250FarmId));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #region Potential VHCB Projects

        public static DataTable GetAct250ProjectsList(int Act250FarmID, bool IsActiveOnly)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAct250ProjectsList";
                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static ConservationAct250Result AddAct250Projects(int Act250FarmID, int ProjectID, int LKTownConserve, 
            decimal AmtFunds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AddAct250Projects";

                        command.Parameters.Add(new SqlParameter("Act250FarmID", Act250FarmID));
                        command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                        command.Parameters.Add(new SqlParameter("LKTownConserve", LKTownConserve));
                        command.Parameters.Add(new SqlParameter("AmtFunds", AmtFunds));
                        //command.Parameters.Add(new SqlParameter("DateClosed", DateClosed.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : DateClosed));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        ConservationAct250Result ap = new ConservationAct250Result();

                        ap.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        ap.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return ap;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateAct250Projects(int Act250ProjectID, decimal AmtFunds, bool IsRowIsActive)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdateAct250Projects";

                        command.Parameters.Add(new SqlParameter("Act250ProjectID", Act250ProjectID));
                        command.Parameters.Add(new SqlParameter("AmtFunds", AmtFunds));
                        //command.Parameters.Add(new SqlParameter("DateClosed", DateClosed.ToShortDateString() == "1/1/0001" ? System.Data.SqlTypes.SqlDateTime.Null : DateClosed));
                        command.Parameters.Add(new SqlParameter("IsRowIsActive", IsRowIsActive));

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Potential VHCB Projects

        public static DataTable GetConservationTownList(int ProjectId)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetConservationTownList";
                        command.Parameters.Add(new SqlParameter("ProjectId", ProjectId));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }

    public class ConservationAct250Result
    {
        public bool IsDuplicate { set; get; }
        public bool IsActive { set; get; }
    }
}
