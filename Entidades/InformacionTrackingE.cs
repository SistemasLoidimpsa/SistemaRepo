using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class InformacionTrackingE
    {
        public string numeroOrdenInterno { get; set; }
        public string tracking { get; set; }
        public string NombreCompleto { get; set; }
        public string cedulaCliente { get; set; }
        public string idTransportista { get; set; }
        public string nombreTransportista { get; set; }
        public string peso { get; set; }
        public string idImp { get; set; }
        public string idImpC { get; set; }
        public string codigoCategoriaC { get; set; }
        public string valorImp { get; set; }
        public string descripImp { get; set; }
        public string fechaRecibidoMiami { get; set; }
        public string fechaRegistro { get; set; }
        public string descripcion { get; set; }
        public string observaciones { get; set; }
        public string precio { get; set; }
        public string estado { get; set; }
        public string imgFactura { get; set; }
        public string idEnvio { get; set; }
        public string categoria { get; set; }
        public string paqueteSeparado { get; set; }
        public string bodega { get; set; }




    }

    public class InformacionEnvioE
    {
        public string idEnvio { get; set; }

        public string tipoEntrega { get; set; }

        public string categoria { get; set; }

        public string fechaRegistro { get; set; }

        public string usuario { get; set; }

        public string observacion { get; set; }

        public string NombreCompleto { get; set; }
        public string EjecutivoCuenta { get; set; }
        public string totalPesoEnvio { get; set; }
        public string totalPagoEnvio { get; set; }
        public string estadoOrden { get; set; }
        public string strFile { get; set; }
        public string cedulaNumero { get; set; }

        public string id_rol { get; set; }
        public string valorEnvioDomi { get; set; }
        public string detDesc { get; set; }
        public string tipoPago { get; set; }
        public string pagoVerificar { get; set; }
    }



    public class HistorialTrackingE
    {
        public string numeroOrdenInterno { get; set; }
        public string fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string estadoOrden { get; set; }
        public string observacion { get; set; }

    }

    [XmlRoot("adjunto_list")]
    public class AdjuntosList
    {
        public AdjuntosList() { Items = new List<ArchivosAdjuntoE>(); }
        [XmlElement("archivosAdjuntoE")]
        public List<ArchivosAdjuntoE> Items { get; set; }
    }

    public class ArchivosAdjuntoE
    {

        [XmlElement("nom_arc")]
        public string Nom_Arc { get; set; }

        [XmlElement("ext")]
        public string Ext { get; set; }
        [XmlElement("archivo")]
        public Byte[] Archivo { get; set; }
        [XmlElement("identificacionCliente")]
        public string identificacionCliente { get; set; }
       
        [XmlElement("nroOrden")]
        public string nroOrden { get; set; }
      


    }


}
