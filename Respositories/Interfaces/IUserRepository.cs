using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IUserRepository : IRepository<TblUser>
    {
        Task<TblUser>? GetUserByPhoneNumber(string pNumber);
        Task<TblUser>? GetUserByCCCD(string sCCCD);

        Task<TblUser>? GetUserByEmail(string sEmail);
        //void AddUser(TblUser user);
    }
}
