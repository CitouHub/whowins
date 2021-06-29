using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using VemVinner.Data;
using VemVinner.Domain;
using VemVinner.Domain.Variable;

namespace VemVinner.Service
{
    public interface IAchievementService
    {
        Task AddUserAchievement(int userId, AchievementType achievementType);
        Task UpdateUserStatisticAchievement(int userId);
        Task<List<AchievementDTO>> GetUserAchievements(int userId);
    }

    public class AchievementService : IAchievementService
    {
        private readonly WhoWinsDbContext _context;
        private readonly IMapper _mapper;

        public AchievementService(
            WhoWinsDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUserAchievement(int userId, AchievementType achievementType)
        {
            var achievementExists = (await _context.UserAchievements
                .AnyAsync(_ => _.UserId == userId && _.AchievementId == (short)achievementType));
            if (achievementExists == false)
            {
                await _context.UserAchievements.AddAsync(new UserAchievement()
                {
                    InsertByUser = userId,
                    UserId = userId,
                    AchievementId = (short)achievementType
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserStatisticAchievement(int userId)
        {
            var userStatistics = await _context.UserStatistics.FindAsync(userId);
            if(userStatistics != null)
            {
                var userAchievements = _context.UserAchievements.Where(_ => _.UserId == userId).ToList();
                var achievementTypes = new List<AchievementType>();

                if (userStatistics.LoggedIn >= 10) achievementTypes.Add(AchievementType.Login10);
                if (userStatistics.LoggedIn >= 100) achievementTypes.Add(AchievementType.Login100);
                if (userStatistics.GamesFirstPlace >= 1) achievementTypes.Add(AchievementType.Won1);
                if (userStatistics.GamesFirstPlace >= 10) achievementTypes.Add(AchievementType.Won10);
                if (userStatistics.GamesFirstPlace >= 100) achievementTypes.Add(AchievementType.Won100);
                if (userStatistics.GamesFirstPlaceStreak >= 2) achievementTypes.Add(AchievementType.Won2C);
                if (userStatistics.GamesFirstPlaceStreak >= 4) achievementTypes.Add(AchievementType.Won4C);
                if (userStatistics.GamesFirstPlaceStreak >= 8) achievementTypes.Add(AchievementType.Won8C);
                if (userStatistics.GamesPlayed >= 1) achievementTypes.Add(AchievementType.Played1);
                if (userStatistics.GamesPlayed >= 10) achievementTypes.Add(AchievementType.Played10);
                if (userStatistics.GamesPlayed >= 100) achievementTypes.Add(AchievementType.Played100);
                if (userStatistics.GamesPlayed >= 1000) achievementTypes.Add(AchievementType.Played1000);
                if (userStatistics.GamesPlayed >= 10000) achievementTypes.Add(AchievementType.Played10000);
                if (userStatistics.GamesLastPlaceStreak >= 1) achievementTypes.Add(AchievementType.Lost1);
                if (userStatistics.GamesLastPlaceStreak >= 3) achievementTypes.Add(AchievementType.Lost3);
                if (userStatistics.GamesLastPlaceStreak >= 5) achievementTypes.Add(AchievementType.Lost5);
                if (userStatistics.GamesSecondPlaceStreak >= 5) achievementTypes.Add(AchievementType.Second5);
                if (userStatistics.GamesThirdPlaceStreak >= 5) achievementTypes.Add(AchievementType.Third5);

                var newUserAchievements = achievementTypes.Where(achievementType => userAchievements.Any(_ => _.AchievementId == (short)achievementType) == false).ToList();

                if(newUserAchievements.Any())
                {
                    foreach (var achievementType in newUserAchievements)
                    {
                        await _context.UserAchievements.AddAsync(new UserAchievement()
                        {
                            InsertByUser = userId,
                            UserId = userId,
                            AchievementId = (short)achievementType
                        });
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<AchievementDTO>> GetUserAchievements(int userId)
        {
            var achievements = await _context.UserAchievements
                .Where(_ => _.UserId == userId)
                .OrderBy(_ => _.AchievementId)
                .Select(_ => _.Achievement)
                .ToListAsync();
            return _mapper.Map<List<AchievementDTO>>(achievements);
        }
    }
}