﻿using ePizzaHub.Core.Contracts;
using ePizzaHub.Models.ApiModels.Response;
using ePizzaHub.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ValidateUserResponse> ValidateUserAsync(string username, string password)
        {
            var userDetails = await _userRepository.FindByUserNameAsync(username);
            if (userDetails is null)
                throw new Exception($"The user with Email Address {username} does not exists.");

            bool IsValidPassword = BCrypt.Net.BCrypt.Verify(password, userDetails.Password);
            if (!IsValidPassword)
                throw new Exception($"Invalid credentials passed for an Email Address : {username}.");

            return new ValidateUserResponse
            {
                Email = userDetails.Email,
                Name = userDetails.Name,
                UserId  =  userDetails.Id,
                Roles = userDetails.Roles.Select(x => x.Name).ToList()

            };
        }
    }
}
