using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InformacionTicketE
    {

        public string idTicket { get; set; }
        public string NombreCompleto { get; set; }
        public string idTracking { get; set; }
        public string usuarioRegistro { get; set; }
        public string nombreTransportista { get; set; }
        public string nombreAgente { get; set; }
        public string descripContenido { get; set; }
        public string descripProblema { get; set; }

        public string statusTicket { get; set; }

        public string fechaRegistroTicket { get; set; }


    }
    public class CatalogoProducto
    {

        public string idCatalogo { get; set; }
        public string idImgCatalog { get; set; }
        public int  puntos { get; set; }
        public string nombreUnico { get; set; }
        public int cantidadProdcuto { get; set; }
        public string estadoProducto { get; set; }
        public string eliminado { get; set; }
       


    }
    public class InformacionTicketDetalleE
    {
        public string idTicket { get; set; }

        public string comentario { get; set; }
        public string userRegistro { get; set; }
        public string fecha_Registro_Comentario { get; set; }






    }
    public class CatalogoDetalle
    {
        public string idCatalogo { get; set; }

        public string idImgCatalog { get; set; }
        public string puntos { get; set; }
        public string nombreUnico { get; set; }
        public string codigoCange { get; set; }
        public string cantidadProdcuto { get; set; }
        public string estadoProducto { get; set; }



    }

    public class CupHistCab
    {
        public int  idCupDescCap { get; set; }
        public string numCupDesc { get; set; }

        public int  valorDescPor { get; set; }
    }
}