USE
QLYTHUVIEN
go

create table sach (
    ma_sach int identity(1,1) primary key,       
    ten_sach nvarchar(200) not null,             
    tac_gia nvarchar(100) not null,                  
    the_loai nvarchar(100) not null,                 
    so_luong int not null constraint chk_soluong check (so_luong >= 0),                       
    so_luong_con int not null,
	constraint chk_soluongcon check (so_luong_con >= 0 and so_luong_con <= so_luong),
	CONSTRAINT uq_ten_sach_tac_gia UNIQUE (ten_sach, tac_gia)
)
go

create table student (
    ma_sinh_vien int identity(1,1) primary key, 
	hoTen nvarchar(100) not null,
    so_the_dang_ki varchar(20) not null unique,                    
    Lop nvarchar(50) not null,                         
    soDienThoai varchar(20) not null,                          
    diaChi nvarchar(200) not null,
	email nvarchar(200) not null 
)

select *  from sach
select * from student
go
create table phieu_muon (
    ma_phieu int identity(1,1) primary key,
    ma_sinh_vien int not null foreign key references student(ma_sinh_vien),
    ma_sach int not null foreign key references sach(ma_sach),
    ngay_muon date not null,
    han_tra date not null,
    trang_thai nvarchar(50) not null,
	constraint chk_ngaytra_ngaymuon check (han_tra >= ngay_muon)
)
select hoTen, so_the_dang_ki, soDienThoai, diaChi, email 
from student 
where so_the_dang_ki = 
go
select ma_sinh_vien from student where so_the_dang_ki
go
insert into phieu_muon(ma_sinh_vien, ma_sach, ngay_muon, han_tra, trang_thai)
values()
go
select * from phieu_muon



