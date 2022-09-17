using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class TrackingsN
    {
        TrackingD trackingD;
        public List<InformacionTrackingE> ListadoTrackings(string cnn, string strCliente)
        {
            trackingD = new TrackingD();
            return trackingD.ListadoTrackings(cnn, strCliente);
        }

        public List<InformacionTrackingE> EditarTrackings(string cnn, string strOrden)
        {
            trackingD = new TrackingD();
            return trackingD.EditarTrackings(cnn, strOrden);
        }

        public List<List<InformacionTrackingE>> ListadoEnviosEstado(string cnn, string query)
        {
            return new TrackingD().ListadoTrackingEstado(cnn, query);
        }

        public List<HistorialTrackingE> HistorialTracking(string cnn, int intNumOrdenInterna)
        {
            trackingD = new TrackingD();
            return trackingD.HistorialTracking(cnn, intNumOrdenInterna);
        }

        public bool? ActualizaEnvioCliente(int? intOrdenInterna,
                                           string imgImagenDocumento,
                                           decimal valorEnvio,
                                          string usuarioRegistro, string strDescripcion, string check)
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaEnvioCliente(intOrdenInterna,imgImagenDocumento,valorEnvio,usuarioRegistro,strDescripcion, check);
        }

        public int? IngresoImpuestoAduana(int intOrdenInterna,
                                            string tipoPago,
                                          string observacion,
                                          string identificacionCliente,
                                         string estadoOrden,
                                         decimal totalPeso,
                                          decimal pagoImpuesto, 
                                          string numCodigoImp,
                                          string usuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.IngresoImpuestoAduana(intOrdenInterna, tipoPago, observacion, identificacionCliente, estadoOrden, totalPeso, pagoImpuesto, numCodigoImp, usuarioRegistro);
        }



        public bool? ActualizaArchivoFactura(int? intOrdenInterna,
                                           string imgImagenDocumento,
                                      
                                          string usuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaArchivoFactura(intOrdenInterna, imgImagenDocumento, usuarioRegistro);
        }

        public bool? ActualizaEnvioAdministrador(int? intOrdenInterna,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaEnvioAdministrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);
        }

        public bool? EliminaEnvioAdministrador(int? intOrdenInterna,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        { 
            trackingD = new TrackingD();
            return trackingD.EliminaEnvioAdministrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);
        }


        public bool? EliminaProduc(string idProducto,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.EliminaProduc(idProducto, strEstado, strObservaciones, strUsuarioRegistro);
        }
        public bool? EliminaTicketResuelto(string idTicket,
                                    string strEstado,
                                    string strObservaciones,
                                   string strUsuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.EliminaTicketResuelto(idTicket, strEstado, strObservaciones, strUsuarioRegistro);
        }

        public bool? ReversaEnvioAdministrador(int? intOrdenInterna,
                                           string strEstado,
                                           string strObservaciones,
                                          string strUsuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.ReversaEnvioAdministrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);
        }


        
        public List<InformacionTrackingE> ListadoTrackingsFiltro(string cnn, string strEstadoTrackings, string strNroOrden, string strNroTracking
                                   , DateTime? datFechaIngreso, DateTime? datFechaRecibidoMiami, string strEjecutivo, string categoria)
        {
            trackingD = new TrackingD();
            return trackingD.ListadoTrackingsFiltro(cnn,strEstadoTrackings, strNroOrden, strNroTracking, datFechaIngreso, datFechaRecibidoMiami, strEjecutivo, categoria);
        }

        //Peso Consulta
        public List<ReportePeso> ReportePesoAnual(string cnn, string strAnio)
        {
            trackingD = new TrackingD();
            return trackingD.ReportePesoAnual(cnn, strAnio);
        }
        public List<ReportePromTrack> ReportePromDiasTrack(string cnn, string strAnio)
        {
            trackingD = new TrackingD();
            return trackingD.ReportePromDiasTrack(cnn, strAnio);
        }

        public List<InformacionEnvioE> ListadoEnviosCliente(string cnn, DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strTipoEnvio)
        {
            trackingD = new TrackingD();
            return trackingD.ListadoEnviosCliente(cnn, datFechaIngreso, datFechaFin , strOrdenEstado, strTipoEnvio);
        }

        public List<InformacionTicketE> ListadoTicketCliente(string cnn, DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strOrdenTicket)
        {
            trackingD = new TrackingD();
            return trackingD.ListadoTicketCliente(cnn, datFechaIngreso, datFechaFin, strOrdenEstado, strOrdenTicket);
        }

        public List<CatalogoProducto> ListadoProductos(string cnn, int estadoProducto)
        {
            trackingD = new TrackingD();
            return trackingD.ListadoProductos(cnn, estadoProducto);
        }
        public List<InformacionTrackingE> ListadoEnviosDetalle(string cnn, string idEnvio)

        {
            trackingD = new TrackingD();
            return trackingD.ListadoEnviosDetalle(cnn, idEnvio);
        }

        public Dictionary<string, string> Envio(int idEnvio)
        {
            trackingD = new TrackingD();
            return trackingD.Envio(idEnvio);
        }
        public int EnvioCorreo(int numOrdInt)
        {
            trackingD = new TrackingD();
            return trackingD.EnvioCorreoPnts(numOrdInt);
        }
        public int EstadoCuponesUser(String nameUser, String idCupDescCab, String numOr)
        {
            trackingD = new TrackingD();
            return trackingD.EstadoCuponesUser(nameUser, idCupDescCab,  numOr);
        }
        public Dictionary<string, string> EstadoCuponesProm()
        {
            trackingD = new TrackingD();
            return trackingD.EstadoCuponesProm();
        }
        public Dictionary<string, string> Factura(int idEnvio)
        {
            trackingD = new TrackingD();
            return trackingD.Factura(idEnvio);
        }
        public List<InformacionTicketDetalleE> ListadoTicketDetalle(string cnn, string idTicket)

        {
            trackingD = new TrackingD();
            return trackingD.ListadoTicketDetalle(cnn, idTicket);
        }

        public List<CatalogoDetalle> ListaCatalogos()
        {
            trackingD = new TrackingD();
            return trackingD.ListaCatalogos();
        }

        public int? GeneraOrdenEnvio(string strTipoEntrega, string strUsuarioRegistro, string strNombreArchivoPago, string categoria, string strObservacion, string strCliente, decimal pesoTotalEnvio, decimal pagoTotalEnvio, decimal valorEnvioDomi, string tipoPago, string tipoBanco, String detDesc, string valNumCed)
        {
            trackingD = new TrackingD();
            return trackingD.GeneraOrdenEnvio(strTipoEntrega, strUsuarioRegistro, strNombreArchivoPago, categoria, strObservacion, strCliente, pesoTotalEnvio, pagoTotalEnvio, valorEnvioDomi, tipoPago, tipoBanco, detDesc, valNumCed);
        }
        public  string GeneraCanjeo(int puntosUsados, string idCliente, int idCatalogo, string usuarioRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.GeneraCanjeo(puntosUsados, idCliente, idCatalogo, usuarioRegistro);
        }
        public int? GeneraOrdenPago(string strTipoEntrega, string strUsuarioRegistro, string strNombreArchivoPago, string categoria, string strObservacion, string strCliente, decimal pesoTotalEnvio, decimal pagoTotalEnvio, string tipoPago)
        {
            trackingD = new TrackingD();
            return trackingD.GeneraOrdenPago(strTipoEntrega, strUsuarioRegistro, strNombreArchivoPago, categoria, strObservacion, strCliente, pesoTotalEnvio, pagoTotalEnvio, tipoPago);
        }

        public bool? GuardaInformacionDetalleEnvio(int idOrdenEnvio,
                                         int idOrden)
        { 
            trackingD = new TrackingD();
            return trackingD.GuardaInformacionDetalleEnvio(idOrdenEnvio, idOrden);
        }

        public bool? GuardaInformacionDetalleImpuesto(int idImp,
                                       int idOrden)
        {
            trackingD = new TrackingD();
            return trackingD.GuardaInformacionDetalleImpuesto(idImp, idOrden);
        }

        public bool? ActualizaCheckOut(int intOrdenEnvio,
                                        string strObservacion, string strUsuario,string tipoPagoConfir , string numFacturaContf, string numSeg, string enviadoPor
                                     )
        { 
             trackingD = new TrackingD();
            return trackingD.ActualizaCheckOut(intOrdenEnvio, strObservacion,strUsuario , tipoPagoConfir, numFacturaContf,  numSeg,  enviadoPor);
        }
        public bool? IngresoEnvio(int intOrdenEnvio, string numSeguimiento,
                                     string empresaEnvio, string userRegistro,
                                     string provincia, string ciudad)
        {
            trackingD = new TrackingD();
            return trackingD.IngresoEnvio(intOrdenEnvio, numSeguimiento, empresaEnvio,  userRegistro, provincia, ciudad);
        }

        public bool? IngresoFactura(int intOrdenEnvio, string codFactura,
                                     string userRegistro)
        {
            trackingD = new TrackingD();
            return trackingD.IngresoFactura(intOrdenEnvio, codFactura, userRegistro);
        }

        public bool? ActualizaConfirm(string strconfirmPago, int intOrdenEnvio, string strUsuario, string strObservacion
                                   )
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaConfirm(strconfirmPago, intOrdenEnvio,  strUsuario, strObservacion);
        }
        public bool? ActualizaDebiCredi(string accion,int valor, string strUsuario, string strObservacion, string idCliente
                                 )
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaDebiCredi(accion, valor, strUsuario, strObservacion, idCliente);
        }

        public bool? ActualizaResueltoTicket(string idTicket, string srtStatusTicket,
                                     string strObservacion, string strUsuario
                                  )
        {
            trackingD = new TrackingD();
            return trackingD.ActualizaResueltoTicket(idTicket, srtStatusTicket,  strObservacion, strUsuario);
        }
    }
}
