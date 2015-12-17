using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyThietBi.DB
{
    class ThietBi
    {
        public String MaThietBi
        {
            get;
            set;
        }
        public String TenThietBi
        {
            get;
            set;
        }
        public String TinhTrang
        {
            get;
            set;
        }
        public String NgayNhap
        {
            get;
            set;
        }
        public String TinhNang
        {
            get;
            set;
        }
        public ThietBi(String mathietbi, String tenthietbi, String tinhtrang, String ngaynhap, String tinhnang)
        {
            MaThietBi = mathietbi;
            TenThietBi = tenthietbi;
            TinhTrang = tinhtrang;
            NgayNhap = ngaynhap;
            TinhNang = tinhnang;
    }
        public void luuThongTin(bool isinsert)
        {
            if (MaThietBi != null && TenThietBi != null && TinhTrang != null && NgayNhap != null)
            {
                ClassDatabase db = new ClassDatabase();
                String sql = "SELECT * FROM tblThietBi WHERE MaThietBi=@MaThietBi";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@MaThietBi", MaThietBi));
                string command = "";
                if (db.Select(sql, arr).Rows.Count > 0)
                {
                    if (isinsert)
                    {
                        throw new Exception("Thiet bi nay da ton tai. vui long chon ma khac cho thiet bi nay.");
                    }
                    else
                    {
                        command = "UPDATE tblThietBi SET MaThietBi=@MaThietBi,TenThietBi=@TenThietBi,TinhTrang=@TinhTrang,NgayNhap=@NgayNhap,TinhNang=@TinhNang WHERE MaThietBi=@MaThietBi";
                    }
                }
                else
                {
                    command = "INSERT INTO tblThietBi(MaThietBi,TenThietBi,TinhTrang,NgayNhap,TinhNang) VALUES(@MaThietBi,@TenThietBi,@TinhTrang,@NgayNhap,@TinhNang)";
                }
                arr = new ArrayList();
                arr.Add(new SqlParameter("@MaThietBi", MaThietBi));
                arr.Add(new SqlParameter("@TenThietBi", TenThietBi));
                arr.Add(new SqlParameter("@TinhTrang", TinhTrang));
                arr.Add(new SqlParameter("@NgayNhap", NgayNhap));
                arr.Add(new SqlParameter("@TinhNang", TinhNang));
                SqlParameter paratinhnang = new SqlParameter();
                paratinhnang.ParameterName = "@TinhNang";
                if (TinhNang == null)
                {
                    paratinhnang.Value = DBNull.Value;
                }
                else
                {
                    paratinhnang.Value = TinhNang;
                }
                arr.Add(paratinhnang);
                db.DoCommand(command, arr);
            }
            else
            {
                throw new Exception("Thiet bi chua du thong tin.");
            }
        }
        public static ArrayList DanhSachThietBiHeThong
        {
            
            get {
                ArrayList danhsach = new ArrayList();
                string sql = "SELECT * FROM tblThietBi";
                ClassDatabase db = new ClassDatabase();
                DataTable table = db.Select(sql,new ArrayList());
                foreach(DataRow row in table.Rows)
                {
                    danhsach.Add(new ThietBi(row["MaThietBi"].ToString(), row["TenThietBi"].ToString(), row["TinhTrang"].ToString(), row["NgayNhap"].ToString(), row["TinhNang"].ToString()));
                }
                return danhsach;
            }
        }
        public static ThietBi Load(string mathietbi)
        {
            string sql="SELECT * FROM tblThietBi WHERE MaThietBi=@MaThietBi";
            ClassDatabase db = new ClassDatabase();
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@MaThietBi",mathietbi));
            DataTable table = db.Select(sql,arr);
            if(table.Rows.Count>0)
            {
                DataRow row = table.Rows[0];
                return new ThietBi(row["MaThietBi"].ToString(), row["TenThietBi"].ToString(), row["TinhTrang"].ToString(), row["NgayNhap"].ToString(), row["TinhNang"].ToString());
            }
            return null;
        }
        public void Xoa()
        {
            string sql = "DELETE FROM tblThietBi WHERE MaThietBi=@MaThietBi";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@MaThietBi",MaThietBi));
            ClassDatabase db = new ClassDatabase();
            db.DoCommand(sql,arr);
        }
        public void Them()
        {
            string sql = "SELECT FROM tblThietBi WHERE MaThietBi=@MaThietBi";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@MaThietBi", MaThietBi));
            arr.Add(new SqlParameter("@TenThietBi", TenThietBi));
            arr.Add(new SqlParameter("@TinhTrang", TinhTrang));
            arr.Add(new SqlParameter("@NgayNhap", NgayNhap));
            arr.Add(new SqlParameter("@TinhNang", TinhNang));
            ClassDatabase db = new ClassDatabase();
            if(db.Select(sql,arr).Rows.Count==0){
                sql = "INSERT INTO tblThietBi(MaThietBi,TenThietBi,TinhTrang,NgayNhap,TinhNang) VALUES(@MaThietBi,@TenThietBi,@TinhTrang,@NgayNhap,@TinhNang)";
                arr.Add(new SqlParameter("@MaThietBi", MaThietBi));
                arr.Add(new SqlParameter("@TenThietBi", TenThietBi));
                arr.Add(new SqlParameter("@TinhTrang", TinhTrang));
                arr.Add(new SqlParameter("@NgayNhap", NgayNhap));
                arr.Add(new SqlParameter("@TinhNang", TinhNang));
            db.DoCommand(sql, arr);
            }
        }
    }
}
