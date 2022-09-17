using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class GeneralesD
    {
        public Dictionary<string, string> Provincias()
        {
            Dictionary<string, string> provincias = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_provincias();
                    foreach (var item in sp)
                    {
                        provincias.Add(item.id_prov, item.provincia);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return provincias;
        }


        public Dictionary<string, string> Pay()
        {
            Dictionary<string, string> payData = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_pay_values();
                    foreach (var item in sp)
                    {
                        payData.Add("usuario", item.usuario);
                        payData.Add("llavemd5", item.llavemd5);
                        payData.Add("urlback", item.urlback);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return payData;
        }
        public Dictionary<string, string> Api(string nombre)
        {
            Dictionary<string, string> payData = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_api_values(nombre);
                    foreach (var item in sp)
                    {
                        payData.Add("apiKey", item.apiKey);
                        payData.Add("apiToken", item.apiToken);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return payData;
        }


        public Dictionary<string, string> Cantones(string id_prov)
        {
            Dictionary<string, string> cantones = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_cantones_provincia(id_prov);
                    foreach (var item in sp)
                    {
                        cantones.Add(item.id_cant, item.canton);
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return cantones;
        }


        public Dictionary<string, string> RolesAdministrativos()
        {
            Dictionary<string, string> cantones = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_roles_administrativos();
                    foreach (var item in sp)
                    {
                        cantones.Add(item.descripcion, item.id_rol.ToString());
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return cantones;
        }

        public Dictionary<string, string> RolesGeneral()
        {
            Dictionary<string, string> cantones = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_roles_general();
                    foreach (var item in sp)
                    {
                        cantones.Add(item.descripcion, item.id_rol.ToString());
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return cantones;
        }

        public List<PagoE> BuscarRegistroPago(string cnn, string strCid, string stridorden, string estado)
        {
            List<PagoE> pagoList = new List<PagoE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@strCid", strCid);
            parameter[1] = new SqlParameter("@stridorden", stridorden);
            parameter[2] = new SqlParameter("@strEstado", estado);
            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_pago", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    PagoE items = new PagoE();

                    items.cid = item["cid"].ToString();
                    items.idorden = item["idorden"].ToString();
                    items.estado = item["estado"].ToString();
                    items.fechaRegistro = item["fechaRegistro"].ToString();
                    pagoList.Add(items);
                }
            }
            catch (Exception ex)
            {
                pagoList = null;
            }

            return pagoList;
        }

        public bool? registroPayGet(string strCid, string stridorden, DateTime fechaRegistro, string respuestaC)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_registro_payments_get(strCid, stridorden,  respuestaC,  fechaRegistro);
                    foreach (var item in sp)
                    {
                        respuesta = (item == 1) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = true;
            }
            return respuesta;
        }

    }
}
