<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Menu.Master" CodeBehind="Productos.aspx.cs" Inherits="LOADIMPSA.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (confirm("Esta seguro que desea eliminar el producto?")) {
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
                <asp:Label runat="server" Text="PRODUCTOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <br />
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">


                <asp:RadioButtonList ID="panelId" runat="server" CssClass="rbl" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="radio_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Ingresar Producto" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Consultar" Value="1"></asp:ListItem>
                </asp:RadioButtonList>


            </div>
            <br />

            <asp:Panel ID="PanelIngreso" runat="server" HorizontalAlign="Center">

                <br />
                <br />

                <br />

                <br />
                <asp:Panel ID="pnlClientesTracking" runat="server" Width="100%" HorizontalAlign="Center" Visible="false">
                    <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                        <table style="margin-left: auto; margin-right: auto; width: 80%">
                            <tr style="background: #1D4289">
                                <td colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="INGRESO DE PRODUCTOS" Font-Bold="True" Font-Size="10px" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Nombre del Producto:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtNombreProdu" class="textboxNormal" runat="server" CssClass="form-control" Width="85%"></asp:TextBox>
                                </td>
                                <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Cantidad de Producto:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCantidaProdu" class="textboxNormal" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" Width="85%"></asp:TextBox>
                                </td>
                            </tr>


                            <tr>
                                <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                    <br />
                                    <asp:Label ID="Label4" runat="server" Text="Puntos del Producto:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPuntos" class="textboxNormal" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="85%"></asp:TextBox>
                                </td>
                                <td style="width: 33%; text-align: center; margin-left: auto; margin-right: auto" rowspan="5">

                                    <asp:UpdatePanel ID="uppSubirFactura" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                <div style="width: 100%; margin-left: auto; margin-right: auto">
                                                    <table style="width: 100%; margin-left: auto; margin-right: auto">
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <asp:FileUpload ID="examinarAdjuntoResolutor" runat="server" Font-Names="Leelawadee" ForeColor="Black"
                                                                    Style="margin-left: 1px" Width="85%" Font-Size="X-Small" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="text-align: center; width: 10%">

                                                                <asp:Button Text="Cargar Imagen" Font-Size="XX-Small" runat="server" BackColor="#1D4289" ID="btnCargarFileResolutor" CausesValidation="False" CssClass="MiButton" OnClick="btnCargarFileResolutor_Click" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" />
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
                                            <asp:HiddenField ID="hddNombreArchivo" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnCargarFileResolutor" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>

                                <td style="width: 33%; text-align: center; margin-left: auto; margin-right: auto" colspan="2">
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnIngresarOrden" runat="server" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Font-Size="9px" OnClick="btnIngresarProducto_Click" Text="Ingresar Producto" />
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
                            <asp:Label ID="Label9" runat="server" Text="Estado: " CssClass="subtituloHeader"></asp:Label>




                            <asp:DropDownList ID="dllProductEstado" runat="server" Width="80%">
                                <asp:ListItem Text="TODOS" Value="2"></asp:ListItem>
                                <asp:ListItem Text="VISIBLES" Value="1"></asp:ListItem>
                                <asp:ListItem Text="OCULTO" Value="0"></asp:ListItem>


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
                    <asp:GridView ID="dtgProductos" runat="server" HorizontalAlign="Center"
                        Font-Size="X-Small"
                        AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="dtgTicket_RowDataBound"
                        ShowHeaderWhenEmpty="True"
                        CssClass="mGrid"
                        EmptyDataText="No hay productos para mostrar.">
                        <Columns>

                            <asp:TemplateField HeaderText="Editar" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnRegistroCheckOut" OnClick="btnEditar_Click"
                                        CommandArgument='<%# Eval("idCatalogo")  + ";" + Eval("nombreUnico") + ";" + Eval("puntos") + ";" + Eval("cantidadProdcuto") + ";" + Eval("estadoProducto") %>'
                                        ImageUrl="~/images/edit.png" ToolTip="Check Out" Width="20px" />
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:BoundField DataField="idCatalogo" HeaderText="ID CATALOGO" />
                            <asp:BoundField DataField="nombreUnico" HeaderText="NOMBRE PRODUCTO" />
                            <asp:BoundField DataField="puntos" HeaderText="PUNTOS" />
                            <asp:BoundField DataField="cantidadProdcuto" HeaderText="CANTIDAD PRODUCTOS" />
                            <asp:BoundField DataField="idImgCatalog" HeaderText="ARCHIVO" />






                            <asp:TemplateField HeaderText="Eliminar Producto">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnBorrarGuia" OnClick="btnBorrarTicket_Click"
                                        CommandArgument='<%# Eval("idCatalogo")%>'
                                        ImageUrl="~/images/trash.png" ToolTip="Borrar Imagen" Width="20px" OnClientClick="Confirm();" />
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

                <asp:Button ID="hButton" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="100%" Style="display: none" CssClass="modalPopup">
                    <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="lblTituloV" Text="EDITAR PRODUCTO:" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                        <br />
                                        <asp:Label ID="Label5" runat="server" Text="Nombre del Producto:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtName" class="textboxNormal" runat="server" CssClass="form-control" Width="85%"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                        <br />
                                        <asp:Label ID="Label6" runat="server" Text="Cantidad de Producto:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtCount" class="textboxNormal" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="width: 40%; text-align: left; margin-left: auto; margin-right: auto">
                                        <br />
                                        <asp:Label ID="Label7" runat="server" Text="Puntos del Producto:" CssClass="subtituloHeader"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtPtn" class="textboxNormal" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="85%"></asp:TextBox>
                                    </td>
                                
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Estado: " CssClass="subtituloHeader"></asp:Label>
                                     <asp:DropDownList ID="ddlEstadoActi" runat="server" Width="100%">
                                        <asp:ListItem Text="VISIBLES" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="OCULTO" Value="0"></asp:ListItem>


                                    </asp:DropDownList>

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
                                <asp:Button ID="btnCheckOut" runat="server" OnClick="btnCheckOut_ClickR" Font-Size="Small" Text="Actualizar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
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

