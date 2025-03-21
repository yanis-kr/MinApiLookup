IF OBJECT_ID('Products', 'U') IS NOT NULL
    DROP TABLE Products;

CREATE TABLE Products
(
	Id INT PRIMARY KEY IDENTITY(1,1),
    Code_L NVARCHAR(50),
    Code_T NVARCHAR(50),
    Category NVARCHAR(50),
    AccountType NVARCHAR(50)
);

--GO --uncomment if running from SSMS

INSERT INTO dbo.Products (Code_L, Code_T, Category, AccountType) VALUES 
('A1', 'B1', 'CategoryA', 'TypeX'),
('A2', 'B2', 'CategoryB', 'TypeY');
--GO
--------------------------------------------------------------------------------

-- Countries table
IF OBJECT_ID('Countries', 'U') IS NOT NULL DROP TABLE Countries;

CREATE TABLE Countries (
    Code NVARCHAR(10) PRIMARY KEY,
    [Name] NVARCHAR(100)
);
--GO

INSERT INTO Countries (Code, [Name]) VALUES
('us', 'United States'),
('ca', 'Canada'),
('gb', 'Great Britain');

--GO
IF OBJECT_ID('ProductAccounts', 'U') IS NOT NULL DROP TABLE ProductAccounts;

CREATE TABLE ProductAccounts
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code_L NVARCHAR(50) NULL,
    Code_T NVARCHAR(50) NULL,
    AccountNumber NVARCHAR(50) NULL,
    CreatedAt DATETIME2 DEFAULT(GETDATE())
);

INSERT INTO ProductAccounts (Code_L, Code_T, AccountNumber) VALUES
('CL001', 'CT001', 'AC123456'),
('CL001', 'CT002', 'AC234567'),
('CL002', 'CT001', 'AC345678'),
('CL002', 'CT002', 'AC456789'),
('CL003', 'CT003', 'AC567890'),
('CL004', 'CT004', 'AC678901'),
('CL005', 'CT005', 'AC789012'),
('CL006', 'CT006', 'AC890123'),
('CL007', 'CT007', 'AC901234'),
('CL008', 'CT008', 'AC012345');
('CL001', 'CT002', 'AC234567'),
('CL002', 'CT001', 'AC345678'),
('CL002', 'CT002', 'AC456789'),
('CL003', 'CT003', 'AC567890'),
('CL004', 'CT004', 'AC678901'),
('CL005', 'CT005', 'AC789012'),
('CL006', 'CT006', 'AC890123'),
('CL007', 'CT007', 'AC901234'),
('CL008', 'CT008', 'AC012345');
