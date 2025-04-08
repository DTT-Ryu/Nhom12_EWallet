using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.ViewModels;
using static Nhom12_EWallet.ViewModels.TransactionManagementVM;

namespace Nhom12_EWallet.Service.Interfaces
{
    public interface ITransactionService 
    {
        public interface IDepositService
        {
            //Task<(bool Success, string ErrorMessage)> DepositAsync(int userId, DepositVM model);
            //Task<List<TblBankAccount>> GetUserBankAccountsAsync(int userId);
            //Task<List<TblBank>> GetAllBanksAsync();
            //Task<(bool Success, string ErrorMessage)> AddBankAccountAsync(int userId, AddBankAccountVM model);
        }

        Task<List<TransactionManagementVM>> GetAllTransaction();

        Task<TransactionManagementVM?> GetTransactionByID(int id);

        Task<bool> DeleteTransaction(int id);
    }
}
