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
    public class CotizadoresD
    {
        public CotizadoresE InsertaCotizacionB(string strNombre, string strCorreo,string strCliente, string strCategoria, string strDescripcionMercancia,
            decimal decValorFOF, decimal decPeso, decimal decServicioCourier, decimal totalEnvio, string strUsuarioRegistro
            , decimal nacionalizacion, decimal flete, decimal impuestoSenae, decimal decValorEnvioDomicilio)
        {
            CotizadoresE items = new CotizadoresE();

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_cotizacion(strNombre, strCorreo,strCliente, strCategoria, strDescripcionMercancia, decValorFOF, decPeso, decServicioCourier,
                        totalEnvio, strUsuarioRegistro, nacionalizacion, flete, impuestoSenae, decValorEnvioDomicilio);
                    foreach (var item in sp)
                    {
                        items.id_codigoContizacion = item.Value;
                        //items.tipo_Certificado = item.tipoCertificado;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return items;
        }



        public CotizadoresE InsertaCotizacionClc(string nombre, string correo, string strCliente, string strTipo, string strDescripcionMercancia,
           string importador, decimal decPeso, string puertoOrigen, string puertoDestino, string strUsuarioRegistro
           , int diasTranscurrido, decimal metrosCubicos, decimal fleteInternacional, decimal gastoSpot, decimal coFee, decimal iva, decimal totalGasto, decimal totalCotiza, DateTime fechaExpiraCotizacion, string obs)

        {
            CotizadoresE items = new CotizadoresE();

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_cotizacionLcl(nombre, correo, strCliente, strTipo, strDescripcionMercancia,
                importador, decPeso, puertoOrigen, puertoDestino, strUsuarioRegistro, diasTranscurrido, metrosCubicos, fleteInternacional,
                gastoSpot, coFee, iva, totalGasto, totalCotiza, fechaExpiraCotizacion, obs);
                    foreach (var item in sp)
                    {
                        items.id_codigoContizacion = item.Value;
                        //items.tipo_Certificado = item.tipoCertificado;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return items;
        }

        public bool InsertaRegistroVenta(DateTime fechaVentaEfectiva, string tipoCliente, string tipoDocumento, string nroDocumento, decimal valorVenta,
                        string tipoVenta, int porcentaje, decimal valorComision, string usuarioRegistro, string identificacionEjecutivo, 
                                string identificacionCliente, string strDescripcionVenta, decimal decValorVentaSinIVA)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_registro_venta_comision(fechaVentaEfectiva,tipoCliente,tipoDocumento,nroDocumento,valorVenta,tipoVenta,porcentaje,
                                                        valorComision,usuarioRegistro, identificacionEjecutivo, identificacionCliente, strDescripcionVenta,
                                                        decValorVentaSinIVA);
                    foreach (var item in sp)
                    {
                        respuesta = (item == 1) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return respuesta;
        }

        public List<ListadoVentasE> ListadoClientes(string cnn, string strEjecutivo,DateTime strFechaInicio, DateTime strFechaFin)
        {
            List<ListadoVentasE> ventasList = new List<ListadoVentasE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@strEjecutivo", strEjecutivo);
            parameter[1] = new SqlParameter("@strFechaInicio", strFechaInicio);
            parameter[2] = new SqlParameter("@strFechaFin", strFechaFin);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_ventas_ejecutivo", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ListadoVentasE items = new ListadoVentasE();
                    items.idVenta = item["idVenta"].ToString();
                    items.FechaVentaEfectiva = item["FechaVentaEfectiva"].ToString().Substring(0,10);
                    items.fechaRegistroVenta = item["fechaRegistroVenta"].ToString();
                    items.tipoCliente = item["tipoCliente"].ToString();
                    items.tipoDocumento = item["tipoDocumento"].ToString();
                    items.nroDocumento = item["nroDocumento"].ToString();
                    items.valorVenta = item["valorVenta"].ToString();
                    items.tipoVenta = item["tipoVenta"].ToString();
                    items.porcentaje = item["porcentaje"].ToString();
                    items.valorComision = item["valorComision"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.estado = item["estado"].ToString();
                    items.identificacionEjecutivo = item["identificacionEjecutivo"].ToString();
                    items.NombreCompleto = item["Nombre"].ToString();
                    ventasList.Add(items);
                }
            }
            catch (Exception ex)
            {
                ventasList = null;
            }

            return ventasList;
        }

        public bool? ActualizarEstadoVenta (string idVenta, int porcentaje, decimal valorComision, string estado)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_estado_ventas(idVenta,porcentaje, valorComision, estado);
                    foreach (var item in sp)
                    {
                        respuesta = (item == 1) ? true : false;
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return respuesta;
        }

        public List<ListadoVentasE> ListadoVentasReporte(string cnn, string strEjecutivo, DateTime strFechaInicio, DateTime strFechaFin, string strEstado)
        {
            List<ListadoVentasE> ventasList = new List<ListadoVentasE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@strEjecutivo", strEjecutivo);
            parameter[1] = new SqlParameter("@strFechaInicio", strFechaInicio);
            parameter[2] = new SqlParameter("@strFechaFin", strFechaFin);
            parameter[3] = new SqlParameter("@strEstado", strEstado);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_ventas_ejecutivo_reporte", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ListadoVentasE items = new ListadoVentasE();
                    items.idVenta = item["idVenta"].ToString();
                    items.FechaVentaEfectiva = item["FechaVentaEfectiva"].ToString().Substring(0, 10);
                    items.fechaRegistroVenta = item["fechaRegistroVenta"].ToString();
                    items.tipoCliente = item["tipoCliente"].ToString();
                    items.tipoDocumento = item["tipoDocumento"].ToString();
                    items.nroDocumento = item["nroDocumento"].ToString();
                    items.valorVenta = item["valorVenta"].ToString();
                    items.tipoVenta = item["tipoVenta"].ToString();
                    items.porcentaje = item["porcentaje"].ToString();
                    items.valorComision = item["valorComision"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.estado = item["estado"].ToString();
                    items.identificacionEjecutivo = item["identificacionEjecutivo"].ToString();
                    items.NombreCompleto = item["Nombre"].ToString();
                    ventasList.Add(items);
                }
            }
            catch (Exception ex)
            {
                ventasList = null;
            }

            return ventasList;
        }

    }
}
