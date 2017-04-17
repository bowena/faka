using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.Areas.Admin.Models.Enum;
using HZ.MVC.FaKa.Dal;
using HZ.MVC.FaKa.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace HZ.MVC.FaKa.BLL
{
    public class BKaMi
    {
        public static bool Insert(KaMiViewModel model)
        {
            if (model == null)
                return false;
            string sql = string.Empty;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO Kamis ("
             + EKamis.Content.ToString() + ","
             + EKamis.State.ToString() + ","
             + EKamis.Product_Id.ToString() + ","
             + EKamis.ProductType_Id.ToString() + ","
             + EKamis.Trade_No.ToString() + ","
             + EKamis.Remark.ToString() + ","
             + EKamis.AddedTime.ToString() + ","
             + EKamis.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EKamis.Content.ToString() + ","
             + "@" + EKamis.State.ToString() + ","
             + "@" + EKamis.Product_Id.ToString() + ","
             + "@" + EKamis.ProductType_Id.ToString() + ","
             + "@" + EKamis.Trade_No.ToString() + ","
             + "@" + EKamis.Remark.ToString() + ","
             + "@" + EKamis.AddedTime.ToString() + ","
             + "@" + EKamis.UpdateTime.ToString()
             + ");";
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Id.ToString(), Value = model.Id });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Content.ToString(), Value = model.Content });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.State.ToString(), Value = model.State });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Product_Id.ToString(), Value = model.Product_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.ProductType_Id.ToString(), Value = model.ProductType_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Trade_No.ToString(), Value = model.Trade_No });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Remark.ToString(), Value = model.Remark });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.AddedTime.ToString(), Value = model.AddedTime });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.UpdateTime.ToString(), Value = model.UpdateTime });

            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool Insert(List<KaMiViewModel> model)
        {
            if (model == null || model.Count == 0)
                return false;
            List<string> sqls = new List<string>();
            List<SQLiteParameter[]> paramArr = new List<SQLiteParameter[]>();
            foreach (var item in model)
            {
                string sql = string.Empty;
                List<SQLiteParameter> ps = new List<SQLiteParameter>();
                sql = "INSERT INTO Kamis ("
                 + EKamis.Content.ToString() + ","
                 + EKamis.State.ToString() + ","
                 + EKamis.Product_Id.ToString() + ","
                 + EKamis.ProductType_Id.ToString() + ","
                 + EKamis.Trade_No.ToString() + ","
                 + EKamis.Remark.ToString() + ","
                 + EKamis.AddedTime.ToString() + ","
                 + EKamis.UpdateTime.ToString()
                 + ") VALUES ("
                 + "@" + EKamis.Content.ToString() + ","
                 + "@" + EKamis.State.ToString() + ","
                 + "@" + EKamis.Product_Id.ToString() + ","
                 + "@" + EKamis.ProductType_Id.ToString() + ","
                 + "@" + EKamis.Trade_No.ToString() + ","
                 + "@" + EKamis.Remark.ToString() + ","
                 + "@" + EKamis.AddedTime.ToString() + ","
                 + "@" + EKamis.UpdateTime.ToString()
                 + ");";
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.Id.ToString(), Value = item.Id });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.Content.ToString(), Value = item.Content });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.State.ToString(), Value = item.State });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.Product_Id.ToString(), Value = item.Product_Id });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.ProductType_Id.ToString(), Value = item.ProductType_Id });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.Trade_No.ToString(), Value = item.Trade_No });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.Remark.ToString(), Value = item.Remark });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.AddedTime.ToString(), Value = item.AddedTime });
                ps.Add(new SQLiteParameter() { ParameterName = EKamis.UpdateTime.ToString(), Value = item.UpdateTime });
                sqls.Add(sql);
                paramArr.Add(ps.ToArray());

            }
            return ManagerSqlite.ExecuteBatchNonQuery(sqls, paramArr);
        }

        public static bool UpdateBySql(List<KaMiViewModel> models)
        {
            if (models == null || models.Count == 0)
            {
                return false;
            }
            List<string> sqls = new List<string>();
            foreach (var item in models)
            {
                sqls.Add("update Kamis set State=" + (int)KamiState.Used + ",Remark='" + item.Remark + "',Trade_No='" + item.Trade_No + "' where Id = " + item.Id);
            }

            return ManagerSqlite.ExecuteNonQuery(sqls, null);
        }

        public static bool Update(KaMiViewModel model)
        {
            if (model == null)
                return false;
            StringBuilder sb = new StringBuilder(2048);
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sb.Append("UPDATE Kamis SET ");

            sb.Append(EKamis.Content.ToString() + "=@" + EKamis.Content.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Content.ToString(), Value = model.Content });

            sb.Append(EKamis.State.ToString() + "=@" + EKamis.State.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.State.ToString(), Value = model.State });


            sb.Append(EKamis.Product_Id.ToString() + "=@" + EKamis.Product_Id.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Product_Id.ToString(), Value = model.Product_Id });

            sb.Append(EKamis.ProductType_Id.ToString() + "=@" + EKamis.ProductType_Id.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.ProductType_Id.ToString(), Value = model.ProductType_Id });

            sb.Append(EKamis.Trade_No.ToString() + "=@" + EKamis.Trade_No.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Trade_No.ToString(), Value = model.Trade_No });

            sb.Append(EKamis.Remark.ToString() + "=@" + EKamis.Remark.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Remark.ToString(), Value = model.Remark });

            sb.Append(EKamis.UpdateTime.ToString() + "=@" + EKamis.UpdateTime.ToString());
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.UpdateTime.ToString(), Value = model.UpdateTime });

            sb.Append(" where ");
            sb.Append(EKamis.Id.ToString() + "=@" + EKamis.Id.ToString());
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Id.ToString(), Value = model.Id });


            string sql = sb.ToString();
            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool Delete(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return false;
            List<string> sqls = new List<string>();
            foreach (var item in ids)
            {
                sqls.Add("delete from Kamis where Id = " + item);
            }

            return ManagerSqlite.ExecuteNonQuery(sqls, null);
        }

        public static List<KaMiViewModel> SearchAll()
        {
            string sql = "select a.*,b.Name,c.ProductName FROM Kamis a JOIN Products b on a.Product_Id = b.Id JOIN ProductTypes c on a.ProductType_Id = c.Id ";

            List<KaMiViewModel> modelArr = new List<KaMiViewModel>();
            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        KaMiViewModel model = new KaMiViewModel();
                        model.Id = Convert.ToInt32(reader[EKamis.Id.ToString()]);
                        model.Content = reader[EKamis.Content.ToString()].ToString();
                        model.State = Convert.ToInt32(reader[EKamis.State.ToString()]);
                        model.Product_Id = Convert.ToInt32(reader[EKamis.Product_Id.ToString()]);
                        model.ProductName = reader["Name"].ToString();
                        model.ProductType_Id = Convert.ToInt32(reader[EKamis.ProductType_Id.ToString()]);
                        model.ProductTypeName = reader["ProductName"].ToString();
                        model.AddedTime = Convert.ToDateTime(reader[EKamis.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EKamis.UpdateTime.ToString()]);
                        model.Trade_No = reader[EKamis.Trade_No.ToString()].ToString();
                        model.Remark = reader[EKamis.Remark.ToString()].ToString();
                        modelArr.Add(model);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static List<KaMiViewModel> SearchBysql(string name)
        {
            string sql = "select a.*,b.Name,c.ProductName FROM Kamis a JOIN Products b on a.Product_Id = b.Id JOIN ProductTypes c on a.ProductType_Id = c.Id where a.[Content] like '%" + name + "%'";
            List<KaMiViewModel> modelArr = new List<KaMiViewModel>();
            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        KaMiViewModel model = new KaMiViewModel();
                        model.Id = Convert.ToInt32(reader[EKamis.Id.ToString()]);
                        model.Content = reader[EKamis.Content.ToString()].ToString();
                        model.State = Convert.ToInt32(reader[EKamis.State.ToString()]);
                        model.Product_Id = Convert.ToInt32(reader[EKamis.Product_Id.ToString()]);
                        model.ProductName = reader["Name"].ToString();
                        model.ProductType_Id = Convert.ToInt32(reader[EKamis.ProductType_Id.ToString()]);
                        model.ProductTypeName = reader["ProductName"].ToString();
                        model.AddedTime = Convert.ToDateTime(reader[EKamis.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EKamis.UpdateTime.ToString()]);
                        model.Trade_No = reader[EKamis.Trade_No.ToString()].ToString();
                        model.Remark = reader[EKamis.Remark.ToString()].ToString();
                        modelArr.Add(model);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static Dictionary<int, string> SearchKamiByTrade(OrderViewModel order)
        {
            string sql = "select Id,Content from Kamis where Product_Id=" + order.Product_Id + " and State=" + (int)KamiState.NotUse + " order by AddedTime asc limit " + order.Count;
            Dictionary<int, string> modelArr = new Dictionary<int, string>();
            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[EKamis.Id.ToString()]);
                        string content = reader[EKamis.Content.ToString()].ToString();
                        modelArr.Add(id, content);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static Dictionary<int, string> SearchKamiByContact(string email, string tradeNo)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(email))
            {
                sql = "select a.Id,a.Content from Kamis a join Orders b on a.Product_Id=b.Product_Id where a.Remark='" + email + "' and b.Remark='" + email + "' ";
            }
            else
            {
                sql = "select a.Id,a.Content from Kamis a join Orders b on a.Product_Id=b.Product_Id where a.Trade_No='" + tradeNo + "' and b.NO='" + tradeNo + "' ";
            }

            Dictionary<int, string> modelArr = new Dictionary<int, string>();
            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[EKamis.Id.ToString()]);
                        string content = reader[EKamis.Content.ToString()].ToString();
                        modelArr.Add(id, content);
                    }
                }
            }
                ));
            return modelArr;
        }

        public static KaMiViewModel SearchById(int id)
        {
            string sql = "select * from Kamis where id=" + id;

            KaMiViewModel model = new KaMiViewModel();

            ManagerSqlite.GetSQLiteDataReader(sql, null, new IDbDataReaderCallBack(delegate(DbDataReader reader)
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            model.Id = Convert.ToInt32(reader[EKamis.Id.ToString()]);
                            model.Content = reader[EKamis.Content.ToString()].ToString();
                            model.State = Convert.ToInt32(reader[EKamis.State.ToString()]);
                            model.Product_Id = Convert.ToInt32(reader[EKamis.Product_Id.ToString()]);
                            model.ProductType_Id = Convert.ToInt32(reader[EKamis.ProductType_Id.ToString()]);
                            model.AddedTime = Convert.ToDateTime(reader[EKamis.AddedTime.ToString()]);
                            model.UpdateTime = Convert.ToDateTime(reader[EKamis.UpdateTime.ToString()]);
                            model.Trade_No = reader[EKamis.Trade_No.ToString()].ToString();
                            model.Remark = reader[EKamis.Remark.ToString()].ToString();
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