using Entidades;
using Negocio;
using System;
using System.Collections.Generic;

namespace LOADIMPSA
{
    public partial class ActualizacionClave : System.Web.UI.Page
    {
        UsuarioN usuN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
            }
            else
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }

        protected void btnIngreso_Click(object sender, EventArgs e)
        {
            List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
            usuN = new UsuarioN();
            if (usuN.ActualizaClave(usuE[0].cod_usu, txtPassAnti.Text, txtPassNew.Text))
            {
                script = "alert('Clave actualizada');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                Response.Redirect("Home.aspx");
            }
            else
            {
                script = "alert('Los datos proporcionados no son correctos');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
        }
    }
}