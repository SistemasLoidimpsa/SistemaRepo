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
    
    public partial class ServicioTick
    {
        public int idTicket { get; set; }
        public string cedulaCliente { get; set; }
        public string idTracking { get; set; }
        public Nullable<int> idTransportista { get; set; }
        public string nombreTransportista { get; set; }
        public string descripContenido { get; set; }
        public string descripProblema { get; set; }
        public string nombreAgente { get; set; }
        public string statusTicket { get; set; }
        public string usuarioRegistro { get; set; }
        public Nullable<System.DateTime> fechaRegistroTicket { get; set; }
        public int estadoTicket { get; set; }
    }
}
