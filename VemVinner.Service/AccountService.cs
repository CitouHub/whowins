using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

using VemVinner.Data;
using VemVinner.Data.Util;
using VemVinner.Domain.Account;
using VemVinner.Domain.Variable;

namespace VemVinner.Service
{
    public interface IAccountService
    {
        UserDTO User { get; }

        Task Initialize();
        Task Login(LoginDTO login);
        Task<bool> UserExists(string username);
        Task Register(RegisterUserDTO registerUser);
        Task<List<UserDTO>> SearchUsers(string username);
    }

    public class AccountService : IAccountService
    {
        public UserDTO User { get; private set; }

        private string _userKey = "WhoWinsUser";
        private readonly WhoWinsDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILocalStorageService _localStorageService;
        private readonly IUserStatisticsService _userStatisticsService;
        private readonly IAchievementService _achievementService;

        public AccountService(
            WhoWinsDbContext context, 
            IMapper mapper, 
            ILocalStorageService localStorageService,
            IUserStatisticsService userStatisticsService,
            IAchievementService achievementService)
        {
            _context = context;
            _mapper = mapper;
            _localStorageService = localStorageService;
            _userStatisticsService = userStatisticsService;
            _achievementService = achievementService;
        }

        public async Task Initialize()
        {
            var cachedUser = await _localStorageService.GetItem<UserDTO>(_userKey);
            if(cachedUser != null)
            {
                var userExists = await _context.Users.AnyAsync(_ => _.Username == cachedUser.Username && _.Id == cachedUser.Id);
                if (userExists)
                {
                    User = cachedUser;
                }
            }
        }

        public async Task Login(LoginDTO login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(_ => _.Username == login.Username);
            if (user != null)
            {
                if (user.Password == GetPasswordHash(login.Password, user.PasswordSalt))
                {
                    User = _mapper.Map<UserDTO>(user);
                    await _localStorageService.SetItem<UserDTO>(_userKey, User);
                    await _userStatisticsService.UserLoggedIn(user.Id);
                    await _achievementService.UpdateUserStatisticAchievement(user.Id);
                }
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(_ => _.Username == username);
        }

        public async Task Register(RegisterUserDTO registerUser)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var newUser = new User()
            {
                Username = registerUser.Username,
                Password = GetPasswordHash(registerUser.Password, salt),
                PasswordSalt = salt
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            await _achievementService.AddUserAchievement(newUser.Id, AchievementType.Registered);
        }

        private static string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
        }

        public async Task<List<UserDTO>> SearchUsers(string username)
        {
            var users = await _context.sp_searchUsers(username);
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}