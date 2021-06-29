DELETE GroupGameEventUserResult
DELETE GroupGameEvent
DELETE GroupGame
DELETE GroupUser
DELETE UserAchievement
DELETE UserStatistics
DELETE [User]
DELETE [Group]
DELETE Game

SET IDENTITY_INSERT [User] ON
GO
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (1, 'Rikard', N'mf28LcCk4ctask5UGNJ1cINDwUAox6VONgz8gmeNaTk=', 0xDEFAE59816540B36D35FC02F100AB215)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (2, 'Jossan', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (3, 'Viktor', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (4, 'Annika', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (5, 'Jan-�ke', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (6, 'Albin', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (7, 'Petter', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (8, 'Matthias', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (9, 'Pauly', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (10, 'Gustav', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (11, 'Ricke', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (12, 'Eric', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (13, 'Pelle', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (14, 'Johan', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (15, 'Nisse', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (16, 'Sven', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (17, 'Kalle', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (18, 'Marcus', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (19, 'Bosse', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (20, 'Leia', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (21, 'Noah', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (22, 'Emma', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (23, 'Jacob', 'Test', 0x0)
INSERT INTO [User]([Id], [Username], [Password], [PasswordSalt]) VALUES (24, 'Henrik', 'Test', 0x0)
SET IDENTITY_INSERT [User] OFF
GO

SET IDENTITY_INSERT [Game] ON
GO
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (1, 'Settlers of Catan', '/images/game/game4.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (2, '500', '/images/game/game9.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (3, 'Backgammon', '/images/game/game1.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (4, 'Ticket to ride', '/images/game/game8.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (5, 'Chicago', '/images/game/game9.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (6, 'Vem vet m�st', '/images/game/game11.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (7, 'Kalaha', '/images/game/game6.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (8, 'Plump', '/images/game/game9.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (9, 'Sten, sax, och p�se', '/images/game/game10.png')
INSERT INTO [Game]([Id], [Name], [ProfilePictureURL]) VALUES (10, 'Rummy', '/images/game/game9.png')
SET IDENTITY_INSERT [Game] OFF
GO

SET IDENTITY_INSERT [Group] ON
GO
INSERT INTO [Group]([Id], [Name]) VALUES (1, 'Familjen') 
INSERT INTO [Group]([Id], [Name]) VALUES (2, 'Gejm och MG') 
INSERT INTO [Group]([Id], [Name]) VALUES (3, 'J&R') 
INSERT INTO [Group]([Id], [Name]) VALUES (4, 'RPM') 
INSERT INTO [Group]([Id], [Name]) VALUES (5, 'Arimmon') 
SET IDENTITY_INSERT [Group] OFF
GO

INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 1)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 2)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 3)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 4)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 5)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (1, 6)

INSERT INTO [GroupUser](GroupId, UserId) VALUES (2, 1)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (2, 10)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (2, 11)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (2, 12)

INSERT INTO [GroupUser](GroupId, UserId) VALUES (3, 1)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (3, 2)

INSERT INTO [GroupUser](GroupId, UserId) VALUES (4, 1)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (4, 8)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (4, 9)

INSERT INTO [GroupUser](GroupId, UserId) VALUES (5, 1)
INSERT INTO [GroupUser](GroupId, UserId) VALUES (5, 24)

INSERT INTO [GroupGame](GroupId, GameId) VALUES (1, 6)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (1, 8)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (1, 10)

INSERT INTO [GroupGame](GroupId, GameId) VALUES (2, 1)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (2, 4)

INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 1)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 2)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 3)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 4)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 5)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 6)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 7)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 8)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 9)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (3, 10)

INSERT INTO [GroupGame](GroupId, GameId) VALUES (4, 1)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (4, 4)
INSERT INTO [GroupGame](GroupId, GameId) VALUES (4, 6)

INSERT INTO [GroupGame](GroupId, GameId) VALUES (5, 3)

DECLARE @GroupId INT
DECLARE @GameId INT
DECLARE group_cursor CURSOR FOR SELECT GroupId, GameId FROM [GroupGame]
OPEN group_cursor 
FETCH NEXT FROM group_cursor INTO @GroupId, @GameId

WHILE @@FETCH_STATUS = 0  
BEGIN  
	DECLARE @RoundsPlayed INT = RAND()*100 + 10
	DECLARE @Round INT = 0

	WHILE(@Round < @RoundsPlayed)
	BEGIN
		INSERT INTO GroupGameEvent(GroupId, GameId) VALUES(@GroupId, @GameId)

		DECLARE @GroupGameEventId INT = @@IDENTITY

		INSERT INTO GroupGameEventUserResult(GroupGameEventId, UserId, Placement)
		SELECT @GroupGameEventId, UserId, ROW_NUMBER() OVER(ORDER BY NEWID() ASC)
		FROM GroupUser WHERE GroupId = @GroupID
		ORDER BY NEWID()

		SET @Round = @Round + 1;
	END

	FETCH NEXT FROM group_cursor INTO @GroupId, @GameId 
END 

CLOSE group_cursor  
DEALLOCATE group_cursor 

DECLARE @UserId INT
DECLARE user_cursor CURSOR FOR SELECT Id FROM [User]
OPEN user_cursor 
FETCH NEXT FROM user_cursor INTO @UserId

WHILE @@FETCH_STATUS = 0  
BEGIN  
	DECLARE @A TABLE(Id SMALLINT NOT NULL, GuidSeed NVARCHAR(50) NOT NULL)
	INSERT INTO @A SELECT ID, NEWID() FROM [Achievement]

	INSERT INTO UserAchievement(UserId, AchievementId)
	SELECT @UserId, Id FROM @A
	WHERE CAST(SUBSTRING(GuidSeed, PATINDEX('%[0-9]%', GuidSeed), 1) AS INT) % 3 = 0

	FETCH NEXT FROM user_cursor INTO @UserId
END 

CLOSE user_cursor  
DEALLOCATE user_cursor 