using BCPAO.PermitManager.Data;
using BCPAO.PermitManager.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PermitManager.Controllers
{
	public class PermitsController : Controller
    {
        private readonly DatabaseContext _context;

        public PermitsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Permit
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permits.ToListAsync());
        }

        // GET: Permit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingPermit = await _context.Permits.SingleOrDefaultAsync(m => m.PermitId == id);
            if (buildingPermit == null)
            {
                return NotFound();
            }

            return View(buildingPermit);
        }

        // GET: Permit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Permit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PermitId,PropertyId,ParcelId,PermitNumber,PermitStatus,IssueDate,FinalDate,PermitValue,PermitDesc,PermitCode,DistrictAuthority,CreateDate")] Permit permit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permit);
        }

        // GET: Permit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingPermit = await _context.Permits.SingleOrDefaultAsync(m => m.PermitId == id);
            if (buildingPermit == null)
            {
                return NotFound();
            }
            return View(buildingPermit);
        }

        // POST: Permit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PermitId,PropertyId,ParcelId,PermitNumber,PermitStatus,IssueDate,FinalDate,PermitValue,PermitDesc,PermitCode,DistrictAuthority,CreateDate")] Permit permit)
        {
            if (id != permit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingPermitExists(permit.Id))
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
            return View(permit);
        }

        // GET: Permit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingPermit = await _context.Permits
                .SingleOrDefaultAsync(m => m.PermitId == id);
            if (buildingPermit == null)
            {
                return NotFound();
            }

            return View(buildingPermit);
        }

        // POST: Permit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingPermit = await _context.Permits.SingleOrDefaultAsync(m => m.PermitId == id);
            _context.Permits.Remove(buildingPermit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingPermitExists(int id)
        {
            return _context.Permits.Any(e => e.PermitId == id);
        }
    }
}
