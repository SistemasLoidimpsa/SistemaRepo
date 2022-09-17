using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ProductoD
    {


        public List<ProductoE> BuscarProductoContf(string cnn, int userCliente)
        {
            List <ProductoE> productoList = new List<ProductoE>();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", userCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_producto_contifico", parameter);
                int cont = 3;
                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    ProductoE items = new ProductoE();


                    items.producto_id = item["id"].ToString();
                    items.cantidad = Convert.ToDouble(item["cantidad"]);
                    items.precio = Convert.ToDouble(item["precio"]);
                    items.porcentaje_iva = Convert.ToInt32(item["porcentajeIva"]);
                    items.porcentaje_descuento = Convert.ToDouble(item["porcentajeDesc"]);
                    items.base_cero = Convert.ToDouble(item["baseCero"]);
                    items.base_gravable = Convert.ToDouble(item["baseGrav"]);
                    items.base_no_gravable = Convert.ToDouble(item["basenoGrav"]);

                    /*double iva = Convert.ToDouble(item["precio"])*Convert.ToDouble(item["cantidad"])-Convert.ToDouble(item["porcentajeDesc"]);
                    if (item["id"].ToString() == "pzb8qpJ46szg2dEw" || item["id"].ToString() == "01dNxVvqwTEpOaX7") {
                        
                    }
                    else {
                        items.producto_id = item["id"].ToString();
                        items.cantidad = Convert.ToDouble(item["cantidad"]);
                        items.precio = Convert.ToDouble(item["precio"]);
                        items.porcentaje_iva = Convert.ToInt32(item["porcentajeIva"]);
                        items.porcentaje_descuento = Convert.ToDouble(item["porcentajeDesc"]);
                        items.base_cero = Convert.ToDouble(item["baseCero"]);
                        items.base_gravable = iva;
                        items.base_no_gravable = Convert.ToDouble(item["basenoGrav"]);
                    }*/
                    productoList.Add(items);
                    
                }
            }
            catch (Exception ex)
            {
                productoList = null;
            }

            return productoList;
        }

        public CobroTr BuscarCobrosContf(string cnn, int idEnvio)
        {
            CobroTr cobroTr = new CobroTr();
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", idEnvio);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_buscar_cobro_contifico", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    CobroTr items = new CobroTr();


                    items.forma_cobro = item["formaCobro"].ToString();
                    items.monto = Math.Round( Convert.ToDouble(item["pagoTotalEnvio"]),2);
                    items.fecha =Convert.ToDateTime(item["fechaRegistro"]).ToString(@"dd\/MM\/yyyy");
                    items.cuenta_bancaria_id = item["id_cuenta"].ToString();

                    cobroTr = items;
                }
            }
            catch (Exception ex)
            {
                cobroTr = null;
            }
            
            return cobroTr;
        }


        public string ObtenerDescrip(string cnn, int userCliente)
        {
            string  descripcion  = "Courier ";
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@filtro", userCliente);

            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_descripcion_contifico", parameter);

                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                  


                    descripcion+= "- "+item["idOrden"].ToString();
                  
                }
            }
            catch (Exception ex)
            {
                descripcion = null;
            }

            return descripcion;
        }
        public string ObteneDocumentoFac(string cnn)
        {
            string descripcion = "";
            DataSet dSet = null;

            SqlParameter[] parameter = new SqlParameter[1];
         
            try
            {
                dSet = Conexiones.EjecutaSPSQL(cnn, "sp_documento_factura");

                foreach (DataRow item in dSet.Tables[0].Rows)
                {



                    descripcion =  item["codigoFactura"].ToString();

                }
            }
            catch (Exception ex)
            {
                descripcion = null;
            }

            return descripcion;
        }
    }
}
