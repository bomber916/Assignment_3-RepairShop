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

namespace RepairShop.Controllers
{
    public class UsersController : BaseController
    {
        private readonly RepairShopContext _context;

        public UsersController(RepairShopContext context)
        {
            _context = context;    
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var userDetail = new UserDetailViewModel();

            var user = await _context.Users
				.AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
			userDetail.User = user;
			userDetail.Devices = _context.Devices
				.AsNoTracking()
				.Where(d => d.UserId == user.Id).ToList();
			userDetail.TotalEarned = _context.DeviceServices
				.AsNoTracking()
				.Where(ds => ds.Device.UserId == user.Id)
				.Sum(ds => ds.Service.Price);

            return View(userDetail);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phone,EmailAddress")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Address,Phone,EmailAddress")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

		public IActionResult Login(string controller = "Home", string action = "Index")
		{
			return View(new User());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([Bind("UserName,Password")] User user)
		{
			try
			{
				if (!String.IsNullOrWhiteSpace(user.UserName) && !String.IsNullOrWhiteSpace(user.Password))
				{
					var foundUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == user.UserName.ToLower());
					if (foundUser != null)
					{
						if (foundUser.Password == user.Password)
						{
							HttpContext.Session.SetString(SessionKeyUserName, foundUser.UserName);
							HttpContext.Session.SetString(SessionKeyUserId, foundUser.Id.ToString());
						}
						else
						{
							throw new UnauthorizedAccessException();
						}
					}
					else
					{
						throw new UnauthorizedAccessException();
					}
				}
				else
				{
					throw new UnauthorizedAccessException();
				}
			}
			catch (UnauthorizedAccessException)
			{
				return RedirectToAction("Login");
			}
			return RedirectToAction("Index", "Home", new { area = "" });

		}

		public IActionResult Logout([Bind("UserName,Password")] User user)
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home", new { area = "" });
		}

		private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
