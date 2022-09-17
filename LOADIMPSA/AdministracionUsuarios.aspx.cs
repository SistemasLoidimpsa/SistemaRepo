using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace LOADIMPSA
{
    public partial class AdministracionUsuarios : System.Web.UI.Page
    {
        UsuarioN usuN;
        GeneralesN generalesN;
        string script;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    CargarRoles();
                    CargarUsuarios();

                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;
                        if (item.rol == 2 || item.rol == 4 || item.rol == 6 || item.rol == 9)
                        {

                           
                          
                            dtgUsuarios.Columns[4].Visible = false;
                            dtgUsuarios.Columns[5].Visible = false;
                            dtgUsuarios.Columns[6].Visible = false;
                            dtgUsuarios.Columns[7].Visible = false;




                        }
                       

                    }
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        private void CargarRoles()
        {
            generalesN = new GeneralesN();
            var cantones = generalesN.RolesGeneral();
            ddlRoles.Items.Clear();

            ddlRoles.Items.Add(new ListItem("TODOS", "*"));
            foreach (var item in cantones)
            {
                ddlRoles.Items.Add(new ListItem(item.Key, item.Key));
            }

        }
        private void CargarUsuarios()
        {
            usuN = new UsuarioN();
            var usuarios = usuN.ListadoUsuarios();
            dtgUsuarios.DataSource = usuarios;
            dtgUsuarios.DataBind();
        }

        protected void btnResetear_Click(object sender, EventArgs e)
        {
            int cont = 0;
            foreach (GridViewRow gr in dtgUsuarios.Rows)
            {
                foreach (Control ctr in gr.Cells[0].Controls)
                {
                    if (ctr is CheckBox)
                    {
                        if (((CheckBox)ctr).Checked)
                        {
                            usuN = new UsuarioN();
                            if (!usuN.ReseteoClave(Server.HtmlDecode(gr.Cells[2].Text), Server.HtmlDecode(gr.Cells[1].Text)
                                , Server.HtmlDecode(gr.Cells[2].Text)))
                            {

                            }
                            else
                            {
                                cont++;
                            }
                        }
                    }
                }
            }
            script = "alert('Se han reseteado " + cont + " claves');";
            ScriptManager.RegisterStartupScript(uppUsuarios, uppUsuarios.GetType(), "script", script, true);
        }


        protected void dtgUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gr = dtgUsuarios.Rows[e.RowIndex];
            int id_rol = 3;
            switch (Server.HtmlDecode(gr.Cells[4].Text))
            {
                case "Contabilidad":
                    id_rol = 8;
                    break;
                case "Coordinador de Carrera":
                    id_rol = 7;
                    break;
                case "Gestor de Cartera":
                    id_rol = 6;
                    break;
                case "Administrador Talento Humano":
                    id_rol = 5;
                    break;
                case "Estudiante Ultimo Nivel":
                    id_rol = 4;
                    break;
                case "Estudiante":
                    id_rol = 3;
                    break;
                case "Docente":
                    id_rol = 2;
                    break;
                case "Administrador":
                    id_rol = 1;
                    break;
            }
            bool estado = false;
            switch (Server.HtmlDecode(gr.Cells[5].Text))
            {
                case "Activo":
                    estado = false;
                    break;
                case "Inactivo":
                    estado = true;
                    break;
            }

            usuN = new UsuarioN();
            if (!usuN.CambioEstadoUsuario(estado, Server.HtmlDecode(gr.Cells[1].Text),
                Server.HtmlDecode(gr.Cells[2].Text), id_rol))
            {
                script = "alert('Ocurrio un error, consulte al administrador');";
                ScriptManager.RegisterStartupScript(uppUsuarios, uppUsuarios.GetType(), "script", script, true);
            }
            else
            {
                CargarRoles();
                CargarUsuarios();
            }
        }

        protected void btnBorrarCliente_Click(object sender, ImageClickEventArgs e)
        {
            string confirmvalue = Request.Form["confirm_value"];
            var respuesta = confirmvalue.Split(',');
            if (respuesta.LastOrDefault() == "Yes")
            {

                string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
                usuN = new UsuarioN();
                var respuestas = usuN.EliminaClienteEmpleado(commandArgs[0].ToString());
                if (respuestas == true)
                {
                    script = "alert('El Cliente:" + commandArgs[1].ToString() + " se a eliminado correctamente.');";
                    ScriptManager.RegisterStartupScript(uppUsuarios, uppUsuarios.GetType(), "script", script, true);
                    CargarRoles();
                    CargarUsuarios();
                }
            }
        }
    }
}