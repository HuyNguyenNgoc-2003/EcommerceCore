using EcommerceCore.Data;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly EcommerceContext db;

        public ProductController(EcommerceContext context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var proDucts = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                proDucts = proDucts.Where(p => p.MaLoai == loai.Value);
            }
            var result = proDucts.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,//có thể null
                Hinh = p.Hinh ?? "",//rỗng
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);//
        }
        public IActionResult Search(string? query)
        {
            var proDucts = db.HangHoas.AsQueryable();
            if (query != null)
            {
                proDucts = proDucts.Where(p => p.TenHh.Contains(query));
            }
            var result = proDucts.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,//có thể null
                Hinh = p.Hinh ?? "",//rỗng
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var data = db.HangHoas
                .Include(p=>p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if(data==null)
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
                return Redirect("/404");//
            }
            var result = new ChiTietHangHoaVM
            {
                MaHh = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,//
                DiemDanhGia = 5,//
            };
            return View(result);//

        }

    }
}
