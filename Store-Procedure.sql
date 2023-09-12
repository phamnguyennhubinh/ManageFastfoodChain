ALTER PROC spMonthlySales (@year int, @month int)
AS
BEGIN
	IF EXISTS(SELECT * FROM DonHang WHERE Month(NgayDat) = @month)
	BEGIN
		SELECT c.MaSP, s.TenSP, SUM(c.SoLuong) AS SoLuong
		FROM DonHang d 
		INNER JOIN ChiTietDonHang c ON d.MaDH=c.MaDH
		INNER JOIN SanPham s ON s.MaSP=c.MaSP
		WHERE Year(d.NgayDat)= @year AND Month(d.NgayDat) = @month
		GROUP BY c.MaSP, s.TenSP
	END
	ELSE
		SELECT NULL AS MaSP, NULL AS TenSP, NULL AS SoLuong WHERE 1 = 0;
END

EXEC spMonthlySales 2023, 6
----------------------------------

ALTER PROC spDonHang @maTK int
AS
BEGIN
	IF EXISTS(SELECT * FROM TaiKhoan WHERE MaTK=@maTK)
	BEGIN
		SELECT dh.MaDH,s.TenSP,ctdh.SoLuong,ctdh.DonGia,dh.NgayDat,dh.NgayGiao,ctdh.SoLuong * ctdh.DonGia AS TongTien,dh.DiaChiNhan,dh.OMessage
		FROM TaiKhoan tk
		INNER JOIN ChiTietDonHang ctdh ON tk.MaTK=ctdh.MaTK
		INNER JOIN DonHang dh ON dh.MaDH = ctdh.MaDH
		INNER JOIN SanPham s ON s.MaSP=ctdh.MaSP
		WHERE tk.MaTK = @maTK
	END
	ELSE
	BEGIN
		SELECT NULL AS MaDH, NULL AS TenSP, NULL AS SoLuong, NULL AS DonGia, NULL AS NgayDat, NULL AS NgayGiao, NULL AS TongTien, NULL AS DiaChiNhan, NULL AS OMessage;
	END
END

EXEC spDonHang 1
--------------------
CREATE PROCEDURE sp_ThongKeDoanhThu (@thang int,@nam int)
AS
BEGIN
	IF EXISTS(SELECT * FROM DonHang WHERE Month(NgayDat) = @thang)
	BEGIN
		SELECT SUM(DonGia * SoLuong * (1 -Discount)) AS DoanhThu
		FROM ChiTietDonHang od JOIN DonHang o ON od.MaDH = o.MaDH
		Where MONTH(NgayDat) = @thang AND YEAR(NgayDat) = @nam
	END
	ELSE
		SELECT NULL AS DoanhThu
END

EXEC sp_ThongKeDoanhThu 5, 2023