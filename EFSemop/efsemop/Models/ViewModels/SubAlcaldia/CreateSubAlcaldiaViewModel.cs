using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efsemop.Models.ViewModels.SubAlcaldia
{
    public class CreateSubAlcaldiaViewModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Zona es un dato requerido.")]
        [StringLength(190, ErrorMessage = "Ha superado el número de caracteres permitidos")]
        public string Zona { get; set; }
        public string Telefono { get; set; }
        public string NombreSubAlcalde { get; set; }
    }
}