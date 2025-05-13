IF DB_ID(N'computerstoredb') IS NULL
BEGIN
    CREATE DATABASE computerstoredb;
END
GO
USE computerstoredb;
GO
DROP TABLE IF EXISTS dbo.Products;
DROP TABLE IF EXISTS dbo.Categories;
GO
CREATE TABLE dbo.Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);
GO
CREATE TABLE dbo.Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES dbo.Categories(Id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT dbo.Categories ON;
INSERT INTO dbo.Categories (Id, Name, Description) VALUES
 (1,'CPU','Processors'),
 (2,'GPU','Graphics cards'),
 (3,'Motherboard','Mainboards'),
 (4,'RAM','Memory'),
 (5,'Storage','SSDs and HDDs'),
 (6,'PSU','Power supplies'),
 (7,'Case','PC enclosures'),
 (8,'Monitor','Displays'),
 (9,'Keyboard','Keyboards'),
 (10,'Mouse','Mice');
SET IDENTITY_INSERT dbo.Categories OFF;
GO
INSERT INTO dbo.Products (Name, Description, Price, Quantity, CategoryId) VALUES
 ('Intel Core i7-14700K','20-core CPU',409.99,20,1),
 ('AMD Ryzen 7 7800X3D','8-core CPU',399.99,18,1),
 ('NVIDIA RTX 4080','16GB GPU',999.99,8,2),
 ('AMD Radeon RX 7800 XT','16GB GPU',499.99,12,2),
 ('ASUS ROG Strix Z790-E','Z790 ATX',389.99,15,3),
 ('MSI MAG B650 Tomahawk','B650 ATX',219.99,22,3),
 ('Corsair Vengeance DDR5-6000 32GB','2x16GB CL36',139.99,40,4),
 ('G.Skill Trident Z5 DDR5-6400 32GB','2x16GB CL32',179.99,30,4),
 ('Samsung 990 PRO 2TB','PCIe 4.0 NVMe',189.99,25,5),
 ('Seagate Barracuda 4TB','7200RPM HDD',89.99,60,5),
 ('Corsair RM850x','850W Gold PSU',149.99,18,6),
 ('Seasonic Focus GX-750','750W Gold PSU',129.99,24,6),
 ('NZXT H7 Flow','ATX Mid Tower',129.99,14,7),
 ('Lian Li O11 Dynamic','Dual-chamber case',169.99,10,7),
 ('Dell G2724D','27in 165Hz QHD',269.99,12,8),
 ('LG UltraGear 32GQ950','32in 4K 160Hz',999.99,4,8),
 ('Keychron Q1 Pro','Wireless keyboard',199.99,25,9),
 ('Logitech MX Keys S','Productivity keyboard',109.99,30,9),
 ('Logitech G Pro X Superlight 2','Wireless gaming mouse',159.99,28,10),
 ('Razer Viper V3 Pro','Lightweight mouse',149.99,26,10);
GO
