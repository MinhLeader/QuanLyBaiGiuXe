-- Bảng Khách hàng
CREATE TABLE KhachHang (
  MaKH INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenKH NVARCHAR(255) NOT NULL,
  SoDienThoai CHAR(12) NOT NULL,
  DiaChi NVARCHAR(255) NOT NULL,
  Email VARCHAR(255) UNIQUE
);

-- Bảng Màu xe
CREATE TABLE MauXe (
  MaMauXe INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenMau NVARCHAR(50) NOT NULL
);

-- Bảng Xe
CREATE TABLE Xe (
  BienSoXe VARCHAR(20) PRIMARY KEY,
  MaMauXe INT NOT NULL,
  HieuXe NVARCHAR(50) NOT NULL,
  Model NVARCHAR(50) NOT NULL,
  LoaiXe NVARCHAR(50) NOT NULL,
  FOREIGN KEY (MaMauXe) REFERENCES MauXe(MaMauXe)
);



CREATE TABLE LoaiVe (
  MaLoaiVe INT PRIMARY KEY IDENTITY(1, 1),
  TenLoaiVe NVARCHAR(50) NOT NULL,
  GiaVe DECIMAL(18, 2) NOT NULL
);


-- Bảng Vé gửi xe
CREATE TABLE VeGuiXe (
  MaVe INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  MaLoaiVe INT NOT NULL,
  BienSoXe VARCHAR(20) NOT NULL,
  NgayGui DATETIME NOT NULL,
  NgayLay DATETIME,
  MaKH INT,
  FOREIGN KEY (MaLoaiVe) REFERENCES LoaiVe(MaLoaiVe),
  FOREIGN KEY (BienSoXe) REFERENCES Xe(BienSoXe),
  FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- Bảng Khu vực
CREATE TABLE KhuVuc (
  MaKhuVuc INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenKhuVuc NVARCHAR(50) NOT NULL,
  SucChua INT NOT NULL
);

-- Bảng Chỗ đỗ xe
CREATE TABLE ChoDoXe (
  MaChoDoXe INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  MaKhuVuc INT NOT NULL,
  BienSoXe VARCHAR(20) NOT NULL,
  LoaiXe NVARCHAR(50) NOT NULL,
  TrangThai NVARCHAR(50) NOT NULL,
  FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc),
  FOREIGN KEY (BienSoXe) REFERENCES Xe(BienSoXe),
  --FOREIGN KEY (LoaiXe) REFERENCES LoaiXe(TenLoaiXe)  -- Assuming TenLoaiXe is the primary key in LoaiXe
);



-- Bảng Chức vụ
CREATE TABLE ChucVu (
  MaChucVu INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenChucVu NVARCHAR(50) NOT NULL
);

-- Bảng Nhân viên
CREATE TABLE NhanVien (
  MaNV INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenNV NVARCHAR(255) NOT NULL,
  SoDienThoai CHAR(12) NOT NULL,
  DiaChi NVARCHAR(255) NOT NULL,
  Email VARCHAR(255) UNIQUE,
  MaChucVu INT NOT NULL,
  FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);



-- Bảng Quyền hạn
CREATE TABLE QuyenHan (
  MaQuyenHan INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenQuyenHan NVARCHAR(50) NOT NULL
);
-- Bảng Loại tài khoản
CREATE TABLE LoaiTaiKhoan (
  MaLoaiTaiKhoan INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenLoaiTaiKhoan NVARCHAR(50) NOT NULL
  );
-- Bảng Tài khoản
CREATE TABLE TaiKhoan (
  MaTaiKhoan INT PRIMARY KEY IDENTITY(1, 1),  -- Use IDENTITY instead of AUTO_INCREMENT
  TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
  MatKhau NVARCHAR(255) NOT NULL,
  MaLoaiTaiKhoan INT NOT NULL,
  MaNV INT,
  FOREIGN KEY (MaLoaiTaiKhoan) REFERENCES LoaiTaiKhoan(MaLoaiTaiKhoan),
  FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);



