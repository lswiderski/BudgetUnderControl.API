using AutoMapper;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.ApiInfrastructure.Profiles.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(s => Enum.Parse<UserRole>(s.Role)))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(s => s.Role));
            CreateMap<Domain.User, UserListItemDTO>();
        }

    }
}
