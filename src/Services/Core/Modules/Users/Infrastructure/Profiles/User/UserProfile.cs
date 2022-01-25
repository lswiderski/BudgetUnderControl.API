using AutoMapper;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Users.Application.DTO;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Modules.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(s => Enum.Parse<UserRole>(s.Role)))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(s => s.Role))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(s => s.Id));
            CreateMap<User, UserListItemDTO>()
              .ForMember(dest => dest.Role, opt => opt.MapFrom(s => Enum.Parse<UserRole>(s.Role)))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(s => s.Role))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(s => s.Id));
        }

    }
}
