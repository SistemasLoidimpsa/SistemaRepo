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
    
    public partial class sp_orden_envio_Result
    {
        public int idEnvio { get; set; }
        public string tipoEntrega { get; set; }
        public string categoria { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string observacion { get; set; }
        public string NombreCompleto { get; set; }
        public string EjecutivoCuenta { get; set; }
        public Nullable<decimal> totalPesoEnvio { get; set; }
        public Nullable<decimal> pagoTotalEnvio { get; set; }
        public string estadoOrden { get; set; }
        public string archivoPago { get; set; }
        public string cedulaNumero { get; set; }
        public string descripcion { get; set; }
        public string tipoPago { get; set; }
        public string pagoVerificar { get; set; }
        public Nullable<decimal> valorEnvioDomi { get; set; }
    }
}
