using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ClienteN
    {
        ClienteD clienteD;

        public bool? IngresaCliente(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
           string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, string provinciaId,
               string provincia, string cantonId, string canton, string direccionEntrega, DateTime fechaNacimiento, string campoMarketing)
        {
            clienteD = new ClienteD();
            return clienteD.IngresaCliente(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre, primerApellido, segundoApellido, sexoID,
                                    telefono, correo, celular, provinciaId, provincia, cantonId, canton, direccionEntrega, fechaNacimiento, campoMarketing);
        }

        public Dictionary<string, string> Clientes()
        {
            clienteD = new ClienteD();
            return clienteD.Clientes();
        }



        public List<ClienteE> BuscarCliente(string cnn, string filtro)
        {
            return new ClienteD().BuscarCliente(cnn, filtro);
        }
        public ClienteC BuscarClienteContf(string cnn, int filtro)
        {
            return new ClienteD().BuscarClienteContf(cnn, filtro);
        }

        public List<ClienteCanje> BuscarCanje(string cnn, string filtro)
        {
            return new ClienteD().BuscarCanje(cnn, filtro);
        }


        //Buscar los casilleros
        public List<ReporteCas> ReporteCasillero(string cnn, string strAnio)
        {
            return new ClienteD().ReporteCasillero(cnn, strAnio);
        }

        public List<ReporteCategoriaRed> ReporteCategoriaRed(string cnn, string strAnio)
        {
            return new ClienteD().ReporteCategoriaRed(cnn, strAnio);
        }

        public InformacionClienteE DatosClientes(string identificacion)
        {
            clienteD = new ClienteD();
            return clienteD.DatosClientes(identificacion);
        }

        public InformacionClienteCanje DatosClienteCanje(string identificacion, string codigoCanje)
        {
            clienteD = new ClienteD();
            return clienteD.DatosClienteCanje(identificacion, codigoCanje);
        }
        public List<InformacionClienteCanjeHist> DatosClienteCanjeHistorial(string cnn, string identificacion)
        {
            clienteD = new ClienteD();
            return clienteD.DatosClienteCanjeHistorial( cnn, identificacion);
        }
        public List<EnvioClienteGestion> EnviosClientes(string cnn)
        {
            clienteD = new ClienteD();
            return clienteD.EnviosClientes(cnn);
        }

        public List<EnvioClienteGestion> EnviosClientesFiltro(string cnn,  DateTime datFechaIngreso, DateTime datFechaFin, string strEmpresaEnvio)
        {
            clienteD = new ClienteD();
            return clienteD.ListadoEnviosClienteFiltro(cnn, datFechaIngreso, datFechaFin, strEmpresaEnvio);
        }


        public bool ActualizarDatos(int tipoIdentificacion, string numeroIdentificacion, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, int sexoID, string telefono, string correo, string celular, string provinciaId,
                string provincia, string cantonId, string canton, string direccionEntrega, DateTime fechaNacimiento, 
                string campoMarketing,string  strCodigoCLienteVip, string strCodUsuario)

        {
            clienteD = new ClienteD();


            return clienteD.ActualizarDatos(tipoIdentificacion, numeroIdentificacion, primerNombre, segundoNombre,
                    primerApellido, segundoApellido, sexoID, telefono, correo, celular, provinciaId,
                    provincia, cantonId, canton, direccionEntrega, fechaNacimiento, campoMarketing, strCodigoCLienteVip, strCodUsuario);
        }

        public bool ActualizarDireccion(string tipoIdentificacion, string direccion)

        {
            clienteD = new ClienteD();
            return clienteD.ActualizarDireccion(tipoIdentificacion, direccion);
        }
        public List<InformacionClienteE> ListadoClientes(string cnn, string strEmpleado)
        {
            clienteD = new ClienteD();
            return clienteD.ListadoClientes(cnn, strEmpleado);
        }
        public List<InformacionClienteE> ListadoClientesE(string cnn, string strCliente)
        {
            clienteD = new ClienteD();
            return clienteD.ListadoClientesE(cnn, strCliente);
        }
        public void  ListadoConsultaReporte(string cnn, string strAnio)
        {
           
        }

        public bool? TransferirCliente(string strCliente, string strIdEjectivo, string strIdEjecutivoNuevo)
        {
            clienteD = new ClienteD();
            return clienteD.TransferirCliente(strCliente, strIdEjectivo,strIdEjecutivoNuevo);
        }
    }
}
