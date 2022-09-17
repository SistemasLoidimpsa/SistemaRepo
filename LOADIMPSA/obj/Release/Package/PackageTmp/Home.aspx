<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LOIDIMPSA.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio</title>
    <meta name="facebook-domain-verification" content="x4ugnfcntyhxy6hctzi4ni9m66h4d4" />
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
    </style>
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
        fbq('track', 'ViewContent');
    </script>
    <noscript>
        <img height="1" width="1"
            src="https://www.facebook.com/tr?id=1111161526407188&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->
</head>
<body>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <div class="login100-pic js-tilt">

                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/inicio.png" Width="250%" Height="100%" />
                </div>

                <form class="login100-form validate-form" runat="server">
                    <span class="login100-form-title">
                        <%--<asp:Label ID="Label1" runat="server" ForeColor="#1D4289" Text="Inicio Sesión"></asp:Label>--%>
                        <br />
                        <br />
                       
                        <br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoo PNG.png" Width="80%" />
                    </span>

                    <div class="wrap-input100 validate-input" data-validate="Codigo de usuario es necesario">

                        <asp:TextBox ID="txtCodigo" runat="server" class="input100" type="text" name="email" placeholder="Código Usuario"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-user" aria-hidden="true"></i>
                        </span>
                    </div>
                    <div class="wrap-input100 validate-input" data-validate="Password es necesario">

                        <asp:TextBox ID="txtPass" runat="server" class="input100" TextMode="Password" name="pass" placeholder="Password"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock" aria-hidden="true"></i>
                        </span>
                    </div>
                    <div class="container-login100-form-btn">
                        <asp:Button ID="btnLogin" runat="server" class="login100-form-btn" Text="Ingresar" OnClick="btnLogin_Click" />
                    </div>
                    <div class="text-center p-t-12">
                        <br />
                        <a class="txt2" href="RegistroCliente.aspx">Crea tu cuenta con nosotros <i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
                        </a>
                        <br />
                        <br />
                        <br />
                        <a class="txt2" href="RecuperacionClave.aspx">Olvidaste tu contraseña?<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
                        </a>
                    </div>
                    <br />
                  
                </form>
            </div>
        </div>
        <%-- <div  style="background-color: #1D4289" align="center">
        <table>
            <tr>
                <td align="center" class="auto-style7">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mundo.png" widht="50%" Height="50%" />
                </td>
                <td align="center" class="auto-style8">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/rueda.png" widht="50%" Height="50%" />
                </td>
                <td align="center" class="auto-style6">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/usuario.png" widht="50%" Height="50%" />
                </td>
                <td align="center" class="auto-style9">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/images/muñeco.png" widht="50%" Height="50%" />
                </td>
                <td align="center" class="auto-style1">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/images/avion.png" widht="50%" Height="50%" />
                </td>
            </tr>
            <tr>
                <td align="center" class="auto-style7">
                    <asp:Label ID="Label1" runat="server" Text="SEGUIMIENTO Y" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label><br />

                    <asp:Label ID="Label3" runat="server" Text="ENTREGA" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label>
                </td>
                <td align="center" class="auto-style8">
                    <asp:Label ID="Label4" runat="server" Text="VUELOS TODAS " ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label><br />
                    <asp:Label ID="Label8" runat="server" Text="LAS SEMANAS" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label>
                </td>
                <td align="center" class="auto-style6">
                    <asp:Label ID="Label5" runat="server" Text="SEGURIDAD " ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label><br />
                    <asp:Label ID="Label9" runat="server" Text="GARANTIZADA" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label>
                </td>
                <td align="center" class="auto-style9">
                    <asp:Label ID="Label6" runat="server" Text="ATENCIÓN AL" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label><br />
                    <asp:Label ID="Label11" runat="server" Text="CLIENTE" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label>
                </td>
                <td align="center" class="auto-style1">
                    <asp:Label ID="Label7" runat="server" Text="OPERADOR FORMAL" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label><br />
                    <asp:Label ID="Label12" runat="server" Text="Y AUTORIZADO" ForeColor="White" Font-Bold="true" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
        </table>
    </div>--%>
    </div>




    <!--===============================================================================================-->
    <script src="vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/bootstrap/js/popper.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/tilt/tilt.jquery.min.js"></script>
    <script>
        $('.js-tilt').tilt({
            scale: 1.1
        })
    </script>
    <!--===============================================================================================-->
    <script src="js/main.js"></script>

</body>
</html>
