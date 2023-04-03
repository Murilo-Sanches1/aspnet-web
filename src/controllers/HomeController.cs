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
            return NotFound("Não encontrado");
        }

        [Route("/bad")]
        public IActionResult Bad()
        {
            return BadRequest("Requisição má formada");
        }

        [Route("/old")]
        public IActionResult Redirect()
        {
            return RedirectToAction(
                actionName: "NewUrl",
                controllerName: "New",
                routeValues: new { name = "Murilo" });
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

        [Route("/product/{id?}")]
        public string ProductModelBinding(int? id)
        {
            return $"Produto - {id}";
        }

        [Route("/book")]
        public IActionResult Book([FromQuery] Book book)
        {
            if (!ModelState.IsValid)
            {
                // List<string> errList = new List<string>();
                // foreach (var val in ModelState.Values)
                // {
                //     foreach (var err in val.Errors)
                //     {
                //         errList.Add(err.ErrorMessage);
                //     }
                // }
                List<string> errList = ModelState.Values.SelectMany((val) =>
                {
                    return val.Errors;
                }).Select((err) => err.ErrorMessage).ToList();

                return BadRequest(string.Join("\n", errList));
            }

            Console.WriteLine(book.ToString());
            return Content($"Produto - {book.BookId}");
        }

        [Route("/sign-user")]
        public IActionResult UserJson([FromForm] User user)
        {

            return Json("");
        }
    }
}