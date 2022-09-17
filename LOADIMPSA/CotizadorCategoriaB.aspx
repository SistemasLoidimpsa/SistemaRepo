<%@ Page Title="Cotizador B" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="CotizadorCategoriaB.aspx.cs" Inherits="LOADIMPSA.CotizadorCategoriaB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 44 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
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

        .auto-style2 {
            width: 50%;
            height: 179px;
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
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="COTIZADOR CATEGORIA B" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" Font-Size="9px" CssClass="btn btn-primary btn-lg" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
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
                            <td colspan="2">
                                <asp:Label ID="lblInformacion" runat="server" Text="INFORMACIÓN CLIENTE" Font-Bold="True" Font-Size="11px" ForeColor="White"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblNombresCom" ForeColor="Black" runat="server" CssClass="subtituloHeader"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCedula" runat="server" Text="Cedula:" CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lBlCed" ForeColor="Black" runat="server" CssClass="subtituloHeader"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCarrera" runat="server" Text="Código de Cliente:" CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblCodCliente" runat="server" ForeColor="Black" CssClass="subtituloHeader"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlCotizador" runat="server" HorizontalAlign="Center" Width="100%" Height="100%" ScrollBars="Auto" Visible="false">
                    <br />
                    <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                        <table style="margin-left: auto; margin-right: auto; width: 80%">
                            <tr style="background: #1D4289">
                                <td colspan="4">
                                    <asp:Label ID="Label1" runat="server" Text="COTIZADOR CATEGORIA B" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; margin-left: auto; margin-right: auto" colspan="3" class="auto-style2">
                                    <br />
                                    <asp:Label ID="Label7" runat="server" Text="Descripción de la Mercaderia:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtDescripcionMercaderia" class="textboxNormal" TextMode="MultiLine" Rows="3" Columns="80" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <asp:Label ID="Label4" runat="server" Text="Valor Factura:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtFactura" class="textboxNormal" runat="server" CssClass="form-control"
                                        OnTextChanged="txtFactura_TextChanged" AutoPostBack="true"
                                        Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>
                                <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left" colspan="2">
                                    <asp:Label ID="Label5" runat="server" Text="Peso en Libras:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPeso" class="textboxNormal" runat="server" CssClass="form-control"
                                        Width="80%" onkeypress="return isNumberKey(event)"
                                        OnTextChanged="txtPeso_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="ENTREGA A DOMICILIO A NIVEL NACIONAL (**) :" Font-Size="Medium" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                </td>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="lblServicioDomicilio" runat="server" Text="$ 5" CssClass="subtituloHeader" ForeColor="Black" Font-Size="Large"></asp:Label>
                                    <br />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label6" runat="server" Text="SERVICIO COURIER CATEGORIA B :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                </td>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label11" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="Large"></asp:Label>
                                    <asp:Label ID="lblServicioCorrier" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="Large"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="TOTAL IMPORTACIÓN UNIDAD (*): " Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                </td>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label10" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblSumatoria" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; margin-left: auto; margin-right: auto; width: 33%" colspan="4">
                                    <br />
                                    <asp:Label ID="Label8" runat="server" Text="* Los valores detallados son calculados en base a un peso y valor aproximados calculados en base a un costo y peso referencial verificados en internet. Estos pueden sufrir variaciones."
                                        CssClass="subtituloHeader"></asp:Label><br />
                                    <br />
                                    <asp:Label ID="Label9" runat="server" Text="(*) Valor cotizado no incluye entrega a domicilio." CssClass="subtituloHeader"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; margin-left: auto; margin-right: auto; width: 33%" colspan="4">
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" BackColor="#1D4289" Text="Registrar Cotización" OnClick="btnRegistrar_Click" />
                                    <br />
                                    <br /><br /><br /><br /><br /><br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
