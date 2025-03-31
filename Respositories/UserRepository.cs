using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) { 
            _context = context;
        }
        public TblUser? GetUserByPhoneNumber(string pNumber)
        {
            return _context.TblUsers.FirstOrDefault(u => u.SPhoneNumber == pNumber);
        }
    }
}
