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