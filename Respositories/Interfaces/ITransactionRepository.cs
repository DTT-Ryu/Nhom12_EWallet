using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(TblTransaction transaction);
    }
}