-- Bảng Lịch sử giao dịch
CREATE TABLE LichSuGiaoDich (
    MaGiaoDich INT PRIMARY KEY IDENTITY(1,1),
    ThoiGianGiaoDich DATETIME NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL,
    MaVe INT NOT NULL,
    FOREIGN KEY (MaVe) REFERENCES VeGuiXe(MaVe)
);
CREATE TABLE PhuongTienThanhToan (
  MaPhuongTien INT PRIMARY KEY IDENTITY(1, 1),
  TenPhuongTien NVARCHAR(50) NOT NULL
);
CREATE TABLE GiaoDichThanhToan (
  MaGiaoDichThanhToan INT PRIMARY KEY IDENTITY(1, 1),
  MaGiaoDich INT NOT NULL,
  MaPhuongTien INT NOT NULL,
  SoTien DECIMAL(18, 2) NOT NULL,
  FOREIGN KEY (MaGiaoDich) REFERENCES LichSuGiaoDich(MaGiaoDich),
  FOREIGN KEY (MaPhuongTien) REFERENCES PhuongTienThanhToan(MaPhuongTien)
);


INSERT INTO KhachHang (TenKH, SoDienThoai, DiaChi, Email)
VALUES (N'Nguyễn Văn A', '0123456789', N'123 Đường ABC', 'nguyenvana@example.com'),
       (N'Trần Thị B', '0987654321', N'456 Đường XYZ', 'tranthib@example.com');

INSERT INTO MauXe (TenMau)
VALUES (N'Đen'), (N'Trắng'), (N'Đỏ');

INSERT INTO Xe (BienSoXe, MaMauXe, HieuXe, Model, LoaiXe)
VALUES ('29A-12345', 1, N'Honda', N'City', N'Sedan'),
       ('51C-98765', 2, N'Toyota', N'Vios', N'Sedan');

INSERT INTO LoaiVe (TenLoaiVe, GiaVe)
VALUES (N'Vé ngày', 50000.00),
       (N'Vé tháng', 1000000.00);

INSERT INTO VeGuiXe (MaLoaiVe, BienSoXe, NgayGui, NgayLay, MaKH)
VALUES (1, '29A-12345', '2024-05-09 08:00:00', NULL, 1),
       (2, '51C-98765', '2024-05-09 10:00:00', NULL, 2);

INSERT INTO KhuVuc (TenKhuVuc, SucChua)
VALUES (N'Khu vực A', 100),
       (N'Khu vực B', 50);

INSERT INTO ChoDoXe (MaKhuVuc, BienSoXe, LoaiXe, TrangThai)
VALUES (1, '29A-12345', N'Sedan', N'Trống'),
       (2, '51C-98765', N'Sedan', N'Đã đỗ');

INSERT INTO ChucVu (TenChucVu)
VALUES (N'Nhân viên bán vé'),
       (N'Quản lý'),
       (N'Nhân viên gửi xe');

INSERT INTO NhanVien (TenNV, SoDienThoai, DiaChi, Email, MaChucVu)
VALUES (N'Nguyễn Văn C', '0369876543', N'789 Đường MNP', 'nguyenvanc@example.com', 1),
       (N'Trần Văn D', '0901234567', N'246 Đường XYZ', 'tranvand@example.com', 3);

INSERT INTO QuyenHan (TenQuyenHan)
VALUES (N'Quản lý nhân viên'),
       (N'Quản lý vé gửi xe'),
       (N'Quản lý doanh thu');

INSERT INTO LoaiTaiKhoan (TenLoaiTaiKhoan)
VALUES (N'Nhân viên'),
       (N'Quản lý');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaLoaiTaiKhoan, MaNV)
VALUES (N'Admin', '12345', 1, 1),
       (N'NhanVien', '678910', 2, 2);

INSERT INTO LichSuGiaoDich (ThoiGianGiaoDich, TongTien, MaVe)
VALUES ('2024-05-09 08:00:00', 50000.00, 1),
       ('2024-05-09 10:00:00', 1000000.00, 2);

INSERT INTO PhuongTienThanhToan (TenPhuongTien)
VALUES (N'Tiền mặt'),
       (N'Thẻ tín dụng'),
       (N'Chuyển khoản');

INSERT INTO GiaoDichThanhToan (MaGiaoDich, MaPhuongTien, SoTien)
VALUES (1, 1, 50000.00),
       (2, 2, 1000000.00);

