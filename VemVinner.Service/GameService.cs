using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using VemVinner.Data;
using VemVinner.Data.Util;
using VemVinner.Domain;
using VemVinner.Common.Extension;
using VemVinner.Domain.Variable;

namespace VemVinner.Service
{
    public interface IGameService
    {
        Task<bool> GameExists(string name);
        Task AddGame(int userId, GameDTO game);
        Task UpdateGame(int userId, GameDTO gameUpdate);
        Task<GameDTO> GetGame(int gameId);
        Task<List<GameDTO>> SearchGames(string name);
        Task<List<GameDTO>> GetGroupGames(int groupId);
        Task<List<GroupGameUserPlacementDTO>> GetGroupGameUserPlacements(int groupId, int gameId);
        Task<List<GroupGameEventDTO>> GetGroupGameEvents(int groupId, int gameId, short limit);
        Task SaveResult(int userId, int groupId, int gameId, List<GameUserScoreDTO> result);
    }

    public class GameService : IGameService
    {
        private readonly WhoWinsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserStatisticsService _userStatisticsService;
        private readonly IAchievementService _achievementService;

        public GameService(
            WhoWinsDbContext context,
            IMapper mapper,
            IUserStatisticsService userStatisticsService,
            IAchievementService achievementService) 
        {
            _context = context;
            _mapper = mapper;
            _userStatisticsService = userStatisticsService;
            _achievementService = achievementService;
        }

        public async Task<bool> GameExists(string name)
        {
            return await _context.Games.AnyAsync(_ => _.Name == name);
        }

        public async Task AddGame(int userId, GameDTO game)
        {
            var newGame = _mapper.Map<Game>(game);
            newGame.InsertByUser = userId;

            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGame(int userId, GameDTO gameUpdate)
        {
            var game = await _context.Games.FindAsync(gameUpdate.Id);
            if (game != null)
            {
                _mapper.Map(gameUpdate, game);
                game.UpdateByUser = userId;
                game.UpdateDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameDTO> GetGame(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            return _mapper.Map<GameDTO>(game);
        }

        public async Task<List<GroupGameUserPlacementDTO>> GetGroupGameUserPlacements(int groupId, int gameId)
        {
            var groupGameUserPlacemets = await _context.sp_getGroupGameUserPlacements(groupId, gameId);
            return _mapper.Map<List<GroupGameUserPlacementDTO>>(groupGameUserPlacemets);
        }

        public async Task<List<GroupGameEventDTO>> GetGroupGameEvents(int groupId, int gameId, short limit)
        {
            var groupGameEvents = await _context.sp_getLatestGroupGameEvents(groupId, gameId, limit);
            var groupGameEventsDTO = _mapper.Map<List<GroupGameEventDTO>>(groupGameEvents)
                .DistinctBy(p => new { p.Id, p.EventTime }).ToList();

            foreach (var groupGameEvent in groupGameEvents.GroupBy(_ => _.Id ))   
            {
                var groupGameEventId = groupGameEvent.Key;
                var groupGameEventDTO = groupGameEventsDTO.FirstOrDefault(_ => _.Id == groupGameEventId);
                var userPlacement = groupGameEvents.Where(_ => _.Id == groupGameEvent.Key).ToList();
                groupGameEventDTO.UserPlacements = _mapper.Map<List<GameUserScoreDTO>>(userPlacement);
            }

            return groupGameEventsDTO;
        }

        public async Task<List<GameDTO>> SearchGames(string name)
        {
            var games = await _context.sp_searchGames(name);
            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task<List<GameDTO>> GetGroupGames(int groupId)
        {
            var games = await _context.sp_getGroupGames(groupId);
            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task SaveResult(int userId, int groupId, int gameId, List<GameUserScoreDTO> result)
        {
            var groupGameEvent = new GroupGameEvent()
            {
                InsertByUser = userId,
                GroupId = groupId,
                GameId = gameId
            };
            await _context.GroupGameEvents.AddAsync(groupGameEvent);
            await _context.SaveChangesAsync();

            for(short placement = 1; placement <= result.Count; placement++)
            {
                var userResult = result[placement - 1];
                await _context.GroupGameEventUserResults.AddAsync(new GroupGameEventUserResult()
                {
                    InsertByUser = userId,
                    GroupGameEventId = groupGameEvent.Id,
                    UserId = userResult.UserId,
                    Placement = placement
                });
                await _context.SaveChangesAsync();

                await _userStatisticsService.UserGameResult(userResult.UserId, placement, (short)result.Count);
                await _achievementService.UpdateUserStatisticAchievement(userResult.UserId);
            }

            await AddBeatAchievement(userId, result, "Jossan", AchievementType.BeatJossan);
            await AddBeatAchievement(userId, result, "Josefine", AchievementType.BeatJossan);
            await AddBeatAchievement(userId, result, "Gustav", AchievementType.BeatGustav);
        }

        private async Task AddBeatAchievement(int userId, List<GameUserScoreDTO> result, string playerToBeat, AchievementType achievementType)
        {
            if(result.Any(_ => _.Username == playerToBeat))
            {
                var ownPlacement = result.IndexOf(result.FirstOrDefault(_ => _.UserId == userId));
                var playerPlacement = result.IndexOf(result.FirstOrDefault(_ => _.Username == playerToBeat));

                if(ownPlacement < playerPlacement)
                {
                    await _achievementService.AddUserAchievement(userId, achievementType);
                }
            }
        }
    }
}