using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IBankRepository : IRepository<TblBank>
    {
        Task<List<TblBank>> GetAllBanksAsync();
        Task<TblBank> GetBankByIdAsync(string bankId);

        //For BankManagement
        Task<TblBank> GetBankByID(string id);

        Task<TblBank> GetBankByName(string name);

        Task<bool> UpdateBankDeleted(string id);
    }
}
