using AutoMapper;
using Challenge.DTOs;
using Challenge.Entities;


namespace Practice.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
