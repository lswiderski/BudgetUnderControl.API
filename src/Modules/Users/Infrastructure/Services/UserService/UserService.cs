using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using BudgetUnderControl.Common.Enums;
using AutoMapper;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Modules.Users.Domain.Repositories;
using BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser;
using BudgetUnderControl.Shared.Abstractions.Enums;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.UpdateUser;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtHandlerService jwtHandlerService;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;
        // private readonly ITokenRepository tokenRepository;
        public UserService(IUserRepository userRepository, IEncrypter encrypter,
            IJwtHandlerService jwtHandlerService,
            IMemoryCache cache,
            IMapper mapper)//,
                           // ITokenRepository tokenRepository)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.jwtHandlerService = jwtHandlerService;
            this.cache = cache;
            this.mapper = mapper;
            // this.tokenRepository = tokenRepository;
        }

        public async Task<string> ValidateLoginAsync(string username, string password)
        {
            var user = await userRepository.GetAsync(username);

            if (user == null)
            {
                return string.Empty;
            }

            var hash = encrypter.GetHash(password, user.Salt);

            if (hash != user.Password)
            {
                return string.Empty;
            }

            var token = jwtHandlerService.CreateToken(user);

            return token;
        }

        public async Task<string> RegisterUserAsync(CreateNewUserCommand command)
        {
            var salt = encrypter.GetSalt();

            var hash = encrypter.GetHash(command.Password, salt);
            var user = User.Create(command.Username, command.FirstName, command.LastName, UserRole.User.GetStringValue(), command.Email, hash, salt);

            await userRepository.AddUserAsync(user);

            var token = jwtHandlerService.CreateToken(user);

            // var activationToken = Token.Create(TokenType.Activation, user.ExternalId, user.Id, DateTime.UtcNow.AddDays(1));
            await Task.CompletedTask;// tokenRepository.AddAsync(activationToken);


            // await this.notificationService.SendRegisterNotificationAsync(this.mapper.Map<UserDTO>(user), activationToken.Code);
            return token;
        }

        public async Task ResetActivationCodeAsync(Guid userId)
        {
            var user = await this.userRepository.GetAsync(userId);

            await this.userRepository.UpdateUserAsync(user);
            /*
                        await tokenRepository.DeactivateTokensAsync(TokenType.Activation, user.ExternalId);
                        var activationToken = Token.Create(TokenType.Activation, user.ExternalId, user.Id, DateTime.UtcNow.AddDays(1));
                        await tokenRepository.AddAsync(activationToken);
            */
            // await this.notificationService.SendRegisterNotificationAsync(this.mapper.Map<UserDTO>(user), activationToken.Code);
        }

        public async Task<bool> ActivateUserAsync(Guid userId, string code)
        {
            var user = await userRepository.GetAsync(userId);

            if (user == null)
            {
                return false;
            }
            /*  var token = await tokenRepository.GetByCodeAsync(code, TokenType.Activation, user.ExternalId);

              if(token != null && token.IsValid && token.ValidUntil >= DateTime.UtcNow)
              {
                  var result = user.Activate();
                  token.Devalidate();

                  await this.userRepository.UpdateUserAsync(user);
                  await this.tokenRepository.UpdateAsync(token);
                  return result;
              }
            */
            return false;
        }

        public IUserIdentityContext CreateUserIdentityContext(string userId)
        {
            var userExternalId = Guid.Parse(userId);

            if (userExternalId == Guid.Empty)
            {
                throw new ArgumentException();
            }


            Task<User> task = Task.Run<User>(async () => await this.userRepository.GetAsync(userExternalId));
            var user = task.Result;

            var context = new UserIdentityContext
            {
                UserId = user.Id,
                RoleName = user.Role,
                IsActivated = user.IsActivated,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                IsDeleted = user.IsDeleted,
                ModifiedOn = user.ModifiedOn,
                CreatedAt = user.CreatedAt,
            };


            return context;
        }

        public async Task EditUserAsync(UpdateUserCommand command)
        {
            var user = await this.userRepository.GetAsync(command.ExternalId);

            user.LastName = command.LastName;
            user.FirstName = command.FirstName;
            user.EditEmail(command.Email);
            user.EditUsername(command.Username);
            user.EditRole(command.Role);

            await this.userRepository.UpdateUserAsync(user);
        }
    }
}
