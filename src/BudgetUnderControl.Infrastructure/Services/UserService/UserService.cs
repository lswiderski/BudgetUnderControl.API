﻿using BudgetUnderControl.Domain.Repositiories;
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
using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using AutoMapper;
using BudgetUnderControl.Common.Contracts.User;
using BudgetUnderControl.CommonInfrastructure.Commands.User;

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
        private readonly IMapper mapper;
        private readonly IValidator<EditUser> editUserValidator;
        private readonly ITokenRepository tokenRepository;
        public UserService(IUserRepository userRepository, IEncrypter encrypter,
            IJwtHandlerService jwtHandlerService,
            IMemoryCache cache,
            IValidator<RegisterUserCommand> registerUserValidator,
            INotificationService notificationService,
            IMapper mapper,
            IValidator<EditUser> editUserValidator,
            ITokenRepository tokenRepository)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.jwtHandlerService = jwtHandlerService;
            this.cache = cache;
            this.registerUserValidator = registerUserValidator;
            this.notificationService = notificationService;
            this.mapper = mapper;
            this.editUserValidator = editUserValidator;
            this.tokenRepository = tokenRepository;
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

                var activationToken = Token.Create(TokenType.Activation, user.ExternalId, user.Id, DateTime.UtcNow.AddDays(1));
                await tokenRepository.AddAsync(activationToken);
                

                await this.notificationService.SendRegisterNotificationAsync(this.mapper.Map<UserDTO>(user), activationToken.Code);
            }
        }

        public async Task ResetActivationCodeAsync(Guid userId)
        {
            var user = await this.userRepository.GetAsync(userId);

            await this.userRepository.UpdateUserAsync(user);

            await tokenRepository.DeactivateTokensAsync(TokenType.Activation, user.ExternalId);
            var activationToken = Token.Create(TokenType.Activation, user.ExternalId, user.Id, DateTime.UtcNow.AddDays(1));
            await tokenRepository.AddAsync(activationToken);

            await this.notificationService.SendRegisterNotificationAsync(this.mapper.Map<UserDTO>(user), activationToken.Code);
        }

        public async Task<bool> ActivateUserAsync(ActivateUserCommand command)
        {
            var user = await userRepository.GetAsync(command.UserId);

            if(user == null)
            {
                return false;
            }
            var token = await tokenRepository.GetByCodeAsync(command.Code, TokenType.Activation, user.ExternalId);

            if(token != null && token.IsValid && token.ValidUntil >= DateTime.UtcNow)
            {
                var result = user.Activate();
                token.Devalidate();

                await this.userRepository.UpdateUserAsync(user);
                await this.tokenRepository.UpdateAsync(token);
                return result;
            }

            return false;
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
           /* var context = new UserIdentityContext
            {
                UserId = 1,
                ExternalId = new Guid("10000000-0000-0000-0000-000000000001"),
                RoleName = "User",
                IsActivated = true,
                FirstName = "Demo",
                LastName = "Demo",
                Email = "Demo"
            };*/

            return context;
        }

        public async Task EditUserAsync(EditUser command)
        {
            var validationResult = this.editUserValidator.Validate(command);
            if(validationResult.IsValid)
            {
                var user = await this.userRepository.GetAsync(command.ExternalId);

                user.LastName = command.LastName;
                user.FirstName = command.FirstName;
                user.EditEmail(command.Email);
                user.EditUsername(command.Username);
                user.EditRole(command.Role);

                this.userRepository.UpdateUserAsync(user);
            }
        }


    }
}
