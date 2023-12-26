using System.ComponentModel.DataAnnotations;
namespace EcommerceCore.ViewModels
{//ko lấy trực ti6p1 entity model mà định nghĩa ra ViewModel
    public class RegisterVM
    {
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage="Tối đa 20 ký tự")]
        public string MaKh { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;//default

        [Display(Name ="Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [RegularExpression(@"0[9875]\d{8}",ErrorMessage ="Chưa đúng định dạng")]
        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        public string Email { get; set; } 

        public string? Hinh { get; set; }
    }
}
