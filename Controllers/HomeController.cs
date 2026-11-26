using BookCorner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookCorner.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
        public IActionResult SetCookies(string cookieName, string cookieValue)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(15),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("FavoriteCategory", cookieValue, options);
            return Ok("Cookies has been set.");
        }
        public IActionResult GetCookies()
        {
            var cookies = Request.Cookies;

            foreach (var c in cookies)
            {
                if (c.Key == "FavoriteCategory")
                {
                    return Ok(c.Key + ": " + c.Value);
                }
            }

            return Ok("Cookie not found.");
        }
        public IActionResult SetSession(string key, string value)
        {
            HttpContext.Session.SetString("SelectedBook", value);
            return RedirectToAction("GetSession");
        }

        public IActionResult GetSession()
        {
            return Ok(HttpContext.Session.GetString("SelectedBook"));
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
