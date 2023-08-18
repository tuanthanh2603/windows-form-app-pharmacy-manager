CREATE TABLE [dbo].[tbNhanVien] (
    [MaDuocSy]  NVARCHAR (50) NOT NULL,
    [TenDuocSy] NVARCHAR (50) NOT NULL,
    [GioiTinh]  NVARCHAR (10) NOT NULL,
    [DiaChi]    NVARCHAR (50) NOT NULL,
    [DienThoai] NVARCHAR (15) NOT NULL,
    [NgaySinh]  DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([MaDuocSy] ASC)
)