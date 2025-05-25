using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Product;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
