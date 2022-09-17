<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="RegistroEmpleado.aspx.cs" Inherits="LOADIMPSA.RegistroEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat' rel='stylesheet' type='text/css' />

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
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
        <script type="text/javascript">
       
            function verificaElemento(ddl) {
                combobox = ddl.value;
                var lbNombre = document.getElementById("<%=Label4.ClientID %>");
            var lbsgNombre = document.getElementById("<%=Label5.ClientID %>");
            var lbApellido = document.getElementById("<%=Label6.ClientID %>");
            var lbsegApellido = document.getElementById("<%=Label7.ClientID %>");
            var txtsgNombre = document.getElementById("<%=txtSegundoNombre.ClientID %>");
            var txtpApellido = document.getElementById("<%=txtprimerApellido.ClientID %>");
            var txtsegApellido = document.getElementById("<%=txtSegundoApellido.ClientID %>");


                if (combobox == '3') {
                    lbNombre.innerHTML = "Razón Social";
                    lbApellido.innerHTML = "Nombre Comercial";
                    txtsgNombre.disabled = true;
                    txtsegApellido.disabled = true;
                    txtsgNombre.value = "";
                    txtsegApellido.value = "";
                }
                else if (combobox == "2") {
                    lbNombre.innerHTML = "Primer Nombre";
                    lbApellido.innerHTML = "Segundo Nombre";
                    txtsgNombre.disabled = false;
                    txtsegApellido.disabled = false;

                }
                else if (combobox = "1") {
                    lbNombre.innerHTML = "Primer Nombre";
                    lbApellido.innerHTML = "Segundo Nombre";
                    txtsgNombre.disabled = false;
                    txtsegApellido.disabled = false;
                }

        }

         
            
        

        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppEmpleados">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress" style="width: 250px; height: 250px">
                    <div id="theprogress">
                        <asp:Image ID="imgWaitIcon1" Width="40%" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/logoPrincipal.png" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uppEmpleados" runat="server">
        <ContentTemplate>

            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <asp:Label runat="server" Text="REGISTRO EMPLEADOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
            </div>
            <br />
            <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                <table style="margin-left: auto; margin-right: auto; width: 80%">
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <asp:Label ID="Label2" runat="server" Text="Tipo Documento"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlTipoIdentificacion" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onchange="verificaElemento(this)">
                                <asp:ListItem Text="Cédula" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pasaporte" Value="2"></asp:ListItem>
                                <asp:ListItem Text="RUC" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                            <asp:Label ID="Label3" runat="server" Text="Identificación"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtIdentificacion" class="textboxNormal" runat="server" CssClass="form-control" MaxLength="13" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Primer Nombre"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPrimerNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label5" runat="server" Text="Segundo Nombre"></asp:Label>
                     
                            <asp:TextBox ID="txtSegundoNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>

                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label6" runat="server" Text="Primer Apellido"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtprimerApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Segundo Apellido"></asp:Label>
                       
                            <asp:TextBox ID="txtSegundoApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtMail" class="textboxNormal" runat="server" CssClass="form-control" TextMode="Email" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label10" runat="server" Text="Celular"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCelular" class="textboxNormal" runat="server" CssClass="form-control" MaxLength="10" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                            <br />
                            <asp:Label ID="Label11" runat="server" Text="Teléfono"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtTelefono" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label15" runat="server" Text="Fecha Nacimiento"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFechaNacimiento" class="textboxNormal" runat="server" CssClass="form-control" TextMode="Date" Width="80%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                            <br />
                            <asp:Label ID="Label16" runat="server" Text="Género"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlGenero" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Femenino" Value="2"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td style="text-align: left; width: 33%">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Rol"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <caption>
                        <tr>
                            <td style="text-align: left; width: 33%"></td>
                            <td style="text-align: center; width: 33%">
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" Text="Registrar" OnClick="btnGuardar_Click" />
                            </td>
                            <td style="text-align: left; width: 33%"></td>
                        </tr>
                    </caption>
                </table>
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
