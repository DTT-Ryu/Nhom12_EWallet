using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public TblUser? GetUserByPhoneNumber(string pNumber)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SPhoneNumber == pNumber);
        }

        public TblUser? GetUserByCCCD(string sCCCD)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SCccd == sCCCD);
        }

        public TblUser? GetUserByEmail(string sEmail)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SEmail == sEmail);
        }
        public void AddUser(TblUser user)
        {
            _context.TblUsers.Add(user); //Thêm người dùng vào DbSet
            _context.SaveChanges(); //Lưu vào db
        }
    }
}
