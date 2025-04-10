﻿using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<TblUser>? Login(string phoneNumber, string password)
        {
            var user = await _userRepository.GetUserByPhoneNumber(phoneNumber);
            if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.SPasswordHash))
            {
                return null;
            }

            if(user.SStatus == "blocked")
            {
                throw new Exception("Tài khoãn đã bị khóa!");
            }
            return user;

        }

        public void SetUserSession(HttpContext httpContext, TblUser user)
        {
            httpContext.Session.SetInt32("UserId", user.IUserIdPk);
            httpContext.Session.SetString("UserName", user.SFullName);
            httpContext.Session.SetInt32("UserRole", user.IRoleIdFk);

            Console.WriteLine("Session UserId: " + httpContext.Session.GetInt32("UserId"));
            Console.WriteLine("Session UserName: " + httpContext.Session.GetString("UserName"));
        }

        public async Task Logout(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }

        public async Task<ResultService> Register(RegisterVM model)
        {
            var result  = new ResultService();
            if(model == null)
            {
                result.Success = false;
                result.Error.Add("Model", "Dữ liệu không hợp lệ!");
                return result;
            }

            //check sđt
            if(await _userRepository.GetUserByPhoneNumber(model.PhoneNumber) != null)
            {
                result.Error.Add("PhoneNumber", "Số điện thoại đã tồn tại!");
            }

            //check cccd
            if(await _userRepository.GetUserByCCCD(model.CCCD) != null)
            {
                result.Error.Add("CCCD", "Căn cước công dân đã tồn tại");
            }

            //check email

            if(await _userRepository.GetUserByEmail(model.Email) != null)
            {
                result.Error.Add("Email", "Email đã tồn tại!");
            }

            if(result.Error.Count > 0)
            {
                result.Success = false;
                return result;
            }

            string hashedPass = BCrypt.Net.BCrypt.HashPassword(model.Password);
            string hashedPin = BCrypt.Net.BCrypt.HashPassword(model.PinCode);
            var newUser = new TblUser
            {
                SFullName = model.FullName,
                SPhoneNumber = model.PhoneNumber,
                SCccd = model.CCCD,
                DBirthDate = DateOnly.FromDateTime(model.BirthDate),
                SEmail = model.Email,
                FBalance = 0,
                SPasswordHash = hashedPass,
                SPinCode = hashedPin,
                IRoleIdFk = 2,
                DCreatedAt = DateTime.Now,
                DUpdatedAt = null,
            };
            await _userRepository.Add(newUser);
            result.Success = true;
            return result;
        }


        public async Task<IEnumerable<UserManagementVM>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersWithRoleAsync();

            return users.Select(u => new UserManagementVM
            {
                id = u.IUserIdPk,
                fullName = u.SFullName,
                phoneNumber = u.SPhoneNumber,
                cccd = u.SCccd,
                email = u.SEmail,
                balance = u.FBalance ?? 0,
                birthDate = u.DBirthDate,
                createdAt = u.DCreatedAt, //?? DateTime.MinValue;
                updatedAt = u.DUpdatedAt,
                roleId = u.IRoleIdFk,
                role = u.IRoleIdFkNavigation?.SRoleName ?? "Chưa xác định",
                status = u.SStatus
            }).ToList();
        }

        public async Task<UserManagementVM> GetUserByID(int id)
        {
            var u = await _userRepository.GetUserWithRoleById(id);
            if (u == null) return null;

            var model = new UserManagementVM
            {
                id = u.IUserIdPk,
                fullName = u.SFullName,
                phoneNumber = u.SPhoneNumber,
                cccd = u.SCccd,
                email = u.SEmail,
                balance = u.FBalance ?? 0,
                birthDate = u.DBirthDate,
                createdAt = u.DCreatedAt, //?? DateTime.MinValue;
                updatedAt = u.DUpdatedAt,
                roleId = u.IRoleIdFk,
                role = u.IRoleIdFkNavigation != null ? u.IRoleIdFkNavigation.SRoleName : "Chưa xác định",
                status = u.SStatus
            };

            return model;
        }

        public async Task<List<TblRole>> GetListRole()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<bool> UpdateUserRole (int userId, byte roleId)
        {
            bool result = await _userRepository.UpdateUserRole(userId, roleId);
            return result;
        }

        public async Task<bool> UpdateUserStatus(int userId)
        {
            return await _userRepository.UpdateUserStatus(userId);
        }


        public async Task<bool> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            if (result == null) return true;
            return false;
        }

        public async Task<UserManagementVM> UpdateUserInfor(int id, UserManagementVM model)
        {
            var user = await _userRepository.GetUserWithRoleById(id);

            if (user == null) return null;

            user.SFullName = model.fullName;
            user.SEmail = model.email;
            user.DBirthDate = model.birthDate;
            user.DUpdatedAt = DateTime.Now;

            await _userRepository.Update(user);
            return new UserManagementVM
            {
                id = user.IUserIdPk,
                fullName = user.SFullName,
                phoneNumber = user.SPhoneNumber,
                cccd = user.SCccd,
                email = user.SEmail,
                birthDate = user.DBirthDate,
                balance = user.FBalance ?? 0m,
            };
        }
    }
}
