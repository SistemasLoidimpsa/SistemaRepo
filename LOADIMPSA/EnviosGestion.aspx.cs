using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Configuration;

namespace LOADIMPSA
{
    public partial class EnviosGestion : System.Web.UI.Page
    {
        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
        UsuarioN usuN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {

                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        DateTime fechaFin = DateTime.Now;
                        txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
                        DateTime fechaInicio = fechaFin.AddMonths(-1);
                        txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                        if (item.rol == 1 || item.rol == 4)
                        {




                        }
                        else if (item.rol == 2 || item.rol == 6 || item.rol == 8 || item.rol == 9)
                        {

                        }
                        else if (item.rol == 3 || item.rol == 5 || item.rol == 7)
                        {

                           

                        }

                        else
                        {
                            Response.Redirect("Home.aspx");
                        }
                        DatosClientesHistorial();
                    }
                }
            }
        }

        private bool DatosClientes(string identificacion)
        {
            bool save = false;
            pnlListadoHistorial.Visible = true;
            try
            {
                clienteN = new ClienteN();
                var datos = clienteN.DatosClientes(identificacion);

            }
            catch (Exception ex)
            {

            }
            return save;
        }

        public void CargarListado(DateTime datFechaIngreso, DateTime datFechaFin, string strEmpresaEnvio)
        {
            try
            {
                clienteN = new ClienteN();
                var data = clienteN.EnviosClientesFiltro(ConfigurationManager.AppSettings["cnnSQL"], datFechaIngreso, datFechaFin, strEmpresaEnvio);
                gvEnviosClientes.DataSource = data;
                gvEnviosClientes.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(txtFechaInicio.Text))
            {
                if (!String.IsNullOrEmpty(txtFechaFin.Text))
                {
                    if(ddlEmpresaEnvio.SelectedValue == "TODOS")
                    {
                        DatosClientesHistorial();
                    }
                    else
                    {
                        CargarListado(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), ddlEmpresaEnvio.SelectedValue);
                    }
                    
                }
                else
                {
                    script = "alert('La fecha fin es obligatoria para buscar.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                }
            }
            else
            {
                script = "alert('La fecha inicio es obligatoria para buscar.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

            }
        }
        protected void dtgEnvios_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void DatosClientesHistorial()
        {

            try
            {
                clienteN = new ClienteN();
                usuN = new UsuarioN();
                var datos = clienteN.EnviosClientes(ConfigurationManager.AppSettings["cnnSQL"]);
                gvEnviosClientes.DataSource = datos;
                gvEnviosClientes.DataBind();

                pnlListadoHistorial.Visible = true;

                

            }
            catch (Exception ex)
            {

            }

        }








    }
}

