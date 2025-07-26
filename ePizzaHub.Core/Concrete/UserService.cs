using AutoMapper;
using ePizzaHub.Core.Contracts;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        //Business use cases needs to write over here
        public async Task<bool> CreateUserRequestAsync(CreateUserRequest createUserRequest)
        {
            //1. Insert records in User table, UserRoles tables
            //2. Hash the Password sending my end user

            var roleDetails = _roleRepository.GetAll().Where(x => x.Name == "User").FirstOrDefault();
            if (roleDetails != null) 
            {
                var user = _mapper.Map<User>(createUserRequest);
                user.Roles.Add(roleDetails);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _userRepository.AddAsync(user);
                int rowsInserted = await _userRepository.CommittAsync();
                return rowsInserted > 0;
            };  
            return false;
        }
    }
}
