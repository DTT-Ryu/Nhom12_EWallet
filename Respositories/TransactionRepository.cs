using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;

namespace Nhom12_EWallet.Respositories
{
    public class TransactionRepository : Repository<TblTransaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
