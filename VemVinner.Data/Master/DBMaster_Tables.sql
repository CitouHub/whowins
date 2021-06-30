-- Scaffold-DbContext "Server=localhost\SQLEXPRESS02;Initial Catalog=db_whowins;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir . -Context BaseDbContext -Force -NoOnConfiguring
-- =====================================================================
IF OBJECTPROPERTY(object_id('dbo.GroupGameEventUserResult'), N'IsTable') = 1 DROP TABLE [dbo].[GroupGameEventUserResult]
GO
IF OBJECTPROPERTY(object_id('dbo.GroupGameEvent'), N'IsTable') = 1 DROP TABLE [dbo].[GroupGameEvent]
GO
IF OBJECTPROPERTY(object_id('dbo.GroupGame'), N'IsTable') = 1 DROP TABLE [dbo].[GroupGame]
GO
IF OBJECTPROPERTY(object_id('dbo.GroupUser'), N'IsTable') = 1 DROP TABLE [dbo].[GroupUser]
GO
IF OBJECTPROPERTY(object_id('dbo.UserAchievement'), N'IsTable') = 1 DROP TABLE [dbo].[UserAchievement]
GO
IF OBJECTPROPERTY(object_id('dbo.UserStatistics'), N'IsTable') = 1 DROP TABLE [dbo].[UserStatistics]
GO
IF OBJECTPROPERTY(object_id('dbo.Achievement'), N'IsTable') = 1 DROP TABLE [dbo].[Achievement]
GO
IF OBJECTPROPERTY(object_id('dbo.User'), N'IsTable') = 1 DROP TABLE [dbo].[User]
GO
IF OBJECTPROPERTY(object_id('dbo.Group'), N'IsTable') = 1 DROP TABLE [dbo].[Group]
GO
IF OBJECTPROPERTY(object_id('dbo.Game'), N'IsTable') = 1 DROP TABLE [dbo].[Game]
GO

-- Create new tables
-- =====================================================================
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](500) NULL,
	[PasswordSalt] [varbinary](100) NULL,
	[IsProxy] [bit] NOT NULL DEFAULT(0),
	[IsActive] [bit] NOT NULL DEFAULT(1)
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
CREATE UNIQUE NONCLUSTERED INDEX [IdxUser_Username] ON [dbo].[User]
(
    [Username] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[UserStatistics](
	[UserId] [int] NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[LoggedIn] [int] NOT NULL DEFAULT(0),
	[GamesPlayed] [int] NOT NULL DEFAULT(0),
	[GamesFirstPlace] [int] NOT NULL DEFAULT(0),
	[GamesFirstPlaceStreak] [smallint] NOT NULL DEFAULT(0),
	[GamesSecondPlace] [int] NOT NULL DEFAULT(0),
	[GamesSecondPlaceStreak] [smallint] NOT NULL DEFAULT(0),
	[GamesThirdPlace] [int] NOT NULL DEFAULT(0),
	[GamesThirdPlaceStreak] [smallint] NOT NULL DEFAULT(0),
	[GamesLastPlace] [int] NOT NULL DEFAULT(0),
	[GamesLastPlaceStreak] [smallint] NOT NULL DEFAULT(0)
 CONSTRAINT [UserStatistics_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[Achievement](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [Achievement_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[UserAchievement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[UserId] [int] NOT NULL,
	[AchievementId] [smallint] NOT NULL
 CONSTRAINT [UserAchievement_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[UserAchievement] WITH CHECK ADD CONSTRAINT [UserAchievement_UserFK] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserAchievement] WITH CHECK ADD CONSTRAINT [UserAchievement_AchievementFK] FOREIGN KEY([AchievementId]) REFERENCES [dbo].[Achievement] ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IdxUserAchievement_Unique] ON [dbo].[UserAchievement]
(
    [UserId] ASC,
	[AchievementId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [Group_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[GroupUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[InvitationAccepted] [bit] NOT NULL DEFAULT(0),
	[IsActive] [bit] NOT NULL DEFAULT(1)
 CONSTRAINT [GroupUser_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[GroupUser] WITH CHECK ADD CONSTRAINT [GroupUser_GroupFK] FOREIGN KEY([GroupId]) REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupUser] WITH CHECK ADD CONSTRAINT [GroupUser_UserFK] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IdxGroupUser_Unique] ON [dbo].[GroupUser]
(
    [GroupId] ASC,
	[UserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[Game](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ProfilePictureURL] [nvarchar](100) NOT NULL
 CONSTRAINT [Game_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
CREATE UNIQUE NONCLUSTERED INDEX [IdxGame_Name] ON [dbo].[Game]
(
    [Name] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[GroupGame](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[GroupId] [int] NOT NULL,
	[GameId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT(1)
 CONSTRAINT [GroupGame_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[GroupGame] WITH CHECK ADD CONSTRAINT [GroupGame_GroupFK] FOREIGN KEY([GroupId]) REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupGame] WITH CHECK ADD CONSTRAINT [GroupGame_GameFK] FOREIGN KEY([GameId]) REFERENCES [dbo].[Game] ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IdxGroupGame_Unique] ON [dbo].[GroupGame]
(
    [GroupId] ASC,
	[GameId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[GroupGameEvent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[GroupId] [int] NOT NULL,
	[GameId] [int] NOT NULL,
	[EventTime] [datetime2](0) NOT NULL DEFAULT(GETUTCDATE())
 CONSTRAINT [GroupGameEvent_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[GroupGameEvent] WITH CHECK ADD CONSTRAINT [GroupGameEvent_GroupFK] FOREIGN KEY([GroupId]) REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupGameEvent] WITH CHECK ADD CONSTRAINT [GroupGameEvent_GameFK] FOREIGN KEY([GameId]) REFERENCES [dbo].[Game] ([Id])
GO

CREATE TABLE [dbo].[GroupGameEventUserResult](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[GroupGameEventId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Placement] [smallint] NOT NULL
 CONSTRAINT [GroupGameEventUserResult_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[GroupGameEventUserResult] WITH CHECK ADD CONSTRAINT [GroupGameEventUserResult_GroupGameEventFK] FOREIGN KEY([GroupGameEventId]) REFERENCES [dbo].[GroupGameEvent] ([Id])
GO
ALTER TABLE [dbo].[GroupGameEventUserResult] WITH CHECK ADD CONSTRAINT [GroupGameEventUserResult_UserFK] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IdxGroupGameEventUserResult_Unique] ON [dbo].[GroupGameEventUserResult]
(
    [GroupGameEventId] ASC,
	[UserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO