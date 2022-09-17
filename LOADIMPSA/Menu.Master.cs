using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LOIDIMPSA
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        UsuarioN usuN;
        string script;
        ParametrosCorporativosN parametrosCorporativosN;
     
        DataTable Menus = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    usuN = new UsuarioN();

                    List<UsuarioE> roelesUsuario = (List<UsuarioE>)Session["usuario"];
                    Lblnombre.Text = roelesUsuario[0].Nombres;
                    lblCedula.Text = roelesUsuario[0].cod_usu;

                    List<ListaPuntos> puntos = usuN.ListaPuntos(roelesUsuario[0].cod_usu);
                    if (puntos.Count == 0){
                        lblPuntos.Text = "PUNTOS:  0 pts";
                    }
                
                    else {
                        lblPuntos.Text = "PUNTOS:  +" + puntos[0].puntosObtenidos + " pts"; 
                }
                    List<ListaCasillero> codigoCasillero = usuN.ListaCasillero(roelesUsuario[0].cod_usu);
                    if (codigoCasillero.Count == 0)
                    {
                        lblCasillero.Text = "NO TIENE CASILLERO";
                    }

                    else
                    {
                        lblCasillero.Text = "CASILLERO No. LO" + codigoCasillero[0].codiCasillero;
                    }
                    
                    switch (roelesUsuario[0].rol)
                    {
                        case 1:
                            lblRol.Text += "SUPER ADMINISTRADOR";
                            break;
                        case 2:
                            lblRol.Text += "EJECUTIVO DE CUENTA";
                            break;
                        case 3:
                            lblRol.Text += "CLIENTE";
                            break;
                        case 4:
                            lblRol.Text += "ADMINISTRADOR";
                            break;
                        case 5:
                            lblRol.Text += "CLIENTE VIP";
                            break;
                        case 6:
                            lblRol.Text += "OPERACIONES";
                            break;
                        case 7:
                            lblRol.Text += "INVITADO";
                            break;
                        case 8:
                            lblRol.Text += "OPERACIONES DESPACHO";
                            break;
                        case 9:
                            lblRol.Text += "SERVICIO AL CLIENTE";
                            break;
                    }

                    List<int> roles = new List<int>();
                    foreach (var item in roelesUsuario)
                    {
                        roles.Add(item.rol);
                    }
                    string strRoles = string.Join(",", roles.ToArray());

                    usuN = new UsuarioN();
                    var menuUusario = usuN.MenuUsuarioNuevo(roelesUsuario[0].estado, strRoles);

                    Menus = ToDataTable(menuUusario);
                    this.BindMenu(Menus);
                    //QuitarNiveles(roles, menuUusario);
                    ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
                    List<ParametrosCorporativosE> parametroLink = new List<ParametrosCorporativosE>();
                    parametroLink = parametrosCorporativosN.BuscaParametrosCorporativos("LINKIMAGEN");
                    foreach (var items in parametroLink)
                    {
                        imgMenu.ImageUrl= items.valorchar.ToString();
                    }

                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            else
            {
                if (Session["usuario"] == null)
                {
                    script = "alert('Su sesion ha expirado'); ; window.location='Inicio.aspx';";
                    ScriptManager.RegisterStartupScript(null, null, "script", script, true);
                }
            }


        }


        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }




        protected void rptMenu_OnItemBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (Menus != null)
                    {
                        DataRowView drv = e.Item.DataItem as DataRowView;
                        string ID = drv["MenuId"].ToString();
                        string Title = drv["Title"].ToString();
                        DataRow[] rows = Menus.Select("ParentMenuId=" + ID);
                        if (rows.Length > 0)
                        {

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<ul id='" + Title + "' class='sub-menu collapse'>");
                            foreach (var item in rows)
                            {
                                string parentId = item["MenuId"].ToString();
                                string parentTitle = item["Title"].ToString();

                                DataRow[] parentRow = Menus.Select("ParentMenuId=" + parentId);

                                if (parentRow.Count() > 0)
                                {
                                    sb.Append("<li data-toggle='collapse' data-target='#" + parentTitle + "' class='collapsed'><a href='" + item["Url"] + "'>" + item["Title"] + "<span class='arrow'></span></a>");
                                    sb.Append("</li>");
                                }
                                else
                                {
                                    sb.Append("<li><a href='" + item["Url"] + "'>" + item["Title"] + "</a>");
                                    sb.Append("</li>");
                                }
                                sb = CreateChild(sb, parentId, parentTitle, parentRow);
                            }
                            sb.Append("</ul>");
                            (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();
                        }
                    }
                }
            }
        }

        private StringBuilder CreateChild(StringBuilder sb, string parentId, string parentTitle, DataRow[] parentRows)
        {
            if (parentRows.Length > 0)
            {
                sb.Append("<ul id='" + parentTitle + "' class='sub-menu collapse'>");
                foreach (var item in parentRows)
                {
                    string childId = item["MenuId"].ToString();
                    string childTitle = item["Title"].ToString();
                    DataRow[] childRow = Menus.Select("ParentMenuId=" + childId);

                    if (childRow.Count() > 0)
                    {
                        sb.Append("<li data-toggle='collapse' data-target='#" + childTitle + "' class='collapsed'><a href='" + item["Url"] + "'>" + item["Title"] + "<span class='arrow'></span></a>");
                        sb.Append("</li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + item["Url"] + "'>" + item["Title"] + "</a>");
                        sb.Append("</li>");
                    }
                    CreateChild(sb, childId, childTitle, childRow);
                }
                sb.Append("</ul>");

            }
            return sb;
        }

        private void BindMenu(DataTable Menus)
        {

            //Menus = GetData("SELECT [MenuId], [ParentMenuId], [Title], [Description], [Url],[CssFont] FROM [Menus]");
            DataView view = new DataView(Menus);
            view.RowFilter = "ParentMenuId=0";
            this.rptCategories.DataSource = view;
            this.rptCategories.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuPrincipal.aspx");
        }
    }
}