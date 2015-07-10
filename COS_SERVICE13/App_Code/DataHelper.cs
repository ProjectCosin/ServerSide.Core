﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
///DataHelper 的摘要说明
/// </summary>
public class DataHelper
{
	public DataHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public enum DataBaseFact
    {
        CENTRAL,
        PROPERTY
    };

    public static string OUTPUT_PARA_INT16 = "0";
    public static string OUTPUT_PARA_INT32 = "1";
    public static string OUTPUT_PRAR_STRING = "2";

    /// <summary>
    /// Query(SqlDataAdapter)
    /// </summary>
    /// <param name="connectionstring">数据库字符串</param>
    /// <param name="query">查询主体</param>
    /// <param name="args">查询条件值数组</param>
    /// <returns>DataTable类型返回</returns>
    public static DataTable Query_SqlDataAdapter(DataBaseFact connectionstring, string query, string[] args)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring.ToString()].ConnectionString))
        {

            if (args != null)
            {
                for (int s = 0; s < args.Length; s++)
                {
                    int m = query.IndexOf('?');

                    if (m + 1 == query.Length)
                    {

                        query = query.Substring(0, m) + args[0];
                    }
                    else
                    {

                        query = query.Substring(0, m) + args[0] + query.Substring(m + 1, query.Length - m - 1);
                    }


                }
            }

            SqlCommand mysc = new SqlCommand();
            mysc.Connection = myconn;
            mysc.CommandText = query;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = mysc;

            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;

        }
    
    
    
    }

    /// <summary>
    /// Query(ExecuteNonQuery)
    /// </summary>
    /// <param name="connectionstring">数据库字符串</param>
    /// <param name="query">查询主体</param>
    /// <param name="args">查询条件值数组</param>
    /// <returns>执行结果</returns>
    public static int Query_ExecuteNonQuery(DataBaseFact connectionstring, string query, string[] args)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring.ToString()].ConnectionString))
        {

            if (args != null) {

                for (int s = 0; s < args.Length; s++)
                {
                    int m = query.IndexOf('?');

                    if (m + 1 == query.Length)
                    {

                        query = query.Substring(0, m) + args[0];
                    }
                    else
                    {

                        query = query.Substring(0, m) + args[0] + query.Substring(m + 1, query.Length - m - 1);
                    }


                }
            }

           

            SqlCommand mysc = new SqlCommand();
            mysc.Connection = myconn;
            mysc.CommandText = query;
            myconn.Open();
            try
            {
                mysc.ExecuteNonQuery();
                return 1;
            }
            catch {

                return 0;
            }

        }
    
    
    
    }

    /// <summary>
    /// Query(ExecuteScalar)
    /// </summary>
    /// <param name="connectionstring">数据库字符串</param>
    /// <param name="query">查询主体</param>
    /// <param name="args">查询条件值数组</param>
    /// <returns>执行返回值</returns>
    public static string Query_ExecuteScalar(DataBaseFact connectionstring, string query, string[] args)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring.ToString()].ConnectionString))
        {

            if (args != null)
            {

                for (int s = 0; s < args.Length; s++)
                {
                    int m = query.IndexOf('?');

                    if (m + 1 == query.Length)
                    {

                        query = query.Substring(0, m) + args[0];
                    }
                    else
                    {

                        query = query.Substring(0, m) + args[0] + query.Substring(m + 1, query.Length - m - 1);
                    }


                }
            }

            SqlCommand mysc = new SqlCommand();
            mysc.Connection = myconn;
            mysc.CommandText = query;
            myconn.Open();
            try
            {
                return mysc.ExecuteScalar().ToString();
            }
            catch
            {

                return "";
            }

        }



    }


    /// <summary>
    /// QueryWithSP_OUTPUT
    /// </summary>
    /// <param name="connectionstring">数据库字符串</param>
    /// <param name="SPName">存储过程名称</param>
    /// <param name="ParaCollection">输入参数集合</param>
    /// <param name="OutPutCollection">输出参数集合</param>
    /// <returns>输出参数集合</returns>
    public static List<SqlParameter> QueryWithSP_OUTPUT(DataBaseFact connectionstring, string SPName, string[,] ParaCollection, string[,] OutPutCollection)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring.ToString()].ConnectionString))
        {
          
            SqlCommand mysc = new SqlCommand();
            mysc.Connection = myconn;
            mysc.CommandType = CommandType.StoredProcedure;
            mysc.CommandText = SPName;

            for (int i = 0; i < ParaCollection.Length/2; i++) {

                SqlParameter sp = new SqlParameter();
                sp.ParameterName = ParaCollection[0, i];
                sp.Value = ParaCollection[1, i];
                mysc.Parameters.Add(sp);
            }

                List<SqlParameter> OutPutList = new List<SqlParameter>();

                if (OutPutCollection != null)
                {

                    for (int j = 0; j < OutPutCollection.Length / 2; j++)
                    {

                        SqlParameter sp = new SqlParameter();
                        sp.ParameterName = OutPutCollection[0, j];
                        sp.Direction = ParameterDirection.Output;

                        if (OutPutCollection[1, j] == OUTPUT_PARA_INT16)
                        {
                            sp.DbType = DbType.Int16;
                            sp.Size = 16;
                        }
                        else if (OutPutCollection[1, j] == OUTPUT_PARA_INT32)
                        {
                            sp.DbType = DbType.Int32;
                            sp.Size = 32;
                        }
                        else if (OutPutCollection[1, j] == OUTPUT_PRAR_STRING)
                        {
                            sp.DbType = DbType.String;
                            sp.Size = 50;
                        }
                        mysc.Parameters.Add(sp);
                        OutPutList.Add(sp);

                    }
                }

          

            myconn.Open();

            try
            {
                mysc.ExecuteNonQuery();

                return OutPutList;

            }
            catch {

                return null;
            }
        }
    
    
    }


    /// <summary>
    /// QueryWithSP_SqlDataAdapter
    /// </summary>
    /// <param name="connectionstring">数据库字符串</param>
    /// <param name="SPName">存储过程名称</param>
    /// <param name="ParaCollection">输入参数集合</param>
    /// <param name="OutPutCollection">输入参数集合</param>
    /// <returns>DataTable类型返回</returns>
    public static DataTable QueryWithSP_SqlDataAdapter(DataBaseFact connectionstring, string SPName, string[,] ParaCollection, string[,] OutPutCollection)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionstring.ToString()].ConnectionString))
        {

            SqlCommand mysc = new SqlCommand();
            mysc.Connection = myconn;
            mysc.CommandType = CommandType.StoredProcedure;
            mysc.CommandText = SPName;

            for (int i = 0; i < ParaCollection.Length / 2; i++)
            {

                SqlParameter sp = new SqlParameter();
                sp.ParameterName = ParaCollection[0, i];
                sp.Value = ParaCollection[1, i];
                mysc.Parameters.Add(sp);
            }

            List<SqlParameter> OutPutList = new List<SqlParameter>();

            if (OutPutCollection != null)
            {
                for (int j = 0; j < OutPutCollection.Length / 2; j++)
                {

                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = OutPutCollection[0, j];
                    sp.Direction = ParameterDirection.Output;

                    if (OutPutCollection[1, j] == OUTPUT_PARA_INT16)
                    {
                        sp.DbType = DbType.Int16;
                        sp.Size = 16;
                    }
                    else if (OutPutCollection[1, j] == OUTPUT_PARA_INT32)
                    {
                        sp.DbType = DbType.Int32;
                        sp.Size = 32;
                    }
                    else if (OutPutCollection[1, j] == OUTPUT_PRAR_STRING)
                    {
                        sp.DbType = DbType.String;
                        sp.Size = 50;
                    }
                    mysc.Parameters.Add(sp);
                    OutPutList.Add(sp);

                }
            }

            myconn.Open();

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = mysc;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            return dt;
           
        }


    }

}