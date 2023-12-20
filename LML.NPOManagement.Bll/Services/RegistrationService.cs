using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IInvestorRepository _investorRepository;
        private readonly IUserRepository _userRepository;

        public RegistrationService(IUserRepository userRepository, IInvestorRepository investorRepository)
        {
            _investorRepository = investorRepository;
            _userRepository = userRepository;
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.GetUserByEmail(userModel.Email);
            if (user != null && BC.Verify(userModel.Password, user.Password))
            {
                user.Password = null;
                user.Token = TokenCreationHelper.GenerateJwtToken(user, configuration);
                return user;
            }
            return null;
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.GetUserByEmail(userModel.Email);

            if (user == null)
            {
                userModel.Password = BC.HashPassword(userModel.Password);
               await _userRepository.AddUser(userModel);


                var newUser = await _userRepository.GetUserByEmail(userModel.Email);             
                newUser.Token = TokenCreationHelper.GenerateJwtToken(newUser, configuration);
                newUser.Password = null;
                return newUser;
            }
            else if (user.Status == StatusEnumModel.Closed)
            {
                return null;//to do handle this condition differently
            }
            return null;
        }

        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration)
        {
           
            await _userRepository.AddUserInformation(userInformationModel);
            
            if (userInformationModel.UserTypeEnum == UserTypeEnum.Investor)
            {
               await _investorRepository.AddInvestor(userInformationModel);
            }
            return userInformationModel.Id;
        }
    }
}
