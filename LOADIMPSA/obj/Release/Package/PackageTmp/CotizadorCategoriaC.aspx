<%@ Page Title="Cotizador C" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="CotizadorCategoriaC.aspx.cs" Inherits="LOADIMPSA.CotizadorCategoriaC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        .cssContent {
            background-color: #D3DEEF;
            font-family: 'Montserrat', sans-serif;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 98%;
            height: 100%;
        }

        .cssHeaderSelected {
            background-color: #1D4289;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 50px;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_minus.png);
            background-repeat: no-repeat;
            background-position: right;
        }

        .cssHeader {
            background-color: #009CDE;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 30PX%;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_plus.png);
            background-repeat: no-repeat;
            background-position: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppListadoTracking">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress" style="width: 250px; height: 250px">
                    <div id="theprogress">
                        <asp:Image ID="Image1" runat="server" Width="40%" ImageUrl="~/images/loading.gif" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uppListadoTracking" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="COTIZADOR C" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                    <br />
                    <br />
                    <asp:RadioButtonList ID="panelId" runat="server" CssClass="rbl" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="radio_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ingresar Cotización " Value="0"></asp:ListItem>
                        <asp:ListItem Text="Consultar" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <br />
                <asp:Panel ID="PanelIngreso" runat="server" HorizontalAlign="Center">


                    <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                        <asp:Label ID="Label30" runat="server" Text="ES CLIENTE:" CssClass="subtituloHeader"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlstrCliente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">

                            <asp:ListItem Text="SI" Value="SI" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                      <asp:Panel ID="panelCompletoC" Visible="False" runat="server" HorizontalAlign="Center">
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
                    </asp:Panel>
                     <asp:Panel ID="panelCompletoNC" Visible="False" runat="server" HorizontalAlign="Center">
                        <table style="margin-left: auto; margin-right: auto; width: 80%">
                            <tr style="background: #1D4289">
                                <td colspan="5">
                                    <asp:Label ID="Label31" runat="server" Text="DATOS DE CLIENTE" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; margin-left: auto; margin-right: auto" class="auto-style2">
                                    <asp:Label ID="Label32" runat="server" Text="NOMBRES COMPLETOS:" CssClass="subtituloHeader"></asp:Label>
                                </td>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">

                                    <asp:TextBox ID="txtNombres" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; margin-left: auto; margin-right: auto" class="auto-style2">
                                    <asp:Label ID="Label33" runat="server" Text="IDENTIFICACIÓN:" CssClass="subtituloHeader"></asp:Label>
                                </td>
                                <td style="width: 35%; text-align: left; margin-left: auto; margin-right: auto">

                                    <asp:TextBox ID="txtIDE" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="60%"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left; margin-left: auto; margin-right: auto" class="auto-style2">
                                    <asp:Label ID="Label34" runat="server" Text="CORREO:" CssClass="subtituloHeader"></asp:Label>
                                </td>
                                <td style="width: 35%; text-align: left; margin-left: auto; margin-right: auto">

                                    <asp:TextBox ID="txtCorreo" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="60%"></asp:TextBox>

                                </td>

                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlCotizador" runat="server" HorizontalAlign="Center" Width="100%" Height="100%" ScrollBars="Auto" Visible="false">
                        <br />
                        <br />
                        <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                            <table style="margin-left: auto; margin-right: auto; width: 80%">
                                <tr style="background: #1D4289">
                                    <td colspan="4">
                                        <asp:Label ID="Label1" runat="server" Text="COTIZADOR CATEGORÍA C" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>
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
                                    <td style="width: 35%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <asp:Label ID="Label4" runat="server" Text="Valor Factura:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtFactura" OnTextChanged="txtFactura_TextChanged" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="30%" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <br />
                                        <br />
                                    </td>
                                    <td style="width: 65%; margin-left: auto; margin-right: auto; text-align: left" colspan="2">
                                        <asp:Label ID="Label5" runat="server" Text="Peso en Libras:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtPeso" class="textboxNormal" runat="server" Font-Size="11px" CssClass="form-control" Width="30%" onkeypress="return isNumberKey(event)" OnTextChanged="txtPeso_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr style="background: #1D4289">
                                    <td colspan="4">
                                        <asp:Label ID="Label28" runat="server" Text="SERVICIO DE COURIER" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label12" runat="server" Text="NACIONALIZACIÓN :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label16" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblNacionalizacion" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label14" runat="server" Text="FLETE INTERNACIONAL :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label29" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblFleteInternacional" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                       
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label15" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblServicioDomicilio" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label6" runat="server" Text="TOTAL SERVICIO COURIER :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label11" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblTotalServicioCorrier" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr style="background: #1D4289">
                                    <td colspan="4">
                                        <asp:Label ID="Label13" runat="server" Text="IMPUESTOS SENAE" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label17" runat="server" Text="BASE IMPONIBLE :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label18" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblBaseImponible" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label21" runat="server" Text="PORCENTAJE DE AD-VALOREM  :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:TextBox ID="txtPorcentajeOperacion" class="textboxNormal" runat="server" CssClass="form-control" Width="30%" onkeypress="return isNumberKey(event)" OnTextChanged="txtPorcentajeOperacion_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label19" runat="server" Text="ARANCEL :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label20" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblArancel" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label22" runat="server" Text="FODINFA :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label23" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblFondimfa" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label24" runat="server" Text="IVA :" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label25" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblIVA" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label26" runat="server" Text="TOTAL IMPUESTOS SENAE:" Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label27" runat="server" Text="$" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblTotalImpuestosSenae" runat="server" CssClass="subtituloHeader" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>


                                <tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                        <br />
                                        <asp:Label ID="Label3" runat="server" Text="TOTAL IMPORTACIÓN (*): " Font-Size="9px" CssClass="subtituloHeader"></asp:Label>
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
                                        <asp:Label ID="Label8" runat="server" Text="* Los valores detallados son calculados en base a un peso y valor referenciales. Estos pueden sufrir variaciones."
                                            CssClass="subtituloHeader"></asp:Label><br />
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="** En caso de acercarse a retirar a oficina, no se considerará valor de envio a domicilio." CssClass="subtituloHeader"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; margin-left: auto; margin-right: auto; width: 33%" colspan="4">
                                        <br />
                                        <br />
                                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" BackColor="#1D4289" Text="Registrar Cotización" OnClick="btnRegistrar_Click" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>

                </asp:Panel>
                 <asp:Panel ID="PanelConsulta" Visible="false" runat="server" HorizontalAlign="Center">
                          <asp:Panel ID="pnlFiltros" runat="server" HorizontalAlign="Center" Width="100%">
                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="cssHeader" HeaderSelectedCssClass="cssHeaderSelected"
                    ContentCssClass="cssContent" SelectedIndex="0" FadeTransitions="true"
                    SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                    RequireOpenedPane="false" AutoSize="None">
                    <Panes>
                        <cc1:AccordionPane ID="accRecibidoMiami" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/iconFiltro.png" Width="80%" ImageAlign="Left" />
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label35" runat="server" Text="FILTROS" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <table style="text-align: left; padding-left: 1em">

                                    <tr>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label36" runat="server" Text="Fecha desde:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtFechaIngreso" class="textboxNormal" Font-Size="X-Small" TextMode="Date" runat="server" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label37" runat="server" Text="Fecha Hasta:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtFechaRecbidoMiami" runat="server" Font-Size="X-Small" class="textboxNormal" TextMode="Date" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label38" runat="server" Text="Ejecutivo Cuenta:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:DropDownList ID="ddlEjecutivos" runat="server" Font-Size="X-Small" Width="80%" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="text-align: right; padding-right: 4em">
                                            <br />
                                            <asp:Button ID="Button1" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar1_Click" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </asp:Panel>

            <br />
            <asp:Panel ID="pnlListado" runat="server" HorizontalAlign="Center" Height="800px" Width="100%" Visible="false" ScrollBars="Vertical">
                <asp:GridView ID="dtgEnvios" runat="server" HorizontalAlign="Center"
                    Font-Size="X-Small"
                    AutoGenerateColumns="False" Width="100%"
                
                    ShowHeaderWhenEmpty="True"
                    CssClass="mGrid"
                    EmptyDataText="No hay envios en curso.">
                    <Columns>


                        <asp:BoundField DataField="idCotizacion" HeaderText="ID COTIZACIÓN" />
                        <asp:BoundField DataField="Nombre" HeaderText="NOMBRE" />
                        <asp:BoundField DataField="descripcionMercaderia" HeaderText="MERCANCÍA" />
                        <asp:BoundField DataField="valorFof" HeaderText="VALOR FOB" />
                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                        <asp:BoundField DataField="servicioCourier" HeaderText="SERVICIO COURIER" />
                        <asp:BoundField DataField="totalEnvio" HeaderText="TOTAL ENVIO" />


                        <asp:BoundField DataField="impuestoSenae" HeaderText="IMPUESTO SENAE" />
                        <asp:BoundField DataField="fechaCotizacion" HeaderText="FECHA REGISTRO" />
                        <asp:BoundField DataField="usuarioRegstro" HeaderText="USUARIO REGISTRO" />
                        <asp:TemplateField HeaderText="Archivo" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <a href="javascript:VerFile('div<%# Eval("idCotizacion")%>' );">
                                    <asp:ImageButton runat="server" ID="btnRegistroCheckOut2" OnClick="btnRegistroCheckOut_ClickFile"
                                        CommandArgument='<%# Eval("idCotizacion")%>'
                                        ImageUrl="~/images/buscar.png" ToolTip="Check Out" Width="20px" />
                                    <%--   --%>
                            </ItemTemplate>
                            <ControlStyle Width="30px" />
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>




                    </Columns>
                    <FooterStyle BackColor="#D5D8DC" />
                    <AlternatingRowStyle CssClass="alt" />
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>
            </asp:Panel>    
                 </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
