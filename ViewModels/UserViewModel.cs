using System.ComponentModel.DataAnnotations;

namespace Nhom12_EWallet.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 số và bắt đầu bằng số 0.")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        //[MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        //    ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
