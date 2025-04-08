using Microsoft.EntityFrameworkCore;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Respositories
{
    public class BankAccountRepository : Repository<TblBankAccount>, IBankAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public BankAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        //public async Task<TblBankAccount> GetBankAccountByIdAsync(int bankAccountId)
        //{
        //    return await _context.TblBankAccounts
        //        .Include(ba => ba.IAccountIdPk)
        //        .FirstOrDefaultAsync(ba => ba.IBankAccountIdPk == bankAccountId);
        //}

        public async Task<List<TblBankAccount>> GetBankAccountsByUserIdAsync(int userId)
        {
            return await _context.TblBankAccounts
                .Include(ba => ba.IAccountIdPk)
                .Where(ba => ba.IUserIdFk == userId && ba.SStatus == "active")
                .ToListAsync();
        }

        public async Task<TblBankAccount?> GetBankAccountByAccountNumberAsync(string accountNumber)
        {
            return await _context.TblBankAccounts
                .FirstOrDefaultAsync(ba => ba.SAccountNumber == accountNumber);
        }

        public async Task<List<TblBankAccount>> GetAllBankAccount()
        {
            return await _context.TblBankAccounts
                        .Include(u => u.IUserIdFkNavigation)
                        .Include(u => u.SBankIdFkNavigation).ToListAsync();
        }


        public async Task<List<TblUser>> GetListUser()
        {
            return await _context.TblUsers.ToListAsync();
        }

        public async Task<TblBankAccount?> GetBankAccountByID(int id)
        {
            return await _context.TblBankAccounts
                        .Include(u => u.IUserIdFkNavigation)
                        .Include(u => u.SBankIdFkNavigation)
                        .FirstOrDefaultAsync(u => u.IAccountIdPk == id);
        }
        
        public async Task<TblBankAccount?> GetBankAccountByNumber(string n)
        {
            return await _context.TblBankAccounts.FirstOrDefaultAsync(u => u.SAccountNumber == n);
        }

        public async Task<bool> UpdateBankAccount(int accId, int userId, string bankId, string number)
        {
            var a = await _context.TblBankAccounts.FirstOrDefaultAsync(u => u.IAccountIdPk == accId);
            if( a == null)
            {
                return false;
            }

            a.IUserIdFk = userId;
            a.SBankIdFk = bankId;
            a.SAccountNumber = number;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBankAccountStatus(int id)
        {
            var a = await _context.TblBankAccounts.FirstOrDefaultAsync(u => u.IAccountIdPk == id);
            if(a == null)
            {
                return false;
            }

            if (a.SStatus == "active") a.SStatus = "blocked";
            else if (a.SStatus == "blocked") a.SStatus = "active";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
