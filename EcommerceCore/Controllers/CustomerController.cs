using AutoMapper;
using EcommerceCore.Data;
using EcommerceCore.Helpers;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public CustomerController(EcommerceContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;//alt enter
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)//
        {
            if (ModelState.IsValid)
            {
                try { 
                //var khachhang = model;
                var khachHang = _mapper.Map<KhachHang>(model);
                khachHang.RandomKey = MyUtil.GenerateRandomKey();//định nghĩa
                khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                khachHang.HieuLuc = true;//sẽ xử lý khi dùng mail active
                khachHang.VaiTro = 0;

                if (Hinh != null)
                {
                    khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                }
                _context.Add(khachHang);
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch(Exception ex){//

            }
        }
            return View();
        }
    }
}
