CREATE TABLE [dbo].[FundooNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[MeassageDescription] [varchar](1000) NOT NULL,
	[NoteImage] [varchar](100) NULL,
	[Color] [varchar](20) NULL,
	[CreatedDATETime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[AddReminder] [datetime] NULL,
	[UserId] [int] NOT NULL,
	[IsPin] [bit] NOT NULL,
	[IsNote] [bit] NOT NULL,
	[IsArchive] [bit] NOT NULL,
	[IsTrash] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FundooNotes]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[FundooApp] ([Id])
GO


