using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
namespace LOADIMPSA
{
    public partial class RecuperacionClave : System.Web.UI.Page
    {

        UsuarioN usuN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
            usuN = new UsuarioN();
            if (usuN.RecuperacionClaveMail(txtCedulaUsuario.Text.Trim()))
            {
                script = "alert('La clave fue enviada a su correo electrónico personal.');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                btnRecuperar.Enabled = false;
                btnRecuperar.Visible = false;
                lblCedula.Visible = false;
                txtCedulaUsuario.Visible = false;
                lblMensaje.Visible = true;
            }
            else
            {
                script = "alert('Los datos proporcionados no son correctos');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
        }
    }
}