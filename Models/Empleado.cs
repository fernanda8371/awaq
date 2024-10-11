using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoQuest1
{
 

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
        public string Departamento { get; set; }
        public string Biografia { get; set; }
        public int Permission { get; set; }
        public string Password { get; set; }
        public string Celular { get; set; }
        public int idDepartamento { get; set; }
    }

}

