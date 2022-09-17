using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;
using Entidades;


namespace Negocio
{
    public class GeneralesN 
    {
        GeneralesD generalesD;
        public Dictionary<string, string> Provincias()
        {
            generalesD = new GeneralesD();
            return generalesD.Provincias();
        }
       

        public Dictionary<string, string> Pay()
        {
            generalesD = new GeneralesD();
            return generalesD.Pay();
        }

        public Dictionary<string, string> Api(string nombre)
        {
            generalesD = new GeneralesD();
            return generalesD.Api(nombre);
        }

        
        public Dictionary<string, string> Cantones(string id_prov)
        {
            generalesD = new GeneralesD();
            return generalesD.Cantones(id_prov);
        }

        public Dictionary<string, string> RolesAdministrativos()
        {
            generalesD = new GeneralesD();
            return generalesD.RolesAdministrativos();
        }

        public Dictionary<string, string> RolesGeneral()
        {
            generalesD = new GeneralesD();
            return generalesD.RolesGeneral();
        }

        public bool? registroPayGet(string strCid, string stridorden, DateTime fechaRegistro, string respuestaC   )
        {
            generalesD = new GeneralesD();
            return generalesD.registroPayGet(strCid, stridorden, fechaRegistro, respuestaC);
        }


        public List<PagoE> BuscarRegistroPago(string cnn, string strCid, string stridorden, string estado)
        {
            return new GeneralesD().BuscarRegistroPago(cnn, strCid, stridorden, estado);
        }

    }
}
