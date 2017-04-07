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
    public class BOrder
    {
        public static bool Insert(OrderViewModel model)
        {
            string sql = string.Empty; ;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO Orders ("
             + EOrders.NO.ToString() + ","
             + EOrders.Product_Id.ToString() + ","
             + EOrders.Type.ToString() + ","
             + EOrders.Status.ToString() + ","
             + EOrders.Price.ToString() + ","
             + EOrders.Count.ToString() + ","
             + EOrders.LocalStatus.ToString() + ","
             + EOrders.Remark.ToString() + ","
             + EOrders.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EOrders.NO.ToString() + ","
             + "@" + EOrders.Product_Id.ToString() + ","
             + "@" + EOrders.Type.ToString() + ","
             + "@" + EOrders.Status.ToString() + ","
             + "@" + EOrders.Price.ToString() + ","
             + "@" + EOrders.Count.ToString() + ","
             + "@" + EOrders.LocalStatus.ToString() + ","
             + "@" + EOrders.Remark.ToString() + ","
             + "@" + EOrders.UpdateTime.ToString()
             + ");";
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Id.ToString(), Value = model.Id });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.NO.ToString(), Value = model.NO });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Product_Id.ToString(), Value = model.Product_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Type.ToString(), Value = model.Type });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Status.ToString(), Value = model.Status });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Price.ToString(), Value = model.Price });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Count.ToString(), Value = model.Count });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.LocalStatus.ToString(), Value = model.LocalStatus });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.Remark.ToString(), Value = model.Remark });
            ps.Add(new SQLiteParameter() { ParameterName = EOrders.UpdateTime.ToString(), Value = model.UpdateTime });
            
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
            string sql = "delete from Orders where Id = " + id;

            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }

        public static OrderViewModel SearchByTradeNo(string tradeNo)
        {
            string sql = "select * from Orders where NO ='" + tradeNo.Trim() + "' ";
            OrderViewModel model = null;

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new OrderViewModel();
                        model.Id = Convert.ToInt32(reader[EOrders.Id.ToString()]);
                        model.NO = reader[EOrders.NO.ToString()].ToString();
                        model.Type = reader[EOrders.Type.ToString()].ToString();
                        model.Product_Id = Convert.ToInt32(reader[EOrders.Product_Id.ToString()]);
                        model.Price = Convert.ToDouble(reader[EOrders.Price.ToString()]);
                        model.Count = Convert.ToInt32(reader[EOrders.Count.ToString()]);
                        model.Status = reader[EOrders.Status.ToString()].ToString();
                        model.LocalStatus = reader[EOrders.LocalStatus.ToString()].ToString();
                        model.UpdateTime = Convert.ToDateTime(reader[EOrders.UpdateTime.ToString()]);
                        model.Remark = reader[EOrders.Remark.ToString()].ToString();
                    }
                }
            }
                ));
            return model;
        }

        public static OrderViewModel SearchByContact(string contacts)
        {
            string sql = "select * from Orders where Remark ='" + contacts.Trim() + "' ";
            OrderViewModel model = null;

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new OrderViewModel();
                        model.Id = Convert.ToInt32(reader[EOrders.Id.ToString()]);
                        model.NO = reader[EOrders.NO.ToString()].ToString();
                        model.Type = reader[EOrders.Type.ToString()].ToString();
                        model.Product_Id = Convert.ToInt32(reader[EOrders.Product_Id.ToString()]);
                        model.Price = Convert.ToDouble(reader[EOrders.Price.ToString()]);
                        model.Count = Convert.ToInt32(reader[EOrders.Count.ToString()]);
                        model.Status = reader[EOrders.Status.ToString()].ToString();
                        model.LocalStatus = reader[EOrders.LocalStatus.ToString()].ToString();
                        model.UpdateTime = Convert.ToDateTime(reader[EOrders.UpdateTime.ToString()]);
                        model.Remark = reader[EOrders.Remark.ToString()].ToString();
                    }
                }
            }
                ));
            return model;
        }

        public static OrderViewModel SearchBySql(string sql)
        {

            OrderViewModel model = new OrderViewModel();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader[EOrders.Id.ToString()]);
                        model.NO = reader[EOrders.NO.ToString()].ToString();
                        model.Type = reader[EOrders.Type.ToString()].ToString();
                        model.Product_Id = Convert.ToInt32(reader[EOrders.Product_Id.ToString()]);
                        model.Price = Convert.ToDouble(reader[EOrders.Price.ToString()]);
                        model.Count = Convert.ToInt32(reader[EOrders.Count.ToString()]);
                        model.Status = reader[EOrders.Status.ToString()].ToString();
                        model.LocalStatus = reader[EOrders.LocalStatus.ToString()].ToString();
                        model.UpdateTime = Convert.ToDateTime(reader[EOrders.UpdateTime.ToString()]);
                        model.Remark = reader[EOrders.Remark.ToString()].ToString();
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