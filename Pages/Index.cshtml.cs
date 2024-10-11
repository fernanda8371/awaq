using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecoQuest1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        ViewData["ShowNavbar"] = true;
        ViewData["ShowIngresarink"] = true;
        ViewData["ShowDashboardLink"] = true;
        ViewData["ShowAddLink"] = true;
        ViewData["ShowEditLink"] = true;
        Response.Redirect("Ingresar");
    }
}

