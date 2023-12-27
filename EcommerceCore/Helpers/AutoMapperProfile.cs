using AutoMapper;
using EcommerceCore.Data;
using EcommerceCore.ViewModels;

namespace EcommerceCore.Helpers
{
    public class AutoMapperProfile:Profile//kế thừa
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();//cùng tên tự động map qua
            //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            //.ReverseMap();
            
        }
    }
}
