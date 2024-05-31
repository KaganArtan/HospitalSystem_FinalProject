using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalSystem_FinalProject.Data;
using HospitalSystem_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystem_FinalProject.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var hospitals = _context.Hospitals.Include(h => h.City).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                hospitals = hospitals.Where(h => h.Address.Contains(searchString));
            }

            return View(await hospitals.ToListAsync());
        }



        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.City)
                .FirstOrDefaultAsync(m => m.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        [Authorize]
        // GET: Hospitals/Create
        public IActionResult Create()
        {
            // Örnek: Şehir listesini City tablosundan getir
            var cities = _context.Cities.Select(c => new { c.CityId, c.Name }).ToList();

            // ViewBag'e CityName ve CityId'yi içeren bir SelectList ekle
            ViewBag.CityId = new SelectList(cities, "CityId", "Name");

            return View();
        }
        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HospitalId,Name,Address,Phone,CityId")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", hospital.CityId);
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", hospital.CityId);
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HospitalId,Name,Address,Phone,CityId")] Hospital hospital)
        {
            if (id != hospital.HospitalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.HospitalId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", hospital.CityId);
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.City)
                .FirstOrDefaultAsync(m => m.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.HospitalId == id);
        }

        //DOKTORLARI GÖSTER

        public async Task<IActionResult> Doctors(int hospitalId)
        {
            var doctors = await _context.Doctors.Where(d => d.HospitalId == hospitalId).ToListAsync();
            return View(doctors);
        }

    }
}
