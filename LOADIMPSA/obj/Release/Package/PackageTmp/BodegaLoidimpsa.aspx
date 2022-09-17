<%@ Page Title="Ordenes de Retiro" Async="true" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="BodegaLoidimpsa.aspx.cs" Inherits="LOADIMPSA.BodegaLoisimpsa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
               .button {
            display: inline-block;
            padding: 10px 0px;
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
            top: 30%;
            left: 30%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
            position: fixed;
            z-index: 999;
        }

        .grdSearchResultbreakword {
            word-wrap: break-word;
            word-break: break-all;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            position: center;
            left: 5%;
            right: 55%;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            width: 200px;
            height: 350px;
            backdrop: 'static'; 
            keyboard: false;
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
            font-size: 11px;
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
        function ObProvincia() {
            UseCallback();
        }

        function getObProvServer(ddlProvinciaBode, context) {
            document.forms[0].ddlProvinciaBode.value = ddlProvinciaBode;
        }

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

        function MuestraOculta(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "images/close.gif";
                }
                else {
                    img.src = "images/close.gif";
                }
                img.alt = "Ocultar detalles";
            }
            else {
                //const strMaterias = document.getElementById('_materias').value;
                //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "images/detail.gif";
                }
                else {
                    img.src = "images/detail.gif";
                }
                img.alt = "Mostrar detalles";
            }
        }



        function VerFile(nameFile) {
            console.log(cedula + nameFile);

            /*   var div = document.getElementById(obj);
               var img = document.getElementById('img' + obj);
   
               if (div.style.display == "none") {
                   //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                   div.style.display = "block";
                   if (row == 'alt') {
                       img.src = "images/close.gif";
                   }
                   else {
                       img.src = "images/close.gif";
                   }
                   img.alt = "Ocultar detalles";
               }
               else {
                   //const strMaterias = document.getElementById('_materias').value;
                   //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                   div.style.display = "none";
                   if (row == 'alt') {
                       img.src = "images/detail.gif";
                   }
                   else {
                       img.src = "images/detail.gif";
                   }
                   img.alt = "Mostrar detalles";
               }*/
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppBodegaLoidimpsa">
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
    <asp:UpdatePanel ID="uppBodegaLoidimpsa" runat="server">
        <ContentTemplate>
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <asp:Label runat="server" Text="ORDENES GENERADAS DE RETIRO" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <table style="width: 70%; margin-left: auto; margin-right: auto">
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Tipo de Entrega: " CssClass="subtituloHeader"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoEntrega" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                            <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                            <asp:ListItem Text="ENVIO A DOMICILIO" Value="ENVIO A DOMICILIO"></asp:ListItem>
                            <asp:ListItem Text="RETIRAR EN OFICINA (GUAYAQUIL)" Value="RETIRAR EN OFICINA "></asp:ListItem>

                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Fecha Inicio" CssClass="subtituloHeader"></asp:Label>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Fecha Fin:" CssClass="subtituloHeader"></asp:Label>
                        <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Estado de Orden: " CssClass="subtituloHeader"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstadoOrden" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                            <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                            <asp:ListItem Text="INGRESADA" Value="INGRESADA"></asp:ListItem>
                            <asp:ListItem Text="REALIZADO CHECK OUT" Value="REALIZADO CHECK OUT"></asp:ListItem>

                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />

                    </td>
                </tr>
            </table>
            <br />
              <asp:HiddenField ID="hddTotalEnvio" runat="server" />
              <asp:HiddenField ID="hddTipoPago" runat="server" />
              <asp:HiddenField ID="hddTipoEntrega" runat="server" />
              <asp:HiddenField ID="hddValorEnvio" runat="server" />
            <br />
            <asp:Panel ID="pnlListado" runat="server" HorizontalAlign="Center" Height="800px" Width="100%" Visible="false" ScrollBars="Vertical">
                <asp:GridView ID="dtgEnvios" runat="server" HorizontalAlign="Center"
                    Font-Size="X-Small"
                    AutoGenerateColumns="False" Width="100%"
                    OnRowDataBound="dtgEnvios_RowDataBound"
                    ShowHeaderWhenEmpty="True"
                    CssClass="mGrid"
                    EmptyDataText="No hay envios en curso.">
                    <Columns>
                        <asp:TemplateField HeaderText="FINALIZAR" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnRegistroCheckOut" OnClick="btnRegistroCheckOut_Click"
                                    CommandArgument='<%# Eval("NombreCompleto")+";"+Eval("idEnvio")+";"+Eval("totalPagoEnvio")+";"+Eval("tipoPago")+";"+Eval("categoria")+";"+Eval("tipoEntrega")%>'
                                    ImageUrl="~/images/checkout.png" ToolTip="Check Out" Width="20px" />
                            </ItemTemplate>
                            <ControlStyle Width="30px" />
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REVISAR PAGO" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnRevisarCheckOut" OnClick="btnRevisarCheckOut_Click"
                                    CommandArgument='<%# Eval("NombreCompleto")+";"+Eval("idEnvio")+";"+Eval("pagoVerificar")%>'
                                    ImageUrl="~/images/dollarAdd.png" ToolTip="Click para confirmar Pago" Width="20px" />
                            </ItemTemplate>
                            <ControlStyle Width="30px" />
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idEnvio" HeaderText="ORDEN DE DESPACHO" />
                        <asp:BoundField DataField="tipoEntrega" HeaderText="TIPO DE ENTREGA" />
                        <asp:BoundField DataField="tipoPago" HeaderText="TIPO DE PAGO" />
                        <asp:BoundField DataField="categoria" HeaderText="CATEGORIA" />
                        <asp:BoundField DataField="fechaRegistro" HeaderText="FECHA REGISTRO" />
                        <asp:BoundField DataField="usuario" HeaderText="USUARIO REGISTRO" />
                        <asp:BoundField DataField="id_rol" HeaderText="TIPO CLIENTE" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="CLIENTE" />
                        <asp:BoundField DataField="EjecutivoCuenta" HeaderText="EJECUTIVO" />
                        <asp:BoundField DataField="totalPesoEnvio" HeaderText="PESO TOTAL" />
                        <asp:BoundField DataField="totalPagoEnvio" HeaderText="PAGO TOTAL" />
                        <asp:BoundField DataField="valorEnvioDomi" HeaderText="VALOR DELIVERY" />
                        <asp:BoundField DataField="detDesc" HeaderText=" % DESCUENTO" />
                         <asp:BoundField DataField="estadoOrden" HeaderText="ESTADO ORDEN" />
                   
                        <asp:BoundField DataField="pagoVerificar" HeaderText="VERIFICAR PAGO" />
                        <asp:BoundField DataField="strFile" HeaderText="Nombre Archivo"
                            ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="true"
                            HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" FooterStyle-CssClass="hideGridColumn"></asp:BoundField>

                        <asp:BoundField DataField="observacion" HeaderText="CODIGO CANJEO" />
                        <asp:TemplateField HeaderText="Archivo" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <a href="javascript:VerFile('div<%# Eval("strFile")%>' );">
                                    <asp:ImageButton runat="server" ID="btnRegistroCheckOut2" OnClick="btnRegistroCheckOut_ClickFile"
                                        CommandArgument='<%# Eval("cedulaNumero") + ";"+ Eval("strFile")%>'
                                        ImageUrl="~/images/buscar.png" ToolTip="Check Out" Width="20px" />
                                    <%--   --%>
                            </ItemTemplate>
                            <ControlStyle Width="30px" />
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Detalle Envio">
                            <ItemTemplate>
                                <a href="javascript:MuestraOculta('div<%# Eval("idEnvio")%>', 'one');">
                                    <img id="imgdiv <%# Eval("idEnvio")%>" src="images/detail.gif" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="10">
                                        <div id="div<%# Eval("idEnvio") %>" style="display: none; font-size: 11px; position: relative; left: 15px; overflow: auto; width: 100%; margin: 6px;">
                                            <asp:GridView ID="dtgEnviosDetalle" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="95%"
                                                ShowHeader="true"
                                                Font-Size="XX-Small"
                                                HeaderStyle-BackColor="#1D4289"
                                                ShowFooter="true"
                                                HeaderStyle-ForeColor="White"
                                                OnRowDataBound="dtgEnviosDetalle_RowDataBound"
                                                ShowHeaderWhenEmpty="true"
                                                HorizontalAlign="Center" GridLines="Vertical">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroOrdenInterno" HeaderText="ID INTERNO" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="tracking" HeaderText="TRACKING" />
                                                    <asp:BoundField DataField="precio" HeaderText="PRECIO" />
                                                    <asp:BoundField DataField="paqueteSeparado" HeaderText="PAQUETE SEPARADO" />
                                                    <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-Width="400px" />
                                                    <asp:BoundField DataField="peso" HeaderText="PESO" />


                                                </Columns>
                                                <FooterStyle BackColor="#D5D8DC" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#D5D8DC" />
                    <AlternatingRowStyle CssClass="alt" />
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>
            </asp:Panel>
              <asp:HiddenField ID="hddValLoidimp" runat="server" />
                  <asp:HiddenField ID="hddValContif" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlFinalizar" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV" Text="FINALIZAR IMPORTACIÓN:" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label3" runat="server" CssClass="subtituloHeader" Text="Nombre Cliente:"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNombreCliente" runat="server" CssClass="subtituloHeader" ForeColor="Black"></asp:Label>
                                        <br />
                                        
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMetodoPagoConfir" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="pagoConfir_SelectedIndexChanged">
                                            <asp:ListItem Text="TRANSFERENCIA/DEPOSITO" Value="TRANSFERENCIA/DEPOSITO"></asp:ListItem>
                                            <asp:ListItem Text="EFECTIVO" Value="EFECTIVO"></asp:ListItem>
                                            <asp:ListItem Text="TARJETA DEBITO/CREDITO" Value="TARJETA DEBITO/CREDITO"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        
                                          <asp:Button ID="btnFacturar" CssClass="button"  visible="false" Text="FACTURAR" runat="server" BackColor="#1D4289" ForeColor="White" OnClick="btnFacturar_ClickAsync" />
                                        &nbsp &nbsp &nbsp
                                        <asp:Label ID="Label6" runat="server" CssClass="subtituloHeader" Text="No. Factura:"></asp:Label>

                                    </td>
                                    <td>
                                        
                                        <asp:TextBox ID="txtNumFacturaContf" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                          <asp:Label ID="lblConfirFac" runat="server" CssClass="subtituloHeader" Text=""></asp:Label>
                                        <br />
                                        <asp:Button ID="btnVerDoc" runat="server"  Font-Size="Small" Text="Generar Número" OnClick="btnVerDoc_Click" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                                        <asp:Button ID="Button1" runat="server" OnClick="btnGuardarFac_Click" Font-Size="Small" Text="Guardar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td  style="text-align: justify; padding: 1em">
                                        <asp:Panel id="pnlRegistro" runat="server" Visible="false"> 
                                                <asp:Label ID="Label13" runat="server" CssClass="subtituloHeader" Text="Enviado por"></asp:Label>
                                            <asp:Label ID="lblEnvido" runat="server" ></asp:Label><br />
                                                        <asp:Label ID="Label14" runat="server" CssClass="subtituloHeader" Text="Num Seguimiento"></asp:Label>
                                                 <asp:Label ID="lblNumSe" runat="server" ></asp:Label><br />
                                      
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em"  rowspan="5">
                                        <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Text="Observación:"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                                    </td>
                                    
                                     <td style="text-align: justify; padding: 1em">
                                       
                                         <asp:Panel ID="pnlAggEnvio" runat="server"  Visible="false">
                                               <asp:Label ID="Label12" runat="server" CssClass="subtituloHeader" Text="Envio por:"></asp:Label>
                                               <asp:DropDownList ID="ddlEnviado" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="pagoConfir_SelectedIndexChanged">
                                              <asp:ListItem Text="SELECCIONE" Value=""></asp:ListItem>
                                                   <asp:ListItem Text="SERVIENTREGA" Value="SERVIENTREGA"></asp:ListItem>
                                            <asp:ListItem Text="LOIDIMP S.A." Value="LOIDIMP S.A."></asp:ListItem>
                                            <asp:ListItem Text="OTROS" Value="OTROS"></asp:ListItem>
                                        </asp:DropDownList> 
                                       
                                         </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Panel ID="pnlAggProv" runat="server" Visible="false">
                                            <asp:Label ID="Label15" runat="server" CssClass="subtituloHeader" Text="Provincia:"></asp:Label>
                                            <asp:DropDownList ID="ddlProvinciaBode" Font-Size="X-Small" runat="server" AutoPostBack="true" OnTextChanged="ddlProvinciaBode_TextChanged"
                                                >
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Panel ID="pnlAggCiudad" runat="server" Visible="false">
                                            <asp:Label ID="Label16" runat="server" CssClass="subtituloHeader" Text="Ciudad:"></asp:Label>
                                            <asp:DropDownList ID="ddlCiudadBode" Font-Size="X-Small" runat="server" AutoPostBack="false" 
                                             >
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td  style="text-align: justify; padding: 1em">
                                        <asp:Panel id="pnlSegum" runat="server" Visible="false"> <asp:Label ID="Label10" runat="server" CssClass="subtituloHeader" Text="Numero de Seguimiento:"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtNumSeguim" runat="server"  Width="90%"   AutoPostBack="false" ></asp:TextBox></asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td  style="text-align: justify; padding: 1em">
                                        <asp:Panel id="pnlGuardar" runat="server" Visible="false"> 
                                       <asp:Button ID="btnGuardarEn" runat="server" OnClick="btnGuardarEnv_Click" Font-Size="Small" Text="Guardar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                     </asp:Panel>
                                    </td>
                                </tr>

                                
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlRevisar" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="Label7" Text="REVISAR PAGO:" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lblorden" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label9" runat="server" CssClass="subtituloHeader" Text="Nombre Cliente:"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblnombreC" runat="server" CssClass="subtituloHeader" ForeColor="Black"></asp:Label>
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label11" runat="server" CssClass="subtituloHeader" Text="Confirmar:"></asp:Label>


                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpagoVerifica" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="pagoConfir_SelectedIndexChanged">
                                            <asp:ListItem Text="PAGO NO VERIFICADO" Value="PAGO NO VERIFICADO"></asp:ListItem>
                                            <asp:ListItem Text="PAGO VERIFICADO" Value="PAGO VERIFICADO"></asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label8" runat="server" CssClass="subtituloHeader" Text="Observación:"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtObservacion2" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
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
                            <br />
                            <asp:Button ID="btnCheckOut" runat="server" OnClick="btnCheckOut_Click" Font-Size="Small" Text="Finalizar Importación" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                             <asp:Button ID="btnEnvio" runat="server" OnClick="btnEnvio_Click" Font-Size="Small" Text="Agregar Envio" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Font-Size="Small" Text="Confirmar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
