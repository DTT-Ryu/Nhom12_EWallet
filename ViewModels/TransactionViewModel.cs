namespace Nhom12_EWallet.ViewModels
{
    public class TransactionViewModel
    {
        public class DepositVM
        {
            public decimal Amount { get; set; }
            public int BankAccountId { get; set; }
            public string PinCode { get; set; }
            public string Description { get; set; }
        }
        public class AddBankAccountVM
        {
            public string BankId { get; set; }
            public string AccountNumber { get; set; }
        }
        public class WithdrawVM
        {
            public decimal Amount { get; set; } // Số tiền rút
            public int BankAccountId { get; set; } // ID tài khoản ngân hàng
            public string PinCode { get; set; } // Mã PIN
            public string Description { get; set; } // Mô tả giao dịch
        }
    }
}
