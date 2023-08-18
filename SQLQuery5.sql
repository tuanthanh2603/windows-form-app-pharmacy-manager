CREATE TABLE [dbo].[tbKhach] (
    [MaKhach]   NVARCHAR (10) NOT NULL,
    [TenKhach]  NVARCHAR (50) NOT NULL,
    [DiaChi]    NVARCHAR (50) NOT NULL,
    [DienThoai] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaKhach] ASC)
)