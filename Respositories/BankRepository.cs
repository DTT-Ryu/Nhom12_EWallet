using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;

namespace Nhom12_EWallet.Respositories
{
    public class BankRepository : Repository<TblBank>, IBankRepository
    {
        private readonly ApplicationDbContext _context;
        public BankRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
