using Microsoft.AspNetCore.Mvc;

namespace OblakotekaClient.Controllers;

public class ProductController : Controller
{

    public ProductController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
