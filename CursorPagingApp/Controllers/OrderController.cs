using CursorPagingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursorPagingApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10;

        public OrderController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetProducts(int? cursorId, string search = "")
        {
            var query = _context.Products.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || p.Category.Contains(search));

            // Cursor-based pagination logic
            if (cursorId.HasValue)
                query = query.Where(p => p.Id > cursorId);

            // Fetch one extra to check if next page exists
            var products = await query
                .OrderBy(p => p.Id)
                .Take(PageSize + 1)
                .ToListAsync();

            // Check if there's a next page
            bool hasMore = products.Count > PageSize;
            if (hasMore) products.RemoveAt(PageSize); // Remove extra item

            return Json(new
            {
                data = products,
                nextCursor = hasMore ?(int?)products.Last().Id : null
            });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts1(int draw, int start, int length, string search)
        {
            var query = _context.Products.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || p.Category.Contains(search));

            // Cursor-based pagination logic
            int? cursorId = start == 0 ? null : (int?)start;  // Convert `start` to cursorId logic
            if (cursorId.HasValue)
                query = query.Where(p => p.Id > cursorId);

            // Fetch `length + 1` items to check for the next page
            var products = await query
                .OrderBy(p => p.Id)
                .Take(length + 1)
                .ToListAsync();

            // Check if there's a next page
            bool hasMore = products.Count > length;
            if (hasMore) products.RemoveAt(length); // Remove extra item

            return Json(new
            {
                draw = draw,
                recordsTotal = await _context.Products.CountAsync(),
                recordsFiltered = query.Count(),
                data = products,
                nextCursor = hasMore ? (int?)products.Last().Id : null
            });
        }

        public IActionResult Index1() => View();
    }
}
