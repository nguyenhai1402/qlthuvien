CREATE DATABASE QLYTHUVIEN
USE
QLYTHUVIEN

CREATE TABLE TaiKhoan (
    MaNguoiDung VARCHAR(20) PRIMARY KEY,     
    TaiKhoan VARCHAR(150) NOT NULL UNIQUE,   -- username
    MatKhau VARCHAR(150) NOT NULL,           -- password

    HoTen NVARCHAR(150) NOT NULL,
    SoDienThoai VARCHAR(10),
    DiaChi NVARCHAR(200),

    Quyen INT NOT NULL CHECK (QUYEN IN (0,1))-- 0 = admin, 1 = nhanvien
);

drop table TaiKhoan
select * from TaiKhoan