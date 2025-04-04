using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories
{
    public interface IUserRepository : IRepository<TblUser>
    {
        TblUser? GetUserByPhoneNumber(string pNumber);
        TblUser? GetUserByCCCD(string sCCCD);

        TblUser? GetUserByEmail(string sEmail);
        //void AddUser(TblUser user);
    }
}
