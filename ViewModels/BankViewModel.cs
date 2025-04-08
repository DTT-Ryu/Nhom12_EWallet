using System.ComponentModel.DataAnnotations;

namespace Nhom12_EWallet.ViewModels
{
    public class BankViewModel
    {
        [Required(ErrorMessage = "Mã ngân hàng không được để trống")]
        [Display(Name = "Mã ngân hàng")]
        public string bankId { get; set; }

        [Required(ErrorMessage = "Tên ngân hàng không được để trống")]
        [Display(Name = "Tên ngân hàng")]
        public string bankName { get; set; }

        [Required(ErrorMessage = "Ảnh không được để trống")]
        [Display (Name = "Ảnh")]
        public string bankImg {  get; set; }

        public int deleted { get; set; }
    }
}
