using AutoMapper;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;

namespace ValuBakery.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            #region Entities
            CreateMap<UserDto, User>();
            #endregion

            #region Dtos
            CreateMap<User, UserDto>();
            #endregion
        }
    }
}
