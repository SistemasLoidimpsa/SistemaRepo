<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" EnableEventValidation = "false" AutoEventWireup="true" CodeBehind="AdministracionEnvios.aspx.cs" Inherits="LOADIMPSA.AdministracionEnvios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script>
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (confirm("Esta seguro que desea eliminar la orden?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
            top: 4%;
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
    </style>
    <style type="text/css">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppAdministracionEnvios">
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
    <asp:UpdatePanel ID="uppAdministracionEnvios" runat="server">
        <ContentTemplate>
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <asp:Label runat="server" Text="PANEL DE GESTIÓN LOIDIMPSA EXPRESS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <br />
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
                                            <asp:Label ID="Label1" runat="server" Text="FILTROS" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <table style="text-align: left; padding-left: 1em">
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label2" runat="server" Text="Estado Tracking" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlEstado" runat="server" Width="80%" Font-Size="X-Small">
                                                <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                                                <asp:ListItem Text="Recibido en Miami" Value="RECIBIDO EN MIAMI"></asp:ListItem>
                                                <asp:ListItem Text="Autorizado para Volar" Value="AUTORIZADO PARA VOLAR"></asp:ListItem>
                                                <asp:ListItem Text="En tránsito y proceso de Aduana" Value="EN TRANSITO Y PROCESO DE ADUANA"></asp:ListItem>
                                                <asp:ListItem Text="Bodega Loidimpsa" Value="BODEGA LOIDIMPSA"></asp:ListItem>
                                                <asp:ListItem Text="Finalizado" Value="FINALIZADO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label3" runat="server" Text="Nro. Orden:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtNroOrdenInterna" class="textboxNormal" Font-Size="X-Small" runat="server" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label4" runat="server" Text="Nro. Tracking:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:TextBox ID="txtNroTracking" class="textboxNormal" runat="server" Font-Size="X-Small" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Fecha desde:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaIngreso" class="textboxNormal" Font-Size="X-Small" TextMode="Date" runat="server" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Fecha Hasta:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaRecbidoMiami" runat="server" Font-Size="X-Small" class="textboxNormal" TextMode="Date" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Ejecutivo Cuenta:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEjecutivos" runat="server" Font-Size="X-Small" Width="80%" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                   
                                     <tr>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label16" runat="server" Text="Categoria" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="80%" Font-Size="X-Small">
                                                <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                            
                                            </asp:DropDownList>
                                        </td>
                                  
                                    </tr>

                                    <tr>
                                        <td colspan="6" style="text-align: right; padding-right: 4em">
                                            <br />
                                            <asp:Button ID="btnBuscar" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />
                                         &nbsp    &nbsp    &nbsp    &nbsp 
                                             <asp:Button ID="btnExcel" Font-Size="X-Small" runat="server" Text="Exportar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnExport_Click" />
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
            <br />
            <asp:Panel ID="pnlListado" runat="server" HorizontalAlign="Center" Width="100%">
                <asp:GridView ID="dtgListadoTrackingFiltro" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True" Width="98%" CssClass="mGrid"
                    Font-Size="XX-Small"
                    OnRowDataBound="dtgListadoTrackingFiltro_RowDataBound"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                    FooterStyle-BackColor="#D5D8DC"
                    EmptyDataText="No Existen trackings."
                    BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccione">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="true" OnCheckedChanged="chkEstado_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="No. Orden" />
                        <asp:BoundField DataField="tracking" HeaderText="ID Tracking" />
                        <asp:BoundField DataField="cedulaCliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo"  ItemStyle-Width="7"/>
                        <asp:BoundField DataField="idTransportista" HeaderText="ID Transportista" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                        <asp:BoundField DataField="nombreTransportista" HeaderText="Nombre Transportista" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                        <asp:BoundField DataField="peso" HeaderText="Peso"  />
                        <asp:BoundField DataField="precio" HeaderText="Precio" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones"  ItemStyle-Width="7%" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ItemStyle-Width="15%" />
                           <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="Fecha Recibido Miami" />
                        <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Registro" />
                         <asp:BoundField DataField="imgFactura" HeaderText="Registro Factura" Visible="false" />
                          <asp:BoundField DataField="bodega" HeaderText="BODEGA" Visible="true" />
                      
                          
                        <asp:TemplateField HeaderText="Eliminar Guía">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnBorrarGuia" OnClick="btnBorrarGuia_Click"
                                    CommandArgument='<%# Eval("numeroOrdenInterno")+";"+Eval("nombreTransportista")%>'
                                    ImageUrl="~/images/borrarguia.png" ToolTip="Borrar Guía" Width="20px" OnClientClick="Confirm();"/>
                            </ItemTemplate>
                            <ControlStyle Width="40px" />
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ver Historial">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnHistorial" OnClick="btnHistorial_Click"
                                    CommandArgument='<%# Eval("numeroOrdenInterno")+";"+Eval("nombreTransportista")+";"+Eval("imgFactura")+";"+Eval("estado")%>'
                                    ImageUrl="~/images/iconoHistorial.png" ToolTip="Ver Historial" Width="20px" />
                            </ItemTemplate>
                            <ControlStyle Width="40px" />
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#D5D8DC" />
                    <AlternatingRowStyle CssClass="alt" />
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>

             
            </asp:Panel>
            <div style="width: 100%; margin-left: auto; margin-right: auto; text-align: center">
                <br />
                <asp:Button ID="btnAutorizar" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="Small" BackColor="#1D4289" Text="Gestión de Guías" OnClick="btnAutorizar_Click" />

                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                 <asp:Button ID="btnReversarGuias" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="Small" BackColor="red" Text="Reverso Guías" OnClick="btnReversarGuias_Click" />
                <br />
            </div>
            <br />
            <br />
            <asp:HiddenField ID="hddIdEjecutivo" runat="server" />
            <asp:HiddenField ID="hddIdentificacion" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="650px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGestion" runat="server" Visible="false" Width="100%">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV" Text="GESTIÓN DE GUÍAS" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 2em">
                                        <asp:Label ID="Label9" runat="server" Text="Envios a gestionar:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" Font-Size="Small" Font-Bold="false" ForeColor="Black" Text="Detalle de Envios." CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:Panel runat="server" Width="100%" Height="150px" ScrollBars="Vertical">
                                            <asp:GridView ID="dtgListadoEnviosGestionar" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                FooterStyle-BackColor="#D5D8DC"
                                                Font-Size="XX-Small"
                                                EmptyDataText="No existen envios a gestionar."
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="No. Orden" />
                                                    <asp:BoundField DataField="cedulaCliente" HeaderText="Cliente" />
                                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                                                    <asp:BoundField DataField="peso" HeaderText="Peso" />
                                                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <br />
                                        <asp:Label ID="Label10" runat="server" Text="Observación:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label11" runat="server" Font-Size="Small" Font-Bold="false" ForeColor="Black" Text="Justificación de cambio de estado." CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtObservaciones" runat="server" Width="90%" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlHistorial" runat="server" Visible="false" Width="100%">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="Label12" Text="HISTORIAL DE ORDEN / TRANSPORTISTA" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="Label13" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 2em">
                                        <asp:Label ID="Label14" runat="server" CssClass="subtituloHeader" Visible="false" Text="Escoja el empleado a transferir:"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label15" runat="server" Text="Historial de Envio:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:GridView ID="dtgHistorialTracking" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                            FooterStyle-BackColor="#D5D8DC"
                                            Font-Size="X-Small"
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
                          <asp:HiddenField ID="hddImgFactura" runat="server" />
                        <div style="margin-left: auto; margin-right: auto; text-align: center">
                            <br />
                            <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: right">
                            <br />
                            <asp:Button ID="btnReversarEstado" runat="server" OnClick="btnReversarEstado_Click" Font-Size="X-Small" Text="Reversar Estado" BackColor="Red" CssClass="btn btn-primary btn-lg" Visible="false" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnCambiarEstado" runat="server" OnClick="btnCambiarEstado_Click" Font-Size="X-Small" Text="Cambiar Estado" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="false" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnVerFactura" runat="server" OnClick="btnVerFactura_Click" Font-Size="X-Small" Text="Ver Factura" BackColor="#1D4289" CssClass="btn btn-primary btn-lg"   Visible="false" />
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
    <asp:Panel  runat="server">
             
    </asp:Panel>
</asp:Content>
