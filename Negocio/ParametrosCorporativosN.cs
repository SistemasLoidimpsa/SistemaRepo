using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ParametrosCorporativosN
    {
        ParametrosCorporativosD parametrosCorporativosD;
        public List<ParametrosCorporativosE> BuscaParametrosCorporativos(string strNombreCodificado)
        {
            parametrosCorporativosD = new ParametrosCorporativosD();
            return parametrosCorporativosD.BuscaParametrosCorporativos(strNombreCodificado);
        }

        public List<ParametrosCorporativosE> ListadoParametrosCorporativos()
        {
            parametrosCorporativosD = new ParametrosCorporativosD();
            return parametrosCorporativosD.ListadoParametrosCorporativos();
        }

        public bool? ActualizarParametroCorporativo(int intCodigoParametro, int valorint, string valorchar, DateTime? valordate, decimal valordecimal)
        {
            parametrosCorporativosD = new ParametrosCorporativosD();
            return parametrosCorporativosD.ActualizarParametroCorporativo(intCodigoParametro, valorint, valorchar, valordate, valordecimal);
        }
    }
}
