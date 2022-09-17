<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Menu.Master" CodeBehind="EnviosGestion.aspx.cs" Inherits="LOADIMPSA.EnviosGestion" %>

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
            top: 30%;
            left: 30%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
            position: fixed;
            z-index: 999;
        }

        .grdSearchResultbreakword {
            word-wrap: break-word;
            word-break: break-all;
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
            font-size: 11px;
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
        .cssContent {
            background-color: #D3DEEF;
            font-family: 'Montserrat', sans-serif;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 98%;
            height: 100%;
        }

        .cssHeaderSelected {
            background-color: #1D4289;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 50px;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_minus.png);
            background-repeat: no-repeat;
            background-position: right;
        }

        .cssHeader {
            background-color: #009CDE;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 30PX%;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_plus.png);
            background-repeat: no-repeat;
            background-position: right;
        }
         .float {
            color: #fff;
            position: fixed;
            width: 60px;
            height: 60px;
            bottom: 40px;
            right: 40px;
            background-color: #1D4289;
            
            border-radius: 50px;
            text-align: center;
            font-size: 30px;
            box-shadow: 2px 2px 3px #999;
            z-index: 100;
        }

            .float:hover {
                text-decoration: none;
               
                background-color:#1D4289;
                 color: #fff;
            }

        .my-float {
            margin-top: 16px;   
            color: #fff;
             
        }
    </style>
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

        function MuestraOculta(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "images/close.gif";
                }
                else {
                    img.src = "images/close.gif";
                }
                img.alt = "Ocultar detalles";
            }
            else {
                //const strMaterias = document.getElementById('_materias').value;
                //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "images/detail.gif";
                }
                else {
                    img.src = "images/detail.gif";
                }
                img.alt = "Mostrar detalles";
            }
        }


        function VerFile(nameFile) {
            console.log(cedula + nameFile);

            /*   var div = document.getElementById(obj);
               var img = document.getElementById('img' + obj);
   
               if (div.style.display == "none") {
                   //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                   div.style.display = "block";
                   if (row == 'alt') {
                       img.src = "images/close.gif";
                   }
                   else {
                       img.src = "images/close.gif";
                   }
                   img.alt = "Ocultar detalles";
               }
               else {
                   //const strMaterias = document.getElementById('_materias').value;
                   //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                   div.style.display = "none";
                   if (row == 'alt') {
                       img.src = "images/detail.gif";
                   }
                   else {
                       img.src = "images/detail.gif";
                   }
                   img.alt = "Mostrar detalles";
               }*/
        }

    </script>
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
            <asp:Panel runat="server">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="GESTIÓN DE ENVIOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                      <br />
           
                <table style="width: 70%; margin-left: auto; margin-right: auto">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Fecha Inicio" CssClass="subtituloHeader"></asp:Label>
                        
                        <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Fecha Fin:" CssClass="subtituloHeader"></asp:Label>
                        <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Empresa de Envios: " CssClass="subtituloHeader"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmpresaEnvio" class="textboxNormal" runat="server" CssClass="form-control" Width="95%">
                            <asp:ListItem Text="TODOS" Value="TODOS"></asp:ListItem>
                            <asp:ListItem Text="SERVIENTREGA" Value="SERVIENTREGA"></asp:ListItem>
                            <asp:ListItem Text="LOIDIMP S.A." Value="LOIDIMP S.A."></asp:ListItem>
                            <asp:ListItem Text="OTROS" Value="OTROS"></asp:ListItem>

                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />

                    </td>
                </tr>
            </table>
              
                <asp:Panel ID="pnlListadoHistorial" runat="server" HorizontalAlign="Center" Height="800px" Width="100%" Visible="false" ScrollBars="Vertical">
                     <br />
          
                 
                    <br />
                    <br />
                      <asp:Panel runat="server" Width="70%" HorizontalAlign="Center" Style="margin-left: auto; margin-right: auto">
                    <asp:GridView ID="gvEnviosClientes" runat="server" HorizontalAlign="Center"
                        Font-Size="X-Small"
                        AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="dtgEnvios_RowDataBound"
                        ShowHeaderWhenEmpty="True"
                        CssClass="mGrid"
                        EmptyDataText="No hay envios a presentar.">
                        <Columns>

                            <asp:BoundField DataField="idEnvio" HeaderText="ID ENVIO" />
                            <asp:BoundField DataField="nombres" HeaderText="NOMBRE" />

                            <asp:BoundField DataField="numSeguim" HeaderText="NUMERO DE SEGUIMIENTO" />

                            <asp:BoundField DataField="empresa" HeaderText="EMPRESA" />
                            <asp:BoundField DataField="provincia" HeaderText="PROVINCIA" />
                            <asp:BoundField DataField="canton" HeaderText="CANTON" />

                            <asp:BoundField DataField="fechaRegistroCliente" HeaderText="FECHA REGISTRO" />
                            <asp:BoundField DataField="userRegistro" HeaderText="USUARIO REGISTRO" />

                        </Columns>
                        <FooterStyle BackColor="#D5D8DC" />
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                              </asp:Panel>
                </asp:Panel>


            </asp:Panel>
  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>