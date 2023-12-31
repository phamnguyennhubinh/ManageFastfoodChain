﻿-------------_Tao moi database--------
CREATE DATABASE [QuanLyBanFastFood16]
GO
----------Thiet-ke-co-so-du-lieu-----------
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [int] IDENTITY(1,1) Primary key,
	[HoNV] [nvarchar](100) NOT NULL,
	[TenNV] [nvarchar](50) NOT NULL,
	[NgaySinh] [datetime] NULL,
	[NgayVaoLam] [datetime] NULL,
	[SDT] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[GhiChu] ntext NULL)

CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) Primary key,
	[HoKH] [nvarchar](100) NOT NULL,
	[TenKH] [nvarchar](50) NOT NULL,
	[SDT] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](500) NULL)

CREATE TABLE [dbo].[DonHang](
	[MaDH] [int] IDENTITY(1,1) Primary key,
	[MaKH] [int] Foreign key references KhachHang(MaKH),
	[MaNV] [int] Foreign key references NhanVien(MaNV),
	[OMessage] [nvarchar](max) NULL,
	[NgayDat] [datetime] NULL,
	[NgayGiao] [datetime] NULL,
	[DiaChiNhan] [nvarchar](max) NULL)

CREATE TABLE [dbo].[LoaiSP](
	[MaLoai] [int] IDENTITY(1,1) Primary Key,
	[TenLoai] [nvarchar](100) NOT NULL,
	[Discription] ntext NULL)

CREATE TABLE [dbo].[NhaCungCap](
[MaNCC] [int] IDENTITY(1,1) Primary key,
[TenNCC] [nvarchar](40) NOT NULL,
[SDT] [nvarchar](24) NULL,
[DiaChi] [nvarchar](60) NULL)

CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) Primary key,
	[MaLoai] [int] Foreign key references LoaiSP(MaLoai),
	[MaNCC] [int] Foreign key references NhaCungCap(MaNCC),
	[TenSP] [nvarchar](200) NOT NULL,
	[DonGia] [money] NULL,
	[MoTa] [nvarchar](max) NULL)

CREATE TABLE [dbo].[TaiKhoan](
	[MaTK] [int] IDENTITY(1,1) Primary key,
	[TenDangNhap] [nvarchar](30) NOT NULL,
	[MatKhau] [nvarchar](20) NOT NULL)

CREATE TABLE [dbo].[ChiTietDonHang](
	[MaDH] [int] Foreign key references DonHang(MaDH),
	[MaSP] [int] Foreign key references SanPham(MaSP),
	[SoLuong] [int] NOT NULL,
	[DonGia] [money] NOT NULL,
	[MaTK] [int] Foreign key references TaiKhoan(MaTK),
	[Discount] [real] NOT NULL,
	Primary key(MaDH, MaSP))

