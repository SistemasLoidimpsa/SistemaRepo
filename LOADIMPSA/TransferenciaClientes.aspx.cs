using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
namespace LOADIMPSA
{
    public partial class ListadoTracking : System.Web.UI.Page
    {
        ClienteN clienteN;
        EmpleadoN empleadoN;
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
                        CargarEmpleados();
                        if (item.rol == 1)
                        {
                            CargarListadoClientes(ddlEjecutivos.SelectedValue);
                            pnlBusquedaClientes.Visible = false;
                            pnlClientes.Visible = false;
                            pnlListadoClientes.Visible = true;
                            pnlGeneral.Visible = true;
                        }
                        else if (item.rol == 4)
                        {
                            dtgListadoClientes.Columns[10].Visible = false;
                            dtgListadoClientes.Columns[9].Visible = false;
                            dtgListadoClientes.Columns[4].Visible = false;
                            dtgListadoClientes.Columns[7].Visible = false;
                        }
                        else if (item.rol == 2)
                        {
                            dtgListadoClientes.Columns[10].Visible = false;
                            dtgListadoClientes.Columns[9].Visible = false;
                            dtgListadoClientes.Columns[4].Visible = false;
                            dtgListadoClientes.Columns[7].Visible = false;
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        private void CargarEmpleados()
        {
            empleadoN = new EmpleadoN();
            ddlEjecutivos.Items.Clear();
            var docentes = empleadoN.Empleados();
            ddlEjecutivos.Items.Add(new ListItem("Todos", "%"));
            foreach (var item in docentes)
            {
                ddlEjecutivos.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        private void CargarEmpleadosTransferir()
        {
            empleadoN = new EmpleadoN();
            ddlEmpleadosTransferir.Items.Clear();
            var docentes = empleadoN.Empleados();
            foreach (var item in docentes)
            {
                ddlEmpleadosTransferir.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        public void CargarListadoClientes(string strEmpleado)
        {
            clienteN = new ClienteN();
            var list = clienteN.ListadoClientes(ConfigurationManager.AppSettings["cnnSQL"], strEmpleado);
            dtgListadoClientes.DataSource = list;
            dtgListadoClientes.DataBind();
            Label4.Text = dtgListadoClientes.Rows.Count.ToString();
        }
        public void CargarListadoClientesE(string strCliente)
        {
            clienteN = new ClienteN();
            var list = clienteN.ListadoClientesE(ConfigurationManager.AppSettings["cnnSQL"], strCliente);
            dtgListadoClientes.DataSource = list;
            dtgListadoClientes.DataBind();
            Label4.Text = dtgListadoClientes.Rows.Count.ToString();
        }

        protected void ddlEjecutivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListadoClientes(ddlEjecutivos.SelectedValue);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                gvClientes.DataSource = null;
                gvClientes.DataBind();
                if (!string.IsNullOrWhiteSpace(txtCliente.Text.Trim()))
                {
                    pnlClientes.Visible = true;
                  
                    var lista = new ClienteN().BuscarCliente(ConfigurationManager.AppSettings["cnnSQL"], txtCliente.Text.Trim());
                    gvClientes.DataSource = lista;
                    gvClientes.DataBind();
                    gvClientes.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {

            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gv = gvClientes.SelectedRow;

            pnlListadoClientes.Visible = true;
            CargarListadoClientesE(Server.HtmlDecode(gv.Cells[1].Text));
        }
        protected void btnTransferir_Click(object sender, ImageClickEventArgs e)
        {
            mdlPopup.Show();
            CargarEmpleadosTransferir();
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[2].ToString();
            hddIdEjecutivo.Value = commandArgs[1].ToString();
            ddlEmpleadosTransferir.SelectedValue = hddIdEjecutivo.Value;
            hddIdentificacion.Value = commandArgs[0].ToString();
            btnTransferirCliente.Visible = true;
            btnTransferirClienteMasivo.Visible = false;
        }



        protected void btnTransferirCliente_Click(object sender, EventArgs e)
        {
            clienteN = new ClienteN();
            if ((bool)clienteN.TransferirCliente(hddIdentificacion.Value, hddIdEjecutivo.Value, ddlEmpleadosTransferir.SelectedValue))
            {
                script = "alert('El cliente: " + lbltitulo2.Text + " ha sido transferido exitosamente al Empleado: " + ddlEmpleadosTransferir.SelectedItem + ".');";
                ScriptManager.RegisterStartupScript(pnlListadoClientes, pnlListadoClientes.GetType(), "script", script, true);
                CargarEmpleados();
                ddlEjecutivos_SelectedIndexChanged(null, null);
            }
            else
            {
                script = "alert('Ocurrio un error comuniquese con el Administrador.');";
                ScriptManager.RegisterStartupScript(pnlListadoClientes, pnlListadoClientes.GetType(), "script", script, true);

            }
        }

        //Cambio de Clientes Masivo
        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkRowTodos_CheckedChanged(object sender, EventArgs e)
        {
            GridView grid = sender as GridView;
            bool check = false;
            foreach (var item in dtgListadoClientes.HeaderRow.Cells[0].Controls)
            {
                if (item is CheckBox)
                {
                    check = ((CheckBox)item).Checked;
                }
            }

            foreach (GridViewRow gr in dtgListadoClientes.Rows)
            {
                foreach (Control ctr in gr.Cells[0].Controls)
                {
                    if (ctr is CheckBox)
                    {
                        ((CheckBox)ctr).Checked = check;
                    }
                }
            }
        }

        protected void btnTransferirClientes_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            CargarEmpleadosTransferir();
            lbltitulo2.Text = string.Empty;
            lblTituloV.Text = "TRANSFERIR CLIENTES MASIVO";
            btnTransferirCliente.Visible = false;
            btnTransferirClienteMasivo.Visible = true;
        }

        protected void btnTransferirClienteMasivo_Click(object sender, EventArgs e)
        {
            int contadorClientes = 0;
            foreach (GridViewRow gr in dtgListadoClientes.Rows)
            {
                foreach (Control ctr in gr.Cells[0].Controls)
                {
                    if (ctr is CheckBox)
                    {
                        if (((CheckBox)ctr).Checked)
                        {
                            string strCliente = (gr.FindControl("lblIdentificacionCliente") as Label).Text;
                            string strIdEjecutivoCuenta = (gr.FindControl("lblIdEjectivoCuenta") as Label).Text;
                            clienteN = new ClienteN();
                            if ((bool)clienteN.TransferirCliente(strCliente, strIdEjecutivoCuenta, ddlEmpleadosTransferir.SelectedValue))
                            {
                                contadorClientes++;
                            }
                        }
                    }
                }
            }

            script = "alert('Se han Transferido:" + contadorClientes + " al ejecutivo: " + ddlEmpleadosTransferir.SelectedItem + ".');";
            ScriptManager.RegisterStartupScript(pnlListadoClientes, pnlListadoClientes.GetType(), "script", script, true);
            CargarListadoClientes(ddlEjecutivos.SelectedValue);
        }
    }
}