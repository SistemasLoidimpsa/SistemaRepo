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
    
    public partial class sp_reporte_cotizacionesLcl_Result
    {
        public int idCotizacionLcl { get; set; }
        public string nombreCliente { get; set; }
        public string correo { get; set; }
        public string descripcionMercancia { get; set; }
        public string numeroIdentificacion { get; set; }
        public Nullable<decimal> peso { get; set; }
        public string importador { get; set; }
        public string puertoOrigen { get; set; }
        public string puertoDestino { get; set; }
        public Nullable<int> diasTranscurrido { get; set; }
        public Nullable<decimal> metrosCubicos { get; set; }
        public Nullable<decimal> fleteInternacional { get; set; }
        public Nullable<decimal> gastoSpot { get; set; }
        public decimal coFee { get; set; }
        public decimal iva { get; set; }
        public decimal totalGasto { get; set; }
        public decimal totalCotiza { get; set; }
        public Nullable<System.DateTime> fechaExpiraCotizacion { get; set; }
        public System.DateTime fechaEmisionCoti { get; set; }
        public string usuarioRegistro { get; set; }
    }
}
