<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="AdministracionUsuarios.aspx.cs" Inherits="LOADIMPSA.AdministracionUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/GridStyle.css" rel="stylesheet" />
    <script>
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (confirm("¿Esta seguro que desea eliminar el Cliente?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style>
        .tituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #fff;
            font-size: 30px;
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
            font-size: 14px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function jScript() {
            $(document).ready(function () {
                $('#<%= txtFilCod.ClientID%>').keyup(function (event) {
                    event.preventDefault();
                    var searchKey = $('#<%= txtFilCod.ClientID%>').val().toLowerCase();
                    $('#<%= dtgUsuarios.ClientID%> tr td:nth-child(2)').each(function () {
                        var cellText = $(this).text().toLowerCase();
                        if (cellText.indexOf(searchKey) >= 0) {
                            $(this).parent().show();
                        }
                        else {
                            $(this).parent().hide();
                        }
                    });

                });
            });
        }
        function jScript2() {
            $(document).ready(function () {
                $('#<%= txtFilIdent.ClientID%>').keyup(function (event) {
                    event.preventDefault();
                    var searchKey = $('#<%= txtFilIdent.ClientID%>').val().toLowerCase();
                    $('#<%= dtgUsuarios.ClientID%> tr td:nth-child(3)').each(function () {
                        var cellText = $(this).text().toLowerCase();
                        if (cellText.indexOf(searchKey) >= 0) {
                            $(this).parent().show();
                        }
                        else {
                            $(this).parent().hide();
                        }
                    });

                });
            });
        }
        function jScript3() {
            $(document).ready(function () {
                $('#<%= txtFilNom.ClientID%>').keyup(function (event) {
                    event.preventDefault();
                    var searchKey = $('#<%= txtFilNom.ClientID%>').val().toLowerCase();
                    console.log(searchKey);
                    $('#<%= dtgUsuarios.ClientID%> tr td:nth-child(4)').each(function () {
                        var cellText = $(this).text().toLowerCase();
                        console.log(cellText);
                        if (cellText.indexOf(searchKey) >= 0) {
                            $(this).parent().show();
                        }
                        else {
                            $(this).parent().hide();
                            console.log("estoy aki");
                        }
                    });

                });
            });
        }
        function jScript4() {
            $(document).ready(function () {
                $('#<%= ddlRoles.ClientID%>').change(function (event) {
                    event.preventDefault();
                    var searchKey = $('#<%= ddlRoles.ClientID%>').val();
                    console.log(searchKey);

                    $('#<%= dtgUsuarios.ClientID%> tr td:nth-child(5)').each(function () {
                        var cellText = $(this).text();
                       
                        if(cellText != (searchKey)) {
                            $(this).parent().hide();
                            $(this).parent().next().hide();
                        }
                        else {
                            $(this).parent().show();
                            $(this).parent().next().show();
                        }
                        if (searchKey == "*") {
                            $(this).parent().show();
                            //add this to show all filter to include cracker class too
                            $(this).parent().next().show();
                        }
                    });

                });
            });
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="Stylesheet" href="css/TabStyles.css" type="text/css" media="screen" />
    <asp:UpdatePanel ID="uppUsuarios" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(jScript);
                Sys.Application.add_load(jScript2);
                Sys.Application.add_load(jScript3);
                Sys.Application.add_load(jScript4);
                Sys.Application.add_load(jScript5);
            </script>
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <link rel="Stylesheet" href="css/TabStyles.css" type="text/css" media="screen" />
                <asp:Label runat="server" Text="USUARIOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                <br />
                <br />
                <cc1:TabContainer ID="tabDatos" runat="server" ActiveTabIndex="0" CssClass="Tab" Width="100%">
                    <cc1:TabPanel ID="pnlnUsuariosSisacad" runat="server" HeaderText="Usuarios LOIDIMPSA" TabIndex="1">
                        <ContentTemplate>
                            <table style="width: 100%; text-align: center">
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <asp:Label ID="Label6" runat="server" CssClass="subtituloHeader" Text="ADMINISTRACIÓN DE USUARIOS "></asp:Label><br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFilCod" placeholder="Filtro Código" runat="server" CssClass="MiTexto"></asp:TextBox><br />
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFilIdent" placeholder="Filtro Identificación" runat="server" CssClass="MiTexto"></asp:TextBox><br />
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFilNom" placeholder="Filtro Nombres" runat="server" CssClass="MiTexto"></asp:TextBox><br />
                                        <br />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" Width="80%" >
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="pnlUsuarios" runat="server" Height="450px" ScrollBars="Auto">
                                            <asp:GridView runat="server" ID="dtgUsuarios" AutoGenerateColumns="False"
                                                Width="100%" OnRowDeleting="dtgUsuarios_RowDeleting" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                CssClass="mGrid"
                                                PageSize="7">

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cod_usu" HeaderText="Código">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo Usuario">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estado" HeaderText="Estado">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:BoundField>
                                                    <asp:CommandField DeleteText="Activar/Desactivar" HeaderText="Cambiar Estado" ShowDeleteButton="True">
                                                        <HeaderStyle CssClass="header" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Eliminar Cliente">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="btnBorrarCliente" OnClick="btnBorrarCliente_Click"
                                                                CommandArgument='<%# Eval("identificacion")+";"+Eval("cod_usu")%>'
                                                                ImageUrl="~/images/borrarguia.png" ToolTip="Borrar Guía" Width="20px" OnClientClick="Confirm();" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnResetear" CssClass="login100-form-btn" runat="server" Text="Resetear" OnClick="btnResetear_Click" /></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
