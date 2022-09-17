using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReporteCas
    {
        public string Mes { get; set; }
        public string cantidadMes { get; set; }
    }

    public class ReportePeso
    {
        public string Mes { get; set; }
        public double cantidadMes { get; set; }
        public string peso { get; set; }

    }

    public class ReporteCategoriaRed
    {
        public string Mes { get; set; }
        public string cantidadMes { get; set; }
        public string campoMarketing { get; set; }

    }


}
