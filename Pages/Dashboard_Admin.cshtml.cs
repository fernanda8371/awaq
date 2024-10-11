using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ecoQuest1.Pages
{
    public class Dashboard_AdminModel : PageModel
    {
        public int IdEmpleado { get; set; }
        public List<Leaderboard> LeaderboardList { get; set; }

        public async Task OnGetAsync()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("Ingresar");
            }

            int admin = (int)HttpContext.Session.GetInt32("permission");

            if (admin != 1)
            {
                Response.Redirect("Ingresar");
            }

            ViewData["ShowNavbar"] = true;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowDashboardLink"] = false;
            ViewData["ShowDashboard-AdminLink"] = true;
            ViewData["ShowAddLink"] = true;
            ViewData["ShowEditLink"] = false;
            ViewData["ShowExitLink"] = true;
            ViewData["ShowClasificacionLink"] = true;

            // Llamar a la API para obtener los datos del leaderboard
            string apiUrl = "https://localhost:7196/api/Empleados/TopEmpleados";
            IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("idEmpleado"));

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
    }

    public class Leaderboard
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdDepartamento { get; set; } // Nuevo campo
        public string Departamento { get; set; } // Nuevo campo
        public int Porcentaje { get; set; }
    }
}
