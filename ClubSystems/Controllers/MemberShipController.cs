using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubSystems.Data;
using ClubSystems.Models;
using System.Security.Cryptography;

namespace ClubSystems.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly ClubSystemsContext _context;

        public MemberShipController(ClubSystemsContext context)
        {
            _context = context;
        }

        // GET: MemberShip
        public async Task<IActionResult> Index()
        {
            var clubSystemsContext = _context.MemberShip.Include(m => m.Person);
            return View(await clubSystemsContext.ToListAsync());
        }

        // GET: MemberShip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberShip == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.MemberShipNumber == id);
            if (memberShip == null)
            {
                return NotFound();
            }

            return View(memberShip);
        }

        // GET: MemberShip/Create
        public IActionResult Create()
        {
            var enumData = from MemberShipType e in Enum.GetValues(typeof(MemberShipType))
                           select new
                           {
                               ID = (int)e,
                               Type = e.ToString()
                           };
            ViewData["MemberShipType"]= new SelectList(enumData, "ID", "Type");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames");
            return View();
        }

        // POST: MemberShip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberShipNumber,MemberShipType,AccountBalance,PersonID,IsOverdrawn")] MemberShip memberShip)
        {
            if (validMembership(memberShip.PersonID,memberShip.MemberShipType))
            {
                _context.Add(memberShip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var enumData = from MemberShipType e in Enum.GetValues(typeof(MemberShipType))
                           select new
                           {
                               ID = (int)e,
                               Type = e.ToString()
                           };
            ViewData["MemberShipType"] = new SelectList(enumData, "ID", "Type");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames", memberShip.PersonID);
            ViewData["ErrorMessage"] = "Selected Membership Type is already exists for this Member";
            return View(memberShip);
        }

        // GET: MemberShip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberShip == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip.FindAsync(id);
            if (memberShip == null)
            {
                return NotFound();
            }
            var enumData = from MemberShipType e in Enum.GetValues(typeof(MemberShipType))
                           select new
                           {
                               ID = (int)e,
                               Type = e.ToString()
                           };
            ViewData["MemberShipType"] = new SelectList(enumData, "ID", "Type");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames", memberShip.PersonID);
            return View(memberShip);
        }

        // POST: MemberShip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberShipNumber,MemberShipType,AccountBalance,PersonID,IsOverdrawn")] MemberShip memberShip)
        {
            if (id != memberShip.MemberShipNumber)
            {
                return NotFound();
            }

            if (validMembership(memberShip.PersonID, memberShip.MemberShipType,id))
            {
                try
                {
                    _context.Update(memberShip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberShipExists(memberShip.MemberShipNumber))
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
            var enumData = from MemberShipType e in Enum.GetValues(typeof(MemberShipType))
                           select new
                           {
                               ID = (int)e,
                               Type = e.ToString()
                           };
            ViewData["MemberShipType"] = new SelectList(enumData, "ID", "Type");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames", memberShip.PersonID);
            ViewData["ErrorMessage"] = "Selected Membership Type is already exists for this Member";
            return View(memberShip);
        }

        // GET: MemberShip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberShip == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.MemberShipNumber == id);
            if (memberShip == null)
            {
                return NotFound();
            }

            return View(memberShip);
        }

        // POST: MemberShip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberShip == null)
            {
                return Problem("Entity set 'ClubSystemsContext.MemberShip'  is null.");
            }
            var memberShip = await _context.MemberShip.FindAsync(id);
            if (memberShip != null)
            {
                _context.MemberShip.Remove(memberShip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Indiviual()
        {
            var clubSystemsContext = _context.MemberShip.Include(m => m.Person);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames");
            return View(await clubSystemsContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Indiviual(int pid)
        {
            List<MemberShip>? list = await _context.MemberShip?.Where(e => e.PersonID == pid).ToListAsync();
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames");
            return View(list);
        }

        public async Task<IActionResult> OverdrawnAccount()
        {
            List<MemberShip>? list = await _context.MemberShip?.Where(e => e.IsOverdrawn == true).ToListAsync();
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Forenames");
            return View(list);
        }




            private bool MemberShipExists(int id)
        {
          return (_context.MemberShip?.Any(e => e.MemberShipNumber == id)).GetValueOrDefault();
        }

        private bool validMembership(int id, MemberShipType type, int mid=0)
        {
            bool flag = true;

            List<MemberShip>? list;
            if(mid==0)
                list= _context.MemberShip?.Where(e => e.PersonID == id).ToList();
            else
                list = _context.MemberShip?.Where(e => e.PersonID == id && e.MemberShipNumber!=mid).ToList();
            if ((bool)list?.Exists(e => e.MemberShipType == type))
            {
                flag = false;
            }
            return flag;
        }
    }
}