-------------Them-du-lieu-Nha-Cung-Cap-------------
INSERT INTO NhaCungCap(TenNCC, SDT, DiaChi) 
VALUES ('ChickenChicken','0129118963','TPHCM'),
('PastaCo','0987234567',N'Hà Nội'),
('PepsiCo','0993318620','TPHCM'),
('CocaCola','0659862044',N'Vĩnh Long'),
(N'Nestlé','0208763456','Long An'),
('PotatoCo','0981234567','TPHCM'),
('NoiceBurger','0908109006','TPHCM'),
('SuperSausage','0122347890',N'Ðà Lạt')
GO
-------------Them-du-lieu-Loai-San-Pham-------------
INSERT INTO LoaiSP(TenLoai, Discription) 
VALUES (N'Gà rán',NULL),
(N'Khoai tây chiên',NULL),
(N'Hamburger',NULL),
('Xúc xích',NULL),
(N'Mì ý',NULL),
(N'Nước ngọt',NULL),
(N'Nước suối',NULL)
GO
----------------Them-du-lieu-San-Pham----------------
INSERT INTO SanPham(MaLoai,MaNCC,TenSP,DonGia,MoTa)
VALUES (1,1,N'Gà truyền thống',39000,NULL),
(1,1,N'Gà giòn không cay',39000,NULL),
(1,1,N'Gà giòn cay',44000,NULL),
(1,1,N'Gà viên',33000,NULL),
(3,7,N'Burger Tôm',35000,NULL),
(3,7,N'Burger Bò',35000,NULL),
(3,7,N'Burger Heo',30000,NULL),
(3,7,N'Burger Gà',30000,NULL),
(5,2,N'Mì ý sốt bò bằm',49000,NULL),
(5,2,N'Mì ý hải sản',65000,NULL),
(5,2,N'Mì ý sốt kem',65000,NULL),
(4,8,N'Xúc xích phô mai cay',29000,NULL),
(4,8,N'Xúc xích chiên bo tỏi',29000,NULL),
(2,6,N'Khoai tây chiên truyền thống',24000,NULL),
(2,6,N'Khoai tây chiên lắc phô mai',30000,NULL),
(6,3,N'Pepsi',15000,NULL),
(6,3,N'7Up',15000,NULL),
(6,4,N'Coca Cola',15000,NULL),
(7,3,N'Aquafina',12000,NULL),
(7,5,N'Lavie',12000,NULL)
GO
-----------------Them-du-lieu-Nhan-Vien--------------------
INSERT INTO NhanVien(HoNV,TenNV,NgaySinh,NgayVaoLam,SDT,Email,DiaChi,GhiChu)
VALUES (N'Nguyễn Tú',N'Hồng','2000/2/3','2021/6/4','0329117043','thihong@gmail.com','Long An',NULL),
(N'Nguyễn Trúc',N'Phương','2000/9/2','2021/12/1','0983373138','trucphuong@gmail.com','Dak Lak',NULL),
(N'Hoàng Trang',N'Hương','2001/6/1','2020/10/5','0933195059','huong12@gmail.com','An Giang',NULL),
(N'Nguyễn Thị Cẩm','Nhung','2002/8/9','2022/12/12','0329887055','nhung123@gmail.com','TPHCM',NULL),
(N'Nguyễn Trúc',N'Linh','1999/10/9','2020/11/3','0914194079','linhnguyen@gmail.com','TPHCM',NULL),
(N'Phạm Quế',N'Trâm','1998/4/18','2021/8/3','0348029056','tram1994@gmail.com',N'Cà Mau',NULL),
(N'Ðinh Thanh',N'Nhàn','1997/5/2','2023/1/9','0987125610','nhandinhthi@gmail.com',N'Bến Tre',NULL)
GO
-----------------Them-du-lieu-Khach-Hang--------------------
INSERT INTO KhachHang(HoKH,TenKH,SDT,Email,DiaChi)
VALUES (N'Nguyễn Văn',N'Mãi','0126782355','maivan@gmail.com','TPHCM'),
(N'Ðào Thị',N'Duyên','0983785283','duyenthi@gmail.com','An Giang'),
(N'Trần Văn','Anh','0917267989','ahtran@mgmail.com','Đăk Lăk'),
(N'Nguyễn Thi',N'Thảo','0327894563','chipchip@gmail.com','TPHCM'),
(N'Phạmm Thanh','An','0983456789','antham@gmail.com',N'Ðăk Nông'),
(N'Phạm Tấn',N'Lộc','0657887877','locphamtan@gmail.com',N'Hà Nội'),
(N'Ðặng Tấn','An','0123112322','andavid@gmail.com','Cà Mau'),
(N'Nguyễn Hoàng Phương','Nghi','0987112311','nghinghi@gmail.com','TPHCM'),
(N'Ðinh Tấn',N'Nghị','0990343456','nghi098@gmail.com','Long An'),
(N'Trần Minh',N'Tiến','0123121245','mtbien@gmail.com','Gia Lai'),
(N'Huỳnh Nguyễn Tường','Vy','0909123451','vyvy@gmail.com',N'Kiên Giang'),
(N'Mai Thu','Trinh','0911563969','maithutrinh@gmail.com',N'Hà Tĩnh'),
(N'Nguyễn Thị',N'Huệ','0911563934','hue@gmail.com',N'Kiên Giang'),
(N'Nguyễn Thanh',N'Bảo','0911563869','bao2@gmail.com',N'TPHCM'),
(N'Đinh Thanh','Hoa','0981563969','hoa12@gmail.com',N'TPHCM'),
(N'Trần Thị Thu',N'Hương','0981560969','houong@gmail.com',N'TPHCM'),
(N'Phan Đào',N'Hào','0981060969','haug@gmail.com',N'An Giang'),
(N'Đồng Hữu','Than','0981860969','hathan@gmail.com',N'An Giang'),
(N'Võ Mạnh',N'Cường','0381860969','hatcuong@gmail.com',N'TPHCM'),
(N'Võ Thanh','Qui','0381860909','hatng@gmail.com',N'TPHCM'),
(N'Lê Nhật',N'Thanh','0281860909','hthanh@gmail.com',N'TPHCM'),
(N'Đào Như',N'Huyền','0281850909','huyen1@gmail.com',N'Long An'),
(N'Huyền Trang',N'Nhã','0381850909','nhanhan@gmail.com',N'TPHCM')
GO
-----------------Them-du-lieu-Tai-Khoan---------------------
INSERT INTO TaiKhoan(TenDangNhap,MatKhau)
VALUES ('0126782355','123NhuBinhna'),
('0983785283','123456Abn1'),
('0917267989','4561289dbNA'),
('0327894563','7899178Bhnd'),
('0983456789','abc12j39dA12'),
('0657887877','123nhubinhBxyz'),
('0123112322','12dcAdhnubini'),
('0987112311','13bnhu567B'),
('0990343456','password212B'),
('0123121245','nbiz098hB1'),
('0909123451','matkhau12Pass'),
('0911563969','cobeWord0921'),
('0911563934','xinhtuoi25267Ti'),
('0911563869','hiphOpneverdie12'),
('0981563969','luv2554NBMT'),
('0981560969','applewTTnhu12345'),
('0981060969','haokiet1234mnA'),
('0981860969','nguyenu7891mAB'),
('0381860969','nguencuong09K12'),
('0381860909','saobang098NK'),
('0281860909','nice76789MN'),
('0281850909','iphonen876NBM'),
('0381850909','mayne876MiO'),
('0329117044','0329117044Admin'),
('0965484189','0965484189Admin'),
('0933195059','0933195059Admin'),
('0329887055','0329887055Admin'),
('0943194079','09843194079Admin'),
('0348029056','0348029056Admin'),
('0987125610','0987125610Admin')
GO
--23
-----------------------Them-Du-Lieu-Don-hang----------------------
INSERT INTO DonHang(MaKH,MaNV,OMessage,NgayDat,NgayGiao,DiaChiNhan)
VALUES (1,7,NULL,'2023/1/5','2023/1/6',N'23 Phạm Ngũ Lão, P5, Gò Vấp, TPHCM'),
(2,1,NULL,'2023/1/5','2023/1/6',N'79 Nguyễn Thái Sơn, P4, Gò Vấp, TPHCM'), 
(3,5,NULL,'2023/1/11','2023/1/12',N'96 Ðường số 1, P7, Thủ Ðức, TPHCM'),
(4,6,NULL,'2023/1/11','2023/1/12',N'371 Nguyễn Kiệm, P3, Gò Vấp, TPHCM'),
(5,2,NULL,'2023/1/12','2023/1/13',N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(6,2,NULL,'2023/1/15','2023/1/16',N'32 đường Trần Huy Liệu, P3, Quận 3, TPHCM'),
(7,4,NULL,'2023/2/20','2023/2/21',N'36 đường Trần Huy Liệu, P3, Quận 3, TPHCM'),
(8,3,NULL,'2023/3/25','2023/3/26',N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(9,7,NULL,'2023/4/10','2023/4/11',N'123 đường Nguyễn Thái Sơn, P4, Quận Gò Vấp, TPHCM'),
(10,6,NULL,'2023/4/21','2023/4/22',N'45 đường Trường Chinh, P8, Quận Tân Phú, TPHCM'),
(11,4,NULL,'2023/5/22','2023/5/23',N'12/9 đường Cầu Xéo, phường Tân Quý, quận Tân Phú, TPHCM'),
(12,3,NULL,'2023/5/24','2023/5/25',N'34/2 đường Phan Văn Trị, P5, Gò Vấp, TPHCM'),
(13,5,NULL,'2023/5/24','2023/5/25',N'55 Trần Hưng Đạo, quận 7, TPHCM'),
(14,3,NULL,'2023/5/25','2023/5/26',N'96/9/7 Trần Bá Giao, P8, Quận 10, TPHCM'),
(15,3,NULL,'2023/5/25','2023/5/26',N'89 Phan Văn Mãng, quận 9, TPHCM'),
(1,2,NULL,'2023/6/3','2023/6/4',N'23 Phạm Ngũ Lão, P5, Gò Vấp, TPHCM'),
(10,5,NULL,'2023/6/11','2023/6/12',N'45 đường Trường Chinh, P8, Quận Tân Phú, TPHCM'),
(8,2,NULL,'2023/6/11','2023/6/12',N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(16,3,NULL,'2023/6/12','2023/6/13',N'91 Quang Trung, P4, Quận Gò Vấp, TPHCM'),
(8,3,NULL,'2023/6/12','2023/6/13',N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(12,7,NULL,'2023/6/12','2023/6/13',N'34/2 đường Phan Văn Trị, P5, Gò Vấp, TPHCM'),
(7,1,NULL,'2023/6/12','2023/6/13',N'36 đường Trần Huy Liệu, P3, Quận 3, TPHCM'),
(5,1,NULL,'2023/6/14','2022/6/15',N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(2,7,NULL,'2023/7/15','2023/7/16',N'79 Nguyễn Thái Sơn, P4, Gò Vấp, TPHCM'), 
(5,7,NULL, '2023/7/25','2023/7/26',N'98 Nguyễn Thái Sơn, P4, Gò Vấp, TPHCM'), 
(5,3,NULL, '2023/8/26','2023/8/27',N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(6,5,NULL,'2023/8/26','2023/8/27',N'32 đường Trần Huy Liệu, P3, Quận 3, TPHCM'),
(9,7,NULL,'2023/8/26','2023/8/27',N'123 đường Nguyễn Thái Sơn, P4, Quận Gò Vấp, TPHCM'),
(10,1,NULL,'2023/8/26','2023/8/27',N'45 đường Trường Chinh, P8, Quận Tân Phú, TPHCM'),
(12,2,NULL,'2023/8/26','2023/8/27',N'34/2 đường Phan Văn Trị, P5, Gò Vấp, TPHCM'),
(5,6,NULL,'2023/8/27','2023/8/28',N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(8,5,NULL,'2023/8/28',NULL,N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(15,7,NULL,'2023/8/28',NULL,N'89 Phan Văn Mãng, quận 9, TPHCM'),
(5,2,NULL,'2023/8/28',NULL,N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(7,1,NULL,'2023/8/28',NULL,N'36 đường Trần Huy Liệu, P3, Quận 3, TPHCM'),
(12,7,NULL,'2023/9/2',NULL,N'34/2 đường Phan Văn Trị, P5, Gò Vấp, TPHCM'),
(14,2,NULL,'2023/9/2',NULL,N'96/9/7 Trần Bá Giao, P8, Quận 10, TPHCM'),
(13,5,NULL,'2023/9/3',NULL,N'55 Trần Hưng Đạo, quận 7, TPHCM'),
(5,7,NULL,'2023/9/3',NULL,N'12 đường Trần Thái Tông, P1, Quận Phú Nhuận, TPHCM'),
(10,2,NULL,'2023/9/3',NULL,N'45 đường Trường Chinh, P8, Quận Tân Phú, TPHCM'),
(9,1,NULL,'2023/9/3',NULL,N'123 đường Nguyễn Thái Sơn, P4, Quận Gò Vấp, TPHCM'),
(8,3,NULL,'2023/9/3',NULL,N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(9,6,NULL,'2023/9/4',NULL,N'123 đường Nguyễn Thái Sơn, P4, Quận Gò Vấp, TPHCM'),
(8,2,NULL,'2023/9/4',NULL,N'91 đường số 1, P6, Quận Gò Vấp, TPHCM'),
(10,5,NULL,'2023/9/4',NULL,N'45 đường Trường Chinh, P8, Quận Tân Phú, TPHCM')
GO
-----------------------Them-Du-Lieu-Chi-Tiet-Don-hang----------------------
INSERT INTO ChiTietDonHang(MaDH,MaSP,SoLuong,DonGia,MaTK,Discount)
VALUES (1,1,1,39000,1,0),
(2,2,2,39000,2,0),
(3,4,2,33000,3,0),
(4,13,4,29000,4,0),
(5,3,1,44000,5,0),
(5,18,1,15000,5,0),
(6,5,1,35000,6,0),
(6,17,1,15000,6,0),
(6,14,1,24000,6,0),
(7,3,2,44000,7,0),
(7,16,2,15000,7,0),
(8,1,1,39000,8,0),
(8,6,1,35000,8,0),
(8,20,2,12000,8,0),
(9,10,1,65000,9,0),
(9,19,1,12000,9,0),
(10,9,1,49000,10,0),
(10,15,1,30000,10,0),
(11,2,1,39000,11,0),
(11,13,1,29000,11,0),
(11,20,1,12000,11,0),
(12,11,1,65000,12,0),
(12,16,1,15000,12,0),
(13,10,3,65000,13,0),
(13,9,2,49000,13,0),
(13,1,2,39000,13,0),
(13,20,2,12000,13,0),
(14,5,3,35000,14,0),
(14,7,5,30000,14,0),
(14,8,4,30000,14,0),
(14,10,2,65000,14,0),
(14,11,10,65000,14,0),
(14,20,12,12000,14,0),
(14,4,6,33000,14,0),
(14,14,3,24000,14,0),
(14,13,2,29000,14,0),
(14,12,8,29000,14,0),
(14,1,12,39000,14,0),
(15,1,4,39000,15,0),
(15,2,6,39000,15,0),
(15,17,2,15000,15,0),
(15,16,4,15000,15,0),
(16,1,4,39000,1,0),
(16,3,6,44000,1,0),
(16,15,4,30000,1,0),
(16,12,7,29000,1,0),
(17,1,3,39000,10,0),
(17,2,6,39000,10,0),
(17,14,2,24000,10,0),
(17,5,1,35000,10,0),
(17,11,6,65000,10,0),
(17,4,2,33000,10,0),
(18,3,1,44000,8,0),
(18,1,3,39000,8,0),
(18,2,4,39000,8,0),
(19,5,1,35000,16,0),
(20,7,3,30000,8,0),
(20,1,3,39000,8,0),
(21,6,3,35000,12,0),
(21,1,3,39000,12,0),
(21,5,3,35000,12,0),
(21,7,3,30000,12,0),
(21,8,4,30000,12,0),
(21,14,5,24000,12,0),
(22,1,9,39000,7,0),
(22,17,5,15000,7,0),
(23,14,5,24000,5,0),
(24,1,12,39000,2,0),
(25,12,4,29000,5,0),
(26,3,4,44000,5,0),
(26,1,1,39000,5,0),
(27,16,1,15000,6,0),
(28,7,3,30000,9,0),
(29,20,1,12000,10,0),
(29,19,2,12000,10,0),
(29,14,1,24000,10,0),
(30,4,3,33000,12,0),
(31,1,8,39000,5,0),
(31,3,2,44000,5,0),
(31,6,1,35000,5,0),
(31,20,1,12000,5,0),
(32,3,5,44000,8,0),
(33,1,3,39000,15,0),
(33,3,1,44000,15,0),
(34,1,1,39000,5,0),
(35,10,6,65000,7,0),
(35,20,5,12000,7,0),
(35,16,5,15000,7,0),
(36,1,3,39000,12,0),
(36,2,6,39000,12,0),
(36,3,2,44000,12,0),
(36,5,5,35000,12,0),
(36,7,4,30000,12,0),
(36,10,1,65000,12,0),
(36,12,3,29000,12,0),
(36,15,1,30000,12,0),
(36,16,1,15000,12,0),
(36,18,4,15000,12,0),
(36,20,8,12000,12,0),
(37,1,3,39000,14,0),
(37,3,1,44000,14,0),
(38,5,1,35000,13,0),
(39,4,4,33000,5,0),
(40,1,5,39000,10,0),
(41,3,5,44000,9,0),
(41,5,1,35000,9,0),
(42,4,3,33000,8,0),
(42,19,1,12000,8,0),
(42,6,3,35000,8,0),
(43,1,8,39000,9,0),
(44,12,2,29000,8,0),
(45,10,1,39000,10,0)
GO