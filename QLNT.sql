CREATE TABLE [dbo].[tbChiTietHD] (
    [MaHDBan]   NVARCHAR (30) NOT NULL,
    [MaThuoc]   NVARCHAR (50) NOT NULL,
    [SoLuong]   FLOAT (53)    NOT NULL,
    [DonGia]    FLOAT (53)    NOT NULL,
    [GiamGia]   FLOAT (53)    NOT NULL,
    [ThanhTien] FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([MaThuoc] ASC, [MaHDBan] ASC)
)