using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ClienteE
    {
        public string idCasillero { get; set; }
        public string numeroidentificacion { get; set; }
        public string nombres { get; set; }
        public string tipoCliente { get; set; }
        public string idEjectuvio { get; set; }
        public string cod_usu { get; set; }
        public string valorFob { get; set; }
    }

    public class ClienteC
    {

        public string cedula { get; set; }
        public string razon_social { get; set; }
        public string telefonos { get; set; }
        public string direccion { get; set; }
        public string tipo { get; set; }
        public string email { get; set; }
       

    }


    public class ClienteCanje
    {
        public string numeroidentificacion { get; set; }
        public string producto { get; set; }
        public string nombresCliente { get; set; }
        public string codigoCanje { get; set; }
        public string puntosCanjeado { get; set; }
        public string puntosTotal { get; set; }
    }



}
