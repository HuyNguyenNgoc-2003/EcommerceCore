using EcommerceCore.Data;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceCore.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly EcommerceContext db;

        public MenuLoaiViewComponent(EcommerceContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
               MaLoai = lo.MaLoai,
               TenLoai = lo.TenLoai,
               SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View(data);//default.cshtml
        }
    }
}
