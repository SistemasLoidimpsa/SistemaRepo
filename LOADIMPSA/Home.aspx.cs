using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace LOIDIMPSA
{
    public partial class Home : System.Web.UI.Page
    {
        UsuarioN usuN;

        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            txtCodigo.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            usuN = new UsuarioN();
            var usuarioRoles = usuN.AccesoUsuario(txtCodigo.Text, txtPass.Text);
            if (usuarioRoles.Count != 0)
            {
                Session["usuario"] = usuarioRoles;
                if (usuarioRoles[0].cambio_clave != null && Convert.ToBoolean(usuarioRoles[0].cambio_clave))
                {
                    
                    if (usuarioRoles[0].estado == true)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }
                    else
                    {
                        script = "alert('Usuario o clave incorrectos o usuario inactivo, consulte con el administrador');";
                        ScriptManager.RegisterStartupScript(this,this.GetType(), script, script, true);
                    }
                }
                else
                {
                    Response.Redirect("ActualizacionClave.aspx");
                }
            }
            else
            {
                script = "alert('Usuario o clave incorrectos o usuario inactivo, consulte con el administrador');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
        }
    }
}