<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Menu.Master" CodeBehind="CotizacionesCenvidas.aspx.cs" Inherits="LOADIMPSA.CotizacionesCenvidas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            top: 30%;
            left: 30%;
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
            left: 5%;
            right: 55%;
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
                <asp:Label runat="server" Text="COTIZACIONES GENERADAS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <asp:Panel ID="pnlFiltros" runat="server" HorizontalAlign="Center" Width="100%">
                <cc1:accordion id="Accordion1" runat="server" headercssclass="cssHeader" headerselectedcssclass="cssHeaderSelected"
                    contentcssclass="cssContent" selectedindex="0" fadetransitions="true"
                    suppressheaderpostbacks="true" transitionduration="250" framespersecond="40"
                    requireopenedpane="false" autosize="None">
                    <panes>
                        <cc1:accordionpane id="accRecibidoMiami" runat="server">
                            <header>
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
                            </header>
                            <content>
                                <table style="text-align: left; padding-left: 1em">

                                    <tr>
                                       <td style="width: 10%">
                                            <asp:Label ID="Label8" runat="server" Text="Fecha desde:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtFechaIngreso" class="textboxNormal" Font-Size="X-Small" TextMode="Date" runat="server" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label9" runat="server" Text="Fecha Hasta:" Font-Size="X-Small"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtFechaRecbidoMiami" runat="server" Font-Size="X-Small" class="textboxNormal" TextMode="Date" Width="80%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label10" runat="server" Text="Ejecutivo Cuenta:" Font-Size="X-Small"></asp:Label>
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
                                            <asp:Button ID="btnBuscar" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />
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
                    OnRowDataBound="dtgEnvios_RowDataBound"
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
            <asp:HiddenField ID="hddIdentificacion" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                        <asp:ListItem Text="TARGETA DEBITO/CREDITO" Value="ENVIO A DOMICILIO"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label6" runat="server" CssClass="subtituloHeader" Text="No. Factura:"></asp:Label>


                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumFacturaContf" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 1em">
                                    <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Text="Observación:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
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
                            <asp:Button ID="btnCheckOut" runat="server" OnClick="btnCheckOut_Click" Font-Size="Small" Text="Finalizar Importación" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
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
