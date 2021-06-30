IF OBJECTPROPERTY(object_id('dbo.sp_searchUsers'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_searchUsers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_searchUsers]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@Username NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON

	SELECT TOP 10 Id, Username FROM [User] 
	WHERE Username LIKE '%' + @Username + '%' 
	ORDER BY Username ASC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_getGroups'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_getGroups]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getGroups]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[Group].Id, 
		[Group].Name, 
		[Group].Description,
		GroupUser.InvitationAccepted,
		(SELECT COUNT(*) FROM GroupUser AS GU WHERE GU.GroupId = GroupUser.GroupId) AS UsersInGroup
	FROM GroupUser
		INNER JOIN [Group] ON GroupUser.GroupId = [Group].Id
	WHERE GroupUser.UserId = @UserId
		AND GroupUser.IsActive = 1
	ORDER BY Name ASC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_searchGames'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_searchGames]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_searchGames]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@Name NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON

	SELECT TOP 10 Id, Name FROM [Game] 
	WHERE Name LIKE '%' + @Name + '%' 
	ORDER BY Name ASC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_getGroupGames'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_getGroupGames]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getGroupGames]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@GroupId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT Game.Id, Game.Name, Game.ProfilePictureURL FROM GroupGame
		INNER JOIN Game ON GroupGame.GameId = Game.Id
	WHERE GroupGame.GroupId = @GroupId
		AND GroupGame.IsActive = 1
	ORDER BY Game.Name ASC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_getGroupUsers'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_getGroupUsers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getGroupUsers]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@GroupId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT [User].Id, [User].Username FROM GroupUser
		INNER JOIN [User] ON GroupUser.UserId = [User].Id
	WHERE GroupUser.GroupId = @GroupId
		AND GroupUser.IsActive = 1
	ORDER BY [User].Username ASC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_getGroupGameUserPlacements'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_getGroupGameUserPlacements]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getGroupGameUserPlacements]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@GroupId INT,
	@GameId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @PlacementValue TABLE(Placement SMALLINT NOT NULL, PlacementValue SMALLINT NOT NULL)
	INSERT INTO @PlacementValue VALUES(1, 8)
	INSERT INTO @PlacementValue VALUES(2, 4)
	INSERT INTO @PlacementValue VALUES(3, 2)

	SELECT TOP 3 [User].Id AS UserId, 
		[User].Username Username, 
		SUM(ISNULL(PV.PlacementValue, 1)) AS Score
	FROM GroupGameEventUserResult
		INNER JOIN [User] ON GroupGameEventUserResult.UserId = [User].Id
			AND [User].IsActive = 1
		INNER JOIN GroupGameEvent ON GroupGameEventUserResult.GroupGameEventId = GroupGameEvent.Id
			AND GroupGameEvent.GroupId = @GroupId
			AND GroupGameEvent.GameId = @GameId
		INNER JOIN GroupUser ON GroupUser.GroupId = @GroupId 
			AND GroupUser.UserId = [User].Id
			AND GroupUser.IsActive = 1
		LEFT JOIN @PlacementValue AS PV ON GroupGameEventUserResult.Placement = PV.Placement
	GROUP BY [User].Id, [User].Username
	ORDER BY SUM(ISNULL(PV.PlacementValue, 1)) DESC
END
GO

IF OBJECTPROPERTY(object_id('dbo.sp_getLatestGroupGameEvents'), N'IsProcedure') = 1 DROP PROCEDURE [dbo].[sp_getLatestGroupGameEvents]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getLatestGroupGameEvents]
-- =====================================================================
-- Author:			Rikard Gustafsson
-- Create date:		2021-06-18
-- Description:		
-- =====================================================================
	@GroupId INT,
	@GameId INT,
	@Limit INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @LatestEvents TABLE(Id INT NOT NULL, EventTime DATETIME2(0) NOT NULL)
	INSERT INTO @LatestEvents SELECT TOP (@Limit) ID, EventTime FROM GroupGameEvent
	WHERE GroupId = @GroupId
		AND GameId = @GameId
	ORDER BY GroupGameEvent.EventTime DESC, GroupGameEvent.Id DESC

	SELECT 
		LatestEvent.Id,
		LatestEvent.EventTime,
		[User].Username,
		GroupGameEventUserResult.Placement
	FROM @LatestEvents AS LatestEvent
		INNER JOIN GroupGameEventUserResult ON LatestEvent.Id = GroupGameEventUserResult.GroupGameEventId
		INNER JOIN [User] ON GroupGameEventUserResult.UserId = [User].Id
	WHERE GroupGameEventUserResult.Placement IN (1,2,3)
	ORDER BY LatestEvent.EventTime DESC, LatestEvent.Id DESC, GroupGameEventUserResult.Placement ASC
END
GO