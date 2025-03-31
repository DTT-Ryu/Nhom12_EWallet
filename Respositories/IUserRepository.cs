using Nhom12_EWallet.Models;

namespace Nhom12_EWallet.Respositories
{
    public interface IUserRepository
    {
        TblUser? GetUserByPhoneNumber(string pNumber);
    }
}
