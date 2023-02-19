using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proUrl.Ds;
using proUrl.Models;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Polly;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using proUrl.Migrations;

namespace proUrl.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {

        private readonly DbData _dataContext;
        private readonly ILogger<UrlPair> _logger;

        public ApiController(DbData context, ILogger<UrlPair> logger)
        {
            _dataContext = context;
            _logger = logger;

        }
        [HttpPost]
        //[Authorize]
        public ActionResult Shorten([FromBody] string fullUrl)
        {
            //get user id from http request
            var currentUser = HttpContext.User;
            var userid = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var idUser = _dataContext.Users.SingleOrDefault(u => u.Id == userid);

            var urlPair = _dataContext.Short(fullUrl, idUser);
            //var count  = _dataContext.CountFromDataBase(shortUrl);
            //return Ok(shortUrl);
            return Created("", new { FullUrl = fullUrl, ShortUrl = urlPair.ShortUrl, Count = urlPair.Count, ClickCount = urlPair.ClickCount });
        }

        //[HttpGet]
        //public ActionResult link(string shortUrl)
        //{
        //    var press = _dataContext.PressLink(shortUrl);

        //    //return Ok(shortUrl);
        //    return Ok();

        //}


        //public actionresult url([frombody] string fullurl)
        //{
        //    return redirectpermanent("");
        //}









        //[HttpGet("{fullUrl}")]
        //public ActionResult Index(string fullUrl)
        //{
        //    var shortUrl = _dataContext.Short(fullUrl);

        //    return Ok(shortUrl);
        //    //return Ok(new  {FullUrl =fullUrl, ShortUrl=shortUrl });
        //}
    }
}
