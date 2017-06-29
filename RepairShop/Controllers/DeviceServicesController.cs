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
	public class DeviceServicesController : BaseController
	{
		private readonly RepairShopContext _context;

		public DeviceServicesController(RepairShopContext context)
		{
			_context = context;
		}

		// GET: DeviceServices
		public async Task<IActionResult> Index()
		{
			var repairShopContext = _context.DeviceServices.Include(d => d.Device).Include(d => d.Service);
			return View(await repairShopContext.ToListAsync());
		}

		public async Task<IActionResult> Cart()
		{
			var cartString = HttpContext.Session.GetString(SessionCart) ?? "";
			List<DeviceService> shoppingCart = JsonConvert.DeserializeObject<List<DeviceService>>(cartString);
			return View("Index", shoppingCart);

		}

		// GET: DeviceServices/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var deviceService = await _context.DeviceServices
				.Include(d => d.Device)
				.Include(d => d.Service)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (deviceService == null)
			{
				return NotFound();
			}

			return View(deviceService);
		}

		// GET: DeviceServices/Create
		public IActionResult Create()
		{
			ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "SerialNbr");
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title");
			return View();
		}

		// POST: DeviceServices/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,DeviceId,ServiceId,DateStarted,DateCompleted,Comments")] DeviceService deviceService)
		{
			if (ModelState.IsValid)
			{
				_context.Add(deviceService);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "SerialNbr", deviceService.DeviceId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", deviceService.ServiceId);
			return View(deviceService);
		}

		// GET: DeviceServices/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var deviceService = await _context.DeviceServices.SingleOrDefaultAsync(m => m.Id == id);
			if (deviceService == null)
			{
				return NotFound();
			}
			ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "SerialNbr", deviceService.DeviceId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", deviceService.ServiceId);
			return View(deviceService);
		}

		// POST: DeviceServices/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Id,DeviceId,ServiceId,DateStarted,DateCompleted,Comments")] DeviceService deviceService)
		{
			if (id != deviceService.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(deviceService);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DeviceServiceExists(deviceService.Id))
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
			ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "SerialNbr", deviceService.DeviceId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", deviceService.ServiceId);
			return View(deviceService);
		}

		// GET: DeviceServices/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var deviceService = await _context.DeviceServices
				.Include(d => d.Device)
				.Include(d => d.Service)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (deviceService == null)
			{
				return NotFound();
			}

			return View(deviceService);
		}

		// POST: DeviceServices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var deviceService = await _context.DeviceServices.SingleOrDefaultAsync(m => m.Id == id);
			_context.DeviceServices.Remove(deviceService);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}



		private bool DeviceServiceExists(long id)
		{
			return _context.DeviceServices.Any(e => e.Id == id);
		}
	}
}
