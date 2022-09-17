using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Datos
{
    public class ReportesD
    {
        public ReportesE ReporteCotizacionD(string cnn, string strCliente)
        {
            ReportesE items = new ReportesE();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strCliente", strCliente);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_cotizaciones", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {

                    items.idCotizacion = item["idCotizacion"].ToString();
                    items.Nombre = item["Nombre"].ToString();
                    items.correo = item["correo"].ToString();
                    items.descripcionMercaderia = item["descripcionMercancia"].ToString();
                    items.valorFof = item["valorFof"].ToString();
                    items.peso = item["peso"].ToString();
                    items.servicioCourier = item["servicioCourier"].ToString();
                    items.totalEnvio = item["totalEnvio"].ToString();
                    items.fechaCotizacion = item["fechaCotizacion"].ToString();
                    items.usuarioRegstro = item["usuarioRegistro"].ToString();
                    items.nacionalizacion = item["nacionalizacion"].ToString();
                    items.fleteInternacional = item["fleteInternacional"].ToString();
                    items.envioDomicilio = item["envioDomicilio"].ToString();
                    items.impuestoSenae = item["impuestoSenae"].ToString();

                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }


        public ReporteLcl ReporteCotizacionLcl(string cnn, string strCliente)
        {
            ReporteLcl items = new ReporteLcl();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strCliente", strCliente);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_cotizacionesLcl", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {

                    items.idCotizacion = item["idCotizacionLcl"].ToString();

                    items.nombreCliente = item["nombreCliente"].ToString();
                    items.numeroIdentificacion = item["numeroIdentificacion"].ToString();
                    items.correo = item["correo"].ToString();
                    items.descripcionMercaderia = item["descripcionMercancia"].ToString();
                    items.importador = item["importador"].ToString();
                    items.peso = item["peso"].ToString();
                    items.puertoOrigen = item["puertoOrigen"].ToString();
                    items.puertoDestino = item["puertoDestino"].ToString();
                    items.diasTranscurrido = item["diasTranscurrido"].ToString();
                    items.metrosCubicos = item["metrosCubicos"].ToString();
                    items.fleteInternacional = item["fleteInternacional"].ToString();
                    items.gastoSpot = item["gastoSpot"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.coFee = item["coFee"].ToString();
                    items.iva = item["iva"].ToString();
                    items.totalGasto = item["totalGasto"].ToString();
                    items.totalCotiza = item["totalCotiza"].ToString();
                    items.fechaExpiraCotizacion = item["fechaExpiraCotizacion"].ToString();
                    items.fechaEmisionCoti = item["fechaEmisionCoti"].ToString();
                    items.observacion = item["observacion"].ToString();

                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }
        public List<ReportesE> ConsultaCotizacionD(string cnn, DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            List<ReportesE> ListCotiza = new List<ReportesE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@strEjecutivo", strUsuario);
            parameter[1] = new SqlParameter("@strFechaInicio", strFechaInicio);
            parameter[2] = new SqlParameter("@strFechaFin", strFechaFin);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_consulta_cotizaciones", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReportesE items = new ReportesE();
                    items.idCotizacion = item["idCotizacion"].ToString();
                    items.Nombre = item["Nombre"].ToString();
                    items.correo = item["correo"].ToString();
                    items.descripcionMercaderia = item["descripcionMercancia"].ToString();
                    items.valorFof = item["valorFof"].ToString();
                    items.peso = item["peso"].ToString();
                    items.servicioCourier = item["servicioCourier"].ToString();
                    items.totalEnvio = item["totalEnvio"].ToString();
                    items.fechaCotizacion = item["fechaCotizacion"].ToString();
                    items.usuarioRegstro = item["usuarioRegistro"].ToString();
                    items.nacionalizacion = item["nacionalizacion"].ToString();
                    items.fleteInternacional = item["fleteInternacional"].ToString();
                    items.envioDomicilio = item["envioDomicilio"].ToString();
                    items.impuestoSenae = item["impuestoSenae"].ToString();
                    ListCotiza.Add(items);
                }
            }
            catch (Exception ex)
            {
                ListCotiza = null;
            }

            return ListCotiza;
        }

        public List<ReporteLcl> ConsultaCotizacionCLcl(string cnn, DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            List<ReporteLcl> ListCotiza2 = new List<ReporteLcl>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@strEjecutivo", strUsuario);
            parameter[1] = new SqlParameter("@strFechaInicio", strFechaInicio);
            parameter[2] = new SqlParameter("@strFechaFin", strFechaFin);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_consulta_cotizacionesLcl", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReporteLcl items = new ReporteLcl();
                    items.idCotizacion = item["idCotizacionLcl"].ToString();
                   
                    items.nombreCliente = item["nombreCliente"].ToString();
                    items.numeroIdentificacion = item["numeroIdentificacion"].ToString();
                    items.correo = item["correo"].ToString();
                    items.descripcionMercaderia = item["descripcionMercancia"].ToString();
                    items.importador = item["importador"].ToString();
                    items.peso = item["peso"].ToString();
                    items.puertoOrigen = item["puertoOrigen"].ToString();
                    items.puertoDestino = item["puertoDestino"].ToString();
                    items.diasTranscurrido = item["diasTranscurrido"].ToString();
                    items.metrosCubicos = item["metrosCubicos"].ToString();
                    items.fleteInternacional = item["fleteInternacional"].ToString();
                    items.gastoSpot = item["gastoSpot"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.coFee = item["coFee"].ToString();
                    items.iva = item["iva"].ToString();
                    items.totalGasto = item["totalGasto"].ToString();
                    items.totalCotiza = item["totalCotiza"].ToString();
                    items.fechaExpiraCotizacion = item["fechaExpiraCotizacion"].ToString();
                    items.fechaEmisionCoti = item["fechaEmisionCoti"].ToString();
                   
                    ListCotiza2.Add(items);
                }
            }
            catch (Exception ex)
            {
                ListCotiza2 = null;
            }

            return ListCotiza2;
        }


    }
}
