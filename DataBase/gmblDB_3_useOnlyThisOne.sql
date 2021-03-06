USE [gimblDB]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 05.04.2016 22:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accounts](
	[accountid] [uniqueidentifier] NOT NULL,
	[login] [nvarchar](500) NOT NULL,
	[password] [nvarchar](500) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[country] [nvarchar](500) NULL,
	[city] [nvarchar](500) NULL,
	[mail] [nvarchar](100) NOT NULL,
	[countpost] [int] NULL,
	[createdtime] [datetime] NOT NULL,
	[photo] [varbinary](max) NULL,
	[mimetype] [nvarchar](50) NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[accountid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[comments]    Script Date: 05.04.2016 22:52:32 ******/
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
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_comments] PRIMARY KEY CLUSTERED 
(
	[comid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Likes]    Script Date: 05.04.2016 22:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[likeid] [uniqueidentifier] NOT NULL,
	[accountid] [uniqueidentifier] NOT NULL,
	[postid] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[likeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[posts]    Script Date: 05.04.2016 22:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[posts](
	[postid] [uniqueidentifier] NOT NULL,
	[postname] [nvarchar](50) NOT NULL,
	[source] [varbinary](max) NULL,
	[mimetype] [nvarchar](12) NULL,
	[createdtime] [datetime2](7) NOT NULL,
	[accountid] [uniqueidentifier] NOT NULL,
	[rating] [int] NOT NULL,
	[text] [nvarchar](500) NULL,
 CONSTRAINT [PK_post] PRIMARY KEY CLUSTERED 
(
	[postid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 05.04.2016 22:52:32 ******/
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
/****** Object:  Table [dbo].[tags]    Script Date: 05.04.2016 22:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tags](
	[postid] [uniqueidentifier] NULL,
	[tag] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 05.04.2016 22:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[Id] [uniqueidentifier] NOT NULL,
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
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
