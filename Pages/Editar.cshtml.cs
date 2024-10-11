using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace ecoQuest1.Pages
{
    public class EditarModel : PageModel
    {
        public string UserName { get; set; }
        public int IdEmpleado { get; set; }
        public int Modulo { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string NombreDepartamento { get; set; }
        public string Celular { get; set; }
        public string Linkedin { get; set; }
        public string BiografiaActual { get; set; } // Propiedad para almacenar la biografía actual

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Biografia { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
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

            await OnGetNombreDepartamentoAsync(IdEmpleado);
            await ObtenerBiografiaActualAsync(IdEmpleado);
            return Page();
        }

        public async Task<IActionResult> OnGetNombreDepartamentoAsync(int id)
        {
            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"api/Empleados/GetNombreDepartamento/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        NombreDepartamento = JsonConvert.DeserializeObject<string>(apiResponse);
                        ViewData["NombreDepartamento"] = NombreDepartamento;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al obtener el nombre del departamento.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al comunicarse con la API: " + ex.Message);
                }
            }

            return Page();
        }

        private async Task<IActionResult> AgregarBioAsync(int id, string biografia)
        {
            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(biografia), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"api/Empleados/UpdateBiography/{id}", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Biografía actualizada correctamente.";
                        return RedirectToPage("/Editar"); // Redirigir a la misma página
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Empleado no encontrado.");
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al comunicarse con la API: " + ex.Message);
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));

            return await AgregarBioAsync(IdEmpleado, Input.Biografia);
        }

        public IActionResult OnPostRedirectToFeedback()
        {
            return RedirectToPage("/Feedback");
        }

        private async Task ObtenerBiografiaActualAsync(int id)
        {
            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"api/Empleados/GetBiography/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        BiografiaActual = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        BiografiaActual = "No se pudo obtener la biografía actual.";
                    }
                }
                catch (Exception ex)
                {
                    BiografiaActual = "Error al comunicarse con la API: " + ex.Message;
                }
            }
        }
    }
}