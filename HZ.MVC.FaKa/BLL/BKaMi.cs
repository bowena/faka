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
    public class BKaMi
    {
        public static bool Insert(KaMiViewModel model)
        {
            string sql = string.Empty; ;
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sql = "INSERT INTO Kamis ("
             + EKamis.Content.ToString() + ","
             + EKamis.State.ToString() + ","
             + EKamis.Product_Id.ToString() + ","
             + EKamis.Remark.ToString() + ","
             + EKamis.AddedTime.ToString() + ","
             + EKamis.UpdateTime.ToString()
             + ") VALUES ("
             + "@" + EKamis.Content.ToString() + ","
             + "@" + EKamis.State.ToString() + ","
             + "@" + EKamis.Product_Id.ToString() + ","
             + "@" + EKamis.Remark.ToString() + ","
             + "@" + EKamis.AddedTime.ToString() + ","
             + "@" + EKamis.UpdateTime.ToString()
             + ");";
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Id.ToString(), Value = model.Id });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Content.ToString(), Value = model.Content });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.State.ToString(), Value = model.State });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Product_Id.ToString(), Value = model.Product_Id });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Remark.ToString(), Value = model.Remark });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.AddedTime.ToString(), Value = model.AddedTime });
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.UpdateTime.ToString(), Value = model.UpdateTime });
            
            int count = ManagerSqlite.ExecuteNonQuery(sql, ps.ToArray());
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool Update(KaMiViewModel model)
        {
            StringBuilder sb = new StringBuilder(2048);
            List<SQLiteParameter> ps = new List<SQLiteParameter>();
            sb.Append("UPDATE Kamis SET ");

            sb.Append(EKamis.Content.ToString() + "=@" + EKamis.Content.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Content.ToString(), Value = model.Content });

            sb.Append(EKamis.State.ToString() + "=@" + EKamis.State.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.State.ToString(), Value = model.State });


            sb.Append(EKamis.Product_Id.ToString() + "=@" + EKamis.Product_Id.ToString() + ",");
            ps.Add(new SQLiteParameter() { ParameterName = EKamis.Product_Id.ToString(), Value = model.Product_Id });


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

        public static bool Delete(int id)
        {
            string sql = "delete from Kamis where Id = " + id;

            int count = ManagerSqlite.ExecuteNonQuery(sql, null);
            if (count > 0)
                return true;
            else
                return false;
        }

        public static List<KaMiViewModel> Search()
        {
            string sql = "select * from Kamis ";

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
                        model.AddedTime = Convert.ToDateTime(reader[EKamis.AddedTime.ToString()]);
                        model.UpdateTime = Convert.ToDateTime(reader[EKamis.UpdateTime.ToString()]);
                        model.Remark = reader[EKamis.UpdateTime.ToString()].ToString();
                        modelArr.Add(model);
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
                            model.AddedTime = Convert.ToDateTime(reader[EKamis.AddedTime.ToString()]);
                            model.UpdateTime = Convert.ToDateTime(reader[EKamis.UpdateTime.ToString()]);
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