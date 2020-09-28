/*  
Create PoisnFangTodo table
*/

CREATE TABLE [dbo].[PoisnFangTodo](
	[TodoId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
  CONSTRAINT [PK_PoisnFangTodo] PRIMARY KEY CLUSTERED 
  (
	[TodoId] ASC
  )
)
GO

/*  
Create foreign key relationships
*/
ALTER TABLE [dbo].[PoisnFangTodo]  WITH CHECK ADD  CONSTRAINT [FK_PoisnFangTodo_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].Module ([ModuleId])
ON DELETE CASCADE
GO