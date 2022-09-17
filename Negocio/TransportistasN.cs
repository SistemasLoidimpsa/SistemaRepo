using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class TransportistasN
    {
        TransportistasD transportistasD;
        public Dictionary<string, int> Transportistas()
        {
            transportistasD = new TransportistasD();
            return transportistasD.Transportistas();
        }
    }
}
