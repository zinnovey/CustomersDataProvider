CREATE DATABASE Customers

USE Customers

CREATE TABLE [dbo].[Customers] (
    [CustomerID]           INT           IDENTITY (1, 1) NOT NULL,
    [CustomerName]         NVARCHAR (30) NOT NULL,
    [CustomerContactEmail] NVARCHAR (25) NOT NULL,
    [CustomerMobileNumber] NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

CREATE TABLE [dbo].[Transactions] (
    [TransactionID]       INT         IDENTITY (1, 1) NOT NULL,
    [CustomerID]          INT         NOT NULL,
    [TransactionDateTime] DATETIME    NOT NULL,
    [TransactionAmount]   DECIMAL (38, 2) NOT NULL,
    [TransactionCurrency] INT         NOT NULL,
    [TransactionStatus]   INT         NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [FK_Customers_Transactions] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);


