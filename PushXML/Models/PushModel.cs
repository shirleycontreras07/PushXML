using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PushXML.Models
{
    public class PushModel
    {
        [Key]
        public int PushId { get; set; }

        [Display(Name = "Codigo Asiento")]
        public string IdAsiento { get; set; }

        [Display(Name = "Numero de Asiento")]
        public string NoAsiento { get; set; }

        [Display(Name = "Descripcion del Asiento")]
        public string DescripcionAsiento { get; set; }

        [Display(Name = "Fecha de Asiento")]
        public string FechaAsiento { get; set; }

        [Display(Name = "Cuenta Contable")]
        public string CuentaContable { get; set; }

        [Display(Name = "Tipo de Movimiento")]
        public string TipoMovimiento { get; set; }

        [Display(Name = "Monto")]
        public string Monto { get; set; }

    }
}