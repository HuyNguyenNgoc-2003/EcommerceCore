using Microsoft.AspNetCore.Mvc;

namespace EcommerceCore.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int? loai)
        {
            return View();
        }
    }
}
