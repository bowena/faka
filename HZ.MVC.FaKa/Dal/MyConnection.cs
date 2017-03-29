using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace HZ.MVC.FaKa.Dal
{
    public class MyConnection
    {
        /// <summary>
        ///设置数据库连接字典
        /// </summary>
        public static Dictionary<string, SQLiteConnection> ConnectionDictionary = new Dictionary<string, SQLiteConnection>();

        public static Dictionary<string, LockInfo> LockDictionary = new Dictionary<string, LockInfo>();
        private static object diclock = "HZ.MVC.FaKa.Dal.MyConnection.GetSQLiteConnection";
        private static string DBString = @"data source={0};synchronous=OFF;Pooling=false;cache size=20000;page size=" + (16 * 1024) + ";";
        private static ReaderWriterLock rwlock = new ReaderWriterLock();
        public static SQLiteConnection GetSQLiteConnection()
        {
            try
            {
                //注册一下 排序函数
                RegisterFunction.DoRegister();
                lock (diclock)
                {
                    SQLiteConnection conn = null;
                    conn = new System.Data.SQLite.SQLiteConnection(string.Format(DBString, HttpContext.Current.Server.MapPath("~/App_Data/kami.db")));
                    conn.Open();
                    return conn;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static bool Close(SQLiteConnection conn)
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                conn.Dispose();
                SQLiteConnection.ClearPool(conn);
                GC.Collect();
                System.Threading.Thread.Sleep(50);//等待连接池关闭

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        #region 获取读写权限
        public static bool AcquireRead(ref DbConnection oldconn)
        {
            return true;
        }
        public static void AcquireWrite(ref DbConnection oldconn, Action callback)
        {

            if (rwlock.IsReaderLockHeld == true)
            {
                rwlock.UpgradeToWriterLock(10000);
                try
                {
                    if (callback != null)
                    {
                        callback();
                    }
                }
                catch (System.Exception ex)
                {
                   // MyLog.DalSqlite.Print.Debug("AcquireWrite error:" + ex);
                }
                finally
                {
                    ReleaseWrite(ref oldconn);
                }

            }
        }
     
        public static void ReleaseWrite(ref DbConnection oldconn)
        {
            try
            {
                LockInfo locker = new LockInfo();
                if (LockDictionary.TryGetValue(oldconn.DataSource, out locker))
                {
                    if (locker.locker.IsWriterLockHeld == true)
                    {
                        locker.locker.ReleaseWriterLock();
                    }

                }
            }
            catch (System.Exception ex)
            {
            }

        }
        public static void ReleaseAcquire(ref DbConnection oldconn)
        {
            try
            {

            }
            catch (System.Exception ex)
            {

            }

        }

        #endregion 获取读写权限
    }

    public class LockInfo
    {
        public LockInfo()
        {
            locker = new ReaderWriterLock();
            ThreadID = Thread.CurrentThread.ManagedThreadId;
        }

        public ReaderWriterLock locker { get; set; }
        public int ThreadID { get; set; }
    }

    public class RegisterFunction
    {
        //单例
        private static object Instence = null;

        //是否已经注册过
        private static bool isRegistered = false;

        public static bool IsRegistered
        {
            get { return RegisterFunction.isRegistered; }
        }

        private RegisterFunction()
        {
        }

        public static void DoRegister()
        {
            if (Instence == null)
            {//注册
                SQLiteFunction.RegisterFunction(typeof(SQLiteCollation_PinYin));    // 注入
                SQLiteFunction.RegisterFunction(typeof(SQLiteCollation_Length));
                SQLiteFunction.RegisterFunction(typeof(SQLiteCollation_Number));
                isRegistered = true;
                Instence = new object();
            }
            else
            {
                return;
            }
        }

        // 中文拼音比较规则
        [SQLiteFunction(FuncType = FunctionType.Collation, Name = "PinYin")]
        private class SQLiteCollation_PinYin : SQLiteFunction
        {
            public override int Compare(string x, string y)
            {
                return string.Compare(x, y, System.StringComparison.CurrentCultureIgnoreCase);
            }
        }

        // 长度排序规则
        [SQLiteFunction(FuncType = FunctionType.Collation, Name = "Length")]
        private class SQLiteCollation_Length : SQLiteFunction
        {
            public override int Compare(string x, string y)
            {
                int xl = Encoding.Default.GetByteCount(x);
                int yl = Encoding.Default.GetByteCount(y);
                if (xl == yl)
                {
                    return 0;
                }
                else if (xl > yl)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }


            }
        }


        // 数字排序规则
        [SQLiteFunction(FuncType = FunctionType.Collation, Name = "Number")]
        private class SQLiteCollation_Number : SQLiteFunction
        {
            public override int Compare(string x, string y)
            {
                int a;
                if (!int.TryParse(x, out a))
                {
                    a = 0;
                }
                int b;
                if (!int.TryParse(y, out b))
                {
                    b = 0;
                }
                if (a == b)
                {
                    return 0;
                }
                else if (a > b)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }


            }
        }
    }
}