USE [GMBD]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 13.12.2015 18:34:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[accountid] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[country] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[mail] [nvarchar](10) NOT NULL,
	[rating] [int] NOT NULL,
	[countpost] [int] NOT NULL,
	[createdtime] [datetime] NOT NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[accountid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[comments]    Script Date: 13.12.2015 18:34:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comments](
	[comid] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](250) NOT NULL,
	[createdtime] [datetime2](7) NOT NULL,
	[accountid] [int] NOT NULL,
	[postid] [int] NOT NULL,
 CONSTRAINT [PK_comments_1] PRIMARY KEY CLUSTERED 
(
	[comid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[fotos]    Script Date: 13.12.2015 18:34:27 ******/
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
/****** Object:  Table [dbo].[posts]    Script Date: 13.12.2015 18:34:27 ******/
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
 CONSTRAINT [PK_post] PRIMARY KEY CLUSTERED 
(
	[postid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tagposts]    Script Date: 13.12.2015 18:34:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tagposts](
	[postid] [int] NOT NULL,
	[tagid] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tags]    Script Date: 13.12.2015 18:34:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tags](
	[tagid] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_tags_1] PRIMARY KEY CLUSTERED 
(
	[tagid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_accounts] FOREIGN KEY([accountid])
REFERENCES [dbo].[accounts] ([accountid])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_accounts]
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_post] FOREIGN KEY([postid])
REFERENCES [dbo].[posts] ([postid])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_post]
GO
ALTER TABLE [dbo].[fotos]  WITH CHECK ADD  CONSTRAINT [FK_foto_post] FOREIGN KEY([postid])
REFERENCES [dbo].[posts] ([postid])
GO
ALTER TABLE [dbo].[fotos] CHECK CONSTRAINT [FK_foto_post]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK_post_accounts] FOREIGN KEY([accountid])
REFERENCES [dbo].[accounts] ([accountid])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK_post_accounts]
GO
ALTER TABLE [dbo].[tagposts]  WITH CHECK ADD  CONSTRAINT [FK_rel_tag_post] FOREIGN KEY([postid])
REFERENCES [dbo].[posts] ([postid])
GO
ALTER TABLE [dbo].[tagposts] CHECK CONSTRAINT [FK_rel_tag_post]
GO
ALTER TABLE [dbo].[tagposts]  WITH CHECK ADD  CONSTRAINT [FK_rel_tag_tags] FOREIGN KEY([tagid])
REFERENCES [dbo].[tags] ([tagid])
GO
ALTER TABLE [dbo].[tagposts] CHECK CONSTRAINT [FK_rel_tag_tags]
GO
