//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cotizaciones
    {
        public int idCotizacion { get; set; }
        public string nombre { get; set; }
        public string categoriCotizacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string correo { get; set; }
        public string descripcionMercancia { get; set; }
        public Nullable<decimal> valorFof { get; set; }
        public Nullable<decimal> peso { get; set; }
        public Nullable<decimal> servicioCourier { get; set; }
        public Nullable<decimal> totalEnvio { get; set; }
        public Nullable<System.DateTime> fechaCotizacion { get; set; }
        public string usuarioRegistro { get; set; }
        public Nullable<decimal> nacionalizacion { get; set; }
        public Nullable<decimal> fleteInternacional { get; set; }
        public Nullable<decimal> envioDomicilio { get; set; }
        public Nullable<decimal> impuestoSenae { get; set; }
    }
}
