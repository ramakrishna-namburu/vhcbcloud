using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VHCBCommon.DataAccessLayer;
using VHCBCommon.DataAccessLayer.Conservation;

namespace DataAccessLayer
{
    public class LookupValuesData
    {
        public static DataTable Getlookupvalues(int LookupType)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "get_lookup_values";
                        command.Parameters.Add(new SqlParameter("lookuptype", LookupType));
                        
                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetSubLookupValues(int TypeId)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "get_sublookup_values";
                        command.Parameters.Add(new SqlParameter("TypeId", TypeId));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetManagers()
        {
            DataTable dtManagers = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "get_managers";
                        
                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtManagers = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtManagers;
        }

        public static DataTable GetBoardDates()
        {
            DataTable dtBoardDates = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetBoardDates";

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtBoardDates = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtBoardDates;
        }

        public static DataTable GetTop6BoardDates(string MeetingType)
        {
            DataTable dtBoardDates = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("MeetingType", MeetingType));
                        command.CommandText = "GetTop6BoardDates";

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtBoardDates = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtBoardDates;
        }

        public static DataTable GetHUCList(string OrderBy)
        {
            DataTable dtManagers = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetHUCList";
                        command.Parameters.Add(new SqlParameter("OrderBy", OrderBy));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtManagers = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtManagers;
        }

        public static DataTable GetlookupvaluesSPZ(int LookupType)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "BindLookSPZUP";
                        command.Parameters.Add(new SqlParameter("lookuptype", LookupType));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetlookupvaluesET(int LookupType)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "BindLookETUP";
                        command.Parameters.Add(new SqlParameter("lookuptype", LookupType));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetWQPerformanceMeasures(int ProjectTypeID, bool IsActiveOnly)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQPerformanceMeasures";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static void UpdateGetWQPerformanceMeasures(int WQPerformanceTypeID, string PerformanceMeasure, bool RowIsActive)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateGetWQPerformanceMeasures";
                command.Parameters.Add(new SqlParameter("WQPerformanceTypeID", WQPerformanceTypeID));
                command.Parameters.Add(new SqlParameter("PerformanceMeasure", PerformanceMeasure));
                command.Parameters.Add(new SqlParameter("RowIsActive", RowIsActive));

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

        public static Result AddWQPerformanceMeasures(int ProjectTypeID, string PerformanceMeasure)
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
                        command.CommandText = "AddWQPerformanceMeasures";

                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("PerformanceMeasure", PerformanceMeasure));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        Result objResult = new Result();

                        objResult.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        objResult.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return objResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Result AddWQDeliverables(int ProjectTypeID, string Deliverable)
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
                        command.CommandText = "AddWQDeliverables";

                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("Deliverable", Deliverable));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        Result objResult = new Result();

                        objResult.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        objResult.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return objResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateWQDeliverables(int WQDeliverablesID, string Deliverable, bool RowIsActive)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateWQDeliverables";
                command.Parameters.Add(new SqlParameter("WQDeliverablesID", WQDeliverablesID));
                command.Parameters.Add(new SqlParameter("Deliverable", Deliverable));
                command.Parameters.Add(new SqlParameter("RowIsActive", RowIsActive));

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

        public static DataTable GetWQDeliverables(int ProjectTypeID, bool IsActiveOnly)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQDeliverables";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetWQPerformanceMeasuresByProjectTypeId(int ProjectTypeID)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQPerformanceMeasuresByProjectTypeId";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetWQDeliverablesByProjectTypeId(int ProjectTypeID)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQDeliverablesByProjectTypeId";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static DataTable GetWQMilestones(int ProjectTypeID, bool IsActiveOnly)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQMilestones";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static void UpdateWQMilestones(int WQProjectMSTypeID, string Milestone, bool RowIsActive)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateWQMilestones";
                command.Parameters.Add(new SqlParameter("WQProjectMSTypeID", WQProjectMSTypeID));
                command.Parameters.Add(new SqlParameter("Milestone", Milestone));
                command.Parameters.Add(new SqlParameter("RowIsActive", RowIsActive));

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

        public static Result AddWQMilestones(int ProjectTypeID, string Milestone)
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
                        command.CommandText = "AddWQMilestones";

                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
                        command.Parameters.Add(new SqlParameter("Milestone", Milestone));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        Result objResult = new Result();

                        objResult.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        objResult.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return objResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetWQProjectMilestonesByProjectTypeId(int ProjectTypeID)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQProjectMilestonesByProjectTypeId";
                        command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static Result AddWQSubWatersheds(int Watershed, string SubWaterhed, string HUC12)
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
                        command.CommandText = "AddWQSubWatersheds";

                        command.Parameters.Add(new SqlParameter("Watershed", Watershed));
                        command.Parameters.Add(new SqlParameter("SubWaterhed", SubWaterhed));
                        command.Parameters.Add(new SqlParameter("HUC12", HUC12));

                        SqlParameter parmMessage = new SqlParameter("@isDuplicate", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        SqlParameter parmMessage1 = new SqlParameter("@isActive", SqlDbType.Int);
                        parmMessage1.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage1);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        Result objResult = new Result();

                        objResult.IsDuplicate = DataUtils.GetBool(command.Parameters["@isDuplicate"].Value.ToString());
                        objResult.IsActive = DataUtils.GetBool(command.Parameters["@isActive"].Value.ToString());

                        return objResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetWQSubWatersheds(int Watershed, bool IsActiveOnly)
        {
            DataTable dtLookupvalues = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetWQSubWatersheds";
                        command.Parameters.Add(new SqlParameter("Watershed", Watershed));
                        command.Parameters.Add(new SqlParameter("IsActiveOnly", IsActiveOnly));

                        command.CommandTimeout = 60 * 5;

                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);

                        da.Fill(ds);

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                        {
                            dtLookupvalues = ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtLookupvalues;
        }

        public static void UpdateWQSubWatersheds(int WQSubWatershedID, string SubWaterhed, string HUC12, bool RowIsActive)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateWQSubWatersheds";
                command.Parameters.Add(new SqlParameter("WQSubWatershedID", WQSubWatershedID));
                command.Parameters.Add(new SqlParameter("SubWaterhed", SubWaterhed));
                command.Parameters.Add(new SqlParameter("HUC12", HUC12));
                command.Parameters.Add(new SqlParameter("RowIsActive", RowIsActive));

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
