<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ActualizacionDatos.aspx.cs" Inherits="LOADIMPSA.ActualizacionDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="uppActualizacion">
        <ContentTemplate>
            <link rel="Stylesheet" href="css/TabStyles.css" type="text/css" media="screen" />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center" colspan="5">
                        <asp:Label ID="Label1" runat="server" Text="ACTUALIZACIÓN DE DATOS CLIENTE" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="5" style="text-align: center; margin-left: auto; margin-right: auto">
                        <asp:Panel ID="pnlAdministracion" Width="100%" runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                            <br />
                            <asp:TextBox ID="txtClienteBuscar" runat="server" Visible="true" CssClass="textboxNormal" placaholder=" Identificación - Nombres" BorderColor="Black"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;&nbsp;
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-primary" Font-Size="9px" OnClick="btnBuscar_Click" />
                            <br />
                            <br />
                        </asp:Panel>

                    </td>
                </tr>
                <tr>
                    <td colspan="4">

                        <asp:Panel ID="pnlClientes" runat="server" Width="50%" HorizontalAlign="Center" Style="margin-left: auto; margin-right: auto">
                            <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                BackColor="White" BorderColor="#999999" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />

                                    <asp:BoundField DataField="numeroidentificacion" HeaderText="Identificación" />
                                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                    <asp:BoundField DataField="cod_usu" HeaderText="UserName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idCasillero" HeaderText="Código Casillero">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#D5D8DC" />
                                <AlternatingRowStyle CssClass="alt" />
                                <PagerStyle CssClass="pgr" />
                            </asp:GridView>

                            <br />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlDatosClientes" runat="server" Visible="false">
                <table style="margin-left: auto; margin-right: auto; width: 80%">
                    <tr style="background: #1D4289">
                        <td colspan="3">
                            <asp:Label ID="Label3" runat="server" Text="DATOS INFORMATIVOS" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="Tipo Documento"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlTipoIdentificacion" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                <asp:ListItem Text="Cédula" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pasaporte" Value="2"></asp:ListItem>
                                <asp:ListItem Text="RUC" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                            <br />
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Identificación"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtIdentificacion" class="textboxNormal" runat="server" CssClass="form-control" MaxLength="13" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>

                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label5" runat="server" Text="Primer Nombre"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPrimerNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label6" runat="server" Text="Segundo Nombre"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtSegundoNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>

                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Primer Apellido"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtprimerApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label8" runat="server" Text="Segundo Apellido"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtSegundoApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                        <td style="text-align: left; width: 33%">
                            <br />
                        </td>
                    </tr>
                    <tr style="background: #1D4289">
                        <td colspan="3">
                            <asp:Label ID="Label9" runat="server" Text="DATOS DE LA CUENTA" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label10" runat="server" Text="Email"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtMail" class="textboxNormal" runat="server" CssClass="form-control" TextMode="Email" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label11" runat="server" Text="Celular"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCelular" class="textboxNormal" runat="server" CssClass="form-control" MaxLength="10" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                            <br />
                            <asp:Label ID="Label12" runat="server" Text="Telèfono Convencional"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtTelefono" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label13" runat="server" Text="Provincia"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlProvincia" class="textboxNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" runat="server" CssClass="form-control" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label14" runat="server" Text="Canton"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlCantones" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%" colspan="3">
                            <br />
                            <asp:Label ID="Label15" runat="server" Text="Dirección Entrega"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtDireccion" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label16" runat="server" Text="Fecha Nacimiento"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFechaNacimiento" class="textboxNormal" runat="server" CssClass="form-control" TextMode="Date" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label17" runat="server" Text="Género"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlGenero" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Femenino" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                            <br />
                            <asp:Label ID="Label18" runat="server" Text="Rol"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlRol" class="textboxNormal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" CssClass="form-control" Width="80%">
                                <asp:ListItem Text="Super Administrador" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Ejecutivo de Cuenta" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Cliente" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Cliente VIP" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Invitado" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox ID="txtCodigoClienteVIP" class="textboxNormal" runat="server" Visible="false" CssClass="form-control" Width="80%"></asp:TextBox>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <br />
                        </td>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Font-Size="9px" class="btn btn-primary" Text="Actualizar" />
                        </td>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="_rutUser" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
