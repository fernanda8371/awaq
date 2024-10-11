using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoQuest1
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        public int AdministradorId { get; set; }

        [ForeignKey("AdministradorId")]
        public Administrador Administrador { get; set; }


    }


}

