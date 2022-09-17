<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Menu.Master" CodeBehind="ConsultaCanje.aspx.cs" Inherits="LOADIMPSA.ConsultaCanje" %>

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
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="CONSULTA CODIGO CANJE" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                  <br />
                <br />
                <asp:Panel ID="pnlBusquedaClientesCanjeo" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="No. Canje - Identificación - Nombres" />
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
                            BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged"   EmptyDataText="No hay Canje  a Presentar.">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="numeroIdentificacion" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombresCliente" HeaderText="Nombre Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="codigoCanje" HeaderText="Codigo Canje">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="producto" HeaderText="Nombre Producto">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="puntosCanjeado" HeaderText="Puntos Canjeado">
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
                                <asp:Label ID="lblInformacion" runat="server" Text="INFORMACIÓN CLIENTE (CODIGO CANJE)" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label></td>
                        </tr>
                         <tr>
                            <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label1" runat="server" Text="ID de Canje:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblIdCanje" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                              <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombres:  " CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblNombresCom" ForeColor="Black" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                               <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblCedula" runat="server" Text="Cedula:  " CssClass="subtituloHeader" Font-Bold="true"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lBlCed" ForeColor="Black" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                             <td style="width: 25%; text-align: center">
                                <asp:Label ID="lblCarrera" runat="server" Text="Código de Canje:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblCodCliente" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr

                        <tr>
                               <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label2" runat="server" Text="Fecha de Canjeo:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblFechaCanje" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                       <tr>
                              <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label3" runat="server" Text="Nombre de Producto:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblNombreProducto" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr
                     <tr>
                              <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label4" runat="server" Text="Puntos Usados:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblPuntosUsados" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                     <tr>
                               <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label5" runat="server" Text="Puntos Acumulados:  " CssClass="subtituloHeader"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblPuntosacumulados" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <br />
             


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
