using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ecoQuest1.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public FeedbackModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public FeedbackInputModel Input { get; set; }

        public int IdEmpleado { get; set; }


        public class FeedbackInputModel
        {
            public int IdEmpleado { get; set; }

            [Required]
            public string Sugerencias { get; set; }

            [Required]
            public string QueQuitar { get; set; }

            [Required]
            public string Preguntas { get; set; }
        }

        public string UserName { get; set; }

        public IActionResult OnGet()
        {

            ViewData["ShowNavbar"] = true;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowExitLink"] = true;
            ViewData["ShowClasificacionLink"] = true;
            ViewData["ShowDashboardLink"] = true;
            ViewData["ShowDashboard-AdminLink"] = false;
            ViewData["ShowAddLink"] = false;
            ViewData["ShowEditLink"] = true;
            ViewData["ShowGuideLink"] = true;
            ViewData["ShowInstructionLink"] = true;


            UserName = HttpContext.Session.GetString("nombre");
            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));

            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserName = HttpContext.Session.GetString("nombre");
            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));

            Feedback feedbackItem = new Feedback
            {
                idEmpleados = IdEmpleado,
                sugerencias = Input.Sugerencias,
                que_quitar = Input.QueQuitar,
                preguntas = Input.Preguntas
            };

            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToPage("/Error");
            }


            string baseAddress = "https://localhost:7196/"; // Asegúrate de tener esto configurado en tu appsettings.json
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(feedbackItem), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Empleados/PostFeedback", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Feedback enviado correctamente.";
                        return RedirectToPage("/Guia_de_estudio");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al enviar el feedback.");
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al comunicarse con la API: " + ex.Message);
                    return Page();
                }
            }

            return RedirectToPage("/Success");
        }

        public class Feedback
        {
            public int idEmpleados { get; set; }
            public string sugerencias { get; set; }
            public string que_quitar { get; set; }
            public string preguntas { get; set; }
        }
    }
}
