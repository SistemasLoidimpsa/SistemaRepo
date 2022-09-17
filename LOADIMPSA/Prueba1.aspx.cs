using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Entidades;
using Negocio;

namespace LOADIMPSA
{
    public partial class Prueba1 : System.Web.UI.Page
    {
        TrackingsN trackingsN;
        ICollection CreateDataSource()
        {
            

            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow dr;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("puntos", typeof(String)));
            dt.Columns.Add(new DataColumn("estado", typeof(String)));
            dt.Columns.Add(new DataColumn("imagen", typeof(String)));

            // Populate the table with sample values.
            trackingsN = new TrackingsN();
            List<string> lisOfEstaos = new List<string>();

            var catalogos = trackingsN.ListaCatalogos();
            foreach (var item in catalogos)
            {
                dr = dt.NewRow();

                dr[0] = item.idCatalogo;
                dr[1] = item.nombreUnico;
                dr[2] = item.puntos;
                dr[3] = item.estadoProducto;
               dr[4] = item.idImgCatalog;

                dt.Rows.Add(dr);
                lisOfEstaos.Add(item.estadoProducto);
            }
            for (int i = 0; i < 9; i++)
            {
                dr = dt.NewRow();

                dr[0] = i;
                dr[1] = "Description for item " + i.ToString();
                dr[2] = 1.23 * (i + 1);
                dr[3] = "Image" + i.ToString() + ".jpg";

                dt.Rows.Add(dr);
            }

            DataView dv = new DataView(dt);
            return dv;
        }


        protected void Page_Load(Object sender, EventArgs e)
        {

            // Load sample data only once, when the page is first loaded.
            if (!IsPostBack)
            {
                ItemsList.DataSource = CreateDataSource();
                ItemsList.DataBind();
                if (Session["respuesta"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", Session["respuesta"].ToString(), true);
                    Session["respuesta"] = null;
                }
                if (Session["usuario"] != null)
                {

                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                  //  CargarCatalogos();
                    foreach (var item in usuE)
                    {

                        if (item.rol == 1)
                        {

                            hddCodigoUsuario.Value = item.cod_usu;

                            //ddlMetodoPago.Items[2].Enabled = false;


                        }
                        else if (item.rol == 2 || item.rol == 4 || item.rol == 6)
                        {

                            hddCodigoUsuario.Value = item.cod_usu;



                        }
                        else if (item.rol == 3 || item.rol == 5 || item.rol == 7)
                        {

                            hddIdentificacion.Value = item.identificacion;

                            hddCodigoUsuario.Value = item.cod_usu;


                        }

                        Session["rol"] = item.rol;
                        Session["rolUser"] = item.rol;


                        Session["listAdjuntos"] = new List<ArchivosAdjuntoE>();
                    }
                }
            }

        }

        //protected void Button_Click(Object sender, EventArgs e)
        //{

        //    // Set the repeat direction based on the selected value of the
        //    // DirectionList DropDownList control.
        //    ItemsList.RepeatDirection =
        //        (RepeatDirection)DirectionList.SelectedIndex;

        //    // Set the repeat layout based on the selected value of the
        //    // LayoutList DropDownList control.
        //    ItemsList.RepeatLayout = (RepeatLayout)LayoutList.SelectedIndex;

        //    // Set the number of columns to display based on the selected
        //    // value of the ColumnsList DropDownList control.
        //    ItemsList.RepeatColumns = ColumnsList.SelectedIndex;

        //    // Show or hide the gridlines based on the value of the
        //    // ShowBorderCheckBox property. Note that gridlines are displayed
        //    // only if the RepeatLayout property is set to Table.
        //    if ((ShowBorderCheckBox.Checked)
        //        && (ItemsList.RepeatLayout == RepeatLayout.Table))
        //    {
        //        ItemsList.BorderWidth = Unit.Pixel(1);
        //        ItemsList.GridLines = GridLines.Both;
        //    }
        //    else
        //    {
        //        ItemsList.BorderWidth = Unit.Pixel(0);
        //        ItemsList.GridLines = GridLines.None;
        //    }

        //}
        protected void btnRegistroCheckOut_Click(object sender, CommandEventArgs e)
        {

            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
           // lbl1.Text = commandArgs[0];
            trackingsN = new TrackingsN();
            string codigoSolicitud = trackingsN.GeneraCanjeo(Convert.ToInt32(commandArgs[1]), hddIdentificacion.Value, Convert.ToInt32( commandArgs[0]), hddCodigoUsuario.Value);
            if (codigoSolicitud == "")
            {
                String script = "alert('¡No tienes Puntos Sufiencientes o No esta disponible Producto!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }
            else
            {

                Session["respuesta"] = "alert('¡Felicitaciones tu canje fue Exitoso!. Tu código de canje es el siguiente: " + codigoSolicitud + " . Ingresalo al momento de generar tu orden de Retiro');";
                Response.Redirect("LugarCanje.aspx");


            }



        }

        private void CreateDataTable()
        {
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataList DataList1 = (DataList)e.Item.FindControl("DataList1");
               
            }
        }





        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "content")
            {
               // lblPrueba.Text = e.CommandArgument.ToString();
                Response.Redirect("Prueba.aspx?content=" + e.CommandArgument.ToString());
            }
        }

        protected void Button_Click(Object sender, EventArgs e)
        {

            // Set the repeat direction based on the selected value of the
            // DirectionList DropDownList control.
            ItemsList.RepeatDirection =
                (RepeatDirection)DirectionList.SelectedIndex;

            // Set the repeat layout based on the selected value of the
            // LayoutList DropDownList control.
            ItemsList.RepeatLayout = (RepeatLayout)LayoutList.SelectedIndex;

            // Set the number of columns to display based on the selected
            // value of the ColumnsList DropDownList control.
            ItemsList.RepeatColumns = ColumnsList.SelectedIndex;

            // Show or hide the gridlines based on the value of the
            // ShowBorderCheckBox property. Note that gridlines are displayed
            // only if the RepeatLayout property is set to Table.
            if ((ShowBorderCheckBox.Checked)
                && (ItemsList.RepeatLayout == RepeatLayout.Table))
            {
                ItemsList.BorderWidth = Unit.Pixel(1);
                ItemsList.GridLines = GridLines.Both;
            }
            else
            {
                ItemsList.BorderWidth = Unit.Pixel(0);
                ItemsList.GridLines = GridLines.None;
            }

        }
    }

  
}