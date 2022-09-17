<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="TransferenciaClientes.aspx.cs" Inherits="LOADIMPSA.ListadoTracking" %>

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
            <asp:Panel runat="server" HorizontalAlign="Center" Width="100%">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="TRANSFERENCIA CLIENTES" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />

            </asp:Panel>
            <asp:Panel ID="pnlGeneral"  Visible="false" runat="server" HorizontalAlign="Center">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <table style="width: 50%">
                        <tr>
                            <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="Empleados:" CssClass="subtituloHeader"></asp:Label>
                                <br />
                                <br />
                                <asp:DropDownList ID="ddlEjecutivos" runat="server" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlEjecutivos_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <br />
                            </td>
                            <td style="width: 25%; text-align: right; margin-left: auto; margin-right: auto" colspan="3">
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Cantidad de Clientes:" CssClass="subtituloHeader"></asp:Label>
                            </td>
                            <td style="width: 25%; text-align: center; margin-left: auto; margin-right: auto" colspan="3">
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="#" CssClass="subtituloHeader"></asp:Label>

                            </td>

                        </tr>
                    </table>
                </div>
    </asp:Panel>
                
                <asp:Panel ID="pnlBusquedaClientes" runat="server" HorizontalAlign="Center">
                      <br />
                <br />
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
                    </asp:Panel>
                </asp:Panel>
              
                <asp:Panel runat="server" ID="pnlClientes" Width="100%" HorizontalAlign="Center" ScrollBars="Vertical">
                      <br />
                <br />
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
            <asp:Panel ID="pnlListadoClientes" runat="server" Visible="false" HorizontalAlign="Center" Width="100%" Height="600px" ScrollBars="Auto">
                <br />
                <br />
                <asp:GridView ID="dtgListadoClientes" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True" Width="95%" CssClass="mGrid"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" ShowFooter="True"
                    FooterStyle-BackColor="#D5D8DC"
                    EmptyDataText="No Existen clientes asignados para este Empleado."
                    BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkRowTodos" runat="server" AutoPostBack="true" OnCheckedChanged="chkRowTodos_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idCasillero" HeaderText="Casillero" />
                        <asp:TemplateField HeaderText="Identificación">
                            <ItemTemplate>
                                <asp:Label ID="lblIdentificacionCliente" runat="server" Text='<%# Bind("numeroIdentificacion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Cliente">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("NombreCliente") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:BoundField DataField="correo" HeaderText="Correo" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                       
                        <asp:BoundField DataField="canton" HeaderText="Cantón" />
                        <asp:BoundField DataField="provincia" HeaderText="Provincia" />
                   
                        <asp:TemplateField HeaderText="idEjecutivoCuenta">
                            <ItemTemplate>
                                <asp:Label ID="lblIdEjectivoCuenta" runat="server" Text='<%# Bind("idEjecutivoCuenta") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle CssClass="hideGridColumn" />
                            <HeaderStyle CssClass="hideGridColumn" />
                            <ItemStyle CssClass="hideGridColumn" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="direccionEntrega" HeaderText="Direccion Entrega" ItemStyle-Wrap="true">
                            <ItemStyle Wrap="True" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Transferencia">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnTransferir" OnClick="btnTransferir_Click"
                                    CommandArgument='<%# Eval("numeroIdentificacion")+";"+Eval("idEjecutivoCuenta")+";"+Eval("NombreCliente")%>'
                                    ImageUrl="~/images/iconoTransferencia.png" ToolTip="Transferir Cliente" Width="20px" />
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

                <div style="margin-left: auto; margin-right: auto; text-align: center">
                    <br />
                    <asp:Button ID="btnTransferirClientes" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" BackColor="#1D4289" Text="Transferir Clientes" OnClick="btnTransferirClientes_Click" />
                    <br />
                    <br />
                    <br />
                </div>
            </asp:Panel>
            <br />
            <br />
            <asp:HiddenField ID="hddIdEjecutivo" runat="server" />
            <asp:HiddenField ID="hddIdentificacion" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="650px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr style="background-color: #1D4289">
                                <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                    <asp:Label ID="lblTituloV" Text="TRANSFERIR CLIENTE " CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 2em">
                                    <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Text="Escoja el empleado a transferir:"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Empleados:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlEmpleadosTransferir" runat="server" Width="80%">
                                    </asp:DropDownList>
                                    <br />
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
                            <asp:Button ID="btnTransferirClienteMasivo" runat="server" OnClick="btnTransferirClienteMasivo_Click" Font-Size="Small" Text="Transferir" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="false" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnTransferirCliente" runat="server" OnClick="btnTransferirCliente_Click" Font-Size="Small" Text="Transferir" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
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
