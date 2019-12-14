CREATE TABLE [dbo].[EmployeeTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](20) NOT NULL,
	[LastName] [nchar](20) NOT NULL,
	[Age] [nchar](3) NOT NULL,
	[Email] [nchar](25) NOT NULL,
	[EmployeeAddress] [nchar](100) NOT NULL,
 CONSTRAINT [PK_EmployeeTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


