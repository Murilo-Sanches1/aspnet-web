using Microsoft.AspNetCore.Mvc;
using Ultimate.Models;

namespace Ultimate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    [Controller]
    public class EmptyController : Controller
    {
        [Route("/")]
        [Route("/index")]
        public IActionResult Index()
        {
            return Content("<h1>Home - Olá</h1>", "text/html; charset=utf-8");
            // return new ContentResult() { Content = "<h1>Home</h1>", ContentType = "text/html" };
        }

        [Route("/user")]
        public IActionResult UserJson()
        {
            User user = new User() { Id = Guid.NewGuid(), FirstName = "Murilo", LastName = "Sanches", Age = 19 };
            return Json(user);
        }

        [Route("/relative-file")]
        public IActionResult File()
        {
            return File("./sample.pdf", "application/pdf");
        }

        [Route("/absolute-file")]
        public IActionResult FileAbs()
        {
            return PhysicalFile("C:\\Users\\sanch\\Documents\\sample.pdf", "application/pdf");
        }

        [Route("/byte-file")]
        public IActionResult FileByte()
        {
            byte[] bytes = System.IO.File.ReadAllBytes("C:\\Users\\sanch\\Documents\\sample.pdf");
            return File(bytes, "application/pdf");
        }

        [Route("/block")]
        public IActionResult Block()
        {
            return Unauthorized("Você não tem permissão para acessar essa rota protegida");
        }

        [Route("/not")]
        public IActionResult Idk()
        {
            return NotFound("Você não tem permissão para acessar essa rota protegida");
        }

        [Route("/bad")]
        public IActionResult Bad()
        {
            return BadRequest("Você não tem permissão para acessar essa rota protegida");
        }

        [Route("/about")]
        public string About()
        {
            return "About";
        }

        [Route("/contact-us")]
        public string Contact()
        {
            return "Contact";
        }

        [Route("/product/{id:int}")]
        public string Product()
        {
            return $"Produto - {Request.RouteValues["id"]}";
        }
    }
}