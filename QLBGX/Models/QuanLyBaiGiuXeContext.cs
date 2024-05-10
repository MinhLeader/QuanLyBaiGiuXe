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
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-1M5E6QBD\\MINHCUTE;Initial Catalog=QuanLyBaiGiuXe;Persist Security Info=True;User ID=sa;Password=28062003;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChoDoXe>(entity =>
        {
            entity.HasKey(e => e.MaChoDoXe).HasName("PK__ChoDoXe__0F345651D7459E83");

            entity.ToTable("ChoDoXe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LoaiXe).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__BienSoX__498EEC8D");

            entity.HasOne(d => d.MaKhuVucNavigation).WithMany(p => p.ChoDoXes)
                .HasForeignKey(d => d.MaKhuVuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoDoXe__MaKhuVu__489AC854");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__D4639533CCC63C28");

            entity.ToTable("ChucVu");

            entity.Property(e => e.TenChucVu).HasMaxLength(50);
        });

        modelBuilder.Entity<GiaoDichThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDichThanhToan).HasName("PK__GiaoDich__9ECF47B86E3198B5");

            entity.ToTable("GiaoDichThanhToan");

            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGiaoDichNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaGiaoDich)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaGia__5F7E2DAC");

            entity.HasOne(d => d.MaPhuongTienNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaPhuongTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaPhu__607251E5");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E5C104B32");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D105349BF444EA").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenKh)
                .HasMaxLength(255)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<KhuVuc>(entity =>
        {
            entity.HasKey(e => e.MaKhuVuc).HasName("PK__KhuVuc__0676EB83453132BE");

            entity.ToTable("KhuVuc");

            entity.Property(e => e.TenKhuVuc).HasMaxLength(50);
        });

        modelBuilder.Entity<LichSuGiaoDich>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__LichSuGi__0A2A24EBBF00CA26");

            entity.ToTable("LichSuGiaoDich");

            entity.Property(e => e.ThoiGianGiaoDich).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaVeNavigation).WithMany(p => p.LichSuGiaoDiches)
                .HasForeignKey(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuGiao__MaVe__5AB9788F");
        });

        modelBuilder.Entity<LoaiTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTaiKhoan).HasName("PK__LoaiTaiK__5F6E141CAEE40F72");

            entity.ToTable("LoaiTaiKhoan");

            entity.Property(e => e.TenLoaiTaiKhoan).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiVe>(entity =>
        {
            entity.HasKey(e => e.MaLoaiVe).HasName("PK__LoaiVe__1224E2F761DD8E12");

            entity.ToTable("LoaiVe");

            entity.Property(e => e.GiaVe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenLoaiVe).HasMaxLength(50);
        });

        modelBuilder.Entity<MauXe>(entity =>
        {
            entity.HasKey(e => e.MaMauXe).HasName("PK__MauXe__487AF6FD0F87F3AA");

            entity.ToTable("MauXe");

            entity.Property(e => e.TenMau).HasMaxLength(50);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A1193A776");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Email, "UQ__NhanVien__A9D10534E1AE6A3B").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNv)
                .HasMaxLength(255)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaChuc__4F47C5E3");
        });

        modelBuilder.Entity<PhuongTienThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__35B6C8B0F9F08AA1");

            entity.ToTable("PhuongTienThanhToan");

            entity.Property(e => e.TenPhuongTien).HasMaxLength(50);
        });

        modelBuilder.Entity<QuyenHan>(entity =>
        {
            entity.HasKey(e => e.MaQuyenHan).HasName("PK__QuyenHan__3EAF3EE6E0C6A6A1");

            entity.ToTable("QuyenHan");

            entity.Property(e => e.TenQuyenHan).HasMaxLength(50);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C65291CA3A687");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC05D20D32F").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiTaiKhoanNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaLoaiTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaLoai__56E8E7AB");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__TaiKhoan__MaNV__57DD0BE4");
        });

        modelBuilder.Entity<VeGuiXe>(entity =>
        {
            entity.HasKey(e => e.MaVe).HasName("PK__VeGuiXe__2725100F377AE5FA");

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
                .HasConstraintName("FK__VeGuiXe__BienSoX__42E1EEFE");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__VeGuiXe__MaKH__43D61337");

            entity.HasOne(d => d.MaLoaiVeNavigation).WithMany(p => p.VeGuiXes)
                .HasForeignKey(d => d.MaLoaiVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VeGuiXe__MaLoaiV__41EDCAC5");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.BienSoXe).HasName("PK__Xe__A78059936A2D6A44");

            entity.ToTable("Xe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HieuXe).HasMaxLength(50);
            entity.Property(e => e.LoaiXe).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);

            entity.HasOne(d => d.MaMauXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaMauXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaMauXe__3D2915A8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
