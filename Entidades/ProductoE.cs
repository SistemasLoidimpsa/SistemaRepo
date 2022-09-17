using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ProductoE
    {
        public string producto_id { get; set; }
        public double cantidad { get; set; }
        public double precio { get; set; }
        public int porcentaje_iva { get; set; }
        public double porcentaje_descuento { get; set; }
        public double base_cero { get; set; }
        public double base_gravable { get; set; }
        public double base_no_gravable { get; set; }
 
    }
}
