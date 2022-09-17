using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace LOADIMPSA
{
    public partial class RegistroEmpleado : System.Web.UI.Page
    {
        GeneralesN generalesN;
        EmpleadoN empleadoN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();

            }
          
        }

        private void CargarRoles()
        {
            generalesN = new GeneralesN();
            var cantones = generalesN.RolesAdministrativos();
            ddlRoles.Items.Clear();

            foreach (var item in cantones)
            {
                ddlRoles.Items.Add(new ListItem(item.Key, item.Value));
            }
          
        }

        public void LimpiarCajas()
        {
            txtIdentificacion.Text = string.Empty;
            txtPrimerNombre.Text = string.Empty;
            txtSegundoNombre.Text = string.Empty;
            txtprimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlTipoIdentificacion.SelectedIndex = 0;
            CargarRoles();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            empleadoN = new EmpleadoN();

            if (!String.IsNullOrEmpty(txtIdentificacion.Text))
            {
                if (!String.IsNullOrEmpty(txtPrimerNombre.Text))
                {
                    if (!String.IsNullOrEmpty(txtSegundoNombre.Text) || (ddlTipoIdentificacion.SelectedItem.Value.Equals("3")))
                    {
                        if (!String.IsNullOrEmpty(txtprimerApellido.Text) )
                        {
                            if (!String.IsNullOrEmpty(txtSegundoApellido.Text) || (ddlTipoIdentificacion.SelectedItem.Value.Equals("3")))
                            {
                                if (!String.IsNullOrEmpty(txtCelular.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtMail.Text))
                                    {
                                        if (!String.IsNullOrEmpty(txtFechaNacimiento.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtTelefono.Text))
                                            {
                                                bool respuesta = (bool)empleadoN.IngresaEmpleado(Convert.ToInt32(ddlTipoIdentificacion.SelectedValue), txtIdentificacion.Text, txtPrimerNombre.Text.ToUpper(),
                                                                                  txtSegundoNombre.Text.ToUpper(), txtprimerApellido.Text.ToUpper(), txtSegundoApellido.Text.ToUpper(),
                                                                                  Convert.ToInt32(ddlGenero.SelectedValue),
                                                                                  txtCelular.Text, txtMail.Text, Convert.ToDateTime(txtFechaNacimiento.Text),
                                                                                  txtTelefono.Text, Convert.ToInt32(ddlRoles.SelectedValue));
                                                if (respuesta)
                                                {

                                                    script = "alert('El Empleado se ha registrado correctamente.');";
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                    LimpiarCajas();

                                                }
                                                else
                                                {
                                                    script = "alert('Ya se ha registrado un Empleado con ese numero de Identificación.');";
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                    LimpiarCajas();
                                                }
                                            }
                                            else
                                            {
                                                script = "alert('El campo Teléfono es obligatorio.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                            }
                                        }

                                        else
                                        {
                                            script = "alert('El campo Fecha de Nacimiento es obligatorio.');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                        }
                                    }
                                    else
                                    {
                                        script = "alert('El campo Correo es obligatorio.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                                    }
                                }
                                else
                                {
                                    script = "alert('El campo Celular es obligatorio.');";
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
                        script = "alert('El campo Segundo Nombre es obligatorio.');";
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
    }
}