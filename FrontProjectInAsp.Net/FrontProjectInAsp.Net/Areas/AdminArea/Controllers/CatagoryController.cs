using FrontProjectInAsp.Net.Data;
using FrontProjectInAsp.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontProjectInAsp.Net.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CatagoryController : Controller
    {
        private readonly AppDbContext _context;
        public CatagoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Catagory> catagories = await _context.Catagories.AsNoTracking().ToListAsync();
            return View(catagories);
        }
        //Begin Create Method
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist =  _context.Catagories.Any(m => m.catagoryName.ToLower().Trim() == catagory.catagoryName.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("catagoryName", "Bu catagory artiq movcuddur");
                return View();
            }

            await _context.Catagories.AddAsync(catagory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //End Create Method

        //Begin Edit Method
        public async Task<IActionResult> Edit(int id)
        {
            Catagory catagory = await _context.Catagories.Where(m=>m.Id==id).FirstOrDefaultAsync();
            if (catagory == null) return NotFound();
            return View(catagory);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id != catagory.Id) return BadRequest();

            Catagory dbcatagory = await _context.Catagories.Where(m => m.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (dbcatagory.catagoryName.ToLower().Trim() == catagory.catagoryName.ToLower().Trim()) return RedirectToAction(nameof(Index));
            bool isExist = _context.Catagories.Any(m => m.catagoryName.ToLower().Trim() == catagory.catagoryName.ToLower().Trim());
            
            if (isExist)
            {
                ModelState.AddModelError("catagoryName", "Bu catagory artiq movcuddur");
                return View();
            }
            
            //dbcatagory.catagoryName = catagory.catagoryName;
            _context.Update(catagory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            
            
        }
        //End Edit Method

        //Begin Delete Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Catagory catagory = await _context.Catagories.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (catagory == null) return NotFound();

            _context.Catagories.Remove(catagory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
