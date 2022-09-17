<%@ Page Title="Ingreso de Tracking" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="IngresoTracking.aspx.cs" Inherits="LOADIMPSA.IngresoTracking" %>

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
        function edValueKeyPress() {
            var ingresoOrden = document.getElementById("<%=txtOrdenInterno.ClientID %>").value;
            console.log(ingresoOrden);
            if (ingresoOrden != "") {

                document.getElementById("<%=uppSubirFactura.ClientID %>").style.display = 'inherit';


                console.log("Estoy aqui null");
            }
            else {
                console.log("Estoy aqui");
                document.getElementById("<%=uppSubirFactura.ClientID %>").style.display = 'none';


            }
        }

    </script>
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

    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppTracking">
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
    <asp:UpdatePanel ID="uppTracking" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="INGRESO DE TRACKING" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
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
                    </asp:Panel>

                </asp:Panel>
                <br />
                <asp:Panel ID="pnlDatosEstudiante" runat="server" Width="100%" Visible="False">
                    <br />
                    <table style="width: 50%; margin-right: auto; margin-left: auto; text-align: center; border: double">
                        <tr style="background: #1D4289">
                            <td colspan="2">
                                <asp:Label ID="lblInformacion" runat="server" Text="INFORMACIÓN CLIENTE" Font-Bold="True" Font-Size="10px" ForeColor="White"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="subtituloHeader" Font-Size="9px" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblNombresCom" ForeColor="Black" runat="server" Font-Size="9px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCedula" runat="server" Text="Cedula:" CssClass="subtituloHeader" Font-Size="9px" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lBlCed" ForeColor="Black" runat="server" Font-Size="9px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCarrera" runat="server" Text="Código de Cliente:" CssClass="subtituloHeader" Font-Size="9px"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblCodCliente" runat="server" ForeColor="Black" Font-Size="9px"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlClientesTracking" runat="server" Width="100%" HorizontalAlign="Center" Visible="false">
                    <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                        <table style="margin-left: auto; margin-right: auto; width: 80%">
                            <tr style="background: #1D4289">
                                <td colspan="4">
                                    <asp:Label ID="Label1" runat="server" Text="INGRESO DE TRACKING" Font-Bold="True" Font-Size="10px" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
<tr>
                                    <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">

                                    <asp:Label ID="Label9" runat="server" Text=" Bodega Miami:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlBodega" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlseparar_SelectedIndexChanged">
                                        <asp:ListItem Text="SEPDENTS - 9195 NW 101 ST" Value="SEPDENTS - 9195 NW 101 ST"></asp:ListItem>
                                        <asp:ListItem Text="GLS - 10301 NW 108TH AVE" Value="GLS - 10301 NW 108TH AVE"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
</tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto;">
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Número Tracking:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTracking" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                </td>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">

                                    <asp:Label ID="Label15" runat="server" Text="Paquetes Separados:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlsepararPaquete" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlseparar_SelectedIndexChanged">
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="SI" Value="1"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">
                                    <asp:Label ID="Label4" runat="server" Text="Número de Orden Interno:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtOrdenInterno" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>
                                <td style="width: 50%" rowspan="4">
                                    <asp:UpdatePanel ID="uppSubirFactura" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                <div style="width: 100%; margin-left: auto; margin-right: auto">
                                                    <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <asp:FileUpload ID="examinarAdjuntoResolutor" runat="server" Font-Names="Leelawadee" ForeColor="Black"
                                                                    Style="margin-left: 1px" Width="100%" Font-Size="X-Small" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="text-align: center; width: 10%">

                                                                <asp:Button Text="Cargar Factura" Font-Size="XX-Small" runat="server" BackColor="#1D4289" ID="btnCargarFileResolutor" CausesValidation="False" CssClass="MiButton" OnClick="btnCargarFileResolutor_Click" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" />
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td colspan="2" style="text-align: center; padding-top: 20px; margin-left: auto; margin-right: auto">
                                                                <asp:GridView ID="gvArchivosResolutor" runat="server"
                                                                    HorizontalAlign="Center" AutoGenerateColumns="False"
                                                                    CssClass="mGrid"
                                                                    ShowHeader="true"
                                                                    Width="98%"
                                                                    EmptyDataText="No existen archivos subidos."
                                                                    OnRowDeleting="gvArchivosResolutor_RowDeleting"
                                                                    OnSelectedIndexChanged="gvArchivosResolutor_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:CommandField ShowDeleteButton="True" />
                                                                        <asp:BoundField DataField="Nom_Arc" HeaderText="Nombre Archivo" />
                                                                    </Columns>
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <PagerStyle CssClass="pgr" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:Label runat="server" ID="lbArchExiste" Text="Archivo ya fue subido" ForeColor="Red" Visible="False"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </asp:Panel>
                                            <asp:HiddenField ID="hddNombreArchivoFactura" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnCargarFileResolutor" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left">
                                    <asp:Label ID="Label5" runat="server" Text="Peso en Libras:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPeso" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left">
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Transportista" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlTransportista" OnSelectedIndexChanged="ddlTransportista_SelectedIndexChanged" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:TextBox ID="txtOtroTransportista" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" Visible="false"></asp:TextBox>
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">
                                    <br />
                                    <asp:Label ID="Label6" runat="server" Text="Fecha Recibido en Miami:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtFechaRecibidoMiami" class="textboxNormal" runat="server" TextMode="Date" CssClass="form-control" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">
                                    <br />
                                    <asp:Label ID="Label7" runat="server" Text="Descripción del Envio:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtDescripcion" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">
                                    <br />
                                    <asp:Label ID="Label8" runat="server" Text="Observación:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtObservaciones" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center; margin-left: auto; margin-right: auto; width: 33%" colspan="4">
                                    <br />
                                    <asp:Button ID="btnIngresarOrden" runat="server" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Text="Ingresar Orden" Font-Size="9px" OnClick="btnIngresarOrden_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </asp:Panel>
            </asp:Panel>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
