using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoQuest1
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }

        public int DepartamentoId { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamento Departamento { get; set; }
    }
}

