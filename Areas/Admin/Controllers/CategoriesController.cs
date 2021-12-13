using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.ViewModel;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly MohajerContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(MohajerContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var mohajerContext = await  _context.Categores.Where(p=>p.IsRemove==false).Include(c => c.ParentCategory).ToListAsync();
            var result = _mapper.Map<List< CategoryViewModel>>(mohajerContext);
            return View(result);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categores
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
         
            ViewData["ParentCategoryId"] = new SelectList(_context.Categores.Where(p=>p.ParentCategoryId==null && p.IsRemove==false), "Id", "Title");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Category category)
        {
            ModelState.Remove("ParentCategoryId");
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categores.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(new CategoryEditViewModel { Id=category.Id ,Title=category.Title});
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] CategoryEditViewModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            ModelState.Remove("ParentCategoryId");
            if (ModelState.IsValid)
            {
                try
                {
                    var oldCategry = _context.Categores.Where(p => p.Id == category.Id).AsNoTracking().FirstOrDefault();
                    oldCategry.Title = category.Title;
                    _context.Update(oldCategry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categores
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            var result = _mapper.Map<CategoryViewModel>(category);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categores.FindAsync(id);
            category.IsRemove = true;
            category.Removed = DateTime.Now;
            _context.Categores.Remove(category);
            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categores.Any(e => e.Id == id);
        }
    }
}
