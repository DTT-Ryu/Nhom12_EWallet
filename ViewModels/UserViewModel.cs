using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Nhom12_EWallet.ViewModels
{
    public class RegisterVM
    {
        //Họ và tên
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        //Số điện thoại
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 số và bắt đầu bằng số 0.")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        //CCCD
        [Required(ErrorMessage = "Căn cước công dân không được để trống.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Căn cước công dân phải có 12 số.")]
        [Display(Name = "Căn cước công dân")]
        public string CCCD { get; set; }


        //Ngày sinh
        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        [AgeValidation(16, ErrorMessage = "Bạn phải đủ 16 tuổi.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime BirthDate { get; set; }

        //Email
        [Required(ErrorMessage = "Email không được để trống.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Mã PIN
        [Required(ErrorMessage = "Mã PIN không được để trống.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Mã pin phải có 6 số.")]
        [DataType (DataType.Password)]
        [Display(Name = "Mã PIN")]
        public string PinCode { get; set; }




        //Mật khẩu
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }


        //Nhập lại mật khẩu
        [Required(ErrorMessage = "Mật khẩu nhập lại không được để trống.")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }

    }


    public class LoginVM
    {
        //Số điện thoại
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 số và bắt đầu bằng số 0.")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }


        //Mật khẩu
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        //[DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

    }

    public class UserManagementVM
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string phoneNumber {  get; set; }
        public string cccd { get; set; }
        public string email { get; set; }
        public decimal balance { get; set; }
        public string role {  get; set; }
        public string status { get; set; }
        
    }

    public class AgeValidationAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public AgeValidationAttribute(int minAge)
        {
            _minAge = minAge;
            ErrorMessage = $"Bạn phải đủ {_minAge} tuổi.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime birthDate = (DateTime)value;
            int age = DateTime.Now.Year - birthDate.Year;

            // Điều chỉnh nếu ngày sinh chưa đến trong năm hiện tại
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;

            return age >= _minAge;
        }
    }
}
