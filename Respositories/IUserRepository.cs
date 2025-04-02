using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories
{
    public interface IUserRepository
    {
        TblUser? GetUserByPhoneNumber(string pNumber);
        TblUser? GetUserByCCCD(string sCCCD);
        void AddUser(TblUser user);
    }
}
