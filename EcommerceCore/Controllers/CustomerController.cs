using AutoMapper;
using EcommerceCore.Data;
using EcommerceCore.Helpers;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace EcommerceCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public CustomerController(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;//alt enter
        }
        #region Register
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
                try
                {
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
                catch (Exception ex)
                {//
                    var mess = $"{ex.Message} er";
                }
            }
            return View();
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {//nếu chạy đúng login thì login
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        //async Task await
        {//nếu chạy đúng login thì login,ko thì lưu lại
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)//nếu không có
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài khoản này đã bị khóa. Vui lòng liên hệ admin.");

                    }
                    else
                    {
                        //if (khachHang.MatKhau == model.Password.ToMd5Hash(khachHang.RandomKey)) {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {

                            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            //Ghi nhận
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email,khachHang.Email),
                                new Claim(ClaimTypes.Name,khachHang.HoTen),
                                new Claim("CustomerID",khachHang.MaKh),
                                //claim-role động
                                new Claim(ClaimTypes.Role,"Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);

                            }
                            else
                            {
                                return Redirect("/");//Trang chủ
                            }
                        }
                    }
                }
            }

            return View();
        }
        #endregion
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]//phải đăng nhập
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
        
    
