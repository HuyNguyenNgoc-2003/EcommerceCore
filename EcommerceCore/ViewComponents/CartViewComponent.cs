using EcommerceCore.Helpers;
using EcommerceCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceCore.ViewComponents
{
	public class CartViewComponent:ViewComponent//kế thừa
	{
		public IViewComponentResult Invoke()
		{
			var cart =  HttpContext.Session.Get<List<CartItem>>
				(MySetting.CART_KEY)?? new List<CartItem> ();

			return View("CartPanel",new CartModel
			{
				Quantity=cart.Sum(p=>p.SoLuong),
				Total=cart.Sum(p=>p.ThanhTien)
			});//truyền dữ liệu
		}
	}
}
