using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<TblBankAccount> GetBankAccountByIdAsync(int bankAccountId);
        Task<List<TblBankAccount>> GetBankAccountsByUserIdAsync(int userId);
        Task<TblBankAccount> GetBankAccountByAccountNumberAsync(string accountNumber);
        Task AddBankAccountAsync(TblBankAccount bankAccount);
    }
}
