using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;


namespace Negocio
{
    public class CotizacionN
    {
        public CotizadoresE InsertaCotizacionB(string strNombre, string strCorreo,string strCliente, string strCategoria, string strDescripcionMercancia,
            decimal decValorFOF, decimal decPeso, decimal decServicioCourier, decimal totalEnvio, string strUsuarioRegistro
            , decimal nacionalizacion, decimal flete, decimal impuestoSenae, decimal decValorEnvioDomicilio)

        {
            return new CotizadoresD().InsertaCotizacionB(strNombre, strCorreo,strCliente, strCategoria, strDescripcionMercancia,
                decValorFOF, decPeso, decServicioCourier, totalEnvio, strUsuarioRegistro, nacionalizacion, flete, impuestoSenae, decValorEnvioDomicilio);

        }
        //Modificacion para CLC
        public CotizadoresE InsertaCotizacionClc(string nombre, string correo, string strCliente, string strTipo, string strDescripcionMercancia,
           string importador, decimal decPeso, string puertoOrigen, string puertoDestino, string strUsuarioRegistro
           , int diasTranscurrido, decimal metrosCubicos, decimal fleteInternacional, decimal gastoSpot, decimal coFee, decimal iva, decimal totalGasto, decimal totalCotiza, DateTime fechaExpiraCotizacion, string obs)

        {
            return new CotizadoresD().InsertaCotizacionClc(nombre, correo, strCliente, strTipo, strDescripcionMercancia,
                importador, decPeso, puertoOrigen, puertoDestino, strUsuarioRegistro, diasTranscurrido, metrosCubicos, fleteInternacional,
                gastoSpot, coFee, iva, totalGasto, totalCotiza, fechaExpiraCotizacion, obs);

        }
        public bool InsertaRegistroVenta(DateTime fechaVentaEfectiva, string tipoCliente, string tipoDocumento, string nroDocumento, decimal valorVenta,
                        string tipoVenta, int porcentaje, decimal valorComision, string usuarioRegistro, string indentificacionEjecutivo, 
                        string identificacionCliente, string strDescripcionVenta, decimal decValorVentaSinIVA)
        {
            return new CotizadoresD().InsertaRegistroVenta(fechaVentaEfectiva, tipoCliente, tipoDocumento, nroDocumento, valorVenta, tipoVenta, porcentaje,
                                                        valorComision, usuarioRegistro, indentificacionEjecutivo, identificacionCliente, strDescripcionVenta
                                                        , decValorVentaSinIVA);

        }

        public List<ListadoVentasE> ListadoClientes(string cnn, string strEjecutivo, DateTime strFechaInicio, DateTime strFechaFin)
        {
            return new CotizadoresD().ListadoClientes(cnn, strEjecutivo,  strFechaInicio, strFechaFin);
        }

        public bool? ActualizarEstadoVenta(string idVenta, int porcentaje, decimal valorComision, string estado)
        {
            return new CotizadoresD().ActualizarEstadoVenta(idVenta, porcentaje,valorComision,estado);
        }

        public List<ListadoVentasE> ListadoVentasReporte(string cnn, string strEjecutivo, DateTime strFechaInicio, DateTime strFechaFin, string strEstado)
        {
            return new CotizadoresD().ListadoVentasReporte(cnn, strEjecutivo, strFechaInicio,strFechaFin,strEstado);
        }
    }
}
