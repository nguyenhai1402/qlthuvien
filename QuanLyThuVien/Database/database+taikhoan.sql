CREATE DATABASE QLYTHUVIEN
USE
QLYTHUVIEN

create table TaiKhoan(
    id int NOT NULL IDENTITY(1,1) primary key,
    taikhoan varchar(150) not null,
    matkhau varchar(150) not null
);

insert into TaiKhoan values('hainek','123456');
insert into TaiKhoan values('hieunek','123456');
select * from TaiKhoan