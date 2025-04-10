using Nhom12_EWallet.Models;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IBankAccountRepository : IRepository<TblBankAccount>
    {
        //Task<TblBankAccount> GetBankAccountByIdAsync(int bankAccountId);
        Task<List<TblBankAccount>> GetBankAccountsByUserIdAsync(int userId);
        Task<TblBankAccount> GetBankAccountByAccountNumberAsync(string accountNumber);

        Task<List<TblBankAccount>> GetAllBankAccount();


        Task<List<TblUser>> GetListUser();

        Task<TblBankAccount> GetBankAccountByID(int id);

        Task<TblBankAccount?> GetBankAccountByNumber(string n);

        Task<bool> UpdateBankAccount(int accId, int userId, string bankId, string number);

        Task<bool> UpdateBankAccountStatus(int id);
    }
}
