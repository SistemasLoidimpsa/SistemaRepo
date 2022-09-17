using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Negocio;


namespace LOADIMPSA
{
    public partial class respuesta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GeneralesN generalesN;

           
            string currurl = HttpContext.Current.Request.RawUrl;

            string referencia = Request.QueryString["referencia"];
                string moneda = Request.QueryString["moneda"];
                string valor = Request.QueryString["valor"];
                string respuesta = Request.QueryString["respuesta"];
                string cuentanro = Request.QueryString["cuentanro"];
                string metodousado = Request.QueryString["metodousado"];
                string autorizacion = Request.QueryString["autorizacion"];
                string nrotransaccion = Request.QueryString["nrotransaccion"];
                string cid = Request.QueryString["cid"];
            string idorden = Request.QueryString["idorden"];


            if (!string.IsNullOrEmpty(cid))
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);



                Dictionary<string, string> respuestaPago = new Dictionary<string, string>();
                respuestaPago.Add("respuesta", respuesta);
                respuestaPago.Add("referencia", referencia);
                respuestaPago.Add("moneda", moneda);
                respuestaPago.Add("valor", valor);
                respuestaPago.Add("cuentanro", cuentanro);
                respuestaPago.Add("metodousado", metodousado);
                respuestaPago.Add("autorizacion", autorizacion);
                respuestaPago.Add("nrotransaccion", nrotransaccion);
                respuestaPago.Add("returnUrl", currurl);

                XElement root = new XElement("respuestaPago", from keyValue in respuestaPago select new XElement(keyValue.Key, keyValue.Value)
        );
                DateTime fechaRegistro = DateTime.Now;
                var fechaTranf = fechaRegistro.ToString().Replace("/", "-").Replace(":", ".");
                string path = AppDomain.CurrentDomain.BaseDirectory + "/respuesta/";
                string pathFull = AppDomain.CurrentDomain.BaseDirectory + "/respuesta/" + cid + "-" + idorden + "-" + fechaTranf + ".txt";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!File.Exists(pathFull))
                {


                    root.Save(pathFull);
                }


                generalesN = new GeneralesN();
                bool? res = generalesN.registroPayGet(cid, idorden, fechaRegistro, respuesta);

                if (res == true)
                {

                }



            }

        }
    }
}