using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ecoQuest1.Pages
{
    public class IngresarModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public const string SessionKeyName = "_Nombre";

        [BindProperty]
        public LoginRequest _LoginRequest { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IngresarModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool Permission { get; set; }

        [BindProperty]
        public bool StayConnected { get; set; }



        public void OnGet()
        {
            ViewData["ShowNavbar"] = false;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowDashboardLink"] = false;
            ViewData["ShowAddLink"] = false;
            ViewData["ShowEditLink"] = false;
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {


                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Serializar el objeto LoginRequest a JSON
                    var jsonContent = JsonConvert.SerializeObject(_LoginRequest);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Enviar la solicitud POST a la API REST para autenticar
                    HttpResponseMessage response = await client.PostAsync("api/Empleados/Authenticate", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var empleado = JsonConvert.DeserializeObject<Empleado>(apiResponse);

                        // Guardar información en la sesión

                        //Guardar información en la sesión
                        HttpContext.Session.SetString("username", empleado.Nombre);

                        HttpContext.Session.SetString("idEmpleado", empleado.idEmpleado.ToString());
                        HttpContext.Session.SetString("nombre", empleado.Nombre);
                        HttpContext.Session.SetString("apellido", empleado.Apellido);
                        HttpContext.Session.SetString("edad", empleado.Edad.ToString());
                        HttpContext.Session.SetString("email", empleado.Email);
                        HttpContext.Session.SetString("genero", empleado.Genero);
                        HttpContext.Session.SetString("pais", empleado.Pais);

                        HttpContext.Session.SetInt32("permission", empleado.Permission);
                        HttpContext.Session.SetString("linkedin", empleado.Linkedin);
                        HttpContext.Session.SetString("contacto", empleado.Celular);

                        await CallUpdateNPCAsync(empleado.idEmpleado);


                        if (empleado.Permission == 1) // Administrador
                        {
                            return RedirectToPage("Dashboard_Admin");
                        }
                        else//Empleado
                        {
                            return RedirectToPage("Dashboard");
                        }
                    }
                    else
                    {
                        // Manejar la respuesta de error
                        ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                    }

                    var Nombre = HttpContext.Session.GetString(SessionKeyName);
                    _logger.LogInformation("Session Name: {nombre}", Nombre);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    ModelState.AddModelError(string.Empty, "Error al comunicarse con la API: " + ex.Message);
                }
            }
            return Page();
        }

        private async Task CallUpdateNPCAsync(int empId)
        {
            string baseAddress = "https://localhost:7196/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync($"api/Empleados/UpdateNPC/{empId}", null);

                if (!response.IsSuccessStatusCode)
                {
                    // Manejar la respuesta de error
                    _logger.LogError("Error al actualizar NPC: " + response.ReasonPhrase);
                }
            }
        }


        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Empleado
        {
            public int idEmpleado { get; set; }
            public int Modulo { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Edad { get; set; }
            public string Email { get; set; }
            public string Genero { get; set; }
            public string Pais { get; set; }
            public string Password { get; set; }
            public int Permission { get; set; }
            public string Celular { get; set; }
            public string Linkedin { get; set; }
        }

        


    }
}