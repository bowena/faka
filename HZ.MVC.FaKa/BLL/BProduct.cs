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

        public static bool Update(ProductViewModel model)
        {
            string sql = "update Products set Name=@Name,Price=@Price,ProductType_Id=@ProductType_Id,UpdateTime=@UpdateTime where Id=@Id ";
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.Name.ToString(), Value = model.Name });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.Price.ToString(), Value = model.Price });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.ProductType_Id.ToString(), Value = model.ProductType_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.UpdateTime.ToString(), Value = model.UpdateTime });
            ps.Add(new SQLiteParameter() { ParameterName = EProducts.Id.ToString(), Value = model.Id });
            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
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

        public static bool Delete(List<int> ids)
        {

            List<string> sqls = new List<string>();
            foreach (var item in ids)
            {
                sqls.Add("delete from Products where Id = " + item);
            }

            return ManagerSqlite.ExecuteNonQuery(sqls, null);
        }

        public static List<ProductViewModel> SearchAll()
        {
            string sql = "select a.*,b.[ProductName] from Products a join ProductTypes b on a.[ProductType_Id] = b.[Id] ";
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
                        model.Price = Convert.ToDouble(reader[EProducts.Price.ToString()]);
                        model.ProductType_Id = Convert.ToInt32(reader[EProducts.ProductType_Id.ToString()]);
                        model.ProductTypeName = reader["ProductName"].ToString();
                        model.AddedTime = Convert.ToDateTime(reader[EProducts.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EProducts.UpdateTime.ToString()]);
                        modelArr.Add(model);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static List<ProductViewModel> SearchByTypeId(string id)
        {
            string sql = "select a.*,b.[ProductName] from Products a join ProductTypes b on a.[ProductType_Id] = b.[Id] where a.[ProductType_Id]=" + id;
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
                        model.ProductTypeName = reader["ProductName"].ToString();
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

        public static List<ProductViewModel> SearchBysql(string name)
        {
            string sql = "select a.*,b.[ProductName] from Products a join ProductTypes b on a.[ProductType_Id] = b.[Id] where a.[Name] like '%" + name + "%'";
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
                        model.ProductTypeName = reader["ProductName"].ToString();
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

        public static object ExecuteSql(string sql)
        {
            return ManagerSqlite.ExecuteScalar(sql, null);
        }
    }
}