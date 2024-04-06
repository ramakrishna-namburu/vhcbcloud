using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace VHCBCommon.DataAccessLayer
{
    public class WaterQualityData
    {
        public static void InsertWQProjectType(int ProjectID, int ProjectTypeID, decimal Phosphorus, decimal TotalAcres, int TacticalBasin)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertWQProjectType";
                command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                command.Parameters.Add(new SqlParameter("ProjectTypeID", ProjectTypeID));
               // command.Parameters.Add(new SqlParameter("SubDescription", SubDescription));
                command.Parameters.Add(new SqlParameter("Phosphorus", Phosphorus));
                command.Parameters.Add(new SqlParameter("TotalAcres", TotalAcres));
                command.Parameters.Add(new SqlParameter("TacticalBasin", TacticalBasin));

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

        public static DataRow GetWQProjectType(int ProjectId)
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
                        command.CommandText = "GetWQProjectType";
                        command.Parameters.Add(new SqlParameter("ProjectId", ProjectId));

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
    }
}
