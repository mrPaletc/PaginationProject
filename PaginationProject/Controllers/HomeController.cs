using Microsoft.AspNetCore.Mvc;
using PaginationProject.Models;
using System.Diagnostics;

namespace PaginationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EntityDBContext _context;
        public HomeController(ILogger<HomeController> logger, EntityDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public JsonResult GetTable(int? size, int? page)
        {
            List<EntityByDb> data = new List<EntityByDb>();
            page = page ?? 1;
            int startId = ((int)page - 1) * (size ?? 0);
            data = _context.Entities.Skip(startId).Take(size ?? 0).ToList();
            
            int count = _context.Entities.Count();
            int pagesNum = count / (int)size;
            return Json(new { data = data, pageCurrent = page, count = count, pagesnum = pagesNum });
        }

        public IActionResult Index()
        {
            return View();
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