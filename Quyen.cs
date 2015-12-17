using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyThietBi.DB
{
    public class Quyen
    {
        public string MaQuyen
        {
            get;
            set;
        }
        public string TenQuyen
        {
            get;
            set;
        }
        public Quyen(string maquyen, string tenquyen)
        {
            MaQuyen = maquyen;
            TenQuyen = tenquyen;
        }
        public void Save(bool isinsert)
        {
            if (TenQuyen != null && MaQuyen != null)
            {
                ClassDatabase db = new ClassDatabase();
                String sql = "SELECT * FROM tblQuyen WHERE maquyen=@maquyen";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@maquyen", MaQuyen));
                string command = "";
                if (db.Select(sql, arr).Rows.Count > 0)
                {
                    if (isinsert)
                    {
                        throw new Exception("Quyen nay da ton tai. Vui long chon ma quyen khac.");
                    }
                    else
                    {
                        //update
                        command = "UPDATE tblQuyen SET tenquyen=@tenquyen WHERE maquyen=@maquyen";
                    }
                }
                else
                {
                    //insert
                    command = "INSRET INTO tblQuyen(maquyen,tenquyen) VALUES(@maquyen,@tenquyen)";
                }
                arr = new ArrayList();
                arr.Add(new SqlParameter("@maquyen", MaQuyen));
                arr.Add(new SqlParameter("@tenquyen", TenQuyen));
                db.DoCommand(command, arr);
            }
            else
            {
                throw new Exception("Quyen nay chua du thong tin");
            }
        }

        public static ArrayList DanhSachQuyenHeThong
        {
            get
            {
                ArrayList danhsach = new ArrayList();
                string sql = "SELECT * FROM tblQuyen";

                ClassDatabase db = new ClassDatabase();
                DataTable table = db.Select(sql, new ArrayList());
                foreach (DataRow row in table.Rows)
                {
                    danhsach.Add(new Quyen(row["maquyen"].ToString(), row["tenquyen"].ToString()));
                }
                return danhsach;
            }
        }

        public static Quyen Load(string maquyen)
        {
           
            string sql = "SELECT * FROM tblQuyen WHERE maquyen=@maquyen";
            ClassDatabase db = new ClassDatabase();
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@maquyen", maquyen));
            DataTable table = db.Select(sql, arr);
            if (table.Rows.Count > 0)
            {
                DataRow row=table.Rows[0];
                return new Quyen(row["maquyen"].ToString(), row["tenquyen"].ToString());
            }
            return null;
        }
    }
}
