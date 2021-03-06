USE [gmblDB]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accounts](
	[accountid] [uniqueidentifier] NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[country] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[mail] [nvarchar](10) NOT NULL,
	[rating] [int] NULL,
	[countpost] [int] NULL,
	[createdtime] [datetime] NOT NULL,
	[photo] [varbinary](max) NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[accountid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccountsRoles]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountsRoles](
	[Id] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[AccountId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[comments]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comments](
	[comid] [uniqueidentifier] NOT NULL,
	[text] [nvarchar](250) NOT NULL,
	[createdtime] [datetime2](7) NOT NULL,
	[accountid] [uniqueidentifier] NOT NULL,
	[postid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_comments] PRIMARY KEY CLUSTERED 
(
	[comid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[foto]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[foto](
	[fotoid] [uniqueidentifier] NOT NULL,
	[foto_file] [varbinary](max) NOT NULL,
	[foto_type] [nvarchar](10) NOT NULL,
	[postid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_foto] PRIMARY KEY CLUSTERED 
(
	[fotoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fotos]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fotos](
	[fotoid] [int] IDENTITY(1,1) NOT NULL,
	[foto_file] [varbinary](max) NOT NULL,
	[foto_type] [nvarchar](10) NOT NULL,
	[postid] [int] NOT NULL,
 CONSTRAINT [PK_foto_1] PRIMARY KEY CLUSTERED 
(
	[fotoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[post]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post](
	[postid] [uniqueidentifier] NOT NULL,
	[postname] [nvarchar](50) NOT NULL,
	[descp] [nvarchar](250) NULL,
	[createdtime] [datetime2](7) NOT NULL,
	[accountid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_alboms] PRIMARY KEY CLUSTERED 
(
	[postid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[posts]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts](
	[postid] [int] NOT NULL,
	[postname] [nvarchar](50) NOT NULL,
	[source] [nvarchar](400) NULL,
	[createdtime] [datetime2](7) NOT NULL,
	[accountid] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[text] [nvarchar](500) NULL,
 CONSTRAINT [PK_post] PRIMARY KEY CLUSTERED 
(
	[postid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rel_tag]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rel_tag](
	[postid] [uniqueidentifier] NULL,
	[tagid] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tagposts]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tagposts](
	[postid] [uniqueidentifier] NOT NULL,
	[tag] [nvarchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tags]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tags](
	[tagid] [uniqueidentifier] NOT NULL,
	[text] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_tags] PRIMARY KEY CLUSTERED 
(
	[tagid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 18.03.2016 23:40:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[Id] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_accounts] FOREIGN KEY([accountid])
REFERENCES [dbo].[accounts] ([accountid])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_accounts]
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_alboms] FOREIGN KEY([postid])
REFERENCES [dbo].[post] ([postid])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_alboms]
GO
ALTER TABLE [dbo].[foto]  WITH CHECK ADD  CONSTRAINT [FK_foto_post] FOREIGN KEY([postid])
REFERENCES [dbo].[post] ([postid])
GO
ALTER TABLE [dbo].[foto] CHECK CONSTRAINT [FK_foto_post]
GO
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [FK_alboms_accounts] FOREIGN KEY([accountid])
REFERENCES [dbo].[accounts] ([accountid])
GO
ALTER TABLE [dbo].[post] CHECK CONSTRAINT [FK_alboms_accounts]
GO
ALTER TABLE [dbo].[rel_tag]  WITH CHECK ADD  CONSTRAINT [FK_rel_tag_alboms] FOREIGN KEY([postid])
REFERENCES [dbo].[post] ([postid])
GO
ALTER TABLE [dbo].[rel_tag] CHECK CONSTRAINT [FK_rel_tag_alboms]
GO
ALTER TABLE [dbo].[rel_tag]  WITH CHECK ADD  CONSTRAINT [FK_rel_tag_tags] FOREIGN KEY([tagid])
REFERENCES [dbo].[tags] ([tagid])
GO
ALTER TABLE [dbo].[rel_tag] CHECK CONSTRAINT [FK_rel_tag_tags]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
