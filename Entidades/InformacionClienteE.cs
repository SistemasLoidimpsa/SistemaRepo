using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InformacionClienteE
    {
        public int tipoIdentificacion { get; set; }
        public string numeroidentificacion { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public int sexoId { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public string provinciaID { get; set; }
        public string provincia { get; set; }
        public string cantonID { get; set; }
        public string canton { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string campoMarketing { get; set; }
        public string direccionEntrega { get; set; }
        public string idCasillero { get; set; }
        public string idEjecutivoCuenta { get; set; }
        public string NombreCliente { get; set; }
        public string fechaRegistroCliente { get; set; }
        public string valorFob { get; set; }
        public string id_rol { get; set; }
        public string cod_usu { get; set; }



    }
    public class InformacionClienteCanje
    {
        public string numeroidentificacion { get; set; }
        public string idCangeo { get; set; }
        public string codigoCanje { get; set; }
        public string canjeadoEstado { get; set; }
        public string fechaCanjeo { get; set; }
        public string nombresCompletos { get; set; }
      
        public string nombreProducto { get; set; }
        public string usuarioRegistroCanjeo { get; set; }
        public string puntosUsados { get; set; }
        public string puntosAcumulado { get; set; }
        public string fechaRegistroPuntos { get; set; }
        public string usuarioRegistroPuntos { get; set; }
        



    }

    public class InformacionClienteCanjeHist
    {
        public string idCangeoHist { get; set; }
        public string descripHist { get; set; }
        public string idCliente { get; set; }
        public string puntosHist { get; set; }
        public string fechaCanjeoHist { get; set; }
        public string clasePuntos { get; set; }

        public string nombreCompleto { get; set; }
        public string cod_usu { get; set; }





    }
    public class EnvioClienteGestion
    {
        public string idEnvio { get; set; }
        public string numSeguim { get; set; }
        public string empresa { get; set; }
        public string provincia { get; set; }

        public string canton { get; set; }
        public string fechaRegistroCliente { get; set; }
        public string userRegistro { get; set; }
        public string nombres { get; set; }
    }

    public class CuponHistDetCli
    {
        public int detDesc { get; set; }
    }

}
