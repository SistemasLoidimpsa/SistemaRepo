using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class EmpleadoN
    {
        EmpleadoD empleadoD;


        public bool? IngresaEmpleado(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, int sexoID, string celular, string correo, DateTime fechaNacimiento, string telefono, int rol)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.IngresaEmpleado(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre, primerApellido, segundoApellido, sexoID,
                                    celular, correo, fechaNacimiento, telefono, rol);
        }

        public bool InsertaOrdenInterna(int intOrdenInterno, string strTracking, string strCliente, int intIdTransportista,
              string strNombreTransportista, decimal decPeso, DateTime datFechaRecibidoMiami, string strDescripcion,  string strObservaciones,
              string strUsuarioRegistro, string strImgTracking, int paqueteSeparado, string bodega)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.InsertaOrdenInterna(intOrdenInterno, strTracking, strCliente, intIdTransportista, strNombreTransportista, decPeso
                                                            , datFechaRecibidoMiami, strDescripcion, strObservaciones, strUsuarioRegistro, strImgTracking, paqueteSeparado, bodega);
        }

        public bool ActualizaOrdenInterna(int intOrdenInterno, string strTracking, string strCliente, decimal decPeso, DateTime datFechaRecibidoMiami, string codigoCategoriaC, string strJustifica, string strDescripcion, string strObservaciones, string strUsuarioRegistro, int paqueteSeparado, decimal precio)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.ActualizaOrdenInterna(intOrdenInterno, strTracking, strCliente, decPeso
                                                            , datFechaRecibidoMiami, codigoCategoriaC, strJustifica, strDescripcion, strObservaciones,  strUsuarioRegistro ,paqueteSeparado, precio);
        }

        public bool ActualizaProducto(int idProducto, string nombre, int cantPro, int puntoPro, int estadoVisible)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.ActualizaProducto(idProducto, nombre, cantPro, puntoPro
                                                            , estadoVisible);
        }

        public bool InsertaOrdenTicket( string strTracking, string strCliente, int intIdTransportista,
             string strNombreTransportista,  string strDescripcion,  string strContenido, string strEstadoTicket,
             string strUsuarioRegistro)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.InsertaOrdenTicket(strTracking, strCliente, intIdTransportista, strNombreTransportista, strDescripcion
                                                            , strContenido, strEstadoTicket, strUsuarioRegistro);
        }

        public bool InsertaProducto(string nombreProducto, int cantidadProducto, int puntosProductos,
            string strImagen)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.InsertaProducto(nombreProducto, cantidadProducto, puntosProductos, strImagen);
        }

        public Dictionary<string, string> Empleados()
        {
            empleadoD = new EmpleadoD();
            return empleadoD.Empleados();
        }

        public List<EmpleadoB> BuscarEmpleado(string cnn, string filtro)
        {
            return new EmpleadoD().BuscarEmpleado(cnn, filtro);
        }

        public  EmpleadoC BuscarEmpleadoContf(string cnn, string filtro)
        {
            return new EmpleadoD().BuscarEmpleadoContf(cnn, filtro);
        }
        public EmpleadosE DatosEmpleado(string identificacion)
        {
            empleadoD = new EmpleadoD();
            return empleadoD.DatosEmpleado(identificacion);
        }

        public bool ActualizarDatosEmpleados(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
          string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, DateTime fechaNacimiento,
             string strRol,  string strCodUsuario)

        {
            empleadoD = new EmpleadoD();


            return empleadoD.ActualizarDatosEmpleados(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre,
                    primerApellido, segundoApellido, sexoID, telefono, correo, celular,  fechaNacimiento, strRol, strCodUsuario);
        }
    }
}
