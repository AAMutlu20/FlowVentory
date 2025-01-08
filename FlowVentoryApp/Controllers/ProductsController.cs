using Microsoft.AspNetCore.Mvc;

namespace FlowVentoryApp.Controllers;

public class ProductsController : Controller
{
    public IActionResult Products()
    {
        return View();
    }
}