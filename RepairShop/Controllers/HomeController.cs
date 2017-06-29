using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RepairShop.Data;
using RepairShop.Models;
using Newtonsoft.Json;
using RepairShop.Models.ViewModels;

namespace RepairShop.Controllers
{
    public class HomeController : BaseController
    {
		private readonly RepairShopContext _context;

		public HomeController(RepairShopContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }

		public PartialViewResult SessionData()
		{
			var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKeyUserId) ?? "0");
			User user = null;
			if (userId != 0)
			{
				user = _context.Users.Single(u => u.Id == userId); 
			}

			var cartString = HttpContext.Session.GetString(SessionCart);
			List<DeviceService> shoppingCart = JsonConvert.DeserializeObject<List<DeviceService>>(cartString);
			SessionDataViewModel vm = new SessionDataViewModel { User = user, ShoppingCart = shoppingCart };
			return PartialView(vm);
		}
    }
}
