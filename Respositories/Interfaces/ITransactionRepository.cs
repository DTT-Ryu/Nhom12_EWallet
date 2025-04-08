using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface ITransactionRepository : IRepository<TblTransaction>
    {
        Task AddTransactionAsync(TblTransaction transaction);

        Task<List<TblTransaction>> GetAllTransaction();
        Task<TblTransaction?> GetTransactionByID(int id);
    }
}
