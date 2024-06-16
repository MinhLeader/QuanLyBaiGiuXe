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

    public virtual DbSet<TrangThaiChoDoXe> TrangThaiChoDoXes { get; set; }

    public virtual DbSet<VeGuiXe> VeGuiXes { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-1M5E6QBD\\MINHCUTE;Initial Catalog=QuanLyBaiGiuXe;Persist Security Info=True;User ID=sa;Password=28062003;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChoDoXe>(entity =>
        {
            entity.HasKey(e => e.MaChoDoXe).HasName("PK__ChoDoXe__0F345651A0A9BFBD");

            entity.ToTable("ChoDoXe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.BienSoXe)
                .HasConstraintName("FK__ChoDoXe__BienSoX__6CA31EA0");

            entity.HasOne(d => d.MaKhuVucNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.MaKhuVuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__MaKhuVu__6BAEFA67");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__MaTrang__6D9742D9");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__D4639533E76C692B");

            entity.ToTable("ChucVu");

            entity.Property(e => e.TenChucVu).HasMaxLength(50);
        });

        modelBuilder.Entity<GiaoDichThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDichThanhToan).HasName("PK__GiaoDich__9ECF47B8B31307E5");

            entity.ToTable("GiaoDichThanhToan");

            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGiaoDichNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaGiaoDich)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaGia__047AA831");

            entity.HasOne(d => d.MaPhuongTienNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaPhuongTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaPhu__056ECC6A");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1EF00AFAC8");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D105347CEBA275").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.TenKh)
                .HasMaxLength(255)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<KhuVuc>(entity =>
        {
            entity.HasKey(e => e.MaKhuVuc).HasName("PK__KhuVuc__0676EB83AAC7804F");

            entity.ToTable("KhuVuc");

            entity.Property(e => e.TenKhuVuc).HasMaxLength(50);
        });

        modelBuilder.Entity<LichSuGiaoDich>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__LichSuGi__0A2A24EBBA2FAFBB");

            entity.ToTable("LichSuGiaoDich");

            entity.Property(e => e.ThoiGianGiaoDich).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaVeNavigation).WithMany(p => p.LichSuGiaoDiches)
                .HasForeignKey(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuGiao__MaVe__7FB5F314");
        });

        modelBuilder.Entity<LoaiTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTaiKhoan).HasName("PK__LoaiTaiK__5F6E141CD89F3541");

            entity.ToTable("LoaiTaiKhoan");

            entity.Property(e => e.TenLoaiTaiKhoan).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiVe>(entity =>
        {
            entity.HasKey(e => e.MaLoaiVe).HasName("PK__LoaiVe__1224E2F7B0B1ED6E");

            entity.ToTable("LoaiVe");

            entity.Property(e => e.GiaVe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaVeGio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenLoaiVe).HasMaxLength(50);
        });

        modelBuilder.Entity<MauXe>(entity =>
        {
            entity.HasKey(e => e.MaMauXe).HasName("PK__MauXe__487AF6FD91E97138");

            entity.ToTable("MauXe");

            entity.Property(e => e.TenMau).HasMaxLength(50);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A27AAE822");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Email, "UQ__NhanVien__A9D105346D7843F6").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.TenNv)
                .HasMaxLength(255)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaChuc__73501C2F");
        });

        modelBuilder.Entity<PhuongTienThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__35B6C8B02AA6432B");

            entity.ToTable("PhuongTienThanhToan");

            entity.Property(e => e.TenPhuongTien).HasMaxLength(50);
        });

        modelBuilder.Entity<QuyenHan>(entity =>
        {
            entity.HasKey(e => e.MaQuyenHan).HasName("PK__QuyenHan__3EAF3EE6726C41AB");

            entity.ToTable("QuyenHan");

            entity.Property(e => e.TenQuyenHan).HasMaxLength(50);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C65293E24FBE6");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC0C75980B4").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__TaiKhoan__MaKH__7BE56230");

            entity.HasOne(d => d.MaLoaiTaiKhoanNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaLoaiTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaLoai__7AF13DF7");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__TaiKhoan__MaNV__7CD98669");
        });

        modelBuilder.Entity<TrangThaiChoDoXe>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__AADE41380A1F05B6");

            entity.ToTable("TrangThaiChoDoXe");

            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<VeGuiXe>(entity =>
        {
            entity.HasKey(e => e.MaVe).HasName("PK__VeGuiXe__2725100F9A169B8D");

            entity.ToTable("VeGuiXe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayGui).HasColumnType("datetime");
            entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
            entity.Property(e => e.NgayLay).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VeGuiXe__BienSoX__6319B466");

            entity.HasOne(d => d.MaChoDoXeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaChoDoXe)
                .HasConstraintName("FK_VeGuiXe_ChoDoXe");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__VeGuiXe__MaKH__640DD89F");

            entity.HasOne(d => d.MaLoaiVeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaLoaiVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VeGuiXe__MaLoaiV__6225902D");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.BienSoXe).HasName("PK__Xe__A7805993793411F6");

            entity.ToTable("Xe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HieuXe).HasMaxLength(50);
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.LoaiXe).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);

            entity.HasOne(d => d.MaMauXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaMauXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaMauXe__5D60DB10");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
