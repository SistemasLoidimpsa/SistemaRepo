<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ListadoTracking.aspx.cs" Inherits="LOADIMPSA.ListadoTracking1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 44 && charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;

            }
            else {
                if ((evt.target.value.search(/\,/) > -1 || evt.target.value.search(/\./) > -1) && (charCode == 46 || charCode == 44)) {
                    return false;
                }
                return true;
            }
        }

        function ValidaComas(value) {
            if (value.includes(",")) {
                return false;
            }
        }

    </script>
    <style type="text/css">
        #overlay {
            position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: #000;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=20);
            opacity: 0.2;
        }

        #theprogress {
            padding: 10px;
            background-color: #000;
            filter: Alpha(Opacity=20);
            line-height: 30px;
            text-align: center;
        }

        #modalprogress {
            top: 40%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
            position: fixed;
            z-index: 999;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            padding-top: 10px;
            padding-left: 10px;
        }

        .auto-style1 {
            height: 25px;
        }
    </style>
    <link href="css/GridStyle.css" rel="stylesheet" />
    <style>
        .tituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #fff;
            font-size: 20px;
        }

        .subtituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 9px;
            font-weight: bold;
        }

        .textboxNormal {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 9px;
            font-weight: bold;
        }


        .hideGridColumn {
            display: none;
        }

        .header {
            background-color: #1D4289;
            font-weight: bold;
            color: white;
            text-align: center;
            align-content: center;
        }

        .MiButton {
            border-color: #1D4289;
            border-style: solid;
            color: white;
            font-family: 'Montserrat';
            font-weight: bold;
            background: #243D75;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppListadoTracking">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress" style="width: 250px; height: 250px">
                    <div id="theprogress">
                        <asp:Label ID="imgLoader" runat="server" Text=" Por favor espere......" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uppListadoTracking" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server">
                <div style="text-align: center; width: 90%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="ADMINISTRADOR DE TRACKINGS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="No. Orden - Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
                    </asp:Panel>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel runat="server" ID="pnlClientes" Width="100%" HorizontalAlign="Center" ScrollBars="Vertical">
                    <asp:Panel runat="server" Width="50%" HorizontalAlign="Center" Style="margin-left: auto; margin-right: auto">
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                            BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="numeroIdentificacion" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#D5D8DC" />
                            <AlternatingRowStyle CssClass="alt" />
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlDatosCliente" runat="server" Width="100%" Visible="False">
                    <br />
                    <table style="width: 50%; margin-right: auto; margin-left: auto; text-align: center; border: double">
                        <tr style="background: #1D4289">
                            <td colspan="3">
                                <asp:Label ID="lblInformacion" runat="server" Text="INFORMACIÓN CLIENTE" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblNombresCom" ForeColor="Black" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCedula" runat="server" Text="Cedula:" CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lBlCed" ForeColor="Black" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCarrera" runat="server" Text="Código de Cliente:" CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblCodCliente" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlListadoTracking" runat="server" HorizontalAlign="Center" Width="100%" Height="600px" ScrollBars="Auto">
                    <br />
                    <asp:GridView ID="dtgListadoTracking" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True" Width="95%" CssClass="mGrid"
                        BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                        FooterStyle-BackColor="#D5D8DC"
                        EmptyDataText="No Existen trackings para este Cliente"
                        BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:TemplateField HeaderText="Nro. Orden Interna">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("numeroOrdenInterno") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("numeroOrdenInterno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                            <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" />
                            <asp:TemplateField HeaderText="TRANSPORTISTA">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("nombreTransportista") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("nombreTransportista") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="peso" HeaderText="PESO" />
                            <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA RECIBIDO EN MIAMI" />
                            <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />

                            <asp:TemplateField HeaderText="Ver Historial">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnHistorial" OnClick="btnHistorial_Click"
                                        CommandArgument='<%# Eval("numeroOrdenInterno")+";"+Eval("nombreTransportista")%>'
                                        ImageUrl="~/images/iconoHistorial.png" ToolTip="Ver Historial" Width="20px" />
                                </ItemTemplate>
                                <ControlStyle Width="40px" />
                                <HeaderStyle CssClass="fa-header" Width="40px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnEditar" OnClick="btnEditar_Click"
                                        CommandArgument='<%# Eval("numeroOrdenInterno") + ";" + Eval("nombreTransportista") + ";" + Eval("tracking") + ";" + Eval("idTransportista") + ";" + Eval("peso") + ";" + Eval("fechaRecibidoMiami") + ";" + Eval("descripcion") + ";" + Eval("cedulaCliente") + ";" + Eval("observaciones") +";"+Eval("paqueteSeparado") +";"+Eval("codigoCategoriaC")+";"+Eval("precio")%>'
                                        ImageUrl="~/images/edit.png" ToolTip="Editar" Width="20px" />
                                </ItemTemplate>
                                <ControlStyle Width="40px" />
                                <HeaderStyle CssClass="fa-header" Width="40px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#D5D8DC" />
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </asp:Panel>
                <br />
                <br />
                <asp:Button ID="hButton" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlPopup" runat="server" Width="800px" Height="100%" Style="display: none" CssClass="modalPopup">
                    <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlHistorial" runat="server" Width="100%" Visible="false">
                                <table style="width: 100%">
                                    <tr style="background-color: #1D4289">
                                        <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                            <asp:Label ID="lblTituloV" Text="HISTORIAL DE ORDEN / TRANSPORTISTA" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                            <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: justify; padding: 2em">
                                            <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Visible="false" Text="Escoja el empleado a transferir:"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label3" runat="server" Text="Historial de Tracking:" CssClass="subtituloHeader"></asp:Label>
                                            <br />
                                            <asp:GridView ID="dtgHistorialTracking" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="95%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                FooterStyle-BackColor="#D5D8DC"
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="Nro. Orden" />
                                                    <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="usuarioRegistro" HeaderText="Usuario" />
                                                    <asp:BoundField DataField="estadoOrden" HeaderText="Estado" />
                                                    <asp:BoundField DataField="observacion" HeaderText="Observaciones" />

                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                            <br />
                                        </td>
                                    </tr>
                                </table>

                            </asp:Panel>


                            <%--  Modal Popu 2 Editar--%>
                            <asp:Panel ID="pnlVerOrden" runat="server" Width="100%" Visible="false">
                                <table style="margin-left: auto; margin-right: auto; width: 100%">
                                    <tr style="background: #1D4289">
                                        <td colspan="4">
                                            <asp:Label ID="Label1" runat="server" Text="INGRESO DE TRACKING" Font-Bold="True" Font-Size="10px" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">

                                            <asp:Label ID="Label2" runat="server" Text="Número Identificación:" CssClass="subtituloHeader"></asp:Label>
                                            <asp:TextBox ID="txtCedula" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                        </td>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">
                                            <asp:Label ID="Label6" runat="server" Text="Número de Orden Interno:" CssClass="subtituloHeader"></asp:Label>
                                            <asp:TextBox ID="txtOrdenInterno" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                            <asp:Label ID="Label9" runat="server" Text="Número Tracking:" CssClass="subtituloHeader"></asp:Label>
                                            <asp:TextBox ID="txtTracking" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left" colspan="2">
                                            <asp:Label ID="Label7" runat="server" Text="Peso en Libras:" CssClass="subtituloHeader"></asp:Label>

                                            <asp:TextBox ID="txtPeso" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                            <asp:Label ID="Label8" runat="server" Text="Fecha Recibido en Miami:" CssClass="subtituloHeader"></asp:Label>

                                            <asp:TextBox ID="txtFechaRecibidoMiami" class="textboxNormal" runat="server" TextMode="Date" CssClass="form-control" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left" colspan="2">

                                            <asp:Label ID="Label12" runat="server" Text="Transportista" CssClass="subtituloHeader"></asp:Label>

                                            <asp:DropDownList ID="ddlTransportista" OnSelectedIndexChanged="ddlTransportista_SelectedIndexChanged" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                            </asp:DropDownList>

                                            <asp:TextBox ID="txtOtroTransportista" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">

                                            <asp:Label ID="Label16" runat="server" Text="Precio Articulo" CssClass="subtituloHeader"></asp:Label>
                                            <br />
                                             <asp:TextBox ID="txtPrecio" class="textboxNormal" runat="server" CssClass="form-control" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            
                                           
                                            </td>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            &nbsp&nbsp
                                            <asp:Label ID="Label14" runat="server" Text="Código para Categoria C:" CssClass="subtituloHeader"></asp:Label>
                                            <br />
                                            &nbsp&nbsp
                                            <asp:DropDownList ID="ddlCategoriaC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlseparar_SelectedIndexChanged">

                                                <asp:ListItem Text="ELEGIR" Value=""></asp:ListItem>
                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                           
                                            </td>
                                        <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                            <asp:Label ID="Label15" runat="server" Text="Paquetes Separados:" CssClass="subtituloHeader"></asp:Label>
                                             <br />
                                            <asp:DropDownList ID="ddlsepararPaquete" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlseparar_SelectedIndexChanged">

                                                <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                            <asp:Label ID="Label10" runat="server" Text="Descripción del Envio:" CssClass="subtituloHeader"></asp:Label>

                                            <asp:TextBox ID="txtDescripcion" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                            <asp:Label ID="Label11" runat="server" Text="Observación:" CssClass="subtituloHeader"></asp:Label>

                                            <asp:TextBox ID="txtObservaciones" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label4" runat="server" Text="Justificación:" CssClass="subtituloHeader"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label13" runat="server" Font-Size="Small" Font-Bold="false" ForeColor="Black" Text="Justificación de cambio de estado." CssClass="subtituloHeader"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtJustifica" runat="server" Width="90%" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                        </td>

                                    </tr>

                                </table>

                            </asp:Panel>

                            <div style="margin-left: auto; margin-right: auto; text-align: center">
                                <br />
                                <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
                                <br />
                            </div>
                            <div style="margin-left: auto; margin-right: auto; text-align: right">
                                <asp:Button ID="btnActualizaOrden" runat="server" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Text="Actualiza Orden" Font-Size="9px" OnClick="btnActualiza_Click" />
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:Button ID="btnClose" runat="server" Text="Cancelar" BackColor="Black" Font-Size="Small" CssClass="btn btn-primary btn-lg" Visible="true" />
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <br />
                                <br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnlCustomerDetail">
                        <ProgressTemplate>
                            <div id="Background"></div>
                            <div id="Progress" style="color: darkred">
                                Por favor espere ......
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="hButton" PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="modalBackground" BehaviorID="_content_mdlPopup" DynamicServicePath="" />



            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
