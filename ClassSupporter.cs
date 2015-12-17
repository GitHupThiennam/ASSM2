using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QuanLyThietBi.DB
{
    public class ClassSupporter
    {
        public static void BindQuyen(ListBox list, ArrayList danhsachquyen)
        {
            list.Items.Clear();
            foreach (Quyen quyen in danhsachquyen)
            {
                list.Items.Add(quyen);
            }
            list.DisplayMember = "TenQuyen";
            list.ValueMember = "MaQuyen";
        }
        public static void BoundNguoidungListView(ListView lst, ArrayList danhsachnguoidung)
        {
            lst.Items.Clear();
            foreach (NguoiDung nd in danhsachnguoidung)
            {
                string[] view=new string[]{nd.TenDangNhap,nd.TenDayDu,(nd.ToanQuyen? "Có":"Không"),(nd.DuocKichHoat? "Có":"Không")};
                ListViewItem item = new ListViewItem(view, 1);
                item.Tag = nd;
                lst.Items.Add(item);
            }
        }
        public static void BoundThietBiListView(ListView lst, ArrayList danhsachnguoidung)
        {
            lst.Items.Clear();
            foreach (ThietBi nd in danhsachnguoidung)
            {
                string[] view = new string[] { nd.MaThietBi,nd.TenThietBi,nd.TinhTrang,nd.NgayNhap.ToString(),nd.TinhNang };
                ListViewItem item = new ListViewItem(view, 1);
                item.Tag = nd;
                lst.Items.Add(item);
            }
        }
    }
}
