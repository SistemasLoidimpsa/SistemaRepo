using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReporteLcl
    {
        public string idCotizacion { get; set; }
        public string tipoCoti { get; set; }
        public string nombreCliente { get; set; }
        public string numeroIdentificacion { get; set; }
        public string correo { get; set; }
        public string descripcionMercaderia { get; set; }
        public string importador { get; set; }
        public string peso { get; set; }
        public string puertoOrigen { get; set; }
        public string puertoDestino { get; set; }
        public string diasTranscurrido { get; set; }
        public string metrosCubicos { get; set; }
        public string fleteInternacional { get; set; }
        public string gastoSpot { get; set; }

        public string coFee { get; set; }
        public string iva { get; set; }
        public string totalGasto { get; set; }
        public string totalCotiza { get; set; }
        public string fechaExpiraCotizacion { get; set; }
        public string fechaEmisionCoti { get; set; }


        public string usuarioRegistro { get; set; }
        public string observacion { get; set; }


    }
}
