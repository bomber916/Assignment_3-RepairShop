using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairShop.Data;
using RepairShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RepairShop.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly RepairShopContext _context;

        public ServicesController(RepairShopContext context)
        {
            _context = context;    
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart([Bind("DeviceId,ServiceId,Quantity")] DeviceService ds)
		{
			if (HttpContext.Session.Keys.Count() == 0)
			{
				return RedirectToAction(AuthAction, "Users");
			}
			var cartString = HttpContext.Session.GetString(SessionCart) ?? "";
			List<DeviceService> shoppingCart = JsonConvert.DeserializeObject<List<DeviceService>>(cartString);

			if (shoppingCart == null)
			{
				shoppingCart = new List<DeviceService>();
			}

			ds.DateOrdered = DateTime.Now;

			shoppingCart.Add(ds);

			cartString = JsonConvert.SerializeObject(shoppingCart);
			HttpContext.Session.SetString(SessionCart, cartString);

			return RedirectToAction("Index");
		}

		// GET: Services/Details/5
		public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .SingleOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

			var user = _context.Users
				.Include(c => c.Devices)
				.Single(c => c.Id == 1);

			var userDevices = new List<SelectListItem>();
			foreach ( var item in user.Devices)
			{
				userDevices.Add(
					new SelectListItem
					{
						Text = item.Make + item.Model,
						Value = item.Id.ToString(),
					});
			}
			ViewData["user_devices"] = userDevices;

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Description")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.SingleOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Price,Description")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .SingleOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(m => m.Id == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ServiceExists(long id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
