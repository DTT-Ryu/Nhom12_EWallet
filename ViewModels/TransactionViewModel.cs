using System.ComponentModel.DataAnnotations;

namespace Nhom12_EWallet.ViewModels
{
    public class DepositWithdrawVM
    {
        public int userId { get; set; }
        public int bankAccId { get; set; }
        public string bankAccNumber { get; set; }

        public decimal ammount { get; set; }
        public string desciption { get; set; }
    }
    //public class DepositVM
    //{

    //    public decimal Amount { get; set; }
    //    public int BankAccountId { get; set; }
    //    public string PinCode { get; set; }
    //    public string Description { get; set; }
    //}
    //public class AddBankAccountVM
    //{
    //    public string BankId { get; set; }
    //    public string AccountNumber { get; set; }
    //}
    //public class WithdrawVM
    //{
    //    public decimal Amount { get; set; } // Số tiền rút
    //    public int BankAccountId { get; set; } // ID tài khoản ngân hàng
    //    public string PinCode { get; set; } // Mã PIN
    //    public string Description { get; set; } // Mô tả giao dịch
    //}


    public class TransactionManagementVM
    {
        public int TransactionId { get; set; }
        public int SenderUserId { get; set; }
        public string? SenderUserPhone { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Description { get; set; }
        public int? RecipientUserId { get; set; }
        public string? RecipientUserPhone { get; set; }
        public string? BankId { get; set; }
        public string? BankAccName { get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }

    }
}
