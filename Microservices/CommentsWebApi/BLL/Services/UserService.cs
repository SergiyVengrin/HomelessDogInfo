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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Login(UserModel userModel)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserAsync(_mapper.Map<User>(userModel));

                if (user != null && PasswordHasher.VerifyPassword(userModel.Password, user.Password))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

            try
            {
                await _unitOfWork.UserRepository.AddAsync(mappedUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
    }
}
