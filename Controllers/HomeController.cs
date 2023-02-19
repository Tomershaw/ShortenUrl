//using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proUrl.Ds;
using proUrl.Models;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace proUrl.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager; 

        private readonly DbData _dataContext;
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger, DbData dataContext, UserManager<ApplicationUser> userManager)
        public HomeController(ILogger<HomeController> logger, DbData dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
            //_userManager = userManager;
        }
        //[Authorize]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("List")]
        [Authorize]
        public IActionResult Links()
        {
            var currentUser = HttpContext.User;
            var userid = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var tempdata = _dataContext.Users.Where(u => u.Id == userid);
            if(tempdata != null)
            {
                var data = _dataContext.UrlPairs.Where(u => u.UrlUser.Id == userid).ToList();
                return View(data);
            }
            else
            {
                var data1 = _dataContext.UrlPairs.Where(u => u.UrlUser == null).ToList();
                return View(data1);
            }

        }

        //[HttpGet("list")]//
        //public IActionResult ListOfShort()
        //{
        //    return View();
        //}



        //public ActionResult Redirect(string shorturl)
        //{
        //    return RedirectPermanent("\api\fullUrl");
        //}
        //[HttpGet("")]
        //public RedirectResult Index(string fullurl)
        //{
        //    return new RedirectResult(url: "/api/Index", permanent: true,
        //                              preserveMethod: true);
        //}





        //[HttpGet("{shortUrl}/{fullUrl}")]
        //public IActionResult Index(string shortUrl, string fullUrl)
        //{
        //    ViewData["shortUrl"] = shortUrl;
        //    ViewData["fullUrl"] = fullUrl;
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Index(string fullUrl)
        //{

        //    var shortUrl = _dataContext.Short(fullUrl);
        //    return RedirectToAction("index", "home", new { shortUrl = shortUrl, fullUrl = fullUrl });
        //}
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}