	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='CPRevoriaUsers' and TABLE_NAME='TempRevoriaUsersProject')
	BEGIN
				CREATE TABLE [CPRevoriaUsers].[TempRevoriaUsersProject](
				[RevoriaUsersProjectId] [int] IDENTITY(1,1) NOT NULL,
				[ProjectName] [nvarchar](max) NULL,
				[UserId] [int] NULL,
				[ProjectGuid] [uniqueidentifier] NULL,
				[DeviceId] [int] NULL,
				[Media] [nvarchar](max) NULL,
				[Colorant] [nvarchar](max) NULL,
				[RipSettings] [nvarchar](max) NULL,
				[Notes] [nvarchar](max) NULL,
				[PrintQId] [nvarchar](max) NULL,
				[ServicePostUrl] [nvarchar](max) NULL,
				[CreatedDate] [datetime] NULL,
				[ModifiedDate] [datetime] NULL,
			 CONSTRAINT [PK_TempRevoriaUsersProject] PRIMARY KEY CLUSTERED 
			(
				[RevoriaUsersProjectId] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			ALTER TABLE [CPRevoriaUsers].[TempRevoriaUsersProject] ADD  CONSTRAINT [DF_TempRevoriaUsersProject_CreatedDate]  DEFAULT (GETDATE()) FOR [CreatedDate]
	END