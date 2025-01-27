﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VHCBCommon.DataAccessLayer.Housing
{
    public class PortfolioDataData
    {
        public static DataRow GetPortfolioData(int ProjectId, string Year, int PortfolioType)
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
                        command.CommandText = "GetPortfolioData";
                        command.Parameters.Add(new SqlParameter("ProjectId", ProjectId));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("PortfolioType", PortfolioType));

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

        public static void AddProjectPortfolio1(int PortfolioType, string Year, int ProjectID, int TotalUnits,
            int MGender, int FGender, int UGender, int White, int Black, int Asian, int Indian, int Hawaiian, int MultiRacial, int UnknownRace, int Hispanic, int NonHisp, int UnknownEthnicity, int Homeless,
            int MarketRate, int I100, int I80, int I75, int I60, int I50, int I30, int I120, bool IsSubmit, int OtherGender, int Nonbinary, int VacanciesGender, int VacanciesRace, int Latinx, int NonLatinx, int VacanciesEthnicity)
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
                        command.CommandText = "AddProjectPortfolio1";

                        command.Parameters.Add(new SqlParameter("PortfolioType", PortfolioType));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                        command.Parameters.Add(new SqlParameter("TotalUnits", TotalUnits));
                        command.Parameters.Add(new SqlParameter("MGender", MGender));
                        command.Parameters.Add(new SqlParameter("FGender", FGender));
                        command.Parameters.Add(new SqlParameter("UGender", UGender));
                        command.Parameters.Add(new SqlParameter("White", White));
                        command.Parameters.Add(new SqlParameter("Black", Black));
                        command.Parameters.Add(new SqlParameter("Asian", Asian));
                        command.Parameters.Add(new SqlParameter("Indian", Indian));
                        command.Parameters.Add(new SqlParameter("Hawaiian", Hawaiian));
                        command.Parameters.Add(new SqlParameter("MultiRacial", MultiRacial));
                        command.Parameters.Add(new SqlParameter("UnknownRace", UnknownRace));
                        command.Parameters.Add(new SqlParameter("Hispanic", Hispanic));
                        command.Parameters.Add(new SqlParameter("NonHisp", NonHisp));
                        command.Parameters.Add(new SqlParameter("UnknownEthnicity", UnknownEthnicity));
                        command.Parameters.Add(new SqlParameter("Homeless", Homeless));
                        command.Parameters.Add(new SqlParameter("MarketRate", MarketRate));
                        command.Parameters.Add(new SqlParameter("I100", I100));
                        command.Parameters.Add(new SqlParameter("I80", I80));
                        command.Parameters.Add(new SqlParameter("I75", I75));
                        command.Parameters.Add(new SqlParameter("I60", I60));
                        command.Parameters.Add(new SqlParameter("I50", I50));
                        command.Parameters.Add(new SqlParameter("I30", I30));
                        command.Parameters.Add(new SqlParameter("I120", I120));
                        command.Parameters.Add(new SqlParameter("IsSubmit", IsSubmit));

                        command.Parameters.Add(new SqlParameter("OtherGender", OtherGender));
                        command.Parameters.Add(new SqlParameter("Nonbinary", Nonbinary));
                        command.Parameters.Add(new SqlParameter("VacanciesGender", VacanciesGender));
                        command.Parameters.Add(new SqlParameter("VacanciesRace", VacanciesRace));
                        command.Parameters.Add(new SqlParameter("Latinx", Latinx));
                        command.Parameters.Add(new SqlParameter("NonLatinx", NonLatinx));
                        command.Parameters.Add(new SqlParameter("VacanciesEthnicity", VacanciesEthnicity));

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

        public static void UpdateProjectPortfolio1(int ProjectPortfolioID, int PortfolioType, string Year, int TotalUnits,
            int MGender, int FGender, int UGender, int White, int Black, int Asian, int Indian, int Hawaiian, int MultiRacial, int UnknownRace, int Hispanic, int NonHisp, int UnknownEthnicity, int Homeless,
            int MarketRate, int I100, int I80, int I75, int I60, int I50, int I30, int I120, int ProjectID, int OtherGender, int Nonbinary, int VacanciesGender, int VacanciesRace, int Latinx, int NonLatinx, int VacanciesEthnicity)
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
                        command.CommandText = "UpdateProjectPortfolio1";

                        command.Parameters.Add(new SqlParameter("ProjectPortfolioID", ProjectPortfolioID));
                        command.Parameters.Add(new SqlParameter("PortfolioType", PortfolioType));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                        command.Parameters.Add(new SqlParameter("TotalUnits", TotalUnits));
                        command.Parameters.Add(new SqlParameter("MGender", MGender));
                        command.Parameters.Add(new SqlParameter("FGender", FGender));
                        command.Parameters.Add(new SqlParameter("UGender", UGender));
                        command.Parameters.Add(new SqlParameter("White", White));
                        command.Parameters.Add(new SqlParameter("Black", Black));
                        command.Parameters.Add(new SqlParameter("Asian", Asian));
                        command.Parameters.Add(new SqlParameter("Indian", Indian));
                        command.Parameters.Add(new SqlParameter("Hawaiian", Hawaiian));
                        command.Parameters.Add(new SqlParameter("MultiRacial", MultiRacial));
                        command.Parameters.Add(new SqlParameter("UnknownRace", UnknownRace));
                        command.Parameters.Add(new SqlParameter("Hispanic", Hispanic));
                        command.Parameters.Add(new SqlParameter("NonHisp", NonHisp));
                        command.Parameters.Add(new SqlParameter("UnknownEthnicity", UnknownEthnicity));
                        command.Parameters.Add(new SqlParameter("Homeless", Homeless));
                        command.Parameters.Add(new SqlParameter("MarketRate", MarketRate));
                        command.Parameters.Add(new SqlParameter("I100", I100));
                        command.Parameters.Add(new SqlParameter("I80", I80));
                        command.Parameters.Add(new SqlParameter("I75", I75));
                        command.Parameters.Add(new SqlParameter("I60", I60));
                        command.Parameters.Add(new SqlParameter("I50", I50));
                        command.Parameters.Add(new SqlParameter("I30", I30));
                        command.Parameters.Add(new SqlParameter("I120", I120));

                        command.Parameters.Add(new SqlParameter("OtherGender", OtherGender));
                        command.Parameters.Add(new SqlParameter("Nonbinary", Nonbinary));
                        command.Parameters.Add(new SqlParameter("VacanciesGender", VacanciesGender));
                        command.Parameters.Add(new SqlParameter("VacanciesRace", VacanciesRace));
                        command.Parameters.Add(new SqlParameter("Latinx", Latinx));
                        command.Parameters.Add(new SqlParameter("NonLatinx", NonLatinx));
                        command.Parameters.Add(new SqlParameter("VacanciesEthnicity", VacanciesEthnicity));

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


        public static void AddProjectPortfolio(int PortfolioType, string  Year, int  ProjectID,  int TotalUnits, 
            int MGender, int FGender, int UGender, int White, int Black, int Asian, int Indian, int Hawaiian, int MultiRacial, int UnknownRace, int Hispanic, int NonHisp, int UnknownEthnicity, int Homeless,
            int MarketRate, int I100, int I80, int I75, int I60, int I50, int I30, int I120, bool IsSubmit)
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
                        command.CommandText = "AddProjectPortfolio";

                        command.Parameters.Add(new SqlParameter("PortfolioType", PortfolioType));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                        command.Parameters.Add(new SqlParameter("TotalUnits", TotalUnits));
                        command.Parameters.Add(new SqlParameter("MGender", MGender));
                        command.Parameters.Add(new SqlParameter("FGender", FGender));
                        command.Parameters.Add(new SqlParameter("UGender", UGender));
                        command.Parameters.Add(new SqlParameter("White", White));
                        command.Parameters.Add(new SqlParameter("Black", Black));
                        command.Parameters.Add(new SqlParameter("Asian", Asian));
                        command.Parameters.Add(new SqlParameter("Indian", Indian));
                        command.Parameters.Add(new SqlParameter("Hawaiian", Hawaiian));
                        command.Parameters.Add(new SqlParameter("MultiRacial", MultiRacial));
                        command.Parameters.Add(new SqlParameter("UnknownRace", UnknownRace));
                        command.Parameters.Add(new SqlParameter("Hispanic", Hispanic));
                        command.Parameters.Add(new SqlParameter("NonHisp", NonHisp));
                        command.Parameters.Add(new SqlParameter("UnknownEthnicity", UnknownEthnicity));
                        command.Parameters.Add(new SqlParameter("Homeless", Homeless));
                        command.Parameters.Add(new SqlParameter("MarketRate", MarketRate));
                        command.Parameters.Add(new SqlParameter("I100", I100));
                        command.Parameters.Add(new SqlParameter("I80", I80));
                        command.Parameters.Add(new SqlParameter("I75", I75));
                        command.Parameters.Add(new SqlParameter("I60", I60));
                        command.Parameters.Add(new SqlParameter("I50", I50));
                        command.Parameters.Add(new SqlParameter("I30", I30));
                        command.Parameters.Add(new SqlParameter("I120", I120));
                        command.Parameters.Add(new SqlParameter("IsSubmit", IsSubmit));

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

        public static void UpdateProjectPortfolio(int ProjectPortfolioID, int PortfolioType, string Year, int TotalUnits,
            int MGender, int FGender, int UGender, int White, int Black, int Asian, int Indian, int Hawaiian, int MultiRacial, int UnknownRace, int Hispanic, int NonHisp, int UnknownEthnicity, int Homeless,
            int MarketRate, int I100, int I80, int I75, int I60, int I50, int I30, int I120, int ProjectID, int OtherGender, int Nonbinary, int VacanciesGender, int VacanciesRace, int Latinx, int NonLatinx, int VacanciesEthnicity)
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
                        command.CommandText = "UpdateProjectPortfolio";
                        
                        command.Parameters.Add(new SqlParameter("ProjectPortfolioID", ProjectPortfolioID));
                        command.Parameters.Add(new SqlParameter("PortfolioType", PortfolioType));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("ProjectID", ProjectID));
                        command.Parameters.Add(new SqlParameter("TotalUnits", TotalUnits));
                        command.Parameters.Add(new SqlParameter("MGender", MGender));
                        command.Parameters.Add(new SqlParameter("FGender", FGender));
                        command.Parameters.Add(new SqlParameter("UGender", UGender));
                        command.Parameters.Add(new SqlParameter("White", White));
                        command.Parameters.Add(new SqlParameter("Black", Black));
                        command.Parameters.Add(new SqlParameter("Asian", Asian));
                        command.Parameters.Add(new SqlParameter("Indian", Indian));
                        command.Parameters.Add(new SqlParameter("Hawaiian", Hawaiian));
                        command.Parameters.Add(new SqlParameter("MultiRacial", MultiRacial));
                        command.Parameters.Add(new SqlParameter("UnknownRace", UnknownRace));
                        command.Parameters.Add(new SqlParameter("Hispanic", Hispanic));
                        command.Parameters.Add(new SqlParameter("NonHisp", NonHisp));
                        command.Parameters.Add(new SqlParameter("UnknownEthnicity", UnknownEthnicity));
                        command.Parameters.Add(new SqlParameter("Homeless", Homeless));
                        command.Parameters.Add(new SqlParameter("MarketRate", MarketRate));
                        command.Parameters.Add(new SqlParameter("I100", I100));
                        command.Parameters.Add(new SqlParameter("I80", I80));
                        command.Parameters.Add(new SqlParameter("I75", I75));
                        command.Parameters.Add(new SqlParameter("I60", I60));
                        command.Parameters.Add(new SqlParameter("I50", I50));
                        command.Parameters.Add(new SqlParameter("I30", I30));
                        command.Parameters.Add(new SqlParameter("I120", I120));
                        command.Parameters.Add(new SqlParameter("OtherGender", OtherGender));
                        command.Parameters.Add(new SqlParameter("Nonbinary", Nonbinary));
                        command.Parameters.Add(new SqlParameter("VacanciesGender", VacanciesGender));
                        command.Parameters.Add(new SqlParameter("VacanciesRace", VacanciesRace));
                        command.Parameters.Add(new SqlParameter("Latinx", Latinx));
                        command.Parameters.Add(new SqlParameter("NonLatinx", NonLatinx));
                        command.Parameters.Add(new SqlParameter("VacanciesEthnicity", VacanciesEthnicity));

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
        public static DataTable GetPopulatePortfolioTypesByProj(string ProjectNumber, string Year)
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
                        command.CommandText = "GetPopulatePortfolioTypesByProj";
                        command.Parameters.Add(new SqlParameter("ProjectNumber", ProjectNumber));
                        command.Parameters.Add(new SqlParameter("Year", Year));

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
        public static DataTable GetPopulatePortfolioTypes(string LoginName, string ProjectNumber, string Year)
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
                        command.CommandText = "GetPopulatePortfolioTypes";
                        command.Parameters.Add(new SqlParameter("LoginName", LoginName));
                        command.Parameters.Add(new SqlParameter("ProjectNumber", ProjectNumber));
                        command.Parameters.Add(new SqlParameter("Year", Year));

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

        public static DataTable GetPortfolioYearsbyProj(string ProjectNumber)
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
                        command.CommandText = "GetPortfolioYearsbyProj";
                        command.Parameters.Add(new SqlParameter("ProjectNumber", ProjectNumber));

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

        public static DataTable GetPortfolioYearsbyLoginProj(string LoginName, string ProjectNumber)
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
                        command.CommandText = "GetPortfolioYearsbyLoginProj";
                        command.Parameters.Add(new SqlParameter("LoginName", LoginName));
                        command.Parameters.Add(new SqlParameter("ProjectNumber", ProjectNumber));

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

        public static DataRow GetPortfolioDataForOnLineApp(string LoginName, string ProjectNumber, string Year, string PortfolioTypeID)
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
                        command.CommandText = "GetPortfolioDataForOnLineApp";
                        command.Parameters.Add(new SqlParameter("LoginName", LoginName));
                        command.Parameters.Add(new SqlParameter("ProjectNumber", ProjectNumber));
                        command.Parameters.Add(new SqlParameter("Year", Year));
                        command.Parameters.Add(new SqlParameter("PortfolioTypeID", PortfolioTypeID));

                        DataSet ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
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
        public static void SubmitPortfolioData(string LoginName, int Year)
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
                        command.CommandText = "SubmitPortfolioData";
                        command.Parameters.Add(new SqlParameter("LoginName", LoginName));
                        command.Parameters.Add(new SqlParameter("Year", Year));

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
        
    }
}
