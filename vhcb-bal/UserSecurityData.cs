﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHCBCommon.DataAccessLayer
{
    public static class UserSecurityData
    {
        public static DataTable GetData(string ProcName)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = ProcName;
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetPageSecurityBySelection(int RecordId)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPageSecurityBySelection";
                command.Parameters.Add(new SqlParameter("recordid", RecordId));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static void AddUserPageSecurity(int userId, int pageid)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddUserPageSecurity";
                command.Parameters.Add(new SqlParameter("userid", userId));
                command.Parameters.Add(new SqlParameter("pageid", pageid));

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

        public static void AddUserFxnSecurity(int userId, int FxnID)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddUserFxnSecurity";
                command.Parameters.Add(new SqlParameter("userid", userId));
                command.Parameters.Add(new SqlParameter("FxnID", FxnID));

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

        public static DataTable GetuserPageSecurity(int userid)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetuserPageSecurity";
                command.Parameters.Add(new SqlParameter("userid", userid));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetUserFxnSecurity(int userid)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUserFxnSecurity";
                command.Parameters.Add(new SqlParameter("userid", userid));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetMasterPageSecurity(int userid)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetMasterPageSecurity";
                command.Parameters.Add(new SqlParameter("userid", userid));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static void AddUsersToSecurityGroup(int userId, int usergroupId)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddUsersToSecurityGroup";
                command.Parameters.Add(new SqlParameter("userid", userId));
                command.Parameters.Add(new SqlParameter("usergroupid", usergroupId));

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


        public static DataRow GetUserSecurity(string username)
        {
            DataRow dr = null;

            DataTable dt = GetUserId(username);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtGetUserSec = UserSecurityData.GetUserSecurityByUserId(DataUtils.GetInt(dt.Rows[0]["userid"].ToString()));
                dr = dtGetUserSec.Rows[0];
            }
            return dr;
        }

        public static bool IsUserHasHousingProgram(string username)
        {
            try
            {
                DataTable dt = GetUserId(username);
                if (dt != null && dt.Rows.Count > 0)
                {

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "IsUserHasHousingProgram";
                            command.Parameters.Add(new SqlParameter("UserId", DataUtils.GetInt(dt.Rows[0]["userid"].ToString())));

                            SqlParameter parmMessage = new SqlParameter("@IsValid", SqlDbType.Bit);
                            parmMessage.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parmMessage);

                            command.CommandTimeout = 60 * 5;

                            command.ExecuteNonQuery();

                            return DataUtils.GetBool(command.Parameters["@IsValid"].Value.ToString());
                        }
                    }
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool GetRoleAuth(string username, int projId)
        {
            bool isVerified = false;
            DataTable dtPrg = new DataTable();


            DataRow drProjectDetails = ProjectMaintenanceData.GetprojectDetails(projId);

            if (drProjectDetails != null)
                isVerified = Convert.ToBoolean(drProjectDetails["verified"].ToString());

            DataTable dt = GetUserId(username);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtGetUserSec = UserSecurityData.GetUserSecurityByUserId(DataUtils.GetInt(dt.Rows[0]["userid"].ToString()));

                if (dtGetUserSec.Rows.Count > 0)
                {
                    if (dtGetUserSec.Rows[0]["usergroupid"].ToString() == "3") // View Only
                    {
                        return false;
                    }
                    else if (dtGetUserSec.Rows[0]["usergroupid"].ToString() == "2") //Program Staff
                    {
                        if (dtGetUserSec.Rows[0]["dfltprg"].ToString() != "") //Default Program
                        {
                            dtPrg = UserSecurityData.GetProjectsByProgram(DataUtils.GetInt(dtGetUserSec.Rows[0]["dfltprg"].ToString()), projId);
                        }
                        if (dtPrg.Rows.Count <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return isVerified;
                        }
                    }
                    else if (dtGetUserSec.Rows[0]["usergroupid"].ToString() == "1") //Program Admin
                    {
                        if (dtGetUserSec.Rows[0]["dfltprg"].ToString() != "")
                        {
                            dtPrg = UserSecurityData.GetProjectsByProgram(DataUtils.GetInt(dtGetUserSec.Rows[0]["dfltprg"].ToString()), projId);
                        }
                        if (dtPrg.Rows.Count <= 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void DeleteUsersUserSecurityGroup(int usersUserSecurityGrpId)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteUsersUserSecurityGroup";
                command.Parameters.Add(new SqlParameter("UsersUserSecurityGrpId", usersUserSecurityGrpId));

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
        public static void DeletePageSecurity(int pagesecurityid)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeletePageSecurity";
                command.Parameters.Add(new SqlParameter("pagesecurityid", pagesecurityid));

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

        public static void DeleteUserFxnSecurity(int UserFxnSecurityId)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteUserFxnSecurity";
                command.Parameters.Add(new SqlParameter("UserFxnSecurityId", UserFxnSecurityId));

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

        public static DataTable GetUserId(string username)
        {
            try
            {
                DataTable dtUser = ProjectCheckRequestData.GetUserByUserName(username);
                //return dtUser != null ? GetMasterPageSecurity(Convert.ToInt32(dtUser.Rows[0][0].ToString())) : null;
                return dtUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable GetProjectsByProgram(int ProgId, int projId)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetProjectsByProgram";
                command.Parameters.Add(new SqlParameter("progId", ProgId));
                command.Parameters.Add(new SqlParameter("projId", projId));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetUserSecurityByUserId(int userid)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUserSecurityByUserId";
                command.Parameters.Add(new SqlParameter("userid", userid));

                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetManagerByProjId(int projId)
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetManagerByProjId";
                command.Parameters.Add(new SqlParameter("projId", projId));
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static string GetMasterPageFileFromSession(int userid)
        {
            DataTable dt = new DataTable();
            dt = GetuserPageSecurity(userid);
            if (dt.Rows.Count > 0)
                return "~/SiteNonAdmin.master";
            else
                return "~/Site.master";
        }

        public static DataTable GetMenuDetailsByUser(int UserId)
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
                        command.CommandText = "GetMenuDetailsByUser";
                        command.Parameters.Add(new SqlParameter("UserId", UserId));

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

        public static bool IsUserHasSameProgramId(int UserId, int ProjectId)
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
                        command.CommandText = "IsUserHasSameProgramId";
                        command.Parameters.Add(new SqlParameter("UserId", UserId));
                        command.Parameters.Add(new SqlParameter("ProjectId", ProjectId));

                        SqlParameter parmMessage = new SqlParameter("@IsValid", SqlDbType.Bit);
                        parmMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parmMessage);

                        command.CommandTimeout = 60 * 5;

                        command.ExecuteNonQuery();

                        return DataUtils.GetBool(command.Parameters["@IsValid"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDefaultUserPageSecurity()
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetDefaultUserPageSecurity";
                
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static DataTable GetDefaultUserFxnSecurity()
        {
            DataTable dtProjects = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetDefaultUserFxnSecurity";
                
                using (connection)
                {
                    connection.Open();
                    command.Connection = connection;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null)
                    {
                        dtProjects = ds.Tables[0];
                    }
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
            return dtProjects;
        }

        public static void DeleteDefaultFxnSecurity(int UserFxnSecurityId)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteDefaultFxnSecurity";
                command.Parameters.Add(new SqlParameter("UserFxnSecurityId", UserFxnSecurityId));

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

        
        public static void DeleteDefaultPageSecurity(int DefPageTypeID)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteDefaultPageSecurity";
                command.Parameters.Add(new SqlParameter("DefPageTypeID", DefPageTypeID));

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

        public static void AddDefaultPageSecurity(int TypeID)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddDefaultPageSecurity";

                command.Parameters.Add(new SqlParameter("TypeID", TypeID));

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

        public static void AddDefaultFxnSecurity(int TypeID)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddDefaultFxnSecurity";

                command.Parameters.Add(new SqlParameter("TypeID", TypeID));

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
