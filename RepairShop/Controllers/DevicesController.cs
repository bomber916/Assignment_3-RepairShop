using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairShop.Data;
using RepairShop.Models;
using RepairShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RepairShop.Controllers
{
    public class DevicesController : BaseController
    {
        private readonly RepairShopContext _context;

        public DevicesController(RepairShopContext context)
        {
            _context = context;    
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var repairShopContext = _context.Devices.Include(d => d.User);
            return View(await repairShopContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(long? id, string sortOrder, int? page = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

			if (String.IsNullOrWhiteSpace(sortOrder))
			{
				sortOrder = "endDate";
			}

			ViewData["CurrentSort"] = sortOrder;
			ViewData["ServiceTitleSortParm"] = sortOrder == "serviceTitle" ? "serviceTitle_Desc" : "serviceTitle";
			ViewData["StartDateSortParm"] = sortOrder == "startDate" ? "startDate_desc" : "startDate";
			ViewData["EndDateSortParm"] = sortOrder == "endDate" ? "endDate_desc" : "endDate";
			ViewData["CostSortParm"] = sortOrder == "cost" ? "cost_desc" : "cost";


			var device = await _context.Devices
                .Include(d => d.User)
                .SingleOrDefaultAsync(m => m.Id == id);

			if(device == null)
			{
				return NotFound();
			}

			var deviceServices = _context.DeviceServices
				.Include(d => d.Service)
				.Where(ds => ds.DeviceId == device.Id);

			switch (sortOrder)
			{
				case "serviceTitle":
					deviceServices = deviceServices.OrderBy(ds => ds.Service.Title);
					break;
				case "serviceTitle_Desc":
					deviceServices = deviceServices.OrderByDescending(ds => ds.Service.Title);
					break;
				case nameof(DeviceService.DateOrdered):
					deviceServices = deviceServices.OrderBy(ds => ds.DateOrdered);
					break;
				case nameof(DeviceService.DateOrdered) + "_desc":
					deviceServices = deviceServices.OrderByDescending(ds => ds.DateOrdered);
					break;
				case "cost":
					deviceServices = deviceServices.OrderBy(ds => ds.Service.Price);
					break;
				case "cost_desc":
					deviceServices = deviceServices.OrderByDescending(ds => ds.Service.Price);
					break;
				default:
					deviceServices = deviceServices.OrderByDescending(ds => ds.DateOrdered);
					break;
			}
			int pageSize = 4;

			var deviceDetails = new DeviceDetailViewModel();
			deviceDetails.Device = device;
			deviceDetails.DeviceServices = await PaginatedList<DeviceService>.CreateAsync(deviceServices.AsNoTracking(), page ?? 1, pageSize);
			return View(deviceDetails);
		}

		

		// GET: Devices/Create
		public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SerialNbr,Type,Make,Model")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", device.UserId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.SingleOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", device.UserId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId,SerialNbr,Type,Make,Model")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", device.UserId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var device = await _context.Devices.SingleOrDefaultAsync(m => m.Id == id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DeviceExists(long id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
