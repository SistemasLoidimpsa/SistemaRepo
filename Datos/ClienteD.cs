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
    public class ClienteD
    {


        //public List<InformacionClienteE> AccesoUsuario(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
        //        string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, string provinciaId,
        //        string provincia, string cantonId, string canton, string direccionEntrega, DateTime fechaNacimiento, string campoMarketing)
        //{
        //    List<UsuarioE> usuarioRoles = new List<UsuarioE>();
        //    using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
        //    {
        //        var sp = context.sp_insertar_clientes(tipoIdentificacion, numeroIdentificacion,primerNombre, segundoNombre, primerApellido, segundoApellido, sexoID, 
        //                            telefono,correo,celular, provinciaId, provincia,cantonId,canton,direccionEntrega,fechaNacimiento,campoMarketing);
        //        foreach (var item in sp)
        //        {
        //            InformacionClienteE usu = new InformacionClienteE();
        //            usu.cod_usu = item.cod_usu;
        //            usu.identificacion = item.identificacion;
        //            usu.rol = item.id_rol;
        //            usu.Nombres = item.nombre;
        //            usu.cambio_clave = item.cambio_clave;
        //            usu.fecha_cambio_clave = item.fecha_cambio_clave;
        //            usu.estado = item.estado;
        //            usuarioRoles.Add(usu);
        //        }
        //    }
        //    return usuarioRoles;
        //}


        public bool? IngresaCliente(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, string provinciaId,
                string provincia, string cantonId, string canton, string direccionEntrega, DateTime fechaNacimiento, string campoMarketing)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_insertar_clientes(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre, primerApellido, segundoApellido, sexoID,
                                    telefono, correo, celular, provinciaId, provincia, cantonId, canton, direccionEntrega, fechaNacimiento, campoMarketing);


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

        public Dictionary<string, string> Clientes()
        {
            Dictionary<string, string> trans = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var sp = context.sp_clientes();
                    foreach (var item in sp)
                    {
                        trans.Add((item.primerApellido + " " + item.segundoApellido + " " + item.primerNombre + " " + item.segundoNombre + " [" + item.numeroIdentificacion + "] "), item.numeroIdentificacion);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return trans;
        }

        public bool ActualizarDatos(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
        string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, string provinciaId,
            string provincia, string cantonId, string canton, string direccionEntrega, DateTime fechaNacimiento, string strRol
            , string strCodigoCLienteVip, string strCodUsuario)

        {

            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    context.sp_actualiza_clientes(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre,
                    primerApellido, segundoApellido, sexoID, telefono, correo, celular, provinciaId,
                    provincia, cantonId, canton, direccionEntrega, fechaNacimiento, strRol, strCodigoCLienteVip, strCodUsuario);
                    update = true;
                }
            }

            catch (Exception ex)

            {

            }
            return update;

        }

        public bool ActualizarDireccion(string tipoIdentificacion, string direccion)

        {

            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    context.sp_actualiza_direccion(tipoIdentificacion, direccion);
                    update = true;
                }
            }

            catch (Exception ex)

            {

            }
            return update;

        }
        public List<ClienteE> BuscarCliente(string cnn, string buscarCliente)
        {
            List<ClienteE> clientesList = new List<ClienteE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", buscarCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_cliente", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ClienteE items = new ClienteE();
                    items.idCasillero = item["idCasillero"].ToString();
                    items.numeroidentificacion = item["numeroIdentificacion"].ToString();
                    items.nombres = item["nombres"].ToString();
                    items.tipoCliente = item["descripcion"].ToString();
                    items.cod_usu = item["cod_usu"].ToString();
                    items.idEjectuvio = item["idEjecutivoCuenta"].ToString();
                    items.valorFob = item["valorFob"].ToString();
                    clientesList.Add(items);
                }
            }
            catch (Exception ex)
            {
                clientesList = null;
            }

            return clientesList;
        }

        public ClienteC BuscarClienteContf(string cnn, int userCliente)
        {
            ClienteC cliente = new ClienteC();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", userCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_cliente_contifico", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ClienteC items = new ClienteC();


                    items.cedula = item["cedula"].ToString();
                    items.razon_social = item["razon_social"].ToString();
                    items.telefonos = item["celular"].ToString();
                    items.direccion = item["direccionEntrega"].ToString();

                    items.tipo = item["tipo"].ToString();
                    items.email = item["correo"].ToString();
                    cliente =  (items);
                }
            }
            catch (Exception ex)
            {
                cliente = null;
            }

            return cliente;
        }
        public List<ClienteCanje> BuscarCanje(string cnn, string buscarCliente)
        {
            List<ClienteCanje> clientesList = new List<ClienteCanje>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", buscarCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_canje", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ClienteCanje items = new ClienteCanje();
                    items.numeroidentificacion = item["numeroIdentificacion"].ToString();
                    items.producto = item["nombreUnico"].ToString();
                    items.nombresCliente = item["nombres"].ToString();
                    items.codigoCanje = item["codigoCanje"].ToString();
                    items.puntosCanjeado = item["puntosUsados"].ToString();
                    items.puntosTotal = item["ptnAcumulado"].ToString();
                    clientesList.Add(items);
                }
            }
            catch (Exception ex)
            {
                clientesList = null;
            }

            return clientesList;
        }




        public List<ReporteCas> ReporteCasillero(string cnn, string strAnio)
        {
            List<ReporteCas> reporteList = new List<ReporteCas>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAnio", strAnio);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_clientes", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReporteCas items = new ReporteCas();

                    items.Mes = item["Mes"].ToString();
                    items.cantidadMes = item["cantidadMes"].ToString();
                    reporteList.Add(items);
                }
            }
            catch (Exception ex)
            {
                reporteList = null;
            }

            return reporteList;
        }

        public List<ReporteCategoriaRed> ReporteCategoriaRed(string cnn, string strAnio)
        {
            List<ReporteCategoriaRed> reporteList = new List<ReporteCategoriaRed>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAnio", strAnio);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_reporte_red_social", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ReporteCategoriaRed items = new ReporteCategoriaRed();

                    items.Mes = item["Mes"].ToString();
                    items.cantidadMes = item["cantidadMes"].ToString();
                    items.campoMarketing = item["campoMarketing"].ToString();
                    reporteList.Add(items);
                }
            }
            catch (Exception ex)
            {
                reporteList = null;
            }

            return reporteList;
        }
        public InformacionClienteE DatosClientes(string identificacion)
        {
            InformacionClienteE datosClientes = new InformacionClienteE();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_datos_clientes_actualizacion(identificacion);
                    foreach (var item in sp)
                    {
                        datosClientes.tipoIdentificacion = Convert.ToInt32(item.tipoIdentificacion);
                        datosClientes.numeroidentificacion = item.numeroIdentificacion;
                        datosClientes.primerNombre = item.primerNombre;
                        datosClientes.segundoNombre = item.segundoNombre;
                        datosClientes.primerApellido = item.primerApellido;
                        datosClientes.segundoApellido = item.segundoApellido;
                        datosClientes.sexoId = Convert.ToInt32(item.sexoID);
                        datosClientes.telefono = item.telefono;
                        datosClientes.correo = item.correo;
                        datosClientes.celular = item.celular;
                        datosClientes.provinciaID = item.provinciaId;
                        datosClientes.provincia = item.provincia;
                        datosClientes.cantonID = item.cantonId;
                        datosClientes.canton = item.canton;
                        datosClientes.direccionEntrega = item.direccionEntrega;
                        datosClientes.campoMarketing = item.campoMarketing;
                        datosClientes.id_rol = item.id_rol.ToString();
                        datosClientes.idCasillero = item.idCasillero.ToString();
                        datosClientes.cod_usu = item.cod_usu.ToString();
                        datosClientes.valorFob = item.valorFob.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return datosClientes;
        }

        public InformacionClienteCanje DatosClienteCanje(string identificacion, string codigoCanje)
        {
            InformacionClienteCanje datosClientes = new InformacionClienteCanje();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_datos_clientes_canjeo(identificacion, codigoCanje);
                    foreach (var item in sp)
                    {

                        datosClientes.numeroidentificacion = Convert.ToString(item.numeroIdentificacion);
                        datosClientes.idCangeo = item.idCangeo.ToString();
                        datosClientes.codigoCanje = item.codigoCanje;
                        datosClientes.canjeadoEstado = item.canjeadoEstado.ToString();
                        datosClientes.fechaCanjeo = item.fechaCanjeo.ToString();
                        datosClientes.nombresCompletos = (item.nombresCompletos);
                        datosClientes.nombreProducto = item.nombreUnico;
                        datosClientes.usuarioRegistroCanjeo = item.usuarioRegistroCanjeo;
                        datosClientes.puntosUsados = item.puntosUsados.ToString();
                        datosClientes.puntosAcumulado = item.ptnAcumulado.ToString();
                        datosClientes.fechaRegistroPuntos = item.fechaRegistroPuntos.ToString();
                        datosClientes.usuarioRegistroPuntos = item.usuarioRegistroPuntos;

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return datosClientes;
        }

        public List<InformacionClienteCanjeHist> DatosClienteCanjeHistorial(string cnn, string identificacion)
        {
            List<InformacionClienteCanjeHist> datosClientes = new List<InformacionClienteCanjeHist>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@numeroIdentificacion", identificacion);


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_datos_clientes_canjeo_historial", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    InformacionClienteCanjeHist datosCliente = new InformacionClienteCanjeHist();
                    datosCliente.idCangeoHist = item["idCangeoHist"].ToString();
                    datosCliente.descripHist = item["descripHist"].ToString();

                    datosCliente.puntosHist = item["puntosHist"].ToString();
                    datosCliente.fechaCanjeoHist = item["fechaCanjeoHist"].ToString();
                    datosCliente.clasePuntos = item["clasePuntos"].ToString();
                    datosCliente.nombreCompleto = item["nombreCompleto"].ToString();
                    datosCliente.cod_usu = item["cod_usu"].ToString();
                    datosClientes.Add(datosCliente);

                }

            }
            catch (Exception ex)
            {
                datosClientes = null;
            }
            return datosClientes;
        }

        public List<EnvioClienteGestion> EnviosClientes(string cnn)
        {
            List<EnvioClienteGestion> datosClientes = new List<EnvioClienteGestion>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@numeroIdentificacion", "");


            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_enivos_clientes_gestion", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) //Estudiantes
                {
                    EnvioClienteGestion datosCliente = new EnvioClienteGestion();
                    datosCliente.idEnvio = item["idEnvio"].ToString();
                    datosCliente.numSeguim = item["numSeguim"].ToString();

                    datosCliente.empresa = item["empresa"].ToString();
                    datosCliente.provincia = item["provincia"].ToString();
                    datosCliente.canton = item["canton"].ToString();
                    datosCliente.fechaRegistroCliente = item["fechaRegistroCliente"].ToString();
                    datosCliente.userRegistro = item["userRegistro"].ToString();
                    datosCliente.nombres = item["nombreCompleto"].ToString();
                    datosClientes.Add(datosCliente);

                }

            }
            catch (Exception ex)
            {
                datosClientes = null;
            }
            return datosClientes;
        }

       
        public List<EnvioClienteGestion> ListadoEnviosClienteFiltro(string cnn, DateTime datFechaIngreso, DateTime datFechaFin, string strEmpresaEnvio)
        {
            List<EnvioClienteGestion> mat = new List<EnvioClienteGestion>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@datFechaIngreso", datFechaIngreso);
            parameter[1] = new SqlParameter("@datFechaFin", datFechaFin);
            parameter[2] = new SqlParameter("@empresaEnvioFiltro", strEmpresaEnvio);

            
            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_envios_clientes_gestion_filtro", parameter);
                foreach (DataRow item in dSet.Tables[0].Rows) 
                {
                    EnvioClienteGestion datosCliente = new EnvioClienteGestion();
                    datosCliente.idEnvio = item["idEnvio"].ToString();
                    datosCliente.numSeguim = item["numSeguim"].ToString();
                    datosCliente.empresa = item["empresa"].ToString();
                    datosCliente.provincia = item["provincia"].ToString();
                    datosCliente.canton = item["canton"].ToString();
                    datosCliente.fechaRegistroCliente = item["fechaRegistroCliente"].ToString();
                    datosCliente.userRegistro = item["userRegistro"].ToString();
                    datosCliente.nombres = item["nombreCompleto"].ToString();
                    mat.Add(datosCliente);

                }
            }

            catch (Exception ex)
            {
                mat = null;
            }
            return mat;
        }


        public List<InformacionClienteE> ListadoClientes(string cnn, string strEmpleado)
        {
            List<InformacionClienteE> clientesList = new List<InformacionClienteE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strEmpleado", strEmpleado);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_listado_clientes_empleado", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    InformacionClienteE items = new InformacionClienteE();
                    items.idCasillero = item["idCasillero"].ToString();
                    items.numeroidentificacion = item["numeroIdentificacion"].ToString();
                    items.NombreCliente = item["NombreCliente"].ToString();
                    items.telefono = item["telefono"].ToString();
                    items.correo = item["correo"].ToString();
                    items.provincia = item["provincia"].ToString();
                    items.canton = item["canton"].ToString();
                    items.direccionEntrega = item["direccionEntrega"].ToString();
                    items.idEjecutivoCuenta = item["idEjecutivoCuenta"].ToString();
                    clientesList.Add(items);
                }
            }
            catch (Exception ex)
            {
                clientesList = null;
            }

            return clientesList;
        }

        public List<InformacionClienteE> ListadoClientesE(string cnn, string strCliente)
        {
            List<InformacionClienteE> clientesList = new List<InformacionClienteE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strCliente", strCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_listado_clientes_empleado_filtro", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    InformacionClienteE items = new InformacionClienteE();
                    items.idCasillero = item["idCasillero"].ToString();
                    items.numeroidentificacion = item["numeroIdentificacion"].ToString();
                    items.NombreCliente = item["NombreCliente"].ToString();
                    items.telefono = item["telefono"].ToString();
                    items.correo = item["correo"].ToString();
                    items.provincia = item["provincia"].ToString();
                    items.canton = item["canton"].ToString();
                    items.direccionEntrega = item["direccionEntrega"].ToString();
                    items.idEjecutivoCuenta = item["idEjecutivoCuenta"].ToString();
                    clientesList.Add(items);
                }
            }
            catch (Exception ex)
            {
                clientesList = null;
            }

            return clientesList;
        }

        public bool? TransferirCliente(string strCliente, string strIdEjectivo, string strIdEjecutivoNuevo)
{
    bool? respuesta = false;
    try
    {
        using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
        {
            var sp = context.sp_transferir_cliente_empleado(strCliente, strIdEjectivo, strIdEjecutivoNuevo);
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



    }
}
