<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RespRechazo.aspx.cs" Inherits="LOADIMPSA.respAprobada" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Respuesta Aprobada</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/security.png" />
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


        html {
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
            box-sizing: inherit;
        }

        .tilt {
            
            transform-style: preserve-3d;
            margin: 50px auto;
        }

        .tilt-logo {
            width: 200px;
            height: 200px;
            background-color: #7e56ff;
            background-image: linear-gradient(150deg, #5a00ff 0%, #ff1ff7 100%, #ff1ff7 100%);
        }

        .tilt-logo-inner {
            transform: translateZ(50px) translateY(-50%) translateX(-50%);
            position: absolute;
            top: 50%;
            left: 50%;
            color: white;
            font-size: 1.9rem;
        }

        .tilt-movie, .tilt-movie img {
            width: 312px;
            height: 312px;
        }


        #main {
            width: 50%;
            max-width: 100%;
            margin: 13em auto 0;
            padding: 1em 0 1em 5em;
        }

        #main h1 {
            font-family: sans-serif;
            text-align: center;
            font-weight: bold;
        }

        .ja {
            color: #FF0000;
             font-size: 0.7em;
        }

        .soft {
            color: crimson;
        }

        .org {
            font-size: 0.7em;
            color: darkslategrey;
        }

       
    </style>
</head>
<body>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">

                <form class="login100-form validate-form" runat="server">
                    <span class="login100-form-title2">
                        <%--<asp:Label ID="Label1" runat="server" ForeColor="#1D4289" Text="Inicio Sesión"></asp:Label>--%>
                        <br />
                        <br />

                        <br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoo PNG.png" Width="80%" />
                    </span>

                    <div class="tilt tilt-movie" data-tilt data-tilt-glare="true" data-tilt-scale="1.1">

                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/credit-card-icon.png" />
                    </div>



                    <br />

                </form>
                <div id="main">
                    <h1><span class="ja">ERROR!</span>      </h1>             
                    <br>
                    <p align="justify">Se ha presentado un error con el codigo de tu targeta, revisa tus datos e ingresalos correctamente.</p>
                    <br><p align="center">Gracias por usar nuestros Servicios .</p>
                   
                </div>
            </div>
        </div>
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
