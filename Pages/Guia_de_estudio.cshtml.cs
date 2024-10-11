using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace ecoQuest1.Pages
{
	public class Guia_de_estudioModel : PageModel
    {
        public int IdEmpleado { get; set; }
        public string UserName { get; set; }
        public int Modulo { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string NombreDepartamento { get; set; }
        public string Celular { get; set; }
        public string Linkedin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ShowNavbar"] = true;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowDashboardLink"] = true;
            ViewData["ShowDashboard-AdminLink"] = false;
            ViewData["ShowAddLink"] = false;
            ViewData["ShowEditLink"] = true;
            ViewData["ShowExitLink"] = true;
            ViewData["ShowClasificacionLink"] = true;
            ViewData["ShowGuideLink"] = true;
            ViewData["ShowInstructionLink"] = true;



            if (HttpContext.Session.GetString("username") == null)
            {
                Response.Redirect("Ingresar");
            }

            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));
   
            UserName = HttpContext.Session.GetString("nombre");
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToPage("/Error");
            }

            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));
            Apellido = HttpContext.Session.GetString("apellido");
            Edad = Convert.ToInt32(HttpContext.Session.GetString("edad"));
            Email = HttpContext.Session.GetString("email");
            Celular = HttpContext.Session.GetString("contacto");
            Linkedin = HttpContext.Session.GetString("linkedin");
            Genero = HttpContext.Session.GetString("genero");
            Pais = HttpContext.Session.GetString("pais");
            return Page();
        }

        public IActionResult OnPostRedirectToFeedback()
        {
            return RedirectToPage("/Feedback");
        }

    }
}
