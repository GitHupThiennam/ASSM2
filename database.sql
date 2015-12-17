CREATE DATABASE QLTB
ON PRIMARY (NAME = QLTB,
FILENAME= "D:\Detai\Database\QLTB.mdf",
SIZE=50MB, MAXSIZE = 200MB, FILEGROWTH = 10%)
LOG ON (NAME = QLTB_Log,
FILENAME= "D:\Detai\Database\QLTB_Log.ldf", 
SIZE=10MB, MAXSIZE = UNLIMITED, FILEGROWTH = 5MB)
use QLTB
create table tblNguoiDung
(tendangnhap varchar(50) constraint TDN_Pk primary key,
matkhau varchar(50)not null,
tendaydu nvarchar(2500),
toanquyen bit,duockichhoat bit);
create table Quyen
(
maquyen varchar(50) constraint MQ_PK primary key,
tenquyen nvarchar(2500)
);
create table tblPhanQuyen
(tendangnhap varchar(50)constraint TD_N_FK foreign key (tendangnhap)references NguoiDung(tendangnhap),
maquyen varchar(50) constraint M_Q_PK foreign key (maquyen) references Quyen(maquyen));
create table tblThietBi
(MaThietBi varchar(50) constraint MTB_PK primary key,
TenThietBi nvarchar(2500) not null,
TinhTrang nvarchar(2500),
NgayNhap varchar(50),
TinhNang nvarchar(2500));
use QLTB
create table tblPhieuMuonTra
(ngaymuon datetime ,
ngaytra datetime,
tendangnhap varchar(50) constraint DNT_FK foreign key (tendangnhap) references NguoiDung(tendangnhap),
MaThietBi varchar(50) constraint MTB_FK foreign key (MaThietBi)references ThietBi(MaThietBi)
);
