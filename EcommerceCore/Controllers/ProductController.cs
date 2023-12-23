using EcommerceCore.Data;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

    }
}
