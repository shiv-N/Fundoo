CREATE TABLE [dbo].[Label](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LabelName] [varchar](50) NULL,
	[UserId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Label]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[FundooApp] ([Id])
GO


