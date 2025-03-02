using CursorPagingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CursorPagingApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
            this._context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? lastId, string? category, int pageSize=10)
        {
            var query = _context.Products.AsQueryable();

            if(!string.IsNullOrEmpty(category))
                query= query.Where(s=>s.Category== category);

            // Apply the cursor base Pagination
            if (lastId.HasValue)
                query = query.Where(s => s.Id > lastId);
            query = query.OrderBy(p => p.Id);
            var products = await query.Take(pageSize).ToListAsync();

            //Get the last Id for the best Page Request
            int? newLastId = products.Any() ? products.Last().Id : (int?)null;
            bool isLastPage=products.Count < pageSize;

            // Pass data to View
            ViewBag.LastId = newLastId;
            ViewBag.IsFirstPage = lastId == null;
            ViewBag.IsLastPage = isLastPage;
            ViewBag.CurrentCategory = category;


            return View(products);
        }


        public async Task<IActionResult> Index1(int? lastId, string? category, string? name, int pageSize = 10)
        {
            var query = _context.Products.AsQueryable();

            // Apply multiple WHERE conditions (filters)
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }


            // Apply cursor-based pagination (next page)
            if (lastId.HasValue)
            {
                query = query.Where(p => p.Id > lastId.Value);
            }

            // Get the total count of records (for pagination UI)
            int totalRecords = await query.CountAsync();

            // Fetch paginated results
            query = query.OrderBy(p => p.Id);
            var products = await query.Take(pageSize).ToListAsync();

            // Determine first and last record
            int? newLastId = products.Any() ? products.Last().Id : null;
            int? firstId = await _context.Products.OrderBy(p => p.Id).Select(p => p.Id).FirstOrDefaultAsync();
            int? lastRecordId = await _context.Products.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefaultAsync();
            bool isFirstPage = lastId == null || lastId == firstId;
            bool isLastPage = newLastId == null || newLastId == lastRecordId;

            // Calculate total pages (simulating UI number buttons)
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            int currentPage = lastId.HasValue ? (lastId.Value / pageSize) + 1 : 1;

            // Pass data to View
            ViewBag.LastId = newLastId;
            ViewBag.IsFirstPage = isFirstPage;
            ViewBag.IsLastPage = isLastPage;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentName = name;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = currentPage;

            return View(products);
        }

        // here Cursor is not implemented
        public async Task<IActionResult> Index2(int? lastId, string? category, string? name, int PageSize=10)
        {
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category == category);
            if (!string.IsNullOrEmpty(name)) query = query.Where(p => p.Name.Contains(name));

            // Apply cursor-based pagination
            if (lastId.HasValue) query = query.Where(p => p.Id > lastId.Value);

            // Fetch limited records in one go
            var products = await query
                .OrderBy(p => p.Id)
                .Take(PageSize + 1) // Fetch one extra to check if there's a next page
                .Select(p => new { p.Id, p.Name, p.Category }) // Fetch only required fields
                .ToListAsync();

            // Set pagination metadata
            bool hasMore = products.Count > PageSize;
            if (hasMore) products.RemoveAt(PageSize); // Remove extra item

            ViewBag.LastId = products.LastOrDefault()?.Id;
            ViewBag.IsFirstPage = !lastId.HasValue;
            ViewBag.IsLastPage = !hasMore;

            return View(products);
        }

        public async Task<IActionResult> Index3(int? cursorId, string? category, string? name, bool prev = false, int PageSize=10)
        {
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category == category);
            if (!string.IsNullOrEmpty(name)) query = query.Where(p => p.Name.Contains(name));

            // Cursor-based pagination logic
            if (cursorId.HasValue)
            {
                query = prev ? query.Where(p => p.Id < cursorId) : query.Where(p => p.Id > cursorId);
            }

            // Fetch one extra item to check for next page existence
            var products = await query
                .OrderBy(p => p.Id) // Ensure order is always consistent
                .Take(PageSize + 1)
                .ToListAsync();

            // Determine cursor positions
            bool hasMore = products.Count > PageSize;
            if (hasMore) products.RemoveAt(PageSize); // Remove extra item

            ViewBag.NextCursor = hasMore ?(int?)products.Last().Id : null;
            ViewBag.PrevCursor = products.First().Id;
            ViewBag.IsFirstPage = !cursorId.HasValue;
            ViewBag.IsLastPage = !hasMore;

            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
