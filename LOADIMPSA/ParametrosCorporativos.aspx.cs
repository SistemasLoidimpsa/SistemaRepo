using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace LOADIMPSA
{
    public partial class ParametrosCorporativos : System.Web.UI.Page
    {
        ParametrosCorporativosN parametrosCorporativosN;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        if (item.rol == 1)
                        {
                            CargarParametros();

                        }
                    }
                }
            }

        }

        public void CargarParametros()
        {
            parametrosCorporativosN = new ParametrosCorporativosN();
            var items = parametrosCorporativosN.ListadoParametrosCorporativos();
            dtgParametrosCorporativos.DataSource = items;
            dtgParametrosCorporativos.DataBind();


        }

        protected void dtgParametrosCorporativos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        string script;
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            parametrosCorporativosN = new ParametrosCorporativosN();

            int valorint = String.IsNullOrEmpty(txtValorEntero.Text) ? 0 : Convert.ToInt32(txtValorEntero.Text);
            string valorCadena = String.IsNullOrEmpty(txtValorCadena.Text) ? "" : txtValorCadena.Text;
            decimal valordecimal = String.IsNullOrEmpty(txtValorDecimal.Text) ? 0 : Convert.ToDecimal(txtValorDecimal.Text);

            bool? res = parametrosCorporativosN.ActualizarParametroCorporativo(Convert.ToInt32(hddCodigo.Value),
                valorint,
                  valorCadena,
                  null,
                  valordecimal);

            if (res == true)
            {
                script = "alert('Se actualizo correctamente el Parametro:  " + lbltitulo2.Text + ".');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                CargarParametros();
            }
            else
            {

            }

        }

        protected void btnActualizar_Click(object sender, ImageClickEventArgs e)
        {
            mdlPopup.Show();

            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[1].ToString();
            hddCodigo.Value = commandArgs[0].ToString();
            txtValorEntero.Text = commandArgs[2].ToString();
            txtValorCadena.Text = commandArgs[3].ToString();
            if (!String.IsNullOrEmpty(commandArgs[4].ToString()))
            {
                DateTime fechaInicio = Convert.ToDateTime(commandArgs[4].ToString());
                txtFecha.Text = fechaInicio.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFecha.Text = string.Empty;
            }
      
            txtValorDecimal.Text = commandArgs[5].ToString();
        }
    }
}