using System;

using AutoMapper;

using VemVinner.Data;
using VemVinner.Data.ComplexModel;
using VemVinner.Domain.Account;

namespace VemVinner.Domain
{
    public class AutoMapperProfile : Profile
    {
        private readonly TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

        public AutoMapperProfile()
        {
            CreateMap<sp_searchUsers_Result, UserDTO>();
            CreateMap<sp_getGroupUsers_Result, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<sp_searchGames_Result, GameDTO>();
            CreateMap<sp_getGroupGames_Result, GameDTO>();
            CreateMap<Game, GameDTO>().ReverseMap();

            CreateMap<sp_getGroups_Result, GroupDTO>();
            CreateMap<Group, GroupDTO>().ReverseMap();

            CreateMap<sp_getGroupGameUserPlacements_Result, GroupGameUserPlacementDTO>();
            CreateMap<sp_getLatestGroupGameEvents_Result, GroupGameEventDTO>()
                .ForMember(dest => dest.EventTime, opt => opt.MapFrom(src => GetLocalDateTime(src.EventTime)));
            CreateMap<sp_getLatestGroupGameEvents_Result, GameUserScoreDTO>();

            CreateMap<Achievement, AchievementDTO>().ReverseMap();
        }

        public DateTime? GetLocalDateTime(DateTime? date)
        {
            if (date != null)
            {
                var utcDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, date.Value.Hour, date.Value.Minute, date.Value.Second, DateTimeKind.Utc);
                var localDateTime = TimeZoneInfo.ConvertTime(utcDate, timeZone);
                return localDateTime;
            }

            return null;
        }
    }
}
