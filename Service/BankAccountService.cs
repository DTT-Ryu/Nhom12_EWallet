using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Service
{
    public class BankAccountService : IBankAccountService
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankRepository _bankRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IBankRepository bankRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankRepository = bankRepository;
        }

        
        public async Task<List<BankAccountManagementVM>> GetAllBankAccount()
        {
            var accounts = await _bankAccountRepository.GetAllBankAccount();

            return accounts.Select(u => new BankAccountManagementVM
            {
                AccountId = u.IAccountIdPk,
                UserId = u.IUserIdFk,
                UserName = u.IUserIdFkNavigation?.SFullName ?? "Unknown",
                UserPhoneNumber = u.IUserIdFkNavigation?.SPhoneNumber ?? "Unknown",
                BankId = u.SBankIdFk,
                BankName = u.SBankIdFkNavigation?.SBankName ?? "Unknown",
                AccountNumber = u.SAccountNumber,
                Status = u.SStatus,
                Deleted = u.Deleted,
            }).ToList();
        }
        
        public async Task<BankAccountManagementVM?> GetBankAccountByID(int id)
        {
            var u = await _bankAccountRepository.GetBankAccountByID(id);
            if (u == null)
                return null;
            return  new BankAccountManagementVM
            {
                AccountId = u.IAccountIdPk,
                UserId = u.IUserIdFk,
                UserName = u.IUserIdFkNavigation?.SFullName ?? "Unknown",
                UserPhoneNumber = u.IUserIdFkNavigation?.SPhoneNumber ?? "Unknown",
                BankId = u.SBankIdFk,
                BankName = u.SBankIdFkNavigation?.SBankName ?? "Unknown",
                AccountNumber = u.SAccountNumber,
                Status = u.SStatus,
                Deleted = u.Deleted,
            };
        }
        
        public async Task<List<TblUser>> GetListUser()
        {
            return await _bankAccountRepository.GetListUser();
        }

        public async Task<List<TblBank>> GetListBank()
        {
            return await _bankRepository.GetAll();
        }

        public async Task<BankAccountManagementVM?> GetBankAccountByNumber(string n)
        {
            var u = await _bankAccountRepository.GetBankAccountByNumber(n);
            if (u == null)
                return null;
            return new BankAccountManagementVM
            {
                AccountId = u.IAccountIdPk,
                UserId = u.IUserIdFk,
                UserName = u.IUserIdFkNavigation?.SFullName ?? "Unknown",
                UserPhoneNumber = u.IUserIdFkNavigation?.SPhoneNumber ?? "Unknown",
                BankId = u.SBankIdFk,
                BankName = u.SBankIdFkNavigation?.SBankName ?? "Unknown",
                AccountNumber = u.SAccountNumber,
                Status = u.SStatus,
                Deleted = u.Deleted,
            };
        }

        public async Task<bool> UpdateBankAccount(BankAccountManagementVM model)
        {
            return await _bankAccountRepository.UpdateBankAccount(model.AccountId, model.UserId, model.BankId, model.AccountNumber);
        }

        public async Task<bool> UpdateBankAccountStatus(int id)
        {
            return await _bankAccountRepository.UpdateBankAccountStatus(id);
        }
    }
}
