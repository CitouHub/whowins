using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using VemVinner.Data;
using VemVinner.Data.Util;
using VemVinner.Domain;
using VemVinner.Domain.Account;

namespace VemVinner.Service
{
    public interface IGroupService
    {
        Task AddGroup(int userId, GroupDTO group);
        Task UpdateGroup(int userId, GroupDTO group);
        Task<GroupDTO> GetGroup(int groupId);
        Task<List<GroupDTO>> GetGroups(int userId);
        Task<List<UserDTO>> GetGroupUsers(int groupId);
        Task<List<GameDTO>> GetGroupGames(int groupId);
        Task AnswerGroupInvitation(int userId, int groupId, bool accept);
    }

    public class GroupService : IGroupService
    {
        private readonly WhoWinsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public GroupService(
            WhoWinsDbContext context,
            IMapper mapper,
            IAccountService accountService) 
        {
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task AddGroup(int userId, GroupDTO group)
        {
            var newGroup = new Group()
            {
                InsertByUser = userId,
                Name = group.Name,
                Description = group.Description,
            };
            await _context.Groups.AddAsync(newGroup);
            await _context.SaveChangesAsync();

            foreach (var user in group.Users)
            {
                await _context.GroupUsers.AddAsync(new GroupUser()
                {
                    InsertByUser = userId,
                    GroupId = newGroup.Id,
                    UserId = user.Id
                });
            }
            foreach (var game in group.Games)
            {
                await _context.GroupGames.AddAsync(new GroupGame()
                {
                    InsertByUser = userId,
                    GroupId = newGroup.Id,
                    GameId = game.Id.Value
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(int userId, GroupDTO groupUpdate)
        {
            var group = await _context.Groups.FindAsync(groupUpdate.Id);
            if(group != null)
            {
                _mapper.Map(groupUpdate, group);
                group.UpdateByUser = userId;
                group.UpdateDate = DateTime.UtcNow;

                foreach(var newUser in groupUpdate.Users.Where(_ => _.Id == -1))
                {
                    newUser.Id = await _accountService.RegisterProxy(userId, newUser.Username);
                }

                foreach (var user in groupUpdate.Users)
                {
                    var existingGroupUser = await _context.GroupUsers
                        .FirstOrDefaultAsync(_ => _.GroupId == groupUpdate.Id && _.UserId == user.Id);
                    if (existingGroupUser != null)
                    {
                        if (existingGroupUser.IsActive == false)
                        {
                            existingGroupUser.IsActive = true;
                            existingGroupUser.UpdateByUser = userId;
                            existingGroupUser.UpdateDate = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        await _context.GroupUsers.AddAsync(new GroupUser()
                        {
                            InsertByUser = userId,
                            GroupId = groupUpdate.Id,
                            UserId = user.Id
                        });
                    }
                }
                foreach (var game in groupUpdate.Games)
                {
                    var existingGroupGame = await _context.GroupGames
                        .FirstOrDefaultAsync(_ => _.GroupId == groupUpdate.Id && _.GameId == game.Id);
                    if (existingGroupGame != null)
                    {
                        if (existingGroupGame.IsActive == false)
                        {
                            existingGroupGame.IsActive = true;
                            existingGroupGame.UpdateByUser = userId;
                            existingGroupGame.UpdateDate = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        await _context.GroupGames.AddAsync(new GroupGame()
                        {
                            InsertByUser = userId,
                            GroupId = groupUpdate.Id,
                            GameId = game.Id.Value
                        });
                    }
                }
                foreach (var user in groupUpdate.DeactivateUsers)
                {
                    var existingGroupUser = await _context.GroupUsers
                        .FirstOrDefaultAsync(_ => _.GroupId == groupUpdate.Id && _.UserId == user.Id && _.IsActive == true);
                    if (existingGroupUser != null)
                    {
                        existingGroupUser.IsActive = false;
                        existingGroupUser.UpdateByUser = userId;
                        existingGroupUser.UpdateDate = DateTime.UtcNow;
                    }
                }
                foreach (var game in groupUpdate.DeactivateGames)
                {
                    var existingGroupGame = await _context.GroupGames
                        .FirstOrDefaultAsync(_ => _.GroupId == groupUpdate.Id && _.GameId == game.Id && _.IsActive == true);
                    if (existingGroupGame != null)
                    {
                        existingGroupGame.IsActive = false;
                        existingGroupGame.UpdateByUser = userId;
                        existingGroupGame.UpdateDate = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GroupDTO>> GetGroups(int userId)
        {
            var groups = await _context.sp_getGroups(userId);
            return _mapper.Map<List<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetGroup(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            return _mapper.Map<GroupDTO>(group);
        }

        public async Task<List<UserDTO>> GetGroupUsers(int groupId)
        {
            var groupUsers = await _context.sp_getGroupUsers(groupId);
            return _mapper.Map<List<UserDTO>>(groupUsers);
        }

        public async Task<List<GameDTO>> GetGroupGames(int groupId)
        {
            var groupGames = await _context.sp_getGroupGames(groupId);
            return _mapper.Map<List<GameDTO>>(groupGames);
        }

        public async Task AnswerGroupInvitation(int userId, int groupId, bool accept)
        {
            var group = await _context.GroupUsers.FirstOrDefaultAsync(_ => _.GroupId == groupId && _.UserId == userId);
            group.UpdateByUser = userId;
            group.UpdateDate = DateTime.UtcNow;
            group.InvitationAccepted = accept;
            if (accept == false)
            {
                group.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}