using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.Areas.Admin.Models.Enum;
using HZ.MVC.FaKa.Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.BLL
{
    public class BProductType
    {
        public static bool Insert(ProductTypeViewModel model)
        {
            string sql = string.Empty; ;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO ProductTypes ("
             + EProductTypes.ProductName.ToString() + ","
             + EProductTypes.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EProductTypes.ProductName.ToString() + ","
             + "@" + EProductTypes.UpdateTime.ToString()
             + ");";
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.Id.ToString(), Value = model.Id });
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.ProductName.ToString(), Value = model.ProductName });
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.UpdateTime.ToString(), Value = model.UpdateTime });
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
            string sql = "delete from ProductTypes where Id = " + id;

            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }

        public static List<ProductTypeViewModel> SearchAll()
        {
            string sql = "select * from ProductTypes ";
            List<ProductTypeViewModel> modelArr = new List<ProductTypeViewModel>();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        ProductTypeViewModel model = new ProductTypeViewModel();
                        model.Id = Convert.ToInt32(reader[EProductTypes.Id.ToString()]);
                        model.ProductName = reader[EProductTypes.ProductName.ToString()].ToString();
                        model.UpdateTime = Convert.ToDateTime(reader[EProductTypes.UpdateTime.ToString()]);
                        modelArr.Add(model);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static ProductTypeViewModel SearchBysql(string sql)
        {
            ProductTypeViewModel model = new ProductTypeViewModel();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader[EProductTypes.Id.ToString()]);
                        model.ProductName = reader[EProductTypes.ProductName.ToString()].ToString();
                        model.UpdateTime = Convert.ToDateTime(reader[EProductTypes.UpdateTime.ToString()]);
                    }
                }
            }
                ));
            return model;
        }

        public static object ExecuteSql(string sql)
        {
            return null;
        }
    }
}