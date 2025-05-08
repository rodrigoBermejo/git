USE [CRM-DB]
GO

INSERT INTO [dbo].[Users](
	[IdUser]
	,[UserName]
	,[UserDisplayName]
	,[Email]
	,[Role]
	,[Created]
	,[Updated]
	,[CreatedById]
	,[UpdatedById])
VALUES
	('3fa85f64-5717-4562-b3fc-2c963f66afa6',
  'administrator',
  'SA',
  'admin@crm.com',
  1,
  GETUTCDATE(),
  GETUTCDATE(),
  null,
  null)
GO

SELECT * FROM [dbo].[Users]

