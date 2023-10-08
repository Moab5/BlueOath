using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueOath.Data;
using BlueOath.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlueOath.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly BlueOath.Areas.Identity.Data.BlueOathContext _identityContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomsController(BlueOath.Areas.Identity.Data.BlueOathContext context1, IWebHostEnvironment hostEnvironment)
        {
            _identityContext = context1;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
              return _identityContext.Room != null ? 
                          View(await _identityContext.Room.ToListAsync()) :
                          Problem("Entity set 'BlueOathContext.Room'  is null.");
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _identityContext.Room == null)
            {
                return NotFound();
            }

            var room = await _identityContext.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomType,Facilities,RoomImage,Rate,Status")] Room room)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(room.RoomImage.FileName);
                string extension = Path.GetExtension(room.RoomImage.FileName);
                room.Description = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await room.RoomImage.CopyToAsync(fileStream);
                }
                _identityContext.Add(room);
                await _identityContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _identityContext.Room == null)
            {
                return NotFound();
            }

            var room = await _identityContext.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomType,Facilities,Description,Rate,Status")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _identityContext.Update(room);
                    await _identityContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _identityContext.Room == null)
            {
                return NotFound();
            }

            var room = await _identityContext.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_identityContext.Room == null)
            {
                return Problem("Entity set 'BlueOathContext.Room'  is null.");
            }
            var room = await _identityContext.Room.FindAsync(id);
            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", room.Description);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _identityContext.Room.Remove(room);

            if (room != null)
            {
                _identityContext.Room.Remove(room);
            }
            
            await _identityContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
          return (_identityContext.Room?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
