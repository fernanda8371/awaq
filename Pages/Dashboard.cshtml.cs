using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ecoQuest1.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public class EmpleadoIntentos
        {
            public int IdEmpleados { get; set; }
            public int CountE { get; set; }
            public int CountS { get; set; }
            public int CountT { get; set; }
        }

        public DashboardModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string UserName { get; set; }
        public int IdEmpleado { get; set; }
        public string EmbedUrl { get; set; }
        public string EmbedUrlEtica { get; set; }
        public string EmbedUrlSeguridad { get; set; }
        public string EmbedUrlTEDI { get; set; }
        public string EmbedUrlDashboard { get; set; }

        public int CountE { get; set; }
        public int CountS { get; set; }
        public int CountT { get; set; }

        public async Task OnGetAsync()
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
            // Si el nombre de usuario no está en la sesión, redirige a la página de ingreso

            if (HttpContext.Session.GetString("username") == null)
            {
                Response.Redirect("Ingresar");
            }

            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));
            UserName = HttpContext.Session.GetString("nombre");

            // Obtener los valores de intentos desde la API
            await ObtenerIntento(IdEmpleado);

            string lookerStudioBaseUrl = "https://lookerstudio.google.com/embed/reporting/b4bc5995-14a3-407c-a542-3c9c33892c6e/page/p_tuxem61rhd";
            string parameters = $"params=%7B%22id_empleado_parametro%22%3A%22{IdEmpleado}%22%7D";
            EmbedUrl = $"{lookerStudioBaseUrl}?{parameters}";

            // Etica
            string lookerStudioBaseUrlEtica = "https://lookerstudio.google.com/embed/reporting/65883eb1-3d34-4327-824f-a0eb85d7e596/page/g5k2D";
            string parametersEtica = $"params=%7B%22ds0.id%22%3A%22{IdEmpleado}%22%7D";
            EmbedUrlEtica = $"{lookerStudioBaseUrlEtica}?{parametersEtica}";

            // Seguridad
            string lookerStudioBaseUrlSeguridad = "https://lookerstudio.google.com/embed/reporting/65883eb1-3d34-4327-824f-a0eb85d7e596/page/p_y71zyh43hd";
            string parametersSeguridad = $"params=%7B%22ds0.id%22%3A%22{IdEmpleado}%22%7D";
            EmbedUrlSeguridad = $"{lookerStudioBaseUrlSeguridad}?{parametersSeguridad}";

            // TEDI
            string lookerStudioBaseUrlTEDI = "https://lookerstudio.google.com/embed/reporting/65883eb1-3d34-4327-824f-a0eb85d7e596/page/p_5rga0h43hd";
            string parametersTEDI = $"params=%7B%22ds0.id%22%3A%22{IdEmpleado}%22%7D";
            EmbedUrlTEDI = $"{lookerStudioBaseUrlTEDI}?{parametersTEDI}";

            // General tabla
            string lookerStudioBaseUrlDashboard = "https://lookerstudio.google.com/embed/reporting/d2cef7c9-7e50-4c0e-8945-b6e6c4ebb24e/page/48k2D";
            string parametersDashboard = $"params=%7B%22ds0.id%22%3A%22{IdEmpleado}%22%7D";
            EmbedUrlDashboard = $"{lookerStudioBaseUrlDashboard}?{parametersDashboard}";
        }

        private async Task ObtenerIntento(int id)
        {
            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"api/Empleados/Intentos/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var empleadoIntentos = JsonConvert.DeserializeObject<EmpleadoIntentos>(await response.Content.ReadAsStringAsync());
                        CountE = empleadoIntentos.CountE;
                        CountS = empleadoIntentos.CountS;
                        CountT = empleadoIntentos.CountT;
                    }
                    else
                    {
                        CountE = 0;
                        CountS = 0;
                        CountT = 0;
                    }
                }
                catch (Exception ex)
                {
                    CountE = 0;
                    CountS = 0;
                    CountT = 0;
                    // O maneja el error como prefieras
                }
            }
        }
    }
}
