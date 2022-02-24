IF  OBJECT_ID(N'dbo.UpdateChangeLogs', N'U') IS NULL  
BEGIN
	CREATE TABLE [dbo].[UpdateChangeLogs](
		[UpdateListID] [tinyint] IDENTITY(1,1) NOT NULL,
		[HeaderLine] [nvarchar](100) NULL,
		[SubHeaderLine] [nvarchar](100) NULL,
		[Description] [text] NULL,
		[EntryDate] [datetime] NULL,
		[ExpirationDate] [datetime] NULL,
		[IsActive] [bit] NULL,
		[IsAlert] [bit] NULL,
	 CONSTRAINT [PK_UpdateChangeLogs] PRIMARY KEY CLUSTERED 
	(
		[UpdateListID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	ALTER TABLE [dbo].[UpdateChangeLogs] ADD  CONSTRAINT [df_IsActive]  DEFAULT ((0)) FOR [IsActive]
	ALTER TABLE [dbo].[UpdateChangeLogs] ADD  CONSTRAINT [df_IsAlert]  DEFAULT ((0)) FOR [IsAlert]
END


