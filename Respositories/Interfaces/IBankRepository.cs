using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IBankRepository
    {
        Task<List<TblBank>> GetAllBanksAsync();
        Task<TblBank> GetBankByIdAsync(string bankId);
    }
}
