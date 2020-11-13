using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.CommonInfrastructure.Commands;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.CommonInfrastructure;
using FluentValidation;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Common.Extensions;
using BudgetUnderControl.ApiInfrastructure.Services.EmailService;

namespace BudgetUnderControl.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtHandlerService jwtHandlerService;
        private readonly IMemoryCache cache;
        private readonly IValidator<RegisterUserCommand> registerUserValidator;
        private readonly INotificationService notificationService;
        public UserService(IUserRepository userRepository, IEncrypter encrypter, 
            IJwtHandlerService jwtHandlerService, 
            IMemoryCache cache,
            IValidator<RegisterUserCommand> registerUserValidator,
            INotificationService notificationService)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.jwtHandlerService = jwtHandlerService;
            this.cache = cache;
            this.registerUserValidator = registerUserValidator;
            this.notificationService = notificationService;
        }

        public async Task ValidateLoginAsync(MobileLoginCommand command)
        {
            var user = await userRepository.GetAsync(command.Username);

            if(user == null)
            {
                return;
            }

            var hash = encrypter.GetHash(command.Password, user.Salt);

            if(hash != user.Password)
            {
                return;
            }

            var token = jwtHandlerService.CreateToken(user);
            cache.Set(command.TokenId, token);
        }

        public async Task RegisterUserAsync(RegisterUserCommand command)
        {
            var validationResult = registerUserValidator.Validate(command);
            if(validationResult.IsValid)
            {
                var salt = encrypter.GetSalt();

                var hash = encrypter.GetHash(command.Password, salt);
                var user = User.Create(command.Username, command.FirstName, command.LastName, UserRole.User.GetStringValue(), command.Email, hash, salt);

                await userRepository.AddUserAsync(user);

                var token = jwtHandlerService.CreateToken(user);
                cache.Set(command.TokenId, token);

                await this.notificationService.SendRegisterNotificationAsync(user.ExternalId);
            }
        }

        public async Task<bool> ActivateUserAsync(ActivateUserCommand command)
        {
            var user = await userRepository.GetAsync(command.Username);

            if (user == null)
            {
                user = await userRepository.GetByEmailAsync(command.Email);
            }

            if(user == null)
            {
                return false;
            }

            return user.Active(command.Code);
            
        }

        public IUserIdentityContext CreateUserIdentityContext(string userId)
        {
            var userExternalId = Guid.Parse(userId);

            if(userExternalId == Guid.Empty)
            {
                throw new ArgumentException();
            }


            Task<User> task = Task.Run<User>(async () => await this.userRepository.GetAsync(userExternalId));
            var user = task.Result;

            var context = new UserIdentityContext
            {
                UserId = user.Id,
                ExternalId = user.ExternalId,
                RoleName = user.Role,
                IsActivated = user.IsActivated,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return context;
        }

    }
}
