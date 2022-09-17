using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ParametrosCorporativosD
    {
        List<ParametrosCorporativosE> parametroscorporativos = new List<ParametrosCorporativosE>();
        public List<ParametrosCorporativosE> BuscaParametrosCorporativos(string strNombreCodificado)
        {
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_buscar_parametros_corporativos(strNombreCodificado);
                    foreach (var item in sp)
                    {
                        ParametrosCorporativosE parametrocorporativo = new ParametrosCorporativosE();

                        parametrocorporativo.nombrecodificado = item.nombrecodificado;
                        parametrocorporativo.valorint = item.valorint.ToString();
                        parametrocorporativo.valorchar = item.valorchar;
                        parametrocorporativo.valordate = item.valordate.ToString();
                        parametrocorporativo.valordecimal = item.valordecimal.ToString();
                        parametroscorporativos.Add(parametrocorporativo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return parametroscorporativos;
        }


        public List<ParametrosCorporativosE> ListadoParametrosCorporativos()
        {
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_lista_parametros_corporativos();
                    foreach (var item in sp)
                    {
                        ParametrosCorporativosE parametrocorporativo = new ParametrosCorporativosE();
                        parametrocorporativo.codigoparametro = item.codigoparametro;
                        parametrocorporativo.nombrecodificado = item.nombrecodificado;
                        parametrocorporativo.nombre = item.nombre;
                        parametrocorporativo.valorint = item.valorint.ToString();
                        parametrocorporativo.valorchar = item.valorchar;
                        parametrocorporativo.valordate = item.valordate.ToString();
                        parametrocorporativo.valordecimal = item.valordecimal.ToString();
                        parametroscorporativos.Add(parametrocorporativo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return parametroscorporativos;
        }


        public bool? ActualizarParametroCorporativo(int intCodigoParametro, int valorint, string valorchar, DateTime? valordate, decimal valordecimal)

        {

            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_parametros_corporativos(intCodigoParametro, valorint,valorchar,valordate,valordecimal);
                    foreach (var item in sp)
                    {
                        respuesta = (item==1)?true:false;
                    }
                }
            }

            catch (Exception ex)

            {

            }
            return respuesta;

        }

    }
}
