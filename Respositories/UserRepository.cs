using Microsoft.EntityFrameworkCore;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Respositories
{
    public class UserRepository : Repository<TblUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<TblUser>? GetUserByPhoneNumber(string pNumber)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SPhoneNumber == pNumber);
        }

        public async Task<TblUser>? GetUserByCCCD(string sCCCD)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SCccd == sCCCD);
        }

        public async Task<TblUser> GetUserByEmail(string sEmail)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SEmail == sEmail);
        }
        //public void AddUser(TblUser user)
        //{
        //    _context.TblUsers.Add(user); //Thêm người dùng vào DbSet
        //    _context.SaveChanges(); //Lưu vào db
        //}

        public async Task<List<UserManagementVM>> GetAllUsersWithRoleAsync()
        {
            return await _context.TblUsers
                .Include(u => u.IRoleIdFkNavigation) // Lấy dữ liệu từ bảng Role
                .Select(u => new UserManagementVM
                {
                    id = u.IUserIdPk,
                    fullName = u.SFullName,
                    phoneNumber = u.SPhoneNumber,
                    cccd = u.SCccd,
                    email = u.SEmail,
                    balance = u.FBalance ?? 0,
                    roleId = u.IRoleIdFk,
                    role = u.IRoleIdFkNavigation != null ? u.IRoleIdFkNavigation.SRoleName : "Chưa xác định",
                    status = u.SStatus
                    //status = u.SStatus == "active" ? "Hoạt động" : "Khóa"
                })
                .ToListAsync();
        }

        public async Task<TblUser?> GetUserWithRoleById(int id)
        {
            return await _context.TblUsers
                                 .Include(u => u.IRoleIdFkNavigation) // Bao gồm thông tin Role
                                 .FirstOrDefaultAsync(u => u.IUserIdPk == id); // So sánh trực tiếp với khóa ngoại
        }



        public async Task<List<TblRole>> GetAllRolesAsync()
        {
            return await _context.TblRoles.ToListAsync();
        }

        public async Task<bool> UpdateUserRole(int userId, byte roleId)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.IUserIdPk == userId);
            if (user == null) return false;
            
            user.IRoleIdFk = roleId;
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> UpdateUserStatus(int userid)
        {
            var user =await _context.TblUsers.FirstOrDefaultAsync(u => u.IUserIdPk == userid);
            if (user == null) return false;
            
            if(user.SStatus == "active")
            {
                user.SStatus = "blocked";
            }
            else
            {
                user.SStatus = "active";
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
