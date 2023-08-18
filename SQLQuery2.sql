CREATE TABLE [dbo].[tbThuoc] (
    [MaThuoc]    NVARCHAR (50)  NOT NULL,
    [TenThuoc]   NVARCHAR (50)  NOT NULL,
    [MaThuocA]   NVARCHAR (50)  NOT NULL,
    [SoLuong]    FLOAT (53)     NOT NULL,
    [DonGiaNhap] FLOAT (53)     NOT NULL,
    [DonGiaBan]  FLOAT (53)     NOT NULL,
    [Anh]        NVARCHAR (200) NULL,
    [GhiChu]     NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaThuoc] ASC)
)