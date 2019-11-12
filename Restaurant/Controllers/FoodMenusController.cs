using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    [Authorize(Policy = "CanManageFoodMenus")]
    //[Authorize(Roles = "manager")]
    public class FoodMenusController : Controller
    {
        private readonly RestaurantDbContext _context;

        public FoodMenusController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: FoodMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodMenu.ToListAsync());
        }

        // GET: FoodMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodMenu = await _context.FoodMenu
                .FirstOrDefaultAsync(m => m.FoodMenuId == id);
            if (foodMenu == null)
            {
                return NotFound();
            }

            return View(foodMenu);
        }

        // GET: FoodMenus/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodMenuId,Name,Description,Price")] FoodMenu foodMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodMenu);
        }

        // GET: FoodMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodMenu = await _context.FoodMenu.FindAsync(id);
            if (foodMenu == null)
            {
                return NotFound();
            }
            return View(foodMenu);
        }

        // POST: FoodMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodMenuId,Name,Description,Price")] FoodMenu foodMenu)
        {
            if (id != foodMenu.FoodMenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodMenuExists(foodMenu.FoodMenuId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodMenu);
        }

        // GET: FoodMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodMenu = await _context.FoodMenu
                .FirstOrDefaultAsync(m => m.FoodMenuId == id);
            if (foodMenu == null)
            {
                return NotFound();
            }

            return View(foodMenu);
        }

        // POST: FoodMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodMenu = await _context.FoodMenu.FindAsync(id);
            _context.FoodMenu.Remove(foodMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodMenuExists(int id)
        {
            return _context.FoodMenu.Any(e => e.FoodMenuId == id);
        }
    }
}
