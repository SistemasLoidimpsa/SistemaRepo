using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TransportistasD
    {

        public Dictionary<string, int> Transportistas()
        {
            Dictionary<string, int> trans = new Dictionary<string, int>();
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var sp = context.sp_transportistas();
                    foreach (var item in sp)
                    {
                        trans.Add(item.nombreTransportista, item.idTransportista);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return trans;
        }
    }
}
