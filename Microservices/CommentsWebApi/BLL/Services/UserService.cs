using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Security;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<bool> Login(UserModel userModel)
        {
            var user = await _userRepository.GetUserAsync(_mapper.Map<User>(userModel));

            if (user != null && PasswordHasher.VerifyPassword(userModel.Password, user.Password))
            {
                return true;
            }

            return false;
        }


        public async Task Register(UserModel userModel)
        {
            userModel.Password = PasswordHasher.HashPassword(userModel.Password);
            userModel.UserName = string
                    .Join(" ", userModel.UserName.Split(' ')
                    .ToList()
                    .ConvertAll(word => word.Substring(0, 1).ToUpper() + word.Substring(1)));

            var mappedUser = _mapper.Map<User>(userModel);

            await _userRepository.AddAsync(mappedUser);
        }
    }
}
