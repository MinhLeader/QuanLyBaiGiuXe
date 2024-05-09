using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLBGX.Models;

public partial class QuanLyBaiGiuXeContext : DbContext
{
    public QuanLyBaiGiuXeContext()
    {
    }

    public QuanLyBaiGiuXeContext(DbContextOptions<QuanLyBaiGiuXeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChoDoXe> ChoDoXes { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<GiaoDichThanhToan> GiaoDichThanhToans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuVuc> KhuVucs { get; set; }

    public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; }

    public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }

    public virtual DbSet<LoaiVe> LoaiVes { get; set; }

    public virtual DbSet<MauXe> MauXes { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhuongTienThanhToan> PhuongTienThanhToans { get; set; }

    public virtual DbSet<QuyenHan> QuyenHans { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<VeGuiXe> VeGuiXes { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-1M5E6QBD\\MINHCUTE;Initial Catalog=QuanLyBaiGiuXe;Persist Security Info=True;User ID=sa;Password=28062003;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChoDoXe>(entity =>
        {
            entity.HasKey(e => e.MaChoDoXe).HasName("PK__ChoDoXe__0F3456516C3D7242");

            entity.ToTable("ChoDoXe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LoaiXe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__BienSoX__1EA48E88");

            entity.HasOne(d => d.MaKhuVucNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.MaKhuVuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__MaKhuVu__1DB06A4F");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__D4639533EBF5A97F");

            entity.ToTable("ChucVu");

            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GiaoDichThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDichThanhToan).HasName("PK__GiaoDich__9ECF47B84F8F383B");

            entity.ToTable("GiaoDichThanhToan");

            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGiaoDichNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaGiaoDich)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaGia__3493CFA7");

            entity.HasOne(d => d.MaPhuongTienNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaPhuongTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaPhu__3587F3E0");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E3E115619");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D105341917283F").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenKh)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<KhuVuc>(entity =>
        {
            entity.HasKey(e => e.MaKhuVuc).HasName("PK__KhuVuc__0676EB831291E264");

            entity.ToTable("KhuVuc");

            entity.Property(e => e.TenKhuVuc)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LichSuGiaoDich>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__LichSuGi__0A2A24EB336BB7F8");

            entity.ToTable("LichSuGiaoDich");

            entity.Property(e => e.ThoiGianGiaoDich).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaVeNavigation).WithMany(p => p.LichSuGiaoDiches)
                .HasForeignKey(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuGiao__MaVe__2FCF1A8A");
        });

        modelBuilder.Entity<LoaiTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTaiKhoan).HasName("PK__LoaiTaiK__5F6E141CE15B7393");

            entity.ToTable("LoaiTaiKhoan");

            entity.Property(e => e.TenLoaiTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoaiVe>(entity =>
        {
            entity.HasKey(e => e.MaLoaiVe).HasName("PK__LoaiVe__1224E2F75571797D");

            entity.ToTable("LoaiVe");

            entity.Property(e => e.GiaVe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenLoaiVe)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MauXe>(entity =>
        {
            entity.HasKey(e => e.MaMauXe).HasName("PK__MauXe__487AF6FD2B1D1C2C");

            entity.ToTable("MauXe");

            entity.Property(e => e.TenMau)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A27CE00B7");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Email, "UQ__NhanVien__A9D105342A5C1340").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaChuc__245D67DE");
        });

        modelBuilder.Entity<PhuongTienThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__35B6C8B0F6D03E0C");

            entity.ToTable("PhuongTienThanhToan");

            entity.Property(e => e.TenPhuongTien).HasMaxLength(50);
        });

        modelBuilder.Entity<QuyenHan>(entity =>
        {
            entity.HasKey(e => e.MaQuyenHan).HasName("PK__QuyenHan__3EAF3EE65323B3C6");

            entity.ToTable("QuyenHan");

            entity.Property(e => e.TenQuyenHan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529B72C5934");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC06D1C1BE2").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaLoaiTaiKhoanNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaLoaiTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaLoai__2BFE89A6");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__TaiKhoan__MaNV__2CF2ADDF");
        });

        modelBuilder.Entity<VeGuiXe>(entity =>
        {
            entity.HasKey(e => e.MaVe).HasName("PK__VeGuiXe__2725100FCEF07291");

            entity.ToTable("VeGuiXe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayGui).HasColumnType("datetime");
            entity.Property(e => e.NgayLay).HasColumnType("datetime");

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VeGuiXe__BienSoX__17F790F9");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__VeGuiXe__MaKH__18EBB532");

            entity.HasOne(d => d.MaLoaiVeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaLoaiVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VeGuiXe__MaLoaiV__17036CC0");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.BienSoXe).HasName("PK__Xe__A7805993979033EC");

            entity.ToTable("Xe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HieuXe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LoaiXe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaMauXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaMauXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaMauXe__123EB7A3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
