using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace QuanLyThietBi.DB
{
    public class NguoiDung
    {
        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>
        [Description("Tên đăng nhập của người dùng")]
        [ReadOnly(true)]
        public String TenDangNhap
        {
            get;
            set;
        }
        /// <summary>
        /// Mật khẩu dùng để đăng nhập
        /// </summary>
        [Description("Mật khẩu dùng để đăng nhập hệ thống")]
        [PasswordPropertyText(true)]
        public String MatKhau
        {
            get;
            set;
        }
        [Description("Tên đầy đủ")]
        public String TenDayDu
        {
            get;
            set;
        }
        [Description("Có được toàn quyền hệ thống hay không?")]
        public bool ToanQuyen
        {
            get;
            set;
        }
        [Description("Có được kích hoạt hay không?")]
        public bool DuocKichHoat
        {
            get;
            set;
        }
        public NguoiDung(string tendangnhap, string matkhau, string tendaydu, bool toanquyen, bool duockichhoat)
        {
            TenDangNhap = tendangnhap;
            MatKhau = matkhau;
            TenDayDu = tendaydu;
            ToanQuyen = toanquyen;
            DuocKichHoat = duockichhoat;
        }
        public NguoiDung(string tendangnhap, string matkhau)
        {
            TenDangNhap = tendangnhap;
            MatKhau = matkhau;
        }
        public NguoiDung(string tendangnhap)
        {
            TenDangNhap = tendangnhap;
        }
        public void LuuThongTin(bool isinsert)
        {
            if (TenDangNhap != null && MatKhau != null && ToanQuyen != null && DuocKichHoat != null)
            {
                ClassDatabase db = new ClassDatabase();
                String sql = "SELECT * FROM tblNguoiDung WHERE tendangnhap=@tendangnhap";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                string command = "";
                if (db.Select(sql, arr).Rows.Count > 0)
                {
                    if (isinsert)
                    {
                        throw new Exception("Nguoi dung nay da ton tai. Vui long chon ten dang nhap khac.");
                    }
                    else
                    {
                        //update
                        command = "UPDATE tblNguoiDung SET matkhau=@matkhau,tendaydu=@tendaydu,toanquyen=@toanquyen,duockichhoat=@duockichhoat WHERE  tendangnhap=@tendangnhap";
                    }
                }
                else
                {
                    //insert
                    command = "INSERT INTO tblNguoiDung(tendangnhap,matkhau,tendaydu,toanquyen,duockichhoat) VALUES(@tendangnhap,@matkhau,@tendaydu,@toanquyen,@duockichhoat)";
                }
                arr = new ArrayList();
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                arr.Add(new SqlParameter("@matkhau", MatKhau));
                arr.Add(new SqlParameter("@toanquyen", ToanQuyen));
                arr.Add(new SqlParameter("@duockichhoat", DuocKichHoat));
                SqlParameter paratendaydu = new SqlParameter();
                paratendaydu.ParameterName = "@tendaydu";
                if (TenDayDu == null)
                {
                    paratendaydu.Value = DBNull.Value;
                }
                else
                {
                    paratendaydu.Value = TenDayDu;
                }
                arr.Add(paratendaydu);
                db.DoCommand(command, arr);
            }
            else
            {
                throw new Exception("Nguoi dung nay chua du thong tin");
            }
        }
        public bool DangNhap
        {
            get
            {
                if (MatKhau == null) return false;
                ClassDatabase db = new ClassDatabase();
                String sql = "SELECT * FROM tblNguoiDung WHERE tendangnhap=@tendangnhap AND matkhau=@matkhau";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                arr.Add(new SqlParameter("@matkhau", MatKhau));
                return db.Select(sql, arr).Rows.Count > 0;
            }
        }
        public bool IsExisted
        {
            get
            {
                ClassDatabase db = new ClassDatabase();
                String sql = "SELECT * FROM tblNguoiDung WHERE tendangnhap=@tendangnhap";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                return db.Select(sql, arr).Rows.Count > 0;
            }
        }
        public void ThemQuyen(Quyen quyen)
        {
            string sql = "SELECT * FROM tblPhanQuyen WHERE tendangnhap=@tendangnhap AND maquyen=@maquyen";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@maquyen", quyen.MaQuyen));
            arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
            ClassDatabase db = new ClassDatabase();
            if (db.Select(sql, arr).Rows.Count == 0)
            {
                sql = "INSERT INTO tblPhanQuyen(tendangnhap,maquyen) VALUES(@tendangnhap,@maquyen)";
                arr = new ArrayList();
                arr.Add(new SqlParameter("@maquyen", quyen.MaQuyen));
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                db.DoCommand(sql, arr);
            }
        }
        public void XoaQuyen(Quyen quyen)
        {
            string sql = "DELETE FROM tblPhanQuyen WHERE tendangnhap=@tendangnhap AND maquyen=@maquyen";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@maquyen", quyen.MaQuyen));
            arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
            ClassDatabase db = new ClassDatabase();
            db.DoCommand(sql, arr);
        }
        public void Xoa()
        {
            string sql = "DELETE FROM tblNguoiDung WHERE tendangnhap=@tendangnhap";
            ArrayList arr = new ArrayList();            
            arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
            ClassDatabase db = new ClassDatabase();
            db.DoCommand(sql, arr);
            XoaHetQuyen();
        }
        /// <summary>
        /// Tra ve danh sach quyen cua doi tuong nguoi dung
        /// </summary>
        [Browsable(false)]
        public ArrayList DanhSachQuyenCuaNguoiDung
        {
            get
            {
                ArrayList danhsach=new ArrayList();
                string sql = "SELECT dbo.tblPhanQuyen.maquyen, dbo.tblQuyen.tenquyen FROM dbo.tblQuyen INNER JOIN  dbo.tblPhanQuyen ON dbo.tblQuyen.maquyen = dbo.tblPhanQuyen.maquyen WHERE dbo.tblPhanQuyen.tendangnhap=@tendangnhap";
                ArrayList arr = new ArrayList();
                arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
                ClassDatabase db = new ClassDatabase();
                    DataTable table=db.Select(sql, arr);
                if (table.Rows.Count > 0)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        danhsach.Add(new Quyen(row["maquyen"].ToString(),row["tenquyen"].ToString()));
                    }
                }
                return danhsach;
             }
        }

        public static NguoiDung Load(string tendangnhap)
        {

            string sql = "SELECT * FROM tblNguoiDung WHERE tendangnhap=@tendangnhap";
            ClassDatabase db = new ClassDatabase();
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@tendangnhap", tendangnhap));
            DataTable table = db.Select(sql, arr);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                return new NguoiDung(row["tendangnhap"].ToString(), row["matkhau"].ToString(), row["tendaydu"].ToString(), bool.Parse(row["toanquyen"].ToString()), bool.Parse(row["duockichhoat"].ToString()));
            }
            return null;
        }


        public static ArrayList DanhSachNguoiDungHeThong
        {
            get
            {
                ArrayList danhsach = new ArrayList();
                string sql = "SELECT * FROM tblNguoiDung";

                ClassDatabase db = new ClassDatabase();
                DataTable table = db.Select(sql, new ArrayList());
                foreach (DataRow row in table.Rows)
                {
                    danhsach.Add(new NguoiDung(row["tendangnhap"].ToString(), row["matkhau"].ToString(), row["tendaydu"].ToString(), bool.Parse(row["toanquyen"].ToString()), bool.Parse(row["duockichhoat"].ToString())));
                }
                return danhsach;
            }
        }

        public void XoaHetQuyen()
        {
            string sql = "DELETE FROM tblPhanQuyen WHERE tendangnhap=@tendangnhap";
            ArrayList arr = new ArrayList();            
            arr.Add(new SqlParameter("@tendangnhap", TenDangNhap));
            ClassDatabase db = new ClassDatabase();
            db.DoCommand(sql, arr);
        }
        public void ThemQuyen(ArrayList danhsachquyen)
        {
            XoaHetQuyen();
            foreach (Quyen quyen in danhsachquyen)
            {
                ThemQuyen(quyen);
            }
        }
    }
}
