CREATE TABLE [dbo].[Table]
(
	[MaHDBan] NVARCHAR(30) NOT NULL , 
    [MaThuoc] NVARCHAR(50) NOT NULL, 
    [SoLuong] FLOAT NOT NULL, 
    [DonGia] FLOAT NOT NULL, 
    [GiamGia] FLOAT NOT NULL, 
    [ThanhTien] FLOAT NOT NULL, 
    PRIMARY KEY ([MaThuoc], [MaHDBan])
)
