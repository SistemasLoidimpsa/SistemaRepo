<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperacionClave.aspx.cs" Inherits="LOADIMPSA.RecuperacionClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="css/util.css" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
    <!--===============================================================================================-->
    <style type="text/css">
        .auto-style1 {
            width: 56%
        }

        .auto-style6 {
            width: 21%
        }

        .auto-style7 {
            width: 18%
        }

        .auto-style8 {
            width: 20%
        }

        .auto-style9 {
            width: 23%
        }
        @media (max-width:700px) {
			#txtCedulaUsuario {
				width: 120%;
				padding: 0 5px 0 0px;
			}
		}
		
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <link rel="stylesheet" href="css/main.css" type="text/css" />
        <asp:Panel ID="pnlActualizacion" runat="server" DefaultButton="btnRecuperar">
            <br />
            <br />

            <div style="width: 60%; height: 500px; margin-right: auto; margin-left: auto; border: groove; text-align: center">
                <br />
                <br />
                <asp:Image ID="Image2" ImageUrl="~/images/logoo PNG.png" runat="server" Width="20%" ImageAlign="Middle" />
                <br />
                <br />
                <span class="login100-form-title p-b-34" style="font-weight: bolder; color: #1D4289; font-size: 25px">RECUPERACIÓN DE CLAVE
                </span>

                <table style="width: 70%; margin-right: auto; margin-left: auto; border-radius: 3px 3px; border-color: #036;">
                    <tr>
                        <td>
                            <asp:Label ID="lblCedula" runat="server" Text="Ingrese su cédula:" CssClass="SubTitulo" ForeColor="#1D4289" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        </td>
                        <td>
                            <br />
                            <div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20" data-validate="Ingrese su cédula" style="width: 100%">
                                <asp:TextBox ID="txtCedulaUsuario" runat="server" Font-Size="Medium" CssClass="input100" MaxLength="10" placeholder="Ejm. 1720146735"></asp:TextBox>
                                <span class="focus-input100"></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMensaje" runat="server" Text="Su clave fue enviada correctamente a su correo personal." CssClass="SubTitulo" Font-Bold="true" Font-Size="medium" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; margin-right: auto; margin-left: auto">
                            <br />
                            <asp:Button ID="btnRecuperar" runat="server" Width="100%" Text="Recuperar" BackColor="#1D4289" OnClick="btnRecuperar_Click" Style="text-align: center" CssClass="login100-form-btn" />
                            <br />

                        </td>
                    </tr>
                </table>
            </div>

            <div style="width: 100%">
                <div style="width: 60%">
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
