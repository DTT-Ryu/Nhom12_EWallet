using Nhom12_EWallet.Models;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Service.Interfaces
{
    public interface IBankAccountService
    {
        Task<List<BankAccountManagementVM>> GetAllBankAccount();

        Task<List<TblBank>> GetListBank();

        Task<List<TblUser>> GetListUser();

        Task<BankAccountManagementVM?> GetBankAccountByID(int id);

        Task<BankAccountManagementVM?> GetBankAccountByNumber(string number);

        Task<bool> UpdateBankAccount(BankAccountManagementVM model);

        Task<bool> UpdateBankAccountStatus(int id);
    }
}
