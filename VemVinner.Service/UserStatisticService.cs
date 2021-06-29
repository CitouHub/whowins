using System.Threading.Tasks;

using VemVinner.Data;

namespace VemVinner.Service
{
    public interface IUserStatisticsService
    {
        Task UserLoggedIn(int userId);
        Task UserGameResult(int userId, short placement, short players);
    }

    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly WhoWinsDbContext _context;

        public UserStatisticsService(WhoWinsDbContext context)
        {
            _context = context;
        }

        private async Task<UserStatistic> GetUserStatistic(int userId)
        {
            var userStatistic = await _context.UserStatistics.FindAsync(userId);
            if(userStatistic == null)
            {
                userStatistic = new UserStatistic()
                {
                    InsertByUser = userId,
                    UserId = userId
                };
                await _context.AddAsync(userStatistic);
                await _context.SaveChangesAsync();
            }

            return userStatistic;
        }

        public async Task UserLoggedIn(int userId)
        {
            var userStatistic = await GetUserStatistic(userId);
            userStatistic.UpdateByUser = userId;
            userStatistic.LoggedIn++;
            await _context.SaveChangesAsync();
        }

        public async Task UserGameResult(int userId, short placement, short players)
        {
            var userStatistic = await GetUserStatistic(userId);
            userStatistic.UpdateByUser = userId;
            userStatistic.GamesPlayed++;
            if(placement == 1)
            {
                userStatistic.GamesFirstPlace++;
                userStatistic.GamesFirstPlaceStreak++;
            } 
            else
            {
                userStatistic.GamesFirstPlaceStreak = 0;
            }

            if (placement == 2)
            {
                userStatistic.GamesSecondPlace++;
                userStatistic.GamesSecondPlaceStreak++;
            }
            else
            {
                userStatistic.GamesSecondPlaceStreak = 0;
            }

            if (placement == 3)
            {
                userStatistic.GamesThirdPlace++;
                userStatistic.GamesThirdPlaceStreak++;
            }
            else
            {
                userStatistic.GamesThirdPlaceStreak = 0;
            }

            if (placement == players)
            {
                userStatistic.GamesLastPlace++;
                userStatistic.GamesLastPlaceStreak++;
            }
            else
            {
                userStatistic.GamesLastPlaceStreak = 0;
            }

            await _context.SaveChangesAsync();
        }
    }
}