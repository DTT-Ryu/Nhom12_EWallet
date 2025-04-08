namespace Nhom12_EWallet.ViewModels
{
    public class BankAccountManagementVM
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }  // Họ tên từ tblUsers
        public string UserPhoneNumber { get; set; } // Số điện thoại từ tblUsers
        public string BankId { get; set; }
        public string BankName { get; set; } // Tên ngân hàng từ tblBanks
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }
    }
}
