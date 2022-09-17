using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CobroE
    {
        public string forma_cobro { get; set; }
        public double monto { get; set; }
        public string fecha { get; set; }


    }
    public class CobroTr
    {
        public string forma_cobro { get; set; }
        public double monto { get; set; }

        public string cuenta_bancaria_id { get; set; }
        public string fecha { get; set; }

    }
    public class CobroTc
    {
        public string forma_cobro { get; set; }
        public double monto { get; set; }
        public string tipo_ping { get; set; }
        public string fecha { get; set; }

    }
}
