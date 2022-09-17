using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Contifico
    {
        public string pos { get; set; }
        public string fecha_emision { get; set; }
        public string tipo_documento { get; set; }
        public string documento { get; set; }
        public string estado { get; set; }
        public bool electronico { get; set; }
        public string autorizacion { get; set; }
        public string descripcion { get; set; }
        public double subtotal_0 { get; set; }
        public double subtotal_12 { get; set; }
        public double iva { get; set; }
        public double ice { get; set; }
        public double total { get; set; }


        public ClienteC cliente { get; set; }
        public EmpleadoC vendedor { get; set; }
        public  List<Object> cobros { get; set; }
        public List<ProductoE> detalles { get; set; }
    }
}
