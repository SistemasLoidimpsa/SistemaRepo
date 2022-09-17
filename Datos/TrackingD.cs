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
    public class TrackingD
    {
        public List<InformacionTrackingE> ListadoTrackings(string cnn, string strCliente)
        {
            List<InformacionTrackingE> List = new List<InformacionTrackingE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strCliente", strCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_trackings_cliente", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    InformacionTrackingE items = new InformacionTrackingE();
                    items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                    items.tracking = item["tracking"].ToString();
                    items.idTransportista = item["idTransportista"].ToString();
                    items.nombreTransportista = item["nombreTransportista"].ToString();
                    items.peso = item["peso"].ToString();
                    items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                    items.descripcion = item["descripcion"].ToString();
                    items.cedulaCliente = item["cedulaCliente"].ToString();
                    items.observaciones = item["observaciones"].ToString();
                    items.paqueteSeparado = item["paqueteSeparado"].ToString();
                    items.codigoCategoriaC = item["codigoCategoriaC"].ToString();
                    items.precio = item["precio"].ToString();
                    List.Add(items);
                }
            }
            catch (Exception ex)
            {
                List = null;
            }

            return List;
        }


        public List<InformacionTrackingE> EditarTrackings(string cnn, string strOrden)
        {
            List<InformacionTrackingE> List = new List<InformacionTrackingE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strOrden", strOrden);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_traer_trackings_cliente", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    InformacionTrackingE items = new InformacionTrackingE();
                    items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                    items.tracking = item["tracking"].ToString();
                    items.idTransportista = item["idTransportista"].ToString();
                    items.nombreTransportista = item["nombreTransportista"].ToString();
                    items.peso = item["peso"].ToString();
                    items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                    items.observaciones = item["observaciones"].ToString();
                    List.Add(items);
                }
            }
            catch (Exception ex)
            {
                List = null;
            }

            return List;
        }




        public List<List<InformacionTrackingE>> ListadoTrackingEstado(string cnn, string strCliente)
        {
            {
                DataSet dSet = null;
                List<List<InformacionTrackingE>> list = new List<List<InformacionTrackingE>>();
                List<InformacionTrackingE> listRecibido = new List<InformacionTrackingE>();
                List<InformacionTrackingE> listAutorizado = new List<InformacionTrackingE>();
                List<InformacionTrackingE> listAduana = new List<InformacionTrackingE>();
                List<InformacionTrackingE> listBodega = new List<InformacionTrackingE>();
                List<InformacionTrackingE> listBodegC = new List<InformacionTrackingE>();
                List<InformacionTrackingE> listFinalizado = new List<InformacionTrackingE>();
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@strCliente", strCliente);

                try
                {
                    dSet = Conexiones.EjecutaSPSQL(cnn, "sp_trackings_cliente_estados", parameter);

                    foreach (DataRow item in dSet.Tables[0].Rows) //Miami
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.peso = item["peso"].ToString();
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO":"SI";
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "MIAMI";
                        listRecibido.Add(items);


                    }

                    foreach (DataRow item in dSet.Tables[1].Rows) //Autorizado
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.peso = item["peso"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "AUTORIZADO";
                        listAutorizado.Add(items);


                    }

                    foreach (DataRow item in dSet.Tables[2].Rows) //En tránsito
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.peso = item["peso"].ToString();
                        items.precio = item["precio"].ToString();
                        items.codigoCategoriaC = (Convert.ToDouble(item["precio"].ToString()) > 400 || Convert.ToDouble(item["peso"].ToString()) > 8.6) ? "C": "B";
                        items.idImp = item["idImpu"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "ADUANA";
                        listAduana.Add(items);
                    }

                    foreach (DataRow item in dSet.Tables[3].Rows) //Bodega B
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.categoria = item["categoria"].ToString();
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "BODEGA B";
                        items.idEnvio = item["idEnvio"].ToString();
                        items.precio = item["precio"].ToString();
                        items.peso = item["peso"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                        items.descripcion = item["descripcion"].ToString();
                        listBodega.Add(items);
                    }

                    foreach (DataRow item in dSet.Tables[4].Rows) //En Bodega C
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.categoria = item["categoria"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.peso = item["peso"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "BODEGA C";
                        items.idImpC = item["idImpu"].ToString(); 
                        items.valorImp = item["valorImp"].ToString();
                        items.descripImp = item["tipoPago"].ToString();
                        items.idEnvio = item["idEnvio"].ToString();
                        items.precio = item["precio"].ToString();                       
                        items.descripcion = item["descripcion"].ToString();
                        listBodegC.Add(items);
                    }

                    foreach (DataRow item in dSet.Tables[5].Rows) //Finalizado
                    {
                        InformacionTrackingE items = new InformacionTrackingE();
                        items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                        items.tracking = item["tracking"].ToString();
                        items.idTransportista = item["idTransportista"].ToString();
                        items.nombreTransportista = item["nombreTransportista"].ToString();
                        items.peso = item["peso"].ToString();
                        items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                        items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                        items.observaciones = item["observaciones"].ToString();
                        items.estado = "FINALIZADO";
                        items.idEnvio = item["idEnvio"].ToString();
                        items.precio = item["precio"].ToString();
                    
                        items.descripcion = item["descripcion"].ToString();
                        listFinalizado.Add(items);
                    }

                    list.Add(listRecibido);
                    list.Add(listAutorizado);
                    list.Add(listAduana);
                    list.Add(listBodega);
                    list.Add(listBodegC);
                    list.Add(listFinalizado);

                }
                catch (Exception ex)
                {
                    dSet = null;
                }
                return list;
            }
        }

        public List<HistorialTrackingE> HistorialTracking(string cnn, int intNumOrdenInterna)
        {
            List<HistorialTrackingE> List = new List<HistorialTrackingE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@intNumOrdenInterna", intNumOrdenInterna);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_historial_tracking", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    HistorialTrackingE items = new HistorialTrackingE();
                    items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                    items.fechaRegistro = item["fechaRegistro"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.estadoOrden = item["estadoOrden"].ToString();
                    items.observacion = item["observacion"].ToString();
                    List.Add(items);
                }
            }
            catch (Exception ex)
            {
                List = null;
            }

            return List;
        }

        public Dictionary<string, string> EstadoCuponesProm()
        {
            Dictionary<string, string> List = new Dictionary<string, string>();
           
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_cup_desc_cab();

                    foreach (var item in sp)
                    {
                        List.Add(item.valorDescPor.ToString(), item.numCupDesc);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                List = null;
            }
            return List;
        }


        public int EstadoCuponesUser(String numCedCli, String idCupDescCab, String numOr)
        {

            
            int detCli = new int();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_cup_desc_det(numCedCli, idCupDescCab, numOr);
                    foreach (var item in sp)
                    {
                        detCli = Convert.ToInt32(item);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                detCli = 1;
            }
            return detCli;
        }
        public bool? ActualizaEnvioCliente(int? intOrdenInterna,
                                            string imgImagenDocumento,
                                             decimal valorEnvio,
                                            string usuarioRegistro,
                                            string strDescripcion, string check)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_orden_interna(intOrdenInterna, imgImagenDocumento, valorEnvio, usuarioRegistro, strDescripcion, check);
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
            int? respuesta = -1;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_ingresa_impuesto_c(tipoPago, observacion, identificacionCliente, estadoOrden, totalPeso, pagoImpuesto, numCodigoImp, usuarioRegistro);
                    foreach (var item in sp)
                    {
                        respuesta = item;
                    }

                }
            }
            catch (Exception ex)
            {
           

            }
            return respuesta;
        }


        public bool? ActualizaArchivoFactura(int? intOrdenInterna,
                                       string imgImagenDocumento,
                                      
                                       string usuarioRegistro
                                  )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_archivo_factura(intOrdenInterna, imgImagenDocumento, usuarioRegistro);
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

        public bool? ActualizaEnvioAdministrador(int? intOrdenInterna,
                                             string strEstado,
                                             string strObservaciones,
                                            string strUsuarioRegistro)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_cambio_estado_orden_administrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);

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

        public bool? EliminaEnvioAdministrador(int? intOrdenInterna,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_cambio_estado_orden_administrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);
                     foreach (var item in sp)
                    {
                        respuesta = (item==1)? true: false ;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public bool? EliminaProduc(string idCatalogo,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_elimina_producto(idCatalogo, strEstado, strObservaciones, strUsuarioRegistro);
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

        public bool? EliminaTicketResuelto(string idTicket,
                                          string strEstado,
                                          string strObservaciones,
                                         string strUsuarioRegistro)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_elimina_orden_ticket(idTicket, strEstado, strObservaciones, strUsuarioRegistro);
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

        public bool? ReversaEnvioAdministrador(int? intOrdenInterna,
                                            string strEstado,
                                            string strObservaciones,
                                           string strUsuarioRegistro)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_reverso_estado_orden_administrador(intOrdenInterna, strEstado, strObservaciones, strUsuarioRegistro);

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



        public List<InformacionTrackingE> ListadoTrackingsFiltro(string cnn, string strEstadoTrackings, string strNroOrden, string strNroTracking
                                    , DateTime? datFechaIngreso, DateTime? datFechaRecibidoMiami, string strEjecutivo, string categoria)
        {
            List<InformacionTrackingE> List = new List<InformacionTrackingE>();
            DataSet dSet = null;
            //List<double> vr_sum = null;
            SqlParameter[] parameter = new SqlParameter[7];
            parameter[0] = new SqlParameter("@strEstadoTrackings", strEstadoTrackings);
            parameter[1] = new SqlParameter("@strNroOrden", strNroOrden);
            parameter[2] = new SqlParameter("@strNroTracking", strNroTracking);
            parameter[3] = new SqlParameter("@datFechaIngreso", datFechaIngreso);
            parameter[4] = new SqlParameter("@datFechaRecibidoMiami", datFechaRecibidoMiami);
            parameter[5] = new SqlParameter("@strEjecutivo", strEjecutivo);
            parameter[6] = new SqlParameter("@strCategoria", categoria);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_listado_trackings_filtros", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    
                    InformacionTrackingE items = new InformacionTrackingE();
                    items.numeroOrdenInterno = item["numeroOrdenInterno"].ToString();
                    items.tracking = item["tracking"].ToString();
                    items.cedulaCliente = item["cedulaCliente"].ToString();
                    items.NombreCompleto = item["NombreCompleto"].ToString();
                    items.idTransportista = item["idTransportista"].ToString();
                    items.nombreTransportista = item["nombreTransportista"].ToString();
                    items.peso = item["peso"].ToString();
                    items.fechaRecibidoMiami = item["fechaRecibidoMiami"].ToString();
                    items.fechaRegistro = item["fechaRegistro"].ToString();
                    items.descripcion = item["descripcion"].ToString();
                    items.precio = item["precio"].ToString();
                    items.estado = item["estadoInicialOrden"].ToString();
                    items.observaciones = item["observaciones"].ToString();
                    items.imgFactura = item["rutaFisica"].ToString();
                    items.categoria = (Convert.ToDouble(item["peso"]) >= 8.6 || Convert.ToDouble((item["precio"].ToString() == "") ? "0": (item["precio"])) > 400) ?"C":"B";
                    items.bodega = item["bodega"].ToString();
                    /*var ele = Convert.ToDouble(item["peso"].ToString());
                    vr_sum.Add(ele);*/
                    List.Add(items);
                }
            }
            catch (Exception ex)
            {
                List = null;
            }

            return List;
        }


        public List<ReportePeso> ReportePesoAnual(string cnn, string strAnio)
        {
            List<ReportePeso> reporteList = new List<ReportePeso>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAnio", strAnio);
          


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_peso", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReportePeso items = new ReportePeso();

                    items.Mes = item["Mes"].ToString();                    
                   items.cantidadMes = Convert.ToDouble(item["cantidadMes"].ToString());
                    items.peso = item["peso"].ToString();


                    reporteList.Add(items);
                }
            }
            catch (Exception ex)
            {
                reporteList = null;
            }

            return reporteList;
        }

        public List<ReportePromTrack> ReportePromDiasTrack(string cnn, string strAnio)
        {
            List<ReportePromTrack> reporteList = new List<ReportePromTrack>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAnio", strAnio);



            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_promedio_tracking", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReportePromTrack items = new ReportePromTrack();

                    items.Mes = item["Mes"].ToString();
                    items.promeDias = item["promeDias"].ToString();
                   


                    reporteList.Add(items);
                }
            }
            catch (Exception ex)
            {
                reporteList = null;
            }

            return reporteList;
        }

        public List<CatalogoProducto> ListadoProductos(string cnn,  int estadoPoducto)
        {
            List<CatalogoProducto> mat = new List<CatalogoProducto>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@estadoPoducto", estadoPoducto);
           


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_lista_producto", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    CatalogoProducto items = new CatalogoProducto();

                    items.idCatalogo = item["idCatalogo"].ToString();
                    items.idImgCatalog = item["idImgCatalog"].ToString();
                    items.puntos = Convert.ToInt32( item["puntos"]);
                    items.cantidadProdcuto = Convert.ToInt32(item["cantidadProdcuto"]);
                    items.nombreUnico = item["nombreUnico"].ToString();
                    items.estadoProducto = item["estadoProducto"].ToString();
                    items.eliminado = item["eliminado"].ToString();
                 
                    mat.Add(items);
                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }

        public List<InformacionEnvioE> ListadoEnviosCliente(string cnn, DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strTipoEnvio)
        {
            List<InformacionEnvioE> mat = new List<InformacionEnvioE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@datFechaIngreso", datFechaIngreso);
            parameter[1] = new SqlParameter("@datFechaFin", datFechaFin);
            parameter[2] = new SqlParameter("@estadoOrdenFiltro", strOrdenEstado);
            parameter[3] = new SqlParameter("@tipoEntregaFiltro", strTipoEnvio);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_orden_envio", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    InformacionEnvioE items = new InformacionEnvioE();

                    items.idEnvio = item["idEnvio"].ToString();
                    items.tipoEntrega = item["tipoEntrega"].ToString();
                    items.categoria = item["categoria"].ToString();
                    items.fechaRegistro= item["fechaRegistro"].ToString();
                    items.usuario = item["usuarioRegistro"].ToString();
                    items.NombreCompleto = item["NombreCompleto"].ToString();
                    items.EjecutivoCuenta = item["EjecutivoCuenta"].ToString();
                    items.totalPesoEnvio = item["totalPesoEnvio"].ToString();
                    items.totalPagoEnvio = item["pagoTotalEnvio"].ToString();
                    items.estadoOrden = item["estadoOrden"].ToString();
                    items.strFile = item["archivoPago"].ToString();
                    items.cedulaNumero = item["cedulaNumero"].ToString();
                    items.id_rol = item["descripcion"].ToString();
                    items.valorEnvioDomi = item["valorEnvioDomi"].ToString();
                    items.detDesc = "%" + item["detDesc"].ToString();
                    items.tipoPago = item["tipoPago"].ToString();
                    items.pagoVerificar = item["pagoVerificar"].ToString();
                    items.observacion = item["observacion"].ToString();

                    mat.Add(items);
                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }

        

        public List<InformacionTicketE> ListadoTicketCliente(string cnn, DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strOrdenTicket)
        {
            List<InformacionTicketE> mat = new List<InformacionTicketE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@datFechaIngreso", datFechaIngreso);
            parameter[1] = new SqlParameter("@datFechaFin", datFechaFin);
            parameter[2] = new SqlParameter("@estadoOrdenFiltro", strOrdenEstado);
            parameter[3] = new SqlParameter("@numeroTicket", strOrdenTicket);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_orden_ticket", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    InformacionTicketE items = new InformacionTicketE();

                    items.idTicket = item["idTicket"].ToString();
                    items.NombreCompleto = item["NombreCompleto"].ToString();
                    items.idTracking = item["idTracking"].ToString();
                    items.nombreTransportista = item["nombreTransportista"].ToString();
                    items.descripContenido = item["descripContenido"].ToString();
                    items.descripProblema = item["descripProblema"].ToString();
                    items.nombreAgente = item["EjecutivoCuenta"].ToString();
                    items.statusTicket = item["statusTicket"].ToString();
                    items.usuarioRegistro = item["usuarioRegistro"].ToString();
                    items.fechaRegistroTicket = item["fechaRegistroTicket"].ToString();
                    mat.Add(items);
                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }
        public List<InformacionTrackingE> ListadoEnviosDetalle(string cnn, string intIDEnvio)
        {
            List<InformacionTrackingE> mat = new List<InformacionTrackingE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@intIDEnvio", intIDEnvio);
     


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_orden_envio_detalle", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    InformacionTrackingE items = new InformacionTrackingE();
                    items.numeroOrdenInterno = item["idOrden"].ToString();
                  //  items.cedulaCliente = item["cedulaCliente"].ToString();
                   // items.NombreCompleto = item["NombreCompleto"].ToString();
                    items.descripcion = item["descripcion"].ToString();
                    items.peso = item["peso"].ToString();
                    items.precio = item["precio"].ToString();
                    items.paqueteSeparado = (item["paqueteSeparado"].ToString() == "0") ? "NO" : "SI";
                    items.tracking = item["tracking"].ToString();
                    mat.Add(items);
                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }
        public Dictionary<string, string> Factura(int idEnvio)
        {
            Dictionary<string, string> payData = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_factura_values(idEnvio);
                    foreach (var item in sp)
                    {
                        payData.Add("codigoFactura", item.codigoFactura.ToString());
                        payData.Add("fechaRegistro", item.fechaRegistro.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return payData;
        }



        public Dictionary<string, string> Envio(int idEnvio)
        {
            Dictionary<string, string> payData = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_envio_values(idEnvio);
                    foreach (var item in sp)
                    {
                        payData.Add("numSeguim", item.numSeguim.ToString());
                        payData.Add("empresa", item.empresa);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return payData;
        }

        public List<InformacionTicketDetalleE> ListadoTicketDetalle(string cnn, string idTicket)
        {
            List<InformacionTicketDetalleE> mat = new List<InformacionTicketDetalleE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@idTicket", idTicket);



            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_orden_ticket_detalle", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    InformacionTicketDetalleE items = new InformacionTicketDetalleE();
                    items.idTicket = item["idTicket"].ToString();
                    items.comentario = item["comentario"].ToString();
                    items.userRegistro = item["userRegistro"].ToString();
                    items.fecha_Registro_Comentario = item["fecha_Registro_Comentario"].ToString();
                                     mat.Add(items);
                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }

        public List<CatalogoDetalle> ListaCatalogos()
        {
            List<CatalogoDetalle> catalogos = new List<CatalogoDetalle>();
            DataSet dSet = null;


            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_lista_catalogos();
                    foreach (var item in sp)
                    {
                        CatalogoDetalle m = new CatalogoDetalle();
                        m.idCatalogo = item.idCatalogo.ToString();
                        m.nombreUnico = item.nombreUnico.ToString();
                        m.idImgCatalog = item.idImgCatalog.ToString();
                        m.puntos = item.puntos.ToString();
                        m.estadoProducto = (item.estadoProducto ==0) ? "NO DISPONIBLE": "DISPONIBLE" ;


                        catalogos.Add(m);
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return catalogos;
        }
        public string GeneraCanjeo(int puntosUsados, string idCliente, int idCatalogo, string usuarioRegistro)
        {
            string respuesta = "";
            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_generar_canjeo(puntosUsados, idCliente, idCatalogo, usuarioRegistro);
                    foreach (var item in sp)
                    {
                        respuesta = item;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return respuesta;
        }


        public int? GeneraOrdenEnvio(string strTipoEntrega, string strUsuarioRegistro, string strNombreArchivoPago, string categoria, string strObservacion, string strIdentificacion, decimal pesoTotalEnvio, decimal pagoTotalEnvio, decimal valorEnvioDomi, string tipoPago, string tipoBanco, string detDesc, string valNumCed)
        {
            int? respuesta = -1;
            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_generar_orden(strTipoEntrega, strUsuarioRegistro, strNombreArchivoPago, categoria, strObservacion, strIdentificacion, pesoTotalEnvio, pagoTotalEnvio,valorEnvioDomi, tipoPago, tipoBanco, detDesc, valNumCed);

                    foreach (var item in sp)
                    {
                        respuesta = item;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return respuesta;
        }

        public int? GeneraOrdenPago(string strTipoEntrega, string strUsuarioRegistro, string strNombreArchivoPago, string categoria, string strObservacion, string strIdentificacion, decimal pesoTotalEnvio, decimal pagoTotalEnvio, string tipoPago)
        {
            int? respuesta = -1;
            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    //var sp = context.sp_generar_orden_pago(strTipoEntrega, strUsuarioRegistro, strNombreArchivoPago, categoria, strObservacion, strIdentificacion, pesoTotalEnvio, pagoTotalEnvio, tipoPago);
                    //foreach (var item in sp)
                    //{
                    //    respuesta = item;
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            return respuesta;
        }


        public bool? GuardaInformacionDetalleEnvio(int idOrdenEnvio,
                                            int idOrden
                                         )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_generar_orden_detalle(idOrdenEnvio,idOrden);

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

        public bool? GuardaInformacionDetalleImpuesto(int idImp,
                                         int idOrden)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_generar_orden_detalle_Impuesto(idImp, idOrden);

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

        public bool? ActualizaCheckOut(int intOrdenEnvio,
                                           string strObservacion, string strUsuario, string tipoPagoConfir, string numFacturaContf, string numSeg, string enviadoPor
                                        )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_orden_check_out(intOrdenEnvio, strObservacion, strUsuario, tipoPagoConfir , numFacturaContf,  numSeg,  enviadoPor);
                    foreach (var item in sp)
                    {
                        respuesta = (item==1)?true:false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public int EnvioCorreoPnts(int numOrdenInt)
        {
            
            int respuesta = 0;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_envio_email_pnts(numOrdenInt.ToString());
                    if (sp == 1)
                    {
                        respuesta = 1;
                    }else
                    {
                        respuesta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = 1;
            }
            return respuesta;
        }

        public bool? IngresoEnvio(int intOrdenEnvio,
                                     string numSeguimiento, string empresaEnvio, string userRegistro,
                                     string provincia , string ciudad
                                      )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_ingreso_envio(intOrdenEnvio, numSeguimiento, empresaEnvio, userRegistro, provincia, ciudad);
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

        public bool? IngresoFactura(int intOrdenEnvio,
                                    string codeFac, string userRegistro
                                     )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_ingreso_factura(intOrdenEnvio, codeFac,  userRegistro);
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
        public bool? ActualizaConfirm(string strconfirmPago, int intOrdenEnvio, string strUsuario, string strObservacion
                                       )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_orden_confirm_pago(strconfirmPago, intOrdenEnvio, strUsuario, strObservacion);
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


        public bool? ActualizaDebiCredi(string accion, int valor, string strUsuario, string strObservacion, string idCliente
                                    )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_debi_credi_puntos(accion, valor, strUsuario, strObservacion, idCliente);
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
        public bool? ActualizaResueltoTicket(string idTicket, string strStatusTicket,
                                     string strObservacion, string strUsuario
                                  )
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_orden_ticket(idTicket,  strObservacion, strStatusTicket,  strUsuario);
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