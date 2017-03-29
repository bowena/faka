using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.Areas.Admin.Models.Enum;
using HZ.MVC.FaKa.Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace HZ.MVC.FaKa.BLL
{
    public class BUsers
    {
        public static bool Insert(UsersViewModel model)
        {
            string sql = string.Empty; ;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO Users ("
             + EUsers.userName.ToString() + ","
             + EUsers.password.ToString() + ","
             + EUsers.isRemmber.ToString() + ","
             + EUsers.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EUsers.userName.ToString() + ","
             + "@" + EUsers.password.ToString() + ","
             + "@" + EUsers.isRemmber.ToString() + ","
             + "@" + EUsers.UpdateTime.ToString()
             + ");";

            ps.Add(new SQLiteParameter() { ParameterName = EUsers.userName.ToString(), Value = model.userName });
            ps.Add(new SQLiteParameter() { ParameterName = EUsers.password.ToString(), Value = model.password });
            ps.Add(new SQLiteParameter() { ParameterName = EUsers.isRemmber.ToString(), Value = model.isRemmber });
            ps.Add(new SQLiteParameter() { ParameterName = EUsers.UpdateTime.ToString(), Value = model.UpdateTime });

            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool Update(string sql)
        {
            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool Delete(int id)
        {
            string sql = "delete from Users where Id = " + id;

            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }


        public static UsersViewModel SearchById(int id)
        {
            string sql = "select * from Users where id=" + id;

            UsersViewModel model = new UsersViewModel();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader[EUsers.Id.ToString()]);
                        model.userName = reader[EUsers.userName.ToString()].ToString();
                        model.isRemmber = Convert.ToInt32(reader[EUsers.isRemmber.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EUsers.UpdateTime.ToString()]);
                        model.password = reader[EUsers.password.ToString()].ToString();
                    }
                }
            }
                ));


            return model;
        }

        public static object ExecuteSql(string sql)
        {
            return ManagerSqlite.ExecuteScalar(sql, null);
        }
    }
}