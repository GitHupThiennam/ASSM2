using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace QuanLyThietBi.DB
{
    public class CoSoDuLieu
    {
        public string SaoLuu(string dest_folder)
        {
            string timestring = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string sql = @"BACKUP DATABASE "+CauHinh.Default.Database+@"
                            TO DISK = '" + dest_folder + @"\" + CauHinh.Default.Database + "_" + timestring + @".bak'
                            WITH FORMAT;
                            ";
            ClassDatabase db = new ClassDatabase();
            db.DoCommand(sql, new System.Collections.ArrayList());
            return dest_folder + @"\" + CauHinh.Default.Database + "_" + timestring + @".bak";
        }
        public void PhucHoi(FileInfo fi)
        {
            ClassDatabase db = new ClassDatabase();
            string getpathsql = "SELECT * FROM sys.database_files";
            string logfile = "";
            string databasefile = "";

            string logname = "";
            string dataname = "";
            DataTable dt = db.Select(getpathsql, new System.Collections.ArrayList());
            foreach (DataRow row in dt.Rows)
            {
                if (row["type_desc"].ToString().Equals("ROWS"))
                {
                    databasefile = row["physical_name"].ToString();
                    dataname = row["name"].ToString();
                }
                else if (row["type_desc"].ToString().Equals("LOG"))
                {
                    logfile = row["physical_name"].ToString(); logname = row["name"].ToString();
                }
            }
            string killconnections = @"USE master
                                        ALTER DATABASE "+CauHinh.Default.Database+ @"
                                        SET OFFLINE WITH ROLLBACK IMMEDIATE";
            db = new ClassDatabase("master");
            db.DoCommand(killconnections, new System.Collections.ArrayList());

           /* string sql = @"RESTORE DATABASE [" + CauHinh.Default.Database + @"] 
                            FROM  DISK = N'" + fi.FullName + @"' WITH  FILE = 1,  
                            MOVE N'" + dataname + @"' TO N'"+databasefile+@"',  
                            MOVE N'" + logname + @"' TO N'" +logfile+ @"',  RECOVERY,  NOUNLOAD,  STATS = 10";*/
            string sql = @"RESTORE DATABASE " + CauHinh.Default.Database + @" FROM DISK = '" + fi.FullName + @"' 
            WITH REPLACE";
            db=new ClassDatabase("master");
            db.DoCommand(sql, new System.Collections.ArrayList());
        }

    }
}
