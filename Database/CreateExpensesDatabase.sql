USE Master
GO

DROP DATABASE IF EXISTS Expenses
GO

CREATE DATABASE Expenses
GO

USE Expenses
GO


CREATE TABLE Member(
	MemberId INT IDENTITY(1001,1) NOT NULL PRIMARY KEY,
	MemberName NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE Category(
	CategoryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Title NVARCHAR(100) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL DEFAULT('')
)
GO

CREATE TABLE SubCategory(
	SubCategoryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CategoryId INT NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL DEFAULT('')
)
GO

CREATE TABLE Expense(
	ExpenseId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	MemberId INT NOT NULL,
	SubCategoryId INT NOT NULL,
	Title NVARCHAR(200) NOT NULL,
	Amount DECIMAL(18, 2) NOT NULL,
	ExpenseDate DATE NOT NULL DEFAULT(GETDATE()),
	Description NVARCHAR(MAX) NOT NULL DEFAULT(''),
	Vendor NVARCHAR(100) NOT NULL DEFAULT(''),
	PaymentType NVARCHAR(100) NOT NULL DEFAULT(''),
	IsBusinessExpense BIT NOT NULL DEFAULT(1)
)
GO

-- Insert Members
SET IDENTITY_INSERT Member ON
GO

INSERT INTO Member ( MemberId, MemberName ) VALUES(1001, 'Gregor')

SET IDENTITY_INSERT Member OFF
GO
DBCC CHECKIDENT ('dbo.Member', RESEED, 1003);  
GO 

-- Insert Categories
SET IDENTITY_INSERT Category ON
GO

--INSERT INTO Category ( CategoryId, Title ) VALUES(1, 'Office Supplies')
--INSERT INTO Category ( CategoryId, Title ) VALUES(2, 'Travel')
--INSERT INTO Category ( CategoryId, Title ) VALUES(3, 'Miscellaneous')

INSERT INTO Category ( CategoryId, Title ) 
VALUES(1, 'Office Supplies')
	, (2, 'Travel')
	, (3, 'Miscellaneous')

SET IDENTITY_INSERT Category OFF
GO
DBCC CHECKIDENT ('dbo.Category', RESEED, 4);  
GO 

-- Insert SubCategories
SET IDENTITY_INSERT SubCategory ON
GO

INSERT INTO SubCategory ( SubCategoryId, CategoryId, Title ) 
VALUES(1, 1, 'Office Supplies')
	, (2, 1, 'Software')
	, (3, 1, 'Electronics')
	, (4, 2, 'Flight')
	, (5, 2, 'Car')
	, (6, 2, 'Hotel')
	, (7, 2, 'Per Diem')
	, (8, 3, 'Miscellaneous')

SET IDENTITY_INSERT SubCategory OFF
GO
DBCC CHECKIDENT ('dbo.SubCategory', RESEED, 9);  
GO 

-- create foreign keys
ALTER TABLE Expense
ADD CONSTRAINT FK_Expense_SubCategory
FOREIGN KEY (SubCategoryId)
REFERENCES SubCategory (SubCategoryId)

ALTER TABLE Expense
ADD CONSTRAINT FK_Expense_Member
FOREIGN KEY (MemberId)
REFERENCES Member (MemberId)

ALTER TABLE SubCategory
ADD CONSTRAINT FK_SubCategory_Category
FOREIGN KEY (CategoryId)
REFERENCES Category (CategoryId)
