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
    public class BProduct
    {
        public static bool Insert(ProductViewModel model)
        {
            string sql = string.Empty; ;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO Products ("
             + EProducts.Name.ToString() + ","
             + EProducts.Price.ToString() + ","
             + EProducts.ProductType_Id.ToString() + ","
             + EProducts.AddedTime.ToString() + ","
             + EProducts.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EProducts.Name.ToString() + ","
             + "@" + EProducts.Price.ToString() + ","
             + "@" + EProducts.ProductType_Id.ToString() + ","
             + "@" + EProducts.AddedTime.ToString() + ","
             + "@" + EProducts.UpdateTime.ToString()
             + ");";
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.Name.ToString(), Value = model.Name });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.Price.ToString(), Value = model.Price });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.ProductType_Id.ToString(), Value = model.ProductType_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.AddedTime.ToString(), Value = model.AddedTime });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.UpdateTime.ToString(), Value = model.UpdateTime });

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
            string sql = "delete from Products where Id = " + id;

            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }

        public static List<ProductViewModel> SearchByTypeId(string id)
        {
            string sql = "select * from Products where ProductType_Id=" + id;
            List<ProductViewModel> modelArr = new List<ProductViewModel>();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        ProductViewModel model = new ProductViewModel();
                        model.Id = Convert.ToInt32(reader[EProducts.Id.ToString()]);
                        model.Name = reader[EProducts.Name.ToString()].ToString();
                        model.ProductType_Id = Convert.ToInt32(reader[EProducts.ProductType_Id.ToString()]);
                        model.Price = Convert.ToDouble(reader[EProducts.Price.ToString()]);
                        model.AddedTime = Convert.ToDateTime(reader[EProducts.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EProducts.UpdateTime.ToString()]);
                        modelArr.Add(model);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static ProductViewModel SearchBysql(string sql)
        {
            ProductViewModel model = new ProductViewModel();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader[EProducts.Id.ToString()]);
                        model.Name = reader[EProducts.Name.ToString()].ToString();
                        model.ProductType_Id = Convert.ToInt32(reader[EProducts.ProductType_Id.ToString()]);
                        model.Price = Convert.ToDouble(reader[EProducts.Price.ToString()]);
                        model.AddedTime = Convert.ToDateTime(reader[EProducts.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EProducts.UpdateTime.ToString()]);
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