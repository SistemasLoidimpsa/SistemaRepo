using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ProductoN
    {
        ProductoD productoD;

        public List<ProductoE> BuscarProductoContf(string cnn, int filtro)
        {
            return new ProductoD().BuscarProductoContf(cnn, filtro);
        }
        public CobroTr BuscarCobrosContf(string cnn, int filtro)
        {
            return new ProductoD().BuscarCobrosContf(cnn, filtro);
        }
        public string ObtenerDescrip(string cnn, int filtro)
        {
            return new ProductoD().ObtenerDescrip(cnn, filtro);
        }
        public string ObteneDocumentoFac(string cnn)
        {
            return new ProductoD().ObteneDocumentoFac(cnn);
        }
    }
}
