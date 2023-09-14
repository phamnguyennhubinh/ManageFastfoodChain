using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FastFoodChain16.Models;

namespace FastFoodChain16.Models;

public partial class QuanLyBanFastFood16Context : DbContext
{
    public QuanLyBanFastFood16Context()
    {
    }

    public QuanLyBanFastFood16Context(DbContextOptions<QuanLyBanFastFood16Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiSp> LoaiSps { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
    public DbSet<MyEntities> MyEntities { get; set; }   
    public DbSet<MonthlySaleResult> MonthlySales { get; set; }

    public virtual DbSet<MonthlySaleResult> MonthlySalesResults { get; set; }

    [DbFunction("spMonthlySales", "dbo")]
    public virtual IQueryable<MonthlySaleResult> spMonthlySales(int year, int month)
    {
        var yearParam = new SqlParameter("@year", year);
        var monthParam = new SqlParameter("@month", month);

        return Set<MonthlySaleResult>().FromSqlRaw("EXEC spMonthlySales @year, @month", yearParam, monthParam);
    }
    public DbSet<DonHangKH> DonHangKHs { get; set; }

    // Định nghĩa phương thức tương ứng với Stored Procedure
    [DbFunction("spDonHang", "dbo")]
    public virtual IQueryable<DonHangKH> spDonHang(int maTK)
    {
        var maTKparam = new SqlParameter("@maTK", maTK);
        return Set<DonHangKH>().FromSqlRaw("EXEC spDonHang @maTK", maTKparam);
    }
    public DbSet<RevenueByMonth> ThongKeDoanhThu { get; set; }
    public virtual DbSet<RevenueByMonth> RevenueByMonth { get; set; }
    [DbFunction("sp_ThongKeDoanhThu", "dbo")]
    public virtual IQueryable<RevenueByMonth> sp_ThongKeDoanhThu(int thang, int nam)
    {

        var monthParam = new SqlParameter("@thang", thang);
        var yearParam = new SqlParameter("@nam", nam);

        return Set<RevenueByMonth>().FromSqlRaw("sp_ThongKeDoanhThu @thang, @nam", monthParam, yearParam);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NHBNH482D;Initial Catalog=QuanLyBanFastFood16;Integrated Security=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            modelBuilder.Entity<MonthlySaleResult>().HasNoKey();
            entity.HasKey(e => new { e.MaDh, e.MaSp }).HasName("PK__ChiTietD__F557D6E08473FE6C");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.DonGia).HasColumnType("money");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__48CFD27E");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaSP__49C3F6B7");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__ChiTietDon__MaTK__4AB81AF0");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__27258661FADA726C");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.NgayGiao).HasColumnType("datetime");
            entity.Property(e => e.Omessage).HasColumnName("OMessage");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__DonHang__MaKH__3B75D760");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__DonHang__MaNV__3C69FB99");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1EFB6FA124");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoKh)
                .HasMaxLength(100)
                .HasColumnName("HoKH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<LoaiSp>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiSP__730A575921687370");

            entity.ToTable("LoaiSP");

            entity.Property(e => e.Discription).HasColumnType("ntext");
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A185DEB2C283B06");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(60);
            entity.Property(e => e.Sdt)
                .HasMaxLength(24)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(40)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A37DE3008");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.HoNv)
                .HasMaxLength(100)
                .HasColumnName("HoNV");
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.NgayVaoLam).HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C8294FF02");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.DonGia).HasColumnType("money");
            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.TenSp)
                .HasMaxLength(200)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK__SanPham__MaLoai__4316F928");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK__SanPham__MaNCC__440B1D61");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__272500706C8CEBE4");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.MatKhau).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<FastFoodChain16.Models.Cart>? Cart { get; set; }
}
