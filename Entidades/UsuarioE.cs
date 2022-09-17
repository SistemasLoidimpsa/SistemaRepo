using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class UsuarioE
    {
        public string cod_usu { get; set; }
        public string identificacion { get; set; }
        public int rol { get; set; }
        public string Nombres { get; set; }
        public bool? cambio_clave { get; set; }
        public DateTime? fecha_cambio_clave { get; set; }
        public bool estado { get; set; }
        public string direccionEntrega { get; set; }
        public int privilegiado { get; set; }


    }
}

