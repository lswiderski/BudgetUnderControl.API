using AutoMapper;
using BudgetUnderControl.Common.Contracts.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.ApiInfrastructure.Profiles.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.User, UserDTO>();
        }
    }
}
