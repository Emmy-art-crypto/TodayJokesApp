using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodayJokesApp.Data;
using TodayJokesApp.Models;

namespace TodayJokesApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jokes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // ✅ FIX 1: Load Likes so count starts from 0 properly
            return View(await _context.Jokes
                .Include(j => j.Likes)
                .ToListAsync());
        }

        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes
                .Include(j => j.Comments) // ✅ FIX
                .FirstOrDefaultAsync(m => m.Id == id);

            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComments(int jokeId, string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
                return RedirectToAction("Details", new { id = jokeId });

            var comment = new Comments
            {
                Content = Content,
                JokeId = jokeId,
                CreatedAt = DateTime.Now,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = jokeId });
        }
        // GET: Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokesQuestion,JokesAnswer")] Jokes jokes)
        {
            if (ModelState.IsValid)
            {
                // ✅ FIX 2: Ensure Likes is empty → starts at 0
                jokes.Likes = new List<Like>();

                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes.FindAsync(id);
            if (jokes == null)
            {
                return NotFound();
            }
            return View(jokes);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokesQuestion,JokesAnswer")] Jokes jokes)
        {
            if (id != jokes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(jokes);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes
                .Include(j => j.Likes) // optional fix
                .FirstOrDefaultAsync(m => m.Id == id);

            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jokes = await _context.Jokes.FindAsync(id);
            if (jokes != null)
            {
                _context.Jokes.Remove(jokes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // SEARCH
        public IActionResult Search()
        {
            return View();
        }

        // PROFILE
        [Authorize]
        public IActionResult Profile()
        {
            var email = User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.Email = email;
            ViewBag.UserId = userId;

            return View();
        }

        // SEARCH RESULTS
        public async Task<IActionResult> SearchResults(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Index", await _context.Jokes
                    .Include(j => j.Likes) // ✅ FIX
                    .ToListAsync());
            }

            var results = await _context.Jokes
                .Include(j => j.Likes) // ✅ FIX
                .Where(j => j.JokesQuestion.Contains(query) ||
                            j.JokesAnswer.Contains(query))
                .ToListAsync();

            return View("Index", results);
        }

        // ✅ LIKE BUTTON FIX (MULTI CLICK WORKING)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(int id)
        {
            var joke = await _context.Jokes
                .Include(j => j.Likes)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (joke == null)
            {
                return NotFound();
            }

            // ✔ ALWAYS ALLOW +1 LIKE
            _context.Likes.Add(new Like
            {
                JokeId = id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool JokesExists(int id)
        {
            return _context.Jokes.Any(e => e.Id == id);
        }
    }
}