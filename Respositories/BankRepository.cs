using Microsoft.EntityFrameworkCore;
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
        public async Task<List<TblBank>> GetAllBanksAsync()
        {
            return await _context.TblBanks.ToListAsync();
        }

        public async Task<TblBank> GetBankByIdAsync(string bankId)
        {
            return await _context.TblBanks.FirstOrDefaultAsync(b => b.SBankIdPk == bankId);
        }


        public async Task<TblBank> GetBankByID(string id)
        {
            return await _context.TblBanks.FirstOrDefaultAsync(u => u.SBankIdPk == id);
        }

        public async Task<TblBank> GetBankByName(string name)
        {
            return await _context.TblBanks.FirstOrDefaultAsync(u => u.SBankName == name);
        }

        public async Task<bool> UpdateBankDeleted(string id)
        {
            var bank = await _context.TblBanks.FirstOrDefaultAsync(u => u.SBankIdPk == id);

            if (bank == null)
            {
                return false;
            }

            bank.Deleted = !bank.Deleted;
            //_context.TblBanks.Update(bank);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
