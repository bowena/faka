namespace HZ.MVC.FaKa.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;

    public delegate void IDbDataReaderCallBack(DbDataReader read);

    public class ManagerSqlite
    {
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="command">
        /// 数据操作设置
        /// </param>
        /// <returns>
        /// </returns>
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] sqlParam)
        {
            try
            {
                int resu = 0;
                using (SQLiteConnection con = MyConnection.GetSQLiteConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Connection = con;
                        if (null != sqlParam)
                            cmd.Parameters.AddRange(sqlParam);
                        if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                        {
                            resu = cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            resu = 0;
                        }
                    }
                }
                return resu;
            }
            catch (Exception e)
            {
                //MyLog.DalSqlite.Print.Error("Exception ExecuteNonQuery" + command.SqlText, e);
                return 0;
            }
            finally
            {
                //command.Dispose();
            }
        }

        /// <summary>
        /// 获取首行首列值
        /// </summary>
        /// <param name="command">
        /// 数据操作设置
        /// </param>
        /// <returns>
        /// </returns>
        public static object ExecuteScalar(string sql, SQLiteParameter[] sqlParam)
        {
            try
            {
                using (SQLiteConnection con = MyConnection.GetSQLiteConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Connection = con;
                        if (null != sqlParam && sqlParam.Length > 0)
                            cmd.Parameters.AddRange(sqlParam);
                        if (con != null && con.State == ConnectionState.Open)
                        {
                            return cmd.ExecuteScalar();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MyLog.DalSqlite.Print.Error("Exception ExecuteScalar" + command.SqlText, e);
                return null;
            }
            finally
            {

                //command.Dispose();
            }

        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="command">
        /// 数据操作设置
        /// </param>
        /// <returns>
        /// </returns>
        public static DataSet GetDataSet(string sqlStr, SQLiteParameter[] sqlParam)
        {
            try
            {
                using (SQLiteConnection conn = MyConnection.GetSQLiteConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = sqlStr;
                        cmd.Connection = (SQLiteConnection)conn;
                        if (null != sqlParam)
                            cmd.Parameters.AddRange(sqlParam);
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            using (DataSet ds = new DataSet())
                            {
                                if (conn != null && conn.State == ConnectionState.Open)
                                {
                                    da.Fill(ds);
                                }

                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MyLog.DalSqlite.Print.Error("Exception GetDataSet" + command.SqlText, e);
                return null;
            }
            finally
            {

            }
        }

        public static void GetSQLiteDataReader(string sql, SQLiteParameter[] sqlParam, IDbDataReaderCallBack callback)
        {
            try
            {
                SQLiteConnection con = MyConnection.GetSQLiteConnection();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;

                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        if (null != read)
                            callback(read);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //MyLog.DalSqlite.Print.Error("Exception GetSQLiteDataReader" + command.SqlText, ex);
            }
            finally
            {

            }

        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="key">
        /// 数据库连接标示
        /// </param>
        /// <param name="sql">
        /// sql语句
        /// </param>
        /// <param name="paras">
        /// 参数
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable GetTable(string sql, SQLiteParameter[] sqlParam)
        {

            try
            {
                using (SQLiteConnection conn = MyConnection.GetSQLiteConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Connection = conn;
                        if (null != sqlParam)
                            cmd.Parameters.AddRange(sqlParam);
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            using (DataTable ds = new DataTable())
                            {
                                if (conn != null && conn.State == ConnectionState.Open)
                                {
                                    da.Fill(ds);
                                    return ds;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MyLog.DalSqlite.Print.Error("Exception GetSQLiteDataReader" + command.SqlText, e);
                return null;
            }
            finally
            {

            }

        }
    }
}