using AutoMapper;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class UserAdminService : IUserAdminService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserAdminService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<ICollection<UserListItemDTO>> GetUsersAsync()
        {
            var users = await this.userRepository.GetUsersAsync();

            return this.mapper.Map<ICollection<UserListItemDTO>>(users);
        }

        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            var user = await this.userRepository.GetAsync(id);

            return this.mapper.Map<UserDTO>(user);
        }
    }
}
