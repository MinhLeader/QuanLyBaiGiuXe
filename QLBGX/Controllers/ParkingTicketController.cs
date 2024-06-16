using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models;

namespace QLBGX.Controllers
{
    public class ParkingTicketController : Controller
    {
        private readonly QuanLyBaiGiuXeContext _context;

        public ParkingTicketController(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ShowTicket()
        {
            var veGuiXes = await _context.VeGuiXes.Include(v => v.BienSoXeNavigation)
                                                  .Include(v => v.MaLoaiVeNavigation)
                                                  .Include(v => v.MaKhNavigation)
                                                  .Include(v => v.MaChoDoXeNavigation)
                                                  .ToListAsync();
            return View(veGuiXes);
        }
        public async Task<IActionResult> DetailsTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGuiXe = await _context.VeGuiXes
                .Include(v => v.BienSoXeNavigation)
                .Include(v => v.MaLoaiVeNavigation)
                .Include(v => v.MaKhNavigation)
                .Include(v => v.MaChoDoXeNavigation)
                .FirstOrDefaultAsync(m => m.MaVe == id);
            if (veGuiXe == null)
            {
                return NotFound();
            }

            return View(veGuiXe);
        }

        public IActionResult CreateTicket()
        {
            ViewData["BienSoXe"] = new SelectList(_context.Xes, "BienSoXe", "BienSoXe");
            ViewData["MaLoaiVe"] = new SelectList(_context.LoaiVes, "MaLoaiVe", "TenLoaiVe");
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "TenKh");
            ViewData["MaChoDoXe"] = new SelectList(_context.ChoDoXes, "MaChoDoXe", "MaChoDoXe");
            return View();
        }

        // POST: VeGuiXe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket([Bind("MaVe,MaLoaiVe,BienSoXe,NgayGui,NgayLay,MaKh,NgayHetHan,SoGio,MaChoDoXe,TongTien")] VeGuiXe veGuiXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veGuiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BienSoXe"] = new SelectList(_context.Xes, "BienSoXe", "BienSoXe", veGuiXe.BienSoXe);
            ViewData["MaLoaiVe"] = new SelectList(_context.LoaiVes, "MaLoaiVe", "TenLoaiVe", veGuiXe.MaLoaiVe);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "TenKh", veGuiXe.MaKh);
            ViewData["MaChoDoXe"] = new SelectList(_context.ChoDoXes, "MaChoDoXe", "MaChoDoXe", veGuiXe.MaChoDoXe);
            return View(veGuiXe);
        }


        // GET: VeGuiXe/Edit/5
        public async Task<IActionResult> EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGuiXe = await _context.VeGuiXes.FindAsync(id);
            if (veGuiXe == null)
            {
                return NotFound();
            }
            ViewData["BienSoXe"] = new SelectList(_context.Xes, "BienSoXe", "BienSoXe", veGuiXe.BienSoXe);
            ViewData["MaLoaiVe"] = new SelectList(_context.LoaiVes, "MaLoaiVe", "TenLoaiVe", veGuiXe.MaLoaiVe);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "TenKh", veGuiXe.MaKh);
            ViewData["MaChoDoXe"] = new SelectList(_context.ChoDoXes, "MaChoDoXe", "MaChoDoXe", veGuiXe.MaChoDoXe);
            return View(veGuiXe);
        }

        // POST: VeGuiXe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTicket(int id, [Bind("MaVe,MaLoaiVe,BienSoXe,NgayGui,NgayLay,MaKh,NgayHetHan,SoGio,MaChoDoXe,TongTien")] VeGuiXe veGuiXe)
        {
            if (id != veGuiXe.MaVe)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veGuiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeGuiXeExists(veGuiXe.MaVe))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShowTicket));
            }
            ViewData["BienSoXe"] = new SelectList(_context.Xes, "BienSoXe", "BienSoXe", veGuiXe.BienSoXe);
            ViewData["MaLoaiVe"] = new SelectList(_context.LoaiVes, "MaLoaiVe", "TenLoaiVe", veGuiXe.MaLoaiVe);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "TenKh", veGuiXe.MaKh);
            ViewData["MaChoDoXe"] = new SelectList(_context.ChoDoXes, "MaChoDoXe", "MaChoDoXe", veGuiXe.MaChoDoXe);
            return View(veGuiXe);
        }

        // GET: VeGuiXe/Delete/5
        public async Task<IActionResult> DeleteTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGuiXe = await _context.VeGuiXes
                .Include(v => v.BienSoXeNavigation)
                .Include(v => v.MaLoaiVeNavigation)
                .Include(v => v.MaKhNavigation)
                .Include(v => v.MaChoDoXeNavigation)
                .FirstOrDefaultAsync(m => m.MaVe == id);
            if (veGuiXe == null)
            {
                return NotFound();
            }

            return View(veGuiXe);
        }

        // POST: VeGuiXe/Delete/5
        [HttpPost, ActionName("DeleteTicket")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTicketConfirmed(int id)
        {
            var veGuiXe = await _context.VeGuiXes.FindAsync(id);
            _context.VeGuiXes.Remove(veGuiXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowTicket));
        }

        private bool VeGuiXeExists(int id)
        {
            return _context.VeGuiXes.Any(e => e.MaVe == id);
        }
    }





}
