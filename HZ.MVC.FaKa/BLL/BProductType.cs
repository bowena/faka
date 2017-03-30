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

        public static bool Update(ProductTypeViewModel model)
        {
            string sql = "update ProductTypes set ProductName=@ProductName,UpdateTime=@UpdateTime where Id=@Id ";
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.Id.ToString(), Value = model.Id });
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.ProductName.ToString(), Value = model.ProductName });
            ps.Add(new SQLiteParameter() { ParameterName = EProductTypes.UpdateTime.ToString(), Value = model.UpdateTime });
            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
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

        public static bool Delete(List<int> ids)
        {

            List<string> sqls = new List<string>();
            foreach (var item in ids)
            {
                sqls.Add("delete from ProductTypes where Id = " + item);
            }

            return ManagerSqlite.ExecuteNonQuery(sqls, null);
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

        public static List<ProductTypeViewModel> SearchBysql(string name)
        {
            string sql = "select * from ProductTypes where ProductName like '%" + name + "%'"; 
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

        public static object ExecuteSql(string sql)
        {
            return null;
        }
    }
}