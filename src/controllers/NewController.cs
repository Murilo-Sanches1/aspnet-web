using Microsoft.AspNetCore.Mvc;

namespace Ultimate.Controllers
{
    public class NewController : Controller
    {
        [Route("/url-atualizada/{name}")]
        public IActionResult NewUrl()
        {
            return Json($"Url nova - {Request.RouteValues["name"]}");
        }
    }
}