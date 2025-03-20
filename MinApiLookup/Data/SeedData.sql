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
