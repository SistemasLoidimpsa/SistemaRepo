using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;
using System.Data.SqlClient;
using Entidades;
namespace Datos
{
    public class EmpleadoD
    {
        public bool? IngresaEmpleado(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, int sexoID, string celular, string correo, DateTime fechaNacimiento, string telefono, int rol)
        {
            bool? respuesta = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_empleado(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre, primerApellido, segundoApellido, sexoID,
                                    celular, correo, fechaNacimiento, telefono, rol);
                    if (sp == 1)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return respuesta;
        }

        public bool InsertaOrdenInterna(int intOrdenInterno, string strTracking, string strCliente, int intIdTransportista,
                string strNombreTransportista, decimal decPeso, DateTime datFechaRecibidoMiami, string strDescripcion, string strObservaciones,
                string strUsuarioRegistro, string strImgTracking, int paqueteSeparado,string bodega)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_orden_interna(intOrdenInterno, strTracking, strCliente, intIdTransportista, strNombreTransportista, decPeso
                                                            , datFechaRecibidoMiami, strDescripcion, strObservaciones, strUsuarioRegistro, strImgTracking ,paqueteSeparado , bodega);
                    /* if (sp == 1)
                     {
                         respuesta = true;
                     }*/

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
        public bool ActualizaProducto(int idProducto, string nombre, int cantPro, int puntoPro, int estadoVisible)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_producto(idProducto, nombre, cantPro, puntoPro, estadoVisible);
                    foreach (var item in sp)
                    {
                        respuesta = (item == 1) ? true : false;
                    }



                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool ActualizaOrdenInterna(int intOrdenInterno, string strTracking, string strCliente, decimal decPeso, DateTime datFechaRecibidoMiami, string codigoCategoriaC, string strJustifica, string strDescripcion, string strObservaciones, string strUsuarioRegistro, int paqueteSeparado, decimal precio)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_actualiza_edit_orden_interna(intOrdenInterno, strTracking, strCliente, decPeso
                                                            , datFechaRecibidoMiami, codigoCategoriaC, strJustifica, strDescripcion, strObservaciones, strUsuarioRegistro, paqueteSeparado, precio);
                    foreach(var item in sp )
                     {
                        respuesta = (item == 1) ? true : false;
                    }

                  

                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool InsertaOrdenTicket(string strTracking, string strCliente, int intIdTransportista,
             string strNombreTransportista, string strDescripcion, string strContenido, string strEstadoTicket,
             string strUsuarioRegistro)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_orden_ticket(strTracking, strCliente, intIdTransportista, strNombreTransportista, strDescripcion
                                                            , strContenido, strEstadoTicket, strUsuarioRegistro);
                    /* if (sp == 1)
                     {
                         respuesta = true;v 2
                     }*/

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
        public bool InsertaProducto(string nombreProducto, int cantidadProducto, int puntosProductos,
            string strImagen)
        {
            bool respuesta = false;

            try
            {

                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_inserta_producto(nombreProducto, cantidadProducto, puntosProductos, strImagen);
                    /* if (sp == 1)
                     {
                         respuesta = true;v 2
                     }*/

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
        public Dictionary<string, string> Empleados()
        {
            Dictionary<string, string> trans = new Dictionary<string, string>();
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var sp = context.sp_ejecutivos_cuenta();
                    foreach (var item in sp)
                    {
                        trans.Add(item.numeroIdentificacion, item.Nombre);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return trans;
        }


        public List<EmpleadoB> BuscarEmpleado(string cnn, string buscarEmpleado)
        {
            List<EmpleadoB> empleadoList = new List<EmpleadoB>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", buscarEmpleado);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_empleado", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    EmpleadoB items = new EmpleadoB();

                    items.numeroidentificacion = item["numeroIdentificacion"].ToString();
                    items.nombres = item["nombres"].ToString();
                    ;

                    empleadoList.Add(items);
                }
            }
            catch (Exception ex)
            {
                empleadoList = null;
            }

            return empleadoList;
        }

        public EmpleadoC BuscarEmpleadoContf(string cnn, string buscarEmpleado)
        {
            EmpleadoC empleado = new EmpleadoC();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", buscarEmpleado);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_empleado_contifico", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    EmpleadoC items = new EmpleadoC();

                    items.cedula = item["cedula"].ToString();
                    items.razon_social = item["razon_social"].ToString();
                    items.telefonos = item["celular"].ToString();
                 
                    items.tipo = item["tipo"].ToString();
                    items.email = item["correo"].ToString();


                    empleado = (items);
                }
            }
            catch (Exception ex)
            {
                empleado = null;
            }

            return empleado;
        }

        public EmpleadosE DatosEmpleado(string identificacion)
        {
            EmpleadosE datosEmpleado = new EmpleadosE();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_datos_empleados_actualizacion(identificacion);
                    foreach (var item in sp)
                    {
                        datosEmpleado.tipoIdentificacion = Convert.ToString(item.tipoIdentificacion);
                        datosEmpleado.numeroIdentificacion = item.numeroIdentificacion;
                        datosEmpleado.primerNombre = item.primerNombre;
                        datosEmpleado.segundoNombre = item.segundoNombre;
                        datosEmpleado.primerApellido = item.primerApellido;
                        datosEmpleado.segundoApellido = item.segundoApellido;
                        datosEmpleado.sexoID = Convert.ToString(item.sexoID);
                        datosEmpleado.telefono = item.telefono;
                        datosEmpleado.correo = item.correo;
                        datosEmpleado.celular = item.celular;
                        datosEmpleado.fecha_nacimiento = Convert.ToDateTime(item.fecha_nacimiento);
                        datosEmpleado.id_rol = item.id_rol.ToString();

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return datosEmpleado;
        }

        public bool ActualizarDatosEmpleados(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
      string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, DateTime fechaNacimiento, string strRol
          , string strCodUsuario)

        {

            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    context.sp_actualiza_empleados(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre,
                    primerApellido, segundoApellido, sexoID, telefono, correo, celular, fechaNacimiento, strRol, strCodUsuario);
                    update = true;
                }
            }

            catch (Exception ex)

            {

            }
            return update;

        }

    }
}
