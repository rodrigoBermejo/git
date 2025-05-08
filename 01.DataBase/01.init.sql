GO

CREATE DATABASE UPA_CRM;

GO

CREATE TABLE [dbo].[Customer]
(
  [CustomerID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](50) NOT NULL,
  [Email] [varchar](50) NOT NULL,
  [Phone] [varchar](50) NOT NULL,
  [Address] [varchar](50) NOT NULL,
  [TAXID] [varchar](50) NOT NULL,
  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED
  (
    [CustomerID] ASC
  )
);

GO

CREATE TABLE [dbo].[Activity]
(
  [ActivityID] [int] IDENTITY(1,1) NOT NULL,
  [CustomerID] [int] NOT NULL,
  [Description] [varchar](50) NOT NULL,
  [Date] [date] NOT NULL,
  CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED
  (
    [ActivityID] ASC
  )
)