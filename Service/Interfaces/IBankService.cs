using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Service.Interfaces
{
    public interface IBankService
    {
        //For BankManagement
        Task<TblBank> GetBankByID(string id);

        Task<TblBank> GetBankByName(string name);

        Task<List<TblBank>> GetAllBank();

        Task CreateBank(TblBank bank, IFormFile ImageFile);

        Task UpdateBank(TblBank bank, IFormFile? ImageFile);
        Task<bool> UpdateBankDeleted(string id);
    }
}
