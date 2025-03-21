-- Drop tables if exist (SQLite syntax)
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Countries;
DROP TABLE IF EXISTS ProductAccounts;

-- Products table
CREATE TABLE Products
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code_L TEXT,
    Code_T TEXT,
    Category TEXT,
    AccountType TEXT
);

INSERT INTO Products (Code_L, Code_T, Category, AccountType) VALUES 
('A1', 'B1', 'CategoryA', 'TypeX'),
('A2', 'B2', 'CategoryB', 'TypeY');

-- Countries table
CREATE TABLE Countries (
    Code TEXT PRIMARY KEY,
    Name TEXT
);

INSERT INTO Countries (Code, Name) VALUES
('us', 'United States'),
('ca', 'Canada'),
('gb', 'Great Britain');

-- ProductAccounts table
CREATE TABLE ProductAccounts
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code_L TEXT,
    Code_T TEXT,
    AccountNumber TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
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
('CL008', 'CT008', 'AC012345'),
('CL001', 'CT002', 'AC234567'),
('CL002', 'CT001', 'AC345678'),
('CL002', 'CT002', 'AC456789'),
('CL003', 'CT003', 'AC567890'),
('CL004', 'CT004', 'AC678901'),
('CL005', 'CT005', 'AC789012'),
('CL006', 'CT006', 'AC890123'),
('CL007', 'CT007', 'AC901234'),
('CL008', 'CT008', 'AC012345');
