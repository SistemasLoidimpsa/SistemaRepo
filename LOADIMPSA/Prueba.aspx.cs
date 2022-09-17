using System;
using System.Collections.Generic;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Configuration;

namespace LOADIMPSA
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    // Get these record from database
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[] {
                         new DataColumn("Id"), new DataColumn("Catagory"), new DataColumn("Details"),
                     new DataColumn("Image"), new DataColumn("Price") });

                    dt.Rows.Add(1, "Camera", "Nicon", "Image1.jpg", "$ 104.04");
                    dt.Rows.Add(2, "Laptop", "HP 455 14\"", "Image1.jpg", "$6.74");
                    dt.Rows.Add(3, "TV", "Samsung 32\" EH4000G", "Image1.jpg", "$ 39.18");
                    dt.Rows.Add(4, "Coffee", "Nescafe Piccolo", "Image1.jpg", "$ 18.88");
                    dlProducts.DataSource = dt;
                    dlProducts.DataBind();

                    String id = Request.QueryString["content"];
                    lblPrueba.Text = id;
                }
            }
            else
            {
                Response.Redirect("Prueba.aspx");
            }
        }


        protected void btnRegistroCheckOut_Click(object sender, ImageClickEventArgs e)
        {

            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblPrueba.Text = commandArgs[1].ToString();


        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "content")
            {
                lblPrueba.Text = e.CommandArgument.ToString();
                Response.Redirect("Prueba.aspx?content=" + e.CommandArgument.ToString());
            }
        }
    }
}