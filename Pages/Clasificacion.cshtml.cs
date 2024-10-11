using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static ecoQuest1.Pages.IngresarModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ecoQuest1.Pages
{
    public class ClasificacionModel : PageModel
    {
        public string Username { get; set; }
        private readonly IConfiguration _configuration;
        public List<Leaderboard> LeaderboardList { get; set; }

        public ClasificacionModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Empleado> Empleados { get; set; }
        public List<Leaderboard> LeaderboardEmpleados { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["ShowNavbar"] = true;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowExitLink"] = true;
            ViewData["ShowClasificacionLink"] = true;

            // Obtiene el permiso del usuario de la sesión
            int userPermission = Convert.ToInt32(HttpContext.Session.GetInt32("permission"));


            if (userPermission == 1) // Permiso
            {
                ViewData["ShowDashboardLink"] = false;
                ViewData["ShowDashboard-AdminLink"] = true;
                ViewData["ShowAddLink"] = true;
                ViewData["ShowEditLink"] = false;
                ViewData["ShowGuideLink"] = false;
                ViewData["ShowInstructionLink"] = false;
            }
            else
            {
                ViewData["ShowDashboardLink"] = true;
                ViewData["ShowDashboard-AdminLink"] = false;
                ViewData["ShowAddLink"] = false;
                ViewData["ShowEditLink"] = true;
                ViewData["ShowGuideLink"] = true;
                ViewData["ShowInstructionLink"] = true;
            }

            Username = HttpContext.Session.GetString("username");

            // Llamar a la API para obtener los datos del leaderboard
            LeaderboardEmpleados = await GetTopEmpleadosAsync();

            // Llamar a la API para obtener los datos del leaderboard
            string apiUrl = "https://localhost:7196/api/Empleados/TopEmpleados";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    LeaderboardList = JsonConvert.DeserializeObject<List<Leaderboard>>(apiResponse);
                }
                else
                {
                    // Manejar error
                    LeaderboardList = new List<Leaderboard>();
                }
            }
        }

        private async Task<List<Leaderboard>> GetTopEmpleadosAsync()
        {
            List<Leaderboard> empleados = new List<Leaderboard>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7196/"); // Reemplaza con la URL base de tu API
                HttpResponseMessage response = await client.GetAsync("api/Empleados/TopEmpleados");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<Leaderboard>>(json);
                }
            }

            return empleados;
        }

        public class Empleado
        {
            public int idEmpleado { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Porcentaje { get; set; }
        }

        public class Leaderboard
        {
            public int IdEmpleado { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdDepartamento { get; set; }
            public string Departamento { get; set; }
            public int Porcentaje { get; set; }
       
        }
    }
}

