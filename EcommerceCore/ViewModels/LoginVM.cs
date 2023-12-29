using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Mời nhập tên đăng nhập")]
        [Required(ErrorMessage = "Bạn chưa nhập tên đăng nhập")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 ký tự")]
        public string UserName { get; set; }
        [Display(Name = "Mời nhập mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
