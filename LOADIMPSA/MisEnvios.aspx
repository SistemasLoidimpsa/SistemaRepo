<%@ Page Title="Mis Envios" Async="true" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="MisEnvios.aspx.cs" Inherits="LOADIMPSA.Mis_Envios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta name="facebook-domain-verification" content="x4ugnfcntyhxy6hctzi4ni9m66h4d4" />
    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
            'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '1111161526407188');
        fbq('track', 'CompleteRegistration');
    </script>
    <noscript>
        <img height="1" width="1"
            src="https://www.facebook.com/tr?id=1111161526407188&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->
    <style type="text/css">
        
        .button {
            display: inline-block;
            padding: 10px 25px;
            font-size: 80%;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            outline: none;
            color: #fff;
            border: none;
            border-radius: 15px;
            width: 100%;
        }

        .button1 {
            display: inline-block;
            padding: 1px 0px;
            font-size: 80%;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            outline: none;
            color: #fff;
            border: none;
            border-radius: 15px;
            width: 25%;
        }

        .button:hover {
            background-color: #3e8e41
        }

        .button:active {
            background-color: #3e8e41;
            box-shadow: 0 5px #666;
            transform: translateY(4px);
        }

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
            left: 15%;
            right: 15%;
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
            font-size: 11px;
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

        /*Estilos para Tabla Bancaria*/
        .tftable {
            border-width: 1px;
            border-color: transparent;
            border-collapse: collapse;
            width: 80px;
            height: 30px;
        }

            .tftable th:first-child {
                background-color: #1D4289;
                border-width: 1px;
                border-style: hidden;
                text-align: center;
                color: #ffffff;
                font-size: 8px;
                -moz-border-radius: 3px;
                -webkit-border-radius: 3px;
                border-radius: 3px;
            }

            .tftable th {
                background-color: #1D4289;
                border-width: 1px;
                border-style: hidden;
                text-align: center;
                color: #ffffff;
                font-size: 8px;
            }

            .tftable tr {
                background-color: #dedede;
            }

            .tftable td {
                border-width: 1px;
                border-color: none;
                text-align: left;
                font-size: 8px;
                padding: 1px;
                -moz-border-radius: 1px;
                -webkit-border-radius: 1px;
                border-radius: 1px;
            }

            .tftable tr:hover {
                background-color: #ffffff;
                -moz-border-radius: 1px;
                -webkit-border-radius: 1px;
                border-radius: 1px;
            }

        .contenedor3 {
            display: flex;
            align-items: center;
        }

        .contenedor4 {
            display: flex;
            align-items: center;
            flex-direction: row-reverse;
        }

        .contenido3 {
            margin: 0px 5px 0px 0px /* requerido para alineación izq */
        }

        .contenido4 {
            margin: 0px 0px 0px 5px /* requerido para alineación derec */
        }

        mGridE {
            width: 100%;
            background-color: #fff;
            font-size: 9px;
            margin: 0 auto;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

        .mGridE td {
            padding: 2px;
            border: solid 1px #c1c1c1;
            color: #717171;
        }

        .mGridE tf {
            padding: 4px 2px;
            color: #fff;
            background: #ABB2B9 url(../images/grd_head.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 9px;
        }

        .mGridE th {
            padding: 4px 2px;
            color: #fff;
            background: #1D4289 url(../images/grd_head.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 9px;
        }

        .mGridE .alt {
            background: #fcfcfc url(../images/grd_alt.png) repeat-x top;
        }

        .mGridE .pgr {
            background: #424242 url(../images/grd_pgr.png) repeat-x top;
        }

            .mGridE .pgr table {
                margin: 0 auto;
            }

            .mGridE .pgr td {
                border-width: 0;
                padding: 0 6px;
                border-left: solid 1px #666;
                font-weight: bold;
                font-size: 9px;
                color: #fff;
                line-height: 12px;
            }

            .mGridE .pgr a {
                color: #666;
                text-decoration: none;
            }

                .mGridE .pgr a:hover {
                    color: #000;
                    text-decoration: none;
                }

        .float {
            color: #fff;
            position: fixed;
            width: 60px;
            height: 60px;
            bottom: 40px;
            right: 40px;
            background-color: #1D4289;
            
            border-radius: 50px;
            text-align: center;
            font-size: 30px;
            box-shadow: 2px 2px 3px #999;
            z-index: 100;
        }

            .float:hover {
                text-decoration: none;
               
                background-color:#1D4289;
                 color: #fff;
            }

        .my-float {
            margin-top: 16px;   
            color: #fff;
             
        }
    </style>
    <!-- Incluimos JQUERY -->
    <script src="js/jquery-3.2.1.min.js"></script>

    <!-- Incluimos la librería Javascript de encriptación RSA de ELP -->
    <script type="text/javascript" src="js/rsa_elp.js"></script>

    <script type="text/javascript">
        function edValueKeyPress() {
            var ingresoValor = parseFloat(document.getElementById("<%=txtValorEfectivo.ClientID %>").value);
            var totalPagar = document.getElementById("<%=lblValorEnvioDomicilio.ClientID %>").innerHTML;
            var replaceTotalPagar = parseFloat(totalPagar.replaceAll(',', '.'));
            var resta = ingresoValor - replaceTotalPagar;
            resta = resta.toFixed(2);
            console.log('Este es el valor casteado de tipeo: ' + ingresoValor);
            console.log('Este es el valor total a pagar: ' + replaceTotalPagar);
            var lblValue = document.getElementById("<%=lblValorEfectivo.ClientID %>");
            if (ingresoValor > replaceTotalPagar) {

                lblValue.innerText = "El vuelto a dar es  $ " + resta;
            }
            else {
                lblValue.innerText = "Al cliente le falta $ " + Math.abs(resta) + " para completar el pago.";
            }
        }
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



<%--        function encripta_datos() {

            var encrypt = new JSEncrypt();
            encrypt.setPublicKey($('#<%= pubkey.ClientID %>').val());

            var encrypted = encrypt.encrypt($('#<%= tcnumero.ClientID %>').val());
            var encrypted2 = encrypt.encrypt($('#<%= tccodigodeseguridad.ClientID %>').val());

            console.log('Valor numero' + $('#<%= tcnumero.ClientID %>').val());

            $('#<%= tcnumero.ClientID %>').val(encrypted);
            $('#<%= tccodigodeseguridad.ClientID %>').val(encrypted2);

            if (encrypted == $('#<%= tcnumero.ClientID %>').val()) {

                // Mostramos la alerta de encriptación exitosa, recuerda comentarla con // al comienzo para desactivarla				
                alert('SUPER!!! La encriptacion RSA fue exitosa :) felicitaciones!!!, recuerda comentar con // la linea # 81 de este ejemplo para ocultar esta alerta.');

                // Enviamos el formulario con los datos sensibles ya encriptados
                document.forms["form1"].submit();

                event.stopPropagation();
            
            }
            else {

                // En caso de un error en la encriptación mostramos un mensaje de error
                alert('Error RSA: No pudimos encriptar su solicitud, por favor intente nuevamente, recuerda llenar todos los campos para poder realizar la encriptacion.');

                $('#<%= tcnumero.ClientID %>').val("");
                $('#<%= tccodigodeseguridad.ClientID %>').val("");
            }
            console.log('Ya ingrese');

        }

    <--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppMisEnvios">
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
    <asp:UpdatePanel ID="uppMisEnvios" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" HorizontalAlign="Center" Width="100%">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="MI CASILLERO" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
                    </asp:Panel>
                </asp:Panel>
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
                                   <asp:BoundField DataField="idCasillero" HeaderText="Casillero">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipoCliente" HeaderText="Tipo de Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="idEjectuvio" HeaderText="Id de Ejecutivo">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorFob" HeaderText="Valor Fob Disponible">
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
                <asp:Panel ID="pnlDatosCliente" runat="server" Width="100%" Visible="False">
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
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="Label46" runat="server" Text="Valor FOB disponible:" CssClass="subtituloHeader" Font-Size="9px"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:Label ID="lblvalorFob" runat="server" ForeColor="Black" Font-Size="9px"></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="cssHeader" Visible="false" HeaderSelectedCssClass="cssHeaderSelected"
                    ContentCssClass="cssContent" SelectedIndex="0" FadeTransitions="true"
                    SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                    RequireOpenedPane="false" AutoSize="None">
                    <Panes>
                        <cc1:AccordionPane ID="accRecibidoMiami" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/recibidoenmiami.png" Width="80%" ImageAlign="Left" />
                                          
                                        
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label1" runat="server" Text="RECIBIDO EN MIAMI" Font-Bold="true"></asp:Label>&nbsp
                                             <i class="fa fa-info-circle" aria-hidden="true"  title="PAQUETES RECIBIDOS EN SU CASILLERO LOIDIMPSA EN ESTADOS UNIDOS, ES
NECESARIO QUE SUBA SU FACTURA DE COMPRA PARA CONTINUAR."></i>
                                     
                                        <td>
                                             
                                            </td>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <br />
                                <asp:GridView ID="dtgRecibidoMiami" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    Font-Size="X-Small"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    EmptyDataText="No Existen envios recibidos en Miami."
                                    BorderWidth="1px" CellPadding="3">
                                    <Columns>
                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA RECIBIDO EN MIAMI" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:TemplateField HeaderText="Autorizar">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="btnAutorizar" OnClick="btnAutorizar_Click"
                                                    CommandArgument='<%# Eval("numeroOrdenInterno")+";"+Eval("nombreTransportista")+";"+Eval("fechaRecibidoMiami")%>'
                                                    ImageUrl="~/images/Autorizar.png" ToolTip="Autorizar para volar a Ecuador." Width="20px" />
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
                                <br />
                                <br />
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="accAutorizadoVolar" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/AutorizadoVolar.png" Width="85%" ImageAlign="Left" />
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label2" runat="server" Text="AUTORIZADO PARA VOLAR" Font-Bold="true"></asp:Label>&nbsp
                                             <i class="fa fa-info-circle" aria-hidden="true"  title="SUS PAQUETES SE ENCUENTRAN LISTOS PARA VOLAR A ECUADOR, MUY PRONTO
RECIBIRÁS UNA NOTIFICACION DE ALERTA DE VUELO"></i>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <br />
                                <asp:GridView ID="dtgAutorizadosVolar" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    Font-Size="X-Small"
                                    EmptyDataText="No Existen envios autorizados para volar"
                                    BorderWidth="1px" CellPadding="3">
                                    <Columns>
                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA RECIBIDO EN MIAMI" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:TemplateField HeaderText="Archivo">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="btnUpFile" OnClick="btnLoadFile_Click"
                                                    CommandArgument='<%# Eval("numeroOrdenInterno")+";"+Eval("nombreTransportista")+";"+Eval("fechaRecibidoMiami")%>'
                                                    ImageUrl="~/images/load.png" ToolTip="Actualizar la Factura" Width="20px" />
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
                                <br />
                                <br />
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="accAduana" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/aduana.png" Width="85%" ImageAlign="Left" />
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label3" runat="server" Text="EN TRANSITO Y PROCESO DE ADUANA" Font-Bold="true"></asp:Label>&nbsp
                                             <i class="fa fa-info-circle" aria-hidden="true"  title="SUS PAQUETES HAN SIDO COLOCADOS EN EL SIGUIENTE VUELO DISPONIBLE A
ECUADOR, UNA VEZ HAYAN ARRIBADO INICIARÁN PROCESO DE ADUANA."></i>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <br />
                                <asp:GridView ID="dtgAduana" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    OnRowDataBound="dtgImp_RowDataBound"
                                    Font-Size="X-Small"
                                    EmptyDataText="No Existen envios en transito."
                                    BorderWidth="1px" CellPadding="3">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccione">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkImpuesto" runat="server" AutoPostBack="true" OnCheckedChanged="chkEstadoImp_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                                        <asp:BoundField DataField="precio" HeaderText="PRECIO" />
                                        <asp:BoundField DataField="codigoCategoriaC" HeaderText="CATEGORIA" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA ACTUALIZACIÓN ESTADO" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:BoundField DataField="idImp" HeaderText="idImp" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                    </Columns>
                                    <FooterStyle BackColor="#D5D8DC" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <PagerStyle CssClass="pgr" />
                                </asp:GridView>
                                <br />
                                <div style="width: 100%; margin-left: auto; margin-right: auto; text-align: center">

                                    <asp:Button ID="btnIngImp" Visible="true" runat="server" Font-Size="Small" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Text="Ingresar Impuesto Ctg C" OnClick="btnCargarImpuesto_Click" />

                                    <br />
                                </div>

                                <br />
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="accBodega" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/bodega.png" Width="85%" ImageAlign="Left" />
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label7" runat="server" Text="BODEGA LOIDIMPSA ECUADOR" Font-Bold="true"></asp:Label>&nbsp
                                             <i class="fa fa-info-circle" aria-hidden="true" title="SUS PAQUETES HAN CULMINADO ADUANA Y SE ENCUENTRAN LISTOS EN NUESTRA
BODEGA ECUADOR PARA SER RETIRADOS. A PARTIR DE AHORA PUEDE GENERAR EL
PAGO A TRAVÉS DE SU ORDEN DE RETIRO."></i>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <br />
                                <asp:GridView ID="dtgBodegaLoidimpsa" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    Font-Size="X-Small"
                                    OnRowDataBound="dtgBodegaLoidimpsa_RowDataBound"
                                    EmptyDataText="No Existen envios que se encuentren en la Bodega Loidimpsa en Categoria B."
                                    BorderWidth="1px" CellPadding="3">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Checkout">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="true" OnCheckedChanged="chkEstado_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="TRACKING" />
                                        <asp:BoundField DataField="categoria" HeaderText="CATEGORIA" />
                                        <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-Width="200px" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />

                                        <asp:BoundField DataField="precio" HeaderText="VALOR" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA ACTUALIZACIÓN ESTADO" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                    </Columns>
                                    <FooterStyle BackColor="#D5D8DC" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <PagerStyle CssClass="pgr" />
                                </asp:GridView>

                                <br />
                                <asp:GridView ID="dtgBodegaC" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    Font-Size="X-Small"
                                    OnRowDataBound="dtgBodegaC_RowDataBound"
                                    EmptyDataText="No Existen envios que se encuentren en la Bodega Loidimpsa en categoria C."
                                    BorderWidth="1px" CellPadding="3">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Checkout">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEstadoC" runat="server" AutoPostBack="true" OnCheckedChanged="chkEstadoC_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="TRACKING" />
                                        <asp:BoundField DataField="categoria" HeaderText="CATEGORIA" />
                                        <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-Width="200px" />
                                        <asp:BoundField DataField="precio" HeaderText="VALOR" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA ACTUALIZACIÓN ESTADO" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="idImpC" HeaderText="IMPUESTO" />
                                        <asp:BoundField DataField="valorImp" HeaderText="valorImp" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                         <asp:BoundField DataField="descripImp" HeaderText="descripImp" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                    </Columns>
                                    <FooterStyle BackColor="#D5D8DC" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <PagerStyle CssClass="pgr" />
                                </asp:GridView>

                                <div style="width: 100%; margin-left: auto; margin-right: auto; text-align: center">

                                    <asp:Button ID="btnGenerarOrden" Visible="false" runat="server" Font-Size="Small" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Text="Generar Categoria B" OnClick="btnGenerarOrden_Click" />

                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:Button ID="btnGenerarOrdenC" Visible="false" runat="server" Font-Size="Small" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" Text="Generar Categoria C" OnClick="btnGenerarOrden_Click" />

                                    <br />
                                </div>

                                <br />
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="accFinalizado" runat="server">
                            <Header>
                                <table>
                                    <tr>
                                        <td style="width: 5%; padding-left: 1em; margin: auto">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/finalizado.png" Width="100%" ImageAlign="Left" />
                                        </td>
                                        <td style="width: 90%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label20" runat="server" Text="ENVIOS FINALIZADOS" Font-Bold="true"></asp:Label>
                                            &nbsp
                                             <i class="fa fa-info-circle" aria-hidden="true" title="AQUÍ PODRÁS ENCONTRAR TODOS LOS PAQUETES QUE HAYAS IMPORTADO CON
NOSOTROS."></i>
                                        </td>
                                    </tr>
                                </table>
                            </Header>
                            <Content>
                                <br />
                                <asp:GridView ID="dtgFinalizado" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                    FooterStyle-BackColor="#D5D8DC"
                                    Font-Size="X-Small"
                                    EmptyDataText="No Existen envios finalizados."
                                    BorderWidth="1px" CellPadding="3">

                                    <Columns>

                                        <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                        <asp:BoundField DataField="tracking" HeaderText="TRACKING" />
                                        <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-Width="200px" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" />
                                        <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                        <asp:BoundField DataField="precio" HeaderText="VALOR" />
                                        <asp:BoundField DataField="idTransportista" HeaderText="ID TRANSPORTISTA" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />

                                        <asp:BoundField DataField="fechaRecibidoMiami" HeaderText="FECHA ACTUALIZACIÓN ESTADO" />
                                        <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />
                                        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                    </Columns>
                                    <FooterStyle BackColor="#D5D8DC" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <PagerStyle CssClass="pgr" />
                                </asp:GridView>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </asp:Panel>
                        <asp:HiddenField ID="hddCk" runat="server" />
            <asp:HiddenField ID="hddIdentificacion" runat="server" />
            <asp:HiddenField ID="hddActi" runat="server" />
            <asp:HiddenField ID="hddNombreCliente" runat="server" />
            <asp:HiddenField ID="hddNombreArchivo" runat="server" />
            <asp:HiddenField ID="hddNombreArchivoFactura" runat="server" />
            <asp:HiddenField ID="hddVerificar" runat="server" />
            <br />
            <br />
            <asp:HiddenField ID="hddIdEjecutivo" runat="server" />
            <asp:HiddenField ID="hddIdOrden" runat="server" />
            <asp:HiddenField ID="hddIdOI" runat="server" />
            <asp:HiddenField ID="hddCodigoUsuario" runat="server" />
            <asp:HiddenField ID="hddCategoria" runat="server" />
            <asp:HiddenField ID="hddTotalPeso" runat="server" />
            <asp:HiddenField ID="hddTotalImp" runat="server" />
            <asp:HiddenField ID="hddTotalPesoImp" runat="server" />
            <asp:HiddenField ID="hddTotalSep" runat="server" />
            <asp:HiddenField ID="hddTotalEnvio" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" HorizontalAlign="Center" Width="800px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAutorizar" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="4" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em; width: 25%">
                                        <br />
                                        <asp:Label ID="Label5" Font-Size="X-Small" runat="server" CssClass="subtituloHeader" Text="Fecha Recibido en Miami:"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 25%; text-align: left">
                                        <br />
                                        <asp:Label ID="lblFechaRecibidoMiami" Font-Size="X-Small" ForeColor="Black" runat="server" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 50%; padding: 2em; margin-left: auto; text-align: center; margin-right: auto; border-left: double" colspan="2" rowspan="8">
                                        <br />

                                        <asp:UpdatePanel ID="uppSubirFactura" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                    <div style="width: 100%; margin-left: auto; margin-right: auto">
                                                        <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                       <br />
                                                                       <br />
                                                                       <br />
                                                                    <asp:FileUpload ID="examinarAdjuntoResolutor" runat="server" Font-Names="Leelawadee" ForeColor="Black"
                                                                        Style="margin-left: 1px" Width="90%" Font-Size="X-Small" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center; width: 10%">
                                                                    <br />
                                                                    <asp:Button Text="Cargar Factura" Font-Size="XX-Small" runat="server" BackColor="#1D4289" ID="btnCargarFileResolutor" CausesValidation="False" CssClass="MiButton" OnClick="btnCargarFileResolutor_Click" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="2" style="text-align: center; padding-top: 0px; margin-left: auto; margin-right: auto">
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
                                                             <tr>
                                                                <td colspan="2" padding="3em" style="text-align: left">
                                                                    <br />
                                                                 
                                                                     <asp:CheckBox id="checkbox1" runat="server"  AutoPostBack="True" Text=""  TextAlign="Right"   OnCheckedChanged="Check_Clicked"/>
                                                                    <asp:Label runat="server" ID="Label48" CssClass="subtituloHeader" Font-Size="xx-Small" Text="Declaro que la información de descripción, valor y cantidades; así como el archivo subido como factura es real, y soy responsable de la veracidad de la información aquí declarada." ForeColor="Black" ></asp:Label>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnCargarFileResolutor" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; line-height: 1; padding: 1em" colspan="2">
                                        <br />
                                        <asp:Label ID="Label4" runat="server" Font-Size="X-Small" CssClass="subtituloHeader" Text="Descripción del Envio"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Font-Size="xx-Small" Font-Bold="false" ForeColor="Black"
                                            Text="Añada una  Descripción específica en español
                                            según factura, 
                                            por ejemplo:   " CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label49" runat="server" Font-Size="xx-Small" Font-Bold="false" ForeColor="Black"
                                            Text="1 camiseta, 1 jean, 2 pares de zapatos" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtDescripcion" Font-Size="X-Small" runat="server" Rows="6" Width="95%" TextMode="MultiLine">
                                        </asp:TextBox>

                                    </td>
                                </tr>
                                <tr style="border-bottom: double">
                                    <td style="text-align: justify; padding: 1em" colspan="2">
                                        <asp:Label ID="Label6" runat="server" Font-Size="X-Small" CssClass="subtituloHeader" Text="Valor Envio:"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label16" runat="server" Font-Size="xx-Small" Font-Bold="false" ForeColor="Black"
                                            Text="Añada el valor de la factura del producto." CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtValor" Font-Size="X-Small" class="textboxNormal" runat="server" Width="20%"  OnTextChanged="txtValor_TextChanged" onkeyup="this.value=this.value.replace('.',',')"    onkeypress="return isNumberKey(event)"   ></asp:TextBox>
                                        
                                        <br />

                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlFileUp" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="4" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV2" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo3" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label39" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%" Text="ACTUALIZACIÓN DE FACTURA (EN CASO DE CORRECIÓN DE ARCHIVO)"></asp:Label>

                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: justify; padding: 1em; width: 25%">
                                        <br />
                                        <asp:Label ID="Label57" Font-Size="X-Small" runat="server" CssClass="subtituloHeader" Text="Cargar Factura Nuevamente"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 80%; padding-lef: 1em; margin-left: auto; text-align: center; margin-right: auto; border-left: double" colspan="2" rowspan="8">
                                        <br />

                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                    <div style="width: 100%; margin-left: auto; margin-right: auto">
                                                        <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <asp:FileUpload ID="examinarAdjuntoResolutorB" runat="server" Font-Names="Leelawadee" ForeColor="Black"
                                                                        Style="margin-left: 1px" Width="90%" Font-Size="X-Small" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center; width: 10%">
                                                                    <br />
                                                                    <asp:Button Text="Cargar Factura" Font-Size="XX-Small" runat="server" BackColor="#1D4289" ID="btnCargarFileResolutor2" CausesValidation="False" CssClass="MiButton" OnClick="btnCargarFileResolutor2_Click" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: center; padding-top: 20px; margin-left: auto; margin-right: auto">
                                                                    <asp:GridView ID="gvArchivosResolutor2" runat="server"
                                                                        HorizontalAlign="Center" AutoGenerateColumns="False"
                                                                        CssClass="mGrid"
                                                                        ShowHeader="true"
                                                                        Width="98%"
                                                                        EmptyDataText="No existen archivos subidos."
                                                                        OnRowDeleting="gvArchivosResolutor2_RowDeleting"
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
                                                                    <asp:Label runat="server" ID="lbArchExiste2" Text="Archivo ya fue subido" ForeColor="Red" Visible="False"></asp:Label>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnCargarFileResolutor2" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>

                            </table>
                        </asp:Panel>





                        <asp:Panel ID="pnlImpuesto" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="Label40" Text="INGRESAR IMPUESTO ADUANA" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 2em; width: 100%">
                                        <asp:Label ID="Label41" runat="server" Font-Size="X-Small" Text="Impuestos de Aduana:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label42" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Escoja la opción de Pago de Impuesto gestionada por las siguientes ordenes." CssClass="subtituloHeader"></asp:Label>
                                        <br />

                                        <asp:DropDownList ID="ddlTipoPagoImp" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEnvioImpuesto_SelectedIndexChanged">

                                            <asp:ListItem Selected="True" Text="PAGADO POR CLIENTE" Value="PAGADO POR CLIENTE"></asp:ListItem>
                                            <asp:ListItem Text="PAGADO POR LOIDIMPSA CON REEMBOLSO A CLIENTE" Value="PAGADO POR LOIDIMPSA"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />




                                        <asp:Panel runat="server" Width="100%" Height="100px" ScrollBars="Vertical">
                                            <asp:GridView ID="dtgListadoImpuestoAduana" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                FooterStyle-BackColor="#D5D8DC"
                                                Font-Size="XX-Small"
                                                OnRowDataBound="dtgListadoEnviosImpuesto_RowDataBound"
                                                EmptyDataText="No existen Impuesto a gestionar."
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                                    <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                                    <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                                    <asp:BoundField DataField="peso" HeaderText="PESO" />
                                                    <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </asp:Panel>


                                        <table style="width: 100%">
                                            <tr>


                                                <td style="text-align: left; vertical-align: middle; width: 50%">
                                                    <asp:Label ID="Label43" runat="server" Text="NUMERO DE LIQUIDACION DE ADUANA:" CssClass="subtituloHeader"></asp:Label>
                                                    <br />


                                                    <asp:TextBox ID="txtNumAduana" class="textboxNormal" runat="server" CssClass="form-control" Width="90%" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)" onInput="edValueKeyPress()"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td tyle=" width: 50%">
                                                    <asp:Label ID="Label45" runat="server" Text="OBSERVACION:" Font-Size="X-Small" CssClass="subtituloHeader"></asp:Label>
                                                    <asp:TextBox ID="txtObImp" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                </td>




                                            </tr>
                                            <tr>


                                                <td style="text-align: left; vertical-align: middle; width: 50%">
                                                    <asp:Label ID="Label44" runat="server" Text="VALOR:" CssClass="subtituloHeader"></asp:Label>
                                                    <br />


                                                    <asp:TextBox ID="txtImpuesto" class="textboxNormal" runat="server" CssClass="form-control" Width="90%" onkeyup="this.value=this.value.replace('.',',')" onkeypress="return isNumberKey(event)" onInput="edValueKeyPress()"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>

                                        </table>


                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>



                        <asp:Panel ID="pnlEnvios" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="Label8" Text="GENERAR ORDEN DE ENVIO" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1.5em; width: 100%">
                                        <asp:Label ID="Label14" runat="server" Font-Size="X-Small" Text="Metodo de Entrega:" CssClass="subtituloHeader"></asp:Label>

                                        <asp:Label ID="Label15" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Escoja la opción de Entrega de su preferencia" CssClass="subtituloHeader"></asp:Label>
                                        <br />

                                        <asp:DropDownList ID="ddlTipoEnvio" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEnvio_SelectedIndexChanged">

                                            <asp:ListItem Selected="True" Text="ENVIO A DOMICILIO" Value="ENVIO A DOMICILIO"></asp:ListItem>
                                            <asp:ListItem Text="RETIRAR EN OFICINA" Value="RETIRAR EN OFICINA (GUAYAQUIL)"></asp:ListItem>
                                        </asp:DropDownList>








                                        <asp:Panel ID="pnldtgCatgB" runat="server" Width="100%" Height="100px" ScrollBars="Vertical">
                                            <asp:GridView ID="dtgListadoEnviosGestionar" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                FooterStyle-BackColor="#D5D8DC"
                                                Font-Size="XX-Small"
                                                OnRowDataBound="dtgListadoEnviosGestionar_RowDataBound"
                                                EmptyDataText="No existen envios a gestionar."
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                                    <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                                    <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                                    <asp:BoundField DataField="peso" HeaderText="PESO" />
                                                    <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </asp:Panel>

                                        <asp:Panel ID="pnldtgCatgC" runat="server" Width="100%" Visible="false" Height="100px" ScrollBars="Vertical">
                                            <asp:GridView ID="dtgListadoEnviosGestionarC" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                FooterStyle-BackColor="#D5D8DC"
                                                Font-Size="XX-Small"
                                                OnRowDataBound="dtgListadoEnviosGestionarC_RowDataBound"
                                                EmptyDataText="No existen envios a gestionar."
                                                BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID ORDEN" />
                                                    <asp:BoundField DataField="tracking" HeaderText="Tracking" />
                                                    <asp:BoundField DataField="nombreTransportista" HeaderText="TRANSPORTISTA" />
                                                    <asp:BoundField DataField="peso" HeaderText="PESO" />
                                                       <asp:BoundField DataField="valorImp" HeaderText="VALOR IMPUESTO" />            
                                                    <asp:BoundField DataField="idImpC" HeaderText="idImp" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn" />
                                                <asp:BoundField DataField="descripImp" HeaderText="DESCRIPCION" />
                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    
                                        <table style="width: 100%">
                                            <tr>


                                                <td style="text-align: center; vertical-align: middle; width: 50%">
                                                    <div class="contenedor3">

                                                        <div class="contenido3">
                                                            <div style="border-radius: 15px; padding: 2px 50px 2px 50px; border: 2px solid #1B0C73">

                                                                <asp:DropDownList ID="ddlMetodoPago" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged">
                                                                    <asp:ListItem Text="TRANSFERENCIA/DEPOSITO" Value="TRANSFERENCIA/DEPOSITO" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="EFECTIVO" Value="EFECTIVO"></asp:ListItem>
                                                                    <asp:ListItem Text="TARJETA CREDITO/DEBITO" Value="DEBITO/CREDITO"></asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:Panel ID="pnlCargarPago" runat="server" Width="100%" Visible="false">


                                                                    <%--  Panel Pago trasnferencia--%>
                                                                    <asp:Panel ID="pnlPagoTransferencia" runat="server" Width="100%">
                                                                        <asp:UpdatePanel ID="uppCargarPago" runat="server">
                                                                            <ContentTemplate>
                                                                                <div style="width: 100%;  padding: 3px 0px 0px 0px; margin-left: auto; margin-right: auto">
                                                                                    <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                                                           <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label47" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Seleccionar el Banco donde Realizo la Transferencia" CssClass="subtituloHeader"></asp:Label>
                                                                                                <asp:DropDownList ID="ddlBancos" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoBanco_SelectedIndexChanged">
                                                                                                    <asp:ListItem Text="Seleccione" Value="*" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="BANCO PACIFICO" Value="BANCO PACIFICO"></asp:ListItem>
                                                                                                    <asp:ListItem Text="BANCO PICHINCHA" Value="BANCO PICHINCHA"></asp:ListItem>
                                                                                                                                                                                              
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td   style="padding: 0% 0% 0% 9%;">
                                                                                                <asp:Panel ID="PnlTranferencia" runat="server" Width="70%" Height="40%">
                                                                                                    <table border="1" class="tftable">
                                                                                                        <tr>
                                                                                                            <th colspan="4">
                                                                                                                <asp:Label ID="Label29" runat="server" Text="CUENTAS BANCARIAS"></asp:Label>
                                                                                                            </th>
                                                                                                        </tr>
                                                                                                        <tr>

                                                                                                            <th>
                                                                                                                <asp:Label ID="Label30" runat="server" Text="EMPRESA"></asp:Label>
                                                                                                            </th>
                                                                                                            <th>
                                                                                                                <asp:Label ID="Label21" runat="server" Text="BANCO"></asp:Label>
                                                                                                            </th>
                                                                                                            <th>
                                                                                                                <asp:Label ID="Label32" runat="server" Text="TIPO DE CUENTA"></asp:Label>
                                                                                                            </th>
                                                                                                            <th>
                                                                                                                <asp:Label ID="Label33" runat="server" Text="NÚMERO"></asp:Label>
                                                                                                            </th>

                                                                                                            <tr>
                                                                                                                <td rowspan="2">
                                                                                                                    <asp:Label ID="Label22" runat="server" CssClass="subtituloHeader" Text="RAZON SOCIAL:"></asp:Label>
                                                                                                                    <asp:Label ID="Label34" runat="server" Text="LOIDIMP S.A."></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label26" runat="server" CssClass="subtituloHeader" Text="RUC :"></asp:Label>
                                                                                                                    <asp:Label ID="Label23" runat="server" Text="0993089559001"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label36" runat="server" CssClass="subtituloHeader" Text="CORREO"></asp:Label>
                                                                                                                    <asp:Label ID="Label37" runat="server" Text="info@loidimpsa.com"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label24" runat="server" Text="BANCO PICHINCHA"></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label25" runat="server" Text="CORRIENTE"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label31" runat="server" Text="2100263221"></asp:Label>
                                                                                                                </td>


                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label27" runat="server" Text="BANCO PACÍFICO"></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label28" runat="server" Text="CORRIENTE"></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label35" runat="server" Text="8105437"></asp:Label>

                                                                                                                </td>

                                                                                                            </tr>

                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                     
                                                                                        <tr>
                                                                                            <td style="width: 100%">
                                                                                              
                                                                                                <asp:Label ID="Label11" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Subir el comprobante y luego cargarlo" CssClass="subtituloHeader"></asp:Label>
                                                                                                <asp:FileUpload ID="fupCargarPago" runat="server" Font-Names="Leelawadee" ForeColor="Black"
                                                                                                    Style="margin-left: 1px" Width="100%" Font-Size="X-Small" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center; width: 10%">

                                                                                                <asp:Button Text="Cargar Pago" Font-Size="XX-Small" runat="server" BackColor="#1D4289" ID="btnCargarPago" CausesValidation="False" CssClass="MiButton" OnClick="Button1_Click" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" />
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td colspan="2" style="text-align: center; padding-top: 0px; margin-left: auto; margin-right: auto">
                                                                                                <asp:GridView ID="dtgPago" runat="server"
                                                                                                    HorizontalAlign="Center" AutoGenerateColumns="False"
                                                                                                    CssClass="mGrid"
                                                                                                    ShowHeader="true"
                                                                                                    Width="98%"
                                                                                                    EmptyDataText="No existen archivo de Pago subidos."
                                                                                                    OnRowDeleting="dtgPago_RowDeleting"
                                                                                                    OnSelectedIndexChanged="dtgPago_SelectedIndexChanged">
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
                                                                                                <asp:Label runat="server" ID="lblPagoExiste" Text="Archivo ya fue subido" ForeColor="Red" Visible="False"></asp:Label>

                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>


                                                                                </div>

                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="btnCargarPago" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>

                                                                    </asp:Panel>



                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlCargarEfectivo" runat="server" Width="100%" Visible="false">




                                                                    <%--  Panel Pago Efectivo--%>
                                                                    <asp:Panel ID="pnlEfectivo" runat="server" Width="100%">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <div style="width: 100%; padding: 3px 0px 0px 0px; margin-left: auto; margin-right: auto">
                                                                                    <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label13" runat="server" Text="Efectivo:" CssClass="subtituloHeader"></asp:Label>
                                                                                                <br />
                                                                                                <br />

                                                                                                <asp:TextBox ID="txtValorEfectivo" class="textboxNormal" runat="server" CssClass="form-control" Width="100%" onkeyup="this.value=this.value.replace(',','.')" onkeypress="return isNumberKey(event)" onInput="edValueKeyPress()"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td style="width: 100%">

                                                                                                <br />
                                                                                                <asp:Label ID="Label74" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Lo que corresponden cancelar o devolver es " CssClass="subtituloHeader"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center; width: 10%">
                                                                                                <br />
                                                                                                <br />

                                                                                                <asp:Label ID="lblValorEfectivo" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="$" CssClass="subtituloHeader"></asp:Label>

                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>

                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>



                                                                                    </table>


                                                                                </div>

                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>

                                                                    </asp:Panel>

                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlCargarDebito" runat="server" Width="100%" Visible="false">




                                                                    <%--  Panel Pago Debito--%>
                                                                    <asp:Panel ID="pnlDebito" runat="server" Width="100%">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <div style="width: 100%; padding: 3px 0px 0px 0px; margin-left: auto; margin-right: auto">
                                                                                    <table style="width: 100%; margin-left: auto; margin-right: auto">


                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Button ID="btnPagoTarjeta" CssClass="button" Text="LINK DE PAGO" runat="server" BackColor="#1D4289" ForeColor="White" OnClick="btnPagar_ClickAsync" />

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <br />
                                                                                                <asp:Button ID="btnVerificaPago" CssClass="button" Text="VERIFICAR PAGO" runat="server" BackColor="#4CAF50" ForeColor="White" OnClick="btnVerificar_Click" Visible="false" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%">

                                                                                                <asp:Panel ID="pnlCargando" runat="server" Width="100%" Visible="false" HorizontalAlign="Center">

                                                                                                    <asp:Image ID="imgConfir" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/loading.gif" Width="30%" />
                                                                                                    <br />
                                                                                                    <asp:Label ID="lblConfirm" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Red" Text="" CssClass="subtituloHeader"></asp:Label>
                                                                                                    <asp:Label ID="lblMessage" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="" CssClass="subtituloHeader"></asp:Label>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>


                                                                                </div>

                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>

                                                                    </asp:Panel>

                                                                </asp:Panel>


                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>

                                                <td style="text-align: center; width: 50%">

                                                    <div class="contenedor4">
                                                        <div class="contenido4">
                                                            <div style="border-radius: 15px; padding: 30px 50px 22px 45px; border: 2px solid #1B0C73">

                                                                <asp:GridView ID="dtgCalculoEnvioB" HorizontalAlign="Center" runat="server" AutoGenerateColumns="true"
                                                                    ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGridE" Visible="true"
                                                                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                                                                    FooterStyle-BackColor="#D5D8DC"
                                                                    Font-Size="X-Small"
                                                                    OnRowDataBound="dtgCalculoEnvioB_RowDataBound"
                                                                    BorderWidth="1px" CellPadding="3">
                                                                </asp:GridView>


                                                                <asp:Label ID="Label10" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="Para actualizar la" CssClass="subtituloHeader"></asp:Label>
                                                                <asp:Label ID="Label18" runat="server" Text="DIRECCIÓN DE ENVIO" Font-Size="X-Small" CssClass="subtituloHeader"></asp:Label>
                                                                <asp:Label ID="Label19" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="dirigirse" CssClass="subtituloHeader"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label38" runat="server" Font-Size="X-Small" Font-Bold="false" ForeColor="Black" Text="al Menu Principal" CssClass="subtituloHeader"></asp:Label>
                                                                <br />


                                                                <br />
                                                                <asp:Label ID="Label17" runat="server" Font-Size="Medium" Text="Total a Pagar: $" CssClass="subtituloHeader"></asp:Label>
                                                                <asp:Label ID="lblValorEnvioDomicilio" runat="server" Font-Size="Medium" CssClass="subtituloHeader"></asp:Label>

                                                                
                                                             
                                                            </div>

                                                            
                                                        
                                                    </div>
                                                                                                                                                                                            
                                                </td>
                                                
                                               
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <br />
                                                    <div style="border-radius: 15px; padding: 1px 10px 5px 46px; border: 2px solid #1B0C73; width: 99%;">
                                                        <asp:Label ID="lblPuntos" runat="server" CssClass="subtituloHeader" Font-Size="10px"></asp:Label>
                                                        &nbsp &nbsp 
                                                         <asp:Button ID="btnVerPro" Visible="false" class="button1" Text="CANJEAR" runat="server" BackColor="#1D4289" ForeColor="White" OnCommand="btnIrCanje_Click"></asp:Button>



                                                        <asp:Label ID="Label12" runat="server" Text="CODIGO CANJE:" Font-Size="X-Small" CssClass="subtituloHeader" Width="20%"></asp:Label>&nbsp &nbsp
                                                         <asp:TextBox ID="txtCodigo" runat="server" Width="50%"></asp:TextBox>
                                                    </div>
                                                </td>
                                               
                                                 <td >
                                                          <div style="display: flex; align-items: center; flex-direction: row-reverse;">
                                                              <div style=" border-radius: 15px; padding: 1px 10px 5px 46px; border: 2px solid #1B0C73; width: 99%;">
                                                             <asp:Label ID="Label50" runat="server" CssClass="subtituloHeader" Font-Size="10px"></asp:Label>
                                                            <asp:Label ID="Label51" runat="server" Text="CODIGO DE DESCUENTO:" Font-Size="X-Small" CssClass="subtituloHeader" Width="20%"></asp:Label>&nbsp &nbsp
                                                             <asp:DropDownList ID="DropDownList1" runat="server" Font-Size="X-Small" AutoPostBack="true" OnTextChanged="DropDownList1_TextChanged" >                                                                            
                                                                                                </asp:DropDownList>
                                                                  <br />
                                                                  <asp:Label ID="Label52" runat="server" Font-Size="Medium" Text="Total a Pagar: $" CssClass="subtituloHeader"></asp:Label>
                                                                <asp:Label ID="lblValorEnvioDomicilioDesc" runat="server" Font-Size="Medium" CssClass="subtituloHeader"></asp:Label>
                                                            </div>  
                                                          </div>
                                                           
                                                        
                                                    </td>
                                            </tr>
                                            
                                        </table>
                        </asp:Panel>
                        <div style="margin-left: auto; margin-right: auto; text-align: center">
                            <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: right; padding-top: 1em; padding-bottom: 1em; padding-right: 1em;">
                            <asp:Button ID="btnActualizarF" runat="server" OnClick="btnActualizarFile_Click" Font-Size="XX-Small" Text="Actualizar Archivo" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp
                            <asp:Button ID="btnGenerarOrdenEnvio" runat="server" OnClick="btnGenerarOrdenEnvio_Click" Font-Size="XX-Small" Text="Generar Orden" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp
                            <asp:Button ID="btnAutorizarEnvio" runat="server" OnClick="btnAutorizarEnvio_Click" Font-Size="XX-Small" Text="Autorizar Envio" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp
                             <asp:Button ID="btnIngresarImp" runat="server" OnClick="btnIngresarImp_Click" Font-Size="XX-Small" Text="Guardar Impuesto" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp
                            <asp:Button ID="btnClose" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" BackColor="Black" Font-Size="XX-Small" CssClass="btn btn-primary btn-lg" Visible="true" />

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
                 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
<a href="https://wa.me/593992282734/?text=Hola,%20tengo%20una%20consulta." class="float" target="_blank">
<i class="fa fa-whatsapp my-float"></i>
</a>
</asp:Content>
