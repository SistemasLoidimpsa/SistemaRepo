using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace LOADIMPSA
{
    public partial class RegistroCliente : System.Web.UI.Page
    {
        GeneralesN generalesN;
        ClienteN clienteN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProvincias();
                CargarCantones(ddlProvincia.SelectedValue);
            }
        }

        private void CargarProvincias()
        {
            generalesN = new GeneralesN();
            var provincias = generalesN.Provincias();
            ddlProvincia.Items.Clear();
            foreach (var item in provincias)
            {
                ddlProvincia.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlProvincia.Items.Add(new ListItem("SIN PROVINCIA", "NA"));
        }

        private void CargarCantones(string id_prov)
        {
            generalesN = new GeneralesN();
            var cantones = generalesN.Cantones(id_prov);
            ddlCantones.Items.Clear();

            foreach (var item in cantones)
            {
                ddlCantones.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlCantones.Items.Add(new ListItem("SIN CANTON", "NA"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            clienteN = new ClienteN();

            if (!String.IsNullOrEmpty(txtIdentificacion.Text))
            {
                if (!String.IsNullOrEmpty(txtPrimerNombre.Text))
                {
                     if ((!String.IsNullOrEmpty(txtprimerApellido.Text)) )
                        {
                            if (!String.IsNullOrEmpty(txtSegundoApellido.Text) || (ddlTipoIdentificacion.SelectedItem.Value.Equals("3")))
                            {
                                if (!String.IsNullOrEmpty(txtMail.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtTelefono.Text))
                                    {
                                        if (!String.IsNullOrEmpty(txtCelular.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtDireccion.Text))
                                            {
                                                if (!String.IsNullOrEmpty(txtFechaNacimiento.Text))
                                                {
                                                    if (chkAcuerdo.Checked == true)
                                                    {

                                                        if ((bool)clienteN.IngresaCliente(Convert.ToInt32(ddlTipoIdentificacion.SelectedValue.Trim()), txtIdentificacion.Text, txtPrimerNombre.Text.ToUpper().Trim(),
                                                                                   txtSegundoNombre.Text.ToUpper().Trim(), txtprimerApellido.Text.ToUpper().Trim(), txtSegundoApellido.Text.ToUpper().Trim(), Convert.ToInt32(ddlGenero.SelectedValue),
                                                                                   txtTelefono.Text, txtMail.Text, txtCelular.Text, ddlProvincia.SelectedValue, ddlProvincia.SelectedItem.Text,
                                                                                   ddlCantones.SelectedValue, ddlCantones.SelectedItem.Text, txtDireccion.Text.Trim(), Convert.ToDateTime(txtFechaNacimiento.Text),
                                                                                   ddlMarketing.SelectedValue))
                                                        {
                                                            script = "alert('El Cliente se ha registrado correctamente.');";
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                            pnlFinal.Visible = true;
                                                            pnlInfo.Visible = false;
                                                            lblNombrFinal.Text = txtPrimerNombre.Text.ToUpper() + " " + txtSegundoNombre.Text.ToUpper() + " "
                                                                + txtprimerApellido.Text.ToUpper() + " " + txtSegundoApellido.Text.ToUpper();
                                                            
                                                        }
                                                        else
                                                        {
                                                            script = "alert('Ocurrio  un error comuniquese con el Administrador.');";
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        script = "alert('Se deben aceptar los tèrminos y condiciones.');";
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                    }
                                                }
                                                else
                                                {
                                                    script = "alert('El campo dirección es obligatorio.');";
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                                }
                                            }
                                            else
                                            {
                                                script = "alert('El campo dirección es obligatorio.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                            }
                                        }
                                        else
                                        {
                                            script = "alert('El campo celular es obligatorio.');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                        }
                                    }
                                    else
                                    {
                                        script = "alert('El campo teléfono es obligatorio.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                    }
                                }
                                else
                                {
                                    script = "alert('El campo Mail es obligatorio.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                }
                            }
                            else
                            {
                                script = "alert('El campo Segundo Apellido es obligatorio.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                            }
                        }
                        else
                        {
                            script = "alert('El campo Primer Apellido es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                        }
                    
                   
                }
                else
                {
                    script = "alert('El campo Primer Nombre es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                }
            }
            else
            {
                script = "alert('El campo numero identificaciòn es obligatorio.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

            }
        }


        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCantones(ddlProvincia.SelectedValue);
        }

        protected void ddlTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        protected void txtSegundoNombre_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}