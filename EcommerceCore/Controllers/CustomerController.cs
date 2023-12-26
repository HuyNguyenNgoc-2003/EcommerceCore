using EcommerceCore.Data;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceContext _context;
        public CustomerController(EcommerceContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var khachhang = model;
            }
            return View();
        }
    }
}
