<%@ Page Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ServicioTicket.aspx.cs" Inherits="LOADIMPSA.ServicioTicket" %>

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
        function MuestraOculta(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                div.style.display = "block";

            }
            else {
                //const strMaterias = document.getElementById('_materias').value;
                //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                div.style.display = "none";

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

        .rbl input[type="radio"] {
            margin-left: 20px;
            margin-right: 5px;
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

            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <asp:Label runat="server" Text="TICKETS DE SERVICIO" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <br />
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">


                <asp:RadioButtonList ID="panelId" runat="server" CssClass="rbl" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="radio_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Ingresar Ticket" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Consultar" Value="1"></asp:ListItem>
                </asp:RadioButtonList>


            </div>
            <br />

            <asp:Panel ID="PanelIngreso" runat="server" HorizontalAlign="Center">
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
                                <td colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="INGRESO DE TICKET" Font-Bold="True" Font-Size="10px" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Número Tracking:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTracking" class="textboxNormal" runat="server" CssClass="form-control" Width="45%"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 50%; margin-left: auto; margin-right: auto; text-align: left">
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Transportista" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlTransportista" OnSelectedIndexChanged="ddlTransportista_SelectedIndexChanged" AutoPostBack="true" class="textboxNormal" runat="server" CssClass="form-control" Width="90%">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:TextBox ID="txtOtroTransportista" class="textboxNormal" runat="server" CssClass="form-control" Width="90%" Visible="false" OnTextChanged="txtOtroTransportista_TextChanged"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto">
                                    <asp:Label ID="Label4" runat="server" Text="Contenido:" CssClass="subtituloHeader"></asp:Label>
                                    <br />


                                    <asp:TextBox ID="txtContenido" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="90%" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>

                                </td>


                                <td style="width: 50%; text-align: left; margin-left: auto; margin-right: auto" colspan="2">

                                    <asp:Label ID="Label7" runat="server" Text="Descripción del Problema:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtDescripcion" class="textboxNormal" TextMode="MultiLine" Rows="4" Columns="100" runat="server" CssClass="form-control" Width="90%" OnTextChanged="txtDescripcion_TextChanged"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 33%; text-align: center; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <asp:Button ID="btnIngresarOrden" runat="server" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Font-Size="9px" OnClick="btnIngresarOrden_Click" Text="Ingresar Ticket" />
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

            <asp:Panel ID="PanelConsulta" runat="server" HorizontalAlign="Center">
                <br />
                <table style="width: 70%; margin-left: auto; margin-right: auto">
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Fecha Inicio" CssClass="subtituloHeader"></asp:Label>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Fecha Fin:" CssClass="subtituloHeader"></asp:Label>
                            <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:Label ID="Label12" runat="server" Text="No. Ticket:" CssClass="subtituloHeader"></asp:Label>
                              &nbsp&nbsp
                            <asp:TextBox ID="txtOrdenTicket"  runat="server" Width="70%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Estado: " CssClass="subtituloHeader"></asp:Label>
                        
                       

                  
                            <asp:DropDownList ID="ddlEstadoOrden"   runat="server"    Width="80%">
                                <asp:ListItem Text="Todos" Value="%"></asp:ListItem>
                                <asp:ListItem Text="INGRESADA" Value="INGRESADA"></asp:ListItem>
                                <asp:ListItem Text="EN PROCESO" Value="PROCESO"></asp:ListItem>
                                <asp:ListItem Text="RESUELTO" Value="RESUELTO"></asp:ListItem>

                            </asp:DropDownList>

                        </td>


                        <td>
                            <asp:Button ID="Button1" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_ClickC" />

                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:Panel ID="pnlListado" runat="server" HorizontalAlign="Center" Height="800px" Width="100%" Visible="false" ScrollBars="Vertical">
                    <asp:GridView ID="dtgEnvios" runat="server" HorizontalAlign="Center"
                        Font-Size="X-Small"
                        AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="dtgTicket_RowDataBound"
                        ShowHeaderWhenEmpty="True"
                        CssClass="mGrid"
                        EmptyDataText="No hay envios en curso.">
                        <Columns>

                            <asp:TemplateField HeaderText="Resolver" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnRegistroCheckOut" OnClick="btnRegistroCheckOut_Click"
                                        CommandArgument='<%# Eval("NombreCompleto")+";"+Eval("idTicket")%>'
                                        ImageUrl="~/images/user.png" ToolTip="Check Out" Width="20px" />
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:BoundField DataField="idTicket" HeaderText="ID TICKET" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="CLIENTE" />
                            <asp:BoundField DataField="idTracking" HeaderText="ID TRACKING" />
                            <asp:BoundField DataField="nombreTransportista" HeaderText="CARRIER" />
                            <asp:BoundField DataField="usuarioRegistro" HeaderText="USUARIO REGISTRO" />
                            <asp:BoundField DataField="nombreAgente" HeaderText="EJECUTIVO" />
                            <asp:BoundField DataField="descripContenido" HeaderText="CONTENIDO" />
                            <asp:BoundField DataField="descripProblema" HeaderText="PROBLEMA" />
                            <asp:BoundField DataField="statusTicket" HeaderText="ESTADO TICKET" />
                            <asp:BoundField DataField="fechaRegistroTicket" HeaderText="FECHA REGISTRO" />


                            <asp:TemplateField HeaderText="Eliminar Ticket">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnBorrarGuia" OnClick="btnBorrarTicket_Click"
                                        CommandArgument='<%# Eval("idTicket")+";"+Eval("nombreAgente")%>'
                                        ImageUrl="~/images/trash.png" ToolTip="Borrar Ticket" Width="20px" OnClientClick="Confirm();" />
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ver Resuelto">
                                <ItemTemplate>
                                    <a href="javascript:MuestraOculta('div<%# Eval("idTicket")%>', 'one');">
                                        <img id="imgdiv <%# Eval("idTicket")%>" src="images/plus.png" width="30" height="30" />
                                    </a>

                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="10">
                                            <div id="div<%# Eval("idTicket") %>" style="display: none; font-size: 11px; position: relative; left: 15px; overflow: auto; width: 100%; margin: 6px;">
                                                <asp:GridView ID="dtgTicketDetalle" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="95%"
                                                    ShowHeader="true"
                                                    Font-Size="XX-Small"
                                                    HeaderStyle-BackColor="#1D4289"
                                                    ShowFooter="true"
                                                    HeaderStyle-ForeColor="White"
                                                    OnRowDataBound="dtgTicketDetalle_RowDataBound"
                                                    ShowHeaderWhenEmpty="true"
                                                    HorizontalAlign="Center" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:BoundField DataField="idTicket" HeaderText="ID TICKET" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="comentario" HeaderText="COMENTARIO" />
                                                        <asp:BoundField DataField="userRegistro" HeaderText="USUARIO REGISTRO" />
                                                        <asp:BoundField DataField="fecha_Registro_Comentario" HeaderText="FECHA REGISTRO" ItemStyle-Width="400px" />



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

                <asp:Button ID="hButton" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="100%" Style="display: none" CssClass="modalPopup">
                    <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV" Text="FINALIZAR SERVICIO TICKET:" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label10" runat="server" CssClass="subtituloHeader" Text="Nombre Cliente:"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNombreCliente" runat="server" CssClass="subtituloHeader" ForeColor="Black"></asp:Label>
                                        <br />
                                    </td>

                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label6" runat="server" Text="Estado:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="ddlEstadoTicket" class="textboxNormal" runat="server" CssClass="form-control" Width="90%" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Text="PROCESO" Value="PROCESO"></asp:ListItem>
                                            <asp:ListItem Text="RESUELTO" Value="RESUELTO"></asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="TextBox1" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" Visible="false" OnTextChanged="txtOtroTransportista_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 2em">
                                        <asp:Label ID="Label11" runat="server" CssClass="subtituloHeader" Text="Comentario de Resolución:"></asp:Label>
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
                                <asp:Button ID="btnCheckOut" runat="server" OnClick="btnCheckOut_ClickR" Font-Size="Small" Text="Registrar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
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
