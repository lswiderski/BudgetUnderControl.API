using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using BudgetUnderControl.Common.Enums;
using AutoMapper;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Modules.Users.Domain.Repositories;
using BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser;
using BudgetUnderControl.Shared.Abstractions.Enums;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.UpdateUser;
using BudgetUnderControl.Modules.Users.Application.DTO;
using BudgetUnderControl.Modules.Users.Domain.Enums;
using BudgetUnderControl.Modules.Users.Infrastructure.Clients;
using BudgetUnderControl.Modules.Users.Infrastructure.Clients.Requests;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encryptor;
        private readonly IJwtHandlerService _jwtHandlerService;
        private readonly ITokenRepository _tokenRepository;
        private readonly INotificationsApiClient _notificationsApiClient;
        public UserService(IUserRepository userRepository, IEncrypter encryptor,
            IJwtHandlerService jwtHandlerService,
            IMemoryCache cache,
            IMapper mapper, ITokenRepository tokenRepository, INotificationsApiClient notificationsApiClient)
        {
            this._userRepository = userRepository;
            this._encryptor = encryptor;
            this._jwtHandlerService = jwtHandlerService;
            this._tokenRepository = tokenRepository;
            _notificationsApiClient = notificationsApiClient;
        }

        public async Task<string> ValidateLoginAsync(string username, string password)
        {
            var user = await _userRepository.GetAsync(username);

            if (user == null)
            {
                return string.Empty;
            }

            var hash = _encryptor.GetHash(password, user.Salt);

            if (hash != user.Password)
            {
                return string.Empty;
            }

            var token = _jwtHandlerService.CreateToken(user);

            return token;
        }

        public async Task<string> RegisterUserAsync(CreateNewUserCommand command)
        {
            var salt = _encryptor.GetSalt();

            var hash = _encryptor.GetHash(command.Password, salt);
            var user = User.Create(command.Username, command.FirstName, command.LastName, UserRole.User.GetStringValue(), command.Email, hash, salt);

            await _userRepository.AddUserAsync(user);

            var token = _jwtHandlerService.CreateToken(user);

             var activationToken = Token.Create(TokenType.Activation, user.Id, DateTime.UtcNow.AddDays(1));
            await  _tokenRepository.AddAsync(activationToken);

            await this._notificationsApiClient.CreateActivateUserNotificationAsync(
                new CreateActivateUserNotification(user.Id, user.FirstName, user.LastName, user.Email, activationToken.Code));
            return token;
        }

        public async Task ResetActivationCodeAsync(Guid userId)
        {
            var user = await this._userRepository.GetAsync(userId);

            await this._userRepository.UpdateUserAsync(user);
          
                        await _tokenRepository.DeactivateTokensAsync(TokenType.Activation, user.Id);
                        var activationToken = Token.Create(TokenType.Activation, user.Id, DateTime.UtcNow.AddDays(1));
                        await _tokenRepository.AddAsync(activationToken);
            
                        await this._notificationsApiClient.CreateActivateUserNotificationAsync(
                            new CreateActivateUserNotification(user.Id, user.FirstName, user.LastName, user.Email, activationToken.Code));
        }

        public async Task<bool> ActivateUserAsync(Guid userId, string code)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                return false;
            } 
            var token = await _tokenRepository.GetByCodeAsync(code, TokenType.Activation, user.Id);

            if (token is not {IsValid: true} || token.ValidUntil < DateTime.UtcNow) return false;
            var result = user.Activate();
              token.Devalidate();

              await this._userRepository.UpdateUserAsync(user);
              await this._tokenRepository.UpdateAsync(token);
              return result;

        }

        public IUserIdentityContext CreateUserIdentityContext(string userId)
        {
            var userExternalId = Guid.Parse(userId);

            if (userExternalId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            Task<User> task = Task.Run<User>(async () => await this._userRepository.GetAsync(userExternalId));
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
            var user = await this._userRepository.GetAsync(command.ExternalId);

            user.LastName = command.LastName;
            user.FirstName = command.FirstName;
            user.EditEmail(command.Email);
            user.EditUsername(command.Username);
            user.EditRole(command.Role);

            await this._userRepository.UpdateUserAsync(user);
        }
    }
}
