using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepairShop.Controllers;
using RepairShop.Data;
using RepairShop.Models;
using RepairShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.ViewComponents
{
    public class SessionDataViewComponent : ViewComponent
    {
		protected const string SessionKeyUserName = "_UserName";
		protected const string SessionKeyUserId = "_UserId";
		protected const string SessionCart = "_Cart";

		protected const string AuthController = nameof(UsersController);
		protected const string AuthAction = nameof(UsersController.Login);

		private readonly RepairShopContext _context;
		public SessionDataViewComponent(RepairShopContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKeyUserId) ?? "0");
			User user = null;
			if (userId != 0)
			{
				user = _context.Users.Single(u => u.Id == userId);
			}

			var cartString = HttpContext.Session.GetString(SessionCart) ?? "";
			List<DeviceService> shoppingCart = JsonConvert.DeserializeObject<List<DeviceService>>(cartString);
			if (shoppingCart != null)
			{
				foreach (var item in shoppingCart)
				{
					item.Service = _context.Services.SingleOrDefault(s => s.Id == item.ServiceId);
					item.Device = _context.Devices.SingleOrDefault(d => d.Id == item.DeviceId);
				} 
			}
			SessionDataViewModel vm = new SessionDataViewModel { User = user, ShoppingCart = shoppingCart };
			return View(vm);
		}
	}
}
