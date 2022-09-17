using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReportesE
    {
        public string idCotizacion { get; set; }
        public string Nombre { get; set; }
        public string correo { get; set; }
        public string descripcionMercaderia { get; set; }
        public string valorFof { get; set; }
        public string peso { get; set; }
        public string servicioCourier { get; set; }
        public string totalEnvio { get; set; }
        public string fechaCotizacion { get; set; }
        public string usuarioRegstro { get; set; }

        public string nacionalizacion { get; set; }

        public string fleteInternacional { get; set; }

        public string envioDomicilio { get; set; }

        public string impuestoSenae { get; set; }
    }
}
