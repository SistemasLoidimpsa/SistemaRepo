<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="AdministraciónCotizaciones.aspx.cs" Inherits="LOADIMPSA.AdministraciónCotizaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            position: center;
            left: 20%;
            right: 20%;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            width: 200px;
            height: 350px;
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

        .auto-style2 {
            width: 50%;
            height: 179px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppAdministracionCotizadores">
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
    <asp:UpdatePanel ID="uppAdministracionCotizadores" runat="server">
        <ContentTemplate>
            <link rel="Stylesheet" href="css/TabStyles.css" type="text/css" media="screen" />
            <asp:Panel runat="server">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="REGISTRO DE VENTAS EJECUTIVOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <br />
                <cc1:TabContainer ID="tabDatos" runat="server" ActiveTabIndex="0" CssClass="Tab" Width="100%" AutoPostBack="true">
                    <cc1:TabPanel ID="tpnlRegistrarVentas" runat="server" HeaderText="Registrar Ventas" TabIndex="1">
                        <ContentTemplate>
                            <br />
                            <br />
                            <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                                <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                                    <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" /><asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
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
                            <asp:Panel ID="pnlDatosEstudiante" runat="server" Width="100%" Visible="False">
                                <br />
                                <table style="width: 50%; margin-right: auto; margin-left: auto; text-align: center; border: double">
                                    <tr style="background: #1D4289">
                                        <td colspan="2">
                                            <asp:Label ID="lblInformacion" runat="server" Text="INFORMACIÓN CLIENTE" Font-Bold="True" Font-Size="9px" ForeColor="White"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: center">
                                            <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="subtituloHeader" Font-Bold="True"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblNombresCom" ForeColor="Black" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblCedula" runat="server" Text="Cedula:" CssClass="subtituloHeader" Font-Bold="True"></asp:Label></td>
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
                            <asp:Panel ID="pnlRegistroVenta" runat="server" Visible="False" Width="100%">
                                <table style="margin-left: auto; margin-right: auto; width: 75%">
                                    <tr style="background: #1D4289">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="REGISTRO DE VENTA" Font-Bold="True" Font-Size="9px" ForeColor="White"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="lblEmpleados" runat="server" Text="EMPLEADO:" CssClass="subtituloHeader"></asp:Label><br />
                                            <br />
                                            <asp:DropDownList ID="ddlEjecutivos" runat="server" Width="80%" AutoPostBack="True"></asp:DropDownList><br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label28" runat="server" Text="DESCRIPCIÓN DE LA VENTA:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:TextBox ID="txtDescripcionVenta" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label2" runat="server" Text="FECHA VENTA EFECTIVA:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:TextBox ID="txtFechaVentaEfectiva" class="textboxNormal" runat="server" CssClass="form-control" Width="40%" TextMode="Date"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label4" runat="server" Text="TIPO CLIENTE:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:DropDownList ID="ddlTipoCliente" runat="server" CssClass="form-control" Width="40%">
                                                <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                                <asp:ListItem Text="Importadora" Value="Importadora"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label3" runat="server" Text="DOCUMENTO REGISTRO VENTA:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:DropDownList ID="ddlRegistroVenta" runat="server" CssClass="form-control" Width="40%">
                                                <asp:ListItem Text="Seleccione" Value="*" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Id Cotización" Value="ID"></asp:ListItem>
                                                <asp:ListItem Text="Declaración" Value="DECLARACION"></asp:ListItem>
                                                <asp:ListItem Text="Declaración de Importación" Value="DECLARACION"></asp:ListItem>
                                                <asp:ListItem Text="Id de Tracking" Value="TRACKING"></asp:ListItem>
                                                <asp:ListItem Text="Número Factura" Value="FACTURA"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label5" runat="server" Text="DOCUMENTO:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:TextBox ID="txtDocumento" class="textboxNormal" runat="server" CssClass="form-control" Width="40%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label29" runat="server" Text="VALOR VENTA SIN IVA:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:TextBox ID="txtValorVentaSinIva" class="textboxNormal" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" Width="40%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label6" runat="server" Text="PROFIT:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:TextBox ID="txtValorVenta" class="textboxNormal" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" Width="40%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label7" runat="server" Text="TIPO VENTA:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:DropDownList ID="ddlTipoVenta" runat="server" CssClass="form-control" Width="40%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoVenta_SelectedIndexChanged">
                                                <asp:ListItem Text="Seleccione" Value="*"></asp:ListItem>
                                                <asp:ListItem Text="Categoria C" Value="Categoria C"></asp:ListItem>
                                                <asp:ListItem Text="Flete Internacional" Value="Flete Internacional"></asp:ListItem>
                                                <asp:ListItem Text="Flete Local" Value="Flete Local"></asp:ListItem>
                                                <asp:ListItem Text="Póliza de Seguro" Value="Poliza de Seguro"></asp:ListItem>
                                                <asp:ListItem Text="Despacho Consumo" Value="Despacho Consumo"></asp:ListItem>
                                                <asp:ListItem Text="Otros" Value="Otros"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label8" runat="server" Text="PORCENTAJE:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:DropDownList ID="ddlPorcentaje" runat="server" CssClass="form-control" AutoPostBack="True" Width="20%" OnSelectedIndexChanged="ddlPorcentaje_SelectedIndexChanged">
                                                <asp:ListItem Text="5 %" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="15 %" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label9" runat="server" Text="VALOR A GANAR:" CssClass="subtituloHeader"></asp:Label><br />
                                            <asp:Label ID="Label10" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblValorComision" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="13px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; margin-left: auto; margin-right: auto; width: 33%">
                                            <br />
                                            <br />
                                            <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Font-Size="9px" Text="Registrar Venta" OnClick="btnRegistrar_Click" /><br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpnlAdministrador" runat="server" HeaderText="Administrador Ventas" TabIndex="0">
                        <ContentTemplate>
                            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                                <table style="width: 50%">
                                    <tr>
                                        <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                            <br />
                                            <asp:Label ID="Label11" runat="server" Text="Empleados:" CssClass="subtituloHeader"></asp:Label><br />
                                            <br />
                                            <asp:DropDownList ID="ddlEjecutivosAdministrador" runat="server" Width="80%" AutoPostBack="True" OnSelectedIndexChanged="ddlEjecutivosAdministrador_SelectedIndexChanged"></asp:DropDownList><br />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table style="width: 60%; text-align: left">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio: " CssClass="subtituloHeader"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" class="textboxNormal" AutoPostBack="true" OnTextChanged="txtFechaInicio_TextChanged"></asp:TextBox></td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Fecha Fin: " CssClass="subtituloHeader"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" class="textboxNormal" AutoPostBack="true" OnTextChanged="txtFechaFin_TextChanged"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="pnlListadoCotizaciones" runat="server" HorizontalAlign="Center" Width="100%" Height="600px" ScrollBars="Auto">
                                <br />
                                <asp:GridView ID="dtgListadoVenta" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    OnRowDataBound="dtgListadoVenta_RowDataBound"
                                    ShowHeader="true"
                                    Font-Size="Small"
                                    EmptyDataText="No Existen ventas asignadas a este Ejecutivo"
                                    BorderWidth="1px" CellPadding="3">
                                    <Columns>
                                        <asp:BoundField DataField="idVenta" HeaderText="ID Venta" />
                                        <asp:BoundField DataField="NombreCompleto" HeaderText="Cliente" />
                                        <asp:BoundField DataField="tipoCliente" HeaderText="Tipo Cliente" />
                                        <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Documento" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="nroDocumento" HeaderText="No. Documento" />
                                        <asp:BoundField DataField="valorVenta" HeaderText="Valor Venta" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="tipoVenta" HeaderText="Tipo Venta" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="valorComision" HeaderText="Valor Comisión" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="FechaVentaEfectiva" HeaderText="Fecha Venta Efectiva" />
                                        <asp:BoundField DataField="fechaRegistroVenta" HeaderText="Fecha Registro Venta" />
                                        <asp:BoundField DataField="identificacionEjecutivo" HeaderText="identificacionEjecutivo" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:TemplateField HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/images/ver.png" Width="25px" CommandName="Agregar" OnClick="btnVer_Click"
                                                    CommandArgument='<%# Eval("idVenta")+";"+ Eval("NombreCompleto")+";"+ Eval("tipoDocumento")+";"+ Eval("nroDocumento")+";"+ Eval("valorVenta")+";"+ Eval("porcentaje")
                                                        +";"+ Eval("valorComision")+";"+ Eval("tipoCliente")+";"+ Eval("tipoVenta")+";"+ Eval("FechaVentaEfectiva")+";"+ Eval("fechaRegistroVenta")%>' /><asp:ImageButton ID="btnAprobar" runat="server" ImageUrl="~/images/positivo.png" Width="25px" CommandName="Agregar" OnClick="btnAprobar_Click"
                                                            CommandArgument='<%# Eval("idVenta")+";"+ Eval("NombreCompleto")+";"+ Eval("tipoDocumento")+";"+ Eval("nroDocumento")+";"+ Eval("valorVenta")+";"+ Eval("porcentaje")
                                                        +";"+ Eval("valorComision")+";"+ Eval("tipoCliente")+";"+ Eval("tipoVenta")+";"+ Eval("FechaVentaEfectiva")+";"+ Eval("fechaRegistroVenta")%>' /><asp:ImageButton ID="btnRechazar" runat="server" ImageUrl="~/images/iconRechazado.png" Width="25px" CommandName="Agregar" OnClick="btnRechazar_Click"
                                                            CommandArgument='<%# Eval("idVenta")+";"+ Eval("NombreCompleto")+";"+ Eval("tipoDocumento")+";"+ Eval("nroDocumento")+";"+ Eval("valorVenta")+";"+ Eval("porcentaje")
                                                        +";"+ Eval("valorComision")+";"+ Eval("tipoCliente")+";"+ Eval("tipoVenta")+";"+ Eval("FechaVentaEfectiva")+";"+ Eval("fechaRegistroVenta")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#D5D8DC" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <PagerStyle CssClass="pgr" />
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpnlReporteVentas" runat="server" HeaderText="Reporte Ventas" TabIndex="1">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                                        <table style="width: 50%">
                                            <tr>
                                                <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto">
                                                    <br />
                                                    <asp:Label ID="Label23" runat="server" Text="Empleados:" CssClass="subtituloHeader"></asp:Label><br />
                                                    <br />
                                                    <asp:DropDownList ID="ddlEjecutovosReporte" runat="server" Width="80%" AutoPostBack="True" OnSelectedIndexChanged="ddlEjecutovosReporte_SelectedIndexChanged"></asp:DropDownList><br />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table style="width: 60%; text-align: left">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" Text="Fecha Inicio: " CssClass="subtituloHeader"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtFechaInicioReporte" runat="server" TextMode="Date" class="textboxNormal" AutoPostBack="True" OnTextChanged="txtFechaInicioReporte_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text="Fecha Fin: " CssClass="subtituloHeader"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtFechaFinReporte" runat="server" TextMode="Date" class="textboxNormal" AutoPostBack="True" OnTextChanged="txtFechaFinReporte_TextChanged"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                    <asp:Label ID="Label27" runat="server" Text="Estado: " CssClass="subtituloHeader"></asp:Label></td>
                                                <td>
                                                    <br />
                                                    <asp:DropDownList ID="ddlEstado" runat="server" Width="80%" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                                        <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                                                        <asp:ListItem Text="Aprobado" Value="APROBADO"></asp:ListItem>
                                                        <asp:ListItem Text="Negado" Value="NEGADO"></asp:ListItem>
                                                        <asp:ListItem Text="Ingresado" Value="INGRESADO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <asp:Panel ID="pnlReporte" runat="server" Width="100%" HorizontalAlign="Center">
                                            <asp:GridView ID="dtgReporteVentas" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                OnRowDataBound="dtgReporteVentas_RowDataBound"
                                                Font-Size="XX-Small"
                                                EmptyDataText="No Existen ventas asignadas a este Ejecutivo"
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="idVenta" HeaderText="ID Venta" />
                                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Cliente" />
                                                    <asp:BoundField DataField="tipoCliente" HeaderText="Tipo Cliente" />
                                                    <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="nroDocumento" HeaderText="No. Documento" />
                                                    <asp:BoundField DataField="valorVenta" HeaderText="Valor Venta"></asp:BoundField>
                                                    <asp:BoundField DataField="tipoVenta" HeaderText="Tipo Venta"></asp:BoundField>
                                                    <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje"></asp:BoundField>
                                                    <asp:BoundField DataField="valorComision" HeaderText="Valor Comisión"></asp:BoundField>
                                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                                    <asp:BoundField DataField="FechaVentaEfectiva" HeaderText="Fecha Venta Efectiva" />
                                                    <asp:BoundField DataField="fechaRegistroVenta" HeaderText="Fecha Registro Venta" />
                                                    <asp:BoundField DataField="identificacionEjecutivo" HeaderText="identificacionEjecutivo"></asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div>
                                            <br />
                                            <br />
                                            <asp:Button ID="btnGenerarListado" runat="server" Text="Generar Reporte" Font-Size="Small" OnClick="btnGenerarListado_Click" CssClass="MiButton" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnGenerarListado" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </asp:Panel>
            <asp:HiddenField ID="hddIDVENTA" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="650px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr style="background-color: #1D4289">
                                <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                    <asp:Label ID="lblTituloV" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em; width: 35%">
                                    <asp:Label ID="Label19" runat="server" Text="Cliente:" CssClass="subtituloHeader"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoCliente" runat="server" CssClass="subtituloHeader" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label13" runat="server" Text="Tipo Documento:" CssClass="subtituloHeader"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoDocumento" runat="server" CssClass="subtituloHeader" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label15" runat="server" Text="No. Documento:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNoDocumento" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label16" runat="server" Text="Valor Venta:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="Small" Font-Bold="false"></asp:Label>
                                    <asp:Label ID="lblValorVenta" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label20" runat="server" Text="Tipo Venta:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoVenta" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label17" runat="server" Text="Porcentaje:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="subtituloHeader" onkeypress="return isNumberKey(event)" Width="15%" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label18" runat="server" Text="Valor Comisión:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label25" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="Small" Font-Bold="false"></asp:Label>
                                    <asp:Label ID="lblValorComisionT" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label21" runat="server" Text="Fecha Venta Efectiva:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaVentaEfectiva" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label22" runat="server" Text="Fecha Registro Venta:" CssClass="subtituloHeader" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaRegistro" runat="server" CssClass="subtituloHeader" Font-Size="Small" Font-Bold="false" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-left: auto; margin-right: auto; text-align: center">
                            <br />
                            <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: right">
                            <br />
                            <asp:Button ID="btnAprobar" runat="server" OnClick="btnAprobar_Click1" Text="Aprobar" BackColor="#061c2f" Font-Size="X-Small" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                             <asp:Button ID="btnNegar" runat="server" OnClick="btnNegar_Click" Text="Negar" BackColor="Red" Font-Size="X-Small" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnClose" runat="server" Text="Cancelar" BackColor="Black" Font-Size="X-Small" CssClass="btn btn-primary btn-lg" Visible="true" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
