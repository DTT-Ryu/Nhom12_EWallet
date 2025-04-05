using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;

namespace Nhom12_EWallet.Respositories
{
    public class BankAccountRepository : Repository<TblBankAccount>, IBankAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public BankAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
