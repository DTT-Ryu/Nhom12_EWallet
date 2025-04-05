using Microsoft.EntityFrameworkCore;
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
        public async Task<TblBankAccount> GetBankAccountByIdAsync(int bankAccountId)
        {
            return await _context.TblBankAccounts
                .Include(ba => ba.Bank)
                .FirstOrDefaultAsync(ba => ba.IBankAccountIdPk == bankAccountId);
        }

        public async Task<List<TblBankAccount>> GetBankAccountsByUserIdAsync(int userId)
        {
            return await _context.TblBankAccounts
                .Include(ba => ba.Bank)
                .Where(ba => ba.IUserIdFk == userId && ba.SStatus == "active")
                .ToListAsync();
        }

        public async Task<TblBankAccount> GetBankAccountByAccountNumberAsync(string accountNumber)
        {
            return await _context.TblBankAccounts
                .FirstOrDefaultAsync(ba => ba.SAccountNumber == accountNumber);
        }

        public async Task AddBankAccountAsync(TblBankAccount bankAccount)
        {
            await _context.TblBankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();
        }
    }
}
