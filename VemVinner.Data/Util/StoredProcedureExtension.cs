using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VemVinner.Data.ComplexModel;

namespace VemVinner.Data.Util
{
    public static class StoredProcedureExtension
    {
        public static async Task<List<sp_searchUsers_Result>> sp_searchUsers(this WhoWinsDbContext context, string username)
        {
            return await context.Set<sp_searchUsers_Result>().FromSqlRaw($"exec sp_searchUsers '{username}'").ToListAsync();
        }

        public static async Task<List<sp_searchGames_Result>> sp_searchGames(this WhoWinsDbContext context, string name)
        {
            return await context.Set<sp_searchGames_Result>().FromSqlRaw($"exec sp_searchGames '{name}'").ToListAsync();
        }

        public static async Task<List<sp_getGroups_Result>> sp_getGroups(this WhoWinsDbContext context, int userId)
        {
            return await context.Set<sp_getGroups_Result>().FromSqlRaw($"exec sp_getGroups {userId}").ToListAsync();
        }

        public static async Task<List<sp_getGroupUsers_Result>> sp_getGroupUsers(this WhoWinsDbContext context, int groupId)
        {
            return await context.Set<sp_getGroupUsers_Result>().FromSqlRaw($"exec sp_getGroupUsers {groupId}").ToListAsync();
        }

        public static async Task<List<sp_getGroupGames_Result>> sp_getGroupGames(this WhoWinsDbContext context, int groupId)
        {
            return await context.Set<sp_getGroupGames_Result>().FromSqlRaw($"exec sp_getGroupGames {groupId}").ToListAsync();
        }

        public static async Task<List<sp_getGroupGameUserPlacements_Result>> sp_getGroupGameUserPlacements(this WhoWinsDbContext context, int groupId, int gameId)
        {
            return await context.Set<sp_getGroupGameUserPlacements_Result>().FromSqlRaw($"exec sp_getGroupGameUserPlacements {groupId}, {gameId}").ToListAsync();
        }

        public static async Task<List<sp_getLatestGroupGameEvents_Result>> sp_getLatestGroupGameEvents(this WhoWinsDbContext context, int groupId, int gameId, short limit)
        {
            return await context.Set<sp_getLatestGroupGameEvents_Result>().FromSqlRaw($"exec sp_getLatestGroupGameEvents {groupId}, {gameId}, {limit}").ToListAsync();
        }
    }
}
