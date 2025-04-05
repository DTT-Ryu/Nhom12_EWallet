using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;
using static Nhom12_EWallet.Service.TransactionService;
using static Nhom12_EWallet.ViewModels.TransactionViewModel;

namespace Nhom12_EWallet.Service
{
    public class TransactionService
    {
        public class WalletService : ITransactionService
        {
            private readonly IUserRepository _userRepository;
            private readonly IBankAccountRepository _bankAccountRepository;
            private readonly ITransactionRepository _transactionRepository;
            private readonly IBankRepository _bankRepository;

            public WalletService(IUserRepository userRepository, IBankAccountRepository bankAccountRepository, ITransactionRepository transactionRepository, IBankRepository bankRepository)
            {
                _userRepository = userRepository;
                _bankAccountRepository = bankAccountRepository;
                _transactionRepository = transactionRepository;
                _bankRepository = bankRepository;
            }

            //public async Task<(bool Success, string ErrorMessage)> DepositAsync(int userId, DepositVM model)
            //{
            //    if (model.Amount <= 0)
            //    {
            //        return (false, "Số tiền nạp phải lớn hơn 0.");
            //    }

            //    var user = await _userRepository.GetById(userId);
            //    if (user == null)
            //    {
            //        return (false, "Người dùng không tồn tại.");
            //    }

            //    if (!BCrypt.Net.BCrypt.Verify(model.PinCode, user.SPinCode))
            //    {
            //        return (false, "Mã PIN không đúng.");
            //    }

            //    var bankAccount = await _bankAccountRepository.GetBankAccountByIdAsync(model.BankAccountId);
            //    if (bankAccount == null || bankAccount.IUserIdFk != userId)
            //    {
            //        return (false, "Tài khoản ngân hàng không hợp lệ.");
            //    }

            //    if (bankAccount.SStatus != "active")
            //    {
            //        return (false, "Tài khoản ngân hàng không hoạt động.");
            //    }

            //    user.FBalance += model.Amount;
            //    _userRepository.Update(user);

            //    var transaction = new TblTransaction
            //    {
            //        ISenderUserIdFk = userId,
            //        STransactionType = "deposit",
            //        FAmount = model.Amount,
            //        IBankAccountIdFk = model.BankAccountId,
            //        SDescription = model.Description ?? $"Nạp tiền từ {bankAccount.SBankIdFk}",
            //        SStatus = "completed",
            //        DCreatedAt = DateTime.Now
            //    };
            //    await _transactionRepository.AddTransactionAsync(transaction);

            //    return (true, string.Empty);
            //}

            //public async Task<List<TblBankAccount>> GetUserBankAccountsAsync(int userId)
            //{
            //    return await _bankAccountRepository.GetBankAccountsByUserIdAsync(userId);
            //}

            //public async Task<List<TblBank>> GetAllBanksAsync()
            //{
            //    return await _bankRepository.GetAllBanksAsync();
            //}

            //public async Task<(bool Success, string ErrorMessage)> AddBankAccountAsync(int userId, AddBankAccountVM model)
            //{
            //    var bank = await _bankRepository.GetBankByIdAsync(model.BankId);
            //    if (bank == null)
            //    {
            //        return (false, "Ngân hàng không tồn tại.");
            //    }

            //    var existingAccount = await _bankAccountRepository.GetBankAccountByAccountNumberAsync(model.AccountNumber);
            //    if (existingAccount != null)
            //    {
            //        return (false, "Số tài khoản đã được liên kết.");
            //    }

            //    var bankAccount = new TblBankAccount
            //    {
            //        IUserIdFk = userId,
            //        SBankIdFk = model.BankId,
            //        SAccountNumber = model.AccountNumber,
            //        SStatus = "active"
            //    };
            //    await _bankAccountRepository.AddBankAccountAsync(bankAccount);

            //    return (true, string.Empty);
            //}
        }
    }
}

