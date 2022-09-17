<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActualizacionClave.aspx.cs" Inherits="LOADIMPSA.ActualizacionClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <style type="text/css">
        #elem{
            width: 60%; 
            height: 100%; 
            margin-right: auto; 
            margin-left: auto; 
            border: groove; 
            text-align: center;
        }

        

        @media (max-width:700px) {
			
            .input100 {
				width: 130%;
				padding: 0 30px 0 40px;
			}
            #elem{
                width: 90%;
            }
		}

        .input100{
            padding: 0 30px 0 40px;
        }

        

    </style>

    <asp:Panel ID="pnlActualizacion" runat="server" DefaultButton="btnActualizar">

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="text-center p-t-136 " id="elem" >
            <asp:Image ID="Image2" ImageUrl="~/images/logoo PNG.png" runat="server" Width="20%" ImageAlign="Middle" />
            
            <span class="login100-form-title p-b-34" style="font-weight: bolder; color: #1D4289">ACTUALIZACIÓN DE CLAVE
            </span>
         
            <table style="width: 65%; margin-right: auto; margin-left: auto;">
                <tr>
                    <td>
                        <asp:Label ID="lblPassAnti" runat="server" Text="Ingrese su clave vigente:" CssClass="SubTitulo"></asp:Label>
                    </td>
                    <td>

                        <div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20" data-validate="Ingrese su Password">
                            <asp:TextBox ID="txtPassAnti" runat="server" CssClass="input100" TextMode="Password" placeholder=" Clave por defecto"></asp:TextBox>
                            <span class="focus-input100"></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="rfPassAnt" runat="server" ErrorMessage="Campo obligatorio"
                            ForeColor="Red" Font-Names="Verdana" ControlToValidate="txtPassAnti"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPassNew" runat="server" Text="Ingrese su nueva clave:" CssClass="SubTitulo"></asp:Label>
                    </td>
                    <td>
                        <div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20" data-validate="Ingrese su Password">
                            <asp:TextBox ID="txtPassNew" runat="server" CssClass="input100" TextMode="Password" placeholder="Clave Nueva"></asp:TextBox>
                            <span class="focus-input100"></span>
                        </div>
                    </td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="rfNewPass" runat="server" ErrorMessage="Campo obligatorio"
                            ForeColor="Red" Font-Names="Verdana" ControlToValidate="txtPassNew"></asp:RequiredFieldValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblConfirmNew" runat="server" Text="Confirme su nueva clave:" CssClass="SubTitulo"></asp:Label>
                    </td>
                    <td>

                        <div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20" data-validate="Ingrese su Password">
                            <asp:TextBox ID="txtConfirmNew" runat="server" CssClass="input100" TextMode="Password" placeholder="Repita la Clave Nueva"></asp:TextBox>
                            <span class="focus-input100"></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="rfVerifica" runat="server" ErrorMessage="Campo obligatorio"
                            ForeColor="Red" Font-Names="Verdana" ControlToValidate="txtPassNew"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="cvNewPass" runat="server" ErrorMessage="Las claves no coinciden"
                            ForeColor="Red" Font-Names="Verdana" ControlToCompare="txtPassNew" ControlToValidate="txtConfirmNew"></asp:CompareValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnIngreso_Click" CssClass="login100-form-btn" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br /><br /><br /><br /><br /><br /><br /><br />
    </asp:Panel>
</asp:Content>
