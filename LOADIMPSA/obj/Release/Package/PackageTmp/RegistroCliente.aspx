<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroCliente.aspx.cs" Inherits="LOADIMPSA.RegistroCliente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro Clientes</title>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <link rel="stylesheet" href="js/bootstrap.min.css" />
    <link rel="stylesheet" href="js/bootstrap.min.js" />
    <link rel="stylesheet" href="js/font-awesome.min.css" />
    <link rel="stylesheet" href="js/jquery.min.js" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="css/MenuLateral.css" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
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

        function validateCed(source, arguments) {
                numero = document.getElementById("txtIdentificacion").value;
                /* alert(numero); */
                
                var suma = 0;
                var residuo = 0;
                var pri = false;
                var pub = false;
                var nat = false;
                var numeroProvincias = 24;
                var modulo = 11;

                /* Verifico que el campo no contenga letras */
                var ok = 1;
                for (i = 0; i < numero.length && ok == 1; i++) {
                    var n = parseInt(numero.charAt(i));
                    if (isNaN(n)) ok = 0;
                }
                if (ok == 0) {
                    alert("No puede ingresar caracteres en el número");
                    arguments.IsValid = false;
                    return;
                }

                if (numero.length < 10) {
                    alert('El número ingresado no es válido');
                    arguments.IsValid = false;
                    return ;
                }
                
                /* Los primeros dos digitos corresponden al codigo de la provincia */
                provincia = numero.substr(0, 2);
                if (provincia < 1 || provincia > numeroProvincias) {
                    alert('El código de la provincia (dos primeros dígitos) es inválido');
                    arguments.IsValid = false;
                    return;
                }

                /* Aqui almacenamos los digitos de la cedula en variables. */
                d1 = numero.substr(0, 1);
                d2 = numero.substr(1, 1);
                d3 = numero.substr(2, 1);
                d4 = numero.substr(3, 1);
                d5 = numero.substr(4, 1);
                d6 = numero.substr(5, 1);
                d7 = numero.substr(6, 1);
                d8 = numero.substr(7, 1);
                d9 = numero.substr(8, 1);
                d10 = numero.substr(9, 1);

                /* El tercer digito es: */
                /* 9 para sociedades privadas y extranjeros   */
                /* 6 para sociedades publicas */
                /* menor que 6 (0,1,2,3,4,5) para personas naturales */

                if (d3 == 7 || d3 == 8) {
                    alert('El tercer dígito ingresado es inválido');
                    arguments.IsValid = false;
                    return;
                }

                /* Solo para personas naturales (modulo 10) */
                if (d3 < 6) {
                    nat = true;
                    p1 = d1 * 2; if (p1 >= 10) p1 -= 9;
                    p2 = d2 * 1; if (p2 >= 10) p2 -= 9;
                    p3 = d3 * 2; if (p3 >= 10) p3 -= 9;
                    p4 = d4 * 1; if (p4 >= 10) p4 -= 9;
                    p5 = d5 * 2; if (p5 >= 10) p5 -= 9;
                    p6 = d6 * 1; if (p6 >= 10) p6 -= 9;
                    p7 = d7 * 2; if (p7 >= 10) p7 -= 9;
                    p8 = d8 * 1; if (p8 >= 10) p8 -= 9;
                    p9 = d9 * 2; if (p9 >= 10) p9 -= 9;
                    modulo = 10;
                }

                /* Solo para sociedades publicas (modulo 11) */
                /* Aqui el digito verficador esta en la posicion 9, en las otras 2 en la pos. 10 */
                else if (d3 == 6) {
                    pub = true;
                    p1 = d1 * 3;
                    p2 = d2 * 2;
                    p3 = d3 * 7;
                    p4 = d4 * 6;
                    p5 = d5 * 5;
                    p6 = d6 * 4;
                    p7 = d7 * 3;
                    p8 = d8 * 2;
                    p9 = 0;
                }

                /* Solo para entidades privadas (modulo 11) */
                else if (d3 == 9) {
                    pri = true;
                    p1 = d1 * 4;
                    p2 = d2 * 3;
                    p3 = d3 * 2;
                    p4 = d4 * 7;
                    p5 = d5 * 6;
                    p6 = d6 * 5;
                    p7 = d7 * 4;
                    p8 = d8 * 3;
                    p9 = d9 * 2;
                }

                suma = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9;
                residuo = suma % modulo;

                /* Si residuo=0, dig.ver.=0, caso contrario 10 - residuo*/
                digitoVerificador = residuo == 0 ? 0 : modulo - residuo;

                /* ahora comparamos el elemento de la posicion 10 con el dig. ver.*/
            if (pub == true && numero.length > 10) {
                    if (digitoVerificador != d9 ) {
                        arguments.IsValid = false;
                        alert('El ruc de la empresa del sector público es incorrecto.');
                        return ;
                    }
                    /* El ruc de las empresas del sector publico terminan con 0001*/
                    if (numero.substr(9, 4) != '0001') {
                        arguments.IsValid = false;
                        alert('El ruc de la empresa del sector público debe terminar con 0001');
                        return ;
                    }
                }
                else if (pri == true) {
                    if (digitoVerificador != d10) {
                        arguments.IsValid = false;
                        alert('El ruc de la empresa del sector privado es incorrecto.');
                        return ;
                    }
                    if (numero.substr(10, 3) != '001') {
                        arguments.IsValid = false;
                        alert('El ruc de la empresa del sector privado debe terminar con 001');
                        return ;
                    }
                }

            else if (nat == true && numero.length < 10) {
                    if (digitoVerificador != d10) {
                        arguments.IsValid = false;
                        alert('El número de cédula de la persona natural es incorrecto.');
                        return ;
                    }
                    if (numero.length > 10 && numero.substr(10, 3) != '001') {
                        arguments.IsValid = false;
                        alert('El ruc de la persona natural debe terminar con 001');
                        return ;
                    }
            }
            arguments.IsValid = true;
                return ;   
           }


        function verificaElemento(ddl) {
            combobox = ddl.value;
            var lbNombre = document.getElementById("<%=Label4.ClientID %>");
            var lbsgNombre = document.getElementById("<%=Label5.ClientID %>");
            var lbApellido = document.getElementById("<%=Label6.ClientID %>");
            var lbsegApellido = document.getElementById("<%=Label7.ClientID %>");
            var txtsgNombre= document.getElementById("<%=txtSegundoNombre.ClientID %>");
            var txtpApellido = document.getElementById("<%=txtprimerApellido.ClientID %>");
            var txtsegApellido = document.getElementById("<%=txtSegundoApellido.ClientID %>");
            if (combobox == '2') {
                lbNombre.innerHTML = "Razón Social";
                lbApellido.innerHTML = "Nombre Comercial";
                txtIdentificacion.value = "";
                txtsgNombre.disabled = true;
                txtsegApellido.disabled = true;
                txtsgNombre.value = "";
                txtsegApellido.value = "";
                document.getElementById("txtIdentificacion").setAttribute("maxlength", "13");
            }
            /*else if (combobox == "2") {
                lbNombre.innerHTML = "Primer Nombre";
                lbApellido.innerHTML = "Segundo Nombre";
                txtIdentificacion.value = "";
                document.getElementById("txtIdentificacion").setAttribute("maxlength", "13");
                txtsgNombre.disabled = false;
                txtsegApellido.disabled = false;
               <asp:ListItem Text="Pasaporte" Value="2"></asp:ListItem>
            }*/
            else if (combobox = "1") {
                lbNombre.innerHTML = "Primer Nombre";
                lbApellido.innerHTML = "Segundo Nombre";
                txtIdentificacion.value = "";
                document.getElementById("txtIdentificacion").setAttribute("maxlength", "10");
                txtsgNombre.disabled = false;
                txtsegApellido.disabled = false;
            }

        }





    </script>
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
        fbq('track', 'CompleteRegistration');
    </script>
    <noscript>
        <img height="1" width="1"
            src="https://www.facebook.com/tr?id=1111161526407188&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->
    <style>
        .tituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #fff;
            font-size: 40px;
        }

        .subtituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 14px;
            font-weight: bold;
        }

        .textboxNormal {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-default" role="navigation" style="background: linear-gradient(59deg, rgba(255,255,255,1) 27%, rgba(255,205,0,1) 57%); font-family: 'Trajan Pro'; font-weight: bolder; color: white; font-size: 15px; flood-color: white;">
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-left">
                        <li>
                            <img src="images/logoo PNG.png" width="220" />
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li style="padding: 25px">
                            <asp:Label ID="lblCedula" Text="Registro Clientes" runat="server" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppRegistroCliente" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <asp:Panel ID="pnlFinal" runat="server" Width="100%" HorizontalAlign="Center" Visible="false">
                    <div style="text-align: left; width: 50%; margin-left: auto; margin-right: auto">
                        <asp:Label ID="Label22" runat="server" Text="Estimado/a:">
                        </asp:Label><br />
                        <br />
                        <asp:Label ID="lblNombrFinal" runat="server" Font-Bold="true"></asp:Label>
                        <br />
                        <br>
                        <asp:Label ID="Label24" runat="server" Text="Su solicitud de Registro ha sido registrada correctamente, por favor revise su correo registrado donde se remitirá toda la información de acceso al sistema."></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label25" runat="server" Text="Es muy importante para nosotros que forme parte de nuestra gran familia."></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlInfo" runat="server" HorizontalAlign="Center" Width="100%" Visible="true">
                    <table style="margin-left: auto; margin-right: auto; width: 50%">
                        <tr>
                            <td style="text-align: left" colspan="3">
                                <asp:Label ID="Label1" runat="server" Text="Crea tu Cuenta" CssClass="subtituloHeader"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%">
                                <asp:Label ID="Label2" runat="server" Text="Tipo Documento"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlTipoIdentificacion" class="textboxNormal" runat="server" CssClass="form-control" Width="80%" onchange="verificaElemento(this)">
                                    <asp:ListItem Text="Cédula" Value="1"></asp:ListItem>
                                    
                                    <asp:ListItem Text="RUC" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                           
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                                <asp:Label ID="Label3" runat="server" Text="Identificac&oacute;n"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtIdentificacion" class="textboxNormal" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" MaxLength="10"  Width="80%"></asp:TextBox>
                                 <br />
                                <asp:CustomValidator id="vltIden" runat="server"
                                                       ControlToValidate="txtIdentificacion"
                                                       ClientValidationFunction="validateCed"
                                                       ForeColor="Red"
                                                       Font-Name="verdana"
                                                       ErrorMessage="Error por favor ingrese una cedula, ruc o pasaporte  válido" 
                                                       ></asp:CustomValidator>
                               
                            </td>
                            <td style="text-align: left; width: 33%"></td>
                        </tr>

                        <tr>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="Primer Nombre"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPrimerNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" id="vltName" ForeColor="Red"
                                    controltovalidate="txtPrimerNombre"  ValidationExpression="^(?!.* (?: |$))[a-zA-Z]+$"
                                    Font-Names="Verdana"
                                    errormessage="Por favor digite su Primer nombre sin tildes, adicional no dejar espacios en blanco" ></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                                <br />
                                <asp:Label ID="Label5" runat="server" Text="Segundo Nombre"></asp:Label>

                                <asp:TextBox ID="txtSegundoNombre" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator1" ForeColor="Red"
                                    controltovalidate="txtSegundoNombre" validationexpression="^(?!.* (?: |$))[a-zA-Z]+$"
                                    Font-Names="Verdana"
                                    errormessage="Por favor digite su Primer nombre sin tildes, adicional no dejar espacios en blanco" ></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: left; width: 33%"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Primer Apellido"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtprimerApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator2" ForeColor="Red"
                                    controltovalidate="txtprimerApellido" validationexpression="^(?!.* (?: |$))[a-zA-Z]+$"
                                    Font-Names="Verdana"
                                    errormessage="Por favor digite su Primer nombre sin tildes, adicional no dejar espacios en blanco" ></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                                <br />
                                <asp:Label ID="Label7" runat="server" Text="Segundo Apellido"></asp:Label>

                                <asp:TextBox ID="txtSegundoApellido" class="textboxNormal" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator3" ForeColor="Red"
                                    controltovalidate="txtSegundoApellido" validationexpression="^(?!.* (?: |$))[a-zA-Z]+$"
                                    Font-Names="Verdana"
                                    errormessage="Por favor digite su Primer nombre sin tildes, adicional no dejar espacios en blanco" ></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: left; width: 33%"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="3">
                                <br />
                                <br />
                                <asp:Label ID="Label8" runat="server" Text="Datos Cuenta" CssClass="subtituloHeader"></asp:Label>
                                <br />
                                <br />
                            </td>
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
                                <asp:TextBox ID="txtCelular" class="textboxNormal" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" MaxLength="10" Width="80%"></asp:TextBox>
                                
                            </td>
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                                <br />
                                <asp:Label ID="Label11" runat="server" Text="Telèfono Convencional"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTelefono" class="textboxNormal" runat="server"  onkeypress="return isNumberKey(event)" CssClass="form-control"   MaxLength="10" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <asp:Label ID="Label12" runat="server" Text="Provincia"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlProvincia" class="textboxNormal" runat="server" AutoPostBack="true" CssClass="form-control" Width="80%"
                                    OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">

                                <br />
                                <asp:Label ID="Label13" runat="server" Text="Ciudad"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlCantones" class="textboxNormal" runat="server" CssClass="form-control"
                                    Width="80%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 33%"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%" colspan="3">
                                <br />
                                <asp:Label ID="Label14" runat="server" Text="Dirección Entrega" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDireccion" class="textboxNormal" placeholder="Ingrese la Direccion en Ecuador donde vas a recibir tu producto." runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
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
                            <td style="text-align: left; width: 33%; margin-left: auto; margin-right: auto">
                                <br />
                                <asp:Label ID="Label17" runat="server" Text="Como nos encontraste"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlMarketing" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                    <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                    <asp:ListItem Text="Instagram" Value="Instagram"></asp:ListItem>
                                    <asp:ListItem Text="Prensa Escrita" Value="Prensa Escrita"></asp:ListItem>
                                    <asp:ListItem Text="Radio y TV" Value="Radio y TV"></asp:ListItem>
                                    <asp:ListItem Text="Tik Tok" Value="Tik Tok"></asp:ListItem>
                                        <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                     <asp:ListItem Text="Recomendación de Amigos" Value="Recomendación de Amigos"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="3">
                                <br />
                                <br />
                                <asp:Label ID="Label18" runat="server" Text="Información Adicional (Términos y Condiciones)" CssClass="subtituloHeader"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%" colspan="3">
                                <br />
                                <asp:CheckBox ID="chkAcuerdo" runat="server" />
                                <asp:HyperLink ID="hypAcuerdo" runat="server" Target="_blank" Text="Acepto términos y Condiciones ( conócelos en nuestra página web )" NavigateUrl="https://loidimpsa.com/terminos-condiciones-loidimpsa-express/"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <br />
                            </td>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" CssClass="login100-form-btn" Text="Registrarse" />
                            </td>
                            <td style="text-align: left; width: 33%">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
